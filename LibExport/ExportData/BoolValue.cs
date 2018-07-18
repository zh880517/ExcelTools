namespace LibExport
{
    public class BoolValue : IValue
    {

        public bool Value { get; private set; }

        public BoolValue(bool value)
        {
            Value = value;
        }

        public string ToJson(int tableNum)
        {
            return Value ? "true" : "false";
        }

        public string ToLua(int tableNum)
        {
            return Value ? "true" : "false";
        }
    }
}
