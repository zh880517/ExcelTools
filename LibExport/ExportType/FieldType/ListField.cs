using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace LibExport
{
    [XmlType(TypeName = "list")]
    public class StructListField
    {
        [XmlAttribute]
        public string name;
        [XmlAttribute]
        public string type;
        [XmlAttribute]
        public string sep;
    }

    public class TableListField : StructListField
    {
        [XmlAttribute]
        public string index;
    }
}
