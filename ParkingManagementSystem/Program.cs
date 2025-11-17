namespace ParkingManagementSystem
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var tester = new Tester();
            await tester.RunTests();

            Console.WriteLine("\nAll tests completed.");
        }
    }
}
