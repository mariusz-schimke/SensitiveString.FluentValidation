# SensitiveString.FluentValidation
[![NuGet Version](http://img.shields.io/nuget/v/SensitiveString.FluentValidation.svg?style=for-the-badge&logo=nuget)](https://www.nuget.org/packages/SensitiveString.FluentValidation/) [![NuGet Downloads](https://img.shields.io/nuget/dt/SensitiveString.FluentValidation.svg?style=for-the-badge&logo=nuget)](https://www.nuget.org/packages/SensitiveString.FluentValidation/)

This package extends [SensitiveString](https://www.nuget.org/packages/SensitiveString) to integrate it with [FluentValidation](https://github.com/FluentValidation/FluentValidation) so validation can be performed on request members of that type.

## Example

```c#
using FluentValidation;
using TextPrivacy.SensitiveString.FluentValidation;

...

public class CreateUserRequestValidator : AbstractValidator<CreateUserRequest>
{
    public CreateUserRequestValidator()
    {
        this.RuleForSensitive(x => x.FirstName)
            .NotEmpty();

      	// also valid (performs the same action as the call above)
        RuleFor(x => (string?) x.FirstName)
            .NotEmpty();

        this.RuleForSensitive(x => x.PrimaryEmail)
            .NotEmpty()
            .EmailAddress();

        this.RuleForEachSensitive(x => x.SecondaryEmails)
            .NotEmpty()
            .EmailAddress();

				this.RuleForEachSensitive(x => x.AddressLines)
            .NotEmpty();
    }
}
```