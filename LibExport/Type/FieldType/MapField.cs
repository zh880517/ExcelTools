using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json.Linq;

namespace LibExport.Type
{
    public class MapField : MapType, IFieldType
    {
        public string Key;
        public string Name;
        public JProperty ToJson(JToken value)
        {
            return new JProperty(Name, ToJson((string)value[Key]));
        }
    }
}
