using ParkingManagementSystem.Exceptions;
using ParkingManagementSystem.Settings;
namespace ParkingManagementSystem.Models
{
    internal class Ticket
    {
        public Ticket(Guid id, string plateNumber, DateTimeOffset from, DateTimeOffset to, decimal price, bool isExtend, Guid siteId, Guid? tariffId)
        {
            Id = id;
            PlateNumber = plateNumber;
            From = from;
            To = to;
            Price = price;
            IsExtend = isExtend;
            SiteId = siteId;
            TariffId = tariffId ?? Guid.Empty;
        }

        public Guid Id { get; private set; }
        public string PlateNumber { get; private set; }
        public DateTimeOffset From { get; private set; }
        public DateTimeOffset To { get; private set; }
        public decimal Price { get; private set; }
        public bool IsExtend { get; private set; }
        
        public Guid SiteId { get; set; }
        public Site Site { get; private set; }

        public Guid? TariffId { get; set; }
        public Tariff? Tariff { get; private set; }

        public static Ticket Create(string plateNumber, DateTimeOffset from, DateTimeOffset to, decimal price, Guid siteId, Guid? tariffId)
        {
            return new Ticket(Guid.NewGuid(), plateNumber, from, to, price, false, siteId, tariffId);
        }
        public Ticket Extend(DateTimeOffset newTo, decimal additionalPrice)
        {
            if (!IsExtendable())
                throw new NonExtendableTicketException("Time now exceeds allowed time to extend the ticket");

            return new Ticket(Guid.NewGuid(), this.PlateNumber, this.To, newTo, additionalPrice, true, this.SiteId, this.TariffId);
        }

        public bool IsExtendable()
        {
            return DateTimeOffset.UtcNow <= To + TicketSettings.GracePeriod;
        } 
    }
}