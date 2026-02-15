using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using _2026_PraPBL_Backend.Data;
using _2026_PraPBL_Backend.Models;
using Microsoft.AspNetCore.Authorization; // 1. TAMBAHKAN BARIS INI WAJIB!

namespace _2026_PraPBL_Backend.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    // [Authorize] <-- Kalau kamu taruh di sini, SEMUA pintu di bawahnya otomatis kekunci.
    // Tapi biar lebih jelas, kita gembok satu-satu aja per fungsinya ya.
    public class ReservationsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ReservationsController(AppDbContext context)
        {
            _context = context;
        }

        // -----------------------------------------------------------------
        // PINTU 1: LIHAT DATA (Semua yang login boleh masuk)
        // -----------------------------------------------------------------
        [HttpGet]
        [Authorize] // <-- Sensor Gelang Biasa
        public async Task<ActionResult<IEnumerable<Reservation>>> GetReservations()
        {
            return await _context.Reservations.ToListAsync();
        }

        // -----------------------------------------------------------------
        // PINTU 2: PINJAM RUANGAN (Semua yang login boleh masuk)
        // -----------------------------------------------------------------
        [HttpPost]
        [Authorize] // <-- Sensor Gelang Biasa
        public async Task<ActionResult<Reservation>> PostReservation(Reservation reservation)
        {
            _context.Reservations.Add(reservation);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetReservations), new { id = reservation.Id }, reservation);
        }

        // -----------------------------------------------------------------
        // PINTU 3: UBAH STATUS (HANYA ADMIN YANG BOLEH!)
        // -----------------------------------------------------------------
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")] // <-- SENSOR GELANG VIP! (Selain Admin akan ditolak)
        public async Task<IActionResult> UpdateStatus(int id, [FromBody] string newStatus)
        {
            var reservation = await _context.Reservations.FindAsync(id);
            if (reservation == null) return NotFound();

            reservation.Status = newStatus;

            // Logika mengunci ruangan kalau disetujui
            if (newStatus == "Disetujui")
            {
                var room = await _context.Rooms.FindAsync(reservation.RoomId);
                if (room != null) room.IsAvailable = false;
            }

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // -----------------------------------------------------------------
        // PINTU 4: HAPUS DATA (HANYA ADMIN YANG BOLEH!)
        // -----------------------------------------------------------------
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")] // <-- SENSOR GELANG VIP!
        public async Task<IActionResult> DeleteReservation(int id)
        {
            var reservation = await _context.Reservations.FindAsync(id);
            if (reservation == null) return NotFound();

            // Logika membuka kunci ruangan saat dihapus
            var room = await _context.Rooms.FindAsync(reservation.RoomId);
            if (room != null) room.IsAvailable = true;

            _context.Reservations.Remove(reservation);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}