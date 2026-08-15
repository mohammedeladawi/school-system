namespace SchoolProject.Domain.ClaimStore
{
    public static class PermissionClaims
    {
        public static List<string> UserPermissionClaims = new()
        {
            "User.GetPaginated",
            "User.ChangePassword",
        };
    }
}