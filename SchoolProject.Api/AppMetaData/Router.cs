namespace SchoolProject.Api.AppMetaData;

public static class Router
{
    public const string Root = "api";
    public const string Version = "v1";
    public const string Base = Root + "/" + Version;

    public static class Student
    {
        private const string StudentBase = Base + "/" + "Student" + "/";

        public const string List = StudentBase + "List";
        public const string GetById = StudentBase + "{id}";

        public const string Add = StudentBase + "Add";

        public const string Update = StudentBase + "Update";

        public const string Delete = StudentBase + "{id}";

        public const string PaginatedList = StudentBase + "PaginatedList";

    }

    public static class Department
    {
        private const string DepartmentBase = Base + "/" + "Department" + "/";

        public const string List = DepartmentBase + "List";
        public const string GetById = DepartmentBase + "{id}";

        public const string Add = DepartmentBase + "Add";

        public const string Update = DepartmentBase + "Update";

        public const string Delete = DepartmentBase + "{id}";

        public const string PaginatedList = DepartmentBase + "PaginatedList";
    }


    public static class ApplicationUser
    {
        private const string ApplicationUserBase = Base + "/" + "User" + "/";

        public const string Register = ApplicationUserBase + "Register";
        public const string PaginatedList = ApplicationUserBase + "PaginatedList";
        public const string GetById = ApplicationUserBase + "{id}";
        public const string Update = ApplicationUserBase + "Update";
        public const string Delete = ApplicationUserBase + "{id}";
        public const string ChangePassword = ApplicationUserBase + "Change-Password";


    }

    public static class Authentication
    {
        private const string AuthBase = Base + "/" + "Authentication" + "/";

        public const string Login = AuthBase + "Login";
        public const string RefreshToken = AuthBase + "Refresh-Token";
        public const string Logout = AuthBase + "Logout";
    }

    public class Role
    {
        private const string RoleBase = Base + "/Role/";
        public const string Create = RoleBase + "Create";
        public const string Edit = RoleBase + "Edit";
        public const string Delete = RoleBase + "Delete" + "/{id}";
        public const string GetAll = RoleBase + "GetAll";
        public const string GetById = RoleBase + "GetById" + "/{id}";
    }

    public class Authorization
    {
        private const string AuthorizationBase = Base + "/Authorization/";
        public const string GetUserRolesById = AuthorizationBase + "GetUserRolesById" + "/{userId}";
    }
}