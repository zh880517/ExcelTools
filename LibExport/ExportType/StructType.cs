using System.Collections.Generic;
using System.Xml.Serialization;

namespace LibExport
{
    [XmlType(TypeName = "Struct")]
    public class StructType
    {
        [XmlAttribute]
        public string name;
        [XmlAttribute]
        public string sep;
        [XmlArray("base")]
        public List<StructBaseField> baseFields;
        [XmlArray("list")]
        public List<StructListField> listFields;
    }
}
