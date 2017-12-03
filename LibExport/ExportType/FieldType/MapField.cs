using System.Xml.Serialization;

namespace LibExport
{

    [XmlType(TypeName = "map")]
    public class StructMapField
    {
        [XmlAttribute]
        public string name;
        [XmlAttribute]
        public string keytype;
        [XmlAttribute]
        public string valuetype;
        [XmlAttribute]
        public string sep;
        [XmlAttribute]
        public string kvsep;
    }

    public class TableMapField : StructMapField
    {
        [XmlAttribute]
        public string index;
    }
}
