using System.Collections.Generic;
using System.Xml.Linq;

namespace LibExport
{
    public class TableEntity
    {
        protected string name;
        protected string excel;
        protected string sheet;
        protected int keyrow;
        protected int datarow;
        protected string reference;
        protected List<TableFieldEntity> fields = new List<TableFieldEntity>();

        public string Name { get { return name; } }
        public string Excel { get { return excel; } }
        public string Sheet { get { return sheet; } }
        public string Reference { get { return reference; } }
        public int KeyRow { get { return keyrow; } }
        public int DataRow { get { return datarow; } }

        public List<TableFieldEntity> Fields { get { return fields; } }

        public virtual bool FromXml(XElement xml)
        {
            name = (string)xml.Attribute("name");
            excel = (string)xml.Attribute("excel");
            sheet = (string)xml.Attribute("sheet");
            reference = (string)xml.Attribute("reference");
            keyrow = (int)xml.Attribute("keyrow");
            datarow = (int)xml.Attribute("datarow");
            if (string.IsNullOrWhiteSpace(name))
            {
                ErrorMessage.Error("{0}属性缺少或者为空 : {1}", "name", xml.ToString());
                return false;
            }
            if (string.IsNullOrWhiteSpace(excel))
            {
                ErrorMessage.Error("{0}属性缺少或者为空 : {1}", "excel", xml.ToString());
                return false;
            }
            if (string.IsNullOrEmpty(sheet))
            {
                ErrorMessage.Error("{0}属性缺少或者为空 : {1}", "sheet", xml.ToString());
                return false;
            }
            if (keyrow < 1)
            {
                ErrorMessage.Error("{0}属性缺少或者小于1 : {1}", "keyrow", xml.ToString());
                return false;
            }
            if (datarow < 1)
            {
                ErrorMessage.Error("{0}属性缺少或者小于1 : {1}", "keyrow", xml.ToString());
                return false;
            }
            if (datarow == keyrow)
            {
                ErrorMessage.Error("datarow不能等于keyrow : {0}", xml.ToString());
                return false;
            }
            foreach (var el in xml.Elements())
            {
                TableFieldEntity field = null;
                if (el.Name == "base")
                {
                    field = new TableBaseFieldEntity();
                } 
                else if (el.Name == "list")
                {
                    field = new TableListFieldEntity();
                }
                else if (el.Name == "mixlist")
                {
                    field = new TableMixFieldEntity();
                }
                else if (el.Name == "mix")
                {
                    field = new TableMixFieldEntity();
                }
                else if (el.Name == "map")
                {
                    field = new TableMapFieldEntity();
                }
                else if (el.Name == "exten")
                {
                    field = new TableExtenFieldEntity();
                }
                else
                {
                    continue;
                }
                field.SetParent(this);
                if (!field.FromXml(el))
                {
                    ErrorMessage.Error("解析 Table:{0}时出错", name);
                    return false;
                }
                if (fields.Exists(obj => obj.Name == field.Name))
                {
                    ErrorMessage.Error("解析 Table:{0}时出错 : 有相同名字 {1} 子节点 {2}", name, field.Name, xml.ToString());
                    return false;
                }
                fields.Add(field);
            }
            if (fields.Count == 0)
            {
                ErrorMessage.Error("解析 Table:{0}时出错:缺少field", name);
                return false;
            }
            return true;
        }
    }
}
