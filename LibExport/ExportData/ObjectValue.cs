using System.Collections.Generic;
using System.Text;

namespace LibExport
{
    public class ObjectValue : IValue
    {
        protected Dictionary<string, IValue> fields = new Dictionary<string, IValue>();

        public void Add(string name, IValue value)
        {
            fields.Add(name, value);
        }

        public string ToJson(int tableNum)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("{");
            int count = 0;
            foreach (var kv in fields)
            {
                sb.NewLine().AppendTable(tableNum + 1);
                sb.AppendFormat("\"{0}\" : ", kv.Key);
                sb.Append(kv.Value.ToJson(tableNum + 1));
                if (++count < fields.Count)
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
            foreach (var kv in fields)
            {
                sb.NewLine().AppendTable(tableNum + 1);
                sb.AppendFormat("{0} = ", kv.Key);
                sb.Append(kv.Value.ToLua(tableNum + 1));
                if (++count < fields.Count)
                    sb.Append(',');
            };
            sb.NewLine();
            sb.AppendTable(tableNum);
            sb.Append('}');
            return sb.ToString();
        }
    }
}
