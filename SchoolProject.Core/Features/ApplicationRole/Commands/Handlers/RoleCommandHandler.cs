using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.ApplicationRole.Commands.Models;
using SchoolProject.Core.Interfaces.Identities;
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
        private readonly IApplicationRoleRepository _applicationRoleRepository;
        #endregion

        #region Constructors
        public ApplicationRoleCommandHandler(
            IStringLocalizer<SharedResource> localizer,
            IMapper mapper,
            IApplicationRoleRepository ApplicationRoleService)
            : base(localizer, mapper)
        {
            _applicationRoleRepository = ApplicationRoleService;
        }
        #endregion

        #region Public Methods
        public async Task<Response<string>> Handle(AddRoleCommand request, CancellationToken cancellationToken)
        {
            await _applicationRoleRepository.CreateAsync(request.RoleName);
            return Created<string>(_localizer[SharedResourceKeys.AddedSuccessfully]);
        }

        public async Task<Response<string>> Handle(EditRoleCommand request, CancellationToken cancellationToken)
        {
            var role = await _applicationRoleRepository.GetByIdAsync(request.Id);
            if (role is null)
                return NotFound<string>(_localizer[SharedResourceKeys.NotFound]);

            role.Name = request.NewName;
            await _applicationRoleRepository.EditAsync(role);
            return Success<string>(_localizer[SharedResourceKeys.UpdatedSuccessfully]);
        }

        public async Task<Response<string>> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
        {
            var role = await _applicationRoleRepository.GetByIdAsync(request.Id);
            if (role is null)
                return NotFound<string>(_localizer[SharedResourceKeys.NotFound]);

            await _applicationRoleRepository.DeleteAsync(role);
            return Deleted<string>();
        }
        #endregion
    }
}
