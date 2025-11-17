using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingManagementSystem.Abstractions
{
    internal class PaymentResult
    {
        public PaymentResult(bool isSuccessful, string? refNumber)
        {
            IsSuccessful = isSuccessful;
            RefNumber = refNumber;
        }

        public bool IsSuccessful { get; private set; }
        public string? RefNumber { get; private set; }

    }
}
