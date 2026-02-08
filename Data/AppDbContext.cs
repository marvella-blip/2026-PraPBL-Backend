using Microsoft.EntityFrameworkCore; using _2026_PraPBL_Backend.Models;

namespace _2026_PraPBL_Backend.Data { public class AppDbContext : DbContext { public AppDbContext(DbContextOptions<_2026_PraPBL_Backend.Data.AppDbContext> options) : base(options) { }

public DbSet<Customer> Customers { get; set; }

protected override void OnModelCreating(ModelBuilder modelBuilder) { base.OnModelCreating(modelBuilder); modelBuilder.Entity<Customer>().HasIndex(c => c.Email).IsUnique(); } } }