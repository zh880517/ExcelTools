using System.Collections.Generic;
using System.Xml.Linq;

namespace LibExport
{
    public class RowTableEntity : TableEntity
    {
        protected List<TableKeyFieldEntity> keys = new List<TableKeyFieldEntity>();
        public List<TableKeyFieldEntity> Keys { get { return keys; } }

        public override bool FromXml(XElement xml)
        {
            if (base.FromXml(xml))
            {
                foreach (var el in xml.Elements())
                {
                    if (el.Name == "key")
                    {
                        TableKeyFieldEntity key = new TableKeyFieldEntity();
                        if (!key.FromXml(xml))
                        {
                            ErrorMessage.Error("解析 Table:{0}时出错", name);
                            return false;
                        }
                        if (keys.Exists(obj=>obj.Index == key.Index))
                        {
                            ErrorMessage.Error("解析 Table:{0}时出错 : 有相同index {1} 的key节点 {2}", name, key.Index, xml.ToString());
                            return false;
                        }
                        keys.Add(key);
                    }
                }
            }
            return true;
        }

    }
}
