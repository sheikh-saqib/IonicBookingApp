

using Microsoft.EntityFrameworkCore;
using MyAppAPI.Models;

namespace MyAppAPI.Context
{
    public class DataContext : DbContext
    {
        public DataContext()
        {
        }
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            if (!options.IsConfigured)
            {
                options.UseSqlite("Data source=App.db");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Users> Users { get; set; }
        public DbSet<City> City { get; set; }
        public DbSet<Venue> Venue { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<SlotDetails> SlotDetails { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<BookingSlots> BookingSlots { get; set; }
        public DbSet<Payment> PaymentDetails { get; set; }
    }
}
