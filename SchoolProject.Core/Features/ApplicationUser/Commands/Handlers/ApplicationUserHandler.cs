using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.ApplicationUser.Commands.Models;
using SchoolProject.Data.Entities;
using SchoolProject.Data.Entities.Identities;
using SchoolProject.Service.Abstracts;
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
        private readonly IApplicationUserService _applicationUserService;
        #endregion

        #region Constructors
        public ApplicationUserCommandHandler(
            IStringLocalizer<SharedResource> localizer,
            IMapper mapper,
            IApplicationUserService applicationUserService)
            : base(localizer, mapper)
        {
            _applicationUserService = applicationUserService;
        }
        #endregion

        #region Public Methods

        public async Task<Response<string>> Handle(EditUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _applicationUserService.GetByIdAsync(request.Id);
            _mapper.Map(request, user);
            await _applicationUserService.UpdateAsync(user);
            return Success<string>(_localizer[SharedResourceKeys.UpdatedSuccessfully]);
        }

        public async Task<Response<string>> Handle(DeleteCommand request, CancellationToken cancellationToken)
        {
            var user = await _applicationUserService.GetByIdAsync(request.Id);
            if (user == null)
                return NotFound<string>();

            await _applicationUserService.DeleteAsync(user);

            return Deleted<string>();
        }

        public async Task<Response<string>> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            await _applicationUserService.ChangePasswordAsync(request.Id, request.CurrentPassword, request.NewPassword);
            return Success<string>(_localizer[SharedResourceKeys.UpdatedSuccessfully]);
        }


        #endregion
    }
}
