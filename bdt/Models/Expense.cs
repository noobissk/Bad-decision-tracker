using System.ComponentModel.DataAnnotations;

namespace bdt.Models
{
    public class Expense
    {
        public int Id { get; set; }
        
        [Required]
        public decimal Price { get; set; }
        
        [Required]
        public string? Description { get; set; }
        
        [Required]
        public string? Category { get; set; }

    }
}
