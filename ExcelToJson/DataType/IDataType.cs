using Newtonsoft.Json.Linq;

namespace ExcelToJson
{
    public interface IDataType
    {
        JToken ToJson(string strVal, bool require);
    }

}
