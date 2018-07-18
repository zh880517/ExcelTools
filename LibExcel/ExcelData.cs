using ExcelDataReader;
using System.Collections.Generic;
using System.IO;

namespace LibExcel
{
    public class ExcelData
    {
        private Dictionary<string, SheetData> data = new Dictionary<string, SheetData>();

        public string FullPath { get; private set; }
        public string Name { get; private set; }

        internal ExcelData(string filePath)
        {
            FullPath = Path.GetFullPath(filePath);
            Name = Path.GetFileNameWithoutExtension(FullPath);
        }

        public SheetData GetSheetData(string sheetName)
        {
            if (!data.ContainsKey(sheetName))
                ReadSheet(sheetName);
            data.TryGetValue(sheetName, out SheetData result);
            return result;
        }

        public void ReadSheet(HashSet<string> sheets)
        {
            using (var stream = File.Open(FullPath, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    do
                    {
                        if (sheets.Contains(reader.Name) && !data.ContainsKey(reader.Name))
                        {
                            SheetData sheet = new SheetData(reader);
                            data.Add(sheet.Name, sheet);
                        }
                    } while (reader.NextResult());
                }
            }
        }

        public void ReadAll(StringMatch sheetNameMatch)
        {
            using (var stream = File.Open(FullPath, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    do
                    {
                        if (sheetNameMatch !=null && !sheetNameMatch.Match(reader.Name))
                            continue;
                        if (!data.ContainsKey(reader.Name))
                        {
                            SheetData sheet = new SheetData(reader);
                            data.Add(sheet.Name, sheet);
                        }
                    } while (reader.NextResult());
                }
            }
        }

        private void ReadSheet(string sheetName)
        {
            using (var stream = File.Open(FullPath, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    do 
                    {
                        if (reader.Name == sheetName)
                        {
                            SheetData sheet = new SheetData(reader);
                            data.Add(sheet.Name, sheet);
                            break;
                        }
                    } while (reader.NextResult());
                }
            }
        }
    }
}
