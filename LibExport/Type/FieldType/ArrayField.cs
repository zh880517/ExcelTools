using Newtonsoft.Json.Linq;

namespace LibExport.Type
{
    public class ArrayField : ArrayType, IFieldType
    {
        public string Key;
        public string Name;
        public int Start;
        public int End;
        public bool AllowEmpty;

        public JProperty ToJson(JToken value)
        {
            if (Start != 0 || End != 0)
            {
                JArray array = new JArray();
                for (int i=Start; i<=End; ++i)
                {
                    string key = string.Format("{0}_{1}", Key,i);
                    string val = (string)value[key];
                    if (AllowEmpty || !string.IsNullOrWhiteSpace(val))
                        array.Add(Type.ToJson(val));
                }
                return new JProperty(Name, array);
            }
            else
            {
                return new JProperty(Name, ToJson((string)value[Key]));
            }
        }
    }
}
