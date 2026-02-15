using Microsoft.EntityFrameworkCore;
using _2026_PraPBL_Backend.Data;
using _2026_PraPBL_Backend.Models;

var builder = WebApplication.CreateBuilder(args);

// 1. Konfigurasi CORS agar React bisa akses
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp",
        policy =>
        {
            policy.WithOrigins("http://localhost:5173")
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

builder.Services.AddControllers();

builder.Services.AddAuthentication(Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });

// 2. Koneksi ke Database SQLite
builder.Services.AddDbContext<AppDbContext>(options => 
    options.UseSqlite("Data Source=MaurenDatabase.db"));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy("IzinkanReact",
        policy =>
        {
            policy.WithOrigins("http://localhost:5173") // Port default Vite (Frontend kamu)
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

var app = builder.Build();

// 3. Script untuk memastikan Database & Data Awal (Seeding) tersedia
using (var scope = app.Services.CreateScope()) 
{ 
    var services = scope.ServiceProvider; 
    var context = services.GetRequiredService<AppDbContext>();

    context.Database.EnsureCreated();

    // Cek apakah tabel Rooms masih kosong
    if (!context.Rooms.Any())
    {
        context.Rooms.AddRange(
            new Room { Name = "Lab SCADA", Description = "Lantai 1 Gedung EN", Capacity = 30 },
            new Room { Id = 2, Name = "Ruang Teater", Description = "Lantai 1 Gedung D3", Capacity = 100 },
            new Room { Name = "Lab Agile Development", Description = "Gedung D4 Lantai 2", Capacity = 30 },
            new Room { Name = "Aula Pens", Description = "Gedung Pascasarjana Lantai 6", Capacity = 500 }
        );  
        context.SaveChanges();
    }
}

if (app.Environment.IsDevelopment()) 
{ 
    app.MapOpenApi(); 
}

app.UseAuthentication(); 
app.UseSwagger();
app.UseSwaggerUI(); 
app.UseCors("AllowReactApp");
app.UseAuthorization(); 
app.MapControllers();

app.Run();