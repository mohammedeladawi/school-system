using System.Linq.Expressions;
using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Application.Interfaces.Bases;
using SchoolProject.Application.Interfaces.IdentityServices;
using SchoolProject.Application.Resources;

namespace SchoolProject.Application.Helpers.Validations;

public static class UserValidationRules
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

    public static IRuleBuilderOptions<TCommand, string> ValidateEmail<TCommand>(
        this IRuleBuilder<TCommand, string> ruleBuilder,
        IStringLocalizer<SharedResource> _localizer,
        IUserManager userManager,
        Func<TCommand, int>? excludeUserId = null)
        where TCommand : class
    {
        return ruleBuilder
            .NotEmpty()
            .WithMessage(_ => _localizer[SharedResourceKeys.EmailRequired])

            .Matches(RegxPatterns.EmailPattern)
            .WithMessage(_ => _localizer[SharedResourceKeys.EmailInvalid])

            .MustAsync(async (command, email, cancellationToken) =>
                !await userManager.DoesEmailExist(email, excludeUserId?.Invoke(command)))
            .WithMessage(_ => _localizer[SharedResourceKeys.EmailAlreadyInUse]);
    }

    public static IRuleBuilderOptions<TCommand, string> ValidateUserName<TCommand>(
        this IRuleBuilder<TCommand, string> ruleBuilder,
        IStringLocalizer<SharedResource> _localizer,
        IUserManager userManager,
        Func<TCommand, int>? excludeUserId = null)
        where TCommand : class
    {
        return ruleBuilder
            .NotEmpty()
            .WithMessage(_ => _localizer[SharedResourceKeys.UserNameRequired])

            .MaximumLength(50)
            .WithMessage(_ => _localizer[SharedResourceKeys.UserNameTooLong])

            .Matches(RegxPatterns.UserNamePattern)
            .WithMessage(_ => _localizer[SharedResourceKeys.UserNameInvalid])

            .MustAsync(async (command, userName, cancellationToken) =>
                !await userManager.DoesUserNameExist(userName, excludeUserId?.Invoke(command)))
            .WithMessage(_ => _localizer[SharedResourceKeys.UserNameAlreadyInUse]);
    }


}
