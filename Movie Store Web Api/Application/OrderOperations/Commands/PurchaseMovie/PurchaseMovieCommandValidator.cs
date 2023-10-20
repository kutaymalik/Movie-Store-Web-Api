using FluentValidation;

namespace Movie_Store_Web_Api.Application.OrderOperations.Commands.PurchaseMovie;

public class PurchaseMovieCommandValidator : AbstractValidator<PurchaseMovieCommand>
{
    public PurchaseMovieCommandValidator()
    {
        RuleFor(x => x.Model.MovieId).NotEmpty().GreaterThan(0).WithMessage("MovieId field is required");
    }
}
