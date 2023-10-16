using FluentValidation;

namespace Movie_Store_Web_Api.Application.MovieOperations.Queries.GetMovieDetail
{
    public class GetMovieDetailQueryValidator : AbstractValidator<GetMovieDetailQuery>
    {
        public GetMovieDetailQueryValidator()
        {
            RuleFor(x => x.MovieId).GreaterThan(0).WithMessage("MovieId field must be greater than 0");
        }
    }
}
