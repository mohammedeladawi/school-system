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

        public const string Register = StudentBase + "Register";

        public const string Update = StudentBase + "Update";

        public const string Delete = StudentBase + "{id}";

        public const string PaginatedList = StudentBase + "Paginated-List";

    }

    public static class Department
    {
        private const string DepartmentBase = Base + "/" + "Department" + "/";

        public const string List = DepartmentBase + "Get-All";
        public const string GetById = DepartmentBase + "{id}";
        public const string StudentsCount = DepartmentBase + "Students-Count";

        public const string Add = DepartmentBase + "Add";

        public const string Update = DepartmentBase + "Update";

        public const string Delete = DepartmentBase + "{id}";

        public const string PaginatedList = DepartmentBase + "Paginated-List";
    }

    public static class Instructor
    {
        private const string InstructorBase = Base + "/" + "Instructor" + "/";
        public const string Register = InstructorBase + "Register";
        public const string Delete = InstructorBase + "{id}";
        public const string GetById = InstructorBase + "{id}";
        public const string Update = InstructorBase + "Update";
        public const string PaginatedList = InstructorBase + "Paginated-List";
    }
    public static class ApplicationUser
    {
        private const string ApplicationUserBase = Base + "/" + "User" + "/";
        public const string Register = ApplicationUserBase + "Register";
        public const string PaginatedList = ApplicationUserBase + "Paginated-List";
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

        public const string ConfirmEmail = AuthBase + "Confirm-Email";

        public const string ForgotPassword = AuthBase + "Forgot-Password";
        public const string VerifyResetCode = AuthBase + "Verify-Reset-Code";
        public const string ResetPassword = AuthBase + "Reset-Password";


    }

    public static class Role
    {
        private const string RoleBase = Base + "/" + "Role" + "/";
        public const string Add = RoleBase + "Add";
        public const string Update = RoleBase + "Update";
        public const string Delete = RoleBase + "{id}";
        public const string GetAll = RoleBase + "Get-All";
        public const string GetById = RoleBase + "{id}";
    }

    public static class Authorization
    {
        private const string AuthorizationBase = Base + "/Authorization/";
        public const string GetUserRolesById = AuthorizationBase + "Get-User-Roles" + "/{userId}";
        public const string UpdateUserRoles = AuthorizationBase + "Update-User-Roles";
        public const string GetUserPermissionClaims = AuthorizationBase + "User-Permission-Claims" + "/{userId}";
        public const string UpdateUserPermissionClaims = AuthorizationBase + "Update-User-Permission-Claims";
    }

    public static class Emails
    {
        private const string EmailsBase = Base + "/" + "Emails" + "/";
        public const string Send = EmailsBase + "Send";
    }
}