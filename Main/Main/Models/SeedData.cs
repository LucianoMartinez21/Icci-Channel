using Microsoft.EntityFrameworkCore;
using Main.Data;

namespace Main.Models;

public static class SeedData
{
    public static void Initialize(IServiceProvider ServiceProvider)
    {
        using (var context = new MainContext(
            ServiceProvider.GetRequiredService<
                DbContextOptions<MainContext>>()))
        {
            if (context == null || context.Movie == null) { throw new ArgumentException("Null MainContext"); }
            if (context.Movie.Any()) { return; } //DB has been seeded, if there are any movies in the database, the seed initializer returns and no movies are added.
            context.Movie.AddRange(
                new Movie
                {
                    Title = "Jojo's Bizarre Adventure Phantom Blood",
                    ReleaseDate = DateTime.Parse("2022-12-04"),
                    Genre = "Anime",
                    Price = 1300M //The literal with the M suffix is of type decimal
                },
                new Movie
                {
                    Title = "El viaje de Chihiro",
                    ReleaseDate = DateTime.Parse("2022-12-05"),
                    Genre = "Anime",
                    Price = 4300M //The literal with the M suffix is of type decimal
                }
            );
            context.SaveChanges();
        }
    }
}
