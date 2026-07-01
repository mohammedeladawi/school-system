using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.ApplicationRole.Commands.Models;
using SchoolProject.Data.Entities.Identities;
using SchoolProject.Service.Abstracts;
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
        private readonly IApplicationRoleService _applicationRoleService;
        #endregion

        #region Constructors
        public ApplicationRoleCommandHandler(
            IStringLocalizer<SharedResource> localizer,
            IMapper mapper,
            IApplicationRoleService ApplicationRoleService)
            : base(localizer, mapper)
        {
            _applicationRoleService = ApplicationRoleService;
        }
        #endregion

        #region Public Methods
        public async Task<Response<string>> Handle(AddRoleCommand request, CancellationToken cancellationToken)
        {
            await _applicationRoleService.CreateAsync(request.RoleName);
            return Created<string>(_localizer[SharedResourceKeys.AddedSuccessfully]);
        }

        public async Task<Response<string>> Handle(EditRoleCommand request, CancellationToken cancellationToken)
        {
            var role = await _applicationRoleService.GetByIdAsync(request.Id);
            if (role is null)
                return NotFound<string>(_localizer[SharedResourceKeys.NotFound]);

            role.Name = request.NewName;
            await _applicationRoleService.EditAsync(role);
            return Success<string>(_localizer[SharedResourceKeys.UpdatedSuccessfully]);
        }

        public async Task<Response<string>> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
        {
            var role = await _applicationRoleService.GetByIdAsync(request.Id);
            if (role is null)
                return NotFound<string>(_localizer[SharedResourceKeys.NotFound]);

            await _applicationRoleService.DeleteAsync(role);
            return Deleted<string>();
        }
        #endregion
    }
}
