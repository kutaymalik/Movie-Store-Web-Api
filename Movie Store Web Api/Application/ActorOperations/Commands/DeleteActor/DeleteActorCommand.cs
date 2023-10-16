using Movie_Store_Web_Api.DBOperations;

namespace Movie_Store_Web_Api.Application.ActorOperations.Commands.DeleteActor;

public class DeleteActorCommand
{
    private readonly IMovieStoreDbContext dbContext;
    public int ActorId { get; set; }

    public DeleteActorCommand(IMovieStoreDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public void Handle()
    {
        var actor = dbContext.Actors.SingleOrDefault(x => x.Id == ActorId);

        if(actor == null)
        {
            throw new InvalidOperationException("Record not found!");
        }

        dbContext.Actors.Remove(actor);
        dbContext.SaveChanges();
    }
}
