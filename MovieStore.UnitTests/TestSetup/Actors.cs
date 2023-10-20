using Movie_Store_Web_Api.DBOperations;
using Movie_Store_Web_Api.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStore.UnitTests.TestSetup;

public static class Actors
{
    public static void AddActors(this MovieStoreDbContext context)
    {
        context.Actors.AddRange(
                new Actor
                {
                    FirstName = "Michelle",
                    LastName = "Rodriguez",
                },
                new Actor
                {
                    FirstName = "Robert",
                    LastName = "Downey",
                },
                new Actor
                {
                    FirstName = "Paul",
                    LastName = "Walker",
                },
                new Actor
                {
                    FirstName = "Scarlett",
                    LastName = "Johansson",
                },
                new Actor
                {
                    FirstName = "Vin",
                    LastName = "Diesel",
                },
                new Actor
                {
                    FirstName = "Keanu",
                    LastName = "Reeves",
                },
                new Actor
                {
                    FirstName = "Chris",
                    LastName = "Evans",
                },
                new Actor
                {
                    FirstName = "Tom",
                    LastName = "Hiddleston",
                },
                new Actor
                {
                    FirstName = "Morgan",
                    LastName = "Freeman",
                },
                new Actor
                {
                    FirstName = "Benedict",
                    LastName = "Cumberbatch",
                }
                );
    }
}
