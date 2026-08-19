using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProject.Application.Bases;
using SchoolProject.Application.Features.Authentication.Commands.RegisterOrUpdate;
using SchoolProject.Application.Interfaces.ApiServices;
using SchoolProject.Application.Interfaces.Bases;
using SchoolProject.Application.Interfaces.IdentityServices;
using SchoolProject.Application.Interfaces.Services;
using SchoolProject.Application.Resources;

namespace SchoolProject.Application.Features.ApplicationUser.Commands.EditUser;

// public class EditUserHandler : ResponseHandler, IRequestHandler<EditUserCommand, Response<string>>
// {
//     #region Private Fields
//     private readonly IUserManager _userManager;
//     private readonly IFileService _fileService;
//     #endregion

//     #region Constructors
//     public EditUserHandler(
//         IStringLocalizer<SharedResource> localizer,
//         IMapper mapper,
//         IUserManager userManager,
//         IFileService fileService)
//         : base(localizer, mapper)
//     {
//         _userManager = userManager;
//         _fileService = fileService;
//     }


//     #endregion

//     #region Public Methods
//     public async Task<Response<string>> Handle(EditUserCommand request, CancellationToken cancellationToken)
//     {

//     }


//     #endregion
// }

public class EditUserHandler :
       BaseRegisterOrUpdateUserHandler<EditUserCommand, Domain.Entities.Identities.ApplicationUser>
{
    #region Constructors
    public EditUserHandler(
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
    protected override async Task<Domain.Entities.Identities.ApplicationUser> CreateOrUpdateUserAsync(EditUserCommand request)
    {
        var user = await _userManager.GetByIdAsync(request.Id);

        // Remove old image if new image is uploaded
        if (request.Image is not null && user?.ImagePath is not null)
        {
            RemoveOldImage(request, user);

            string webRootPath = _locationService.GetWebRootPath();
            string relativeFolderPath = "Images/Users";
            user.ImagePath = await _fileService.UploadFileAsync(request.Image, webRootPath, relativeFolderPath);
        }

        // stop confirmaition the email chaged
        _mapper.Map(request, user);
        if (request.Email != user?.Email)
        {
            user!.EmailConfirmed = false;
        }

        await _userManager.UpdateAsync(user!);

        return user!;
    }
}
    #endregion