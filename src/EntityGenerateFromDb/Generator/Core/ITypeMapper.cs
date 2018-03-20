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
    }
}
