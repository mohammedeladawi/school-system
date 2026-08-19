using System.Linq.Expressions;
using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Application.Interfaces.Bases;
using SchoolProject.Application.Interfaces.IdentityServices;
using SchoolProject.Application.Resources;

namespace SchoolProject.Application.Helpers;

public static class ValidationRules
{
    public static IRuleBuilderOptions<TCommand, int> ValidateUserId<TCommand>(
        this IRuleBuilder<TCommand, int> ruleBuilder,
        IStringLocalizer<SharedResource> _localizer,
        Func<int, Task<bool>> DoesExistByIdAsync)
        where TCommand : class
    {

        return ruleBuilder
            .NotEmpty()
            .WithMessage(_ => _localizer[SharedResourceKeys.IdRequired])

            .GreaterThan(0)
            .WithMessage(_ => _localizer[SharedResourceKeys.IdGreaterThanZero])

            .MustAsync(async (id, CancellationToken) =>
                await DoesExistByIdAsync(id))
            .WithMessage(_ => _localizer[SharedResourceKeys.NotFound]);
    }

    public static IRuleBuilderOptions<TCommand, string> ValidatePassword<TCommand>(
        this IRuleBuilder<TCommand, string> ruleBuilder,
        IStringLocalizer<SharedResource> _localizer)
        where TCommand : class
    {

        return ruleBuilder
            .NotEmpty()
            .WithMessage(_ => _localizer[SharedResourceKeys.PasswordRequired])

            .MinimumLength(6)
            .WithMessage(_ => _localizer[SharedResourceKeys.PasswordMinimumLength])

            .Matches("[A-Z]")
            .WithMessage(_ => _localizer[SharedResourceKeys.PasswordRequireUppercase])

            .Matches("[a-z]")
            .WithMessage(_ => _localizer[SharedResourceKeys.PasswordRequireLowercase])

            .Matches("\\d")
            .WithMessage(_ => _localizer[SharedResourceKeys.PasswordRequireDigit])

            .Matches("[^\\w\\s]")
            .WithMessage(_ => _localizer[SharedResourceKeys.PasswordRequireNonAlphanumeric]);
    }

    public static IRuleBuilderOptions<TCommand, string> ValidateConfirmPassword<TCommand>(
        this IRuleBuilder<TCommand, string> ruleBuilder,
        Expression<Func<TCommand, string>> passwordSelector,
        IStringLocalizer<SharedResource> _localizer)
        where TCommand : class
    {

        return ruleBuilder
                .NotEmpty()
                .WithMessage(_ => _localizer[SharedResourceKeys.ConfirmPasswordRequired])

                .Equal(passwordSelector)
                .WithMessage(_ => _localizer[SharedResourceKeys.PasswordsDoNotMatch]);
    }


}