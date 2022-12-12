using System.Data.Entity;

namespace TiendaWeb.Models
{
    public class ProductContext : DbContext
    {
        public ProductContext() : base("TiendaWeb")
        {}
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}

/*
public BloggingContext(DbContextOptions<BloggingContext> options)
          : base(options)
      { }
public BloggingContext(DbContextOptions<BloggingContext> options)
    : base(options)
{ }
 
 */