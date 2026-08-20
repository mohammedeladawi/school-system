using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProject.Application.Bases;
using SchoolProject.Application.Interfaces.Repositories;
using SchoolProject.Application.Resources;

namespace SchoolProject.Application.Features.Student.Queries.GetStudentById;

public class GetStudentByIdHandler : ResponseHandler, IRequestHandler<GetStudentByIdQuery, Response<GetStudentByIdQueryResponse>>
{
    #region Private Fields
    private readonly IStudentManager _StudentManager;
    #endregion

    #region Constructors
    public GetStudentByIdHandler(
        IStudentManager StudentManager,
        IMapper mapper,
        IStringLocalizer<SharedResource> localizer) : base(localizer, mapper)
    {
        _StudentManager = StudentManager;
    }
    #endregion

    #region Public Methods
    public async Task<Response<GetStudentByIdQueryResponse>> Handle(GetStudentByIdQuery request, CancellationToken cancellationToken)
    {
        var student = await _StudentManager.GetByIdAsync(request.Id, [s => s.Department]);
        if (student is null) return NotFound<GetStudentByIdQueryResponse>(_localizer[SharedResourceKeys.NotFound]);

        var response = _mapper.Map<GetStudentByIdQueryResponse>(student);
        return Success(response);
    }
    #endregion
}