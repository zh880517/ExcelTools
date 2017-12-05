using System.Collections.Generic;
using System.Xml.Linq;

namespace LibExport
{
    public class StructEntity
    {
        protected string name;
        protected string sep;
        protected List<StructFieldEntity> fields = new List<StructFieldEntity>();

        public string Name { get { return name; } }
        public string Sep { get { return sep; } }
        public List<StructFieldEntity> Fields { get { return fields; } }

        public bool FromXml(XElement xml)
        {
            name = (string)xml.Attribute("name");
            sep = (string)xml.Attribute("sep");
            if (string.IsNullOrWhiteSpace(name))
            {
                ErrorMessage.Error("{0}属性缺少或者为空 : {1}", "name", xml.ToString());
                return false;
            }
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
                else if (el.Name == "exten")
                {
                    field = new StructExtenFieldEntity();
                }
                else
                {
                    continue;
                }
                field.SetParent(this);
                if (!field.FromXml(el))
                {
                    ErrorMessage.Error("解析 Struct:{0}时出错", name);
                    return false;
                }
                if (fields.Exists(obj=>obj.Name == field.Name))
                {
                    ErrorMessage.Error("解析 Struct:{0}时出错 : 有相同名字 {1} 子节点 {2}", name, field.Name, xml.ToString());
                    return false;
                }
                fields.Add(field);
            }
            if (fields.Count == 0)
            {
                ErrorMessage.Error("解析 Struct:{0}时出错:缺少field", name);
                return false;
            }
            return true;
        }
    }
}
