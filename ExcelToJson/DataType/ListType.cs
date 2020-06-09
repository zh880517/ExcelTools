using Newtonsoft.Json.Linq;

namespace ExcelToJson
{
    public class ListType : IDataType
    {
        public char Separator;
        public IDataType ElementType;

        public JToken ToJson(string strVal, bool require)
        {
            if (!require && string.IsNullOrEmpty(strVal))
                return null;
            JArray array = new JArray();
            if (!string.IsNullOrEmpty(strVal))
            {
                var results = strVal.Split(Separator);
                foreach (var res in results)
                {
                    var token = ElementType.ToJson(res, true);
                    array.Add(token);
                }
            }
            return array;
        }
    }
}
