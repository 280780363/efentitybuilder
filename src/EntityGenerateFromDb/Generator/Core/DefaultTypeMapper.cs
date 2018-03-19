using Generator.Common;
using Generator.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator.Core
{
    public class DefaultTypeMapper : ITypeMapper
    {
        static Dictionary<string, NetType> dic = new Dictionary<string, NetType>(StringComparer.OrdinalIgnoreCase);

        static string lastProvider;

        public string GetType(string dbType, bool isnull) {
            if (lastProvider != Constant.CurrentProvider) {
                dic.Clear();
                lastProvider = Constant.CurrentProvider;

                File.ReadAllLines(Constant.CurrentProviderMapperFile)
                    .Select(r => r.Trim())
                    .Where(r => !r.IsNullOrWhiteSpace() && !r.StartsWith("#"))
                    .ForEach_(r => {
                        var arr = r.Split(':');
                        var dbtype = arr[0];

                        var netType = arr[1];
                        var nullable = netType.EndsWith("?");
                        if (nullable) {
                            netType = netType.TrimEnd('?');
                        }
                        dic.Add(dbtype, new NetType { Type = netType, Nullable = nullable });
                    });
            }

            if (!dic.ContainsKey(dbType))
                return null;
            var type = dic[dbType];
            if (isnull && type.Nullable)
                return type + "?";

            return type.Type;
        }


        private class NetType
        {
            public string Type { get; set; }

            public bool Nullable { get; set; }
        }
    }
}
