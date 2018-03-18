using Generator.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator.Core
{
    public interface ITableProvider
    {
        IEnumerable<TableInfo> GetAll();

        string GenerateEntity(TableInfo tableInfo, IEnumerable<ColumnInfo> columnInfos, IEnumerable<ForeignKeyInfo> foreignKeys);
    }
}
