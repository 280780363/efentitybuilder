﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator.Models
{
    public class All
    {
        public List<ColumnInfo> Columns { get; set; }

        public List<ForeignKeyInfo> ForeignKeys { get; set; }

        public List<TableInfo> Tables { get; set; }
    }
}
