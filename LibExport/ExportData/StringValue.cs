namespace LibExport
{
    public class StringValue : IKeyType
    {

        public string Value { get; private set; }

        public StringValue(string value)
        {
            Value = value;
        }

        public string ToJson(int tableNum)
        {
            return string.Format("\"{0}\"", Value);
        }

        public string ToLua(int tableNum)
        {
            return string.Format("\"{0}\"", Value);
        }

        public string ToJsonKey()
        {
            return string.Format("\"{0}\"", Value);
        }

        public string ToLuaKey()
        {
            return Value;
        }
    }
}
