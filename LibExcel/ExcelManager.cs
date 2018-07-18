using System.Collections.Generic;
using System.IO;

namespace LibExcel
{
    public static class ExcelManager
    {
        private static Dictionary<string, ExcelData> cache = new Dictionary<string, ExcelData>();
        private static bool isInit = false;
        

        public static ExcelData GetExcel(string filePath, StringMatch sheetNameMatch = null)
        {
            if (!isInit)
            {
                System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                isInit = true;
            }
            filePath = Path.GetFullPath(filePath);
            if (!cache.ContainsKey(filePath))
            {
                ExcelData excel = new ExcelData(filePath);
                if (sheetNameMatch != null)
                {
                    excel.ReadAll(sheetNameMatch);
                }
                cache.Add(excel.FullPath, excel);
            }
            return cache[filePath];
        }
        
    }
}
