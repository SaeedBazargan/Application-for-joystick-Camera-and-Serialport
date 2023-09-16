using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppForJoystickCameraAndSerial.Controllers
{
    public class ItemController
    {
        public void CheckBox_Enable(bool Status, params CheckBox[] _checkBox)
        {
            foreach (CheckBox checkBox in _checkBox)
            {
                checkBox.Enabled = Status;
            }
        }

        public void CheckBox_Checked(bool Status, params CheckBox[] _checkBox)
        {
            foreach (CheckBox checkBox in _checkBox)
            {
                checkBox.Checked = Status;
            }
        }

        public void RadioButton_Checked(bool Status, params RadioButton[] _radioButton)
        {
            foreach (RadioButton radioButton in _radioButton)
            {
                radioButton.Checked = Status;
            }
        }

        public void RadioButton_Enable(bool Status, params RadioButton[] _radioButton)
        {
            foreach (RadioButton radioButton in _radioButton)
            {
                radioButton.Enabled = Status;
            }
        }

        public void Button_Enable(bool Status, params Button[] _button)
        {
            foreach (Button button in _button)
            {
                button.Enabled = Status;
            }
        }

        public void Numeric_Enable(bool Status, params NumericUpDown[] _numericUpDown)
        {
            foreach (NumericUpDown numericUpDown in _numericUpDown)
            {
                numericUpDown.Enabled = Status;
            }
        }
    }
}
