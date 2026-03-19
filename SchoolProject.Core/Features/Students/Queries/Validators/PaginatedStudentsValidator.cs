using FluentValidation;
using SchoolProject.Core.Features.Students.Commands.Models;
using SchoolProject.Core.Features.Students.Queries.Models;

namespace SchoolProject.Core.Features.Students.Query.Validators;

public class PaginatedStudentsValidator : AbstractValidator<GetPaginatedStudentsQuery>
{
    public PaginatedStudentsValidator()
    {
        ValidatePageNumber();
        ValidatePageSize();
    }
    private void ValidatePageNumber()
    {
        RuleFor(x => x.PageNumber)
            .NotEmpty().WithMessage("PageNumber is required")
            .GreaterThan(0).WithMessage("PageNumber must be greater than 0.");
    }

    private void ValidatePageSize()
    {
        RuleFor(x => x.PageSize)
            .NotEmpty().WithMessage("PageSize is required")
            .GreaterThan(0).WithMessage("PageSize must be greater than 0.");
    }

}