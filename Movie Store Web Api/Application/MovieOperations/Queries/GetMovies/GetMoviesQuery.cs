using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Movie_Store_Web_Api.Application.CustomerOperations.Queries.GetCustomers;
using Movie_Store_Web_Api.DBOperations;
using Movie_Store_Web_Api.Entities;

namespace Movie_Store_Web_Api.Application.MovieOperations.Queries.GetMovies
{
    public class GetMoviesQuery
    {
        private readonly IMovieStoreDbContext dbContext;
        private readonly IMapper mapper;

        public GetMoviesQuery(IMovieStoreDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public List<MovieViewModel> Handle()
        {
            var movieList = dbContext.Movies
                .Include(x => x.Genre)
                .Include(x => x.Actors)
                .Include(x => x.Director)
                .Where(x => x.IsActive == true)
                .OrderBy(x => x.Id).ToList();

            List<MovieViewModel> vm = mapper.Map<List<MovieViewModel>>(movieList);

            return vm;
        }
    }
}
public class MovieViewModel
{
    public string Name { get; set; }
    public DateTime ReleaseDate { get; set; }
    public decimal Price { get; set; }

    public int GenreId { get; set; }

    public int DirectorId { get; set; }

    public virtual List<Actor> Actors { get; set; }
}
