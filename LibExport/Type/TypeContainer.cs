using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibExport.Type
{
    public class TypeContainer
    {
        private readonly Dictionary<string, IType> types = new Dictionary<string, IType>();
        public IType FindType(string type)
        {
            if (types.TryGetValue(type, out IType dataType))
                return dataType;
            else
                return null;
        }

        public bool AddType(string name, IType type)
        {
            if (!types.ContainsKey(name))
                return false;
            types.Add(name, type);
            return true;
        }
    }
}
