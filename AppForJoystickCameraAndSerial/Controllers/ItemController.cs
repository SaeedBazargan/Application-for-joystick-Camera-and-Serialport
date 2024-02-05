using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace AppForJoystickCameraAndSerial.Controllers
{
    public class ItemController
    {
        public void CheckBox_Enable(bool Status, bool uiThread = true, params CheckBox[] _checkBox)
        {
            foreach (CheckBox checkBox in _checkBox)
            {
                if (uiThread)
                {
                    checkBox.Enabled = Status;
                }
                else
                {
                    checkBox.Invoke((MethodInvoker)delegate ()
                    {
                        checkBox.Enabled = Status;
                    });

                }
            }
        }
        public void CheckBox_Checked(bool Status, bool uiThread = true, params CheckBox[] _checkBox)
        {
            foreach (CheckBox checkBox in _checkBox)
            {
                if (uiThread)
                {
                    checkBox.Checked = Status;
                }
                else
                {
                    checkBox.Invoke((MethodInvoker)delegate ()
                    {
                        checkBox.Checked = Status;
                    });

                }

            }
        }

        public void RadioButton_Checked(bool Status,bool uiThread = true, params RadioButton[] _radioButton)
        {
            foreach (RadioButton radioButton in _radioButton)
            {
                if (uiThread)
                {
                    radioButton.Checked = Status;
                }
                else
                {
                    radioButton.Invoke((MethodInvoker)delegate ()
                    {
                        radioButton.Checked = Status;
                    });

                }
            }
        }

        public void RadioButton_Enable(bool Status, bool uiThread = true, params RadioButton[] _radioButton)
        {
            foreach (RadioButton radioButton in _radioButton)
            {
                if (uiThread)
                {
                    radioButton.Enabled = Status;
                }
                else
                {
                    radioButton.Invoke((MethodInvoker)delegate ()
                    {
                        radioButton.Enabled = Status;
                    });

                }
            }
        }

        public void Button_Enable(bool Status, bool uiThread = true, params Button[] _button)
        {
            foreach (Button button in _button)
            {
                if (uiThread)
                {
                    button.Enabled = Status;
                }
                else
                {
                    button.Invoke((MethodInvoker)delegate ()
                    {
                        button.Enabled = Status;
                    });

                }
            }
        }

        public void Numeric_Enable(bool Status, bool uiThread = true, params NumericUpDown[] _numericUpDown)
        {
            foreach (NumericUpDown numericUpDown in _numericUpDown)
            {
                if (uiThread)
                {
                    numericUpDown.Enabled = Status;
                }
                else
                {
                    numericUpDown.Invoke((MethodInvoker)delegate ()
                    {
                        numericUpDown.Enabled = Status;
                    });

                }
            }
        }
    }
}
