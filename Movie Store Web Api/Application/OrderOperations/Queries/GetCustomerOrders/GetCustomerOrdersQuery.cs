using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Movie_Store_Web_Api.Application.CustomerOperations.Queries.GetCustomers;
using Movie_Store_Web_Api.DBOperations;
using Movie_Store_Web_Api.Entities;

namespace Movie_Store_Web_Api.Application.OrderOperations.Queries.GetCustomerOrders;

public class GetCustomerOrdersQuery
{
    private readonly IMovieStoreDbContext dbContext;
    private readonly IMapper mapper;
    private readonly IHttpContextAccessor httpContextAccessor;

    public GetCustomerOrdersQuery(IMovieStoreDbContext dbContext, IMapper mapper, IHttpContextAccessor httpContextAccessor)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
        this.httpContextAccessor = httpContextAccessor;
    }

    public List<CustomerOrderViewModel> Handle()
    {
        int sessionId = CheckSession();

        var customer = dbContext.Customers.Include(x => x.Orders).Include(x => x.PurchasedMovies).SingleOrDefault(x => x.Id == sessionId);

        if(customer == null)
        {
            throw new InvalidOperationException("Customer record not found!");
        }

        List<Order> orderList = customer.Orders.ToList();

        List<CustomerOrderViewModel> vm = mapper.Map<List<CustomerOrderViewModel>>(orderList);

        return vm;
    }

    private int CheckSession()
    {
        var sessionIdClaim = httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == "Id");

        if (sessionIdClaim == null || !int.TryParse(sessionIdClaim.Value, out int sessionId))
        {
            throw new InvalidOperationException("Session id not found!");
        }

        return sessionId;
    }
}

public class CustomerOrderViewModel
{
    public string MovieName { get; set; }
    public string CustomerName { get; set; }
    public decimal Price { get; set; }
    public DateTime PurchaseDate { get; set; }
}