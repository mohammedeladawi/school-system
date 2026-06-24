using SchoolProject.Data.Entities;
using SchoolProject.Data.Entities.Identities;
using SchoolProject.Service.ServiceBases;

namespace SchoolProject.Service.Abstracts;

public interface IApplicationUserService
{
    public Task AddApplicationUserAsync(ApplicationUser user, string password);
    public Task<int> GetTotalApplicationUsersCountAsync();

    public Task<List<ApplicationUser>> GetPaginatedApplicationUsersAsync(
        int pageNumber,
        int pageSize);

    public Task<ApplicationUser> GetApplicationUserByIdAsync(int id);
}