using SchoolProject.Application.Interfaces.Bases;
using SchoolProject.Application.Mapping.ApplicationUser;

namespace SchoolProject.Application.Interfaces.IdentityServices;

public interface IUserManager :
    IGenericIdentityUserManagerAsync<Domain.Entities.Identities.ApplicationUser>;
