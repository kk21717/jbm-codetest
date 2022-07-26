﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.Exceptions
{
    public class DomainException:Exception
    {
        public DomainException() : base() { }

        public DomainException(string message, Exception innerException) : base(message, innerException) { }
        
    }
}