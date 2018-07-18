using System.Collections.Generic;
using System.Xml.Linq;

namespace LibExport
{
    public class StructEntity
    {
        protected List<StructFieldEntity> fields = new List<StructFieldEntity>();

        public string Name { get; private set; }
        public string Sep { get; private set; }
        public List<StructFieldEntity> Fields { get { return fields; } }

        internal bool FromXml(XElement xml)
        {
            Name = (string)xml.Attribute("name");
            Sep = (string)xml.Attribute("sep");
            if (string.IsNullOrWhiteSpace(Name))
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
                    ErrorMessage.Error("解析 Struct:{0}时出错", Name);
                    return false;
                }
                if (fields.Exists(obj=>obj.Name == field.Name))
                {
                    ErrorMessage.Error("解析 Struct:{0}时出错 : 有相同名字 {1} 子节点 {2}", Name, field.Name, xml.ToString());
                    return false;
                }
                fields.Add(field);
            }
            if (fields.Count == 0)
            {
                ErrorMessage.Error("解析 Struct:{0}时出错:缺少field", Name);
                return false;
            }
            return true;
        }
    }
}
