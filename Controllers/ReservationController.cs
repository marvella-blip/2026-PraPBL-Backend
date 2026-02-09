[Route("api/v1/[controller]")]
[ApiController]
public class ReservationsController : ControllerBase
{
    private readonly AppDbContext _context;
    public ReservationsController(AppDbContext context) { _context = context; }

    [HttpPost]
    public async Task<IActionResult> CreateReservation(Reservation res)
    {
        _context.Reservations.Add(res);
        
        // Update status ruangan jadi tidak tersedia
        var room = await _context.Rooms.FindAsync(res.RoomId);
        if (room != null) room.IsAvailable = false;
        
        await _context.SaveChangesAsync();
        return Ok(res);
    }
}