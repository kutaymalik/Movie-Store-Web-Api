using Microsoft.EntityFrameworkCore;
using Movie_Store_Web_Api.DBOperations;
using Movie_Store_Web_Api.Entities;

namespace Movie_Store_Web_Api.Application.ActorOperations.Commands.UpdateActor;

public class UpdateActorCommand
{
    public UpdateActorModel Model { get; set; }
    private readonly IMovieStoreDbContext dbContext;
    public int ActorId { get; set; }

    public UpdateActorCommand(IMovieStoreDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public void Handle()
    {
        var actor = dbContext.Actors.Include(x => x.PlayedMovies).SingleOrDefault(x => x.Id == ActorId);

        if (actor == null)
        {
            throw new InvalidOperationException("Record not found!");
        }

        var playedmovies = new List<Movie>();

        foreach (int movieId in Model.PlayedMovies)
        {
            var movie = dbContext.Movies.SingleOrDefault(a => a.Id == movieId);
            if (movie != null)
            {
                playedmovies.Add(movie);
            }
            else
            {
                throw new InvalidOperationException($"Movie with ID {movieId} not found.");
            }
        }

        actor.FirstName = string.IsNullOrEmpty(Model.FirstName.Trim()) ? actor.FirstName : Model.FirstName;
        actor.LastName = string.IsNullOrEmpty(Model.LastName.Trim()) ? actor.LastName : Model.LastName;
        actor.PlayedMovies = !Enumerable.Any(playedmovies) ? actor.PlayedMovies : playedmovies;

        dbContext.Actors.Update(actor);
        dbContext.SaveChanges();
    }
}
public class UpdateActorModel
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public List<int> PlayedMovies { get; set; }
}