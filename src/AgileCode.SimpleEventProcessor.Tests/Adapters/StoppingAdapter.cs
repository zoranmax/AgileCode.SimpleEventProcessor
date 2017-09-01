using System.Collections.Generic;
using System.Threading.Tasks;
using AgileCode.SimpleEventProcessor.Messages;
using AgileCode.SimpleEventProcessor.Interfaces;
using AgileCode.SimpleEventProcessor.Adapters;

namespace AgileCode.SimpleEventProcessor.Tests.Adapters
{
    internal class StoppingAdapter : IInputAdapter
    {
        public int MaxMessageCap
        {
            get => throw new System.NotImplementedException();
            set => throw new System.NotImplementedException();
        }

        public bool IsRunning
        {
            get;set;
        }

        public bool IsPaused {
            get => throw new System.NotImplementedException();
            set => throw new System.NotImplementedException();
        }

        public event EventReceivedHandler EventsReceived;

        public async Task<IList<Event>> DequeueAsync(int maxMessageCap, int pollingTimeOut)
        {
            await Task.Delay(30000);
            return await Task.FromResult(new List<Event>());
        }

        public void Pause()
        {
            this.IsRunning = false;
        }

        public void Start()
        {
            this.IsRunning = true;
        }

        public void Stop()
        {
            this.IsRunning = false;
        }

        public void UnPause()
        {
            this.IsRunning = true;
        }
    }
}