using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Main.Data;
using Main.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Main.Pages.Movies
{
    public class IndexModel : PageModel
    {
        private readonly Main.Data.MainContext _context;

        public IndexModel(Main.Data.MainContext context)
        {
            _context = context;
        }

        public IList<Movie> Movie { get;set; } = default!;

        [BindProperty(SupportsGet = true)]  //is required for binding on HTTP Get requests.
        public string? SearchString { get; set; } //Binds from values and query strings with the same name as the property

        public SelectList? Genres { get; set; } //Contains the text users enter in the search text box.
        //Genres contains the list of genres. aloow the user to select a genre from the list.

        [BindProperty(SupportsGet = true)]
        public string? MovieGenre { get; set; } //Contains the specific genre the user selects.

        public async Task OnGetAsync()
        {
            IQueryable<string> genreQuery = from m in _context.Movie //Use LINQ to get list of genres.
                                            orderby m.Genre
                                            select m.Genre;

            var movies = from m in _context.Movie
                         select m; //Creates a LINQ query to select the movies.
            if (!string.IsNullOrEmpty(SearchString))
            {
                #pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
                movies = movies.Where(s => s.Title.Contains(SearchString));
                #pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.
            }

            if (!string.IsNullOrEmpty(MovieGenre)) 
            {
                movies = movies.Where(x => x.Genre == MovieGenre);
            }
            Genres = new SelectList(await genreQuery.Distinct().ToListAsync());
            Movie = await movies.ToListAsync();
            /*
            if (_context.Movie != null)
            {
                Movie = await _context.Movie.ToListAsync();
            }*/
        }
    }
}
