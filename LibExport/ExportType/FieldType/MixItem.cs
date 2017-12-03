using System.Xml.Serialization;

namespace LibExport
{
    [XmlType(TypeName = "item")]
    public class MixItem
    {
        [XmlAttribute]
        public string index;
    }
}
