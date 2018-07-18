namespace LibExport
{
    public class NumberValue : IKeyType
    {
        public string Value { get; private set; }
        public NumberValue(string value)
        {
            Value = value;
        }

        public string ToJson(int tableNum)
        {
            return Value;
        }

        public string ToLua(int tableNum)
        {
            return Value;
        }

        public string ToJsonKey()
        {
            return string.Format("\"{0}\"", Value);
        }

        public string ToLuaKey()
        {
            return string.Format("[{0}]", Value);
        }
    }
}
