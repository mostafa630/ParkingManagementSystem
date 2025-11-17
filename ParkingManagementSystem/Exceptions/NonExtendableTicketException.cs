namespace ParkingManagementSystem.Exceptions
{
    public class NonExtendableTicketException : Exception
    {
        public NonExtendableTicketException(string message) : base(message) { }
    }
}