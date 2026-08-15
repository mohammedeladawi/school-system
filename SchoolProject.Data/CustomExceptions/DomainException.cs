namespace SchoolProject.Data.CustomExceptions;

public class DomainException : Exception
{
    public DomainException(string? message = null) : base(message)
    {

    }
}
