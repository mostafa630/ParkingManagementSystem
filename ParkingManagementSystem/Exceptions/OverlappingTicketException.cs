namespace ParkingManagementSystem.Exceptions
{
    public class OverlappingTicketException : Exception
    {
        public OverlappingTicketException(string message) : base(message) { }
    }
}