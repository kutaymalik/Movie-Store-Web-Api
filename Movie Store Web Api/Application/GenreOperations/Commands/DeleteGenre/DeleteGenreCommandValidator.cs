using FluentValidation;
using Movie_Store_Web_Api.Application.CustomerOperations.Commands.DeleteCustomer;

namespace Movie_Store_Web_Api.Application.GenreOperations.Commands.DeleteGenre
{
    public class DeleteGenreCommandValidator : AbstractValidator<DeleteGenreCommand>
    {
        public DeleteGenreCommandValidator()
        {
            RuleFor(x => x.GenreId).GreaterThan(0);
        }
    }
}
