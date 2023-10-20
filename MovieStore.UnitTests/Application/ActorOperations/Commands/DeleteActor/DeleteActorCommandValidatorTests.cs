using FluentAssertions;
using Movie_Store_Web_Api.Application.ActorOperations.Commands.DeleteActor;
using Movie_Store_Web_Api.Application.GenreOperations.Commands.DeleteGenre;
using MovieStore.UnitTests.TestSetup;

namespace MovieStore.UnitTests.Application.ActorOperations.Commands.DeleteActor;

public class DeleteActorCommandValidatorTests : IClassFixture<CommonTestFixture>
{
    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(-100)]
    public void WhenActorIdLessThanOrEqualZero_ValidationShouldReturnError(int genreId)
    {
        // arrange
        DeleteActorCommand command = new(null);
        command.ActorId = genreId;

        // act
        DeleteActorCommandValidator validator = new DeleteActorCommandValidator();
        var result = validator.Validate(command);

        // assert
        result.Errors.Count.Should().BeGreaterThan(0);
    }

    [Fact]
    public void WhenActorIdGreaterThanZero_ValidationShouldNotReturnError()
    {
        // arrange
        DeleteActorCommand command = new(null);
        command.ActorId = 1;

        // act
        DeleteActorCommandValidator validator = new DeleteActorCommandValidator();
        var result = validator.Validate(command);

        // assert
        result.Errors.Count.Should().Be(0);
    }
}
