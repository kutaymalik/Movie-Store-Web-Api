using Movie_Store_Web_Api.Application.ActorOperations.Commands.UpdateActor;
using Movie_Store_Web_Api.DBOperations;
using Movie_Store_Web_Api.Entities;

namespace Movie_Store_Web_Api.Application.DirectorOperations.Commands.UpdateDirector;

public class UpdateDirectorCommand
{
    public UpdateDirectorModel Model { get; set; }
    private readonly IMovieStoreDbContext dbContext;
    public int DirectorId { get; set; }

    public UpdateDirectorCommand(IMovieStoreDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public void Handle()
    {
        var director = dbContext.Directors.SingleOrDefault(x => x.Id == DirectorId);

        if (director == null)
        {
            throw new InvalidOperationException("Record not found!");
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


        director.FirstName = string.IsNullOrEmpty(Model.FirstName.Trim()) ? director.FirstName : Model.FirstName;
        director.LastName = string.IsNullOrEmpty(Model.LastName.Trim()) ? director.LastName : Model.LastName;
        director.DirectedMovies = !Enumerable.Any(directedMovies) ? director.DirectedMovies : directedMovies;

        dbContext.Directors.Update(director);
        dbContext.SaveChanges();
    }
}
public class UpdateDirectorModel
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public List<int> DirectedMovies { get; set; }
}