using MediatR;
using Microsoft.AspNetCore.Http;
using SchoolProject.Application.Bases;

namespace SchoolProject.Application.Features.Base.Users.Commands.RequestDTOs;

public record BaseDeleteUserByIdCommand(int Id) : IRequest<Response<string>>;