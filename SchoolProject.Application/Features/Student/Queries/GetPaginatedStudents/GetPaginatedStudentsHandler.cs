using System.Linq.Expressions;
using AutoMapper;
using Microsoft.Extensions.Localization;
using SchoolProject.Application.Features.Base.Users.Queries.Handlers;
using SchoolProject.Application.Interfaces.Repositories;
using SchoolProject.Application.Resources;

namespace SchoolProject.Application.Features.Student.Queries.GetPaginatedStudents;

public class GetPaginatedStudentsHandler :
    BaseGetPaginatedUsersHandler<
        GetPaginatedStudentsQuery,
        GetPaginatedStudentsQueryResponse,
        IStudentManager,
        Domain.Entities.Student>
{
    #region Constructors
    public GetPaginatedStudentsHandler(
        IMapper mapper,
        IStringLocalizer<SharedResource> localizer,
        IStudentManager studentManager)
        : base(mapper, localizer, studentManager)
    {
    }
    #endregion

    #region Protected Methods
    protected override Expression<Func<Domain.Entities.Student, object>>[]? GetIncludes()
        => [s => s.Department];
    #endregion
}