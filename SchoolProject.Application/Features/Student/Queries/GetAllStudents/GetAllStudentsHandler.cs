using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProject.Application.Bases;
using SchoolProject.Application.Interfaces.Repositories;
using SchoolProject.Application.Resources;

namespace SchoolProject.Application.Features.Student.Queries.GetAllStudents;

public class GetAllStudentsHandler : ResponseHandler, IRequestHandler<GetAllStudentsQuery, Response<List<GetAllStudentsQueryResponse>>>
{
    #region Private Fields
    private readonly IStudentManager _StudentManager;
    #endregion

    #region Constructors
    public GetAllStudentsHandler(
        IStudentManager StudentManager,
        IMapper mapper,
        IStringLocalizer<SharedResource> localizer) : base(localizer, mapper)
    {
        _StudentManager = StudentManager;
    }
    #endregion

    #region Public Methods
    public async Task<Response<List<GetAllStudentsQueryResponse>>> Handle(GetAllStudentsQuery request, CancellationToken cancellationToken)
    {
        var studentsList = await _StudentManager.GetAllAsync([s => s.Department]);
        var response = _mapper.Map<List<GetAllStudentsQueryResponse>>(studentsList);
        return Success(response);

        throw new Exception();
    }
    #endregion
}