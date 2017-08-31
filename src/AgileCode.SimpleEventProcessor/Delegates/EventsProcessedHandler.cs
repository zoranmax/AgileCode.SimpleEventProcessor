using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AgileCode.SimpleEventProcessor.Messages;

namespace AgileCode.SimpleEventProcessor.Delegates
{
    [Serializable]
    public delegate Task EventsProcessedHandler(List<EventResult> data);
}