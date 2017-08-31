using AgileCode.SimpleEventProcessor.Adapters;

namespace AgileCode.SimpleEventProcessor.Interfaces
{
    /// <summary>
    /// Responsibility of an adapter is to query the source and to inform
    /// the main application about a new item by recording the item in the 
    /// repository
    /// </summary>
    public interface IInputAdapter : ICanStartAndStop
    {
        /// <summary>
        /// Defines the input message being received
        /// </summary>
        event EventReceivedHandler EventsReceived;

        /// <summary>
        /// Max number of messages the Adapter need to retrieve from the source
        /// </summary>
        int MaxMessageCap { get; set; }
    }
}