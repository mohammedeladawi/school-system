using SchoolProject.Data.Entities;
using SchoolProject.Data.Entities.Identities;
using SchoolProject.Service.ServiceBases;

namespace SchoolProject.Service.Abstracts;

public interface IApplicationUserService
{
    public Task AddApplicationUserAsync(ApplicationUser user, string password);
}