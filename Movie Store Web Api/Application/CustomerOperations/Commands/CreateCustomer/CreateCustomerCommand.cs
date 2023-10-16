using AutoMapper;
using Movie_Store_Web_Api.DBOperations;
using Movie_Store_Web_Api.Entities;

namespace Movie_Store_Web_Api.Application.CustomerOperations.Commands.CreateCustomerOperations
{
    public class CreateCustomerCommand
    {
        public CreateCustomerModel Model { get; set; }
        private readonly IMovieStoreDbContext dbContext;
        private readonly IMapper mapper;
        public CreateCustomerCommand(IMovieStoreDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public void Handle()
        {
            var customer = dbContext.Customers.SingleOrDefault(x => x.FirstName == Model.FirstName 
            && x.LastName == Model.LastName
            && x.Email == Model.Email);

            if (customer != null)
            {
                throw new InvalidOperationException("The customer is already exists!");
            }

            customer = mapper.Map<Customer>(Model);

            dbContext.Customers.Add(customer);
            dbContext.SaveChanges();
        }
    }

    public class CreateCustomerModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }

}
