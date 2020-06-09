using System;
using Newtonsoft.Json.Linq;

namespace ExcelToJson
{

    public class NumberType : IDataType
    {
        public JToken ToJson(string strVal, bool require)
        {
            if (!string.IsNullOrEmpty(strVal))
            {
                return double.Parse(strVal);
            }
            return require ? new JValue(0) : null;
        }
    }

    public class BoolenType : IDataType
    {
        public JToken ToJson(string strVal, bool require)
        {
            if (strVal == "1" || strVal == "true")
                return true;
            return require ? new JValue(false) : null;
        }
    }

    public class StringType : IDataType
    {
        public JToken ToJson(string strVal, bool require)
        {
            if (!string.IsNullOrEmpty(strVal))
                return strVal;
            return require ? new JValue("") : null;
        }
    }

    public class DateTimeType : IDataType
    {
        public JToken ToJson(string strVal, bool require)
        {
            if (!string.IsNullOrEmpty(strVal))
                return DateTime.Parse(strVal);
            return require ? new JValue(DateTime.MinValue) : null;
        }
    }
}
