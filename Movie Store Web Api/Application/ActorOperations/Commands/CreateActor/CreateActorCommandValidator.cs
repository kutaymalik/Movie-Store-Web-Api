using FluentValidation;
using Movie_Store_Web_Api.Application.ActorOperations.Commands.CreateActorOperations;

namespace Movie_Store_Web_Api.Application.ActorOperations.Commands.CreateActor;

public class CreateActorCommandValidator : AbstractValidator<CreateActorCommand>
{
    public CreateActorCommandValidator()
    {
        RuleFor(x => x.Model.FirstName).NotEmpty().MinimumLength(3).WithMessage("FirstName field is required");
        RuleFor(x => x.Model.LastName).NotEmpty().MinimumLength(3).WithMessage("LastName field is required");
    }
}
