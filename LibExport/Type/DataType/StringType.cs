using Newtonsoft.Json.Linq;

namespace LibExport.Type
{
    public class StringType : IType
    {
        public JToken ToJson(string value)
        {
            if (string.IsNullOrEmpty(value))
                value = null;
            return new JValue(value);
        }
    }
}
