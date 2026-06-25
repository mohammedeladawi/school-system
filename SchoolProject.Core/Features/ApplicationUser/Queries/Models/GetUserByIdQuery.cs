using MediatR;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.ApplicationUser.Queries.Responses;

namespace SchoolProject.Core.Features.ApplicationUser.Queries.Models;

public record GetUserByIdQuery(int Id) :
    IRequest<Response<GetUserByIdQueryResponse>>;
