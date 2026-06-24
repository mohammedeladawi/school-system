using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Features.ApplicationUser.Commands.Models;
using SchoolProject.Core.Features.ApplicationUser.Commands.Validators;
using SchoolProject.Service.Abstracts;
using SchoolProject.Shared.CustomExceptions;
using SchoolProject.Shared.Resources;

namespace SchoolProject.Core.Features.ApplicationUsers.Commands.Validators;

public class EditApplicationUserValidator :
    AbstractValidator<EditApplicationUserCommand>
{
    private readonly IStringLocalizer<SharedResource> _localizer;
    private readonly IApplicationUserService _applicationUserService;

    public EditApplicationUserValidator(
        IStringLocalizer<SharedResource> localizer,
        IApplicationUserService applicationUserService)
    {
        _localizer = localizer;
        _applicationUserService = applicationUserService;

        Include(new CommonApplicationUserValidator(localizer));
        ValidateId();
    }

    private void ValidateId()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage(_localizer[SharedResourceKeys.IdRequired])

            .GreaterThan(0)
            .WithMessage(_localizer[SharedResourceKeys.IdGreaterThanZero])

            .MustAsync(async (id, CancellationToken) =>
                await _applicationUserService.IsApplicationUserIdExist(id))
            .WithMessage(_localizer[SharedResourceKeys.NotExist]);
    }
}