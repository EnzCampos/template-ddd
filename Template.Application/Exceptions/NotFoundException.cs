namespace Template.Application.Exceptions
{
    public class NotFoundException(string name, object key) 
        : ApplicationException($"Objeto \"{name}\" ({key}) não foi encontrado.")
    {
    }
}
