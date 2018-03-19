using Generator.Common;
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
using Generator.Utils;
using Generator.Models;
using Generator.Core;

namespace Generator
{
    public partial class Conn : Form
    {
        SelectedProject selectedProject;
        public Conn(SelectedProject selectedProject)
        {
            this.selectedProject = selectedProject;
            InitializeComponent();
        }

        private void Conn_Load(object sender, EventArgs e)
        {
            try
            {
                var files = Directory.EnumerateFiles(Constant.BasePath, "config.json", SearchOption.AllDirectories);
                foreach (var item in files)
                {
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

                Constant.SwtichProvider("mysql");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Constant.SwtichProvider(cbxProviders.SelectedItem.ToString());
            using (var conn = Factory.DbProvider().Connection(txtConnString.Text))
            {
                try
                {
                    conn.Open();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("数据库连接失败:" + ex.Message);
                }
            }
            LastDataConfiguration.Instance.Set("ConnectionString", txtConnString.Text);
            LastDataConfiguration.Instance.Set("DbProvider", cbxProviders.SelectedItem.ToString());
            LastDataConfiguration.Instance.Save();
            new Main(selectedProject, txtConnString.Text).Show();
            this.Close();
        }
    }
}
