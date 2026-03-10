namespace SchoolProject.Data.AppMetaData;

public static class Router
{
    public const string Root = "api";
    public const string Version = "v1";
    public const string Base = Root + "/" + Version;

    public static class Student
    {
        public const string List = Base + "/Student/List";
        public const string GetById = Base + "/Student/{id}";

        public const string Add = Base + "/Student/Add";
    }
}