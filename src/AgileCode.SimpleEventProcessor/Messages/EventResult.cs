using System;

namespace AgileCode.SimpleEventProcessor.Messages
{
    [Serializable]
    public class EventResult
    {
        public Event OriginalEvent { get; set; }
        public ExceptionResult Exception { get; set; }
        public Event Response { get; set; }
    }
}