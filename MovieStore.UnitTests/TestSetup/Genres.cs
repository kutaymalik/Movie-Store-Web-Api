using Movie_Store_Web_Api.DBOperations;
using Movie_Store_Web_Api.Entities;

namespace MovieStore.UnitTests.TestSetup;

public static class Genres
{
    public static void AddGenres(this MovieStoreDbContext context)
    {
        context.Genres.AddRange(
                new Genre
                {
                    Name = "Action",
                },
                new Genre
                {
                    Name = "Drama",
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
    }
}
