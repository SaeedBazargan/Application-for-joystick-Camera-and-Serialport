using AppForJoystickCameraAndSerial.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AppForJoystickCameraAndSerial
{
    public partial class ConfigForm : Form
    {
        public SerialPortSetting[] Settings { get; }
        public ConfigForm(SerialPortSetting[] settings)
        {
            InitializeComponent();
            Settings = settings;
        }
        private void SetAndExit_Click(object sender, EventArgs e)
        {
            SetSetting(0);
            SetSetting(1);
            Close();
        }

        private void SetSetting(int index)
        {
            var setting = new SerialPortSetting();
            var com_combo = index == 0 ? Com_ComboBox : Com_ComboBox2;
            var baud_combo = index == 0 ? Baud_ComboBox : Baud_ComboBox2;
            var dataBits_combo = index == 0 ? DataBits_ComboBox : DataBits_ComboBox2;
            if (com_combo.SelectedItem != null)
                setting.PortNumber = com_combo.SelectedItem.ToString();
            if (baud_combo.SelectedItem != null)
                setting.Baudrate = int.Parse(baud_combo.SelectedItem.ToString());
            if (dataBits_combo.SelectedItem != null)
                setting.DataBit = int.Parse(dataBits_combo.SelectedItem.ToString());
            else
            {
                //setOk = 0;
                MessageBox.Show("Please fill in the fields.", "Faild to Connect", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            Settings[index] = setting;
        }
    }
}