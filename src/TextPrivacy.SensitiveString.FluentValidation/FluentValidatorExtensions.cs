using System.Linq.Expressions;
using FluentValidation;
using TextPrivacy.SensitiveString.FluentValidation.Adapters;

namespace TextPrivacy.SensitiveString.FluentValidation;

public static class FluentValidatorExtensions
{
    public static IRuleBuilderInitial<TRequest, string> RuleForSensitiveString<TRequest>(
        this AbstractValidator<TRequest> validator,
        Expression<Func<TRequest, SensitiveString>> expression)
    {
        var result = new RuleBuilderInitialAdapter<TRequest>(validator.RuleFor(expression));
        return result;
    }

    public static IRuleBuilderInitialCollection<TRequest, string> RuleForEachSensitiveString<TRequest>(
        this AbstractValidator<TRequest> validator,
        Expression<Func<TRequest, IEnumerable<SensitiveString>>> expression)
    {
        var result = new RuleBuilderInitialCollectionAdapter<TRequest>(validator.RuleForEach(expression));
        return result;
    }
}