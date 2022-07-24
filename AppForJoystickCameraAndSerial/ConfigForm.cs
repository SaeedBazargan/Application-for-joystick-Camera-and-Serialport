using AppForJoystickCameraAndSerial.Controllers;

namespace AppForJoystickCameraAndSerial
{
    public partial class ConfigForm : Form
    {
        private readonly SerialController _serialController;

        int Baudrate, DataBit;
        string PortNumber;
        int SerialPortIndex;

        public ConfigForm()
        {
            InitializeComponent();
            _serialController = new SerialController();
        }
        public void TransferringData(int SerialIndex)
        {
            SerialPortIndex = SerialIndex;
        }
        private void Set_Button_Click(object sender, EventArgs e)
        {
            byte setOk = 1;
            if (SerialPortIndex == 0)
            {
                if (Com_ComboBox.SelectedItem != null)
                    PortNumber = Com_ComboBox.SelectedItem.ToString();
                if (Baud_ComboBox.SelectedItem != null)
                    Baudrate = int.Parse(Baud_ComboBox.SelectedItem.ToString());
                if (DataBits_ComboBox.SelectedItem != null)
                    DataBit = int.Parse(DataBits_ComboBox.SelectedItem.ToString());
                else
                {
                    setOk = 0;
                    MessageBox.Show("Please fill in the fields.", "Faild to Connect", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                _serialController.SetConfiguration_Port(PortNumber, Baudrate, DataBit);
            }
            if (SerialPortIndex == 1)
            {
                if (Com_ComboBox2.SelectedItem != null)
                    PortNumber = Com_ComboBox2.SelectedItem.ToString();
                if (Baud_ComboBox2.SelectedItem != null)
                    Baudrate = int.Parse(Baud_ComboBox2.SelectedItem.ToString());
                if (DataBits_ComboBox2.SelectedItem != null)
                    DataBit = int.Parse(DataBits_ComboBox2.SelectedItem.ToString());
                else
                {
                    setOk = 0;
                    MessageBox.Show("Please fill in the fields.", "Faild to Connect", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                _serialController.SetConfiguration_Port(PortNumber, Baudrate, DataBit);
            }
            if (setOk == 1)
                MessageBox.Show("Serial settings saved!");
        }
    }
}
