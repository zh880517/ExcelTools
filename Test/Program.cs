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
        public DateTime Key;
    }
    class Program
    {
        static void Main(string[] args)
        {
            Test t = new Test();
            t.Key = DateTime.Now;
            string json = JsonConvert.SerializeObject(t);

            var ne = JsonConvert.DeserializeObject<Test>(json);
        }
    }
}
