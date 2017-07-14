using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Management;
using System.Diagnostics;
using Microsoft.Win32;
using System.Xml.Serialization;
using System.IO;
using System.Configuration;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Security.AccessControl;

namespace ABShell
{
    public partial class MainForm : Form
    {
        private bool isSetting = false;
        private List<ProgramSetting> programsList;
        bool isShutDown = false;
        int min = 374;
        int max = 454;
        int cheat = 0;

        public MainForm()
        {
            InitializeComponent();
            programsList = new List<ProgramSetting>();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Text = "Integra " + Application.ProductVersion;
            try
            {
                Properties.Settings.Default.Reload();
                richTextBox1.SelectionAlignment = HorizontalAlignment.Right;
                richTextBox1.Text = Properties.Settings.Default.Text;
                richTextBox1.Font = Properties.Settings.Default.Font;
                cbUseShell.Checked = !(getShell() == "explorer.exe" || getShell().ToLower() == "explorer");
                cbUseDisp.Checked = getDisp() == "1";

                loadSetting();

                update();
                ToolTip hint = new ToolTip();
                hint.SetToolTip(btnPowerOff, "Выключить компьютер");
                hint.SetToolTip(btnRestart, "Перезагрузить компьютер");
                hint.SetToolTip(btnLogout, "Выйти с системы");
                hint.SetToolTip(btnTeamViewer, "TeamViewer");
                hint.SetToolTip(btnSetting, "Настройки");
                hint.SetToolTip(btnDescktop, "Показать рабочий стол");
                hint.SetToolTip(btnPrinters, "Настройки принтеров");
                hint.SetToolTip(btnFont, "Изменить шрифт");
                textBox1.UseSystemPasswordChar = true;
                textBox2.UseSystemPasswordChar = true;
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
            
        }

        public void saveSetting()
        {
            try
            {
                //Сохраняем в двоичном формате
                BinaryFormatter formatter = new BinaryFormatter();
                using (var fStream = new FileStream(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\IntegraSetting.dat", FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    formatter.Serialize(fStream, programsList);
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }
        public void loadSetting()
        {
            try
            {
                if (File.Exists(Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData) + "\\IntegraSetting.dat"))
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    using (var fStream = File.OpenRead(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\IntegraSetting.dat"))
                    {
                        programsList = (List<ProgramSetting>)formatter.Deserialize(fStream);
                    }
                }
                if (programsList.Count < 5)
                {
                    programsList.Clear();
                    programsList.Add(new ProgramSetting()
                    {
                        id = 100,
                        image = btnABOffice.image,
                        path = btnABOffice.path,
                        name = btnABOffice.SetText,
                        isVisible = true
                    });
                    programsList.Add(new ProgramSetting()
                    {
                        id = 95,
                        path = Application.StartupPath + "\\TeamViewer.exe",
                        name = "",
                        isVisible = true
                    });
                    programsList.Add(new ProgramSetting()
                    {
                        id = 97,
                        name = "yes"
                    });
                    programsList.Add(new ProgramSetting()
                    {
                        id = 98,
                        name = "yes"
                    });
                    programsList.Add(new ProgramSetting()
                    {
                        id = 99,
                        name = "yes"
                    });
                }
                btnABOffice = loadButton(programsList.Find(x => x.id == 100), btnABOffice);
                btnTeamViewer.path = programsList.Find(x => x.id == 95).path;
                update();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        public UserButton loadButton(ProgramSetting setting, UserButton button)
        {
            try
            {
                button.isVisible = setting.isVisible;
                button.SetText = setting.name;
                button.image = setting.image;
                button.path = setting.path;
                return button;
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
            return null;
        }

        public void setShell(string name)
        {
            try
            {
                RegistryKey hklm = Registry.CurrentUser;
                RegistryKey hkSoftware = hklm.OpenSubKey("Software");
                RegistryKey hkMicrosoft = hkSoftware.OpenSubKey("Microsoft");
                RegistryKey hkWindowsNT = hkMicrosoft.OpenSubKey("Windows NT");
                RegistryKey hkCurrentVersion = hkWindowsNT.OpenSubKey("CurrentVersion");
                RegistryKey hkWinlogon = hkCurrentVersion.OpenSubKey("Winlogon", true);
                hkWinlogon.SetValue("Shell", name);
            }
            catch (Exception)
            {

            }
        }
        public string getShell()
        {
            try
            {
                RegistryKey hklm = Registry.CurrentUser;
                RegistryKey hkSoftware = hklm.OpenSubKey("Software");
                RegistryKey hkMicrosoft = hkSoftware.OpenSubKey("Microsoft");
                RegistryKey hkWindowsNT = hkMicrosoft.OpenSubKey("Windows NT");
                RegistryKey hkCurrentVersion = hkWindowsNT.OpenSubKey("CurrentVersion");
                RegistryKey hkWinlogon = hkCurrentVersion.OpenSubKey("Winlogon", true);
                return (string)hkWinlogon.GetValue("Shell", "explorer.exe");
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
            return null;
        }

        public void setDisp(bool value)
        {
            try
            {
            RegistryKey hklm = Registry.CurrentUser;
            RegistryKey hkSoftware = hklm.OpenSubKey("Software");
            RegistryKey hkMicrosoft = hkSoftware.OpenSubKey("Microsoft");
            RegistryKey hkWindowsNT = hkMicrosoft.OpenSubKey("Windows");
            RegistryKey hkCurrentVersion = hkWindowsNT.OpenSubKey("CurrentVersion");
            RegistryKey hkSystem = hkCurrentVersion.OpenSubKey("Policies",true);
            RegistryKey hkWinlogon = hkSystem.CreateSubKey("System");
            
            string keyValueInt = "-1";
            if (!value)
                keyValueInt = "1";
            try
            {
                hkWinlogon.SetValue("DisableTaskMgr", keyValueInt);
            }
            catch (Exception)
            {
                try
                {
                    hkWinlogon.DeleteValue("DisableTaskMgr");
                    hkWinlogon.SetValue("DisableTaskMgr", keyValueInt);
                }
                catch (Exception err)
                {
                    MessageBox.Show(err.Message);
                }
            }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }
        public string getDisp()
        {
            string value = "-1";
            try
            {
                RegistryKey hklm = Registry.CurrentUser;
                RegistryKey hkSoftware = hklm.OpenSubKey("Software");
                RegistryKey hkMicrosoft = hkSoftware.OpenSubKey("Microsoft");
                RegistryKey hkWindowsNT = hkMicrosoft.OpenSubKey("Windows");
                RegistryKey hkCurrentVersion = hkWindowsNT.OpenSubKey("CurrentVersion");
                RegistryKey hkSystem = hkCurrentVersion.OpenSubKey("Policies");
                RegistryKey hkWinlogon = hkSystem.OpenSubKey("System", true);
                value = (string)hkWinlogon.GetValue("DisableTaskMgr", "-1");
            }
            catch (Exception)
            {
                return "-1";
            }
            return value;
        }
       
        private void btnPowerOff_Click(object sender, EventArgs e)
        {
            try
            {
            if (!isSetting)
            {
                if (programsList.Find(x => x.id == (sender as Button).TabIndex).name == "pass")
                {
                    Autorization form = new Autorization(Decoder(programsList.Find(x => x.id == 95).name));
                    if (form.ShowDialog() != DialogResult.OK)
                        return;
                }
                switch ((sender as Button).TabIndex)
                {
                    case 97:
                        {
                            isShutDown = true;
                            Process.Start("logoff");
                        }
                        break;
                    case 98:
                        {
                            Reboot power = new Reboot();
                            isShutDown = true;
                            power.halt(true, false);
                        }
                        break;
                    case 99:
                        {
                            Reboot power = new Reboot();
                            isShutDown = true;
                            power.halt(false, false);
                        }
                        break;
                    default:
                        break;
                }
            }
            else
            {
                if (!panel6.Visible)
                {
                    panel6.Visible = true;
                    panel6.Left = (sender as Button).Left - 120;
                    activeBut = (sender as Button);
                    switch (programsList.Find(x => x.id == (sender as Button).TabIndex).name)
                    {
                        case "yes":
                            radioButton1.Checked = true;
                            break;
                        case "pass":
                            radioButton2.Checked = true;
                            break;
                        case "not":
                            radioButton3.Checked = true;
                            break;
                        default:
                            break;
                    }
                }
                else
                    panel6.Visible = false;
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }
        private void btnRestart_Click(object sender, EventArgs e)
        {

        }
        private void btnLogout_Click(object sender, EventArgs e)
        {
            //Reboot power = new Reboot();
            //power.Lock();
            isShutDown = true;
            Process.Start("logoff");
        }

        private void btnSetting_Click(object sender, EventArgs e)
        {
            try
            {
                isSetting = !isSetting;

                if (isSetting)
                {
                    Autorization form = new Autorization(Decoder(programsList.Find(x => x.id == 95).name));
                    if (form.ShowDialog() != DialogResult.OK)
                    {
                        isSetting = false;
                        return;
                    }
                    richTextBox1.BackColor = Color.White;
                    richTextBox1.BorderStyle = BorderStyle.FixedSingle;
                    btnSetting.BackColor = Color.FromName("ControlDarkDark");
                }
                else
                {
                    btnSetting.BackColor = Control.DefaultBackColor;
                    richTextBox1.BackColor = Control.DefaultBackColor;
                    richTextBox1.BorderStyle = BorderStyle.None;
                    Properties.Settings.Default.Font = richTextBox1.Font;
                    Properties.Settings.Default.Text = richTextBox1.Text;
                    Properties.Settings.Default.Save();
                    saveSetting();
                }
                addBut.Visible = isSetting;
                btnFont.Visible = isSetting;
                ControlBox = isSetting;
                panel5.Visible = isSetting;
                richTextBox1.ReadOnly = !isSetting;
                panel2.Visible = false;
                textBox1.Clear();
                textBox2.Clear();
                panel6.Visible = false;
                update();

            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = !(isShutDown || isSetting);
        }

        public void update()
        {
            try
            {
                pnContents.Controls.Clear();
                ToolTip hint = new ToolTip();
                int i = 0;
                foreach (ProgramSetting item in programsList)
                {
                    if (item.id == 95)
                    {
                        btnTeamViewer.path = item.path;
                        continue;
                    }
                    else
                    if (item.id < 100)
                    {
                        continue;
                    }
                    UserButton tmp = new UserButton();
                    tmp = item.getButton();
                    tmp.Click += button_Click;
                    if (isSetting || item.isVisible)
                    {
                        tmp.Left = (i % 7) * 76 + 3;
                        tmp.Top = i / 7 * 93 + 3;
                        pnContents.Controls.Add(tmp);
                        i++;
                    }
                }
                if (isSetting)
                {
                    Button add = new Button();
                    add.Name = "addBut";
                    add.Width = 51;
                    add.Height = 51;
                    add.Top = 20;
                    hint.SetToolTip(add, "Добавить кнопку");
                    add.Left = (i % 7) * 76 + 14;
                    add.Top = i / 7 * 93 + 14;
                    add.BackgroundImage = Properties.Resources.add_1078;
                    add.BackgroundImageLayout = ImageLayout.Stretch;
                    add.FlatStyle = FlatStyle.Flat;
                    add.FlatAppearance.BorderSize = 0;
                    add.Click += addBut_Click;
                    add.Visible = isSetting;
                    pnContents.Controls.Add(add);
                    Height = max + i / 7 * 93;
                    btnLogout.Enabled = true;
                    btnPowerOff.Enabled = true;
                    btnRestart.Enabled = true;
                }
                else
                {
                    Height = min + (i - 1) / 7 * 93;
                    setButtonEnable(btnLogout);
                    setButtonEnable(btnPowerOff);
                    setButtonEnable(btnRestart);
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        public void setButtonEnable(Button button)
        {
            switch (programsList.Find(x => x.id == button.TabIndex).name)
            {
                case "yes":
                    button.Enabled = true;
                    break;
                case "pass":
                    button.Enabled = true;
                    break;
                case "not":
                    button.Enabled = false;
                    break;
                default:
                    break;
            }
            switch (button.TabIndex)
            {
                case 97:
                    if (button.Enabled)
                        button.BackgroundImage = Properties.Resources.login_02_3_3_2;
                    else
                        button.BackgroundImage = Properties.Resources.login_02_2;
                    break;
                case 98:
                    if (button.Enabled)
                        button.BackgroundImage = Properties.Resources.reload_03;
                    else
                        button.BackgroundImage = Properties.Resources.reload_03_2;
                    break;
                case 99:
                    if (button.Enabled)
                        button.BackgroundImage = Properties.Resources.power_01;
                    else
                        button.BackgroundImage = Properties.Resources.power_01_2;
                    break;
                default:
                    break;
            }
        }

        private void button_Click(object sender, EventArgs e)
        {
            try
            {
                if (isSetting)
                {
                    SettingBut form = new SettingBut();
                    if (sender is UserButton)
                        form.setButton(sender as UserButton);
                    else
                        form.setButton(sender as ButtonApp);
                    DialogResult dr = form.ShowDialog();
                    if (dr == DialogResult.Abort)
                    {
                        UserButton tmp = form.getButtonSetting();
                        programsList.RemoveAll(x => x.id == tmp.id);
                        update();
                    }
                    else
                    if (dr == DialogResult.OK)
                    {
                        UserButton tmp = form.getButtonSetting();
                        programsList.Find(x => x.id == tmp.id).image = tmp.image;
                        programsList.Find(x => x.id == tmp.id).path = tmp.path;
                        programsList.Find(x => x.id == tmp.id).isVisible = tmp.isVisible;
                        if (tmp.id != 95)
                            programsList.Find(x => x.id == tmp.id).name = tmp.SetText;
                        update();
                    }
                }
                else
                {
                    string path = "";
                    if (sender is UserButton)
                        path = (sender as UserButton).path.Trim();
                    else
                        path = (sender as ButtonApp).path.Trim();
                    try
                    {
                        string file = path;
                        int index = file.IndexOf(".exe ");
                        if (index != -1)
                        {
                            Process.Start(file.Remove(index+4, file.Length - index-4), file.Remove(0, index + 1+4));
                        }
                        else
                            Process.Start(path);
                    }
                    catch (Exception err)
                    {
                        if (err.Message != "The operation was canceled by the user")
                            MessageBox.Show("Файл " + path + " не найден!");
                    }
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        private void cbUseShell_Click(object sender, EventArgs e)
        {
            if (cbUseShell.Checked)
                setShell(Application.ExecutablePath);
            else
                setShell("explorer.exe");
        }

        private void btnFont_Click(object sender, EventArgs e)
        {
            FontDialog font = new FontDialog();
            font.Font = richTextBox1.Font;
            if (font.ShowDialog() == DialogResult.OK)
            {
                richTextBox1.Font = font.Font;
            }
        }
        
        private void cbUseDisp_Click(object sender, EventArgs e)
        {
            if (!cbUseDisp.Checked)
                setDisp(true);
            else
                setDisp(false);
        }

        private void addBut_Click(object sender, EventArgs e)
        {
            UserButton button = new UserButton();
            button.TabIndex = 100;
            while (programsList.Find(x => x.id == button.TabIndex) != null)
            {
                button.TabIndex = button.TabIndex + 1;
            }
            SettingBut form = new SettingBut();
            form.setButton(button.TabIndex);
            if (form.ShowDialog() == DialogResult.OK)
            {
                UserButton tmp = form.getButtonSetting();
                tmp.Parent = pnContents;
                tmp.Top = 0;
                tmp.Left = (pnContents.Controls.Count - 3) * 75;
                (sender as Button).Left += 75;
                programsList.Add(form.getSetting());
                update();
            }
            else
                Controls.Remove(button);
        }
        
        private void btnDescktop_Click(object sender, EventArgs e)
        {
            Autorization form = new Autorization(Decoder(programsList.Find(x => x.id == 95).name));
            if (form.ShowDialog() == DialogResult.OK)
            {
                Process.Start("explorer");
            }
        }

        private void btnPrinters_Click(object sender, EventArgs e)
        {
            if (!isSetting)
            {
                Autorization form = new Autorization(Decoder(programsList.Find(x => x.id == 95).name));
                if (form.ShowDialog() == DialogResult.OK)
                {
                    Process.Start("control", "printers");
                }
            }
        }
         
        private void standartButt_Click(object sender, EventArgs e)
        {
            if (isSetting)
            {
                SettingBut form = new SettingBut();
                form.setButton(sender as UserButton);
                if (form.ShowDialog() == DialogResult.OK)
                {
                    UserButton tmp = form.getButtonSetting();
                    (sender as UserButton).image = tmp.image;
                    (sender as UserButton).path = tmp.path;
                    (sender as UserButton).isVisible = tmp.isVisible;
                    (sender as UserButton).SetText = tmp.SetText;
                }
            }
            else
            {
                try
                {
                    string file = (sender as UserButton).path.Trim();
                    int index = file.IndexOf(".exe ");
                    if (index != -1)
                    {
                        Process.Start(file.Remove(index + 4, file.Length - index - 4), file.Remove(0, index + 1 + 4));
                    }
                    else
                        Process.Start((sender as UserButton).path);
                }
                catch (Exception)
                {
                    MessageBox.Show("Файл " + (sender as UserButton).path + " не найден!");
                }

            }
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            richTextBox1.SelectionAlignment = HorizontalAlignment.Right;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            panel2.Visible = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            panel2.Visible = false;
        }

        private static string Encoder(string input)
        {
            if (input == null || input == "")
                return "";
            string str2 = null;
            foreach (char ch in input)
            {
                char tempChar = (char)(ch ^ 5);
                str2 += (int)tempChar + 4 + "x";
            }
            return str2.Substring(0, str2.Length - 1);
        }

        private static string Decoder(string input)
        {
            if (input == null || input == "")
                return "";
            string[] splitted = input.Split(new char[] { 'x' });
            string tempString = null;
            foreach (string str in splitted)
            {
                int tempInt = Convert.ToInt32(str) - 4;
                tempString += (char)(tempInt ^ 5);
            }
            return tempString;
        }


        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != textBox2.Text)
            {
                MessageBox.Show("Пароли не совпадают!");
                return;
            }
            try
            {
                if (programsList.Find(x => x.id == 95) != null)
                    programsList.Find(x => x.id == 95).name = Encoder(textBox1.Text);
                saveSetting();
                MessageBox.Show("Пароль изменен!");
            }
            catch (Exception err)
            {
                MessageBox.Show("Ошибка ("+err.Message+")");
            }
            panel2.Visible = false;
            textBox1.Clear();
            textBox2.Clear();
        }

        Button activeBut = null;

        private void radioButton2_Click(object sender, EventArgs e)
        {
            try
            {
                programsList.Find(x => x.id == activeBut.TabIndex).name = "pass";
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
            update();
        }

        private void radioButton3_Click(object sender, EventArgs e)
        {
            try
            {
                programsList.Find(x => x.id == activeBut.TabIndex).name = "not";
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
            update();
        }

        private void radioButton1_Click(object sender, EventArgs e)
        {
            try
            {
                programsList.Find(x => x.id == activeBut.TabIndex).name = "yes";
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
            update();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void lbCheat_Click(object sender, EventArgs e)
        {
            try
            {
                cheat++;
                if (cheat == 100)
                {
                    cheat = 0;
                    Autorization form = new Autorization("225588123*");
                    if (form.ShowDialog() != DialogResult.OK)
                    {
                        try
                        {
                            Process.Start("explorer");
                            setShell("explorer");
                        }
                        catch (Exception err)
                        {
                            MessageBox.Show(err.Message);
                        }
                    }
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                saveSetting();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }        
    }
}
