using System.Collections.Generic;
using System.Threading.Tasks;
using AgileCode.SimpleEventProcessor.Delegates;
using AgileCode.SimpleEventProcessor.Interfaces;
using AgileCode.SimpleEventProcessor.Messages;

namespace AgileCode.SimpleEventProcessor.Processors
{
    //public class SynchronousEventProcessor : IEventProcessor
    //{
    //    protected readonly IActionHandler ActionHandler;
    //    protected readonly IOutputAdapter OutboundAdapter;
    //    protected long itemsProcessed = 0;
    //    private object _lock = new object();

    //    public SynchronousEventProcessor(IActionHandler actionHandler, IOutputAdapter outboundAdapter)
    //    {
    //        ActionHandler = actionHandler;
    //        OutboundAdapter = outboundAdapter;
    //    }

    //    public async Task Publish(List<Event> events)
    //    {
    //        IncreaseNumberOfItems(events);

    //        foreach (Event @event in events)
    //        {
    //            var eventResult = await ActionHandler.Process(@event);
    //        }

    //        //await OnEventsProcessed(eventResults);

    //        DecreaseNumberOfItems(events);
    //    }

    //    private void DecreaseNumberOfItems(List<Event> events)
    //    {
    //        itemsProcessed = itemsProcessed - events.Count;
    //    }

    //    private void IncreaseNumberOfItems(List<Event> events)
    //    {
    //        itemsProcessed = itemsProcessed + events.Count;
    //    }

    //    public event EventsProcessedHandler EventsProcessed;

    //    public bool CanProcessNewItems(int numberOfNewItems)
    //    {
    //        return true;
    //    }

    //    public long Capacity { get; set; }

    //    protected virtual async Task OnEventsProcessed(List<EventResult> data)
    //    {
    //        if (EventsProcessed != null)
    //        {
    //            await EventsProcessed(data);
    //        }            
    //    }
    //}
}