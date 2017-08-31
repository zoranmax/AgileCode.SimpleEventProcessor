using System;
using System.Threading;
using AgileCode.SimpleEventProcessor.Interfaces;
using AgileCode.SimpleEventProcessor.Messages;
using AgileCode.SimpleEventProcessor.Processors;
using AgileCode.SimpleEventProcessor.Tests.Adapters;
using AgileCode.SimpleEventProcessor.Tests.Helper;
using Moq;
using NUnit.Framework;

namespace AgileCode.SimpleEventProcessor.Tests
{
    [TestFixture]
    public class SimpleEventProcessorRunnerTests
    {
        [Test]
        public void CannotStartProcessorWithoutAnAdapter()
        {
            Assert.Throws(typeof(ArgumentNullException), () =>
            {
                var eventSystem = SimpleEventProcessorRunner.CreateNew(null, null, null);
                eventSystem.Start();
            });
        }

        [Test]
        public void CanStartProcessor()
        {
            var inputAdapter = new Mock<IInputAdapter>();
            inputAdapter.SetupAllProperties();

            inputAdapter.Object.IsRunning = false;
            inputAdapter.Setup(action => action.Start()).Callback(() => inputAdapter.Object.IsRunning = true);

            var eventSystem = new SimpleEventProcessorRunner(new QueuedEventProcessor(null, null, null, 1), inputAdapter.Object, new ConsoleLogger());

            eventSystem.Start();

            var isAdapterRunning = false;
            int i = 0;
            while (isAdapterRunning == false && i < 10)
            {
                isAdapterRunning = inputAdapter.Object.IsRunning;
                Thread.Sleep(100);
            }

            Assert.True(isAdapterRunning);
        }

        [Test(Description = "Checks that starting and stopping the EventSystem actually start/stop the adapter too.")]
        public void CanStopProcesso2r()
        {
            var inputAdapter = new StoppingAdapter();

            var eventSystem = new SimpleEventProcessorRunner(new QueuedEventProcessor(null, null, null, 1), inputAdapter, new ConsoleLogger());

            eventSystem.Start();
            eventSystem.Stop();
            Thread.Sleep(50);
            var isAdapterRunning = true;
            int i = 0;
            while (isAdapterRunning == true && i < 10)
            {
                isAdapterRunning = inputAdapter.IsRunning;
                Thread.Sleep(100);
            }

            Assert.False(isAdapterRunning);
        }

        [Test]
        [Explicit]
        public void CanProcessAnEvent()
        {

            AdapterMock adapterMock = new AdapterMock();
            OutputAdapterMock outputAdapterMock = new OutputAdapterMock();
            var eh = new QueuedEventProcessor(null, outputAdapterMock, null, 1);
            var eventSystem = SimpleEventProcessorRunner.CreateNew(eh, adapterMock, new ConsoleLogger());
            eventSystem.Start();

            for (int i = 0; i < 10; i++)
            {
                adapterMock.Enqueue(new Event(Guid.NewGuid().ToString(), i.ToString()));
            }
            Thread.Sleep(1000);

            eventSystem.Stop();
        }
    }
}