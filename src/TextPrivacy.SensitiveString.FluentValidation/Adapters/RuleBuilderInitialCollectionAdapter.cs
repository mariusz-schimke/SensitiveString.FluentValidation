using FluentValidation;
using FluentValidation.Validators;

namespace TextPrivacy.SensitiveString.FluentValidation.Adapters;

internal class RuleBuilderInitialCollectionAdapter<TRequest> : IRuleBuilderInitialCollection<TRequest, string>
{
    private readonly IRuleBuilderInitialCollection<TRequest, SensitiveString> _source;

    public RuleBuilderInitialCollectionAdapter(IRuleBuilderInitialCollection<TRequest, SensitiveString> source)
    {
        _source = source;
    }

    public IRuleBuilderOptions<TRequest, string> SetValidator(IPropertyValidator<TRequest, string> validator)
    {
        var validatorAdapter = new PropertyValidatorAdapter<TRequest>(validator);
        var result = _source.SetValidator(validatorAdapter);
        return new RuleBuilderOptionsAdapter<TRequest>(result);
    }

    public IRuleBuilderOptions<TRequest, string> SetAsyncValidator(IAsyncPropertyValidator<TRequest, string> validator)
    {
        var validatorAdapter = new AsyncPropertyValidatorAdapter<TRequest>(validator);
        var result = _source.SetAsyncValidator(validatorAdapter);
        return new RuleBuilderOptionsAdapter<TRequest>(result);
    }

    public IRuleBuilderOptions<TRequest, string> SetValidator(IValidator<string> validator, params string[] ruleSets)
    {
        var validatorAdapter = new ValidatorAdapter(validator);
        var result = _source.SetValidator(validatorAdapter, ruleSets);
        return new RuleBuilderOptionsAdapter<TRequest>(result);
    }

    public IRuleBuilderOptions<TRequest, string> SetValidator<TValidator>(
        Func<TRequest, TValidator> validatorProvider, params string[] ruleSets)
        where TValidator : IValidator<string>
    {
        var validatorAdapter = ValidatorAdapter.Convert(validatorProvider);
        var result = _source.SetValidator(validatorAdapter, ruleSets);
        return new RuleBuilderOptionsAdapter<TRequest>(result);
    }

    public IRuleBuilderOptions<TRequest, string> SetValidator<TValidator>(
        Func<TRequest, string, TValidator> validatorProvider, params string[] ruleSets)
        where TValidator : IValidator<string>
    {
        var validatorAdapter = ValidatorAdapter.Convert(validatorProvider);
        var result = _source.SetValidator(validatorAdapter, ruleSets);
        return new RuleBuilderOptionsAdapter<TRequest>(result);
    }
}