﻿using Microsoft.EntityFrameworkCore;
using Movie_Store_Web_Api.Entities;

namespace Movie_Store_Web_Api.DBOperations;

public interface IMovieStoreDbContext
{
    DbSet<Actor> Actors { get; set; }
    DbSet<Customer> Customers { get; set; }
    DbSet<Director> Directors { get; set; }
    DbSet<Genre> Genres { get; set; }
    DbSet<Movie> Movies { get; set; }
    DbSet<Order> Orders { get; set; }

    int SaveChanges();
}
