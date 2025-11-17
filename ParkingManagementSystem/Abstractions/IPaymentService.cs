using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingManagementSystem.Abstractions
{
    internal interface IPaymentService
    {
        public Task<PaymentResult> ProcessPaymentAsync(string cardNumber, decimal amount);
       
    }
}
