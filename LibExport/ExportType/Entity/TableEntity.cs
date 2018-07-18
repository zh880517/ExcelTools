using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;

namespace LibExport
{
    public class TableEntity
    {
        protected List<TableFieldEntity> fields = new List<TableFieldEntity>();

        public string Name { get; protected set; }
        public string Excel { get; protected set; }
        public string Sheet { get; protected set; }
        public string Reference { get; protected set; }
        public int KeyRow { get; protected set; }
        public int DataRow { get; protected set; }

        public List<TableFieldEntity> Fields { get { return fields; } }

        internal virtual bool FromXml(XElement xml)
        {
            Name = (string)xml.Attribute("name");
            Excel = (string)xml.Attribute("excel");
            Sheet = (string)xml.Attribute("sheet");
            Reference = (string)xml.Attribute("reference");
            KeyRow = (int)xml.Attribute("keyrow");
            DataRow = (int)xml.Attribute("datarow");
            if (string.IsNullOrWhiteSpace(Name))
            {
                ErrorMessage.Error("{0}属性缺少或者为空 : {1}", "name", xml.ToString());
                return false;
            }
            if (string.IsNullOrWhiteSpace(Excel))
            {
                ErrorMessage.Error("{0}属性缺少或者为空 : {1}", "excel", xml.ToString());
                return false;
            }
            if (string.IsNullOrEmpty(Sheet))
            {
                ErrorMessage.Error("{0}属性缺少或者为空 : {1}", "sheet", xml.ToString());
                return false;
            }
            if (KeyRow < 1)
            {
                ErrorMessage.Error("{0}属性缺少或者小于1 : {1}", "keyrow", xml.ToString());
                return false;
            }
            if (DataRow < 1)
            {
                ErrorMessage.Error("{0}属性缺少或者小于1 : {1}", "keyrow", xml.ToString());
                return false;
            }
            if (DataRow == KeyRow)
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
                    ErrorMessage.Error("解析 Table:{0}时出错", Name);
                    return false;
                }
                if (fields.Exists(obj => obj.Name == field.Name))
                {
                    ErrorMessage.Error("解析 Table:{0}时出错 : 有相同名字 {1} 子节点 {2}", Name, field.Name, xml.ToString());
                    return false;
                }
                fields.Add(field);
            }
            if (fields.Count == 0)
            {
                ErrorMessage.Error("解析 Table:{0}时出错:缺少field", Name);
                return false;
            }
            return true;
        }

        internal void UpdateExcelPath(string curPath)
        {
            Excel = Path.GetFullPath(Path.Combine(curPath, Excel));
        }
    }
}
