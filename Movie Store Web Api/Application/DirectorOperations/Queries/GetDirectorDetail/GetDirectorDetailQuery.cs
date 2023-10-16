using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Movie_Store_Web_Api.Application.ActorOperations.Queries.GetActorDetail;
using Movie_Store_Web_Api.DBOperations;
using Movie_Store_Web_Api.Entities;

namespace Movie_Store_Web_Api.Application.DirectorOperations.Queries.GetDirectorDetail;

public class GetDirectorDetailQuery
{
    private readonly IMovieStoreDbContext dbContext;
    public int DirectorId { get; set; }
    private readonly IMapper mapper;

    public GetDirectorDetailQuery(IMovieStoreDbContext dbContext, IMapper mapper)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }
    public DirectorDetailViewModel Handle()
    {
        var director = dbContext.Directors
            .Include(x => x.DirectedMovies)
            .Where(x => x.Id == DirectorId).SingleOrDefault();

        if (director == null)
        {
            throw new InvalidOperationException("Record not found!");
        }

        

        DirectorDetailViewModel vm = mapper.Map<DirectorDetailViewModel>(director);

        return vm;
    }

}
public class DirectorDetailViewModel
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public virtual List<Movie> DirectedMovies { get; set; }
}