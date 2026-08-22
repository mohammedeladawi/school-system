using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Application.Resources;

namespace SchoolProject.Application.Helpers.Validations;

public static class StudentValidationRules
{
    public static IRuleBuilderOptions<TCommand, int> ValidateDepartmentId<TCommand>(
    this IRuleBuilder<TCommand, int> ruleBuilder,
    IStringLocalizer<SharedResource> _localizer,
    Func<int, Task<bool>> DoesExistByIdAsync)
    where TCommand : class
    {
        return ruleBuilder
            .GreaterThan(0)
            .WithMessage(_ => _localizer[SharedResourceKeys.DepartmentIdGreaterThanZero])

            .MustAsync(async (id, cancellationToken) =>
                await DoesExistByIdAsync(id))
            .WithMessage(_ => _localizer[SharedResourceKeys.DepartmentNotExist]);
    }
}