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

        }

            
    }
}
