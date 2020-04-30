using Newtonsoft.Json.Linq;

namespace ExcelToJson
{
    public interface IExportType
    {
        JToken Export(string strVal);
    }

}
