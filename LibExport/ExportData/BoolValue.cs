namespace LibExport
{
    public class BoolValue : IValue
    {
        protected bool value;

        public BoolValue(bool value)
        {
            this.value = value;
        }

        public string ToJson(int tableNum)
        {
            return value ? "true" : "false";
        }

        public string ToLua(int tableNum)
        {
            return value ? "true" : "false";
        }
    }
}
