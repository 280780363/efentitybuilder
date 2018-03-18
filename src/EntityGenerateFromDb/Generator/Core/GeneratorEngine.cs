using Generator.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator.Core
{
    public class GeneratorEngine
    {
        IColumnProvider columnProvider;
        IForeignKeyProvider foreignKeyProvider;
        ITableProvider tableProvider;
        public GeneratorEngine(
            IColumnProvider columnProvider,
            IForeignKeyProvider foreignKeyProvider,
            ITableProvider tableProvider)
        {
            this.foreignKeyProvider = foreignKeyProvider;
            this.columnProvider = columnProvider;
            this.tableProvider = tableProvider;
        }

        /// <summary>
        /// 获取表所有的列
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public IEnumerable<ColumnInfo> GetColumns(string tableName)
        {
            return null;
        }

        /// <summary>
        /// 获取指向该表的所有的外键
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public IEnumerable<ForeignKeyInfo> GetForeignKeys(string tableName)
        {
            return null;
        }

        /// <summary>
        /// 生成实体
        /// </summary>
        /// <param name="content"></param>
        public void GenerateEntity(string content)
        {

        }
    }
}
