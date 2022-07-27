using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Lib.EventBus.AuthService
{
    public record AccountRegisteredEvent(string Phone, string Email, string FirstName, string LastName)
    {
        public AccountRegisteredEvent() : this(string.Empty, string.Empty, string.Empty, string.Empty) { }


        public string Phone { get; set; } = Phone;

        public string Email { get; set; } = Email;

        public string FirstName { get; set; } = FirstName;

        public string LastName { get; set; } = LastName;
    }
}
