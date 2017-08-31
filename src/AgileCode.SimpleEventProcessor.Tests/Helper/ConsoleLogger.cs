using System;
using AgileCode.SimpleEventProcessor.Logging;

namespace AgileCode.SimpleEventProcessor.Tests.Helper
{
    public class ConsoleLogger : ILogger
    {
        public void Debug(LogMessage message)
        {
            Console.WriteLine("DEBUG - {1} - {0}", message.Message, message.GetCreditSuisseStructureFormat());
        }

        public void Info(LogMessage message)
        {
            Console.WriteLine("INFO - {1} - {0}", message.Message, message.GetCreditSuisseStructureFormat());
        }

        public void Warn(LogMessage message)
        {
            Console.WriteLine("WARN - {1} - {0}", message.Message, message.GetCreditSuisseStructureFormat());
        }

        public void Error(LogMessage message)
        {
            Console.WriteLine("ERROR - {1} - {0}", message.Message, message.GetCreditSuisseStructureFormat());
        }

        public void Fatal(LogMessage message)
        {
            Console.WriteLine("FATAL - {1} - {0}", message.Message, message.GetCreditSuisseStructureFormat());
        }
    }
}