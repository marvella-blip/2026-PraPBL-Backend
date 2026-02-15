using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using _2026_PraPBL_Backend.Data;

namespace _2026_PraPBL_Backend.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;

        public AuthController(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            // 1. Cari user di database berdasarkan Username & Password
            var user = _context.Users.FirstOrDefault(u => u.Username == request.Username && u.Password == request.Password);
            
            if (user == null)
            {
                return Unauthorized(new { message = "Username atau Password salah!" });
            }

            // 2. Siapkan stempel rahasia dari appsettings.json
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            // 3. Masukkan data ke dalam Token (KTP Digital)
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.Role) // <-- INI YANG MENENTUKAN DIA ADMIN ATAU BUKAN
            };

            // 4. Cetak Tokennya
            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(2), // Tiket hangus dalam 2 jam
                signingCredentials: credentials);

            // 5. Kirim Token ke Frontend
            return Ok(new { 
                token = new JwtSecurityTokenHandler().WriteToken(token),
                role = user.Role,
                username = user.Username
            });
        }
    }

    // Class untuk menerima input dari Frontend
    public class LoginRequest
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}