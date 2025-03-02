using FluentValidation;
using FluentValidation.Validators;

namespace TextPrivacy.SensitiveString.FluentValidation.Adapters;

internal class PropertyValidatorAdapter<TRequest> : IPropertyValidator<TRequest, SensitiveString>
{
    private readonly IPropertyValidator<TRequest, string> _validator;

    public PropertyValidatorAdapter(IPropertyValidator<TRequest, string> validator)
    {
        _validator = validator;
    }

    public bool IsValid(ValidationContext<TRequest> context, SensitiveString value) => _validator.IsValid(context, value.Reveal());

    public string GetDefaultMessageTemplate(string errorCode) => _validator.GetDefaultMessageTemplate(errorCode);

    public string Name => _validator.Name;
}