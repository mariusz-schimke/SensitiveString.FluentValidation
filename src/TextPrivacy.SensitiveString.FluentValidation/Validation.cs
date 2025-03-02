using System.Linq.Expressions;
using FluentValidation;
using FluentValidation.Internal;

namespace TextPrivacy.SensitiveString.FluentValidation;

public static class Validation
{
    /// <summary>
    ///     <inheritdoc cref="AbstractValidator{T}.RuleFor{TProperty}" />
    /// </summary>
    /// <param name="validator">
    ///     <inheritdoc cref="AbstractValidator{T}.RuleFor{TProperty}" />
    /// </param>
    /// <param name="expression">
    ///     <inheritdoc cref="AbstractValidator{T}.RuleFor{TProperty}" />
    /// </param>
    /// <typeparam name="TRequest">
    ///     <inheritdoc cref="AbstractValidator{T}.RuleFor{TProperty}" />
    /// </typeparam>
    public static IRuleBuilderInitial<TRequest, string?> RuleForSensitive<TRequest>(
        this AbstractValidator<TRequest> validator,
        Expression<Func<TRequest, SensitiveString?>> expression)
    {
        // type cast the expression to string so x => x.SensitiveStringProperty becomes x => (string) x.SensitiveStringProperty
        var convertedExpression = Expression.Lambda<Func<TRequest, string>>(
            Expression.Convert(expression.Body, typeof(string)),
            expression.Parameters
        );

        return validator.RuleFor(convertedExpression);
    }

    /// <summary>
    ///     <inheritdoc cref="AbstractValidator{T}.RuleForEach{TElement}" />
    /// </summary>
    /// <param name="validator">
    ///     <inheritdoc cref="AbstractValidator{T}.RuleForEach{TElement}" />
    /// </param>
    /// <param name="expression">
    ///     <inheritdoc cref="AbstractValidator{T}.RuleForEach{TElement}" />
    /// </param>
    /// <typeparam name="TRequest">
    ///     <inheritdoc cref="AbstractValidator{T}.RuleForEach{TElement}" />
    /// </typeparam>
    public static IRuleBuilderInitialCollection<TRequest, string?> RuleForEachSensitive<TRequest>(
        this AbstractValidator<TRequest> validator,
        Expression<Func<TRequest, IEnumerable<SensitiveString?>?>> expression)
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
        var selectCallWithItemTypeConversionLambda = Expression.Lambda<Func<TRequest, IEnumerable<string?>?>>(
            selectCallWithItemTypeConversion,
            expression.Parameters);

        return validator.RuleForEach(selectCallWithItemTypeConversionLambda)
            .Configure(x =>
            {
                x.PropertyName = expression.GetMember().Name;
            });
    }
}