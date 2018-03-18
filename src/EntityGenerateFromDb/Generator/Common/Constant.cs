using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Generator.Common
{
    public class Constant
    {
        /// <summary>
        /// 程序基础路径
        /// </summary>
        public static string BasePath = Assembly.GetExecutingAssembly().Location;
    }
}
