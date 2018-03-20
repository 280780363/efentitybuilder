using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Generator.Utils;
namespace Generator.Models
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

        public string BasePropertyName(All all) {
            if (all.ForeignKeys.Count(r => r.ReferencesTableName.EqualsIgnoreCase(ReferencesTableName) && r.BaseTableName.EqualsIgnoreCase(BaseTableName)) > 1) {
                //如果当前表有多个外键指向同一表
                return getPrefix() + "_" + ReferencesTableName;
            }
            else
                return ReferencesTableName;
        }

        public string ReferencesPropertyName(All all) {
            if (all.ForeignKeys.Count(r => r.ReferencesTableName.EqualsIgnoreCase(ReferencesTableName) && r.BaseTableName.EqualsIgnoreCase(BaseTableName)) > 1) {
                //如果引用到当前表的 表有多个外键指向当前表
                return getPrefix() + "_" + BaseTableName;
            }
            else
                return BaseTableName;
        }

        private string getPrefix() {
            string prefix = BaseColumnName;
            if (BaseColumnName.EndsWith("id", StringComparison.OrdinalIgnoreCase))
                prefix = BaseColumnName.Substring(0, BaseColumnName.IndexOf("id", StringComparison.OrdinalIgnoreCase));

            return prefix;
        }
    }
}
