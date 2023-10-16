using FluentValidation;

namespace Movie_Store_Web_Api.Application.CustomerOperations.Commands.UpdateCustomer
{
    public class UpdateCustomerCommandValidator : AbstractValidator<UpdateCustomerCommand>
    {
        public UpdateCustomerCommandValidator()
        {
            RuleFor(x => x.Model.Email).NotEmpty().MinimumLength(3).WithMessage("Email field is required").When(x => x.Model.Email != string.Empty);
            RuleFor(x => x.Model.Password).NotEmpty().MinimumLength(6).WithMessage("Password field is required").When(x => x.Model.Password != string.Empty);
        }
    }
}
