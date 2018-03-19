using EnvDTE;
using Lazy.Utilities.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator.Utils
{
    public static class ProjectExtension
    {
        /// <summary>
        /// 根据名字获取子项
        /// 没有则返回空
        /// </summary>
        /// <param name="item"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static ProjectItem GetChildProjectItem(this ProjectItem item, string name)
        {
            var items = item.ProjectItems.GetEnumerator();
            while (items.MoveNext())
            {
                var currentItem = (ProjectItem)items.Current;
                if (currentItem.Name == name)
                    return currentItem;
            }

            return null;
        }

        /// <summary>
        /// 根据名字获取子项
        /// 没有则返回空
        /// </summary>
        /// <param name="item"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static ProjectItem GetChildProjectItem(this Project item, string name)
        {
            var items = item.ProjectItems.GetEnumerator();
            while (items.MoveNext())
            {
                var currentItem = (ProjectItem)items.Current;
                if (currentItem.Name == name)
                    return currentItem;
            }

            return null;
        }

        /// <summary>
        /// 增加项目文件夹
        /// 可以以 / 表示文件夹的层级关系
        /// </summary>
        /// <param name="project"></param>
        /// <param name="folderName">文件夹路径,可以以 / 分割层级关系</param>
        /// <returns>返回最后一层的ProjectItem</returns>
        public static ProjectItem AddFolderToProject(this Project project, string folderName)
        {
            if (folderName.IsNullOrWhiteSpace())
                throw new ArgumentNullException(nameof(folderName));

            string[] folderArr = folderName.Split('/');

            var childItem = project.GetChildProjectItem(folderArr[0]);

            ProjectItem previous;
            if (childItem != null)
                previous = childItem;
            else
                previous = project.ProjectItems.AddFolder(folderArr[0]);

            for (int i = 1; i < folderArr.Length; i++)
            {
                if (folderArr[i].IsNullOrWhiteSpace())
                    continue;

                var childItem2 = previous.GetChildProjectItem(folderArr[i]);
                if (childItem2 != null)
                    previous = childItem2;
                else
                    previous = previous.ProjectItems.AddFolder(folderArr[i]);
            }

            project.Save();

            return previous;
        }

        /// <summary>
        /// 是否只选了一个项目
        /// </summary>
        /// <param name="dte"></param>
        /// <returns></returns>
        public static bool IsSelectedOnlyOne(this DTE dte)
        {
            return !dte.SelectedItems.MultiSelect && dte.SelectedItems.Count == 1;
        }

        /// <summary>
        /// 添加项目项
        /// </summary>
        /// <param name="projectDte"></param>
        /// <param name="files"></param>
        public static void AddFilesToProject(this ProjectItem project, params string[] files)
        {

            if (!files.Any())
                return;
            foreach (string file in files)
            {
                project.ProjectItems.AddFromFileCopy(file);
            }

            //project.Save();
        }

        /// <summary>
        /// 排除项目项
        /// </summary>
        /// <param name="projectDte"></param>
        /// <param name="files"></param>
        public static void RemoveFilesFromProject(this ProjectItem project, params string[] files)
        {
            if (!files.Any())
                return;
            foreach (string file in files)
            {
                project.ProjectItems.Item(Path.GetFileName(file)).Remove();
            }

            //project.Save();
        }

        /// <summary>
        /// 添加项目项
        /// </summary>
        /// <param name="projectDte"></param>
        /// <param name="files"></param>
        public static void AddFilesToProject(this Project project, params string[] files)
        {

            if (!files.Any())
                return;
            foreach (string file in files)
            {
                project.ProjectItems.AddFromFileCopy(file);
            }

            //project.Save();
        }

        /// <summary>
        /// 排除项目项
        /// </summary>
        /// <param name="projectDte"></param>
        /// <param name="files"></param>
        public static void RemoveFilesFromProject(this Project project, params string[] files)
        {
            if (!files.Any())
                return;
            foreach (string file in files)
            {
                project.ProjectItems.Item(Path.GetFileName(file)).Remove();
            }

            //project.Save();
        }
    }
}
