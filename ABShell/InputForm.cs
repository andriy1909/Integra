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
    public partial class InputForm : Form
    {
        public string Title { set { this.Text = value; } }
        public string TopText { set { label1.Text = value; } }
        public string Value
        {
            get { return textBox1.Text; }
            set { textBox1.Text = value; }
        }
        public bool UsePassword
        {
            set
            {
                if (value)
                {
                    textBox1.UseSystemPasswordChar = true;
                    cbUsePassword.Visible = true;
                }
            }
        }

        public InputForm()
        {
            InitializeComponent();
        }

        private void InputForm_Load(object sender, EventArgs e)
        {

        }

        private void cbUsePassword_CheckedChanged(object sender, EventArgs e)
        {
            textBox1.UseSystemPasswordChar = !cbUsePassword.Checked;
        }
    }
}
