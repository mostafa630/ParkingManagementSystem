using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ParkingManagementSystem.Models;

namespace ParkingManagementSystem.Infrastructure.Configurations
{
    internal class SiteConfiguration : IEntityTypeConfiguration<Site>
    {
        public void Configure(EntityTypeBuilder<Site> builder)
        {
            builder.ToTable("Sites");
            builder.HasKey(s => s.Id);
            builder.Property(s => s.Name).IsRequired().HasMaxLength(100);
            builder.Property(s => s.Lat).IsRequired().HasMaxLength(50);
            builder.Property(s => s.Long).IsRequired().HasMaxLength(50);


            builder.HasData(SeedSitesData());

        }
        private Site[] SeedSitesData()
        {
            return new[]
            {
                new Site(Guid.Parse("7c8d24cf-0f3c-4d3f-8014-0b44b0cbbc43"), "Site A", "40.7128 N", "74.0060 W"),
                new Site(Guid.Parse("4f0c3b03-2b0b-4a13-b4f2-3aa5e66d0c42"), "Site B", "51.5074 N", "0.1278 W"),
                new Site(Guid.Parse("5f0c3b03-3b0b-4a13-b4f2-3aa5e66d0c42"), "Site C", "55.5074 N", "65.1278 W")
            };
        }
    }
}