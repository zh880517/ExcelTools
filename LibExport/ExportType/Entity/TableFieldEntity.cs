using System.Linq;
using System.Xml.Linq;

namespace LibExport
{
    public class TableFieldEntity
    {
        protected TableEntity parent;
        public string Name { get; protected set; }
        public TableEntity Parent { get { return parent; } }

        public void SetParent(TableEntity parent)
        {
            if (this.parent == null)
                this.parent = parent;
        }

        internal virtual bool FromXml(XElement xml)
        {
            Name = (string)xml.Attribute("name");
            if (string.IsNullOrWhiteSpace(Name))
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
        public string Sep { get; protected set; }

        internal override bool FromXml(XElement xml)
        {
            if (base.FromXml(xml))
            {
                Sep = (string)xml.Attribute("sep");
                if (string.IsNullOrEmpty(Sep))
                {
                    ErrorMessage.Error("{0}属性缺少或者为空 : {1}", "sep", xml.ToString());
                    return false;
                }
                if (Sep.Length == 1)
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
        public string Type { get; protected set; }
        public string[] ItemFields { get; protected set; }

        internal override bool FromXml(XElement xml)
        {
            if (base.FromXml(xml))
            {
                Type = (string)xml.Attribute("type");
                var elements = xml.Elements();
                ItemFields = elements.Where(obj=>obj.Name == "item" && obj.Attribute("index") != null).Select(obj => (string)obj.Attribute("index")).ToArray();
                if (string.IsNullOrWhiteSpace(Type))
                {
                    ErrorMessage.Error("{0}属性缺少或者为空 : {1}", "type", xml.ToString());
                    return false;
                }
                if (ItemFields.Length == 0)
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
        public bool IgnoreNull { get; protected set; }
        internal override bool FromXml(XElement xml)
        {
            if (base.FromXml(xml))
            {
                IgnoreNull = (bool)xml.Attribute("ignorenull");
                return true;
            }
            return false;
        }
    }

    public class TableMapFieldEntity : TableFieldEntity
    {
        public string Index { get; protected set; }
        public string KeyType { get; protected set; }
        public string ValueType { get; protected set; }
        public string Sep { get; protected set; }
        public string KvSep { get; protected set; }

        internal override bool FromXml(XElement xml)
        {
            if (base.FromXml(xml))
            {
                Index = (string)xml.Attribute("index");
                KeyType = (string)xml.Attribute("keytype");
                ValueType = (string)xml.Attribute("valuetype");
                Sep = (string)xml.Attribute("sep");
                KvSep = (string)xml.Attribute("kvsep");
                if (string.IsNullOrWhiteSpace(Index))
                {
                    ErrorMessage.Error("{0}属性缺少或者为空 : {1}", "index", xml.ToString());
                    return false;
                }
                if (string.IsNullOrWhiteSpace(KeyType))
                {
                    ErrorMessage.Error("{0}属性缺少或者为空 : {1}", "keytype", xml.ToString());
                    return false;
                }
                if (string.IsNullOrWhiteSpace(ValueType))
                {
                    ErrorMessage.Error("{0}属性缺少或者为空 : {1}", "valuetype", xml.ToString());
                    return false;
                }
                if (string.IsNullOrEmpty(Sep))
                {
                    ErrorMessage.Error("{0}属性缺少或者为空 : {1}", "sep", xml.ToString());
                    return false;
                }
                if (string.IsNullOrEmpty(KvSep))
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
        public string Type { get; protected set; }
        internal override bool FromXml(XElement xml)
        {
            if (base.FromXml(xml))
            {
                Type = (string)xml.Attribute("type");
                if (string.IsNullOrWhiteSpace(Type))
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
        public string Index { get; protected set; }
        public string Type { get; protected set; }
        public TableEntity Parent { get; protected set; }

        public void SetParent(TableEntity parent)
        {
            if (Parent == null)
                Parent = parent;
        }

        internal bool FromXml(XElement xml)
        {
            Type = (string)xml.Attribute("type");
            Index = (string)xml.Attribute("index");
            if (string.IsNullOrWhiteSpace(Type))
            {
                ErrorMessage.Error("{0}属性缺少或者为空 : {1}", "type", xml.ToString());
                return false;
            }
            if (string.IsNullOrWhiteSpace(Index))
            {
                ErrorMessage.Error("{0}属性缺少或者为空 : {1}", "index", xml.ToString());
                return false;
            }
            return true;
        }
    }
}
