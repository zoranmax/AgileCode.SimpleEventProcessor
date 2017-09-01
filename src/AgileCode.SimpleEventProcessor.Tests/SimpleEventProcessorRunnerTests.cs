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
            var logger = new ConsoleLogger();
            inputAdapter.SetupAllProperties();

            inputAdapter.Object.IsRunning = false;
            inputAdapter.Setup(action => action.Start()).Callback(() => inputAdapter.Object.IsRunning = true);

            var eventSystem = 
                new SimpleEventProcessorRunner(new QueuedEventProcessor(null, null, logger, 1), inputAdapter.Object, logger);

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

        [Test(Description = "Checks that the processor can be stopped.")]
        public void CanStopProcessor()
        {
            var inputAdapter = new StoppingAdapter();
            var logger = new ConsoleLogger();
            var processor = new QueuedEventProcessor(null, null, logger, 1);

            var eventSystem = new SimpleEventProcessorRunner(processor, inputAdapter, logger);

            eventSystem.Start();
            Thread.Sleep(1000);
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
    }
}