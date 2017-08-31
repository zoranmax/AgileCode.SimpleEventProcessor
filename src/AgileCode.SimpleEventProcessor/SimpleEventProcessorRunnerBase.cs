using System;
using System.Threading;
using AgileCode.SimpleEventProcessor.Interfaces;
using AgileCode.SimpleEventProcessor.Logging;

namespace AgileCode.SimpleEventProcessor
{

    public abstract class SimpleEventProcessorRunnerBase
    {
        protected readonly IInputAdapter InputAdapter;
        protected readonly IEventProcessor EventProcessor;
        protected readonly ILogger Logger;

        protected SimpleEventProcessorRunnerBase(IEventProcessor eventProcessor, IInputAdapter inputAdapter, ILogger logger)
        {
            InputAdapter = inputAdapter;
            EventProcessor = eventProcessor;
            Logger = logger;

            if (EventProcessor == null)
            {
                throw new ArgumentNullException("eventProcessor",
                    "EventProcessor should be specified in order to run the EventSystem");
            }
            if (InputAdapter == null)
            {
                throw new ArgumentNullException("inputAdapter",
                    "InputAdapter should be specified in order to run the EventSystem");
            }
        }

        protected virtual bool IsStarting { get; set; }
        protected virtual bool IsStopping { get; set; }

        public void Start()
        {
            if (InputAdapter == null)
            {
                var message = "Cannot start the EventSystem without specifying an InputAdapter";
                LogHelper.LogError(Logger, LogMessage.GetLogMessage(message, GetType()));
                throw new ApplicationException(message);
            }

            if (IsStopping)
            {
                var message = "The EventSystem is currently stopping, cannot start";
                LogHelper.LogError(Logger, LogMessage.GetLogMessage(message, GetType()));
                throw new ApplicationException(message);
            }

            IsStarting = true;

            LogHelper.LogDebug(Logger, LogMessage.GetLogMessage("Starting EventSystem...", GetType()));

            if (InputAdapter == null)
            {
                var message = "EventSystem cannot start as there are no configured adapters";
                LogHelper.LogError(Logger, LogMessage.GetLogMessage(message, GetType()));
                throw new ApplicationException(message);
            }

            StartAdapter();
            //StartProcessors();

            IsRunning = true;
            IsStarting = false;

            LogHelper.LogDebug(Logger,
                LogMessage.GetLogMessage("EventSystem started.",
                    GetType()));
        }

        public void Stop()
        {
            if (IsStarting)
            {
                throw new ApplicationException(
                    "EventProcessor is is currently in the process of starting. Retry later.");
            }

            IsStopping = true;
            LogHelper.LogDebug(Logger, LogMessage.GetLogMessage("Stopping EventSystem...", GetType()));

            StopAdapter();
            //StopProcessors();

            IsRunning = false;
            IsStopping = false;
            LogHelper.LogDebug(Logger, LogMessage.GetLogMessage("EventSystem stopped.", GetType()));
        }

        public bool IsRunning { get; protected set; }

        protected virtual void StartAdapter()
        {
            LogHelper.LogDebug(Logger, LogMessage.GetLogMessage("Starting adapter...", GetType()));

            if (!InputAdapter.IsRunning)
            {
                InputAdapter.Start();
            }

            LogHelper.LogDebug(Logger, LogMessage.GetLogMessage("Adapter started.", GetType()));
        }

        protected virtual void StopAdapter()
        {
            InputAdapter.Stop();
            //waiting for the adapter to fully stop before continuing
            var message = "In the loop: Waiting for 100 milliseconds for the input adapter to stop...";
            while (InputAdapter.IsRunning)
            {
                LogHelper.LogDebug(Logger, LogMessage.GetLogMessage(message, GetType()));
                Thread.Sleep(100);
            }
        }
    }
}