

namespace Domain.Services.Exceptions;

public class RepositoryFailedException: DomainException
{
        
    public RepositoryFailedException(string message, Exception innerException) : base(message, innerException) { }
}