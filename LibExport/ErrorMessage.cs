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

//         public static string XmlLineInfor(this XElement xml)
//         {
//             return string.Format("in line{0}, ", xml.LineNumber, );
//         }
    }
}
