using Microsoft.EntityFrameworkCore;
using ParkingManagementSystem.Infrastructure.Configurations;
using ParkingManagementSystem.Models;

namespace ParkingManagementSystem.Infrastructure
{
    internal class AppDbContext : DbContext
    {
        public DbSet<Site> Sites { get; set; }
        public DbSet<Tariff> Tariffs { get; set; }
        public DbSet<Ticket> Tickets { get; set; }

        public AppDbContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(SiteConfiguration).Assembly);
        }
    }
}