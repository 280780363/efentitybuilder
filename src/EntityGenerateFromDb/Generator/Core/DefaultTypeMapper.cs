using Generator.Utils;
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

        public string GetType(string dbType, bool isnull)
        {
            if (lastProvider != Constant.CurrentProvider)
            {
                dic.Clear();
                lastProvider = Constant.CurrentProvider;

                File.ReadAllLines(Constant.CurrentProviderMapperFile)
                    .Select(r => r.Trim())
                    .Where(r => !r.IsNullOrWhiteSpace() && !r.StartsWith("#"))
                    .ForEach_(r =>
                    {
                        var arr = r.Split(':');
                        var dbtype = arr[0];

                        var netType = arr[1];
                        var hasNullType = netType.EndsWith("?");
                        if (hasNullType)
                        {
                            netType = netType.TrimEnd('?');
                        }
                        dic.Add(dbtype, new NetType { Type = netType, HasNullType = hasNullType });
                    });
            }

            if (!dic.ContainsKey(dbType))
                return "找不到的数据库映射类型:" + dbType + ",请先编辑类型映射文件";
            var type = dic[dbType];
            if (isnull && type.HasNullType)
                return type.Type + "?";

            return type.Type;
        }


        private class NetType
        {
            public string Type { get; set; }

            public bool HasNullType { get; set; }
        }
    }
}
