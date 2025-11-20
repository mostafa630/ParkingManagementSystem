using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using ParkingManagementSystem.Infrastructure;
namespace ParkingManagementSystem
{
    internal class Program
    {
        static async Task Main(string[] args)
        {

            var config = new ConfigurationBuilder().AddJsonFile("appSettings.json").Build();
            var constring = config.GetSection("constr").Value;


            DbContextOptionsBuilder optionsBuilder = new DbContextOptionsBuilder();
            optionsBuilder.UseSqlServer(constring);

            var options = optionsBuilder.Options;

            using (var context = new AppDbContext(options))
            {
                var tester = new Tester(context);
                await tester.RunTests();
            }
        }
    }
}