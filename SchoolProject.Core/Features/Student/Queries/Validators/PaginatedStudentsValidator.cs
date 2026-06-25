using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Features.Student.Queries.Models;
using SchoolProject.Shared.Resources;

namespace SchoolProject.Core.Features.Student.Query.Validators;

public class PaginatedStudentsValidator : AbstractValidator<GetPaginatedStudentsQuery>
{
    #region Private Fields
    private readonly IStringLocalizer<SharedResource> _localizer;
    #endregion

    #region Constructors
    public PaginatedStudentsValidator(IStringLocalizer<SharedResource> localizer)
    {
        _localizer = localizer;
        ValidatePageNumber();
        ValidatePageSize();
    }
    #endregion

    #region Private Methods
    private void ValidatePageNumber()
    {
        RuleFor(x => x.PageNumber)
            .NotEmpty()
            .WithMessage(_localizer[SharedResourceKeys.PageNumberRequired])

            .GreaterThan(0).
            WithMessage(_localizer[SharedResourceKeys.PageNumberGreaterThanZero]);
    }

    private void ValidatePageSize()
    {
        RuleFor(x => x.PageSize)
            .NotEmpty()
            .WithMessage(_localizer[SharedResourceKeys.PageSizeRequired])

            .GreaterThan(0)
            .WithMessage(_localizer[SharedResourceKeys.PageSizeGreaterThanZero]);
    }
    #endregion
}
