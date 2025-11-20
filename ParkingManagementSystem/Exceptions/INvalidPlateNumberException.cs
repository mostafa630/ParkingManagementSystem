namespace ParkingManagementSystem.Exceptions
{
    public class INvalidPlateNumberException : Exception
    {
        public INvalidPlateNumberException(string message) : base(message) { }
    }
}