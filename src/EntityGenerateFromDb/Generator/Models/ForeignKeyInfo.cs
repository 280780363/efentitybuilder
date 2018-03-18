using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator.Abstraction
{
    public class ForeignKeyInfo
    {
        /// <summary>
        /// 引用表名
        /// </summary>
        public string ReferencesTableName { get; set; }

        /// <summary>
        /// 引用列名
        /// </summary>
        public string ReferencesColumnName { get; set; }

        /// <summary>
        /// 外键基表列名
        /// </summary>
        public string BaseColumnName { get; set; }

        /// <summary>
        /// 外键基表
        /// </summary>
        public string BaseTableName { get; set; }
    }
}
