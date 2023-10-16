﻿using FluentValidation;

namespace Movie_Store_Web_Api.Application.ActorOperations.Commands.DeleteActor
{
    public class DeleteActorCommandValidator : AbstractValidator<DeleteActorCommand>
    {
        public DeleteActorCommandValidator()
        {
            RuleFor(x => x.ActorId).GreaterThan(0);
        }
    }
}
