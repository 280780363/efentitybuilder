using Generator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator.Core
{
    public interface IGenerator
    {
        string GenerateEntity(TableInfo table, All all);

        string GenrateContext(All all, string contextName);
    }
}
