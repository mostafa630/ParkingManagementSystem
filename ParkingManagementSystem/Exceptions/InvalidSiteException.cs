namespace ParkingManagementSystem.Exceptions
{
    public class InvalidSiteException : Exception
    {
        public InvalidSiteException(string message) : base(message) { }
    }
}