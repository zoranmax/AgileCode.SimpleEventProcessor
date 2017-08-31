namespace AgileCode.SimpleEventProcessor.Interfaces
{
    public interface ICanStartAndStop
    {
        /// <summary>
        /// Is the adapter currently active.
        /// </summary>
        bool IsRunning { get; set; }

        /// <summary>
        /// The running tasks have the possibility to pause the input, if the memory queue gets too big!
        /// </summary>
        bool IsPaused { get; set; }

        /// <summary>
        /// Starts the Input Adapter.
        /// </summary>
        void Start();

        /// <summary>
        /// Stops the Input Adapter.
        /// </summary>
        void Stop();

        /// <summary>
        ///  Pause the Adapter without stopping (meaning stop reading from queue)
        /// </summary>
        void Pause();

        /// <summary>
        ///  UnPause the Adapter after a Pause (meaning restart reading from queue)
        /// </summary>
        void UnPause();
    }
}