using System.Collections.Generic;
using System.Threading.Tasks;
using AgileCode.SimpleEventProcessor.Adapters;
using AgileCode.SimpleEventProcessor.Messages;

namespace AgileCode.SimpleEventProcessor.Interfaces
{
    public interface IEventSystem
    {
        /// <summary>
        /// Starts the processor
        /// </summary>
        void Start();

        /// <summary>
        /// Stops the processor
        /// </summary>
        void Stop();

        bool IsRunning { get; }        
    }
}