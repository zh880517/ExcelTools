using Newtonsoft.Json.Linq;

namespace LibExport.Type
{
    public class DateTimeField : DateTimeType, IFieldType
    {
        public string Key;
        public string Name;
        public JProperty ToJson(JToken value)
        {
            return new JProperty(Name, ToJson((string)value[Key]));
        }
    }
}
