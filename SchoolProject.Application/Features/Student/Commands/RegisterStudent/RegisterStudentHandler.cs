using AutoMapper;
using Microsoft.Extensions.Localization;
using SchoolProject.Application.Interfaces.Bases;
using SchoolProject.Application.Interfaces.IdentityServices;
using SchoolProject.Application.Interfaces.Services;
using SchoolProject.Application.Resources;
using SchoolProject.Application.Interfaces.ApiServices;
using SchoolProject.Application.Features.Base.Users.Commands.Handlers;

namespace SchoolProject.Application.Features.Student.Commands.RegisterStudent;

public class RegisterStudentHandler :
    BaseRegisterOrUpdateUserHandler<RegisterStudentCommand, Domain.Entities.Student>
{
    #region Constructors
    public RegisterStudentHandler(
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
    protected override async Task<Domain.Entities.Student> CreateOrUpdateUserAsync(RegisterStudentCommand request)
    {
        var student = await CreateAsync(request, request.Password, "Student");
        return student;
    }
    #endregion
}

