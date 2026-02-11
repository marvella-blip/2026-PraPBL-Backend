using System.ComponentModel.DataAnnotations;

namespace _2026_PraPBL_Backend.Models
{
    public class Reservation
    {
        public int Id { get; set; }
        public int RoomId { get; set; }
        
        [Required]
        public string BorrowerName { get; set; } = string.Empty;
        
        [Required]
        public string Purpose { get; set; } = string.Empty;
        
        public DateTime BorrowDate { get; set; }
        
        // Tambahkan ini untuk fitur Pengelolaan Status (PPT Poin 2)
        public string Status { get; set; } = "Menunggu Persetujuan"; 
    }
}
