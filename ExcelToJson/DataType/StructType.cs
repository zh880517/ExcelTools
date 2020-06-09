using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.IO;

namespace ExcelToJson
{
    public class StructType : IDataType
    {
        public struct Field
        {
            public string Name;
            public bool Require;
            public IDataType Type;
        }
        public char Separator;
        public List<Field> Fields = new List<Field>();
        public JToken ToJson(string strVal, bool require)
        {
            if (!require && string.IsNullOrEmpty(strVal))
                return null;
            JObject jObject = new JObject();
            var results = strVal.Split(Separator);
            for (int i=0; i<Fields.Count; ++i)
            {
                string res = i < results.Length ? results[i] : null;
                var field = Fields[i];
                var token = field.Type.ToJson(res, field.Require);
                if (token != null)
                    jObject[field.Name] = token;
            }
            return jObject;
        }
    }
}
