using System;
using System.Diagnostics;
using System.Threading.Tasks;
using AgileCode.SimpleEventProcessor.Interfaces;
using AgileCode.SimpleEventProcessor.Messages;

namespace AgileCode.SimpleEventProcessor.Tests.Adapters
{
    public class OutputAdapterMock : IOutputAdapter
    {
        public string Name { get; set; }
        public int MaxMessageCap { get; set; }

        public Task Publish(EventResult eventResult)
        {
            Console.WriteLine("OutputAdapterMock received: " + eventResult.OriginalEvent.Body);
            Debug.WriteLine("OutputAdapterMock received: " + eventResult.OriginalEvent.Body);

            return Task.FromResult<object>(null);
        }
    }
}