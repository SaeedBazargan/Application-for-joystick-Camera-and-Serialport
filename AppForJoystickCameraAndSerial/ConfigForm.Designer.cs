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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.SerialMonitoring_TextBox = new System.Windows.Forms.TextBox();
            this.SetAndExit = new System.Windows.Forms.Button();
            this.DataBits_ComboBox2 = new System.Windows.Forms.ComboBox();
            this.Com_ComboBox = new System.Windows.Forms.ComboBox();
            this.Baud_ComboBox2 = new System.Windows.Forms.ComboBox();
            this.Baud_ComboBox = new System.Windows.Forms.ComboBox();
            this.Com_ComboBox2 = new System.Windows.Forms.ComboBox();
            this.Baudrate_Label = new System.Windows.Forms.Label();
            this.PortNumber_Label = new System.Windows.Forms.Label();
            this.DataBits_Label = new System.Windows.Forms.Label();
            this.DataBits_ComboBox = new System.Windows.Forms.ComboBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.SerialMonitoring_TextBox);
            this.groupBox1.Controls.Add(this.SetAndExit);
            this.groupBox1.Controls.Add(this.DataBits_ComboBox2);
            this.groupBox1.Controls.Add(this.Com_ComboBox);
            this.groupBox1.Controls.Add(this.Baud_ComboBox2);
            this.groupBox1.Controls.Add(this.Baud_ComboBox);
            this.groupBox1.Controls.Add(this.Com_ComboBox2);
            this.groupBox1.Controls.Add(this.Baudrate_Label);
            this.groupBox1.Controls.Add(this.PortNumber_Label);
            this.groupBox1.Controls.Add(this.DataBits_Label);
            this.groupBox1.Controls.Add(this.DataBits_ComboBox);
            this.groupBox1.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.groupBox1.ForeColor = System.Drawing.Color.White;
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(340, 252);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Serial Configuration";
            // 
            // SerialMonitoring_TextBox
            // 
            this.SerialMonitoring_TextBox.Location = new System.Drawing.Point(6, 123);
            this.SerialMonitoring_TextBox.Multiline = true;
            this.SerialMonitoring_TextBox.Name = "SerialMonitoring_TextBox";
            this.SerialMonitoring_TextBox.Size = new System.Drawing.Size(323, 93);
            this.SerialMonitoring_TextBox.TabIndex = 51;
            // 
            // SetAndExit
            // 
            this.SetAndExit.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.SetAndExit.ForeColor = System.Drawing.SystemColors.ControlText;
            this.SetAndExit.Location = new System.Drawing.Point(6, 222);
            this.SetAndExit.Name = "SetAndExit";
            this.SetAndExit.Size = new System.Drawing.Size(80, 23);
            this.SetAndExit.TabIndex = 50;
            this.SetAndExit.Text = "&Set && Exit";
            this.SetAndExit.UseVisualStyleBackColor = true;
            this.SetAndExit.Click += new System.EventHandler(this.SetAndExit_Click);
            // 
            // DataBits_ComboBox2
            // 
            this.DataBits_ComboBox2.FormattingEnabled = true;
            this.DataBits_ComboBox2.Items.AddRange(new object[] {
            "7",
            "8"});
            this.DataBits_ComboBox2.Location = new System.Drawing.Point(208, 92);
            this.DataBits_ComboBox2.Name = "DataBits_ComboBox2";
            this.DataBits_ComboBox2.Size = new System.Drawing.Size(121, 25);
            this.DataBits_ComboBox2.TabIndex = 48;
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
            this.Com_ComboBox.Location = new System.Drawing.Point(85, 24);
            this.Com_ComboBox.Name = "Com_ComboBox";
            this.Com_ComboBox.Size = new System.Drawing.Size(121, 25);
            this.Com_ComboBox.TabIndex = 42;
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
            this.Baud_ComboBox2.Location = new System.Drawing.Point(208, 58);
            this.Baud_ComboBox2.Name = "Baud_ComboBox2";
            this.Baud_ComboBox2.Size = new System.Drawing.Size(121, 25);
            this.Baud_ComboBox2.TabIndex = 49;
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
            this.Baud_ComboBox.Location = new System.Drawing.Point(85, 58);
            this.Baud_ComboBox.Name = "Baud_ComboBox";
            this.Baud_ComboBox.Size = new System.Drawing.Size(121, 25);
            this.Baud_ComboBox.TabIndex = 44;
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
            this.Com_ComboBox2.Location = new System.Drawing.Point(208, 24);
            this.Com_ComboBox2.Name = "Com_ComboBox2";
            this.Com_ComboBox2.Size = new System.Drawing.Size(121, 25);
            this.Com_ComboBox2.TabIndex = 47;
            // 
            // Baudrate_Label
            // 
            this.Baudrate_Label.AutoSize = true;
            this.Baudrate_Label.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Baudrate_Label.ForeColor = System.Drawing.Color.White;
            this.Baudrate_Label.Location = new System.Drawing.Point(6, 61);
            this.Baudrate_Label.Name = "Baudrate_Label";
            this.Baudrate_Label.Size = new System.Drawing.Size(70, 19);
            this.Baudrate_Label.TabIndex = 43;
            this.Baudrate_Label.Text = "BaudRate:";
            // 
            // PortNumber_Label
            // 
            this.PortNumber_Label.AutoSize = true;
            this.PortNumber_Label.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.PortNumber_Label.ForeColor = System.Drawing.Color.White;
            this.PortNumber_Label.Location = new System.Drawing.Point(6, 27);
            this.PortNumber_Label.Name = "PortNumber_Label";
            this.PortNumber_Label.Size = new System.Drawing.Size(45, 19);
            this.PortNumber_Label.TabIndex = 41;
            this.PortNumber_Label.Text = "COM:";
            // 
            // DataBits_Label
            // 
            this.DataBits_Label.AutoSize = true;
            this.DataBits_Label.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.DataBits_Label.ForeColor = System.Drawing.Color.White;
            this.DataBits_Label.Location = new System.Drawing.Point(6, 95);
            this.DataBits_Label.Name = "DataBits_Label";
            this.DataBits_Label.Size = new System.Drawing.Size(67, 19);
            this.DataBits_Label.TabIndex = 45;
            this.DataBits_Label.Text = "Data bits:";
            // 
            // DataBits_ComboBox
            // 
            this.DataBits_ComboBox.FormattingEnabled = true;
            this.DataBits_ComboBox.Items.AddRange(new object[] {
            "7",
            "8"});
            this.DataBits_ComboBox.Location = new System.Drawing.Point(85, 92);
            this.DataBits_ComboBox.Name = "DataBits_ComboBox";
            this.DataBits_ComboBox.Size = new System.Drawing.Size(121, 25);
            this.DataBits_ComboBox.TabIndex = 46;
            // 
            // ConfigForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(30)))), ((int)(((byte)(40)))));
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.groupBox1);
            this.Name = "ConfigForm";
            this.Text = "ConfigForm";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private GroupBox groupBox1;
        private Button SetAndExit;
        private ComboBox DataBits_ComboBox2;
        private ComboBox Com_ComboBox;
        private ComboBox Baud_ComboBox2;
        private ComboBox Baud_ComboBox;
        private ComboBox Com_ComboBox2;
        private Label Baudrate_Label;
        private Label PortNumber_Label;
        private Label DataBits_Label;
        private ComboBox DataBits_ComboBox;
        private TextBox SerialMonitoring_TextBox;
    }
}