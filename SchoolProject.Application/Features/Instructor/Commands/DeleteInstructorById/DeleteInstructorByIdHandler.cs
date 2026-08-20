using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProject.Application.Bases;
using SchoolProject.Application.Interfaces.IdentityServices;
using SchoolProject.Application.Interfaces.Repositories;
using SchoolProject.Application.Resources;

namespace SchoolProject.Application.Features.ApplicationUser.Commands.DeleteInstructorById;

public class DeleteInstructorByIdHandler : ResponseHandler, IRequestHandler<DeleteInstructorByIdCommand, Response<string>>
{
    #region Private Fields
    private readonly IInstructorManager _instructorManager;
    #endregion

    #region Constructors
    public DeleteInstructorByIdHandler(
        IStringLocalizer<SharedResource> localizer,
        IMapper mapper,
        IInstructorManager instructorManager)
        : base(localizer, mapper)
    {
        _instructorManager = instructorManager;
    }
    #endregion

    #region Public Methods
    public async Task<Response<string>> Handle(DeleteInstructorByIdCommand request, CancellationToken cancellationToken)
    {
        var instructor = await _instructorManager.GetByIdAsync(request.Id);
        await _instructorManager.DeleteAsync(instructor!);
        return Deleted<string>();
    }
    #endregion
}