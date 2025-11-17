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
