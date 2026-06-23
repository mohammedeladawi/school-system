using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.ApplicationUser.Commands.Models;
using SchoolProject.Shared.Resources;
using SchoolProject.Data.Entities.Identities;
using SchoolProject.Service.Abstracts;

namespace SchoolProject.Core.Features.ApplicationUser.Commands.Handlers
{
    public class ApplicationUserCommandHandler :
        ResponseHandler,
        IRequestHandler<AddApplicationUserCommand, Response<string>>

    {
        private readonly IApplicationUserService _applicationUserService;

        public ApplicationUserCommandHandler(
            IStringLocalizer<SharedResource> localizer,
            IMapper mapper,
            IApplicationUserService applicationUserService)
            : base(localizer, mapper)
        {
            _applicationUserService = applicationUserService;
        }


        public async Task<Response<string>> Handle(AddApplicationUserCommand request, CancellationToken cancellationToken)
        {
            var applicationUser = _mapper.Map<Data.Entities.Identities.ApplicationUser>(request);
            await _applicationUserService.AddApplicationUserAsync(applicationUser, request.Password);
            return Created<string>(_localizer[SharedResourceKeys.AddedSuccessfully]);
        }
    }
}
