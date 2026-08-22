using AutoMapper;
using Microsoft.Extensions.Localization;
using SchoolProject.Application.Features.Base.Users.Commands.Handlers;
using SchoolProject.Application.Interfaces.ApiServices;
using SchoolProject.Application.Interfaces.Bases;
using SchoolProject.Application.Interfaces.IdentityServices;
using SchoolProject.Application.Interfaces.Services;
using SchoolProject.Application.Resources;

namespace SchoolProject.Application.Features.Student.Commands.EditStudent;

public class EditStudentHandler :
    BaseRegisterOrUpdateUserHandler<EditStudentCommand, Domain.Entities.Student>
{
    #region Constructors
    public EditStudentHandler(
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
    protected override async Task<Domain.Entities.Student> CreateOrUpdateUserAsync(EditStudentCommand request)
    {
        var student = await UpdateAsync(request.Id, request, "Student");
        return student;
    }
    #endregion
}