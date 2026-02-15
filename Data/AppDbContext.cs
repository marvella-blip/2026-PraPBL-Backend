using Microsoft.EntityFrameworkCore;
using _2026_PraPBL_Backend.Models;

namespace _2026_PraPBL_Backend.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Room> Rooms { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        
        // 1. TAMBAHKAN TABEL USERS
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Seed Data Ruangan (Tetap biarkan seperti yang kamu punya)
            modelBuilder.Entity<Room>().HasData(
                new Room { Id = 1, Name = "Auditorium", Description = "Lantai 2", Capacity = 100, IsAvailable = true },
                new Room { Id = 2, Name = "Sekretariat Bersama", Description = "Lantai 1", Capacity = 20, IsAvailable = true },
                new Room { Id = 3, Name = "Lab Pemrograman", Description = "Gedung D3", Capacity = 30, IsAvailable = true },
                new Room { Id = 4, Name = "Aula PENS", Description = "Pascasarjana Lt 6", Capacity = 200, IsAvailable = true },
                new Room { Id = 5, Name = "Mini Teater", Description = "Gedung D3 Lt 1", Capacity = 200, IsAvailable = true }
            );

            // 2. TAMBAHKAN SEED DATA USER & ADMIN
            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, Username = "admin", Password = "password123", Role = "Admin" },
                new User { Id = 2, Username = "mauren", Password = "user123", Role = "User" }
            );
        }
    }
}