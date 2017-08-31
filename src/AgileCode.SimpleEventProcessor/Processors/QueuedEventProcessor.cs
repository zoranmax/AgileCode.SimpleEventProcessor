using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AgileCode.SimpleEventProcessor.Delegates;
using AgileCode.SimpleEventProcessor.Interfaces;
using AgileCode.SimpleEventProcessor.Logging;
using AgileCode.SimpleEventProcessor.Messages;

namespace AgileCode.SimpleEventProcessor.Processors
{
    /// <summary>
    /// Consumes the messages from the input adapter and enqueues into an internal queue. 
    /// It runs within its own thread.
    /// Coordinates the ActionHandler result and the IOutputAdapter.
    /// </summary>
    [Serializable]
    public class QueuedEventProcessor : IEventProcessor
    {
        protected BlockingCollection<Event> Queue;
        protected CancellationTokenSource TokenSource;
        protected CancellationToken Token;
        protected Task ConsumerTask;
        protected readonly IActionHandler ActionHandler;
        protected readonly IOutputAdapter OutputAdapter;
        public event EventsProcessedHandler EventsProcessed;
        protected ILogger Logger;

        public QueuedEventProcessor(IActionHandler actionHandler, IOutputAdapter outputAdapter, ILogger logger, long capacity = 1)
        {
            ActionHandler = actionHandler;
            OutputAdapter = outputAdapter;
            TokenSource = new CancellationTokenSource();
            Token = TokenSource.Token;
            Capacity = capacity;
            Logger = logger;
            Queue = new BlockingCollection<Event>();

            if (Capacity <= 0)
            {
                Capacity = 1;
            }

            ConsumerTask = Task.Run(async () => await ProcessEvents(), Token);
        }

        private readonly object _locker = new object();

        public Task Publish(List<Event> events)
        {
            lock (_locker)
            {
                if (events.Any())
                {
                    foreach (Event @event in events)
                    {
                        Queue.Add(@event, Token);
                    }
                }
                return Task.FromResult<object>(null);
            }
        }

        /// <summary>
        /// Decides if the new items can be processed or not
        /// </summary>
        /// <param name="numberOfNewItems"></param>
        /// <returns></returns>
        public bool CanProcessNewItems(int numberOfNewItems)
        {
            return Capacity == 0 || Capacity >= Queue.Count + numberOfNewItems;
        }

        /// <summary>
        /// Determines how many messages is this processor allowed to keep in memory!
        /// This is very important from the resilience perspective.
        /// </summary>
        public long Capacity { get; set; }

        protected virtual Task OnEventsProcessed(List<EventResult> data)
        {
            if (EventsProcessed != null)
            {
                return EventsProcessed(data);
            }
            return null;
        }

        protected virtual async Task ProcessEvents()
        {
            if (Token.IsCancellationRequested)
            {
                return;
            }

            foreach (var @event in Queue.GetConsumingEnumerable(Token))
            {
                var eventResult = await ProcessWithActionHandler(@event);
                await PublishToOutbound(eventResult);
            }
        }

        protected virtual async Task PublishToOutbound(EventResult eventResult)
        {
            try
            {
                //TODO: Retry 10 times.... and then drop...
                await OutputAdapter.Publish(eventResult);
            }
            catch (Exception exception)
            {
                LogHelper.LogError(Logger, LogMessage.GetLogMessage("Unhandled Exception: " + exception, GetType()));
            }
        }

        /// <summary>
        /// Takes care of any possible error that can happen in the ActionHandler and it prepares the EventResult.
        /// </summary>
        /// <param name="event"></param>
        /// <returns></returns>
        protected virtual async Task<EventResult> ProcessWithActionHandler(Event @event)
        {
            EventResult eventResult;
            try
            {
                eventResult = await ActionHandler.Process(@event);

                if (eventResult == null)
                {
                    /*extreme case where the action handler is not implemented properly*/
                    eventResult = new EventResult
                    {
                        Exception = new ExceptionResult(
                            "ActionHandler has returned null as the EventResult. Please correct the ActionHandler in order to always return a valid EventResult"),
                        Response = null,
                        OriginalEvent = @event
                    };
                }
            }
            catch (Exception exception)
            {
                LogHelper.LogError(Logger,
                    LogMessage.GetLogMessage(
                        "Handled Exception. ActionHandler returned an exception. This should be always avoided!: " +
                        exception, GetType()));
                /* 
                 * Another case where the ActionHandler simply doesn't work properly and it throws an error.
                 * ActionHandler should always handle the error message inside the Process method, but this obviously 
                 * sometimes might not work!
                 * */
                eventResult = new EventResult()
                {
                    Exception = new ExceptionResult(exception),
                    Response = null,
                    OriginalEvent = @event
                };
            }

            return eventResult;
        }
    }
}