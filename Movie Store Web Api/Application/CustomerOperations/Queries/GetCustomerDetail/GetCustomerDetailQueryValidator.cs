using FluentValidation;

namespace Movie_Store_Web_Api.Application.CustomerOperations.Queries.GetCustomerDetail;

public class GetCustomerDetailQueryValidator : AbstractValidator<GetCustomerDetailQuery>
{
    public GetCustomerDetailQueryValidator()
    {
        RuleFor(x => x.CustomerId).GreaterThan(0).WithMessage("CustomerId field must be greater than 0");
    }
}
