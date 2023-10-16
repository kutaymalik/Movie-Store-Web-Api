using Movie_Store_Web_Api.DBOperations;

namespace Movie_Store_Web_Api.Application.MovieOperations.Commands.DeleteMovie
{
    public class DeleteMovieCommand
    {
        private readonly IMovieStoreDbContext dbContext;
        public int MovieId { get; set; }

        public DeleteMovieCommand(IMovieStoreDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void Handle()
        {
            var movie = dbContext.Movies.SingleOrDefault(x => x.Id == MovieId);

            if (movie == null)
            {
                throw new InvalidOperationException("Record not found!");
            }

            movie.IsActive = false;
            dbContext.Movies.Update(movie);
            dbContext.SaveChanges();
        }
    }
}
