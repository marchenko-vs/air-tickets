using AirTickets.Models;
using AirTickets.Services;
using Microsoft.EntityFrameworkCore;

namespace AirTickets.Data
{
    public class BlitzFlugContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Flight> Flights { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Plane> Planes { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<ServicesTickets> ServiceTicket { get; set; }

        public BlitzFlugContext(DbContextOptions<BlitzFlugContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>()
                .HasOne<User>(s => s.User)
                .WithMany(g => g.Orders)
                .HasForeignKey(s => s.UserId);
            modelBuilder.Entity<Ticket>()
                .HasOne<Flight>(s => s.Flight)
                .WithMany(g => g.Tickets)
                .HasForeignKey(s => s.FlightId);
            modelBuilder.Entity<Ticket>()
                .HasOne<Order>(s => s.Order)
                .WithMany(g => g.Tickets)
                .HasForeignKey(s => s.OrderId);
            modelBuilder.Entity<Service>()
                .HasMany(e => e.Tickets)
                .WithMany(e => e.Services)
                .UsingEntity<ServicesTickets>();
        }
    }
}
