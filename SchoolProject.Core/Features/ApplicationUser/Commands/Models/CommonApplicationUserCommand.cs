using MediatR;
using SchoolProject.Core.Bases;

namespace SchoolProject.Core.Features.ApplicationUser.Commands.Models;

public abstract record CommonApplicationUserCommand
{
        public string NameEn { get; init; } = null!;
        public string NameAr { get; init; } = null!;
        public string UserName { get; init; } = null!;
        public string Email { get; init; } = null!;
        public string? Phone { get; init; }
        public string? Country { get; init; }
}
