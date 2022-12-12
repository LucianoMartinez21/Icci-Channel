using System.Collections.Generic;
using System.Data.Entity;

namespace TiendaWeb.Models
{
    public class ProductDatabaseInitializer : DropCreateDatabaseIfModelChanges<ProductContext>
    {
        protected override void Seed(ProductContext context)
        {
            GetCategories().ForEach(c => context.Categories.Add(c));
            GetProducts().ForEach(p => context.Products.Add(p));
        }
        private static List<Category> GetCategories()
        {
            var categories = new List<Category>()
            {
                new Category
                {
                    CategoryId = 1,
                    CategoryName = "Bebida"
                },
                new Category 
                {
                    CategoryId = 2,
                    CategoryName = "Comestible"
                },
                new Category 
                {
                    CategoryId = 3,
                    CategoryName = "Utiles"
                }
            };
            return categories;
        }
        public static List<Product> GetProducts() 
        {
            var products = new List<Product>()
            {
                new Product
                {
                    ProductId = 1,
                    ProductName = "Lapiz Pasta Azul Bic",
                    Description = "Boligrafo Clasico Cristal Dura Mas.",
                    ImagePath = "~/img/LapizAzul.jpg",
                    UnitPrice = 200,
                    CategoryId = 3
                },
                new Product
                {
                    ProductId = 2,
                    ProductName = "Chicle Grosso Sabor Sandia",
                    Description = "Chicle Grosso Sabor Sandia.",
                    ImagePath = "~/img/ChicleSandia.png",
                    UnitPrice = 100,
                    CategoryId = 2
                },
                new Product
                {
                    ProductId = 3,
                    ProductName = "Monster Energy Ultra",
                    Description = "Bebida energetica.",
                    ImagePath = "~/img/BebidaEnergetica.jpg",
                    UnitPrice = 1500,
                    CategoryId = 1
                }
            };
            return products;
        }
    }
}
