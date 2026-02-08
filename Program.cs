using Microsoft.EntityFrameworkCore; using _2026_PraPBL_Backend.Data; using _2026_PraPBL_Backend.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<_2026_PraPBL_Backend.Data.AppDbContext>(options => options.UseSqlite("Data Source=MaurenDatabase.db"));

var app = builder.Build();

using (var scope = app.Services.CreateScope()) { var services = scope.ServiceProvider; var context = services.GetRequiredService<AppDbContext>();

context.Database.EnsureCreated();

if (!context.Customers.Any())
{
    context.Customers.AddRange(
        new Customer { Name = "Mauren Cantika", Email = "mauren@pens.ac.id", Status = true },
        new Customer { Name = "Muhammad Fazel", Email = "fazel@test.com", Status = true },
        new Customer { Name = "Budi Santoso", Email = "budi@test.com", Status = true },
        new Customer { Name = "Siti Aminah", Email = "siti@test.com", Status = true },
        new Customer { Name = "Andi Wijaya", Email = "andi@test.com", Status = true }
    );
    context.SaveChanges();
}
}

if (app.Environment.IsDevelopment()) { app.MapOpenApi(); }

app.UseHttpsRedirection(); app.UseAuthorization(); app.MapControllers();

app.Run();