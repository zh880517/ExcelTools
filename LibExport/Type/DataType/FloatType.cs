using Newtonsoft.Json.Linq;

namespace LibExport.Type
{
    public class FloatType : IType
    {
        public JToken ToJson(string value)
        {
            if (!string.IsNullOrWhiteSpace(value))
            {
                return new JValue(double.Parse(value));
            }
            else
            {
                return new JValue(0);
            }
        }
    }
}
