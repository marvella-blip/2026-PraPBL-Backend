using System;

namespace _2026_PraPBL_Backend.Models
{
    public class Room
    {
        public int Id { get; set; }
        public string? Name { get; set; } 
        public string? Description { get; set; }
        public int Capacity { get; set; }
        public bool IsAvailable { get; set; } = true;
        public DateTime? DeletedAt { get; set; } 
    }
}