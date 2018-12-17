using Newtonsoft.Json.Linq;

namespace LibExport.Type
{
    public class StructField : IFieldType
    {
        public string Key;
        public string Name;
        public IType Type;
        public JProperty ToJson(JToken value)
        {
            return new JProperty(Name, Type.ToJson((string)value[Key]));
        }
    }
}
