using Movie_Store_Web_Api.DBOperations;
using Movie_Store_Web_Api.Entities;

namespace Movie_Store_Web_Api.Application.DirectorOperations.Commands.CreateDirector;

public class CreateDirectorCommand
{
    public CreateDirectorModel Model { get; set; }
    private readonly IMovieStoreDbContext dbContext;
    public CreateDirectorCommand(IMovieStoreDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public void Handle()
    {
        var director = dbContext.Directors.SingleOrDefault(x => x.FirstName == Model.FirstName && x.LastName == Model.LastName);

        if (director != null)
        {
            throw new InvalidOperationException("The director is already exists!");
        }

        var directedMovies = new List<Movie>();

        foreach (int movieId in Model.DirectedMovies)
        {
            var movie = dbContext.Movies.SingleOrDefault(a => a.Id == movieId);
            if (movie != null)
            {
                directedMovies.Add(movie);
            }
            else
            {
                throw new InvalidOperationException($"Movie with ID {movieId} not found.");
            }
        }

        director = new Director();

        director.FirstName = Model.FirstName;
        director.LastName = Model.LastName;
        director.DirectedMovies = directedMovies;

        dbContext.Directors.Add(director);
        dbContext.SaveChanges();
    }
}
public class CreateDirectorModel
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public List<int> DirectedMovies { get; set; }
}