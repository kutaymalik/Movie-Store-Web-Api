using FluentValidation;
using Movie_Store_Web_Api.Application.CustomerOperations.Commands.DeleteCustomer;

namespace Movie_Store_Web_Api.Application.MovieOperations.Commands.DeleteMovie
{
    public class DeleteMovieCommandValidator : AbstractValidator<DeleteMovieCommand>
    {
        public DeleteMovieCommandValidator()
        {
            RuleFor(x => x.MovieId).GreaterThan(0);
        }
    }
}
