using FluentAssertions;
using Movie_Store_Web_Api.Application.ActorOperations.Commands.DeleteActor;
using Movie_Store_Web_Api.Application.GenreOperations.Commands.DeleteGenre;
using Movie_Store_Web_Api.DBOperations;
using MovieStore.UnitTests.TestSetup;

namespace MovieStore.UnitTests.Application.ActorOperations.Commands.DeleteActor;

public class DeleteActorCommandTests : IClassFixture<CommonTestFixture>
{
    private readonly MovieStoreDbContext context;

    public DeleteActorCommandTests(CommonTestFixture testFixture)
    {
        this.context = testFixture.Context;
    }

    [Fact]
    public void WhenGivenActorIsNotFound_InvalidOperationException_ShouldBeReturn()
    {
        // Arrange

        DeleteActorCommand command = new(context);
        command.ActorId = 999;

        // act & assert
        FluentActions
            .Invoking(() => command.Handle())
            .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Record not found!");
    }

    [Fact]
    public void WhenValidInputsAreGiven_Genre_ShouldBeDeleted()
    {
        // arrange
        DeleteActorCommand command = new(context);
        command.ActorId = 1;

        // act
        FluentActions.Invoking(() => command.Handle()).Invoke();
        // assert
        var actor = context.Actors.SingleOrDefault(g => g.Id == command.ActorId);
        actor.Should().BeNull();
    }
}
