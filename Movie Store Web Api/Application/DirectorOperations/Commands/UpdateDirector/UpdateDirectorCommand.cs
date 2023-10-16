using Movie_Store_Web_Api.Application.ActorOperations.Commands.UpdateActor;
using Movie_Store_Web_Api.DBOperations;

namespace Movie_Store_Web_Api.Application.DirectorOperations.Commands.UpdateDirector;

public class UpdateDirectorCommand
{
    public UpdateDirectorModel Model { get; set; }
    private readonly IMovieStoreDbContext dbContext;
    public int DirectorId { get; set; }

    public UpdateDirectorCommand(IMovieStoreDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public void Handle()
    {
        var director = dbContext.Directors.SingleOrDefault(x => x.Id == DirectorId);

        if (director == null)
        {
            throw new InvalidOperationException("Record not found!");
        }


        director.FirstName = string.IsNullOrEmpty(Model.FirstName.Trim()) ? director.FirstName : Model.FirstName;
        director.LastName = string.IsNullOrEmpty(Model.LastName.Trim()) ? director.LastName : Model.LastName;

        dbContext.Directors.Update(director);
        dbContext.SaveChanges();
    }
}
public class UpdateDirectorModel
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
}