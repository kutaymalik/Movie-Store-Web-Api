using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Movie_Store_Web_Api.Application.CustomerOperations.Queries.GetCustomers;
using Movie_Store_Web_Api.DBOperations;
using Movie_Store_Web_Api.Entities;

namespace Movie_Store_Web_Api.Application.OrderOperations.Queries.GetOrders;

public class GetOrdersQuery
{
    private readonly IMovieStoreDbContext dbContext;
    private readonly IMapper mapper;

    public GetOrdersQuery(IMovieStoreDbContext dbContext, IMapper mapper)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }

    public List<OrderViewModel> Handle()
    {
        var orderList = dbContext.Orders
            .Include(x => x.Customer).ThenInclude(x => x.FavGenre)
            .Include(x => x.Movie)
            .OrderBy(x => x.Id).ToList();

        List<OrderViewModel> vm = mapper.Map<List<OrderViewModel>>(orderList);

        return vm;
    }
}

public class OrderViewModel
{
    public string MovieName { get; set; }
    public string CustomerName { get; set; }
    public decimal Price { get; set; }
    public DateTime PurchaseDate { get; set; }
}