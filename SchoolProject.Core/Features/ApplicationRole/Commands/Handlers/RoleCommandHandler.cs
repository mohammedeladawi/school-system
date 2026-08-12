using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.ApplicationRole.Commands.Models;
using SchoolProject.Core.Interfaces.IdentityServices;
using SchoolProject.Shared.Resources;

namespace SchoolProject.Core.Features.ApplicationRole.Commands.Handlers
{
    public class ApplicationRoleCommandHandler :
        ResponseHandler,
        IRequestHandler<AddRoleCommand, Response<string>>,
        IRequestHandler<EditRoleCommand, Response<string>>,
        IRequestHandler<DeleteRoleCommand, Response<string>>
    {
        #region Private Fields
        private readonly IRoleManager _RoleManager;
        #endregion

        #region Constructors
        public ApplicationRoleCommandHandler(
            IStringLocalizer<SharedResource> localizer,
            IMapper mapper,
            IRoleManager ApplicationRoleService)
            : base(localizer, mapper)
        {
            _RoleManager = ApplicationRoleService;
        }
        #endregion

        #region Public Methods
        public async Task<Response<string>> Handle(AddRoleCommand request, CancellationToken cancellationToken)
        {
            await _RoleManager.CreateAsync(request.RoleName);
            return Created<string>(_localizer[SharedResourceKeys.AddedSuccessfully]);
        }

        public async Task<Response<string>> Handle(EditRoleCommand request, CancellationToken cancellationToken)
        {
            var role = await _RoleManager.GetByIdAsync(request.Id);
            if (role is null)
                return NotFound<string>(_localizer[SharedResourceKeys.NotFound]);

            role.Name = request.NewName;
            await _RoleManager.EditAsync(role);
            return Success<string>(_localizer[SharedResourceKeys.UpdatedSuccessfully]);
        }

        public async Task<Response<string>> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
        {
            var role = await _RoleManager.GetByIdAsync(request.Id);
            if (role is null)
                return NotFound<string>(_localizer[SharedResourceKeys.NotFound]);

            await _RoleManager.DeleteAsync(role);
            return Deleted<string>();
        }
        #endregion
    }
}
