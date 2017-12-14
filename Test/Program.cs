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
            //             Test t = new Test();
            //             t.Key.Add(1);
            //             t.Key.Add(2);
            //             string json = JsonConvert.SerializeObject(t);
            // 
            //             var ne = JsonConvert.DeserializeObject<Test>(json);

            ObjectValue obj = new ObjectValue();
            obj.Add("key", new StringValue("123"));
            MapValue map = new MapValue();
            map.Add(new NumberValue("456"), new StringValue("hhhh"));
            map.Add(new NumberValue("4567"), new StringValue("hh222"));
            obj.Add("map", map);

            ObjectValue obj1 = new ObjectValue();
            obj1.Add("key", new StringValue("123"));
            obj1.Add("map", map);
            ListValue list = new ListValue();
            list.Add(obj1);
            list.Add(obj1);
            list.Add(obj1);
            obj.Add("list", list);

            File.WriteAllText("test.json", obj.ToJson(0));

            File.WriteAllText("test.lua", obj.ToLua(0));
        }
    }
}
