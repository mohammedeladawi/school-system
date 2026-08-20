using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProject.Application.Bases;
using SchoolProject.Application.Interfaces.Bases;
using SchoolProject.Application.Interfaces.Repositories;
using SchoolProject.Application.Resources;

namespace SchoolProject.Application.Features.Student.Commands.EditStudent;

public class EditStudentHandler : ResponseHandler, IRequestHandler<EditStudentCommand, Response<string>>
{
    #region Private Fields
    private readonly IStudentManager _StudentManager;
    private readonly IUnitOfWork _unitOfWork;
    #endregion

    #region Constructors
    public EditStudentHandler(
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
    public async Task<Response<string>> Handle(EditStudentCommand request, CancellationToken cancellationToken)
    {
        var student = _mapper.Map<Domain.Entities.Student>(request);

        await _StudentManager.UpdateAsync(student);
        await _unitOfWork.SaveChangesAsync();

        return Success<string>(_localizer[SharedResourceKeys.UpdatedSuccessfully]);
    }
    #endregion
}