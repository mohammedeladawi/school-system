namespace SchoolProject.Shared.CustomExceptions;

public class ConflictException : Exception
{
    public ConflictException(string? message = null) : base(message)
    {

    }
}
