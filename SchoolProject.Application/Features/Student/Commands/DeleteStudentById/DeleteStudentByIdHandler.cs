using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProject.Application.Bases;
using SchoolProject.Application.Interfaces.Bases;
using SchoolProject.Application.Interfaces.Repositories;
using SchoolProject.Shared.Resources;

namespace SchoolProject.Application.Features.Student.Commands.DeleteStudentById;

public class DeleteStudentByIdHandler : ResponseHandler, IRequestHandler<DeleteStudentByIdCommand, Response<string>>
{
    #region Private Fields
    private readonly IStudentRepository _studentRepository;
    private readonly IUnitOfWork _unitOfWork;
    #endregion

    #region Constructors
    public DeleteStudentByIdHandler(
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
    public async Task<Response<string>> Handle(DeleteStudentByIdCommand request, CancellationToken cancellationToken)
    {
        var student = await _studentRepository.GetByIdAsync(request.Id);
        if (student is null)
            return NotFound<string>(_localizer[SharedResourceKeys.NotFound]);

        await _studentRepository.DeleteAsync(student);
        await _unitOfWork.SaveChangesAsync();

        return Deleted<string>();
    }
    #endregion
}