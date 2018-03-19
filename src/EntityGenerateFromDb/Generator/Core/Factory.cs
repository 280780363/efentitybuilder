using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator.Core
{
    public class Factory
    {
        public static IDbProvider DbProvider() {
            return new DefaultDbProvider();
        }


        public static IQueryProvider QueryProvider() {
            return new DefaultQueryProvider();
        }

        public static ITypeMapper TypeMapper() {
            return new DefaultTypeMapper();
        }

        public static IGenerator Generator() {
            return new DefaultGenerator();
        }
    }
}
