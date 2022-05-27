namespace AppForJoystickCameraAndSerial
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.JoystickInfoTxtBox = new System.Windows.Forms.TextBox();
            this.Camera1_Lable = new System.Windows.Forms.Label();
            this.MainCameraPictureBox = new System.Windows.Forms.PictureBox();
            this.SearchCheckBox = new System.Windows.Forms.CheckBox();
            this.Exit_Btn = new System.Windows.Forms.Button();
            this.MinorPictureBox = new System.Windows.Forms.PictureBox();
            this.Camera2_Lable = new System.Windows.Forms.Label();
            this.Serial1_Lable = new System.Windows.Forms.Label();
            this.Serial2_Lable = new System.Windows.Forms.Label();
            this.Joystick_Lable = new System.Windows.Forms.Label();
            this.ReceiveDataCheckBox = new System.Windows.Forms.CheckBox();
            this.SendDataCheckBox = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.RadarRange_TextBox = new System.Windows.Forms.TextBox();
            this.Range_Label = new System.Windows.Forms.Label();
            this.Theta_TextBox = new System.Windows.Forms.TextBox();
            this.Theta_Label = new System.Windows.Forms.Label();
            this.Phi_TextBox = new System.Windows.Forms.TextBox();
            this.Phi_Label = new System.Windows.Forms.Label();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.Az_TextBox = new System.Windows.Forms.TextBox();
            this.Az_Label = new System.Windows.Forms.Label();
            this.Ay_TextBox = new System.Windows.Forms.TextBox();
            this.Ay_Label = new System.Windows.Forms.Label();
            this.Ax_TextBox = new System.Windows.Forms.TextBox();
            this.AX_Lable = new System.Windows.Forms.Label();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.EiError_TextBox = new System.Windows.Forms.TextBox();
            this.AzError_TextBox = new System.Windows.Forms.TextBox();
            this.EiError_Label = new System.Windows.Forms.Label();
            this.AzError_Label = new System.Windows.Forms.Label();
            this.ProcessMode_TextBox = new System.Windows.Forms.TextBox();
            this.ProcessMode_Label = new System.Windows.Forms.Label();
            this.Fov_TextBox = new System.Windows.Forms.TextBox();
            this.FOV_Lable = new System.Windows.Forms.Label();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.LrfRange_TextBox = new System.Windows.Forms.TextBox();
            this.LrfRange_Label = new System.Windows.Forms.Label();
            this.LrfStatus_Label = new System.Windows.Forms.Label();
            this.SerialPortSetting_Btn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.MainCameraPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MinorPictureBox)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.SuspendLayout();
            // 
            // JoystickInfoTxtBox
            // 
            this.JoystickInfoTxtBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.JoystickInfoTxtBox.Location = new System.Drawing.Point(6, 22);
            this.JoystickInfoTxtBox.Multiline = true;
            this.JoystickInfoTxtBox.Name = "JoystickInfoTxtBox";
            this.JoystickInfoTxtBox.Size = new System.Drawing.Size(111, 38);
            this.JoystickInfoTxtBox.TabIndex = 0;
            // 
            // Camera1_Lable
            // 
            this.Camera1_Lable.AutoSize = true;
            this.Camera1_Lable.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Camera1_Lable.ForeColor = System.Drawing.Color.White;
            this.Camera1_Lable.Location = new System.Drawing.Point(6, 19);
            this.Camera1_Lable.Name = "Camera1_Lable";
            this.Camera1_Lable.Size = new System.Drawing.Size(77, 21);
            this.Camera1_Lable.TabIndex = 1;
            this.Camera1_Lable.Text = "Camera 1";
            // 
            // MainCameraPictureBox
            // 
            this.MainCameraPictureBox.Image = global::AppForJoystickCameraAndSerial.Properties.Resources.wesley_tingey_mvLyHPRGLCs_unsplash;
            this.MainCameraPictureBox.Location = new System.Drawing.Point(310, 12);
            this.MainCameraPictureBox.Name = "MainCameraPictureBox";
            this.MainCameraPictureBox.Size = new System.Drawing.Size(640, 480);
            this.MainCameraPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.MainCameraPictureBox.TabIndex = 2;
            this.MainCameraPictureBox.TabStop = false;
            // 
            // SearchCheckBox
            // 
            this.SearchCheckBox.AutoSize = true;
            this.SearchCheckBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(82)))), ((int)(((byte)(136)))), ((int)(((byte)(193)))));
            this.SearchCheckBox.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.SearchCheckBox.FlatAppearance.BorderSize = 3;
            this.SearchCheckBox.FlatAppearance.CheckedBackColor = System.Drawing.Color.Red;
            this.SearchCheckBox.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.SearchCheckBox.ForeColor = System.Drawing.Color.Transparent;
            this.SearchCheckBox.Location = new System.Drawing.Point(310, 498);
            this.SearchCheckBox.Name = "SearchCheckBox";
            this.SearchCheckBox.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.SearchCheckBox.Size = new System.Drawing.Size(76, 25);
            this.SearchCheckBox.TabIndex = 4;
            this.SearchCheckBox.Text = "Search";
            this.SearchCheckBox.UseVisualStyleBackColor = false;
            this.SearchCheckBox.CheckStateChanged += new System.EventHandler(this.SearchCheckBox_CheckStateChanged);
            // 
            // Exit_Btn
            // 
            this.Exit_Btn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Exit_Btn.Location = new System.Drawing.Point(1089, 639);
            this.Exit_Btn.Name = "Exit_Btn";
            this.Exit_Btn.Size = new System.Drawing.Size(75, 23);
            this.Exit_Btn.TabIndex = 15;
            this.Exit_Btn.Text = "&Exit";
            this.Exit_Btn.UseVisualStyleBackColor = true;
            this.Exit_Btn.Click += new System.EventHandler(this.Exit_Btn_Click);
            // 
            // MinorPictureBox
            // 
            this.MinorPictureBox.Image = global::AppForJoystickCameraAndSerial.Properties.Resources.wesley_tingey_mvLyHPRGLCs_unsplash;
            this.MinorPictureBox.Location = new System.Drawing.Point(326, 29);
            this.MinorPictureBox.Name = "MinorPictureBox";
            this.MinorPictureBox.Size = new System.Drawing.Size(320, 240);
            this.MinorPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.MinorPictureBox.TabIndex = 16;
            this.MinorPictureBox.TabStop = false;
            // 
            // Camera2_Lable
            // 
            this.Camera2_Lable.AutoSize = true;
            this.Camera2_Lable.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Camera2_Lable.ForeColor = System.Drawing.Color.White;
            this.Camera2_Lable.Location = new System.Drawing.Point(110, 19);
            this.Camera2_Lable.Name = "Camera2_Lable";
            this.Camera2_Lable.Size = new System.Drawing.Size(77, 21);
            this.Camera2_Lable.TabIndex = 17;
            this.Camera2_Lable.Text = "Camera 2";
            // 
            // Serial1_Lable
            // 
            this.Serial1_Lable.AutoSize = true;
            this.Serial1_Lable.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Serial1_Lable.ForeColor = System.Drawing.Color.White;
            this.Serial1_Lable.Location = new System.Drawing.Point(6, 40);
            this.Serial1_Lable.Name = "Serial1_Lable";
            this.Serial1_Lable.Size = new System.Drawing.Size(94, 21);
            this.Serial1_Lable.TabIndex = 18;
            this.Serial1_Lable.Text = "Serial Port 1";
            // 
            // Serial2_Lable
            // 
            this.Serial2_Lable.AutoSize = true;
            this.Serial2_Lable.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Serial2_Lable.ForeColor = System.Drawing.Color.White;
            this.Serial2_Lable.Location = new System.Drawing.Point(110, 40);
            this.Serial2_Lable.Name = "Serial2_Lable";
            this.Serial2_Lable.Size = new System.Drawing.Size(94, 21);
            this.Serial2_Lable.TabIndex = 19;
            this.Serial2_Lable.Text = "Serial Port 2";
            // 
            // Joystick_Lable
            // 
            this.Joystick_Lable.AutoSize = true;
            this.Joystick_Lable.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Joystick_Lable.ForeColor = System.Drawing.Color.White;
            this.Joystick_Lable.Location = new System.Drawing.Point(6, 61);
            this.Joystick_Lable.Name = "Joystick_Lable";
            this.Joystick_Lable.Size = new System.Drawing.Size(64, 21);
            this.Joystick_Lable.TabIndex = 20;
            this.Joystick_Lable.Text = "Joystick";
            // 
            // ReceiveDataCheckBox
            // 
            this.ReceiveDataCheckBox.AutoSize = true;
            this.ReceiveDataCheckBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(82)))), ((int)(((byte)(136)))), ((int)(((byte)(193)))));
            this.ReceiveDataCheckBox.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.ReceiveDataCheckBox.FlatAppearance.BorderSize = 3;
            this.ReceiveDataCheckBox.FlatAppearance.CheckedBackColor = System.Drawing.Color.Red;
            this.ReceiveDataCheckBox.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.ReceiveDataCheckBox.ForeColor = System.Drawing.Color.Transparent;
            this.ReceiveDataCheckBox.Location = new System.Drawing.Point(6, 53);
            this.ReceiveDataCheckBox.Name = "ReceiveDataCheckBox";
            this.ReceiveDataCheckBox.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ReceiveDataCheckBox.Size = new System.Drawing.Size(118, 25);
            this.ReceiveDataCheckBox.TabIndex = 21;
            this.ReceiveDataCheckBox.Text = "Receive Data";
            this.ReceiveDataCheckBox.UseVisualStyleBackColor = false;
            // 
            // SendDataCheckBox
            // 
            this.SendDataCheckBox.AutoSize = true;
            this.SendDataCheckBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(82)))), ((int)(((byte)(136)))), ((int)(((byte)(193)))));
            this.SendDataCheckBox.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.SendDataCheckBox.FlatAppearance.BorderSize = 3;
            this.SendDataCheckBox.FlatAppearance.CheckedBackColor = System.Drawing.Color.Red;
            this.SendDataCheckBox.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.SendDataCheckBox.ForeColor = System.Drawing.Color.Transparent;
            this.SendDataCheckBox.Location = new System.Drawing.Point(6, 22);
            this.SendDataCheckBox.Name = "SendDataCheckBox";
            this.SendDataCheckBox.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.SendDataCheckBox.Size = new System.Drawing.Size(100, 25);
            this.SendDataCheckBox.TabIndex = 22;
            this.SendDataCheckBox.Text = "Send Data";
            this.SendDataCheckBox.UseVisualStyleBackColor = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox1.Controls.Add(this.SendDataCheckBox);
            this.groupBox1.Controls.Add(this.ReceiveDataCheckBox);
            this.groupBox1.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.groupBox1.ForeColor = System.Drawing.Color.White;
            this.groupBox1.Location = new System.Drawing.Point(4, 498);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(181, 88);
            this.groupBox1.TabIndex = 23;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Serial Port";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.Camera1_Lable);
            this.groupBox3.Controls.Add(this.Camera2_Lable);
            this.groupBox3.Controls.Add(this.Serial1_Lable);
            this.groupBox3.Controls.Add(this.Joystick_Lable);
            this.groupBox3.Controls.Add(this.Serial2_Lable);
            this.groupBox3.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.groupBox3.ForeColor = System.Drawing.Color.White;
            this.groupBox3.Location = new System.Drawing.Point(12, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(230, 100);
            this.groupBox3.TabIndex = 25;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Port Status";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox2.Controls.Add(this.JoystickInfoTxtBox);
            this.groupBox2.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.groupBox2.ForeColor = System.Drawing.Color.White;
            this.groupBox2.Location = new System.Drawing.Point(4, 592);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(181, 70);
            this.groupBox2.TabIndex = 26;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Joystick Result";
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox4.Controls.Add(this.groupBox8);
            this.groupBox4.Controls.Add(this.groupBox6);
            this.groupBox4.Controls.Add(this.groupBox5);
            this.groupBox4.Controls.Add(this.groupBox7);
            this.groupBox4.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.groupBox4.ForeColor = System.Drawing.Color.White;
            this.groupBox4.Location = new System.Drawing.Point(959, 12);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(205, 518);
            this.groupBox4.TabIndex = 27;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Monitoring";
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.RadarRange_TextBox);
            this.groupBox8.Controls.Add(this.Range_Label);
            this.groupBox8.Controls.Add(this.Theta_TextBox);
            this.groupBox8.Controls.Add(this.Theta_Label);
            this.groupBox8.Controls.Add(this.Phi_TextBox);
            this.groupBox8.Controls.Add(this.Phi_Label);
            this.groupBox8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(82)))), ((int)(((byte)(136)))), ((int)(((byte)(193)))));
            this.groupBox8.Location = new System.Drawing.Point(8, 395);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(188, 116);
            this.groupBox8.TabIndex = 31;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "Radar";
            // 
            // RadarRange_TextBox
            // 
            this.RadarRange_TextBox.Location = new System.Drawing.Point(81, 80);
            this.RadarRange_TextBox.Name = "RadarRange_TextBox";
            this.RadarRange_TextBox.Size = new System.Drawing.Size(100, 25);
            this.RadarRange_TextBox.TabIndex = 45;
            // 
            // Range_Label
            // 
            this.Range_Label.AutoSize = true;
            this.Range_Label.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Range_Label.ForeColor = System.Drawing.Color.White;
            this.Range_Label.Location = new System.Drawing.Point(7, 83);
            this.Range_Label.Name = "Range_Label";
            this.Range_Label.Size = new System.Drawing.Size(50, 19);
            this.Range_Label.TabIndex = 46;
            this.Range_Label.Text = "Range:";
            // 
            // Theta_TextBox
            // 
            this.Theta_TextBox.Location = new System.Drawing.Point(81, 49);
            this.Theta_TextBox.Name = "Theta_TextBox";
            this.Theta_TextBox.Size = new System.Drawing.Size(100, 25);
            this.Theta_TextBox.TabIndex = 43;
            // 
            // Theta_Label
            // 
            this.Theta_Label.AutoSize = true;
            this.Theta_Label.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Theta_Label.ForeColor = System.Drawing.Color.White;
            this.Theta_Label.Location = new System.Drawing.Point(7, 52);
            this.Theta_Label.Name = "Theta_Label";
            this.Theta_Label.Size = new System.Drawing.Size(46, 19);
            this.Theta_Label.TabIndex = 44;
            this.Theta_Label.Text = "Theta:";
            // 
            // Phi_TextBox
            // 
            this.Phi_TextBox.Location = new System.Drawing.Point(81, 18);
            this.Phi_TextBox.Name = "Phi_TextBox";
            this.Phi_TextBox.Size = new System.Drawing.Size(100, 25);
            this.Phi_TextBox.TabIndex = 41;
            // 
            // Phi_Label
            // 
            this.Phi_Label.AutoSize = true;
            this.Phi_Label.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Phi_Label.ForeColor = System.Drawing.Color.White;
            this.Phi_Label.Location = new System.Drawing.Point(7, 21);
            this.Phi_Label.Name = "Phi_Label";
            this.Phi_Label.Size = new System.Drawing.Size(31, 19);
            this.Phi_Label.TabIndex = 42;
            this.Phi_Label.Text = "Phi:";
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.Az_TextBox);
            this.groupBox6.Controls.Add(this.Az_Label);
            this.groupBox6.Controls.Add(this.Ay_TextBox);
            this.groupBox6.Controls.Add(this.Ay_Label);
            this.groupBox6.Controls.Add(this.Ax_TextBox);
            this.groupBox6.Controls.Add(this.AX_Lable);
            this.groupBox6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(82)))), ((int)(((byte)(136)))), ((int)(((byte)(193)))));
            this.groupBox6.Location = new System.Drawing.Point(8, 189);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(188, 117);
            this.groupBox6.TabIndex = 29;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Axes Angles";
            // 
            // Az_TextBox
            // 
            this.Az_TextBox.Location = new System.Drawing.Point(82, 83);
            this.Az_TextBox.Name = "Az_TextBox";
            this.Az_TextBox.Size = new System.Drawing.Size(100, 25);
            this.Az_TextBox.TabIndex = 39;
            // 
            // Az_Label
            // 
            this.Az_Label.AutoSize = true;
            this.Az_Label.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Az_Label.ForeColor = System.Drawing.Color.White;
            this.Az_Label.Location = new System.Drawing.Point(8, 86);
            this.Az_Label.Name = "Az_Label";
            this.Az_Label.Size = new System.Drawing.Size(29, 19);
            this.Az_Label.TabIndex = 40;
            this.Az_Label.Text = "AZ:";
            // 
            // Ay_TextBox
            // 
            this.Ay_TextBox.Location = new System.Drawing.Point(82, 52);
            this.Ay_TextBox.Name = "Ay_TextBox";
            this.Ay_TextBox.Size = new System.Drawing.Size(100, 25);
            this.Ay_TextBox.TabIndex = 37;
            // 
            // Ay_Label
            // 
            this.Ay_Label.AutoSize = true;
            this.Ay_Label.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Ay_Label.ForeColor = System.Drawing.Color.White;
            this.Ay_Label.Location = new System.Drawing.Point(8, 55);
            this.Ay_Label.Name = "Ay_Label";
            this.Ay_Label.Size = new System.Drawing.Size(28, 19);
            this.Ay_Label.TabIndex = 38;
            this.Ay_Label.Text = "AY:";
            // 
            // Ax_TextBox
            // 
            this.Ax_TextBox.Location = new System.Drawing.Point(82, 21);
            this.Ax_TextBox.Name = "Ax_TextBox";
            this.Ax_TextBox.Size = new System.Drawing.Size(100, 25);
            this.Ax_TextBox.TabIndex = 36;
            // 
            // AX_Lable
            // 
            this.AX_Lable.AutoSize = true;
            this.AX_Lable.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.AX_Lable.ForeColor = System.Drawing.Color.White;
            this.AX_Lable.Location = new System.Drawing.Point(8, 24);
            this.AX_Lable.Name = "AX_Lable";
            this.AX_Lable.Size = new System.Drawing.Size(29, 19);
            this.AX_Lable.TabIndex = 36;
            this.AX_Lable.Text = "AX:";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.EiError_TextBox);
            this.groupBox5.Controls.Add(this.AzError_TextBox);
            this.groupBox5.Controls.Add(this.EiError_Label);
            this.groupBox5.Controls.Add(this.AzError_Label);
            this.groupBox5.Controls.Add(this.ProcessMode_TextBox);
            this.groupBox5.Controls.Add(this.ProcessMode_Label);
            this.groupBox5.Controls.Add(this.Fov_TextBox);
            this.groupBox5.Controls.Add(this.FOV_Lable);
            this.groupBox5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(82)))), ((int)(((byte)(136)))), ((int)(((byte)(193)))));
            this.groupBox5.Location = new System.Drawing.Point(8, 24);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(188, 159);
            this.groupBox5.TabIndex = 28;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Camera && Processing";
            // 
            // EiError_TextBox
            // 
            this.EiError_TextBox.Location = new System.Drawing.Point(82, 125);
            this.EiError_TextBox.Name = "EiError_TextBox";
            this.EiError_TextBox.Size = new System.Drawing.Size(100, 25);
            this.EiError_TextBox.TabIndex = 35;
            // 
            // AzError_TextBox
            // 
            this.AzError_TextBox.Location = new System.Drawing.Point(82, 94);
            this.AzError_TextBox.Name = "AzError_TextBox";
            this.AzError_TextBox.Size = new System.Drawing.Size(100, 25);
            this.AzError_TextBox.TabIndex = 34;
            // 
            // EiError_Label
            // 
            this.EiError_Label.AutoSize = true;
            this.EiError_Label.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.EiError_Label.ForeColor = System.Drawing.Color.White;
            this.EiError_Label.Location = new System.Drawing.Point(8, 128);
            this.EiError_Label.Name = "EiError_Label";
            this.EiError_Label.Size = new System.Drawing.Size(57, 19);
            this.EiError_Label.TabIndex = 33;
            this.EiError_Label.Text = "EI Error:";
            // 
            // AzError_Label
            // 
            this.AzError_Label.AutoSize = true;
            this.AzError_Label.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.AzError_Label.ForeColor = System.Drawing.Color.White;
            this.AzError_Label.Location = new System.Drawing.Point(8, 97);
            this.AzError_Label.Name = "AzError_Label";
            this.AzError_Label.Size = new System.Drawing.Size(63, 19);
            this.AzError_Label.TabIndex = 32;
            this.AzError_Label.Text = "AZ Error:";
            // 
            // ProcessMode_TextBox
            // 
            this.ProcessMode_TextBox.Location = new System.Drawing.Point(132, 54);
            this.ProcessMode_TextBox.Name = "ProcessMode_TextBox";
            this.ProcessMode_TextBox.Size = new System.Drawing.Size(50, 25);
            this.ProcessMode_TextBox.TabIndex = 31;
            // 
            // ProcessMode_Label
            // 
            this.ProcessMode_Label.AutoSize = true;
            this.ProcessMode_Label.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.ProcessMode_Label.ForeColor = System.Drawing.Color.White;
            this.ProcessMode_Label.Location = new System.Drawing.Point(8, 57);
            this.ProcessMode_Label.Name = "ProcessMode_Label";
            this.ProcessMode_Label.Size = new System.Drawing.Size(98, 19);
            this.ProcessMode_Label.TabIndex = 30;
            this.ProcessMode_Label.Text = "Process Mode:";
            // 
            // Fov_TextBox
            // 
            this.Fov_TextBox.Location = new System.Drawing.Point(82, 21);
            this.Fov_TextBox.Name = "Fov_TextBox";
            this.Fov_TextBox.Size = new System.Drawing.Size(100, 25);
            this.Fov_TextBox.TabIndex = 29;
            // 
            // FOV_Lable
            // 
            this.FOV_Lable.AutoSize = true;
            this.FOV_Lable.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.FOV_Lable.ForeColor = System.Drawing.Color.White;
            this.FOV_Lable.Location = new System.Drawing.Point(8, 24);
            this.FOV_Lable.Name = "FOV_Lable";
            this.FOV_Lable.Size = new System.Drawing.Size(39, 19);
            this.FOV_Lable.TabIndex = 28;
            this.FOV_Lable.Text = "FOV:";
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.LrfRange_TextBox);
            this.groupBox7.Controls.Add(this.LrfRange_Label);
            this.groupBox7.Controls.Add(this.LrfStatus_Label);
            this.groupBox7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(82)))), ((int)(((byte)(136)))), ((int)(((byte)(193)))));
            this.groupBox7.Location = new System.Drawing.Point(8, 312);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(188, 77);
            this.groupBox7.TabIndex = 30;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "LRF";
            // 
            // LrfRange_TextBox
            // 
            this.LrfRange_TextBox.Location = new System.Drawing.Point(81, 18);
            this.LrfRange_TextBox.Name = "LrfRange_TextBox";
            this.LrfRange_TextBox.Size = new System.Drawing.Size(100, 25);
            this.LrfRange_TextBox.TabIndex = 41;
            // 
            // LrfRange_Label
            // 
            this.LrfRange_Label.AutoSize = true;
            this.LrfRange_Label.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.LrfRange_Label.ForeColor = System.Drawing.Color.White;
            this.LrfRange_Label.Location = new System.Drawing.Point(7, 21);
            this.LrfRange_Label.Name = "LrfRange_Label";
            this.LrfRange_Label.Size = new System.Drawing.Size(50, 19);
            this.LrfRange_Label.TabIndex = 42;
            this.LrfRange_Label.Text = "Range:";
            // 
            // LrfStatus_Label
            // 
            this.LrfStatus_Label.AutoSize = true;
            this.LrfStatus_Label.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.LrfStatus_Label.ForeColor = System.Drawing.Color.White;
            this.LrfStatus_Label.Location = new System.Drawing.Point(77, 46);
            this.LrfStatus_Label.Name = "LrfStatus_Label";
            this.LrfStatus_Label.Size = new System.Drawing.Size(47, 19);
            this.LrfStatus_Label.TabIndex = 43;
            this.LrfStatus_Label.Text = "Status";
            // 
            // SerialPortSetting_Btn
            // 
            this.SerialPortSetting_Btn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.SerialPortSetting_Btn.Location = new System.Drawing.Point(12, 118);
            this.SerialPortSetting_Btn.Name = "SerialPortSetting_Btn";
            this.SerialPortSetting_Btn.Size = new System.Drawing.Size(116, 23);
            this.SerialPortSetting_Btn.TabIndex = 28;
            this.SerialPortSetting_Btn.Text = "&SerialPort Setting";
            this.SerialPortSetting_Btn.UseVisualStyleBackColor = true;
            this.SerialPortSetting_Btn.Click += new System.EventHandler(this.SerialPortSetting_Btn_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(30)))), ((int)(((byte)(40)))));
            this.ClientSize = new System.Drawing.Size(1176, 674);
            this.Controls.Add(this.SerialPortSetting_Btn);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.MinorPictureBox);
            this.Controls.Add(this.Exit_Btn);
            this.Controls.Add(this.SearchCheckBox);
            this.Controls.Add(this.MainCameraPictureBox);
            this.Name = "Form1";
            this.Text = "Form1";
            this.TopMost = true;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.MainCameraPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MinorPictureBox)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TextBox JoystickInfoTxtBox;
        private Label Camera1_Lable;
        private PictureBox MainCameraPictureBox;
        private CheckBox SearchCheckBox;
        private Button Exit_Btn;
        private PictureBox MinorPictureBox;
        private Label Camera2_Lable;
        private Label Serial1_Lable;
        private Label Serial2_Lable;
        private Label Joystick_Lable;
        private CheckBox ReceiveDataCheckBox;
        private CheckBox SendDataCheckBox;
        private GroupBox groupBox1;
        private GroupBox groupBox3;
        private GroupBox groupBox2;
        private GroupBox groupBox4;
        private GroupBox groupBox5;
        private Label EiError_Label;
        private Label AzError_Label;
        private TextBox ProcessMode_TextBox;
        private Label ProcessMode_Label;
        private TextBox Fov_TextBox;
        private Label FOV_Lable;
        private TextBox EiError_TextBox;
        private TextBox AzError_TextBox;
        private GroupBox groupBox6;
        private TextBox Ax_TextBox;
        private Label AX_Lable;
        private TextBox Az_TextBox;
        private Label Az_Label;
        private TextBox Ay_TextBox;
        private Label Ay_Label;
        private GroupBox groupBox8;
        private TextBox RadarRange_TextBox;
        private Label Range_Label;
        private TextBox Theta_TextBox;
        private Label Theta_Label;
        private TextBox Phi_TextBox;
        private Label Phi_Label;
        private GroupBox groupBox7;
        private TextBox LrfRange_TextBox;
        private Label LrfRange_Label;
        private Label LrfStatus_Label;
        private Button SerialPortSetting_Btn;
    }
}