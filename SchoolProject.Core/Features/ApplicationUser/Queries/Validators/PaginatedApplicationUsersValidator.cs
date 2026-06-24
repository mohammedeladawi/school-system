using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Features.ApplicationUser.Queries.Models;
using SchoolProject.Shared.Resources;

namespace SchoolProject.Core.Features.SApplicationUsers.Query.Validators;

public class PaginatedSApplicationUsersValidator : AbstractValidator<GetPaginatedApplicationUsersQuery>
{
    private readonly IStringLocalizer<SharedResource> _localizer;

    public PaginatedSApplicationUsersValidator(IStringLocalizer<SharedResource> localizer)
    {
        _localizer = localizer;
        ValidatePageNumber();
        ValidatePageSize();
    }

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
}
