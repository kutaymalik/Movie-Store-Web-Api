using AutoMapper;
using Movie_Store_Web_Api.DBOperations;
using Movie_Store_Web_Api.Entities;

namespace Movie_Store_Web_Api.Application.GenreOperations.Commands.CreateGenre
{
    public class CreateGenreCommand
    {
        public CreateGenreModel Model { get; set; }
        private readonly IMovieStoreDbContext dbContext;
        private readonly IMapper mapper;
        public CreateGenreCommand(IMovieStoreDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public void Handle()
        {
            var genre = dbContext.Genres.SingleOrDefault(x => x.Name == Model.Name);

            if (genre == null)
            {
                throw new InvalidOperationException("The genre is already exists!");
            }

            genre = mapper.Map<Genre>(Model);

            dbContext.Genres.Add(genre);
            dbContext.SaveChanges();
        }
    }

    public class CreateGenreModel
    {
        public string Name { get; set; }
    }

}
