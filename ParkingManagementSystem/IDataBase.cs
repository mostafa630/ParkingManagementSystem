using ParkingManagementSystem.Models;

namespace ParkingManagementSystem
{
    internal interface IDataBase
    {
        IEnumerable<Site> GetSites(Func<Site, bool>? filter);
        IEnumerable<Tariff> GetTariffs(Func<Tariff, bool>? filter);
        IEnumerable<Ticket> GetTickets(Func<Ticket, bool>? filter);
        Task SaveTicket(Ticket ticket);
    }
}