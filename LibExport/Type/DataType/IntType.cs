using Newtonsoft.Json.Linq;

namespace LibExport.Type
{
    public class IntType : IType
    {
        public JToken ToJson(string value)
        {
            if (!string.IsNullOrWhiteSpace(value))
            {
                return new JValue((long)double.Parse(value));
            }
            else
            {
                return new JValue(0);
            }
        }
    }
}
