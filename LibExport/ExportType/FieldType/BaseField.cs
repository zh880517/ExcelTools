using System.Xml.Serialization;

namespace LibExport
{
    [XmlType(TypeName = "base")]
    public class StructBaseField
    {
        [XmlAttribute]
        public string name;
        [XmlAttribute]
        public string type;
    }

    //[XmlType(TypeName = "base")]
    public class TableBaseField : StructBaseField
    {
        [XmlAttribute]
        public string index;
    }
}
