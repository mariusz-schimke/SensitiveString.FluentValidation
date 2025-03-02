using FluentValidation;
using TextPrivacy.SensitiveString.FluentValidation.Examples.Requests;

namespace TextPrivacy.SensitiveString.FluentValidation.Examples.Validators;

public class CreateUserRequestValidator : AbstractValidator<CreateUserRequest>
{
    public CreateUserRequestValidator()
    {
        this.RuleForSensitive(x => x.FirstName)
            .NotEmpty()
            .WithErrorCode("empty_value");

        this.RuleForSensitive(x => x.LastName)
            .NotEmpty()
            .WithErrorCode("empty_value");

        this.RuleForSensitive(x => x.PrimaryEmail)
            .NotEmpty()
            .EmailAddress()
            .WithErrorCode("invalid_email");

        this.RuleForEachSensitive(x => x.SecondaryEmails)
            .NotEmpty()
            .EmailAddress()
            .WithErrorCode("invalid_email");
    }
}