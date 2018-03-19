using Generator.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Generator.Utils
{
    /// <summary>
    /// 上一次使用的一些配置数据
    /// </summary>
    public class LastDataConfiguration : Dictionary<string, string>
    {
        private static string _lastDataFile;
        static LastDataConfiguration()
        {
            var basepath = Constant.BasePath;

            _lastDataFile = Path.Combine(basepath, "lastdata.json");

            if (File.Exists(_lastDataFile))
            {

                string content = File.ReadAllText(_lastDataFile, Encoding.UTF8);
                Instance = JsonConvert.DeserializeObject<LastDataConfiguration>(content);
            }
            else
                Instance = new LastDataConfiguration();

        }
        private LastDataConfiguration() { }

        public static LastDataConfiguration Instance { get; private set; }

        public void Save()
        {
            string content = JsonConvert.SerializeObject(Instance);
            File.WriteAllText(_lastDataFile, content, Encoding.UTF8);
        }

        /// <summary>
        /// 获取配置数据
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public string Get(string name)
        {
            if (this.ContainsKey(name))
                return this[name];

            else
                return null;
        }

        public void Set(string name, string value)
        {
            if (this.ContainsKey(name))
                this[name] = value;
            else
                this.Add(name, value);
        }
    }
}
