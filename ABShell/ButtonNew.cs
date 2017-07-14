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
    public partial class ButtonNew : UserControl
    {
        public ButtonNew()
        {
            InitializeComponent();
        }

        private void ButtonNew_Load(object sender, EventArgs e)
        {

        }
        public void setVisible(bool value)
        {
            pbSetting.Visible = value;
            cbSetting.Visible = value;
        }
        public void setImage(Image image)
        {
            BackgroundImage = image;
        }
        public bool getIsVisible()
        {
            return cbSetting.Checked;
        }
    }
}
