using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Features.Students.Queries.Models;
using SchoolProject.Core.Resources;

namespace SchoolProject.Core.Features.Students.Query.Validators;

public class PaginatedStudentsValidator : AbstractValidator<GetPaginatedStudentsQuery>
{
    private readonly IStringLocalizer<SharedResource> _localizer;

    public PaginatedStudentsValidator(IStringLocalizer<SharedResource> localizer)
    {
        _localizer = localizer;
        ValidatePageNumber();
        ValidatePageSize();
    }

    private void ValidatePageNumber()
    {
        RuleFor(x => x.PageNumber)
            .NotEmpty().WithMessage(_localizer[SharedResourceKeys.PageNumberRequired])
            .GreaterThan(0).WithMessage(_localizer[SharedResourceKeys.PageNumberGreaterThanZero]);
    }

    private void ValidatePageSize()
    {
        RuleFor(x => x.PageSize)
            .NotEmpty().WithMessage(_localizer[SharedResourceKeys.PageSizeRequired])
            .GreaterThan(0).WithMessage(_localizer[SharedResourceKeys.PageSizeGreaterThanZero]);
    }
}
