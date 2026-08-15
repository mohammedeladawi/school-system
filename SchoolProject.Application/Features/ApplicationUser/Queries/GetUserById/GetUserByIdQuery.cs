using MediatR;
using SchoolProject.Application.Bases;
using SchoolProject.Application.Features.ApplicationUser.Queries.GetUserById;

namespace SchoolProject.Application.Features.ApplicationUser.Queries.GetUserById;

public record GetUserByIdQuery(int Id) : IRequest<Response<GetUserByIdQueryResponse>>;