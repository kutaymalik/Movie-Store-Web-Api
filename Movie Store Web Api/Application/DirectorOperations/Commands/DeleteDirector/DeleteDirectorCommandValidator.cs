using FluentValidation;

namespace Movie_Store_Web_Api.Application.DirectorOperations.Commands.DeleteDirector;

public class DeleteDirectorCommandValidator : AbstractValidator<DeleteDirectorCommand>
{
    public DeleteDirectorCommandValidator()
    {
        RuleFor(x => x.DirectorId).NotEmpty().GreaterThan(0).WithMessage("DirectorId field is required");
    }
}
