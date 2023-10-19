using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Movie_Store_Web_Api.DBOperations;
using Movie_Store_Web_Api.Entities;

namespace Movie_Store_Web_Api.Application.OrderOperations.Commands.PurchaseMovie;

public class PurchaseMovieCommand
{
    private readonly IMovieStoreDbContext dbContext;
    public CreatePurchaseModel Model { get; set; }

    public PurchaseMovieCommand(IMovieStoreDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public void Handle()
    {
        // Finding movie to add
        var movie = dbContext.Movies
            .Include(x => x.Actors)
            .Include(x => x.Genre)
            .Include(x => x.Director)
            .SingleOrDefault(x => x.Id == Model.MovieId);

        if(movie == null)
        {
            throw new InvalidOperationException("The movie you wanted to buy was not found!");
        }

        // Finding the customer who buys
        var customer = dbContext.Customers
            .Include(x => x.FavGenre)
            .Include(x => x.Orders)
            .Include(x => x.PurchasedMovies)
            .SingleOrDefault(x => x.Id == Model.CustomerId);

        if (customer == null)
        {
            throw new InvalidOperationException("The customer information to make the purchase is not correct!");
        }

        customer.PurchasedMovies.Add(movie);


        // Creating order
        var order = new Order();
        order.Price = movie.Price;
        order.MovieId = Model.MovieId;
        order.CustomerId = customer.Id;
        order.PurchaseDate = DateTime.Now;

        // Fav genre finding
        Dictionary<Genre, int> genreCounts = new Dictionary<Genre, int>();

        foreach (var purchasedMovie in customer.PurchasedMovies)
        {
            if (movie.Genre != null)
            {
                if (genreCounts.ContainsKey(purchasedMovie.Genre))
                {
                    genreCounts[purchasedMovie.Genre]++;
                }
                else
                {
                    genreCounts[purchasedMovie.Genre] = 1;
                }
            }
        }

        var mostPurchasedGenre = genreCounts.OrderByDescending(x => x.Value).First().Key;
        customer.FavGenre = mostPurchasedGenre;

        dbContext.Customers.Update(customer);

        dbContext.Orders.Add(order);

        dbContext.SaveChanges();
    }
}

public class CreatePurchaseModel
{
    public int MovieId { get; set; }
    public int CustomerId { get; set; }
}

