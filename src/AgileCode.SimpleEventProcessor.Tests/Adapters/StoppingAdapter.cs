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
            get => throw new System.NotImplementedException();
            set => throw new System.NotImplementedException();
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
            throw new System.NotImplementedException();
        }

        public void Start()
        {
            throw new System.NotImplementedException();
        }

        public void Stop()
        {
            throw new System.NotImplementedException();
        }

        public void UnPause()
        {
            throw new System.NotImplementedException();
        }
    }
}