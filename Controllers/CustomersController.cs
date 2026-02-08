using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using _2026_PraPBL_Backend.Data;
using _2026_PraPBL_Backend.Models;

namespace _2026_PraPBL_Backend.Controllers
{
    [Route("api/v1/customers")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CustomersController(AppDbContext context)
        {
            _context = context;
        }

        // READ (All) - Menampilkan yang belum dihapus saja
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> GetCustomers()
        {
            return await _context.Customers
                .Where(c => c.DeletedAt == null)
                .ToListAsync();
        }

        // READ (Single)
        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> GetCustomer(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null || customer.DeletedAt != null) 
                return NotFound("Yah, data tidak ditemukan atau sudah dihapus.");
            
            return customer;
        }

        // CREATE
        [HttpPost]
        public async Task<ActionResult<Customer>> PostCustomer(Customer customer)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            customer.CreatedDate = DateTime.Now;
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCustomer), new { id = customer.Id }, customer);
        }

        // UPDATE (Task 9)
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomer(int id, Customer customer)
        {
            if (id != customer.Id) return BadRequest("ID tidak cocok!");

            _context.Entry(customer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Customers.Any(e => e.Id == id)) return NotFound();
                else throw;
            }

            return Ok("Data berhasil diperbarui!");
        }

        // DELETE (Soft Delete - Task 9)
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null) return NotFound();

            // Hanya menandai tanggal hapus, tidak menghapus permanen
            customer.DeletedAt = DateTime.Now;
            await _context.SaveChangesAsync();

            return Ok("Data berhasil dihapus secara sistem (Soft Delete).");
        }
    }
}