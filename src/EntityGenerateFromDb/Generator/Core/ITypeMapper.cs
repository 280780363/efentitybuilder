using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator.Core
{
    public interface ITypeMapper
    {
        string GetType(string dbType, bool nullable);

        CsharpType GetCsharpType(string dbType);
    }

    public class CsharpType
    {
        public string TypeString { get; set; }

        /// <summary>
        /// 是否为值类型,有?号的就是值类型
        /// </summary>
        public bool IsValueType { get; set; }
    }
}
