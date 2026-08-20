using AutoMapper;
using Microsoft.Extensions.Localization;
using SchoolProject.Application.Bases;
using SchoolProject.Application.Interfaces.Bases;
using SchoolProject.Application.Interfaces.IdentityServices;
using SchoolProject.Application.Interfaces.Services;
using SchoolProject.Application.Helpers;
using SchoolProject.Application.Resources;
using SchoolProject.Application.Interfaces.ApiServices;
using SchoolProject.Application.Features.Authentication.Commands.RegisterOrUpdate;

namespace SchoolProject.Application.Features.Instructor.Commands.RegisterInstructor;

public class RegisterInstructorHandler :
    BaseRegisterOrUpdateUserHandler<RegisterInstructorCommand, Domain.Entities.Instructor>
{
    #region Constructors
    public RegisterInstructorHandler(
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
    protected override async Task<Domain.Entities.Instructor> CreateOrUpdateUserAsync(RegisterInstructorCommand request)
    {
        var admin = await CreateAsync(request, request.Password, "Instructor");
        return admin;
    }
    #endregion

}

