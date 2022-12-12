using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace TiendaWeb.Models
{
    public class Category
    {
        [ScaffoldColumn(false)] //data annotations
        public int CategoryId { get; set; }

        [Required, StringLength(100), Display(Name = "Name")]
        public string CategoryName { get; set; }

        [Display(Name = "Product Description")]
        public string Description { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
