using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParkingManagementSystem.Abstractions;
using ParkingManagementSystem.Models;

namespace ParkingManagementSystem.Implementations
{
    internal class BookingService
    {
        private readonly IPaymentService _paymentService;
        private readonly IDataBase _database;
        public BookingService(IPaymentService paymentService, IDataBase database)
        {
            _paymentService = paymentService;
            _database = database;
        }

        public async Task<string> BookParkingAsync(string plateNumber, Guid siteId, DateTime from, DateTime to, string cardNumber)
        {
            ValidateBooking(plateNumber, siteId, from, to);

            var tariff = GetTariff(siteId);
            var amount = tariff.Calculate(TimeOnly.FromDateTime(from), TimeOnly.FromDateTime(to));

            if(amount> 0)
            {
                var paymentResult = await _paymentService.ProcessPaymentAsync(cardNumber, amount);
                if (!paymentResult.IsSuccessful)
                {
                    throw new Exception("Payment failed. Booking cannot be completed.");
                }
            }
            
            var ticket=Ticket.Create(plateNumber,from,to,amount);

            await _database.SaveTicket(ticket);

            return ticket.Id.ToString();
        }

        /// <summary>
        /// Validates the booking details
        /// </summary>
        /// <param name="plateNumber"></param>
        /// <param name="siteId"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        private void ValidateBooking(string plateNumber, Guid siteId, DateTime from, DateTime to)
        {



        }

        private Tariff GetTariff(Guid siteId)
        {
            return _database.GetTariffs(null).FirstOrDefault(t => t.Site.Id == siteId)
                ?? throw new Exception("Tariff not found for the specified site.");
        }

    }
}
