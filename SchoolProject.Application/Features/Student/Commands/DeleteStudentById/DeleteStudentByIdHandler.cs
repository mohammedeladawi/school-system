using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProject.Application.Bases;
using SchoolProject.Application.Interfaces.Bases;
using SchoolProject.Application.Interfaces.Repositories;
using SchoolProject.Application.Resources;

namespace SchoolProject.Application.Features.Student.Commands.DeleteStudentById;

public class DeleteStudentByIdHandler : ResponseHandler, IRequestHandler<DeleteStudentByIdCommand, Response<string>>
{
    #region Private Fields
    private readonly IStudentManager _StudentManager;
    private readonly IUnitOfWork _unitOfWork;
    #endregion

    #region Constructors
    public DeleteStudentByIdHandler(
        IMapper mapper,
        IStudentManager StudentManager,
        IStringLocalizer<SharedResource> localizer,
        IUnitOfWork unitOfWork)
        : base(localizer, mapper)
    {
        _StudentManager = StudentManager;
        _unitOfWork = unitOfWork;
    }
    #endregion

    #region Public Methods
    public async Task<Response<string>> Handle(DeleteStudentByIdCommand request, CancellationToken cancellationToken)
    {
        var student = await _StudentManager.GetByIdAsync(request.Id);
        if (student is null)
            return NotFound<string>(_localizer[SharedResourceKeys.NotFound]);

        await _StudentManager.DeleteAsync(student);
        await _unitOfWork.SaveChangesAsync();

        return Deleted<string>();
    }
    #endregion
}