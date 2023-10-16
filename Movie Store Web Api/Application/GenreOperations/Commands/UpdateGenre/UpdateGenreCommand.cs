using AutoMapper;
using Movie_Store_Web_Api.Application.CustomerOperations.Commands.UpdateCustomer;
using Movie_Store_Web_Api.DBOperations;


namespace Movie_Store_Web_Api.Application.GenreOperations.Commands.UpdateGenreOperations
{
    public class UpdateGenreCommand
    {
        public UpdateGenreModel Model { get; set; }
        private readonly IMovieStoreDbContext dbContext;

        public int GenreId { get; set; }

        public UpdateGenreCommand(IMovieStoreDbContext dbContext)
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


            genre.Name = string.IsNullOrEmpty(Model.Name.Trim()) ? genre.Name : Model.Name;
   
            dbContext.Genres.Update(genre);
            dbContext.SaveChanges();
        }
    }

    public class UpdateGenreModel
    {
        public string Name { get; set; }
    }
}
