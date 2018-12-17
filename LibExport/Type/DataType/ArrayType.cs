using Newtonsoft.Json.Linq;

namespace LibExport.Type
{
    public class ArrayType : IType
    {
        public string Sep;
        public string TypeName;
        public IType Type;
        public JToken ToJson(string value)
        {
            JArray array = new JArray();
            if (!string.IsNullOrWhiteSpace(value))
            {
                var result = value.Split(Sep);
                foreach (var val in result)
                {
                    array.Add(Type.ToJson(val));
                }
            }
            return array;
        }
        
    }
}
