using AutoMapper;
using Movie_Store_Web_Api.DBOperations;
using Movie_Store_Web_Api.Entities;

namespace Movie_Store_Web_Api.Application.CustomerOperations.Commands.UpdateCustomer;

public class UpdateCustomerCommand
{
    public UpdateCustomerModel Model { get; set; }
    private readonly IMovieStoreDbContext dbContext;
    private readonly IMapper mapper;

    public int CustomerId { get; set; }

    public UpdateCustomerCommand(IMovieStoreDbContext dbContext, IMapper mapper)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }

    public void Handle()
    {
        var customer = dbContext.Customers.SingleOrDefault(x => x.Id == CustomerId);

        if (customer == null)
        {
            throw new InvalidOperationException("Record not found!");
        }

        if(dbContext.Customers.Any(x=> x.Email == customer.Email))
        {
            throw new InvalidOperationException("The Customer Email already exists!");
        }


        customer.Email = string.IsNullOrEmpty(Model.Email.Trim()) ? customer.Email : Model.Email;
        customer.Password = string.IsNullOrEmpty(Model.Password.Trim()) ? customer.Password : Model.Password;

        dbContext.Customers.Update(customer);
        dbContext.SaveChanges();
    }
}
public class UpdateCustomerModel
{
    public string Email { get; set; }
    public string Password { get; set; }
}