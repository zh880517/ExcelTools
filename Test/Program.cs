using LibExport;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Test
{
    class Test
    {
        public List<int> Key = new List<int>();
    }
    class Program
    {
        static void Main(string[] args)
        {
            Test t = new Test();
            t.Key.Add(1);
            t.Key.Add(2);
            string json = JsonConvert.SerializeObject(t);

            var ne = JsonConvert.DeserializeObject<Test>(json);
        }
    }
}
