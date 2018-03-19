using Generator.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator.Core
{
    public interface IQueryProvider
    {
        All GetAll(IDbConnection connection);
    }
}
