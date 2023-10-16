using Microsoft.EntityFrameworkCore;
using Movie_Store_Web_Api.Entities;

namespace Movie_Store_Web_Api.DBOperations;

public class DataGenerator
{
    public async static Task Initialize(IServiceProvider serviceProvider)
    {
        using(var context = new MovieStoreDbContext(serviceProvider.GetRequiredService<DbContextOptions<MovieStoreDbContext>>()))
        {
            if(context.Genres.Any() || context.Actors.Any() || context.Directors.Any() || context.Movies.Any())
            {
                return;
            }

            await context.Genres.AddRangeAsync(
                new Genre
                {
                    Name = "Action",
                },
                new Genre
                {
                    Name= "Drama",
                },
                new Genre
                {
                    Name = "Documentary",
                },
                new Genre
                {
                    Name = "Science Fiction",
                },
                new Genre
                {
                    Name = "Animation",
                },
                new Genre
                {
                    Name = "Horror",
                }
                );

            await context.Actors.AddRangeAsync(
                new Actor
                {
                    FirstName = "Michelle",
                    LastName = "Rodriguez",
                },
                new Actor
                {
                    FirstName = "Robert",
                    LastName = "Downey",
                },
                new Actor
                {
                    FirstName = "Paul",
                    LastName = "Walker",
                },
                new Actor
                {
                    FirstName = "Scarlett",
                    LastName = "Johansson",
                },
                new Actor
                {
                    FirstName = "Vin",
                    LastName = "Diesel",
                },
                new Actor
                {
                    FirstName = "Keanu",
                    LastName = "Reeves",
                },
                new Actor
                {
                    FirstName = "Chris",
                    LastName = "Evans",
                },
                new Actor
                {
                    FirstName = "Tom",
                    LastName = "Hiddleston",
                },
                new Actor
                {
                    FirstName = "Morgan",
                    LastName = "Freeman",
                },
                new Actor
                {
                    FirstName = "Benedict",
                    LastName = "Cumberbatch",
                }
                );

            await context.Directors.AddRangeAsync(
                new Director
                {
                    FirstName = "Louis",
                    LastName = "Leterrier",
                },
                new Director
                {
                    FirstName = "Cristopher",
                    LastName = "Nolan",
                },
                new Director
                {
                    FirstName = "Anthony",
                    LastName = "Russo",
                },
                new Director
                {
                    FirstName = "Tim",
                    LastName = "Burton",
                },
                new Director
                {
                    FirstName = "Andrey",
                    LastName = "Tarkovski",
                },
                new Director
                {
                    FirstName = "Lana",
                    LastName = "Wachowski",
                }
                );

            var matrix = new Movie
            {
                Name = "Matrix 1",
                ReleaseDate = new DateTime(1999, 3, 31),
                Price = 100,
                GenreId = 4,
                DirectorId = 6,
            };

            var ff = new Movie
            {
                Name = "Fast & Furious",
                ReleaseDate = new DateTime(2001, 11, 2),
                Price = 150,
                GenreId = 1,
                DirectorId = 1,
            };
            
            var avengers = new Movie
            {
                Name = "Avengers Endgame",
                ReleaseDate = new DateTime(2001, 4, 26),
                Price = 150,
                GenreId = 4,
                DirectorId = 4,
 
            };

            await context.Movies.AddRangeAsync(
                new Movie
                {
                    Name = "Matrix 1",
                    ReleaseDate = new DateTime (1999, 3, 31),
                    Price = 100,
                    GenreId = 4,
                    DirectorId = 6,
                    Actors = new List<Actor>()
                },
                new Movie
                {
                    Name = "Fast & Furious",
                    ReleaseDate = new DateTime(2001, 11, 2),
                    Price = 150,
                    GenreId = 1,
                    DirectorId = 1,
                    Actors = new List<Actor>()
                },
                new Movie
                {
                    Name = "Avengers Endgame",
                    ReleaseDate = new DateTime(2001, 4, 26),
                    Price = 150,
                    GenreId = 4,
                    DirectorId = 4,
                    Actors = new List<Actor>()
                }
                );

            await context.Customers.AddRangeAsync(
                new Customer
                {
                    FirstName = "firstname1",
                    LastName = "lastname1",
                    Email = "lastname1",
                    Password = "lastname1",
                    PurchasedMovies = new List<Movie>(),
                    FavGenreId = 1,
                    Orders = new List<Order>(),
                });

            await context.SaveChangesAsync();
        }
    }
}
