using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Features.ApplicationUser.Queries.Models;
using SchoolProject.Shared.Resources;

namespace SchoolProject.Core.Features.SApplicationUsers.Query.Validators;

public class PaginatedSApplicationUsersValidator : AbstractValidator<GetPaginatedUsersQuery>
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
            .WithMessage(_ => _localizer[SharedResourceKeys.PageNumberRequired])

            .GreaterThan(0).
            WithMessage(_ => _localizer[SharedResourceKeys.PageNumberGreaterThanZero]);
    }

    private void ValidatePageSize()
    {
        RuleFor(x => x.PageSize)
            .NotEmpty()
            .WithMessage(_ => _localizer[SharedResourceKeys.PageSizeRequired])

            .GreaterThan(0)
            .WithMessage(_ => _localizer[SharedResourceKeys.PageSizeGreaterThanZero]);
    }
}
