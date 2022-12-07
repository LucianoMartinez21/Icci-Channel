using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Main.Models;

namespace Main.Data
{
    public class MainContext : DbContext
    {
        public MainContext (DbContextOptions<MainContext> options)
            : base(options)
        {
        }

        public DbSet<Main.Models.Movie> Movie { get; set; } = default!; //empty set, corresponds to a database table. An entity corresponds to a row in the table.

        public DbSet<Main.Models.Product> Product { get; set; } = default!;
    }
}
