using Microsoft.EntityFrameworkCore;
using _2026_PraPBL_Backend.Models;

namespace _2026_PraPBL_Backend.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Room> Rooms { get; set; }

      protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    base.OnModelCreating(modelBuilder);
    
    // Kamu harus membungkus datanya di dalam HasData
    modelBuilder.Entity<Room>().HasData(
        new Room { Id = 1, Name = "Lab SCADA", Description = "Lantai 2 Gedung D4", Capacity = 30 },
        new Room { Id = 2, Name = "Ruang Teater", Description = "Lantai 1 Gedung TC", Capacity = 100 }
    );
}
    }
    public DbSet<Reservation> Reservations { get; set; }
    }