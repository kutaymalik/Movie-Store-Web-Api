using AutoMapper;
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
            var actor = dbContext.Actors.SingleOrDefault(x => x.FirstName == Model.FirstName && x.LastName == Model.LastName);

            if (actor != null)
            {
                throw new InvalidOperationException("The actor is already exists!");
            }

            var playedMovies = new List<Movie>();

            foreach (int movieId in Model.PlayedMovies)
            {
                var movie = dbContext.Movies.SingleOrDefault(a => a.Id == movieId);
                if (movie != null)
                {
                    playedMovies.Add(movie);
                }
                else
                {
                    throw new InvalidOperationException($"Movie with ID {movieId} not found.");
                }
            }
            actor = new Actor();

            actor.FirstName = Model.FirstName;
            actor.LastName = Model.LastName;

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
