using ParkingManagementSystem.Implementations;
using ParkingManagementSystem.Models;

namespace ParkingManagementSystem
{
    internal class Tester
    {
        public async Task RunTests()
        {
            var database = new DataBase();
            var paymentService = new PaymentService();
            var bookingService = new BookingService(paymentService, database);

            var sites = database.GetSites(null);
            var siteA = sites.ElementAt(0);
            var siteB = sites.ElementAt(1);

            System.Console.WriteLine($"len = {sites.Count()}");

            var now = DateTimeOffset.UtcNow;

            var testCases = new List<Func<Task>>
            {
                async () =>
                {
                    Console.WriteLine("Test 1: Valid booking");
                    var ticketId = await bookingService.BookParkingAsync("ABC123", siteA.Id, now.AddHours(1), now.AddHours(5), "1111-2222-3333-4444");
                    Console.WriteLine($"Ticket booked successfully: {ticketId}\n");
                },
                async () =>
                {
                    Console.WriteLine("Test 2: Invalid plate number (empty)");
                    try
                    {
                        await bookingService.BookParkingAsync("", siteA.Id, now.AddHours(1), now.AddHours(5), "1111-2222-3333-4444");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Expected error: {ex.Message}\n");
                    }
                },
                async () =>
                {
                    Console.WriteLine("Test 3: Booking in the past");
                    try
                    {
                        await bookingService.BookParkingAsync("XYZ123", siteA.Id, now.AddHours(-1), now.AddHours(1), "1111-2222-3333-4444");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Expected error: {ex.Message}\n");
                    }
                },
                async () =>
                {
                    Console.WriteLine("Test 4: Booking exceeds max duration");
                    try
                    {
                        await bookingService.BookParkingAsync("LONG123", siteA.Id, now.AddHours(1), now.AddHours(26), "1111-2222-3333-4444");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Expected error: {ex.Message}\n");
                    }
                },
                async () =>
                {
                    Console.WriteLine("Test 5: Booking overlapping tickets");
                    // First booking
                    await bookingService.BookParkingAsync("OVER123", siteB.Id, now.AddHours(2), now.AddHours(4), "1111-2222-3333-4444");

                    try
                    {
                        // Overlapping booking
                        await bookingService.BookParkingAsync("OVER123", siteB.Id, now.AddHours(3), now.AddHours(5), "1111-2222-3333-4444");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Expected error: {ex.Message}\n");
                    }
                },
                async () =>
                {
                    Console.WriteLine("Test 6: Booking at non-existent site");
                    try
                    {
                        await bookingService.BookParkingAsync("SITE123", Guid.NewGuid(), now.AddHours(1), now.AddHours(2), "1111-2222-3333-4444");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Expected error: {ex.Message}\n");
                    }
                },
            };

            foreach (var test in testCases)
            {
                await test();
            }


            Console.WriteLine("All Tickets in Database:");
            foreach (var ticket in database.GetTickets(null))
            {
                Console.WriteLine($"Ticket: {ticket.PlateNumber}, Site: {ticket.Site.Name}, From: {ticket.From}, To: {ticket.To}, Amount: {ticket.Price}");
            }
        }
    }
}
