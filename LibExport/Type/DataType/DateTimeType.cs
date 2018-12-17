using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json.Linq;

namespace LibExport.Type
{
    public class DateTimeType : IType
    {
        public JToken ToJson(string value, bool allowNull)
        {
            DateTime date = new DateTime();
            if (!string.IsNullOrWhiteSpace(value) )
            {
                DateTime.TryParse(value, out date);
            }
            return new JValue(date);
        }
    }
}
