using System.Xml.Serialization;

namespace LibExport
{
    [XmlType(TypeName = "key")]
    public class KeyField
    {
        [XmlAttribute]
        public string index;
        [XmlAttribute]
        public string type;
    }
}
