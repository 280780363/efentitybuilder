using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Generator.Common
{
    public class Constant
    {
        /// <summary>
        /// 程序基础路径
        /// </summary>
        public static readonly string BasePath = Path.Combine(Assembly.GetExecutingAssembly().Location, "Config");
        public static readonly string ConfigFile = Path.Combine(CurrentProvider, "config.json");
        public static readonly string EntityTemplateFile = Path.Combine(BasePath, "entity.template");
        public static readonly string ContextTemplateFile = Path.Combine(BasePath, "context.template");
        public static readonly string EntityConfigTemplateFile = Path.Combine(BasePath, "entity.config.template");

        public static void SwtichProvider(string provider) {
            _currentProvider = provider;
        }

        private static string _currentProvider = null;
        public static string CurrentProvider { get; } = _currentProvider;

        public static string CurrentProviderPath {
            get {
                return Path.Combine(BasePath, CurrentProvider);
            }
        }


        public static string CurrentProviderQueryFile {
            get {
                return Path.Combine(CurrentProviderPath, "query.sql");
            }
        }

        public static string CurrentProviderMapperFile {
            get {
                return Path.Combine(CurrentProviderPath, "mapper.txt");
            }
        }


    }
}
