using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ParkingManagementSystem.Models;

namespace ParkingManagementSystem.Infrastructure.Configurations
{
    internal class TicketConfiguration : IEntityTypeConfiguration<Ticket>
    {
        public void Configure(EntityTypeBuilder<Ticket> builder)
        {
            builder.ToTable("Tickets");

            builder.HasKey(t => t.Id);

            builder.HasOne(t => t.Site)
                   .WithMany()
                   .HasForeignKey(t => t.SiteId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(t => t.Tariff)
                   .WithMany()
                   .HasForeignKey(t => t.TariffId)
                   .OnDelete(DeleteBehavior.NoAction);
        }
    }
}