using MediatR;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.ApplicationUser.Queries.GetUserById;

namespace SchoolProject.Core.Features.ApplicationUser.Queries.GetUserById;

public record GetUserByIdQuery(int Id) : IRequest<Response<GetUserByIdQueryResponse>>;