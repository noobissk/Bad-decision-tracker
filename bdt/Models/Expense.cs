using System.ComponentModel.DataAnnotations;

namespace bdt.Models
{
    public class Expense
    {
        public int id { get; set; }
        
        [Required]
        public decimal value { get; set; }
        
        [Required]
        public string? description { get; set; }
        
        public string? category { get; set; }
    }
}
