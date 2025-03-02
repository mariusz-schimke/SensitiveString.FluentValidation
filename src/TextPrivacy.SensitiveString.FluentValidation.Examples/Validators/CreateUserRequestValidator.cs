using FluentValidation;
using TextPrivacy.SensitiveString.FluentValidation.Examples.Requests;

namespace TextPrivacy.SensitiveString.FluentValidation.Examples.Validators;

public class CreateUserRequestValidator : AbstractValidator<CreateUserRequest>
{
    public CreateUserRequestValidator()
    {
        // THIS THROWS AN EXCEPTION BECAUSE OF A TYPECAST TO AN INTERNAL TYPE WHICH I CAN'T IMPLEMENT IN ANY WAY
        // SO THE APPROACH WITH THE ADAPTERS IS NOT POSSIBLE

        this.RuleForSensitive(x => x.FirstName)
            .NotEmpty()
            .WithErrorCode("code1");

        this.RuleForSensitive(x => x.LastName)
            .NotEmpty();

        this.RuleForEachSensitive(x => x.SecondaryEmails)
            .NotEmpty()
            .NotEqual("doej@example.com")
            .WithErrorCode("code1");
    }
}