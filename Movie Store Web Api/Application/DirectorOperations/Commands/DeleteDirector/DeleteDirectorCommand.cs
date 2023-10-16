using Microsoft.EntityFrameworkCore;
using Movie_Store_Web_Api.DBOperations;
using Movie_Store_Web_Api.Entities;

namespace Movie_Store_Web_Api.Application.DirectorOperations.Commands.DeleteDirector;

public class DeleteDirectorCommand
{
    public int DirectorId { get; set; }
    private readonly IMovieStoreDbContext dbContext;

    public DeleteDirectorCommand(IMovieStoreDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public void Handle()
    {
        var director = dbContext.Directors
            .Include(x => x.DirectedMovies)
            .SingleOrDefault(x => x.Id == DirectorId);  

        if(director == null)
        {
            throw new InvalidOperationException("Record not found!");
        }

        if (director.DirectedMovies.Any())
        {
            throw new InvalidOperationException("You must first delete the directed movies!");
        }

        dbContext.Directors.Remove(director);
        dbContext.SaveChanges();
    }
}
