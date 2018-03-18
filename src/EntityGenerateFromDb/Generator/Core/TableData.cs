using Generator.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator.Core
{
    public class TableData
    {
        public TableInfo Tables { get; set; }

        public IEnumerable<ColumnInfo> Columns { get; set; }

        public IEnumerable<ForeignKeyInfo> ForeignKeys { get; set; }
    }
}
