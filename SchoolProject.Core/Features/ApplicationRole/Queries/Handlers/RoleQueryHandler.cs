using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.ApplicationRole.Commands.Models;
using SchoolProject.Core.Features.ApplicationRole.Queries.Models;
using SchoolProject.Core.Features.ApplicationRole.Queries.Responses;
using SchoolProject.Core.Interfaces.Identities;
using SchoolProject.Data.Entities.Identities;
using SchoolProject.Shared.Resources;

namespace SchoolProject.Core.Features.ApplicationRole.Commands.Handlers
{
    public class ApplicationRoleQueryHandler :
        ResponseHandler,
        IRequestHandler<GetAllRolesQuery, Response<List<GetAllRolesQueryResponse>>>,
        IRequestHandler<GetRoleByIdQuery, Response<GetRoleByIdQueryResponse>>
    {
        #region Private Fields
        private readonly IApplicationRoleRepository _applicationRoleService;
        #endregion

        #region Constructors
        public ApplicationRoleQueryHandler(
            IStringLocalizer<SharedResource> localizer,
            IMapper mapper,
            IApplicationRoleRepository ApplicationRoleService)
            : base(localizer, mapper)
        {
            _applicationRoleService = ApplicationRoleService;
        }

        #endregion

        #region Public Methods

        public async Task<Response<List<GetAllRolesQueryResponse>>> Handle(GetAllRolesQuery request, CancellationToken cancellationToken)
        {
            var roles = await _applicationRoleService.GetAllAsync();
            var response = _mapper.Map<List<GetAllRolesQueryResponse>>(roles);
            return Success(response);
        }

        public async Task<Response<GetRoleByIdQueryResponse>> Handle(GetRoleByIdQuery request, CancellationToken cancellationToken)
        {
            var role = await _applicationRoleService.GetByIdAsync(request.Id);
            if (role == null)
                return NotFound<GetRoleByIdQueryResponse>();

            var response = _mapper.Map<GetRoleByIdQueryResponse>(role);
            return Success(response);

        }

        #endregion
    }
}
