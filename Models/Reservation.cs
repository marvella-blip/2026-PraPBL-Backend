namespace _2026_PraPBL_Backend.Models
{
    public class Reservation
    {
        public int Id { get; set; }
        public int RoomId { get; set; } // ID Ruangan yang dipinjam
        public string? BorrowerName { get; set; } // Nama peminjam (misal: Mauren)
        public DateTime BorrowDate { get; set; }
        public string? Purpose { get; set; } // Tujuan (misal: Rapat Hima)
        
        // Relasi ke Model Room
        public virtual Room? Room { get; set; }
    }
}