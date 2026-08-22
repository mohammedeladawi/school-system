using MediatR;
using SchoolProject.Application.Bases;
using SchoolProject.Application.Features.ApplicationUser.Queries.GetUserById;

namespace SchoolProject.Application.Features.Base.Users.Queries.RequestDTOs;

public record BaseGetUserByIdQuery<TResponse>(int Id) : IRequest<Response<TResponse>>;