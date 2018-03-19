using Generator.Common;
using Generator.Models;
using Lazy.Utilities.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Generator.Core
{
    public class DefaultDbProvider : IDbProvider
    {
        public IDbConnection Connection(string connectionString) {


            if (!File.Exists(Constant.ConfigFile))
                throw new Exception($"找不到配置文件:{Constant.ConfigFile}");

            var config = JsonExtensions.DeserializeFromFile<Config>(Constant.ConfigFile);
            if (config.ConnectionType.IsNullOrWhiteSpace())
                throw new Exception($"配置ConnectionType不能为空");

            Type connType;
            if (!config.ProviderAssembly.IsNullOrWhiteSpace()) {
                var dll = Directory.GetFiles(Constant.CurrentProviderPath, "*.dll")?.FirstOrDefault();
                if (dll.IsNullOrWhiteSpace())
                    throw new Exception($"{Constant.CurrentProviderPath}:找不到数据库连接提供程序!");
                var ass = Assembly.LoadFrom(dll);

                connType = ass.ExportedTypes.FirstOrDefault(r => !r.IsAbstract && r.IsClass && r.IsChildTypeOf<IDbConnection>());
            }
            else {
                connType = Type.GetType(config.ConnectionType);
            }

            if (connType == null)
                throw new Exception($"找不到连接类型:{config.ConnectionType}");

            var conn = Activator.CreateInstance(connType) as IDbConnection;

            conn.ConnectionString = connectionString;

            return conn;
        }
    }
}
