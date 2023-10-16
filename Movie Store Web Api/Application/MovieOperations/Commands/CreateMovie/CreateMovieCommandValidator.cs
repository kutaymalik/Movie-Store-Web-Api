using FluentValidation;
using Movie_Store_Web_Api.Application.ActorOperations.Commands.CreateActorOperations;

namespace Movie_Store_Web_Api.Application.MovieOperations.Commands.CreateMovie;
public class CreateMovieCommandValidator : AbstractValidator<CreateMovieCommand>
{
    public CreateMovieCommandValidator()
    {
        RuleFor(x => x.Model.Name).NotEmpty().MinimumLength(3).WithMessage("Name field is required");
        RuleFor(x => x.Model.ReleaseDate).NotEmpty().WithMessage("ReleaseDate field is required");
        RuleFor(x => x.Model.Price).NotEmpty().GreaterThan(0).WithMessage("Price field is required");
        RuleFor(x => x.Model.GenreId).NotEmpty().GreaterThan(0).WithMessage("GenreId field is required");
        RuleFor(x => x.Model.DirectorId).NotEmpty().GreaterThan(0).WithMessage("DirectorId field is required");
    }
}
