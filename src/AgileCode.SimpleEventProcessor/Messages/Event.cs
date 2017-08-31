using System;
using System.Collections.Generic;

namespace AgileCode.SimpleEventProcessor.Messages
{
    [Serializable]
    public class Event
    {
        public Event(string id, object body)
        {
            this.PropertyBag = new List<NameValue>();
            this.Body = body;
            this.Id = id;
        }

        public Event(string id, object body, List<NameValue> propertyBag): this(id, body)
        {
            this.PropertyBag = propertyBag;

            
        }

        public Event(object body)
        {
            Body = body;
        }

        public string Id { get; protected set; }
        public object Body { get; protected set; }
        public List<NameValue> PropertyBag { get; protected set; }
    }
}