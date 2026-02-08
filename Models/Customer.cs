using System; using System.ComponentModel.DataAnnotations;

namespace _2026_PraPBL_Backend.Models { public class Customer { [Key] public int Id { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    public string Email { get; set; }

    public string? Phone { get; set; }

    public string? Address { get; set; }

    public bool Status { get; set; } = true;

    public DateTime CreatedDate { get; set; } = DateTime.Now;

    public DateTime? DeletedAt { get; set; }
}
}