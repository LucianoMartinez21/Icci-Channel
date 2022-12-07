using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Main.Models
{
    public class Movie
    {
        public int Id { get; set; } //primary key.

        //public string? Title { get; set; } //"?" indicates the property is nullable. 
        [StringLength(255, MinimumLength = 3)] //Attributes indicate that a property must have a value.
        public string Title { get; set; } = string.Empty;

        [Display(Name ="Release Date"), DataType(DataType.Date)] //the user isn't requered to enter time info in the date field.
        public DateTime ReleaseDate { get; set; }

        [RegularExpression(@"^[A-Z]+[a-zA-Z\s]*$"), Required, StringLength(30)] //Attribute is used to limit what character can be input.
        public string Genre { get; set; } = string.Empty;

        [Range(1200, 100000), DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18, 2)")] //Data annotation enables Entity FW core to correctly map Price to currency in the database.
        public decimal Price { get; set; }

        [RegularExpression(@"^[A-Z]+[a-zA-Z0-9""'\s-]*$"), StringLength(5)]
        public string Rating { get; set; } = string.Empty;
        /*
         * A dataType is an enumeration
         * provides many data types
         * like Date, Time, PhoneNumber
         * Currency, Email, etc.
         */
    }
}
