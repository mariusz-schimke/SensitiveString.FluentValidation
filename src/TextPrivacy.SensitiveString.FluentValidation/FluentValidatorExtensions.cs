using System.Linq.Expressions;
using FluentValidation;
using TextPrivacy.SensitiveString.FluentValidation.Adapters;

namespace TextPrivacy.SensitiveString.FluentValidation;

public static class FluentValidatorExtensions
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
    public static IRuleBuilderInitial<TRequest, string?> RuleForSensitiveString<TRequest>(
        this AbstractValidator<TRequest> validator,
        Expression<Func<TRequest, SensitiveString?>> expression)
    {
        var result = new RuleBuilderInitialAdapter<TRequest>(validator.RuleFor(expression));
        return result;
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
    public static IRuleBuilderInitialCollection<TRequest, string?> RuleForEachSensitiveString<TRequest>(
        this AbstractValidator<TRequest> validator,
        Expression<Func<TRequest, IEnumerable<SensitiveString?>?>> expression)
    {
        var result = new RuleBuilderInitialCollectionAdapter<TRequest>(validator.RuleForEach(expression));
        return result;
    }
}