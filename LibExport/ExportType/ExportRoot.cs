using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;
using System.Xml.Linq;

namespace LibExport
{
    public class ExportRoot
    {
        private string fullfileName;
        protected List<StructEntity> structs = new List<StructEntity>();
        protected List<TableEntity> tables = new List<TableEntity>();

        public string FileName { get { return fullfileName; } }
        public List<StructEntity> Structs { get { return structs; } }
        public List<TableEntity> Tables { get { return tables; } }

        public bool FromXml(string fileName)
        {
            structs.Clear();
            tables.Clear();
            fullfileName = Path.GetFullPath(fileName);
            XDocument doc = XDocument.Load(fullfileName);
            var root = doc.Element("Root");
            var commonStruct = root.Element("CommonStruct");
            foreach (var el in commonStruct.Elements("Struct"))
            {
                StructEntity st = new StructEntity();
                if (!st.FromXml(el))
                {
                    ErrorMessage.Error("xml文件{0}", fullfileName);
                    return false;
                }
                if (structs.Exists(obj=>obj.Name == st.Name))
                {
                    ErrorMessage.Error("xml文件{0}含有同名的 Struct {1} {2}", fullfileName, st.Name, el.ToString());
                    return false;
                }
                structs.Add(st);
            }
            var table = root.Element("Table");
            foreach (var el in table.Elements())
            {
                TableEntity tb = null;
                if (el.Name == "Row")
                {
                    tb = new RowTableEntity();
                } 
                else if(el.Name == "Column")
                {
                    tb = new ColumnTableEntity();
                }
                else
                {
                    continue;
                }
                if (!tb.FromXml(el))
                {
                    ErrorMessage.Error("xml文件{0}", fullfileName);
                    return false;
                }
                if (tables.Exists(obj=>obj.Name == tb.Name))
                {
                    ErrorMessage.Error("xml文件{0}含有同名的 Table {1} {2}", fullfileName, tb.Name, el.ToString());
                    return false;
                }
                tables.Add(tb);
            }
            return true;
        }

        public StructEntity GetStruct(string name)
        {
            return structs.Find(obj => obj.Name == name);
        }

        public TableEntity GetTable(string name)
        {
            return tables.Find(obj => obj.Name == name);
        }

        public void SetExcelPath(string path)
        {
            path = Path.GetFullPath(path);
            foreach (var tb in tables)
            {
                tb.UpdateExcelPath(path);
            }
        }

        /// <summary>
        /// 获取需要读取的excel表的全路径和excel表需要读取的sheet
        /// 如果配置表和excel表不在同一个目录，需要先调用SetExcelPath设置excel表所在的目录
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, HashSet<string>> GetExcelInfor()
        {
            Dictionary<string, HashSet<string>> result = new Dictionary<string, HashSet<string>>();
            foreach (var tb in tables)
            {
                if (!result.TryGetValue(tb.Name, out HashSet<string> sheets))
                {
                    sheets = new HashSet<string>();
                    result.Add(tb.Name, sheets);
                }
                sheets.Add(tb.Sheet);
            }
            return result;
        }
    }
}
