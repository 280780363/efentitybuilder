using Generator.Common;
using Generator.Core;
using Generator.Models;
using Generator.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
        public Main(SelectedProject selectedProject, string connString)
        {
            this.selectedProject = selectedProject;
            this.connString = connString;
            InitializeComponent();
        }


        All all;
        private async void Main_Load(object sender, EventArgs e)
        {
            await Task.Run(() =>
             {
                 using (var conn = Factory.DbProvider().Connection(connString))
                 {
                     this.Text = $"{this.Text}[{Constant.CurrentProvider}:{conn.Database}]";
                     all = Factory.QueryProvider().GetAll(conn);

                     foreach (var item in all.Tables)
                     {
                         cbxlTables.Items.Add(item.Name);
                     }
                 }
             });
        }
    }
}
