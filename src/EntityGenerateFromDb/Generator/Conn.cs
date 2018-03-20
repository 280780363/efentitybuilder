using Generator.Utils;
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
using Generator.Models;
using Generator.Core;
using System.Reflection;

namespace Generator
{
    public partial class Conn : Form
    {
        SelectedProject selectedProject;
        public Conn(SelectedProject selectedProject) {
            this.selectedProject = selectedProject;
            InitializeComponent();
        }

        private void Conn_Load(object sender, EventArgs e) {
            try {
                Constant.SwtichProvider("mysql");
                WriteFile();
                var files = Directory.EnumerateFiles(Constant.BasePath, "config.json", SearchOption.AllDirectories);
                foreach (var item in files) {
                    var config = JsonHelper.DeserializeFromFile<Config>(item);
                    if (!config.Name.IsNullOrWhiteSpace())
                        cbxProviders.Items.Add(config.Name);
                }

                if (!LastDataConfiguration.Instance.Get("ConnectionString").IsNullOrWhiteSpace())
                    txtConnString.Text = LastDataConfiguration.Instance.Get("ConnectionString");

                if (!LastDataConfiguration.Instance.Get("DbProvider").IsNullOrWhiteSpace())
                    cbxProviders.SelectedItem = LastDataConfiguration.Instance.Get("DbProvider");
                else
                    cbxProviders.SelectedItem = "mysql";
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e) {
            Constant.SwtichProvider(cbxProviders.SelectedItem.ToString());
            using (var conn = Factory.DbProvider().Connection(txtConnString.Text)) {
                try {
                    conn.Open();
                }
                catch (Exception ex) {
                    MessageBox.Show("数据库连接失败:" + ex.Message);
                }
            }
            LastDataConfiguration.Instance.Set("ConnectionString", txtConnString.Text);
            LastDataConfiguration.Instance.Set("DbProvider", cbxProviders.SelectedItem.ToString());
            LastDataConfiguration.Instance.Save();
            new Main(selectedProject, txtConnString.Text).Show();
            this.Close();
        }

        private void WriteFile() {
            var ass = Assembly.GetExecutingAssembly();

            if (!Directory.Exists(Constant.CurrentProviderPath))
                Directory.CreateDirectory(Constant.CurrentProviderPath);

            if (!File.Exists(Constant.EntityTemplateFile)) {
                var entityTemplate = ass.GetManifestResourceStream("Generator.Config.entity.template");
                Write(entityTemplate, Constant.EntityTemplateFile);
            }
            if (!File.Exists(Constant.ContextTemplateFile)) {
                var contextTemplate = ass.GetManifestResourceStream("Generator.Config.context.template");
                Write(contextTemplate, Constant.ContextTemplateFile);
            }
            if (!File.Exists(Constant.ConfigFile)) {
                var configJson = ass.GetManifestResourceStream("Generator.Config.mysql.config.json");
                Write(configJson, Constant.ConfigFile);
            }

            if (!File.Exists(Constant.CurrentProviderMapperFile)) {
                var mapper = ass.GetManifestResourceStream("Generator.Config.mysql.mapper.txt");
                Write(mapper, Constant.CurrentProviderMapperFile);
            }
            var dllFile = Path.Combine(Constant.CurrentProviderPath, "MySql.Data.dll");
            if (!File.Exists(dllFile)) {
                var dll = ass.GetManifestResourceStream("Generator.Config.mysql.MySql.Data.dll");
                Write(dll, dllFile);
            }
            if (!File.Exists(Constant.CurrentProviderQueryFile)) {
                var query = ass.GetManifestResourceStream("Generator.Config.mysql.query.sql");
                Write(query, Constant.CurrentProviderQueryFile);
            }
        }


        static void Write(Stream stream, string filename) {
            StreamWriter sw = new StreamWriter(filename);
            stream.CopyTo(sw.BaseStream);
            sw.Flush();
            sw.Close();
            stream.Close();
        }
    }
}