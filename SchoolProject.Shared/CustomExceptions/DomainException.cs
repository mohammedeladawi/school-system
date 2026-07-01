namespace SchoolProject.Shared.CustomExceptions;

public class DomainException : Exception
{
    public DomainException(string? message = null) : base(message)
    {

    }
}
