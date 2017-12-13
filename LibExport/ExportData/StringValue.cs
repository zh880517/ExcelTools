namespace LibExport
{
    public class StringValue :IValue
    {
        protected string value;

        public string Value { get { return value; } }

        public StringValue(string value)
        {
            this.value = value;
        }

        public string ToJson(int tableNum)
        {
            return string.Format("\"{0}\"", value);
        }

        public string ToLua(int tableNum)
        {
            return string.Format("\"{0}\"", value);
        }
    }
}
