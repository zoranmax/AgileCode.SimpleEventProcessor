using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using AgileCode.SimpleEventProcessor.Adapters;
using AgileCode.SimpleEventProcessor.Interfaces;
using AgileCode.SimpleEventProcessor.Messages;

namespace AgileCode.SimpleEventProcessor.Tests.Adapters
{
    internal class AdapterMock : IInputAdapter
    {
        public AdapterMock()
        {
        }

        public event EventReceivedHandler EventsReceived;

        protected virtual Task OnMessageReceived(List<Event> data)
        {
            if (EventsReceived != null)
            {
                return EventsReceived(data);
            }
            return Task.FromResult<object>(null);
        }
        public string Name { get; set; }
        public bool IsRunning { get; set; }
        public bool IsPaused { get; set; }

        public void Start()
        {
            IsRunning = true;
        }

        public void Enqueue(Event e)
        {
            Console.WriteLine("InputAdapterMock received: " + e.Body);
            Debug.WriteLine("InputAdapterMock received: " + e.Body);

            OnMessageReceived(new List<Event>() { e });
        }
        public void Stop()
        {
            IsRunning = false;
        }

        public void Pause()
        {
            throw new NotImplementedException();
        }

        public void UnPause()
        {
            throw new NotImplementedException();
        }

        public int MaxMessageCap { get; set; }
    }
}