using System.Linq.Expressions;
using AutoMapper;
using Microsoft.Extensions.Localization;
using SchoolProject.Application.Features.ApplicationUser.Queries.GetUserById;
using SchoolProject.Application.Interfaces.Repositories;
using SchoolProject.Application.Resources;

namespace SchoolProject.Application.Features.Student.Queries.GetStudentById;

public class GetStudentByIdHandler :
    BaseGetUserByIdHandler<
        GetStudentByIdQuery,
        GetStudentByIdQueryResponse,
        IStudentManager,
        Domain.Entities.Student>
{
    #region Constructors
    public GetStudentByIdHandler(
        IStudentManager studentManager,
        IMapper mapper,
        IStringLocalizer<SharedResource> localizer)
        : base(studentManager, mapper, localizer)
    {
    }
    #endregion

    #region Protected Methods
    protected override Expression<Func<Domain.Entities.Student, object>>[]? GetIncludes()
        => [s => s.Department];
    #endregion
}