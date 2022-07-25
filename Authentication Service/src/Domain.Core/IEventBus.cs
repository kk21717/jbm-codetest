using Domain.Core.Event;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Core
{
    public interface IEventBus
    {
       public Task pushAccountRegisteredEvent(AccountRegisteredEvent e);
    }
}
