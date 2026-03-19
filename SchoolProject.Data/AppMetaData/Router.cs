namespace SchoolProject.Data.AppMetaData;

public static class Router
{
    public const string Root = "api";
    public const string Version = "v1";
    public const string Base = Root + "/" + Version;

    public static class Student
    {
        private const string StudentBase = Base + "/Student";

        public const string List = StudentBase + "/List";
        public const string GetById = StudentBase + "/{id}";

        public const string Add = StudentBase + "/Add";

        public const string Edit = StudentBase + "/Edit";

        public const string Delete = StudentBase + "/{id}";

        public const string PaginatedList = StudentBase + "/PaginatedList";

    }
}