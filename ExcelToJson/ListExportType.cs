using Newtonsoft.Json.Linq;

namespace ExcelToJson
{
    public class ListExportType : IExportType
    {
        public char Separator;
        public IExportType ElementType;
        public JToken Export(string strVal)
        {
            JArray array = new JArray();
            if (!string.IsNullOrEmpty(strVal))
            {
                var results = strVal.Split(Separator);
                foreach (var res in results)
                {
                    var token = ElementType.Export(res);
                    if (token != null)
                        array.Add(token);
                }
            }
            return array;
        }
    }
}
