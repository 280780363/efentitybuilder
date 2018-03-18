using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator.Abstraction
{
    public class ColumnInfo
    {
        public string Name { get; set; }

        public string Desciption { get; set; }

        public string ColumnDbType { get; set; }

        public bool IsPrimaryKey { get; set; }

        public bool IsRequired { get; set; }

        public bool IsIdentity { get; set; }

        public long Length { get; set; }

        public ForeignKeyInfo ForeignKey { get; set; }
    }
}
