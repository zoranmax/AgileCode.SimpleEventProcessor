using System;

namespace AgileCode.SimpleEventProcessor.Messages
{
    public class ExceptionResult
    {
        public ExceptionResult()
        {

        }

        public ExceptionResult(string message)
        {
            Exception = message;
        }

        public ExceptionResult(Exception exception)
        {
            if (exception != null)
            {
                Exception = exception.ToString();
            }
        }

        public string Exception { get; set; }
    }
}