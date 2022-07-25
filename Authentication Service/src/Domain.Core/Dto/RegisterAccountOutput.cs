using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Core.Dto
{
    public enum RegisterAccountOutput
    {
        Done = 0,

        EmptyPhoneNumber = 1,
        InvalidPhoneNumber = 2,
        InvalidEmail = 3,
        DuplicatePhoneNumber = 4,
        RepositoryFailed = 5,

    }
}
