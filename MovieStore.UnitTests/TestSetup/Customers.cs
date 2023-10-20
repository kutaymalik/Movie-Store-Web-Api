

using Movie_Store_Web_Api.DBOperations;
using Movie_Store_Web_Api.Entities;

namespace MovieStore.UnitTests.TestSetup;

public static class Customers
{
    public static void AddCustomers(this MovieStoreDbContext context)
    {
        context.Customers.AddRange(
                new Customer
                {
                    FirstName = "firstname1",
                    LastName = "lastname1",
                    Email = "lastname1",
                    Password = "83279bee0277f07725f8c9f83b78a635",
                    PurchasedMovies = new List<Movie>(),
                    FavGenreId = 1,
                    Orders = new List<Order>(),
                },
                new Customer
                {
                    FirstName = "John",
                    LastName = "Doe",
                    Email = "john@doe@mail.com",
                    Password = "32250170a0dca92d53ec9624f336ca24",
                    PurchasedMovies = new List<Movie>(),
                    FavGenreId = 1,
                    Orders = new List<Order>(),
                });
    }
}
