using MediatR;
using SchoolProject.Application.Bases;
using SchoolProject.Application.Features.ApplicationUser.Queries.GetUserById;
using SchoolProject.Application.Features.Base.Users.Queries.RequestDTOs;

namespace SchoolProject.Application.Features.Instructor.Queries.GetInstructorById;

public record GetInstructorByIdQuery(int Id) : BaseGetUserByIdQuery<GetInstructorByIdResponse>(Id);