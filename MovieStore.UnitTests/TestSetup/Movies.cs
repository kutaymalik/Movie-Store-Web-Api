using Movie_Store_Web_Api.DBOperations;
using Movie_Store_Web_Api.Entities;

namespace MovieStore.UnitTests.TestSetup;

public static class Movies
{
    public static void AddMovies(this MovieStoreDbContext context)
    {
        context.Movies.AddRangeAsync(
                new Movie
                {
                    Name = "Matrix 1",
                    ReleaseDate = new DateTime(1999, 3, 31),
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
    }
}
