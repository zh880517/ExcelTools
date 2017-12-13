namespace LibExport
{
    public class NumberValue : StringValue
    {
        public NumberValue(string value) : base(value)
        {
        }

        public new string ToJson(int tableNum)
        {
            return value;
        }

        public new string ToLua(int tableNum)
        {
            return value;
        }
    }
}
