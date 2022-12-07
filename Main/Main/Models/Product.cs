using System.ComponentModel.DataAnnotations;

namespace Main.Models
{
    public class Product
    {
        public int Id { get; set; }
        [StringLength(255, MinimumLength = 5)] 
        public string Name { get; set; } = string.Empty; 
        
        [StringLength(255)]
        public string Description { get; set; } = string.Empty;
        
        [RegularExpression(@"^[A-Z]+[a-zA-Z\s]*$"), Required, StringLength(30)]
        public string Category { get; set; } = string.Empty;
        
        [Range(500, 100000), DataType(DataType.Currency)]
        public decimal Price { get; set; }

        [RegularExpression(@"^[A-Z]+[a-zA-Z0-9""'\s-]*$"), StringLength(5)] 
        public string Rating { get; set; } = string.Empty;
    }
}
