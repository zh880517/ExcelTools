using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace LibExport.Type
{
    public class StructProperty
    {
        public string Name;
        public IType Type; 
    }

    public class StructType : IType
    {
        public string Sep;
        public List<StructProperty> Properties = new List<StructProperty>();

        public JToken ToJson(string value)
        {
            JObject obj = new JObject();
            if (!string.IsNullOrWhiteSpace(value))
            {
                var result = value.Split(Sep);
                for (int i=0; i<Properties.Count; ++i)
                {
                    string val = null;
                    if (result.Length > i)
                        val = result[i];
                    var property = Properties[i];
                    obj.Add(property.Name, property.Type.ToJson(val));
                }
            }
            return obj;
        }
    }
}
