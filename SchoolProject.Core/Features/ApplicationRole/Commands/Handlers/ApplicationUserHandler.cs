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
        IRequestHandler<AddRoleCommand, Response<string>>
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
        #endregion
    }
}
