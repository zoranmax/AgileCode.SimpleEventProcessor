using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AgileCode.SimpleEventProcessor.Interfaces;
using AgileCode.SimpleEventProcessor.Logging;
using AgileCode.SimpleEventProcessor.Messages;

namespace AgileCode.SimpleEventProcessor
{
    [Serializable]
    public class SimpleEventProcessorRunner : SimpleEventProcessorRunnerBase, IEventSystem
    {
        public SimpleEventProcessorRunner(IEventProcessor eventProcessor, IInputAdapter inputAdapter, ILogger logger)
            : base(eventProcessor, inputAdapter, logger)
        {
            InputAdapter.EventsReceived += EventsReceived;
        }

        public static SimpleEventProcessorRunner CreateNew(IEventProcessor eventProcessor, IInputAdapter inputAdapter, ILogger logger)
        {
            return new SimpleEventProcessorRunner(eventProcessor, inputAdapter, logger);
        }

        protected virtual async Task EventsReceived(List<Event> events)
        {
            /*
             * Before publishing any message to the EventHander we need to know if 
             * the handler has the capacity of processing this numer of events.
             * The fact is, we may choose not to hold too many messages in memory
             * as the risk is that if the application get shut down those messages would disappear.
             * */
            while (!EventProcessor.CanProcessNewItems(events.Count))
            {
                //Console.WriteLine("System busy... waiting for 10 milliseconds...");
                await Task.Delay(TimeSpan.FromMilliseconds(10));
            }
            await EventProcessor.Publish(events);
        }
    }
}