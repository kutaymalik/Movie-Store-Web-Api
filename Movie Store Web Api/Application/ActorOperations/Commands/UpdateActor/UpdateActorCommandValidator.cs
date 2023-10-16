using FluentValidation;

namespace Movie_Store_Web_Api.Application.ActorOperations.Commands.UpdateActor
{
    public class UpdateActorCommandValidator : AbstractValidator<UpdateActorCommand>
    {
        public UpdateActorCommandValidator()
        {
            RuleFor(x => x.Model.FirstName).NotEmpty().MinimumLength(3).WithMessage("FirstName field is required").When(x => x.Model.FirstName != string.Empty);
            RuleFor(x => x.Model.LastName).NotEmpty().MinimumLength(3).WithMessage("LastName field is required").When(x => x.Model.LastName != string.Empty);
        }
    }
}
