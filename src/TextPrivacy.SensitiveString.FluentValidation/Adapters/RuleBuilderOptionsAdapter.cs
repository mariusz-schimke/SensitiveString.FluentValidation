using FluentValidation;
using FluentValidation.Validators;

namespace TextPrivacy.SensitiveString.FluentValidation.Adapters;

internal class RuleBuilderOptionsAdapter<TRequest> : IRuleBuilderOptions<TRequest, string>
{
    private readonly IRuleBuilderOptions<TRequest, SensitiveString> _source;

    public RuleBuilderOptionsAdapter(IRuleBuilderOptions<TRequest, SensitiveString> source)
    {
        _source = source;
    }

    public IRuleBuilderOptions<TRequest, string> SetValidator(IPropertyValidator<TRequest, string> validator)
    {
        var result = _source.SetValidator(new PropertyValidatorAdapter<TRequest>(validator));
        return new RuleBuilderOptionsAdapter<TRequest>(result);
    }

    public IRuleBuilderOptions<TRequest, string> SetAsyncValidator(IAsyncPropertyValidator<TRequest, string> validator)
    {
        var result = _source.SetAsyncValidator(new AsyncPropertyValidatorAdapter<TRequest>(validator));
        return new RuleBuilderOptionsAdapter<TRequest>(result);
    }

    public IRuleBuilderOptions<TRequest, string> SetValidator(IValidator<string> validator, params string[] ruleSets)
    {
        var result = _source.SetValidator(new ValidatorAdapter(validator), ruleSets);
        return new RuleBuilderOptionsAdapter<TRequest>(result);
    }

    public IRuleBuilderOptions<TRequest, string> SetValidator<TValidator>(Func<TRequest, TValidator> validatorProvider, params string[] ruleSets)
        where TValidator : IValidator<string>
    {
        var validatorAdapter = ValidatorAdapter.Convert(validatorProvider);
        var result = _source.SetValidator(validatorAdapter, ruleSets);
        return new RuleBuilderOptionsAdapter<TRequest>(result);
    }

    public IRuleBuilderOptions<TRequest, string> SetValidator<TValidator>(Func<TRequest, string, TValidator> validatorProvider, params string[] ruleSets)
        where TValidator : IValidator<string>
    {
        var validatorAdapter = ValidatorAdapter.Convert(validatorProvider);
        var result = _source.SetValidator(validatorAdapter, ruleSets);
        return new RuleBuilderOptionsAdapter<TRequest>(result);
    }

    public IRuleBuilderOptions<TRequest, string> DependentRules(Action action)
    {
        var result = _source.DependentRules(action);
        return new RuleBuilderOptionsAdapter<TRequest>(result);
    }
}