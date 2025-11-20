namespace ParkingManagementSystem.Models
{
    internal class Tariff
    {
        public Tariff(Guid id, Guid siteId, decimal firstHour, decimal additionalHour)
        {
            Id = id;
            SiteId = siteId;
            FirstHour = firstHour;
            AdditionalHour = additionalHour;
        }

        public Guid Id { get; private set; }
        public decimal FirstHour { get; private set; }
        public decimal AdditionalHour { get; private set; }
        public Guid SiteId { get; set; }
        public Site Site { get; private set; }

        public decimal Calculate(TimeOnly from, TimeOnly to)
        {
            int minutes = (to.Hour * 60 + to.Minute) - (from.Hour * 60 + from.Minute);

            if (minutes < 0) // that mean that to is in the day after from
            {
                minutes += 24 * 60;
            }

            decimal price = 0;
            if (minutes < 60)
            {
                price = (minutes * FirstHour) / 60;
            }
            else
            {
                price = FirstHour;
                int remaningMinutes = minutes - 60;
                decimal additionalPrice = (remaningMinutes * AdditionalHour) / 60;
                price += additionalPrice;
            }
            return price;
        }
    }
}
