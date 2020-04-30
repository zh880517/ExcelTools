using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace ExcelToJson
{
    public class StructExportType : IExportType
    {
        public struct Field
        {
            public string Name;
            public bool EmptySkip;
            public IExportType Type;
        }
        public char Separator;
        public List<Field> Fields = new List<Field>();
        public bool EmptySkip;
        public JToken Export(string strVal)
        {
            if (string.IsNullOrEmpty(strVal) && EmptySkip)
                return null;
            JObject jObject = new JObject();
            var results = strVal.Split(Separator);
            for (int i=0; i<Fields.Count; ++i)
            {
                string res = string.Empty;
                if (i < results.Length)
                {
                    res = results[i];
                }
                var field = Fields[i];
                if (string.IsNullOrEmpty(res) && field.EmptySkip)
                    continue;
                var token = field.Type.Export(res);
                jObject[field.Name] = token;
            }
            return jObject;
        }
    }
}
