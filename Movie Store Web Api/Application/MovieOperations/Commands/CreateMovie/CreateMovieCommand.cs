using AutoMapper;
using Movie_Store_Web_Api.Application.CustomerOperations.Commands.CreateCustomerOperations;
using Movie_Store_Web_Api.DBOperations;
using Movie_Store_Web_Api.Entities;

namespace Movie_Store_Web_Api.Application.MovieOperations.Commands.CreateMovie;

public class CreateMovieCommand
{
    public CreateMovieModel Model { get; set; }
    private readonly IMovieStoreDbContext dbContext;
    private readonly IMapper mapper;
    public CreateMovieCommand(IMovieStoreDbContext dbContext, IMapper mapper)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }

    public void Handle()
    {
        Movie movie = dbContext.Movies.SingleOrDefault(x => x.Name == Model.Name);

        if (movie != null)
        {
            throw new InvalidOperationException("The movie is already exists!");
        }
        
        var actors = new List<Actor>();

        foreach(int actorId in Model.Actors)
        {
            var actor = dbContext.Actors.SingleOrDefault(a => a.Id == actorId);
            if (actor != null)
            {
                actors.Add(actor);
            }
            else
            {
                throw new InvalidOperationException($"Actor with ID {actorId} not found.");
            }
        }
        movie = new Movie();

        movie.Name = Model.Name;
        movie.ReleaseDate = Model.ReleaseDate;
        movie.Price = Model.Price;
        movie.GenreId = Model.GenreId;
        movie.DirectorId = Model.DirectorId;
        movie.Actors = actors;

        dbContext.Movies.Add(movie);
        dbContext.SaveChanges();
    }
}

public class CreateMovieModel
{
    public string Name { get; set; }
    public DateTime ReleaseDate { get; set; }
    public decimal Price { get; set; }

    public int GenreId { get; set; }

    public int DirectorId { get; set; }

    public virtual List<int> Actors { get; set; }
}