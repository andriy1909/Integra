using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ABShell
{
    public partial class WiFiListForm : Form
    {
        WorkWithWiFi wifi;
        List<WiFiNetwork> list;

        public WiFiListForm()
        {
            InitializeComponent();
            wifi = new WorkWithWiFi();
        }

        private void WiFiListForm_Load(object sender, EventArgs e)
        {
            list = new List<WiFiNetwork>();
            foreach (WiFiNetwork item in list)
            {
                dgvData.Rows.Add(item.Name);
            }
        }

        private void btConnect_Click(object sender, EventArgs e)
        {
            wifi.Connect(list[dgvData.CurrentCell.RowIndex]);
        }
    }
}
