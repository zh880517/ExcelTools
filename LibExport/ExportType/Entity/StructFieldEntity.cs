using System.Xml.Linq;

namespace LibExport
{
    public class StructFieldEntity
    {
        protected string name;
        protected string type;
        private StructEntity parent;
        public string Name { get { return name; } }
        public string Type { get { return type; } }
        public StructEntity Parent { get { return parent; } }

        public void SetParent(StructEntity parent)
        {
            if (this.parent == null)
                this.parent = parent;
        }

        internal virtual bool FromXml(XElement xml)
        {
            name = (string)xml.Attribute("name");
            type = (string)xml.Attribute("type");
            if (string.IsNullOrWhiteSpace(name))
            {
                ErrorMessage.Error("{0}属性缺少或者为空 : {1}","name", xml.ToString());
                return false;
            }
            if (string.IsNullOrWhiteSpace(type))
            {
                ErrorMessage.Error("{0}属性缺少或者为空 : {1}", "type", xml.ToString());
                return false;
            }
            return true;
        }
    }

    public class StructExtenFieldEntity : StructFieldEntity
    {

    }

    public class StructBaseFieldEntity : StructFieldEntity
    {
    }

    public class StructListFieldEntity : StructFieldEntity
    {
        protected string sep;
        public string Sep { get { return sep; } }

        internal override bool FromXml(XElement xml)
        {
            if (base.FromXml(xml))
            {
                sep = (string)xml.Attribute("sep");
                if (string.IsNullOrEmpty(sep))
                {
                    ErrorMessage.Error("{0}属性缺少或者为空 : {1}", "sep", xml.ToString());
                    return false;
                }
                if (sep.Length == 1)
                {
                    ErrorMessage.Error("{0}属性只能是一个字符 : {1}", "sep", xml.ToString());
                    return false;
                }
            }
            return false;
        }
    }

}
