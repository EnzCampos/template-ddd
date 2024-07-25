namespace Template.Application.Exceptions
{
    public class ForbiddenException(string message) : ApplicationException(message)
    {
    }
}
