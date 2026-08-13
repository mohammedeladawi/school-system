using SchoolProject.Data.Entities.Identities;

namespace SchoolProject.Core.Interfaces.IdentityServices;

public interface IJwtService
{
    public Task<string> GenerateJwtTokenAsync(ApplicationUser user);
}
