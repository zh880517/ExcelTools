using System.Collections.Generic;
using System.Text;

namespace LibExport
{
    public static class ErrorMessage
    {
        private static List<string> msgList = new List<string>();

        public static void Error(string msg)
        {
            msgList.Add(msg);
        }

        public static void Error(string fmt, params object[] param)
        {
            msgList.Add(string.Format(fmt, param));
        }

        public static string GetError()
        {
            StringBuilder sb = new StringBuilder();
            for (int i= msgList.Count-1; i>=0; ++i)
            {
                sb.AppendLine(msgList[i]);
            }
            return sb.ToString();
        }

        public static StringBuilder AppendTable(this StringBuilder sb, int tableNum)
        {
            return sb.Append(' ', tableNum*4);
        }
        
        public static StringBuilder NewLine(this StringBuilder sb)
        {
            return sb.Append('\n');
        }

    }
}
