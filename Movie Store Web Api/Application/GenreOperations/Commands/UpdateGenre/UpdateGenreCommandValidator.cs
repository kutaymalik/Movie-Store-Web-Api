using FluentValidation;

namespace Movie_Store_Web_Api.Application.GenreOperations.Commands.UpdateGenreOperations
{
    public class UpdateGenreCommandValidator : AbstractValidator<UpdateGenreCommand>
    {
        public UpdateGenreCommandValidator()
        {
            RuleFor(x => x.Model.Name).NotEmpty().MinimumLength(3).WithMessage("Name field is required").When(x => x.Model.Name != string.Empty);
        }
    }
}
