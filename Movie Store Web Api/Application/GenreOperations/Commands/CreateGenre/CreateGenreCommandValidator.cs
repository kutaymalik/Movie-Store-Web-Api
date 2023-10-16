using FluentValidation;

namespace Movie_Store_Web_Api.Application.GenreOperations.Commands.CreateGenre;

public class CreateGenreCommandValidator : AbstractValidator<CreateGenreCommand>
{
    public CreateGenreCommandValidator()
    {
        RuleFor(x => x.Model.Name).NotEmpty().MinimumLength(3).WithMessage("Name field is required");
    }
}
