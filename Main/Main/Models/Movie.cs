using System.ComponentModel.DataAnnotations;
namespace Main.Models
{
    public class Movie
    {
        public int Id { get; set; } //primary key.
        public string? Title { get; set; } //"?" indicates the property is nullable. 
        [DataType(DataType.Date)] //the user isn't requered to enter time info in the date field.
        public DateTime ReleaseDate { get; set; }
        public string? Genre { get; set; }
        public decimal Price { get; set; }
    }
}
