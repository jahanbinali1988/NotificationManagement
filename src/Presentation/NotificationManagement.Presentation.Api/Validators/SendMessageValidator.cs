using FluentValidation;
using NotificationManagement.Presentation.Api.Messages.User.Request;

namespace NotificationManagement.Presentation.Api.Validators
{
    public class SendMessageValidator : AbstractValidator<SendMessageRequest>
    {
        public SendMessageValidator()
        {
            RuleFor(c => c.Content)
                .NotNull()
                .NotEmpty()
                .WithMessage("Message should have content");

            RuleFor(c => c.Title)
                .NotNull()
                .NotEmpty()
                .WithMessage("Message should have title");

            RuleFor(c => c.UserId)
                .NotNull()
                .NotEmpty()
                .WithMessage("Message should have reciever");
        }
    }
}
