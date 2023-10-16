using Movie_Store_Web_Api.DBOperations;

namespace Movie_Store_Web_Api.Application.GenreOperations.Commands.DeleteGenre;

public class DeleteGenreCommand
{
    private readonly IMovieStoreDbContext dbContext;
    public int GenreId { get; set; }

    public DeleteGenreCommand(IMovieStoreDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public void Handle()
    {
        var genre = dbContext.Genres.SingleOrDefault(x => x.Id == GenreId);

        if (genre == null)
        {
            throw new InvalidOperationException("Record not found!");
        }

        dbContext.Genres.Remove(genre);

    }
}