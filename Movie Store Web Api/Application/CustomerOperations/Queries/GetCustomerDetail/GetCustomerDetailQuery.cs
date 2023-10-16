using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Movie_Store_Web_Api.DBOperations;

namespace Movie_Store_Web_Api.Application.CustomerOperations.Queries.GetCustomerDetail
{
    public class GetCustomerDetailQuery
    {
        private readonly IMovieStoreDbContext dbContext;
        public int CustomerId { get; set; }
        private readonly IMapper mapper;

        public GetCustomerDetailQuery(IMovieStoreDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public CustomerDetailViewModel Handle()
        {
            var customer = dbContext.Customers
                .Include(x => x.FavGenre)
                .Include(x => x.PurchasedMovies)
                .Include(x => x.Orders)
                .Where(x => x.Id == CustomerId).SingleOrDefault();

            if (customer == null)
            {
                throw new InvalidOperationException("Record not found!");
            }

            CustomerDetailViewModel vm = mapper.Map<CustomerDetailViewModel>(customer);

            return vm;
        }
    }
}

public class CustomerDetailViewModel
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PurchasedMovies { get; set; }
    public string FavGenres { get; set; }
    public string Orders { get; set; }
}
