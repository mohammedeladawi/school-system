using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProject.Application.Bases;
using SchoolProject.Application.Interfaces.Repositories;
using SchoolProject.Shared.Resources;

namespace SchoolProject.Application.Features.Student.Queries.GetStudentById;

public class GetStudentByIdHandler : ResponseHandler, IRequestHandler<GetStudentByIdQuery, Response<GetStudentByIdQueryResponse>>
{
    #region Private Fields
    private readonly IStudentRepository _studentRepository;
    #endregion

    #region Constructors
    public GetStudentByIdHandler(
        IStudentRepository studentRepository,
        IMapper mapper,
        IStringLocalizer<SharedResource> localizer) : base(localizer, mapper)
    {
        _studentRepository = studentRepository;
    }
    #endregion

    #region Public Methods
    public async Task<Response<GetStudentByIdQueryResponse>> Handle(GetStudentByIdQuery request, CancellationToken cancellationToken)
    {
        var student = await _studentRepository.GetByIdAsync(request.Id, [s => s.Department]);
        if (student is null) return NotFound<GetStudentByIdQueryResponse>(_localizer[SharedResourceKeys.NotFound]);

        var response = _mapper.Map<GetStudentByIdQueryResponse>(student);
        return Success(response);
    }
    #endregion
}