using FluentAssertions;
using Movie_Store_Web_Api.Application.ActorOperations.Commands.UpdateActor;
using Movie_Store_Web_Api.Application.GenreOperations.Commands.UpdateGenreOperations;
using Movie_Store_Web_Api.DBOperations;
using Movie_Store_Web_Api.Entities;
using MovieStore.UnitTests.TestSetup;

namespace MovieStore.UnitTests.Application.ActorOperations.Commands.UpdateActor;

public class UpdateActorCommandTests : IClassFixture<CommonTestFixture>
{
    private readonly MovieStoreDbContext context;

    public UpdateActorCommandTests(CommonTestFixture testFixture)
    {
        this.context = testFixture.Context;
    }

    [Fact]
    public void WhenGivenActorIsNotFound_InvalidOperationException_ShouldBeReturn()
    {
        // Arrange

        UpdateActorCommand command = new(context);
        command.ActorId = 999;

        // Act & Assert
        FluentActions
            .Invoking(() => command.Handle())
            .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Record not found!");
    }

    [Fact]
    public void WhenValidInputsAreGiven_Actor_ShouldBeUpdated()
    {
        // arrange
        UpdateActorCommand command = new(context);
        var actor = new Actor { FirstName = "Deneme",  LastName = "Deneme"};

        context.Actors.Add(actor);
        context.SaveChanges();

        command.ActorId = actor.Id;
        UpdateActorModel model = new UpdateActorModel
        {
            FirstName = "John",
            LastName = "Doe",
            PlayedMovies = new List<int> { 1, 2 }
        };

        command.Model = model;

        // act
        FluentActions.Invoking(() => command.Handle()).Invoke();
        // assert
        var updatedActor = context.Actors.SingleOrDefault(g => g.Id == actor.Id);
        updatedActor.Should().NotBeNull();
        updatedActor.FirstName.Should().Be(model.FirstName);
        updatedActor.LastName.Should().Be(model.LastName);
        updatedActor.PlayedMovies.Count().Should().Be(2);
    }
}
