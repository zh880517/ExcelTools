using System;
using Newtonsoft.Json.Linq;

namespace ExcelToJson
{

    public class NumberExportType : IExportType
    {
        public JToken Export(string strVal)
        {
            double val = 0;
            if (!string.IsNullOrEmpty(strVal))
            {
                val = double.Parse(strVal);
            }
            return val;
        }
    }

    public class BoolenExportType : IExportType
    {
        public JToken Export(string strVal)
        {
            bool val = false;
            if (strVal == "1" || strVal == "true")
                val = true;
            return val;
        }
    }

    public class StringExportType : IExportType
    {
        public JToken Export(string strVal)
        {
            return strVal ?? "";
        }
    }

    public class DateTimeExportType : IExportType
    {
        public JToken Export(string strVal)
        {
            DateTime dateTime = DateTime.Parse(strVal);
            return dateTime;
        }
    }
}
