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
        static Dictionary<string, CsharpType> dic = new Dictionary<string, CsharpType>(StringComparer.OrdinalIgnoreCase);

        static string lastProvider;

        public CsharpType GetCsharpType(string dbType) {
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
                        var hasNullType = netType.EndsWith("?");
                        if (hasNullType) {
                            netType = netType.TrimEnd('?');
                        }
                        dic.Add(dbtype, new CsharpType { TypeString = netType, IsValueType = hasNullType });
                    });
            }

            if (!dic.ContainsKey(dbType))
                return null;
            var type = dic[dbType];
            return type;
        }

        public string GetType(string dbType, bool nullable) {
            var type = GetCsharpType(dbType);
            if (nullable && type.IsValueType)
                return type.TypeString + "?";

            return type.TypeString;
        }
    }


}
