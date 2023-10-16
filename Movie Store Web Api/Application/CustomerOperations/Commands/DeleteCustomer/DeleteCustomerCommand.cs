using Movie_Store_Web_Api.DBOperations;

namespace Movie_Store_Web_Api.Application.CustomerOperations.Commands.DeleteCustomer;

public class DeleteCustomerCommand
{
    private readonly IMovieStoreDbContext dbContext;
    public int CustomerId { get; set; }

    public DeleteCustomerCommand(IMovieStoreDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public void Handle()
    {
        var customer = dbContext.Customers.SingleOrDefault(x => x.Id == CustomerId);

        if(customer == null)
        {
            throw new InvalidOperationException("Record not found!");
        }

        dbContext.Customers.Remove(customer);
        dbContext.SaveChanges();
    }
}
