using ParkingManagementSystem.Abstractions;
using ParkingManagementSystem.Exceptions;
using ParkingManagementSystem.Infrastructure;
using ParkingManagementSystem.Models;

namespace ParkingManagementSystem.Implementations
{
    internal class BookingService : IBookingService
    {
        private readonly IPaymentService _paymentService;
        private readonly AppDbContext _context;
        public BookingService(IPaymentService paymentService, AppDbContext context)
        {
            _paymentService = paymentService;
            _context = context;
        }

        public async Task<string> BookParkingAsync(string plateNumber, Guid siteId, DateTimeOffset from, DateTimeOffset to, string cardNumber)
        {
            ValidateBooking(plateNumber, siteId, from, to);

            var tariff = GetTariff(siteId);
            var amount = tariff is null ? 0 : tariff.Calculate(TimeOnly.FromDateTime(from.UtcDateTime), TimeOnly.FromDateTime(to.UtcDateTime));

            if (amount > 0)
            {
                var paymentResult = await _paymentService.ProcessPaymentAsync(cardNumber, amount);
                if (!paymentResult.IsSuccessful)
                {
                    throw new Exception("Payment failed. Booking cannot be completed.");
                }
            }

            var site = _context.Sites.Where(s => s.Id == siteId).First();
            var ticket = Ticket.Create(plateNumber, from, to, amount, siteId, tariff?.Id);

            _context.Tickets.Add(ticket);
            _context.SaveChanges();

            return ticket.Id.ToString();
        }

        /// <summary>
        /// Validates the booking details
        /// </summary>
        /// <param name="plateNumber"></param>
        /// <param name="siteId"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        private void ValidateBooking(string plateNumber, Guid siteId, DateTimeOffset from, DateTimeOffset to)
        {
            ValidatePlateNumber(plateNumber);
            ValidateSiteExistence(siteId);
            ValidateBookingTime(from, to);
            ValidateOverlappingTickets(siteId, plateNumber, from, to);
        }
        private void ValidatePlateNumber(string plateNumber)
        {
            if (string.IsNullOrWhiteSpace(plateNumber))
                throw new INvalidPlateNumberException("Plate number cannot be null or empty or whitespace.");
            // we can here also put rules for the plate number
        }
        private void ValidateBookingTime(DateTimeOffset from, DateTimeOffset to)
        {
            if (from < DateTimeOffset.UtcNow)
                throw new InvalidBookingTimeException("Booking cannot be in the past");
            if (to <= from)
                throw new InvalidBookingTimeException("To must be after from");
            if ((to - from) > TimeSpan.FromHours(24))
                throw new InvalidBookingTimeException("Max Duration for Booking is 24");
        }
        private void ValidateSiteExistence(Guid siteId)
        {
            var site = _context.Sites.Where(s => s.Id == siteId).FirstOrDefault();
            if (site is null)
                throw new InvalidSiteException("Ther is no site with that Id");
        }
        private void ValidateOverlappingTickets(Guid siteId, string plateNumber, DateTimeOffset from, DateTimeOffset to)
        {
            var tickets = _context.Tickets.Where(t => t.PlateNumber == plateNumber && t.Site.Id == siteId);

            foreach (var ticket in tickets)
            {
                bool overlap = (from < ticket.To)
                             && (to > ticket.From);

                if (overlap)
                    throw new OverlappingTicketException("that plate numebr already has valid Ticket in this Site");
            }
        }

        private Tariff GetTariff(Guid siteId)
        {
            return _context.Tariffs.FirstOrDefault(t => t.Site.Id == siteId);
        }
    }
}
