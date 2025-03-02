using FluentValidation;
using FluentValidation.Results;

namespace TextPrivacy.SensitiveString.FluentValidation.Adapters;

internal class ValidatorAdapter(IValidator<string> validator) : IValidator<SensitiveString>
{
    public ValidationResult Validate(IValidationContext context) => validator.Validate(context);

    public Task<ValidationResult> ValidateAsync(IValidationContext context, CancellationToken cancellationToken) =>
        validator.ValidateAsync(context, cancellationToken);

    public IValidatorDescriptor CreateDescriptor() => validator.CreateDescriptor();

    // todo: check this
    public bool CanValidateInstancesOfType(Type type) => type.IsAssignableTo(typeof(SensitiveString));

    public ValidationResult Validate(SensitiveString instance) => validator.Validate(instance.Reveal());

    public async Task<ValidationResult> ValidateAsync(SensitiveString instance, CancellationToken cancellationToken) =>
        await validator.ValidateAsync(instance.Reveal(), cancellationToken);

    public static Func<TRequest, IValidator<SensitiveString>> Convert<TRequest, TValidator>(Func<TRequest, TValidator> validatorProvider)
        where TValidator : IValidator<string> =>
        request => new ValidatorAdapter(validatorProvider(request));

    public static Func<TRequest, SensitiveString, IValidator<SensitiveString>> Convert<TRequest, TValidator>(Func<TRequest, string, TValidator> validatorProvider)
        where TValidator : IValidator<string> =>
        (request, value) => new ValidatorAdapter(validatorProvider(request, value.Reveal()));
}