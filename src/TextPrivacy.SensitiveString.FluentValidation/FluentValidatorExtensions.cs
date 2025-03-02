using System.Linq.Expressions;
using FluentValidation;
using TextPrivacy.SensitiveString.FluentValidation.Adapters;

namespace TextPrivacy.SensitiveString.FluentValidation;

public static class FluentValidatorExtensions
{
    public static IRuleBuilderInitial<TRequest, string> RuleForSensitiveString<TRequest>(
        this AbstractValidator<TRequest> validator,
        Expression<Func<TRequest, SensitiveString?>> expression)
    {
        // converts the expression to cast SensitiveString to string
        var convertedExpression = Expression.Lambda<Func<TRequest, string>>(
            Expression.Convert(expression.Body, typeof(string)),
            expression.Parameters
        );

        return validator.RuleFor(convertedExpression);
    }

    public static IRuleBuilderInitialCollection<TRequest, string> RuleForEachSensitiveString<TRequest>(
        this AbstractValidator<TRequest> validator,
        Expression<Func<TRequest, IEnumerable<SensitiveString>>> expression)
    {
        var result = new SensitiveStringRuleAdapter<TRequest>(validator.RuleForEach(expression));
        return result;
    }
}