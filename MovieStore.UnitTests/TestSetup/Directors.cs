using Movie_Store_Web_Api.DBOperations;
using Movie_Store_Web_Api.Entities;

namespace MovieStore.UnitTests.TestSetup;

public static class Directors
{
    public static void AddDirectors(this MovieStoreDbContext context)
    {
        context.Directors.AddRange(
                new Director
                {
                    FirstName = "Louis",
                    LastName = "Leterrier",
                },
                new Director
                {
                    FirstName = "Cristopher",
                    LastName = "Nolan",
                },
                new Director
                {
                    FirstName = "Anthony",
                    LastName = "Russo",
                },
                new Director
                {
                    FirstName = "Tim",
                    LastName = "Burton",
                },
                new Director
                {
                    FirstName = "Andrey",
                    LastName = "Tarkovski",
                },
                new Director
                {
                    FirstName = "Lana",
                    LastName = "Wachowski",
                }
                );
    }
}
