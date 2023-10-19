using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Movie_Store_Web_Api.Application.ActorOperations.Queries.GetActors;
using Movie_Store_Web_Api.DBOperations;
using Movie_Store_Web_Api.Entities;

namespace Movie_Store_Web_Api.Application.DirectorOperations.Queries.GetDirectors;

public class GetDirectorsQuery
{
    private readonly IMovieStoreDbContext dbContext;
    private readonly IMapper mapper;

    public GetDirectorsQuery(IMovieStoreDbContext dbContext, IMapper mapper)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }

    public List<DirectorsViewModel> Handle()
    {
        var directorsList = dbContext.Directors
            .Include(x => x.DirectedMovies)
            .OrderBy(x => x.Id).ToList();

        List<DirectorsViewModel> vm = mapper.Map<List<DirectorsViewModel>>(directorsList);

        return vm;
    }
}
public class DirectorsViewModel
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public List<Movie> DirectedMovies { get; set; }
}