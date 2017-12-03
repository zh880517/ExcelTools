using System.Collections.Generic;
using System.IO;

namespace LibExcel
{
    public static class ExcelManager
    {
        private static Dictionary<string, ExcelData> cache = new Dictionary<string, ExcelData>();

        public static ExcelData GetExcel(string filePath, bool loadAllSheet = false)
        {
            filePath = Path.GetFullPath(filePath);
            if (!cache.ContainsKey(filePath))
            {
                ExcelData excel = new ExcelData(filePath);
                if (loadAllSheet)
                {
                    excel.ReadAll();
                }
                cache.Add(excel.FullPath, excel);
            }
            return cache[filePath];
        }
    }
}
