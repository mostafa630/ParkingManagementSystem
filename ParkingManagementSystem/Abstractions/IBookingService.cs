namespace ParkingManagementSystem.Abstractions
{
    internal interface IBookingService
    {
        public Task<string> BookParkingAsync(string plateNumber, Guid siteId, DateTimeOffset from, DateTimeOffset to, string cardNumber);
    }
}
