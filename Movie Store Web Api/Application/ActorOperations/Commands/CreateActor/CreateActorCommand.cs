using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Movie_Store_Web_Api.DBOperations;
using Movie_Store_Web_Api.Entities;

namespace Movie_Store_Web_Api.Application.ActorOperations.Commands.CreateActorOperations
{
    public class CreateActorCommand
    {
        public CreateActorModel Model { get; set; }
        private readonly IMovieStoreDbContext dbContext;
        public CreateActorCommand(IMovieStoreDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void Handle()
        {
            var actor = dbContext.Actors
                .Include(x => x.PlayedMovies)
                .SingleOrDefault(x => x.FirstName.ToLower() == Model.FirstName.ToLower() && x.LastName.ToLower() == Model.LastName.ToLower());

            if (actor != null)
            {
                throw new InvalidOperationException("The actor is already exists!");
            }

            var playedmovies = new List<Movie>();

            actor = new Actor();

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

            actor.FirstName = Model.FirstName;
            actor.LastName = Model.LastName;
            actor.PlayedMovies = playedmovies;

            dbContext.Actors.Add(actor);
            dbContext.SaveChanges();
        }
    }

    public class CreateActorModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<int> PlayedMovies { get; set; }
    }

}
