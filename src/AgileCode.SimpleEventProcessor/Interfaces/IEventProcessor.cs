using System.Collections.Generic;
using System.Threading.Tasks;
using AgileCode.SimpleEventProcessor.Messages;

namespace AgileCode.SimpleEventProcessor.Interfaces
{
    public interface IEventProcessor
    {
        Task Publish(List<Event> events);

        bool CanProcessNewItems(int numberOfNewItems);

        long Capacity { get; set; }
    }
}