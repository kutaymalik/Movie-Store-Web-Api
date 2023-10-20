using FluentAssertions;
using Movie_Store_Web_Api.Application.ActorOperations.Commands.CreateActor;
using Movie_Store_Web_Api.Application.ActorOperations.Commands.CreateActorOperations;
using Movie_Store_Web_Api.Application.GenreOperations.Commands.CreateGenre;
using MovieStore.UnitTests.TestSetup;

namespace MovieStore.UnitTests.Application.ActorOperations.Commands.CreateActor;

public class CreateActorCommandValidatorTests : IClassFixture<CommonTestFixture>
{
    [Theory]
    [InlineData("a")]
    [InlineData("ab")]
    public void WhenInvalidInputsAreGiven_Validator_ShouldReturnErrors(string name)
    {
        // arrange
        CreateActorCommand command = new CreateActorCommand(null);
        command.Model = new CreateActorModel() { FirstName = name, LastName = name, PlayedMovies = new List<int> { 1, 2 } };

        CreateActorCommandValidator validator = new();

        // act
        var result = validator.Validate(command);

        // assert
        result.Errors.Count.Should().BeGreaterThan(0);
    }
}
