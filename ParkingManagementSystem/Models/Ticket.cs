using ParkingManagementSystem.Exceptions;
using ParkingManagementSystem.Settings;
namespace ParkingManagementSystem.Models
{
    internal class Ticket
    {
        public Ticket(Guid id, string plateNumber, DateTimeOffset from, DateTimeOffset to, decimal price, bool isExtend, Site site, Tariff? tariff)
        {
            Id = id;
            PlateNumber = plateNumber;
            From = from;
            To = to;
            Price = price;
            IsExtend = isExtend;
            Site = site;
            Tariff = tariff;
        }

        public Guid Id { get; private set; }
        public string PlateNumber { get; private set; }
        public DateTimeOffset From { get; private set; }
        public DateTimeOffset To { get; private set; }
        public decimal Price { get; private set; }
        public bool IsExtend { get; private set; }
        public Site Site { get; private set; }
        public Tariff? Tariff { get; private set; } = null;

        public static Ticket Create(string plateNumber, DateTimeOffset from, DateTimeOffset to, decimal price, Site site, Tariff? tariff)
        {
            return new Ticket(Guid.NewGuid(), plateNumber, from, to, price, false, site, tariff);
        }
        public Ticket Extend(DateTimeOffset newTo, decimal additionalPrice)
        {
            if (!IsExtendable())
                throw new NonExtendableTicketException("Time now exceeds allowed time to extend the ticket");

            return new Ticket(this.Id, this.PlateNumber, this.To, newTo, additionalPrice, true, this.Site, this.Tariff);
        }

        public bool IsExtendable()
        {
            return DateTimeOffset.UtcNow <= To + TicketSettings.GracePeriod;
        }
    }
}
