using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Generator.Utils
{
    public static class Constant
    {
        static Constant()
        {
            BasePath = Path.Combine(System.Environment.GetEnvironmentVariable("AppData"), @"efcore.entity.generator\Config");
            if (!Directory.Exists(BasePath))
                Directory.CreateDirectory(BasePath);
        }

        /// <summary>
        /// 程序基础路径
        /// </summary>
        public static string BasePath { get; }
        public static string ConfigFile { get { return Path.Combine(BasePath, CurrentProvider, "config.json"); } }
        public static string EntityTemplateFile { get { return Path.Combine(BasePath, "entity.template"); } }
        public static string ContextTemplateFile { get { return Path.Combine(BasePath, "context.template"); } }
        public static string EntityConfigTemplateFile { get { return Path.Combine(BasePath, "entity.config.template"); } }

        public static void SwtichProvider(string provider)
        {
            _currentProvider = provider;
        }

        private static string _currentProvider = "mysql";
        public static string CurrentProvider
        {
            get
            {

                return _currentProvider;
            }
        }

        public static string CurrentProviderPath
        {
            get
            {
                return Path.Combine(BasePath, CurrentProvider);
            }
        }


        public static string CurrentProviderQueryFile
        {
            get
            {
                return Path.Combine(CurrentProviderPath, "query.sql");
            }
        }

        public static string CurrentProviderMapperFile
        {
            get
            {
                return Path.Combine(CurrentProviderPath, "mapper.txt");
            }
        }


    }
}
