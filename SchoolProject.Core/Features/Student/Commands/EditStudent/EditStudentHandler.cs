using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Interfaces.Bases;
using SchoolProject.Core.Interfaces.Repositories;
using SchoolProject.Shared.Resources;

namespace SchoolProject.Core.Features.Student.Commands.EditStudent;

public class EditStudentHandler : ResponseHandler, IRequestHandler<EditStudentCommand, Response<string>>
{
    #region Private Fields
    private readonly IStudentRepository _studentRepository;
    private readonly IUnitOfWork _unitOfWork;
    #endregion

    #region Constructors
    public EditStudentHandler(
        IMapper mapper,
        IStudentRepository studentRepository,
        IStringLocalizer<SharedResource> localizer,
        IUnitOfWork unitOfWork)
        : base(localizer, mapper)
    {
        _studentRepository = studentRepository;
        _unitOfWork = unitOfWork;
    }
    #endregion

    #region Public Methods
    public async Task<Response<string>> Handle(EditStudentCommand request, CancellationToken cancellationToken)
    {
        var student = _mapper.Map<Data.Entities.Student>(request);

        await _studentRepository.UpdateAsync(student);
        await _unitOfWork.SaveChangesAsync();

        return Success<string>(_localizer[SharedResourceKeys.UpdatedSuccessfully]);
    }
    #endregion
}