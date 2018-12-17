using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json.Linq;

namespace LibExport.Type
{
    public class MapType :IType
    {
        public string Sep;
        public string SepKV;
        public IType KeyType;
        public IType ValueType;
        public JToken ToJson(string value)
        {
            JObject obj = new JObject();
            if (!string.IsNullOrWhiteSpace(value))
            {
                var result = value.Split(Sep);
                foreach (var kv in result)
                {
                    var splitResult = kv.Split(SepKV);
                    string key, val = null;
                    key = splitResult[0];
                    if (splitResult.Length > 1)
                        val = splitResult[1];
                    obj.Add((string)KeyType.ToJson(key), ValueType.ToJson(val));
                }
            }
            return obj;
        }
    }
}
