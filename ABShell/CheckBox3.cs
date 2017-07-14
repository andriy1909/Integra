using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ABShell
{
    public partial class CheckBox3 : UserControl
    {
        private bool cheched;

        public bool Checked
        {
            get
            {
                return cheched;
            }
            set
            {
                cheched = value;
                //if(BackgroundImage==Properties.Resources.)
            }
        }

        public CheckBox3()
        {
            InitializeComponent();
        }

        private void CheckBox3_Click(object sender, EventArgs e)
        {

        }
    }
}
