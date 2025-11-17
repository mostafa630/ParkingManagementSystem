using ParkingManagementSystem.Abstractions;

namespace ParkingManagementSystem.Implementations
{
    internal class PaymentService : IPaymentService
    {
        public PaymentService()
        {

        }

        public async Task<PaymentResult> ProcessPaymentAsync(string cardNumber, decimal amount)
        {
            if (string.IsNullOrWhiteSpace(cardNumber) || amount <= 0)
            {
                return new PaymentResult(false, null);
            }

            await Task.Delay(2000);

            if (cardNumber.EndsWith("0"))
            {
                return new PaymentResult(false, null);
            }

            await Task.Delay(1000);
            var referenceNumber = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 10).ToUpper();
            return new PaymentResult(true, referenceNumber);
        }
    }
}
