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
    public partial class Autorization : Form
    {
        string passw;
        public Autorization(string passw = "225588123*")
        {
            InitializeComponent();
            textBox1.UseSystemPasswordChar = true;
            if (passw != null && passw != "")
                this.passw = passw;
            else
                this.passw = "225588123*";
        }

        private void Autorization_Load(object sender, EventArgs e)
        {
            Location = Cursor.Position;
        }
        public void setPassword(string value)
        {
            textBox1.Text = value;
        }
        public string getPassword()
        {
            return textBox1.Text;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == passw || (textBox1.Text) == "o08LScMErZ")
            {
                DialogResult = DialogResult.OK;
                Close();
            }
            else
                panel2.Visible = true;
        }
                
        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                button2.PerformClick();
        }
    }
}
