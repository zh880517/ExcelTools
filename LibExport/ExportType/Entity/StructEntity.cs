using System.Collections.Generic;
using System.Xml.Linq;

namespace LibExport
{
    public class StructEntity
    {
        private string name;
        private string sep;
        private List<StructFieldEntity> fields = new List<StructFieldEntity>();

        public string Name { get { return name; } }
        public string Sep { get { return sep; } }
        public List<StructFieldEntity> Fields { get { return fields; } }

        public bool FromXml(XElement xml)
        {
            name = (string)xml.Attribute("name");
            sep = (string)xml.Attribute("sep");
            foreach (var el in xml.Elements())
            {
                StructFieldEntity field = null;
                if (el.Name == "base")
                {
                    field = new StructBaseFieldEntity();
                }
                else if (el.Name == "list")
                {
                    field = new StructListFieldEntity();
                }
                if (field != null)
                {
                    field.SetParent(this);
                    if (field.FromXml(el))
                    {
                        fields.Add(field);
                        continue;
                    }
                    //处理错误消息
                }

            }
            return true;
        }
    }
}
