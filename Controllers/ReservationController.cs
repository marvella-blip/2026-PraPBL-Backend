using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using _2026_PraPBL_Backend.Data;
using _2026_PraPBL_Backend.Models;

namespace _2026_PraPBL_Backend.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ReservationsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ReservationsController(AppDbContext context)
        {
            _context = context;
        }

        // PPT Poin 3: Melihat daftar riwayat peminjaman
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Reservation>>> GetReservations()
        {
            return await _context.Reservations.ToListAsync();
        }

        // PPT Poin 1: Menambah data peminjaman
        [HttpPost]
        public async Task<IActionResult> PostReservation(Reservation res)
        {
            _context.Reservations.Add(res);
            await _context.SaveChangesAsync();
            return Ok(res);
        }

        // PPT Poin 2: Mengubah status peminjaman (Setuju/Tolak)
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStatus(int id, [FromBody] string newStatus)
        {
            var res = await _context.Reservations.FindAsync(id);
            if (res == null) return NotFound();

            res.Status = newStatus;

            // Logika Otomatis: Jika disetujui, ruangan jadi tidak tersedia
            if (newStatus == "Disetujui")
            {
                var room = await _context.Rooms.FindAsync(res.RoomId);
                if (room != null) room.IsAvailable = false;
            }

            await _context.SaveChangesAsync();
            return NoContent();
        }
        
        // PPT Poin 1: Menghapus data peminjaman
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReservation(int id)
        {
            var res = await _context.Reservations.FindAsync(id);
            if (res == null) return NotFound();
            _context.Reservations.Remove(res);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}