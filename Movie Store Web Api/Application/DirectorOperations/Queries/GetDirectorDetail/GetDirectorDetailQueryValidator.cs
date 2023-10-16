using FluentValidation;

namespace Movie_Store_Web_Api.Application.DirectorOperations.Queries.GetDirectorDetail;

public class GetDirectorDetailQueryValidator : AbstractValidator<GetDirectorDetailQuery>
{
    public GetDirectorDetailQueryValidator()
    {
        RuleFor(x => x.DirectorId).GreaterThan(0).WithMessage("DirectorId field must be greater than 0");
    }
}
