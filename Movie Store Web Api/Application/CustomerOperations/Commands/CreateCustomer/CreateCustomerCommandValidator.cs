using FluentValidation;
using Movie_Store_Web_Api.Application.CustomerOperations.Commands.CreateCustomerOperations;


namespace Movie_Store_Web_Api.Application.CustomerOperations.Commands.CreateCustomer;

public class CreateCustomerCommandValidator : AbstractValidator<CreateCustomerCommand>
{
    public CreateCustomerCommandValidator()
    {
        RuleFor(x => x.Model.FirstName).NotEmpty().MinimumLength(3).WithMessage("FirstName field is required");
        RuleFor(x => x.Model.LastName).NotEmpty().MinimumLength(3).WithMessage("LastName field is required");
        RuleFor(x => x.Model.Email).NotEmpty().MinimumLength(3).WithMessage("Email field is required");
        RuleFor(x => x.Model.Password).NotEmpty().MinimumLength(6).WithMessage("Password field is required");
    }
}
