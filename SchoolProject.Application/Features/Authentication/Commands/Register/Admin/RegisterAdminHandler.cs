using AutoMapper;
using Microsoft.Extensions.Localization;
using SchoolProject.Application.Bases;
using SchoolProject.Application.Interfaces.Bases;
using SchoolProject.Application.Interfaces.IdentityServices;
using SchoolProject.Application.Interfaces.Services;
using SchoolProject.Application.Helpers;
using SchoolProject.Application.Resources;
using SchoolProject.Application.Interfaces.ApiServices;

namespace SchoolProject.Application.Features.Authentication.Commands.Register.Admin
{
    public class RegisterAdminHandler :
        BaseRegisterUserHandler<RegisterAdminCommand, Domain.Entities.Identities.ApplicationUser>
    {
        #region Constructors
        public RegisterAdminHandler(
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
        protected override async Task<Domain.Entities.Identities.ApplicationUser> AddUser(RegisterAdminCommand request)
        {
            var admin = _mapper.Map<Domain.Entities.Identities.ApplicationUser>(request);
            if (request.Image is not null)
            {
                string webRootPath = _locationService.GetWebRootPath();
                string relativeFolderPath = "Images/Admins";
                admin.ImagePath = await _fileService.UploadFileAsync(request.Image, webRootPath, relativeFolderPath);
            }

            await _userManager.AddAsync(admin, request.Password, "Admin");
            return admin;
        }

        #endregion

    }
}
