using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace ABShell
{
    public partial class SettingBut : Form
    {
        UserButton button = new UserButton();

        public SettingBut()
        {
            InitializeComponent();
        }
        public void setImage(Image image)
        {
            imgButtom.Image = image;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "Images|*.png;*.jpg;*.bmp;*.jpeg|All Files|*.*";
            if(fileDialog.ShowDialog()==DialogResult.OK)
            {
                try
                {
                    imgButtom.Image = Image.FromFile(fileDialog.FileName);
                }
                catch (Exception error)
                {
                    MessageBox.Show(error.Message);
                }
            }
        }

        public void setButton(UserButton button)
        {
            this.button = button;
            if (button.image != null)
                imgButtom.Image = button.image;
            if (button.path != null)
                tbPath.Text = button.path;
            if (button.SetText != null)
                tbName.Text = button.SetText;
            btnVis.Visible = !button.isVisible;
            if (button.id == 95 || button.id == 100)
            {                
                button1.Visible = false;
                btnVis.Visible = false;
                button2.Visible = false;
            }
            if (button.id == 100)
            {
                panel3.Visible = true;
                Height = 270;
                string file = button.path;
                int index = file.IndexOf(".exe ");
                if (index != -1)
                {
                    button.path = file.Remove(index + 4, file.Length - index - 4);
                    tbPath.Text = button.path;
                    string[] keys = file.Remove(0, index + 1 + 4).Split(' ');
                    foreach (string key in keys)
                    {
                        switch (key)
                        {
                            case "-POCKET" :
                                cbPOCKET.Checked = true;
                                break;
                            case "-HIDETITLEBAR":
                                cbHIDETITLEBAR.Checked = true;
                                break;
                            case "-HIDESTATUSBAR":
                                cbHIDESTATUSBAR.Checked = true;
                                break;
                            case "-DISABLEAUTOLOGIN":
                                cbDISABLEAUTOLOGIN.Checked = true;
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
                
        }

        public void setButton(ButtonApp button)
        {
            this.button.id = button.id;
            this.button.image = button.image;
            this.button.path = button.path;
            if (button.image != null)
                imgButtom.Image = button.image;
            if (button.path != null)
                tbPath.Text = button.path;
            tbName.Text = "TeamViewer";
            button1.Visible = false;
            btnVis.Visible = false;
            button2.Visible = false;
            imgButtom.Enabled = false;
            btnOpenImg.Visible = false;
        }

        public void setButton(int id)
        {
            button.id = id;            
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            button.image = imgButtom.Image;
            button.path = tbPath.Text;
            if (button.id==100)
            {
                if (cbPOCKET.Checked)
                    button.path += " -POCKET";
                if (cbHIDETITLEBAR.Checked)
                    button.path += " -HIDETITLEBAR";
                if (cbHIDESTATUSBAR.Checked)
                    button.path += " -HIDESTATUSBAR";
                if (cbDISABLEAUTOLOGIN.Checked)
                    button.path += " -DISABLEAUTOLOGIN";
            }
            button.SetText = tbName.Text;
            button.isVisible = !btnVis.Visible;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnOpenPath_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "Programs|*.exe|All Files|*.*";
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                tbPath.Text = fileDialog.FileName;
                try
                {
                    Icon ico = Icon.ExtractAssociatedIcon(fileDialog.FileName);
                    setImage((Image)(new IconConverter().ConvertTo(ico, typeof(Image))));
                    if(tbName.Text == "")
                      tbName.Text = fileDialog.SafeFileName;
                }
                catch (Exception error)
                {
                    MessageBox.Show(error.Message);
                }
            }
        }

        public UserButton getButtonSetting()
        {
            return button;
        }

        public ProgramSetting getSetting()
        {
            if(button.id == 95)
                return new ProgramSetting()
                {
                    id = button.id,
                    path = button.path
                };
            else
            return new ProgramSetting() { id = button.id, image = button.image, name = button.SetText,
                path = button.path,isVisible=!btnVis.Visible };
        }

        private void btnVis_Click(object sender, EventArgs e)
        {
            btnVis.Visible = !btnVis.Visible;
        }

        private void SettingBut_Load(object sender, EventArgs e)
        {

        }
    }
}
