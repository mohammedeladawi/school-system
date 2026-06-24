using MediatR;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.ApplicationUser.Queries.Responses;

namespace SchoolProject.Core.Features.ApplicationUser.Queries.Models;

public record GetApplicationUserByIdQuery(int Id) :
    IRequest<Response<GetApplicationUserByIdQueryResponse>>;
