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
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.Exit_Btn = new System.Windows.Forms.Button();
            this.MinorPictureBox = new System.Windows.Forms.PictureBox();
            this.Camera2_Lable = new System.Windows.Forms.Label();
            this.Serial1_Lable = new System.Windows.Forms.Label();
            this.Serial2_Lable = new System.Windows.Forms.Label();
            this.Joystick_Lable = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.MainCameraPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MinorPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // JoystickInfoTxtBox
            // 
            this.JoystickInfoTxtBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.JoystickInfoTxtBox.Location = new System.Drawing.Point(12, 624);
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
            this.Camera1_Lable.Location = new System.Drawing.Point(12, 29);
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
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(82)))), ((int)(((byte)(136)))), ((int)(((byte)(193)))));
            this.checkBox1.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.checkBox1.FlatAppearance.BorderSize = 3;
            this.checkBox1.FlatAppearance.CheckedBackColor = System.Drawing.Color.Red;
            this.checkBox1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.checkBox1.ForeColor = System.Drawing.Color.Transparent;
            this.checkBox1.Location = new System.Drawing.Point(310, 498);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.checkBox1.Size = new System.Drawing.Size(76, 25);
            this.checkBox1.TabIndex = 4;
            this.checkBox1.Text = "Search";
            this.checkBox1.UseVisualStyleBackColor = false;
            this.checkBox1.CheckStateChanged += new System.EventHandler(this.checkBox1_CheckStateChanged);
            // 
            // Exit_Btn
            // 
            this.Exit_Btn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Exit_Btn.Location = new System.Drawing.Point(129, 639);
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
            this.Camera2_Lable.Location = new System.Drawing.Point(116, 29);
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
            this.Serial1_Lable.Location = new System.Drawing.Point(12, 50);
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
            this.Serial2_Lable.Location = new System.Drawing.Point(116, 50);
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
            this.Joystick_Lable.Location = new System.Drawing.Point(12, 71);
            this.Joystick_Lable.Name = "Joystick_Lable";
            this.Joystick_Lable.Size = new System.Drawing.Size(64, 21);
            this.Joystick_Lable.TabIndex = 20;
            this.Joystick_Lable.Text = "Joystick";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(30)))), ((int)(((byte)(40)))));
            this.ClientSize = new System.Drawing.Size(1164, 674);
            this.Controls.Add(this.Joystick_Lable);
            this.Controls.Add(this.Serial2_Lable);
            this.Controls.Add(this.Serial1_Lable);
            this.Controls.Add(this.Camera2_Lable);
            this.Controls.Add(this.Camera1_Lable);
            this.Controls.Add(this.MinorPictureBox);
            this.Controls.Add(this.Exit_Btn);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.MainCameraPictureBox);
            this.Controls.Add(this.JoystickInfoTxtBox);
            this.Name = "Form1";
            this.Text = "Form1";
            this.TopMost = true;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.MainCameraPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MinorPictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TextBox JoystickInfoTxtBox;
        private Label Camera1_Lable;
        private PictureBox MainCameraPictureBox;
        private CheckBox checkBox1;
        private Button Exit_Btn;
        private PictureBox MinorPictureBox;
        private Label Camera2_Lable;
        private Label Serial1_Lable;
        private Label Serial2_Lable;
        private Label Joystick_Lable;
    }
}