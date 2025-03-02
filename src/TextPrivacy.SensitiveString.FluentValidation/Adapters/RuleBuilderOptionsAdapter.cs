using FluentValidation;
using FluentValidation.Validators;

namespace TextPrivacy.SensitiveString.FluentValidation.Adapters;

internal class RuleBuilderOptionsAdapter<TRequest> : IRuleBuilderOptions<TRequest, string>
{
    private readonly IRuleBuilderOptions<TRequest, SensitiveString> _ruleBuilderOptions;

    public RuleBuilderOptionsAdapter(IRuleBuilderOptions<TRequest, SensitiveString> ruleBuilderOptions)
    {
        _ruleBuilderOptions = ruleBuilderOptions;
    }

    public IRuleBuilderOptions<TRequest, string> SetValidator(IPropertyValidator<TRequest, string> validator)
    {
        var result = _ruleBuilderOptions.SetValidator(new PropertyValidatorAdapter<TRequest>(validator));
        return new RuleBuilderOptionsAdapter<TRequest>(result);
    }

    public IRuleBuilderOptions<TRequest, string> SetAsyncValidator(IAsyncPropertyValidator<TRequest, string> validator)
    {
        var result = _ruleBuilderOptions.SetAsyncValidator(new AsyncPropertyValidatorAdapter<TRequest>(validator));
        return new RuleBuilderOptionsAdapter<TRequest>(result);
    }

    public IRuleBuilderOptions<TRequest, string> SetValidator(IValidator<string> validator, params string[] ruleSets)
    {
        var result = _ruleBuilderOptions.SetValidator(new ValidatorAdapter(validator), ruleSets);
        return new RuleBuilderOptionsAdapter<TRequest>(result);
    }

    public IRuleBuilderOptions<TRequest, string> SetValidator<TValidator>(Func<TRequest, TValidator> validatorProvider, params string[] ruleSets)
        where TValidator : IValidator<string> =>
        throw new NotImplementedException();

    public IRuleBuilderOptions<TRequest, string> SetValidator<TValidator>(Func<TRequest, string, TValidator> validatorProvider, params string[] ruleSets)
        where TValidator : IValidator<string> =>
        throw new NotImplementedException();

    public IRuleBuilderOptions<TRequest, string> DependentRules(Action action) =>
        throw new NotImplementedException();
}