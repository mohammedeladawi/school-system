using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Interfaces.Repositories;
using SchoolProject.Shared.Resources;

namespace SchoolProject.Core.Features.Student.Queries.GetAllStudents;

public class GetAllStudentsHandler : ResponseHandler, IRequestHandler<GetAllStudentsQuery, Response<List<GetAllStudentsQueryResponse>>>
{
    #region Private Fields
    private readonly IStudentRepository _studentRepository;
    #endregion

    #region Constructors
    public GetAllStudentsHandler(
        IStudentRepository studentRepository,
        IMapper mapper,
        IStringLocalizer<SharedResource> localizer) : base(localizer, mapper)
    {
        _studentRepository = studentRepository;
    }
    #endregion

    #region Public Methods
    public async Task<Response<List<GetAllStudentsQueryResponse>>> Handle(GetAllStudentsQuery request, CancellationToken cancellationToken)
    {
        var studentsList = await _studentRepository.GetAllAsync([s => s.Department]);
        var response = _mapper.Map<List<GetAllStudentsQueryResponse>>(studentsList);

        return Success(response);
    }
    #endregion
}