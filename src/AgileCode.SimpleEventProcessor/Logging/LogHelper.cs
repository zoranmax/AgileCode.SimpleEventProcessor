using AgileCode.SimpleEventProcessor.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;


namespace AgileCode.SimpleEventProcessor.Logging
{
    public class LogHelper
    {
        public static void LogDebug(ILogger logger, LogMessage logMessage)
        {
            if (logger != null)
            {
                if (logMessage != null)
                {
                    AddParameters(logMessage, "Debug");
                }
                logger.Debug(logMessage);
            }
        }

        public static void LogError(ILogger logger, LogMessage logMessage)
        {
            if (logger != null)
            {
                if (logMessage != null)
                {
                    AddParameters(logMessage, "Exception");
                }
                logger.Error(logMessage);
            }
        }

        private static void AddParameters(LogMessage logMessage, string logType)
        {
            var list = new List<NameValue>
            {
                new NameValue("InvokeTime", XmlConvert.ToString(DateTime.Now, XmlDateTimeSerializationMode.Local)),
                new NameValue("Type", logType)
            };

            foreach (var parameter in list)
            {
                if (logMessage.Properties.FirstOrDefault(x => x.Name == parameter.Name) == null)
                {
                    logMessage.Properties.Add(parameter);
                }
            }
        }
    }
}