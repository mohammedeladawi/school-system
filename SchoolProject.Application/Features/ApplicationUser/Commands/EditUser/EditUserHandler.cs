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

    // Update
    // Id, request, role

    #region Protected Methods
    protected override async Task<Domain.Entities.Identities.ApplicationUser> CreateOrUpdateUserAsync(EditUserCommand request)
    {
        var admin = await UpdateAsync(request.Id, request, "Admin");
        return admin;
    }
}
    #endregion