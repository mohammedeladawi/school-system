using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using SchoolProject.Application.Interfaces.Repositories;
using SchoolProject.Domain.Entities;
using SchoolProject.Domain.Entities.Identities;
using SchoolProject.Infrastructure.Data;
using SchoolProject.Infrastructure.Bases;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace SchoolProject.Infrastructure.Repositories;

public class StudentManager :
    GenericIdentityUserManagerAsync<Student>,
    IStudentManager
{
    #region Constructors
    public StudentManager(
        UserManager<ApplicationUser> userManager,
        ILogger<StudentManager> logger) : base(userManager, logger)
    {
    }
    #endregion
}
