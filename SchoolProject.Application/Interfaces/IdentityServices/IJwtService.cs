using SchoolProject.Data.Entities.Identities;

namespace SchoolProject.Application.Interfaces.IdentityServices;

public interface IJwtService
{
    public Task<string> GenerateJwtTokenAsync(ApplicationUser user);
}
