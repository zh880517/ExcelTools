using System.Xml.Linq;

namespace LibExport
{
    public class StructFieldEntity
    {
        public string Name { get; private set; }
        public string Type { get; private set; }
        public StructEntity Parent { get; private set; }

        public void SetParent(StructEntity parent)
        {
            if (Parent == null)
                Parent = parent;
        }

        internal virtual bool FromXml(XElement xml)
        {
            Name = (string)xml.Attribute("name");
            Type = (string)xml.Attribute("type");
            if (string.IsNullOrWhiteSpace(Name))
            {
                ErrorMessage.Error("{0}属性缺少或者为空 : {1}","name", xml.ToString());
                return false;
            }
            if (string.IsNullOrWhiteSpace(Type))
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
