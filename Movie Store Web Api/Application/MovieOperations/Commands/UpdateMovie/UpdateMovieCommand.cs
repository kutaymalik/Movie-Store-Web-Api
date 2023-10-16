using AutoMapper;
using Movie_Store_Web_Api.Application.CustomerOperations.Commands.UpdateCustomer;
using Movie_Store_Web_Api.DBOperations;
using Movie_Store_Web_Api.Entities;

namespace Movie_Store_Web_Api.Application.MovieOperations.Commands.UpdateMovie;

public class UpdateMovieCommand
{
    public UpdateMovieModel Model { get; set; }
    private readonly IMovieStoreDbContext dbContext;
    private readonly IMapper mapper;

    public int MovieId { get; set; }

    public UpdateMovieCommand(IMovieStoreDbContext dbContext, IMapper mapper)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }

    public void Handle()
    {
        var movie = dbContext.Movies.SingleOrDefault(x => x.Id == MovieId);

        if (movie == null)
        {
            throw new InvalidOperationException("Record not found!");
        }

        var actors = new List<Actor>();

        foreach (int actorId in Model.Actors)
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


        movie.Name = string.IsNullOrEmpty(Model.Name.Trim()) ? movie.Name : Model.Name;
        movie.Price = Model.Price != default ? Model.Price : movie.Price;
        movie.GenreId = Model.GenreId != default ? Model.GenreId : movie.GenreId;
        movie.DirectorId = Model.DirectorId != default ? Model.DirectorId : movie.DirectorId;
        movie.IsActive = Model.IsActive != default ? Model.IsActive : movie.IsActive;
        movie.Actors = Model.Actors != default? actors : movie.Actors;
        

        dbContext.Movies.Update(movie);
        dbContext.SaveChanges();
    }
}

public class UpdateMovieModel
{
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int GenreId { get; set; }
    public int DirectorId { get; set; }
    public bool IsActive { get; set; }
    public virtual List<int> Actors { get; set; }
}
