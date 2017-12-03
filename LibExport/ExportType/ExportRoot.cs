using System.Collections.Generic;
using System.Xml.Serialization;

namespace LibExport
{
    [XmlType(TypeName = "root")]
    public class ExportRoot
    {
        [XmlArray("CommonStruct")]
        public List<StructType> structTypes;

        [XmlArray("Export")]
        public List<RowTable> rowTables;
    }
}
