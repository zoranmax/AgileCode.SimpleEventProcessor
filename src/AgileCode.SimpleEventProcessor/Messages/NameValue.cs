namespace AgileCode.SimpleEventProcessor.Messages
{
    public class NameValue
    {
        public NameValue()
        {

        }

        public NameValue(string name, string value)
        {
            Name = name;
            Value = value;
        }

        public string Name;
        public string Value;
    }
}