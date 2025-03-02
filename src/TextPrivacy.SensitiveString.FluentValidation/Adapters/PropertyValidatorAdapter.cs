using FluentValidation;
using FluentValidation.Validators;

namespace TextPrivacy.SensitiveString.FluentValidation.Adapters;

internal abstract class PropertyValidatorAdapter(IPropertyValidator validator) : IPropertyValidator
{
    public string GetDefaultMessageTemplate(string errorCode) => validator.GetDefaultMessageTemplate(errorCode);

    public string Name => validator.Name;
}

internal class PropertyValidatorAdapter<TRequest>(IPropertyValidator<TRequest, string> validator) : PropertyValidatorAdapter(validator),
    IPropertyValidator<TRequest, SensitiveString>
{
    public bool IsValid(ValidationContext<TRequest> context, SensitiveString value) => validator.IsValid(context, value.Reveal());
}