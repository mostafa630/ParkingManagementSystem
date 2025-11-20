using ParkingManagementSystem.Implementations;
using ParkingManagementSystem.Infrastructure;

namespace ParkingManagementSystem
{
    internal class Tester
    {
        private readonly AppDbContext _context;
        public Tester(AppDbContext context)
        {
            _context = context;
        }

        public async Task RunTests()
        {
            var paymentService = new PaymentService();
            var bookingService = new BookingService(paymentService, _context);

            var sites = _context.Sites;
            var siteA = sites.ElementAt(0);
            var siteB = sites.ElementAt(1);


            var now = DateTimeOffset.UtcNow;

            var testCases = new List<Func<Task>>
            {
                async () =>
                {
                    Console.WriteLine("Test 1: Valid booking");
                    var ticketId = await bookingService.BookParkingAsync("ABC123", siteA.Id, now.AddHours(1), now.AddHours(5), "1111-2222-3333-4444");
                    Console.WriteLine("Test 1 : ticket booked done (true)");

                    var ticket = _context.Tickets.Where(t => t.Id.ToString() == ticketId).First();

                    try
                    {
                        var extendedTicket = ticket.Extend(ticket.To.AddHours(1), 5m);
                         _context.Tickets.Add(extendedTicket);
                         _context.SaveChanges();
                        Console.WriteLine($"Test 1 : Extend Ticket Done  (true)");
                    }
                    catch (Exception ex)
                    {

                         Console.WriteLine($"Test 1 : Extend Ticket Fail  (false)");
                    }
                },
                async () =>
                {
                    Console.WriteLine("Test 2: Invalid plate number");
                    try
                    {
                        await bookingService.BookParkingAsync("", siteA.Id, now.AddHours(1), now.AddHours(5), "1111-2222-3333-4444");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Expected error: {ex.Message}\n");
                        Console.WriteLine("Test 2 : Done (true)");
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
                        Console.WriteLine("Test 3 : Done (true)");
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
                        Console.WriteLine("Test 4 : Done (true)");

                    }
                },
                async () =>
                {
                    Console.WriteLine("Test 5: Booking overlapping tickets");
                    // make valid booking
                    await bookingService.BookParkingAsync("OVER123", siteB.Id, now.AddHours(2), now.AddHours(4), "1111-2222-3333-4444");

                    try
                    {
                        // make an booking that overlap witht the above one
                        await bookingService.BookParkingAsync("OVER123", siteB.Id, now.AddHours(3), now.AddHours(5), "1111-2222-3333-4444");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Expected error: {ex.Message}\n");
                        Console.WriteLine("Test 5 : Done (true)");

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
                        Console.WriteLine("Test 6 : Done (true)");

                    }
                },
            };

            foreach (var test in testCases)
            {
                await test();
            }


            Console.WriteLine("All Tickets in Database:");
            foreach (var ticket in _context.Tickets)
            {
                Console.WriteLine($"Ticket: {ticket.PlateNumber}, Site: {ticket.Site.Name}, From: {ticket.From}, To: {ticket.To}, Amount: {ticket.Price}");
            }
        }
    }
}
