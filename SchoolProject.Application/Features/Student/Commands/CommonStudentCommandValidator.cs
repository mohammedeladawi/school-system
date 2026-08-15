using System.Linq.Expressions;
using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Application.Interfaces.Repositories;
using SchoolProject.Application.Resources;

namespace SchoolProject.Application.Features.Student.Commands;

public class CommonStudentCommandValidator : AbstractValidator<CommonStudentDto>
{
    public CommonStudentCommandValidator(
        IStringLocalizer<SharedResource> localizer,
        IDepartmentRepository departmentRepository)
    {

        ValidateAddress(localizer);
        ValidatePhone(localizer);
        ValidateDepartmentId(localizer, departmentRepository);
    }

    private void ValidateAddress(IStringLocalizer<SharedResource> localizer)
    {
        RuleFor(x => x.Address)
            .NotEmpty()
            .WithMessage(_ => localizer[SharedResourceKeys.AddressRequired]);
    }

    private void ValidatePhone(IStringLocalizer<SharedResource> localizer)
    {
        RuleFor(x => x.Phone)
            .NotEmpty()
            .WithMessage(_ => localizer[SharedResourceKeys.PhoneRequired]);
    }

    private void ValidateDepartmentId(IStringLocalizer<SharedResource> localizer, IDepartmentRepository departmentRepository)
    {
        RuleFor(x => x.DepartmentId)
            .GreaterThan(0)
            .WithMessage(_ => localizer[SharedResourceKeys.DepartmentIdGreaterThanZero])

            .MustAsync(async (departmentId, cancellationToken) =>
                await departmentRepository.DoesExistByIdAsync(departmentId))
            .WithMessage(localizer[SharedResourceKeys.DepartmentDoesNotExist]);
    }
}
