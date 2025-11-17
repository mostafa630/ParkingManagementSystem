using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingManagementSystem.Abstractions
{
    internal interface IBookingService
    {
        public Task<string> BookParkingAsync(string plateNumber, string siteId, DateTime from, DateTime to, string cardNumber);
    }
}
