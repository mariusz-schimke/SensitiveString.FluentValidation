using FluentValidation;
using FluentValidation.Validators;

namespace TextPrivacy.SensitiveString.FluentValidation.Adapters;

internal class AsyncPropertyValidatorAdapter<TRequest>(IAsyncPropertyValidator<TRequest, string?> validator)
    : PropertyValidatorAdapter(validator), IAsyncPropertyValidator<TRequest, SensitiveString?>
{
    public async Task<bool> IsValidAsync(ValidationContext<TRequest> context, SensitiveString? value, CancellationToken cancellationToken) =>
        await validator.IsValidAsync(context, value?.Reveal(), cancellationToken);
}