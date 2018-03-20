using EnvDTE;
using Generator.Utils;
using Generator.Core;
using Generator.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Generator
{
    public partial class Main : Form
    {
        SelectedProject selectedProject;
        string connString;
        public Main(SelectedProject selectedProject, string connString) {
            this.selectedProject = selectedProject;
            this.connString = connString;
            InitializeComponent();
        }


        All all;
        private void Main_Load(object sender, EventArgs e) {
            using (var conn = Factory.DbProvider().Connection(connString)) {
                this.Text = $"{this.Text}[{Constant.CurrentProvider}:{conn.Database}]";
                all = Factory.QueryProvider().GetAll(conn);
                foreach (var item in all.Tables) {
                    cbxlTables.Items.Add(item.Name, true);
                }
                cbxAll.Checked = true;
                if (!LastDataConfiguration.Instance.Get("SavePath").IsNullOrWhiteSpace())
                    txtSavePath.Text = LastDataConfiguration.Instance.Get("SavePath");
                if (!LastDataConfiguration.Instance.Get("ContextPrefix").IsNullOrWhiteSpace())
                    txtContextPrefix.Text = LastDataConfiguration.Instance.Get("ContextPrefix");
            }
        }

        private void cbxAll_CheckedChanged(object sender, EventArgs e) {
            for (int i = 0; i < cbxlTables.Items.Count; i++) {
                cbxlTables.SetItemChecked(i, cbxAll.Checked);
            }
        }

        private async void btnGenerate_Click(object sender, EventArgs e) {
            try {
                ProjectItem lastItem = null;
                if (!txtSavePath.Text.IsNullOrWhiteSpace())
                    lastItem = this.selectedProject.ProjectDte.AddFolderToProject(txtSavePath.Text);

                var saveDir = Path.Combine(selectedProject.ProjectDirectoryName, txtSavePath.Text);
                if (!Directory.Exists(saveDir))
                    Directory.CreateDirectory(saveDir);


                //放个临时文件夹 再用AddFileFromCopy的方式 添加到项目中,增加了IO操作
                //实在没找到用什么方法可以直接添加一个现有的文件

                string generatorTempDir = Path.Combine(Constant.BasePath, "generate_temp");
                bool shouldDeleteTempDir = false;
                if (!Directory.Exists(generatorTempDir)) {
                    shouldDeleteTempDir = true;
                    Directory.CreateDirectory(generatorTempDir);
                }

                List<TableInfo> checkedTables = new List<TableInfo>();
                foreach (var item in cbxlTables.CheckedItems) {
                    var table = all.Tables.FirstOrDefault(r => r.Name.EqualsIgnoreCase(item.ToString()));
                    if (table != null)
                        checkedTables.Add(table);
                }

                #region 实体类创建

                var generator = Factory.Generator();

                foreach (var table in checkedTables) {

                    await ShowBuildMsgAsync($"开始为您创建实体:\"{table.Name}.cs\"");

                    //这里无需等待
                    await Task.Run(async () => {
                        try {
                            string classContent = generator.GenerateEntity(table, all);
                            var fileFullName = Path.Combine(saveDir, table.Name + ".cs");

                            if (!File.Exists(fileFullName)) {
                                var fileTempFullName = Path.Combine(generatorTempDir, table.Name + ".cs");

                                File.WriteAllText(fileTempFullName, classContent, Encoding.UTF8);
                                if (lastItem != null)
                                    lastItem.AddFilesToProject(fileTempFullName);
                                else
                                    this.selectedProject.ProjectDte.AddFilesToProject(fileTempFullName);
                                selectedProject.ProjectDte.Save();

                                File.Delete(fileTempFullName);
                            }
                            else {
                                File.WriteAllText(fileFullName, classContent, Encoding.UTF8);
                            }

                            await ShowBuildMsgAsync($"实体文件[{table.Name}.cs]创建完成");
                        }
                        catch (Exception ex) {
                            await ShowBuildMsgAsync($"创建实体文件[{table.Name}.cs]出现错误:{JsonHelper.ToJson(ex)}");
                        }
                    });
                }

                #endregion

                #region dbcontext创建

                string contextName = txtContextPrefix.Text + "DbContext";



                string contextClassContent = generator.GenrateContext(checkedTables, contextName);
                try {
                    var fileFullName = Path.Combine(saveDir, contextName + ".cs");
                    await ShowBuildMsgAsync($"正在为您创建:\"{contextName}.cs\"");

                    if (!File.Exists(fileFullName)) {
                        var fileTempFullName = Path.Combine(generatorTempDir, contextName + ".cs");

                        File.WriteAllText(fileTempFullName, contextClassContent, Encoding.UTF8);

                        if (lastItem != null)
                            lastItem.AddFilesToProject(fileTempFullName);
                        else
                            this.selectedProject.ProjectDte.AddFilesToProject(fileTempFullName);
                        selectedProject.ProjectDte.Save();

                        File.Delete(fileTempFullName);
                    }
                    else {
                        File.WriteAllText(fileFullName, contextClassContent, Encoding.UTF8);
                    }
                }
                catch (Exception ex) {
                    await ShowBuildMsgAsync($"创建DbContext文件[{contextName}.cs]出现错误:{JsonHelper.ToJson(ex)}");
                }


                #endregion

                if (shouldDeleteTempDir)
                    Directory.Delete(generatorTempDir, true);
                LastDataConfiguration.Instance.Set("SavePath", txtSavePath.Text);
                LastDataConfiguration.Instance.Set("ContextPrefix", txtContextPrefix.Text);
                LastDataConfiguration.Instance.Save();

                await ShowBuildMsgAsync("已为您创建完成!");
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }

        private async Task ShowBuildMsgAsync(string msg) {
            if (!txtLog.Text.IsNullOrWhiteSpace())
                txtLog.AppendText("\r\n");
            txtLog.AppendText(msg);
            txtLog.ScrollToCaret();
            await Task.CompletedTask;
        }

        private void 编辑实体模板ToolStripMenuItem_Click(object sender, EventArgs e) {
            System.Diagnostics.Process.Start("notepad.exe", Constant.EntityTemplateFile);
        }

        private void 编辑数据库上下文模板ToolStripMenuItem_Click(object sender, EventArgs e) {
            System.Diagnostics.Process.Start("notepad.exe", Constant.ContextTemplateFile);
        }

        private void 编辑类型映射ToolStripMenuItem_Click(object sender, EventArgs e) {
            System.Diagnostics.Process.Start("notepad.exe", Constant.CurrentProviderMapperFile);
        }

        private void 编辑查询语句ToolStripMenuItem_Click(object sender, EventArgs e) {
            System.Diagnostics.Process.Start("notepad.exe", Constant.CurrentProviderQueryFile);
        }
    }
}
