using System.Threading.Tasks;
using AgileCode.SimpleEventProcessor.Messages;

namespace AgileCode.SimpleEventProcessor.Interfaces
{
    public interface IActionHandler
    {
        Task<EventResult> Process(Event @event);
    }
}