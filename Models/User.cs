namespace _2026_PraPBL_Backend.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        
        public string Password { get; set; } = string.Empty; 
        
        public string Role { get; set; } = string.Empty; // Isinya nanti: "Admin" atau "User"
    }
}