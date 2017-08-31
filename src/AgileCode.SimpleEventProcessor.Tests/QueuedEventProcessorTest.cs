using NUnit.Framework;
using AgileCode.SimpleEventProcessor.Processors;

namespace AgileCode.SimpleEventProcessor.Tests
{
    [TestFixture]
    public class QueuedEventProcessorTest
    {
        [Test]
        public void ProcessorReturnsFalseIfCapacityReached()
        {
            var capacity = 5;
            var numberOfEventsGreaterThanCapacity = 10; //random number bigger than the capacity.
            var numberOfEventsLessThanCapacity = 4; //random number less than the capacity.

            var processor = new QueuedEventProcessor(null, null, null, capacity);

            Assert.That(processor.CanProcessNewItems(numberOfEventsGreaterThanCapacity) == false);
            Assert.That(processor.CanProcessNewItems(numberOfEventsLessThanCapacity) == true);
            Assert.That(processor.CanProcessNewItems(capacity) == true);
        }
    }
}