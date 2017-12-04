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

        public virtual bool FromXml(XElement xml)
        {
            name = (string)xml.Attribute("name");
            type = (string)xml.Attribute("type");
            return string.IsNullOrWhiteSpace(name) && !string.IsNullOrWhiteSpace(type);
        }
    }

    public class StructBaseFieldEntity : StructFieldEntity
    {
    }

    public class StructListFieldEntity : StructFieldEntity
    {
        protected string sep;
        public string Sep { get { return sep; } }

        public override bool FromXml(XElement xml)
        {
            if (base.FromXml(xml))
            {
                sep = (string)xml.Attribute("sep");
                return !string.IsNullOrEmpty(sep) && sep.Length == 1;
            }
            return false;
        }
    }

}
