using FluentValidation;

namespace Movie_Store_Web_Api.Application.ActorOperations.Queries.GetActorDetail;

public class GetActorDetailQueryValidator : AbstractValidator<GetActorDetailQuery>
{
    public GetActorDetailQueryValidator()
    {
        RuleFor(x => x.ActorId).GreaterThan(0).WithMessage("ActorId field must be greater than 0");
    }
}
