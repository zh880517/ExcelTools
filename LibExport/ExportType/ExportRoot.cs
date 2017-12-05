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
            }
            return true;
        }
    }
}
