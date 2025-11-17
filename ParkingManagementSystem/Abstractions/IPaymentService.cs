namespace ParkingManagementSystem.Abstractions
{
    internal interface IPaymentService
    {
        public Task<PaymentResult> ProcessPaymentAsync(string cardNumber, decimal amount);

    }
}
