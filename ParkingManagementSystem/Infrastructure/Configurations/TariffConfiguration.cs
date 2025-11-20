using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ParkingManagementSystem.Models;

namespace ParkingManagementSystem.Infrastructure.Configurations
{
    internal class TariffConfiguration : IEntityTypeConfiguration<Tariff>
    {
        public void Configure(EntityTypeBuilder<Tariff> builder)
        {
            builder.ToTable("Tariffs");

            builder.HasKey(t => t.Id);

            builder.HasOne(t => t.Site)
                   .WithMany()
                   .HasForeignKey(t => t.SiteId)
                   .OnDelete(DeleteBehavior.Cascade);

            // Seed data
            builder.HasData(SeedTariffsData());
        }
        private Tariff[] SeedTariffsData()
        {
            return new[]
            {
                new Tariff(
                    Guid.Parse("4f0c3b03-9b0b-4a13-b4f2-3aa5e66d0c42"),
                    Guid.Parse("7c8d24cf-0f3c-4d3f-8014-0b44b0cbbc43"),
                    5.00m,
                    3.00m
                ),
                new Tariff(
                    Guid.Parse("8f0c3b03-9b0b-4a13-b4f2-3aa5e66d0c42"),
                    Guid.Parse("4f0c3b03-2b0b-4a13-b4f2-3aa5e66d0c42"),
                    6.00m,
                    4.00m
                )
            };
        }
    }
}