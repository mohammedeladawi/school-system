using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Application.Resources;

namespace SchoolProject.Application.Helpers.Validations;

public static class InstructorValidationRules
{
    public static IRuleBuilderOptions<TCommand, int?> ValidateDepartmentId<TCommand>(
        this IRuleBuilder<TCommand, int?> ruleBuilder,
        IStringLocalizer<SharedResource> _localizer,
        Func<int, Task<bool>> DoesExistByIdAsync)
        where TCommand : class
    {
        return ruleBuilder
            .Must(id => !id.HasValue || id.Value > 0)
            .WithMessage(_ => _localizer[SharedResourceKeys.DepartmentIdGreaterThanZero])

            .MustAsync(async (id, cancellationToken) =>
            {
                if (!id.HasValue)
                    return true;

                return await DoesExistByIdAsync(id!.Value);
            })
            .WithMessage(_ => _localizer[SharedResourceKeys.DepartmentNotExist]);
    }


    public static IRuleBuilderOptions<TCommand, int?> ValidateSupervisorId<TCommand>(
        this IRuleBuilder<TCommand, int?> ruleBuilder,
        IStringLocalizer<SharedResource> _localizer,
        Func<int, Task<bool>> DoesExistByIdAsync)
        where TCommand : class
    {
        return ruleBuilder
            .Must(id => !id.HasValue || id.Value > 0)
            .WithMessage(_ => _localizer[SharedResourceKeys.SupervisorIdGreaterThanZero])

            .MustAsync(async (id, cancellationToken) =>
            {
                if (!id.HasValue)
                    return true;

                return await DoesExistByIdAsync(id!.Value);
            })
            .WithMessage(_ => _localizer[SharedResourceKeys.SupervisorNotExist]);
    }
}
