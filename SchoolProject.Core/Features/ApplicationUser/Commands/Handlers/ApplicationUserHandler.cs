using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.ApplicationUser.Commands.Models;
using SchoolProject.Data.Entities.Identities;
using SchoolProject.Service.Abstracts;
using SchoolProject.Shared.Resources;

namespace SchoolProject.Core.Features.ApplicationUser.Commands.Handlers
{
    public class ApplicationUserCommandHandler :
        ResponseHandler,
        IRequestHandler<AddApplicationUserCommand, Response<string>>,
        IRequestHandler<EditApplicationUserCommand, Response<string>>, 
        IRequestHandler<DeleteApplicationUserByIdCommand, Response<string>>
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

        public async Task<Response<string>> Handle(EditApplicationUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _applicationUserService.GetApplicationUserByIdAsync(request.Id);
            _mapper.Map(request, user);
            await _applicationUserService.UpdateApplicationUserAsync(user);
            return Success<string>(_localizer[SharedResourceKeys.UpdatedSuccessfully]);
        }

        public async Task<Response<string>> Handle(DeleteApplicationUserByIdCommand request, CancellationToken cancellationToken)
        {
            var user = await _applicationUserService.GetApplicationUserByIdAsync(request.Id);
            if (user == null)
                return NotFound<string>();

            await _applicationUserService.DeleteApplicationUserById(user);

            return Deleted<string>();
        }
    }
}
