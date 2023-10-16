using Movie_Store_Web_Api.DBOperations;

namespace Movie_Store_Web_Api.Application.ActorOperations.Commands.UpdateActor;

public class UpdateActorCommand
{
    public UpdateActorModel Model { get; set; }
    private readonly IMovieStoreDbContext dbContext;
    public int ActorId { get; set; }

    public UpdateActorCommand(IMovieStoreDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public void Handle()
    {
        var actor = dbContext.Actors.SingleOrDefault(x => x.Id == ActorId);

        if (actor == null)
        {
            throw new InvalidOperationException("Record not found!");
        }


        actor.FirstName = string.IsNullOrEmpty(Model.FirstName.Trim()) ? actor.FirstName : Model.FirstName;
        actor.LastName = string.IsNullOrEmpty(Model.LastName.Trim()) ? actor.LastName : Model.LastName;

        dbContext.Actors.Update(actor);
        dbContext.SaveChanges();
    }
}
public class UpdateActorModel
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
}