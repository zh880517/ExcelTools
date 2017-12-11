using System.Linq;
using System.Xml.Linq;

namespace LibExport
{
    public class TableFieldEntity
    {
        protected string name;
        protected TableEntity parent;
        public string Name { get { return name; } }
        public TableEntity Parent { get { return parent; } }

        public void SetParent(TableEntity parent)
        {
            if (this.parent == null)
                this.parent = parent;
        }

        internal virtual bool FromXml(XElement xml)
        {
            name = (string)xml.Attribute("name");
            if (string.IsNullOrWhiteSpace(name))
            {
                ErrorMessage.Error("{0}属性缺少或者为空 : {1}", "name", xml.ToString());
                return false;
            }
            return true;
        }
    }

    public class TableBaseFieldEntity : TableFieldEntity
    {
        protected string index;
        protected string type;

        public string Index { get { return index; } }
        public string Type { get { return type; } }

        internal override bool FromXml(XElement xml)
        {
            if (base.FromXml(xml))
            {
                index = (string)xml.Attribute("index");
                type = (string)xml.Attribute("type");
                if (string.IsNullOrWhiteSpace(index))
                {
                    ErrorMessage.Error("{0}属性缺少或者为空 : {1}", "index", xml.ToString());
                    return false;
                }
                if (string.IsNullOrWhiteSpace(type))
                {
                    ErrorMessage.Error("{0}属性缺少或者为空 : {1}", "type", xml.ToString());
                    return false;
                }
            }
            return false;
        }
    }
    public class TableListFieldEntity : TableBaseFieldEntity
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

    public class TableMixFieldEntity : TableFieldEntity
    {
        protected string type;
        protected string[] itemFields;
        public string Type { get { return type; } }
        public string[] ItemFields { get { return itemFields; } }

        internal override bool FromXml(XElement xml)
        {
            if (base.FromXml(xml))
            {
                type = (string)xml.Attribute("type");
                var elements = xml.Elements();
                itemFields = elements.Where(obj=>obj.Name == "item" && obj.Attribute("index") != null).Select(obj => (string)obj.Attribute("index")).ToArray();
                if (string.IsNullOrWhiteSpace(type))
                {
                    ErrorMessage.Error("{0}属性缺少或者为空 : {1}", "type", xml.ToString());
                    return false;
                }
                if (itemFields.Length == 0)
                {
                    ErrorMessage.Error("缺少item子节点 : {1}", xml.ToString());
                    return false;
                }
            }
            return false;
        }
    }

    public class TableMixListFieldEntity : TableMixFieldEntity
    {
        protected bool ignoreNull;
        public bool IgnoreNull { get { return ignoreNull; } }
        internal override bool FromXml(XElement xml)
        {
            if (base.FromXml(xml))
            {
                ignoreNull = (bool)xml.Attribute("ignorenull");
                return true;
            }
            return false;
        }
    }

    public class TableMapFieldEntity : TableFieldEntity
    {
        protected string index;
        protected string keyType;
        protected string valueType;
        protected string sep;
        protected string kvSep;

        public string Index { get { return index; } }
        public string KeyType { get { return keyType; } }
        public string ValueType { get { return valueType; } }
        public string Sep { get { return sep; } }
        public string KvSep { get { return kvSep; } }

        internal override bool FromXml(XElement xml)
        {
            if (base.FromXml(xml))
            {
                index = (string)xml.Attribute("index");
                keyType = (string)xml.Attribute("keytype");
                valueType = (string)xml.Attribute("valuetype");
                sep = (string)xml.Attribute("sep");
                kvSep = (string)xml.Attribute("kvsep");
                if (string.IsNullOrWhiteSpace(index))
                {
                    ErrorMessage.Error("{0}属性缺少或者为空 : {1}", "index", xml.ToString());
                    return false;
                }
                if (string.IsNullOrWhiteSpace(keyType))
                {
                    ErrorMessage.Error("{0}属性缺少或者为空 : {1}", "keytype", xml.ToString());
                    return false;
                }
                if (string.IsNullOrWhiteSpace(valueType))
                {
                    ErrorMessage.Error("{0}属性缺少或者为空 : {1}", "valuetype", xml.ToString());
                    return false;
                }
                if (string.IsNullOrEmpty(sep))
                {
                    ErrorMessage.Error("{0}属性缺少或者为空 : {1}", "sep", xml.ToString());
                    return false;
                }
                if (string.IsNullOrEmpty(kvSep))
                {
                    ErrorMessage.Error("{0}属性缺少或者为空 : {1}", "kvsep", xml.ToString());
                    return false;
                }
            }
            return false;
        }
    }

    public class TableExtenFieldEntity : TableFieldEntity
    {
        protected string type;
        public string Type { get { return type; } }
        internal override bool FromXml(XElement xml)
        {
            if (base.FromXml(xml))
            {
                type = (string)xml.Attribute("type");
                if (string.IsNullOrWhiteSpace(type))
                {
                    ErrorMessage.Error("{0}属性缺少或者为空 : {1}", "type", xml.ToString());
                    return false;
                }
            }
            return false;
        }
    }

    public class TableKeyFieldEntity
    {
        protected string index;
        protected string type;
        protected TableEntity parent;
        public string Index { get { return index; } }
        public string Type { get { return type; } }
        public TableEntity Parent { get { return parent; } }

        public void SetParent(TableEntity parent)
        {
            if (this.parent == null)
                this.parent = parent;
        }

        internal bool FromXml(XElement xml)
        {
            type = (string)xml.Attribute("type");
            index = (string)xml.Attribute("index");
            if (string.IsNullOrWhiteSpace(type))
            {
                ErrorMessage.Error("{0}属性缺少或者为空 : {1}", "type", xml.ToString());
                return false;
            }
            if (string.IsNullOrWhiteSpace(index))
            {
                ErrorMessage.Error("{0}属性缺少或者为空 : {1}", "index", xml.ToString());
                return false;
            }
            return true;
        }
    }
}
