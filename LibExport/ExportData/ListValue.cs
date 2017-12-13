using System;
using System.Collections.Generic;
using System.Text;

namespace LibExport
{
    public class ListValue : IValue
    {
        protected List<IValue> values = new List<IValue>();

        public void Add(IValue value)
        {
            values.Add(value);
        }

        public string ToJson(int tableNum)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append('[');
            bool needNewLine = values.Exists(obj => !(obj is StringValue || obj is NumberValue || obj is BoolValue));
            foreach (var val in values)
            {
                if (needNewLine)
                    sb.AppendLine().AppendTable(tableNum + 1);
                sb.Append(val.ToJson(tableNum+1));
                sb.Append(',');
            }
            sb.AppendLine();
            sb.AppendTable(tableNum);
            sb.Append(']');
            return sb.ToString();
        }

        public string ToLua(int tableNum)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append('{');
            bool needNewLine = values.Exists(obj => !(obj is StringValue || obj is NumberValue || obj is BoolValue));
            foreach (var val in values)
            {
                if (needNewLine)
                    sb.AppendLine().AppendTable(tableNum + 1);
                sb.Append(val.ToJson(tableNum + 1));
                sb.Append(',');
            }
            sb.AppendLine();
            sb.AppendTable(tableNum);
            sb.Append('}');
            return sb.ToString();
        }
    }
}
