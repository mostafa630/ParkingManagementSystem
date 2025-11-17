using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingManagementSystem.Models
{
    internal class Tariff
    {
        public Tariff(Guid id, Site site, decimal firstHour, decimal additionalHour)
        {
            Id = id;
            Site = site;
            FirstHour = firstHour;
            AdditionalHour = additionalHour;
        }

        public Guid Id { get; private set; }
        public Site Site { get; private set; }
        public decimal FirstHour { get; private set; }
        public decimal AdditionalHour { get; private set; }

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
