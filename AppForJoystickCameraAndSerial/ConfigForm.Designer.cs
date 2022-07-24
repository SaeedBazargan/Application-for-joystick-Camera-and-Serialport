namespace AppForJoystickCameraAndSerial
{
    partial class ConfigForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox9 = new System.Windows.Forms.GroupBox();
            this.SerialLogBrowse_Button = new System.Windows.Forms.Button();
            this.DataBits_ComboBox2 = new System.Windows.Forms.ComboBox();
            this.Baud_ComboBox2 = new System.Windows.Forms.ComboBox();
            this.Com_ComboBox2 = new System.Windows.Forms.ComboBox();
            this.ClosePort_Button = new System.Windows.Forms.Button();
            this.SetSetting_Button = new System.Windows.Forms.Button();
            this.OpenPort_Button = new System.Windows.Forms.Button();
            this.PortNumber_Label = new System.Windows.Forms.Label();
            this.DataBits_ComboBox = new System.Windows.Forms.ComboBox();
            this.Com_ComboBox = new System.Windows.Forms.ComboBox();
            this.DataBits_Label = new System.Windows.Forms.Label();
            this.Baudrate_Label = new System.Windows.Forms.Label();
            this.Baud_ComboBox = new System.Windows.Forms.ComboBox();
            this.Set_Button = new System.Windows.Forms.Button();
            this.groupBox9.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox9
            // 
            this.groupBox9.Controls.Add(this.SerialLogBrowse_Button);
            this.groupBox9.Controls.Add(this.DataBits_ComboBox2);
            this.groupBox9.Controls.Add(this.Baud_ComboBox2);
            this.groupBox9.Controls.Add(this.Com_ComboBox2);
            this.groupBox9.Controls.Add(this.ClosePort_Button);
            this.groupBox9.Controls.Add(this.SetSetting_Button);
            this.groupBox9.Controls.Add(this.OpenPort_Button);
            this.groupBox9.Controls.Add(this.PortNumber_Label);
            this.groupBox9.Controls.Add(this.DataBits_ComboBox);
            this.groupBox9.Controls.Add(this.Com_ComboBox);
            this.groupBox9.Controls.Add(this.DataBits_Label);
            this.groupBox9.Controls.Add(this.Baudrate_Label);
            this.groupBox9.Controls.Add(this.Baud_ComboBox);
            this.groupBox9.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.groupBox9.ForeColor = System.Drawing.Color.White;
            this.groupBox9.Location = new System.Drawing.Point(12, 12);
            this.groupBox9.Name = "groupBox9";
            this.groupBox9.Size = new System.Drawing.Size(335, 131);
            this.groupBox9.TabIndex = 36;
            this.groupBox9.TabStop = false;
            this.groupBox9.Text = "Serial Configuration";
            // 
            // SerialLogBrowse_Button
            // 
            this.SerialLogBrowse_Button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.SerialLogBrowse_Button.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.SerialLogBrowse_Button.ForeColor = System.Drawing.SystemColors.ControlText;
            this.SerialLogBrowse_Button.Location = new System.Drawing.Point(3, 311);
            this.SerialLogBrowse_Button.Name = "SerialLogBrowse_Button";
            this.SerialLogBrowse_Button.Size = new System.Drawing.Size(75, 23);
            this.SerialLogBrowse_Button.TabIndex = 45;
            this.SerialLogBrowse_Button.Text = "&Browse";
            this.SerialLogBrowse_Button.UseVisualStyleBackColor = true;
            // 
            // DataBits_ComboBox2
            // 
            this.DataBits_ComboBox2.FormattingEnabled = true;
            this.DataBits_ComboBox2.Items.AddRange(new object[] {
            "7",
            "8"});
            this.DataBits_ComboBox2.Location = new System.Drawing.Point(208, 95);
            this.DataBits_ComboBox2.Name = "DataBits_ComboBox2";
            this.DataBits_ComboBox2.Size = new System.Drawing.Size(121, 25);
            this.DataBits_ComboBox2.TabIndex = 40;
            // 
            // Baud_ComboBox2
            // 
            this.Baud_ComboBox2.FormattingEnabled = true;
            this.Baud_ComboBox2.Items.AddRange(new object[] {
            "300",
            "600",
            "1200",
            "1800",
            "2400",
            "7200",
            "9600",
            "14400",
            "19200",
            "57600",
            "115200",
            "230400",
            "460800",
            "921600"});
            this.Baud_ComboBox2.Location = new System.Drawing.Point(208, 61);
            this.Baud_ComboBox2.Name = "Baud_ComboBox2";
            this.Baud_ComboBox2.Size = new System.Drawing.Size(121, 25);
            this.Baud_ComboBox2.TabIndex = 40;
            // 
            // Com_ComboBox2
            // 
            this.Com_ComboBox2.FormattingEnabled = true;
            this.Com_ComboBox2.Items.AddRange(new object[] {
            "COM1",
            "COM2",
            "COM3",
            "COM4",
            "COM5",
            "COM6",
            "COM7",
            "COM8",
            "COM9",
            "COM10",
            "COM11",
            "COM12",
            "COM13",
            "COM14",
            "COM15",
            "COM16",
            "COM17",
            "COM18",
            "COM19",
            "COM20"});
            this.Com_ComboBox2.Location = new System.Drawing.Point(208, 27);
            this.Com_ComboBox2.Name = "Com_ComboBox2";
            this.Com_ComboBox2.Size = new System.Drawing.Size(121, 25);
            this.Com_ComboBox2.TabIndex = 39;
            // 
            // ClosePort_Button
            // 
            this.ClosePort_Button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ClosePort_Button.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.ClosePort_Button.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ClosePort_Button.Location = new System.Drawing.Point(254, 341);
            this.ClosePort_Button.Name = "ClosePort_Button";
            this.ClosePort_Button.Size = new System.Drawing.Size(75, 23);
            this.ClosePort_Button.TabIndex = 38;
            this.ClosePort_Button.Text = "&Close Port";
            this.ClosePort_Button.UseVisualStyleBackColor = true;
            // 
            // SetSetting_Button
            // 
            this.SetSetting_Button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.SetSetting_Button.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.SetSetting_Button.ForeColor = System.Drawing.SystemColors.ControlText;
            this.SetSetting_Button.Location = new System.Drawing.Point(3, 341);
            this.SetSetting_Button.Name = "SetSetting_Button";
            this.SetSetting_Button.Size = new System.Drawing.Size(75, 23);
            this.SetSetting_Button.TabIndex = 37;
            this.SetSetting_Button.Text = "&Set Setting";
            this.SetSetting_Button.UseVisualStyleBackColor = true;
            // 
            // OpenPort_Button
            // 
            this.OpenPort_Button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.OpenPort_Button.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.OpenPort_Button.ForeColor = System.Drawing.SystemColors.ControlText;
            this.OpenPort_Button.Location = new System.Drawing.Point(126, 341);
            this.OpenPort_Button.Name = "OpenPort_Button";
            this.OpenPort_Button.Size = new System.Drawing.Size(75, 23);
            this.OpenPort_Button.TabIndex = 36;
            this.OpenPort_Button.Text = "&Open Port";
            this.OpenPort_Button.UseVisualStyleBackColor = true;
            // 
            // PortNumber_Label
            // 
            this.PortNumber_Label.AutoSize = true;
            this.PortNumber_Label.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.PortNumber_Label.ForeColor = System.Drawing.Color.White;
            this.PortNumber_Label.Location = new System.Drawing.Point(6, 30);
            this.PortNumber_Label.Name = "PortNumber_Label";
            this.PortNumber_Label.Size = new System.Drawing.Size(45, 19);
            this.PortNumber_Label.TabIndex = 29;
            this.PortNumber_Label.Text = "COM:";
            // 
            // DataBits_ComboBox
            // 
            this.DataBits_ComboBox.FormattingEnabled = true;
            this.DataBits_ComboBox.Items.AddRange(new object[] {
            "7",
            "8"});
            this.DataBits_ComboBox.Location = new System.Drawing.Point(85, 95);
            this.DataBits_ComboBox.Name = "DataBits_ComboBox";
            this.DataBits_ComboBox.Size = new System.Drawing.Size(121, 25);
            this.DataBits_ComboBox.TabIndex = 34;
            // 
            // Com_ComboBox
            // 
            this.Com_ComboBox.FormattingEnabled = true;
            this.Com_ComboBox.Items.AddRange(new object[] {
            "COM1",
            "COM2",
            "COM3",
            "COM4",
            "COM5",
            "COM6",
            "COM7",
            "COM8",
            "COM9",
            "COM10",
            "COM11",
            "COM12",
            "COM13",
            "COM14",
            "COM15",
            "COM16",
            "COM17",
            "COM18",
            "COM19",
            "COM20"});
            this.Com_ComboBox.Location = new System.Drawing.Point(85, 27);
            this.Com_ComboBox.Name = "Com_ComboBox";
            this.Com_ComboBox.Size = new System.Drawing.Size(121, 25);
            this.Com_ComboBox.TabIndex = 30;
            // 
            // DataBits_Label
            // 
            this.DataBits_Label.AutoSize = true;
            this.DataBits_Label.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.DataBits_Label.ForeColor = System.Drawing.Color.White;
            this.DataBits_Label.Location = new System.Drawing.Point(6, 98);
            this.DataBits_Label.Name = "DataBits_Label";
            this.DataBits_Label.Size = new System.Drawing.Size(67, 19);
            this.DataBits_Label.TabIndex = 33;
            this.DataBits_Label.Text = "Data bits:";
            // 
            // Baudrate_Label
            // 
            this.Baudrate_Label.AutoSize = true;
            this.Baudrate_Label.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Baudrate_Label.ForeColor = System.Drawing.Color.White;
            this.Baudrate_Label.Location = new System.Drawing.Point(6, 64);
            this.Baudrate_Label.Name = "Baudrate_Label";
            this.Baudrate_Label.Size = new System.Drawing.Size(70, 19);
            this.Baudrate_Label.TabIndex = 31;
            this.Baudrate_Label.Text = "BaudRate:";
            // 
            // Baud_ComboBox
            // 
            this.Baud_ComboBox.FormattingEnabled = true;
            this.Baud_ComboBox.Items.AddRange(new object[] {
            "300",
            "600",
            "1200",
            "1800",
            "2400",
            "7200",
            "9600",
            "14400",
            "19200",
            "57600",
            "115200",
            "230400",
            "460800",
            "921600"});
            this.Baud_ComboBox.Location = new System.Drawing.Point(85, 61);
            this.Baud_ComboBox.Name = "Baud_ComboBox";
            this.Baud_ComboBox.Size = new System.Drawing.Size(121, 25);
            this.Baud_ComboBox.TabIndex = 32;
            // 
            // Set_Button
            // 
            this.Set_Button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Set_Button.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Set_Button.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Set_Button.Location = new System.Drawing.Point(10, 415);
            this.Set_Button.Name = "Set_Button";
            this.Set_Button.Size = new System.Drawing.Size(75, 23);
            this.Set_Button.TabIndex = 38;
            this.Set_Button.Text = "&Set";
            this.Set_Button.UseVisualStyleBackColor = true;
            this.Set_Button.Click += new System.EventHandler(this.Set_Button_Click);
            // 
            // ConfigForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(30)))), ((int)(((byte)(40)))));
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.Set_Button);
            this.Controls.Add(this.groupBox9);
            this.Name = "ConfigForm";
            this.Text = "ConfigForm";
            this.groupBox9.ResumeLayout(false);
            this.groupBox9.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private GroupBox groupBox9;
        private Button SerialLogBrowse_Button;
        private ComboBox DataBits_ComboBox2;
        private ComboBox Baud_ComboBox2;
        private ComboBox Com_ComboBox2;
        private Button ClosePort_Button;
        private Button SetSetting_Button;
        private Button OpenPort_Button;
        private Label PortNumber_Label;
        private ComboBox DataBits_ComboBox;
        private ComboBox Com_ComboBox;
        private Label DataBits_Label;
        private Label Baudrate_Label;
        private ComboBox Baud_ComboBox;
        private Button Set_Button;
    }
}