using System.Threading.Tasks;
using AgileCode.SimpleEventProcessor.Messages;

namespace AgileCode.SimpleEventProcessor.Interfaces
{
    public interface IOutputAdapter
    {
        Task Publish(EventResult eventResult);
    }
}