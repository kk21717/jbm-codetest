using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Command.RegisterUser
{
    public class RegUserCommandOutput
    {
        public RegUserCommandOutput(int newUserId)
        {
            NewUserId = newUserId;
        }

        public int NewUserId { get; set; }
    }
}
