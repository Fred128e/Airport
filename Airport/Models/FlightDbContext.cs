using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Airport.Models
{
    public partial class FlightDbContext : DbContext
    {
        public FlightDbContext()
        {
        }

        public FlightDbContext(DbContextOptions<FlightDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Flight> Flight { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=.;Database=FlightDb;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Flight>(entity =>
            {
                entity.Property(e => e.AirCraftType).IsRequired();

                entity.Property(e => e.ArrivalTime).HasColumnType("datetime");

                entity.Property(e => e.DepartureTime).HasColumnType("datetime");

                entity.Property(e => e.FromLocation).IsRequired();

                entity.Property(e => e.RowVersion)
                    .IsRequired()
                    .IsRowVersion();

                entity.Property(e => e.ToLocation).IsRequired();
            });
        }
    }
}
