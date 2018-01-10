using NativeWifi;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Windows.Forms;

namespace ABShell
{
    public partial class WiFiListForm : Form
    {

        public WiFiListForm()
        {
            InitializeComponent();
        }

        private void WiFiListForm_Load(object sender, EventArgs e)
        {
            WlanClient client = new WlanClient();
            foreach (WlanClient.WlanInterface wlanIface in client.Interfaces)
            {
                Wlan.WlanAvailableNetwork[] wlanBssEntries = wlanIface.GetAvailableNetworkList(0);

                string[] list = new string[100];
                int i = 0;
                dgvData.Rows.Clear();
                foreach (Wlan.WlanAvailableNetwork network in wlanBssEntries)
                {
                    string name = Encoding.ASCII.GetString(network.dot11Ssid.SSID).Trim((char)0);
                    if (name != "" && !list.Contains(name))
                    {
                        list[i] = name;
                        i++;
                        dgvData.Rows.Add(name, network.wlanSignalQuality.ToString() + "%");
                    }
                }
            }
        }
        /// <summary>
        /// Gets the MAC address of the current PC.
        /// </summary>
        /// <returns></returns>
        public PhysicalAddress GetMacAddress()
        {
            foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
            {
                // Only consider Ethernet network interfaces
                if (nic.NetworkInterfaceType == NetworkInterfaceType.Ethernet &&
                    nic.OperationalStatus == OperationalStatus.Up)
                {
                    return nic.GetPhysicalAddress();
                }
            }
            return null;
        }

        private void btConnect_Click(object sender, EventArgs e)
        {
            if (dgvData.RowCount == 0 || dgvData.SelectedRows.Count == 0)
            {
                MessageBox.Show("Нет сетей Wi-Fi для подключения!");
                return;
            }
            try
            {
                WlanClient client = new WlanClient();

                foreach (WlanClient.WlanInterface wlanIface in client.Interfaces)
                {
                    Wlan.WlanAvailableNetwork[] wlanBssEntries = wlanIface.GetAvailableNetworkList(0);
                    foreach (Wlan.WlanAvailableNetwork network in wlanBssEntries)
                    {
                        string profileName = Encoding.ASCII.GetString(network.dot11Ssid.SSID).Trim((char)0);

                        // подключаемся именно к выбранной сети
                        if (dgvData.SelectedRows[0].Cells[0].Value.Equals(profileName))
                        {
                            string strTemplate = "";
                            string authentication = "";

                            authentication = "WPA2PSK";// network.dot11DefaultAuthAlgorithm.ToString();
                            //strTemplate = Properties.Resources.WPA2PSK;
                            string encryption = network.dot11DefaultCipherAlgorithm.ToString().Trim((char)0);

                            // пароль к сети
                            string key = "777777777777";

                            //string mac = GetMacAddress().ToString();
                            //string profileXml = string.Format("<?xml version=\"1.0\"?><WLANProfile xmlns=\"http://www.microsoft.com/networking/WLAN/profile/v1\"><name>{0}</name><SSIDConfig><SSID><hex>{1}</hex><name>{0}</name></SSID></SSIDConfig><connectionType>ESS</connectionType><MSM><security><authEncryption><authentication>open</authentication><encryption>WEP</encryption><useOneX>false</useOneX></authEncryption><sharedKey><keyType>networkKey</keyType><protected>false</protected><keyMaterial>{2}</keyMaterial></sharedKey><keyIndex>0</keyIndex></security></MSM></WLANProfile>", profileName, mac, key);
                            //wlanIface.SetProfile(Wlan.WlanProfileFlags.AllUser, profileXml, true);
                            //wlanIface.Connect(Wlan.WlanConnectionMode.Profile, Wlan.Dot11BssType.Any, profileName);


                            string hex = profileName;
                            byte[] hy = Encoding.Unicode.GetBytes(hex);
                            string hex2 = BitConverter.ToString(hy);
                            hex = hex2.Replace("-", "");
                            hex = hex.Replace("00", "");

                            string xml="";

                            foreach (Wlan.WlanProfileInfo profileInfo in wlanIface.GetProfiles())
                            {
                                xml = wlanIface.GetProfileXml(profileInfo.profileName);
                            }
                          //  string profileXml = string.Format(strTemplate, profileName, authentication, encryption, key, hex);

                            wlanIface.SetProfile(Wlan.WlanProfileFlags.AllUser, xml, true);
                            wlanIface.Connect(Wlan.WlanConnectionMode.Profile, Wlan.Dot11BssType.Any, profileName);

                            MessageBox.Show("Подключено к "+ profileName);
                            Close();
                            return;                        //string profileXml = string.Format(strTemplate, profileName, authentication, encryption, key);

                            // wlanIface.SetProfile(Wlan.WlanProfileFlags.AllUser, profileXml, true);
                            //wlanIface.Connect(Wlan.WlanConnectionMode.Profile, Wlan.Dot11BssType.Any, profileName);

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при подключении, подключится возможно только к сети с сохраненным паролем!");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            WlanClient client = new WlanClient();
            foreach (WlanClient.WlanInterface wlanIface in client.Interfaces)
            {
                Wlan.WlanAvailableNetwork[] wlanBssEntries = wlanIface.GetAvailableNetworkList(0);

                string[] list = new string[100];
                int i = 0;
                dgvData.Rows.Clear();
                foreach (Wlan.WlanAvailableNetwork network in wlanBssEntries)
                {
                    string name = Encoding.ASCII.GetString(network.dot11Ssid.SSID).Trim((char)0);
                    if (name != "" && !list.Contains(name))
                    {
                        list[i] = name;
                        i++;
                        dgvData.Rows.Add(name, network.wlanSignalQuality.ToString() + "%");
                    }
                }
            }
        }

        private void dgvData_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            btConnect.PerformClick();
        }
    }
}
