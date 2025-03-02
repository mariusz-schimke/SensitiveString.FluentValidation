using System.Linq.Expressions;
using FluentValidation;
using FluentValidation.Internal;

namespace TextPrivacy.SensitiveString.FluentValidation;

public static class Validation
{
    /// <inheritdoc cref="AbstractValidator{T}.RuleFor{TProperty}" />
    public static IRuleBuilderInitial<TRequest, string> RuleForSensitive<TRequest, TProperty>(
        this AbstractValidator<TRequest> validator,
        Expression<Func<TRequest, TProperty>> expression)
        where TProperty : SensitiveString?
    {
        // type cast the expression to string so x => x.SensitiveStringProperty becomes x => (string) x.SensitiveStringProperty
        // (for this to work, the actual type, EVEN A DESCENDANT, has to have an implicit or explicit conversion to string implemented)
        var convertedExpression = Expression.Lambda<Func<TRequest, string>>(
            Expression.Convert(expression.Body, typeof(string)),
            expression.Parameters
        );

        return validator.RuleFor(convertedExpression);
    }

    /// <inheritdoc cref="AbstractValidator{T}.RuleForEach{TElement}" />
    public static IRuleBuilderInitialCollection<TRequest, string> RuleForEachSensitive<TRequest, TProperty>(
        this AbstractValidator<TRequest> validator,
        Expression<Func<TRequest, IEnumerable<TProperty>?>> expression)
        where TProperty : SensitiveString?
    {
        // x.SensitiveStringCollectionProperty
        var collectionExpression = expression.Body;

        // item => Convert(item, String)
        var parameter = Expression.Parameter(typeof(SensitiveString), "item");
        var itemTypeConversionExpression = Expression.Convert(parameter, typeof(string));
        var itemTypeConversionLambda = Expression.Lambda<Func<SensitiveString, string>>(itemTypeConversionExpression, parameter);

        // x.SensitiveStringCollectionProperty.Select(item => Convert(item, String))
        var selectCallWithItemTypeConversion = Expression.Call(
            typeof(Enumerable),
            nameof(Enumerable.Select),
            [typeof(SensitiveString), typeof(string)],
            collectionExpression,
            itemTypeConversionLambda);

        // x => x.SensitiveStringCollectionProperty.Select(item => Convert(item, String))
        var selectCallWithItemTypeConversionLambda = Expression.Lambda<Func<TRequest, IEnumerable<string>?>>(
            selectCallWithItemTypeConversion,
            expression.Parameters);

        return validator.RuleForEach(selectCallWithItemTypeConversionLambda)
            .Configure(x =>
            {
                x.PropertyName = expression.GetMember().Name;
            });
    }
}