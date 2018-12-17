using Newtonsoft.Json.Linq;
using System;

namespace LibExport.Type
{
    public class BoolType : IType
    {
        public JToken ToJson(string value)
        {
            bool val = false;
            if (!string.IsNullOrWhiteSpace(value))
            {
                if (value == "1" || value.Equals("true", StringComparison.CurrentCultureIgnoreCase))
                {
                    val = true;
                }
            }
            return new JValue(val);
        }
    }
}
