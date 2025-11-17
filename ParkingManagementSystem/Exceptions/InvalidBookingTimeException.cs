namespace ParkingManagementSystem.Exceptions
{
    public class InvalidBookingTimeException : Exception
    {
        public InvalidBookingTimeException(string message) : base(message) { }

    }
}