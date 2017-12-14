using System.Collections.Generic;
using System.Text;

namespace LibExport
{
    public class MapValue : IValue
    {
        protected Dictionary<IKeyType, IValue> values = new Dictionary<IKeyType, IValue>();

        public void Add(IKeyType key, IValue value)
        {
            values.Add(key, value);
        }

        public string ToJson(int tableNum)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("{");
            int count = 0;
            foreach (var kv in values)
            {
                sb.NewLine();
                sb.AppendTable(tableNum + 1);
                sb.AppendFormat("{0} : ", kv.Key.ToJsonKey());
                sb.Append(kv.Value.ToJson(tableNum + 1));
                if (++count < values.Count)
                    sb.Append(',');
            }
            sb.NewLine();
            sb.AppendTable(tableNum);
            sb.Append('}');
            return sb.ToString();
        }

        public string ToLua(int tableNum)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("{");
            int count = 0;
            foreach (var kv in values)
            {
                sb.NewLine();
                sb.AppendTable(tableNum + 1);
                sb.AppendFormat("{0} = ", kv.Key.ToLuaKey());
                sb.Append(kv.Value.ToLua(tableNum + 1));
                if (++count < values.Count)
                    sb.Append(',');
            }
            sb.NewLine();
            sb.AppendTable(tableNum);
            sb.Append('}');
            return sb.ToString();
        }
    }
}
