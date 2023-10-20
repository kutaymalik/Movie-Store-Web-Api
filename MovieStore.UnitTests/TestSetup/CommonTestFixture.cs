using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Movie_Store_Web_Api.Common;
using Movie_Store_Web_Api.DBOperations;

namespace MovieStore.UnitTests.TestSetup;

public class CommonTestFixture
{
    public MovieStoreDbContext Context { get; set; }
    public IMapper Mapper { get; set; }

    public CommonTestFixture()
    {
        var options = new DbContextOptionsBuilder<MovieStoreDbContext>()
            .UseInMemoryDatabase(databaseName: "MovieStoreDB").Options;

        Context = new MovieStoreDbContext(options);
        Context.Database.EnsureCreated();


        Context.AddGenres();
        Context.AddDirectors();
        Context.AddActors();
        Context.AddMovies();
        Context.AddCustomers();
        Context.SaveChanges();

        Mapper = new MapperConfiguration(cfg => { cfg.AddProfile<MappingProfile>(); }).CreateMapper();
    }
}
