using SchoolProject.Data.Entities;
using SchoolProject.Data.Entities.Identities;

namespace SchoolProject.Service.Abstracts;

public interface IApplicationRoleService
{
    public Task CreateAsync(string roleName);
    public Task<bool> DoesExistByNameAsync(string roleName);
}