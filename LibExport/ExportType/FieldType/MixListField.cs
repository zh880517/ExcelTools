using System.Collections.Generic;
using System.Xml.Serialization;

namespace LibExport
{
    [XmlType(TypeName = "mixlist")]
    public class MixListField
    {
        [XmlAttribute]
        public string name;
        [XmlAttribute]
        public string type;
        [XmlArray]
        public List<MixItem> items;
    }
}
