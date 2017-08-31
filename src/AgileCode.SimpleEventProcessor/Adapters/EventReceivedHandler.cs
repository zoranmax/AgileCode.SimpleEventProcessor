using System.Collections.Generic;
using System.Threading.Tasks;
using AgileCode.SimpleEventProcessor.Messages;

namespace AgileCode.SimpleEventProcessor.Adapters
{
    public delegate Task EventReceivedHandler(List<Event> data);
}