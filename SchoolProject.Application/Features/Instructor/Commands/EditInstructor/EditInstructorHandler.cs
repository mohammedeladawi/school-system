using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProject.Application.Bases;
using SchoolProject.Application.Features.Base.Users.Commands.Handlers;
using SchoolProject.Application.Interfaces.ApiServices;
using SchoolProject.Application.Interfaces.Bases;
using SchoolProject.Application.Interfaces.IdentityServices;
using SchoolProject.Application.Interfaces.Services;
using SchoolProject.Application.Resources;

namespace SchoolProject.Application.Features.ApplicationUser.Commands.EditInstructor;

public class EditInstructorHandler :
       BaseRegisterOrUpdateUserHandler<EditInstructorCommand, Domain.Entities.Instructor>
{
    #region Constructors
    public EditInstructorHandler(
        IUserManager userManager,
        IMapper mapper,
        IStringLocalizer<SharedResource> localizer,
        IUrlService urlService,
        IEmailService emailService,
        IUnitOfWork unitOfWork,
        IFileService fileService,
        ILocationService locationService)
        : base(userManager, mapper, localizer, urlService, emailService, unitOfWork, fileService, locationService)
    {
    }
    #endregion


    #region Protected Methods
    protected override async Task<Domain.Entities.Instructor> CreateOrUpdateUserAsync(EditInstructorCommand request)
    {
        var instructor = await UpdateAsync(request.Id, request, "Instructor");
        return instructor;
    }
}
    #endregion