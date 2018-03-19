using EnvDTE;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator.Utils
{
    public static class Msg
    {
        /// <summary>
        /// 弹出消息
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="msg"></param>
        /// <param name="title"></param>
        public static void Show(this IServiceProvider serviceProvider, string msg, string title = "")
        {
            VsShellUtilities.ShowMessageBox(
                serviceProvider,
                msg,
                title,
                OLEMSGICON.OLEMSGICON_INFO,
                OLEMSGBUTTON.OLEMSGBUTTON_OK,
                OLEMSGDEFBUTTON.OLEMSGDEFBUTTON_FIRST);
        }

        /// <summary>
        /// 输出消息到 输出消息框
        /// </summary>
        /// <param name="dte"></param>
        /// <param name="msg"></param>
        public static void OutputMsg(this DTE dte, string msg)
        {
            var window = dte.Windows.Item(EnvDTE.Constants.vsWindowKindOutput);
            var outputWindow = (OutputWindow)window.Object;
            if (outputWindow.ActivePane == null)
                outputWindow.OutputWindowPanes.Add("EntitiesGenerator");
            outputWindow.ActivePane.Activate();

            outputWindow.ActivePane.OutputString(msg);
            outputWindow.ActivePane.OutputString("\n");
        }
    }
}
