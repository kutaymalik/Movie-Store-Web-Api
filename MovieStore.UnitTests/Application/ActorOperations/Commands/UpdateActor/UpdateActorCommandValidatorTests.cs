using FluentAssertions;
using Movie_Store_Web_Api.Application.ActorOperations.Commands.UpdateActor;
using Movie_Store_Web_Api.Application.GenreOperations.Commands.UpdateGenreOperations;
using MovieStore.UnitTests.TestSetup;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace MovieStore.UnitTests.Application.ActorOperations.Commands.UpdateActor;

public class UpdateActorCommandValidatorTests : IClassFixture<CommonTestFixture>
{
    [Theory]
    [InlineData("1")]
    [InlineData("x")]
    [InlineData("ab")]
    public void WhenModelIsInvalid_Validator_ShouldHaveError(string name)
    {
        // arrange
        var model  = new UpdateActorModel
        {
            FirstName = name,
            LastName = name,
            PlayedMovies = new List<int> { 1, 2 }
        };

        UpdateActorCommand command = new(null);
        command.ActorId = 1;
        command.Model = model;

        // act
        UpdateActorCommandValidator validator = new UpdateActorCommandValidator();
        var result = validator.Validate(command);

        // assert
        result.Errors.Count.Should().BeGreaterThan(0);
    }

    [Theory]
    [InlineData("Deneme1")]
    [InlineData("Deneme Test")]
    public void WhenInputsAreValid_Validator_ShouldNotHaveError(string name)
    {
        // arrange
        var model = new UpdateActorModel
        {
            FirstName = "John",
            LastName = "Doe",
            PlayedMovies = new List<int> { 1, 2 }
        };

        UpdateActorCommand command = new(null);
        command.ActorId = 1;
        command.Model = model;

        // act
        UpdateActorCommandValidator validator = new UpdateActorCommandValidator();
        var result = validator.Validate(command);

        // assert
        result.Errors.Count.Should().Be(0);
    }
}
