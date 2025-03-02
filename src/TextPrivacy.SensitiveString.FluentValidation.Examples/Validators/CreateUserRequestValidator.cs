using FluentValidation;
using TextPrivacy.SensitiveString.FluentValidation.Examples.Requests;

namespace TextPrivacy.SensitiveString.FluentValidation.Examples.Validators;

public class CreateUserRequestValidator : AbstractValidator<CreateUserRequest>
{
    public CreateUserRequestValidator()
    {
        this.RuleForSensitiveString(x => x.FirstName)
            .NotEmpty();

        this.RuleForSensitiveString(x => x.LastName)
            .NotEmpty();

        this.RuleForEachSensitiveString(x => x.AddressLines)
            .NotEmpty();
        this.RuleForEachSensitiveString(x => x.SecondaryEmails)
            .NotEmpty();
    }
}