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
        public ConfigForm()
        {
            InitializeComponent();
        }

        private void Login_Button_Click(object sender, EventArgs e)
        {
            if (Username_TextBox.Text == "admin" && Password_TextBox.Text == "admin")
            {
                MessageBox.Show("Logged in successfully");
                Close();
            }
            else
                MessageBox.Show("Wrong username or password", "Faild to login", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
