using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.Exceptions
{
    public class RepositorayFailedException: DomainException
    {
        public RepositorayFailedException():base(){}

        public RepositorayFailedException(string message, Exception innerException) : base(message, innerException) { }
    }
}
