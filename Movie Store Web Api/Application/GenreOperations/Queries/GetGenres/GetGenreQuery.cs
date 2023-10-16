using AutoMapper;
using Movie_Store_Web_Api.DBOperations;

namespace Movie_Store_Web_Api.Application.GenreOperations.Queries.GetGenres
{
    public class GetGenreQuery
    {
        private readonly IMovieStoreDbContext dbContext;
        private readonly IMapper mapper;

        public GetGenreQuery(IMovieStoreDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }


        public List<GenreViewModel> Handle()
        {
            var genreList = dbContext.Genres.OrderBy(x => x.Id).ToList();

            List<GenreViewModel> vm = mapper.Map<List<GenreViewModel>>(genreList);

            return vm;
        }

    }
}
public class GenreViewModel
{
    public string Name { get; set; }
}