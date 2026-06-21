namespace SchoolProject.Data.CustomExceptions;

public class ConflictException : Exception
{
    public ConflictException(string? message = null) : base(message)
    {

    }
}