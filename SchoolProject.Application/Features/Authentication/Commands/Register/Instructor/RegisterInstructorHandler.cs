using AutoMapper;
using Microsoft.Extensions.Localization;
using SchoolProject.Application.Bases;
using SchoolProject.Application.Interfaces.Bases;
using SchoolProject.Application.Interfaces.IdentityServices;
using SchoolProject.Application.Interfaces.Services;
using SchoolProject.Application.Helpers;
using SchoolProject.Application.Resources;
using SchoolProject.Application.Interfaces.ApiServices;

namespace SchoolProject.Application.Features.Authentication.Commands.Register
{
    public class RegisterInstructorHandler :
        BaseRegisterUserHandler<RegisterInstructorCommand, Domain.Entities.Instructor>
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
        protected override async Task<Domain.Entities.Instructor> AddUser(RegisterInstructorCommand request)
        {
            var instructor = _mapper.Map<Domain.Entities.Instructor>(request);
            if (request.Image is not null)
            {
                string webRootPath = _locationService.GetWebRootPath();
                string relativeFolderPath = "Images/Instructors";
                instructor.ImagePath = await _fileService.UploadFileAsync(request.Image, webRootPath, relativeFolderPath);
            }
            await _userManager.AddAsync(instructor, request.Password, "Instructor");
            return instructor;
        }
        #endregion

    }
}

