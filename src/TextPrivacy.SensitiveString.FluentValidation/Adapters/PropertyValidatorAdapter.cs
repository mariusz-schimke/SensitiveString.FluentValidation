using FluentValidation;
using FluentValidation.Validators;

namespace TextPrivacy.SensitiveString.FluentValidation.Adapters;

internal abstract class PropertyValidatorAdapter(IPropertyValidator validator) : IPropertyValidator
{
    public string GetDefaultMessageTemplate(string errorCode) => validator.GetDefaultMessageTemplate(errorCode);

    public string Name => validator.Name;
}

internal class PropertyValidatorAdapter<TRequest> : PropertyValidatorAdapter, IPropertyValidator<TRequest, SensitiveString>
{
    private readonly IPropertyValidator<TRequest, string> _validator;

    public PropertyValidatorAdapter(IPropertyValidator<TRequest, string> validator)
        : base(validator)
    {
        _validator = validator;
    }

    public bool IsValid(ValidationContext<TRequest> context, SensitiveString value) => _validator.IsValid(context, value.Reveal());
}