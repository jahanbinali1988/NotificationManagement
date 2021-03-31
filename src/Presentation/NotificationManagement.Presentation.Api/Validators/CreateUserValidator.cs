using FluentValidation;
using NotificationManagement.Presentation.Api.Messages.Message.Request;
using System;
using System.Collections.Generic;
using System.Text;

namespace NotificationManagement.Presentation.Api.Validators
{
    public class CreateUserValidator : AbstractValidator<CreateUserRequest>
    {
        public CreateUserValidator()
        {
            RuleFor(c => c.Mobile)
                .NotNull()
                .NotEmpty()
                .WithMessage("Message should have mobile number");

            RuleFor(c => c.Email)
                .NotNull()
                .NotEmpty()
                .WithMessage("Message should have emial address");
        }
    }
}
