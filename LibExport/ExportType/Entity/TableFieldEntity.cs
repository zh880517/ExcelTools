using System.Linq;
using System.Xml.Linq;

namespace LibExport
{
    public class TableFieldEntity
    {
        protected string name;
        private TableEntity parent;
        public string Name { get { return name; } }
        public TableEntity Parent { get { return parent; } }

        public void SetParent(TableEntity parent)
        {
            if (this.parent == null)
                this.parent = parent;
        }

        public virtual bool FromXml(XElement xml)
        {
            name = (string)xml.Attribute("name");
            return string.IsNullOrWhiteSpace(name);
        }
    }

    public class TableBaseFieldEntity : TableFieldEntity
    {
        protected string index;
        protected string type;

        public string Index { get { return index; } }
        public string Type { get { return type; } }

        public override bool FromXml(XElement xml)
        {
            if (base.FromXml(xml))
            {
                index = (string)xml.Attribute("index");
                type = (string)xml.Attribute("type");
                return !string.IsNullOrWhiteSpace(index) && !string.IsNullOrWhiteSpace(type);
            }
            return false;
        }
    }
    public class TableListFieldEntity : TableBaseFieldEntity
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

    public class TableMixFieldEntity : TableFieldEntity
    {
        protected string type;
        protected string[] itemFields;
        public string Type { get { return type; } }
        public string[] ItemFields { get { return itemFields; } }

        public override bool FromXml(XElement xml)
        {
            if (base.FromXml(xml))
            {
                type = (string)xml.Attribute("type");
                var elements = xml.Elements();
                itemFields = elements.Where(obj=>obj.Name == "item" && obj.Attribute("index") != null).Select(obj => (string)obj.Attribute("index")).ToArray();
                return !string.IsNullOrWhiteSpace(type) && itemFields.Length > 0;
            }
            return false;
        }
    }

    public class TableMixListField : TableMixFieldEntity
    {
        private bool ignoreNull;
        public bool IgnoreNull { get { return ignoreNull; } }
        public override bool FromXml(XElement xml)
        {
            if (base.FromXml(xml))
            {
                ignoreNull = (bool)xml.Attribute("ignorenull");
                return true;
            }
            return false;
        }
    }

    public class TableMapField : TableFieldEntity
    {
        private string index;
        private string keyType;
        private string valueType;
        private string sep;
        private string kvSep;

        public string Index { get { return index; } }
        public string KeyType { get { return keyType; } }
        public string ValueType { get { return valueType; } }
        public string Sep { get { return sep; } }
        public string KvSep { get { return kvSep; } }

        public override bool FromXml(XElement xml)
        {
            if (base.FromXml(xml))
            {
                index = (string)xml.Attribute("index");
                keyType = (string)xml.Attribute("keytype");
                valueType = (string)xml.Attribute("valuetype");
                sep = (string)xml.Attribute("sep");
                kvSep = (string)xml.Attribute("kvsep");
                return !string.IsNullOrWhiteSpace(index) 
                    && !string.IsNullOrWhiteSpace(keyType)
                    && !string.IsNullOrWhiteSpace(valueType)
                    && !string.IsNullOrEmpty(sep)
                    && !string.IsNullOrEmpty(kvSep)
                    && sep != kvSep;
            }
            return false;
        }
    }
}
