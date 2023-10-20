using AutoMapper;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Movie_Store_Web_Api.Application.ActorOperations.Commands.CreateActorOperations;
using Movie_Store_Web_Api.DBOperations;
using Movie_Store_Web_Api.Entities;
using MovieStore.UnitTests.TestSetup;

namespace MovieStore.UnitTests.Application.ActorOperations.Commands.CreateActor;

public class CreateActorCommandTests : IClassFixture<CommonTestFixture>
{
    private readonly MovieStoreDbContext context;
    private readonly IMapper mapper;

    public CreateActorCommandTests(CommonTestFixture testFixture)
    {
        this.context = testFixture.Context;
        this.mapper = testFixture.Mapper;
    }

    [Fact]
    public void WhenAlreadyExistActorNameIsGiven_InvalidOperationException_ShouldBeReturn()
    {
        // Arrange
        var actor = new Actor()
        {
            FirstName = "Jon",
            LastName = "Doe"
        };

        context.Actors.Add(actor);
        context.SaveChanges();

        CreateActorCommand command = new CreateActorCommand(context);
        command.Model = new CreateActorModel { FirstName = actor.FirstName, LastName = actor.LastName };

        // Act & Assert
        FluentActions
            .Invoking(() => command.Handle())
            .Should().Throw<InvalidOperationException>().And.Message.Should().Be("The actor is already exists!");
    }

    [Fact]
    public void WhenValidInputsAreGiven_Actor_ShouldBeCreated()
    {
        CreateActorCommand command = new CreateActorCommand(context);
        CreateActorModel model = new CreateActorModel()
        {
            FirstName = "John",
            LastName = "Doe",
            PlayedMovies = new List<int> {1,2}
        };

        command.Model = model;

        // act
        FluentActions.Invoking(() => command.Handle()).Invoke();
        // assert
        var actor = context.Actors
            .Include(x => x.PlayedMovies)
            .SingleOrDefault(x => x.FirstName.ToLower() == model.FirstName.ToLower() && x.LastName.ToLower() == model.LastName.ToLower());
        actor.Should().NotBeNull();
        actor.LastName.Should().Be(model.LastName);
        actor.PlayedMovies.Should().HaveCount(2);
    }
}
