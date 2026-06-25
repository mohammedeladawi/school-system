using SchoolProject.Data.Entities.Identities;

namespace SchoolProject.Service.Abstracts;

public interface IAuthenticationService
{
    string GenerateJwtToken(ApplicationUser user);

}
