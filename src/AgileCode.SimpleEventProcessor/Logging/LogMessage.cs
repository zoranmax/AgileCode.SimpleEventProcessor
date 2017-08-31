using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AgileCode.SimpleEventProcessor.Messages;

namespace AgileCode.SimpleEventProcessor.Logging
{
    public class LogMessage
    {
        public LogMessage()
        {
            Properties = new List<NameValue>();
        }

        public LogMessage(string message)
        {
            Message = message;
        }

        public LogMessage(string requestId, string message, List<NameValue> properties = null)
        {
            RequestId = requestId;
            Message = message;
            Properties = properties;
        }

        public static LogMessage GetLogMessage(string message,
            Type type,
            [System.Runtime.CompilerServices.CallerMemberName] string memberName = "",
            [System.Runtime.CompilerServices.CallerFilePath] string sourceFilePath = "",
            [System.Runtime.CompilerServices.CallerLineNumber] int sourceLineNumber = 0)
        {
            var memberSignature = string.Format("{0}.{1}", type.ToString(), memberName);
            return new LogMessage
            {
                Message = message,
                MethodSignature = memberSignature
            };
        }

        public string MethodSignature
        {
            get
            {
                var method = Properties.FirstOrDefault(x => x.Name == "Method");
                if (method != null)
                {
                    return method.Value;
                }
                return null;
            }
            set
            {
                var method = Properties.FirstOrDefault(x => x.Name == "Method");
                if (method == null)
                {
                    Properties.Add(new NameValue("Method", value));
                }
                else
                {
                    Properties.Remove(method);
                    Properties.Add(new NameValue("Method", value));
                }                
            }
        }

        public string Message { get; set; }
        public string RequestId { get; set; }
        public List<NameValue> Properties { get; set; }
        public string GetCreditSuisseStructureFormat()
        {
            if (Properties != null)
            {
                var sb = new StringBuilder();
                sb.Append("[");
                var id = Properties.FirstOrDefault(x => x.Name == "ID");
                if (id != null)
                {
                    sb.Append(id.Value + " ");
                    Properties.Remove(id);
                }

                foreach (var item in Properties)
                {
                    sb.Append(string.Format(" {0}=\"{1}\"", item.Name, item.Value));
                }
                sb.Append("]");
                return sb.ToString();
            }
            return "-";
        }
    }
}