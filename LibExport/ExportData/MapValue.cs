using System.Collections.Generic;
using System.Text;

namespace LibExport
{
    public class MapValue : IValue
    {
        protected Dictionary<StringValue, IValue> values = new Dictionary<StringValue, IValue>();

        public void Add(StringValue key, IValue value)
        {
            values.Add(key, value);
        }

        public string ToJson(int tableNum)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("{");
            foreach (var kv in values)
            {
                sb.AppendLine().AppendFormat("\"{0}\" = ", kv.Key);
                sb.Append(kv.Value.ToJson(tableNum + 1)).Append(',');
            }
            sb.AppendLine();
            sb.AppendTable(tableNum);
            sb.Append('}');
            return sb.ToString();
        }

        public string ToLua(int tableNum)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("{");
            foreach (var kv in values)
            {
                sb.AppendLine().AppendFormat("[{0}] = ", kv.Key);
                sb.Append(kv.Value.ToJson(tableNum + 1)).Append(',');
            }
            sb.AppendLine();
            sb.AppendTable(tableNum);
            sb.Append('}');
            return sb.ToString();
        }
    }
}
