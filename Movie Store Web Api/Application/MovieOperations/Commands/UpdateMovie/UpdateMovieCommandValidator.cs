using FluentValidation;

namespace Movie_Store_Web_Api.Application.MovieOperations.Commands.UpdateMovie
{
    public class UpdateMovieCommandValidator : AbstractValidator<UpdateMovieCommand>
    {
        public UpdateMovieCommandValidator()
        {
            RuleFor(x => x.Model.Name).MinimumLength(3).WithMessage("Name field is required").When(x => x.Model.Name !=  string.Empty);
            RuleFor(x => x.Model.Price).GreaterThan(0).WithMessage("Price field is required").When(x => x.Model.Price != default);
            RuleFor(x => x.Model.GenreId).GreaterThan(0).WithMessage("GenreId field is required").When(x => x.Model.GenreId != default);
            RuleFor(x => x.Model.DirectorId).GreaterThan(0).WithMessage("DirectorId field is required").When(x => x.Model.DirectorId != default);
        }
    }
}
