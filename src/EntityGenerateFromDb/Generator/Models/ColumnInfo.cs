using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator.Models
{
    public class ColumnInfo
    {
        public string TableName { get; set; }

        public string Name { get; set; }

        public string Desciption { get; set; }

        public string ColumnDbType { get; set; }

        public bool IsPrimaryKey { get; set; }

        public bool Nullable { get; set; }

        public bool IsIdentity { get; set; }

        public long? Length { get; set; }
    }
}
