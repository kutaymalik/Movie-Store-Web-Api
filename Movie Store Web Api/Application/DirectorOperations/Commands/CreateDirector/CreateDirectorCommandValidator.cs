using FluentValidation;

namespace Movie_Store_Web_Api.Application.DirectorOperations.Commands.CreateDirector
{
    public class CreateDirectorCommandValidator : AbstractValidator<CreateDirectorCommand>
    {
        public CreateDirectorCommandValidator()
        {
            RuleFor(x => x.Model.FirstName).NotEmpty().MinimumLength(3).WithMessage("FirstName field is required");
            RuleFor(x => x.Model.LastName).NotEmpty().MinimumLength(3).WithMessage("LastName field is required");
        }
    }
}
