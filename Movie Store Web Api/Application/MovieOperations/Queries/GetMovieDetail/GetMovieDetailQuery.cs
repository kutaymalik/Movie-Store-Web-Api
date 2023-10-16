using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Movie_Store_Web_Api.DBOperations;
using Movie_Store_Web_Api.Entities;

namespace Movie_Store_Web_Api.Application.MovieOperations.Queries.GetMovieDetail;

public class GetMovieDetailQuery
{
    private readonly IMovieStoreDbContext dbContext;
    public int MovieId { get; set; }
    private readonly IMapper mapper;

    public GetMovieDetailQuery(IMovieStoreDbContext dbContext, IMapper mapper)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }

    public MovieDetailViewModel Handle()
    {
        var movie = dbContext.Movies
            .Include(x => x.Genre)
            .Include(x => x.Actors)
            .Include(x => x.Director)
            .Where(x => x.Id == MovieId).SingleOrDefault();

        if (movie == null)
        {
            throw new InvalidOperationException("Record not found!");
        }

        if (movie.IsActive == false)
        {
            throw new InvalidOperationException("Record not active!");
        }

        MovieDetailViewModel vm = mapper.Map<MovieDetailViewModel>(movie);

        return vm;
    }

}

public class MovieDetailViewModel
{
    public string Name { get; set; }
    public DateTime ReleaseDate { get; set; }
    public decimal Price { get; set; }

    public int GenreId { get; set; }

    public int DirectorId { get; set; }

    public virtual List<Actor> Actors { get; set; }
}
