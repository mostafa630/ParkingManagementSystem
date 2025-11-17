using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingManagementSystem.Models
{
    internal class Ticket
    {
        public Ticket(Guid id, string plateNumber, DateTime from, DateTime to, decimal price,bool isExtend)
        {
            Id = id;
            PlateNumber = plateNumber;
            From = from;
            To = to;
            Price = price;
            IsExtend = isExtend;
        }

        public Guid Id { get;private set; }
        public string PlateNumber { get; private set; }
        public DateTime From { get; private set; }
        public DateTime To { get; private set; }
        public decimal Price { get; private set; }
        public bool IsExtend { get;private set; }

        public static Ticket Create(string plateNumber, DateTime from, DateTime to, decimal price)
        {
            return new Ticket(Guid.NewGuid(),plateNumber,from,to,price,false);
        }
        public Ticket Extend(DateTime newTo, decimal additionalPrice)
        {
            return new Ticket(this.Id, this.PlateNumber, this.To, newTo,additionalPrice,true);
        }

        /// <summary>
        /// TODO: Implement method to check if the ticket is extendable
        /// </summary>
        /// <returns></returns>
        public bool IsExtendable()
        {

        }
    }
}
