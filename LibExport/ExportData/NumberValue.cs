namespace LibExport
{
    public class NumberValue : IKeyType
    {
        private string value;
        public string Value { get { return value; } }
        public NumberValue(string value)
        {
            this.value = value;
        }

        public string ToJson(int tableNum)
        {
            return value;
        }

        public string ToLua(int tableNum)
        {
            return value;
        }

        public string ToJsonKey()
        {
            return string.Format("\"{0}\"", value);
        }

        public string ToLuaKey()
        {
            return string.Format("[{0}]", value);
        }
    }
}
