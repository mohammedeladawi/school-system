using SchoolProject.Data.Entities.Identities;

namespace SchoolProject.Core.Interfaces.IdentityServices;

public interface IAuthenticationService
{
    public Task<string> GenerateJwtTokenAsync(ApplicationUser user);
    Task<string> GenerateEncodedEmailConfirmationTokenAsync(ApplicationUser user);
    public Task ConfirmEmailAsync(ApplicationUser user, string token);
    public Task ResetPasswordAsync(ApplicationUser user, string code, string newPassword);

}
