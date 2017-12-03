using System.Collections.Generic;
using System.Xml.Serialization;

namespace LibExport
{
    [XmlType(TypeName = "RowTable")]
    public class RowTable
    {
        [XmlAttribute]
        public string name;
        [XmlAttribute]
        public string excel;
        [XmlAttribute]
        public string sheet;

        [XmlElement]
        public KeyField key;
        [XmlArray]
        public List<TableBaseField> baseFields;
        [XmlArray]
        public List<TableListField> listFields;
        [XmlArray]
        public List<TableMapField> mapFields;
        [XmlArray]
        public List<MixField> mixFields;
        [XmlArray]
        public List<MixListField> mixListFields;
    }
}
