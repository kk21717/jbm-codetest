﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Command.RegisterUser
{
    public class RegUserCommandInput
    {
        public string Phone { get; set; }
        public string Email { get; set; }


        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
