namespace TFC_Garage
{
    partial class MainForm
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
            this.COM_PortComboBox = new System.Windows.Forms.ComboBox();
            this.ConnectButton = new System.Windows.Forms.Button();
            this.CameraPlot = new OxyPlot.WindowsForms.Plot();
            this.DisplayCamera0_CB = new System.Windows.Forms.CheckBox();
            this.DisplayCamera1_CB = new System.Windows.Forms.CheckBox();
            this.TCP_ConnectButton = new System.Windows.Forms.Button();
            this.IP_TB = new System.Windows.Forms.TextBox();
            this.TCP_PORT_TB = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.CameraDataSelect_CB = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // COM_PortComboBox
            // 
            this.COM_PortComboBox.FormattingEnabled = true;
            this.COM_PortComboBox.Location = new System.Drawing.Point(12, 12);
            this.COM_PortComboBox.Name = "COM_PortComboBox";
            this.COM_PortComboBox.Size = new System.Drawing.Size(121, 21);
            this.COM_PortComboBox.TabIndex = 0;
            // 
            // ConnectButton
            // 
            this.ConnectButton.Location = new System.Drawing.Point(139, 10);
            this.ConnectButton.Name = "ConnectButton";
            this.ConnectButton.Size = new System.Drawing.Size(121, 23);
            this.ConnectButton.TabIndex = 1;
            this.ConnectButton.Text = "button1";
            this.ConnectButton.UseVisualStyleBackColor = true;
            this.ConnectButton.Click += new System.EventHandler(this.ConnectButton_Click);
            // 
            // CameraPlot
            // 
            this.CameraPlot.KeyboardPanHorizontalStep = 0.1D;
            this.CameraPlot.KeyboardPanVerticalStep = 0.1D;
            this.CameraPlot.Location = new System.Drawing.Point(12, 91);
            this.CameraPlot.Name = "CameraPlot";
            this.CameraPlot.PanCursor = System.Windows.Forms.Cursors.Hand;
            this.CameraPlot.Size = new System.Drawing.Size(1088, 640);
            this.CameraPlot.TabIndex = 2;
            this.CameraPlot.Text = "plot1";
            this.CameraPlot.ZoomHorizontalCursor = System.Windows.Forms.Cursors.SizeWE;
            this.CameraPlot.ZoomRectangleCursor = System.Windows.Forms.Cursors.SizeNWSE;
            this.CameraPlot.ZoomVerticalCursor = System.Windows.Forms.Cursors.SizeNS;
            this.CameraPlot.Click += new System.EventHandler(this.plot1_Click);
            // 
            // DisplayCamera0_CB
            // 
            this.DisplayCamera0_CB.AutoSize = true;
            this.DisplayCamera0_CB.Checked = true;
            this.DisplayCamera0_CB.CheckState = System.Windows.Forms.CheckState.Checked;
            this.DisplayCamera0_CB.Location = new System.Drawing.Point(290, 16);
            this.DisplayCamera0_CB.Name = "DisplayCamera0_CB";
            this.DisplayCamera0_CB.Size = new System.Drawing.Size(108, 17);
            this.DisplayCamera0_CB.TabIndex = 3;
            this.DisplayCamera0_CB.Text = "Display Camera 0";
            this.DisplayCamera0_CB.UseVisualStyleBackColor = true;
            // 
            // DisplayCamera1_CB
            // 
            this.DisplayCamera1_CB.AutoSize = true;
            this.DisplayCamera1_CB.Location = new System.Drawing.Point(421, 16);
            this.DisplayCamera1_CB.Name = "DisplayCamera1_CB";
            this.DisplayCamera1_CB.Size = new System.Drawing.Size(108, 17);
            this.DisplayCamera1_CB.TabIndex = 4;
            this.DisplayCamera1_CB.Text = "Display Camera 1";
            this.DisplayCamera1_CB.UseVisualStyleBackColor = true;
            // 
            // TCP_ConnectButton
            // 
            this.TCP_ConnectButton.Location = new System.Drawing.Point(282, 61);
            this.TCP_ConnectButton.Name = "TCP_ConnectButton";
            this.TCP_ConnectButton.Size = new System.Drawing.Size(116, 23);
            this.TCP_ConnectButton.TabIndex = 5;
            this.TCP_ConnectButton.Text = "TCP Connect";
            this.TCP_ConnectButton.UseVisualStyleBackColor = true;
            this.TCP_ConnectButton.Click += new System.EventHandler(this.TCP_ConnectButton_Click);
            // 
            // IP_TB
            // 
            this.IP_TB.Location = new System.Drawing.Point(12, 61);
            this.IP_TB.Name = "IP_TB";
            this.IP_TB.Size = new System.Drawing.Size(121, 20);
            this.IP_TB.TabIndex = 6;
            this.IP_TB.Text = "1.2.3.4";
            this.IP_TB.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // TCP_PORT_TB
            // 
            this.TCP_PORT_TB.Location = new System.Drawing.Point(139, 61);
            this.TCP_PORT_TB.Name = "TCP_PORT_TB";
            this.TCP_PORT_TB.Size = new System.Drawing.Size(121, 20);
            this.TCP_PORT_TB.TabIndex = 7;
            this.TCP_PORT_TB.Text = "2000";
            this.TCP_PORT_TB.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(136, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(26, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Port";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "IP Address";
            // 
            // CameraDataSelect_CB
            // 
            this.CameraDataSelect_CB.FormattingEnabled = true;
            this.CameraDataSelect_CB.Items.AddRange(new object[] {
            "RAW",
            "DERIVATIVE",
            "THRESHOLDED"});
            this.CameraDataSelect_CB.Location = new System.Drawing.Point(545, 14);
            this.CameraDataSelect_CB.Name = "CameraDataSelect_CB";
            this.CameraDataSelect_CB.Size = new System.Drawing.Size(121, 21);
            this.CameraDataSelect_CB.TabIndex = 10;
            this.CameraDataSelect_CB.SelectedIndexChanged += new System.EventHandler(this.CameraDataSelect_CB_SelectedIndexChanged);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1112, 819);
            this.Controls.Add(this.CameraDataSelect_CB);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.TCP_PORT_TB);
            this.Controls.Add(this.IP_TB);
            this.Controls.Add(this.TCP_ConnectButton);
            this.Controls.Add(this.DisplayCamera1_CB);
            this.Controls.Add(this.DisplayCamera0_CB);
            this.Controls.Add(this.CameraPlot);
            this.Controls.Add(this.ConnectButton);
            this.Controls.Add(this.COM_PortComboBox);
            this.Name = "MainForm";
            this.Text = "TFC_Garage";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Resize += new System.EventHandler(this.MainForm_Resize);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox COM_PortComboBox;
        private System.Windows.Forms.Button ConnectButton;
        private OxyPlot.WindowsForms.Plot CameraPlot;
        private System.Windows.Forms.CheckBox DisplayCamera0_CB;
        private System.Windows.Forms.CheckBox DisplayCamera1_CB;
        private System.Windows.Forms.Button TCP_ConnectButton;
        private System.Windows.Forms.TextBox IP_TB;
        private System.Windows.Forms.TextBox TCP_PORT_TB;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox CameraDataSelect_CB;
    }
}

