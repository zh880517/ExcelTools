using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibExport.Type
{
    public interface IType
    {
        JToken ToJson(string value);
    }
}
