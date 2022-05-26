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
            ((System.ComponentModel.ISupportInitialize)(this.MainCameraPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MinorPictureBox)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
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
            this.Exit_Btn.Location = new System.Drawing.Point(1077, 639);
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
            this.MinorPictureBox.Size = new System.Drawing.Size(175, 154);
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
            this.groupBox3.Text = "Port Monitoring";
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
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(30)))), ((int)(((byte)(40)))));
            this.ClientSize = new System.Drawing.Size(1164, 674);
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
    }
}