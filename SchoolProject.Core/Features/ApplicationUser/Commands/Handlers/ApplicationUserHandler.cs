using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.ApplicationUser.Commands.Models;
using SchoolProject.Core.Interfaces.IdentityServices;
using SchoolProject.Data.Entities;
using SchoolProject.Data.Entities.Identities;
using SchoolProject.Shared.Helpers;
using SchoolProject.Shared.Resources;

namespace SchoolProject.Core.Features.ApplicationUser.Commands.Handlers
{
    public class ApplicationUserCommandHandler :
        ResponseHandler,
        IRequestHandler<EditUserCommand, Response<string>>,
        IRequestHandler<DeleteCommand, Response<string>>,
        IRequestHandler<ChangePasswordCommand, Response<string>>
    {
        #region Private Fields
        private readonly IUserManager _userManager;
        #endregion

        #region Constructors
        public ApplicationUserCommandHandler(
            IStringLocalizer<SharedResource> localizer,
            IMapper mapper,
            IUserManager ApplicationUserRepositories)
            : base(localizer, mapper)
        {
            _userManager = ApplicationUserRepositories;
        }
        #endregion

        #region Public Methods

        public async Task<Response<string>> Handle(EditUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.GetByIdAsync(request.Id);
            if (user is null)
                return BadRequest<string>(_localizer[SharedResourceKeys.NotFound]);

            _mapper.Map(request, user);
            await _userManager.UpdateAsync(user);
            return Success<string>(_localizer[SharedResourceKeys.UpdatedSuccessfully]);
        }

        public async Task<Response<string>> Handle(DeleteCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.GetByIdAsync(request.Id);
            if (user is null)
                return BadRequest<string>(_localizer[SharedResourceKeys.NotFound]);

            await _userManager.DeleteAsync(user);

            return Deleted<string>();
        }

        public async Task<Response<string>> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            await _userManager.ChangePasswordAsync(request.Id, request.CurrentPassword, request.NewPassword);
            return Success<string>(_localizer[SharedResourceKeys.UpdatedSuccessfully]);
        }


        #endregion
    }
}
