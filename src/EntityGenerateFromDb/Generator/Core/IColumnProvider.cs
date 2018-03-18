using Generator.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator.Core
{
    public interface IColumnProvider
    {
        IEnumerable<ColumnInfo> GetAll();
    }
}
