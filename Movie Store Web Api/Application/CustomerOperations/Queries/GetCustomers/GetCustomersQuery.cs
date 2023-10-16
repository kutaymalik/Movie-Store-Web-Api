using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Movie_Store_Web_Api.DBOperations;

namespace Movie_Store_Web_Api.Application.CustomerOperations.Queries.GetCustomers;

public class GetCustomersQuery
{
    private readonly IMovieStoreDbContext dbContext;
    private readonly IMapper mapper;

    public GetCustomersQuery(IMovieStoreDbContext dbContext, IMapper mapper)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }

    public List<CustomerViewModel> Handle()
    {
        var customerList = dbContext.Customers
            .Include(x => x.FavGenre)
                .Include(x => x.PurchasedMovies)
                .Include(x => x.Orders)
                .OrderBy(x => x.Id).ToList();

        List<CustomerViewModel> vm = mapper.Map<List<CustomerViewModel>>(customerList);

        return vm;
    }

}

public class CustomerViewModel
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PurchasedMovies { get; set; }
    public string FavGenres { get; set; }
    public string Orders { get; set; }
}
