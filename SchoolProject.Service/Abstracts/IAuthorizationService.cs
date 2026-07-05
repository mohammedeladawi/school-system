namespace SchoolProject.Service.Abstracts;

public interface IAuthorizationService
{
    public Task<IList<string>> GetUserRolesByIdAsync(int userId);
}