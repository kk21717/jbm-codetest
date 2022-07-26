using Shared.Lib.EventBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Command.Events
{
    public record AccountRegisteredEvent : BaseEvent
    {
        public AccountRegisteredEvent():base(){}
        
        public AccountRegisteredEvent(string phone, string email, string firstName, string lastName)
        {
            Phone = phone;
            Email = email;
            FirstName = firstName;
            LastName = lastName;
        }


        public string Phone { get; set; }

        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }



    }
}
