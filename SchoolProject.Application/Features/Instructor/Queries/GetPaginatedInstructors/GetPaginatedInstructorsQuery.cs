using MediatR;
using SchoolProject.Application.Bases;
using SchoolProject.Application.Features.ApplicationUser.Queries.GetPaginatedUsers;
using SchoolProject.Application.Features.Base.Users.Queries.RequestDTOs;

namespace SchoolProject.Application.Features.Instructor.Queries.GetPaginatedInstructors;

public record GetPaginatedInstructorsQuery :
    BaseGetPaginatedUsersQuery<GetPaginatedInstructorsResponse>;
