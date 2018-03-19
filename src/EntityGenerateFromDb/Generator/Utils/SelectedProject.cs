using System.Collections.Generic;
using System.IO;
using EnvDTE;
using System.Linq;
using System;

namespace Generator.Utils
{
    public class SelectedProject
    {
        public IServiceProvider ServiceProvider { get; private set; }
        private readonly string _projectPath;
        private readonly Project _projectDte;
        private List<ProjectItem> _allProjectItem;
        private List<string> _allProjectItemName;
        private List<string> _allCsFilenamesWithoutExtension;
        public string ProjectPath { get { return _projectPath; } }
        public string ProjectName { get { return Path.GetFileNameWithoutExtension(ProjectPath); } }
        public string ProjectDirectoryName { get { return Path.GetDirectoryName(ProjectPath); } }
        public Project ProjectDte { get { return _projectDte; } }
        public List<ProjectItem> AllProjectItem { get { return _allProjectItem; } }
        public List<string> AllProjectItemName { get { return AllProjectItemName; } }
        public List<string> AllCsFilenamesWithoutExtension { get { return _allCsFilenamesWithoutExtension; } }


        /// <summary>
        /// 获取选中项目的信息
        /// </summary>
        /// <param name="dte"></param>
        /// <returns></returns>
        public SelectedProject(DTE dte, IServiceProvider _serviceProvider)
        {
            this.ServiceProvider = _serviceProvider;
            _allProjectItem = new List<ProjectItem>();
            _allCsFilenamesWithoutExtension = new List<string>();
            var selectedItems = dte.SelectedItems;

            var projectName = (from SelectedItem item in selectedItems select item.Name).ToList();

            if (!selectedItems.MultiSelect && selectedItems.Count == 1)
            {
                var selectProject = selectedItems.Item(projectName.First());

                _projectPath = selectProject.Project.FullName;
                _projectDte = selectProject.Project;

                foreach (ProjectItem item in selectProject.Project.ProjectItems)
                {
                    FillChildProjectItem(item);
                }
            }

            _allProjectItemName = _allProjectItem.Select(r => r.FileNames[0]).ToList();
            _allCsFilenamesWithoutExtension = _allProjectItemName
                .Where(r => File.Exists(r) && Path.GetExtension(r).Equals(".cs", System.StringComparison.OrdinalIgnoreCase))
                .Select(Path.GetFileNameWithoutExtension)
                .ToList();
        }

        void FillChildProjectItem(ProjectItem item)
        {
            if (item.ProjectItems == null || item.ProjectItems.Count == 0)
                _allProjectItem.Add(item);
            else
            {
                var items = item.ProjectItems.GetEnumerator();
                while (items.MoveNext())
                {
                    var currentItem = (ProjectItem)items.Current;
                    FillChildProjectItem(currentItem);
                }
                _allProjectItem.Add(item);
            }
        }


    }
}
