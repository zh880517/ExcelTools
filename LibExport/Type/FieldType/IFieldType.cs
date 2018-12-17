using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json.Linq;

namespace LibExport.Type
{
    public interface IFieldType
    {
        JProperty ToJson(JToken value);
    }
}
