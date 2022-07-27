

namespace Domain.Services.Exceptions;

public class DomainException:Exception
{
    public DomainException(){ }

    public DomainException(string message, Exception innerException) : base(message, innerException) { }
        
}