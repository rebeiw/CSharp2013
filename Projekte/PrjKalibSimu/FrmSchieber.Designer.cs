namespace PrjKalibSimu
{
    partial class FrmSchieber
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmSchieber));
            this.hScrollBar1 = new System.Windows.Forms.HScrollBar();
            this.BTN_Ende = new Helper.BitButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.LEDClose = new Helper.LedRectangle();
            this.LEDOpen = new Helper.LedRectangle();
            this.BTN_Close = new Helper.BitButton();
            this.BTNOpen = new Helper.BitButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.LEDMode = new Helper.LedRectangle();
            this.BTN_Mode = new Helper.BitButton();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.TXTState = new Helper.TxtBox();
            this.ValveState = new Helper.Valve();
            ((System.ComponentModel.ISupportInitialize)(this.BTN_Ende)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.LEDClose)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LEDOpen)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BTN_Close)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BTNOpen)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.LEDMode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BTN_Mode)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ValveState)).BeginInit();
            this.SuspendLayout();
            // 
            // hScrollBar1
            // 
            this.hScrollBar1.LargeChange = 1;
            this.hScrollBar1.Location = new System.Drawing.Point(5, 375);
            this.hScrollBar1.Minimum = 50;
            this.hScrollBar1.Name = "hScrollBar1";
            this.hScrollBar1.Size = new System.Drawing.Size(181, 37);
            this.hScrollBar1.TabIndex = 65;
            this.hScrollBar1.Value = 50;
            this.hScrollBar1.Scroll += new System.Windows.Forms.ScrollEventHandler(this.hScrollBar1_Scroll);
            // 
            // BTN_Ende
            // 
            this.BTN_Ende.Caption = "";
            this.BTN_Ende.EnableMouseDown = false;
            this.BTN_Ende.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold);
            this.BTN_Ende.Formular = null;
            this.BTN_Ende.Location = new System.Drawing.Point(56, 314);
            this.BTN_Ende.Name = "BTN_Ende";
            this.BTN_Ende.Picture_0 = Helper.BtnStyle.btg_Exit;
            this.BTN_Ende.Picture_1 = Helper.BtnStyle.btg_Blanko;
            this.BTN_Ende.Picture_2 = Helper.BtnStyle.btg_Blanko;
            this.BTN_Ende.Picture_3 = Helper.BtnStyle.btg_Blanko;
            this.BTN_Ende.Picture_4 = Helper.BtnStyle.btg_Blanko;
            this.BTN_Ende.PictureNumber = 0;
            this.BTN_Ende.Size = new System.Drawing.Size(79, 48);
            this.BTN_Ende.Symbol = null;
            this.BTN_Ende.TabIndex = 66;
            this.BTN_Ende.TabStop = false;
            this.BTN_Ende.Click += new System.EventHandler(this.BTN_FrmClose_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.LEDClose);
            this.groupBox1.Controls.Add(this.LEDOpen);
            this.groupBox1.Controls.Add(this.BTN_Close);
            this.groupBox1.Controls.Add(this.BTNOpen);
            this.groupBox1.Location = new System.Drawing.Point(6, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(179, 137);
            this.groupBox1.TabIndex = 69;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Kommando";
            // 
            // LEDClose
            // 
            this.LEDClose.BackColor = System.Drawing.Color.DarkGreen;
            this.LEDClose.Location = new System.Drawing.Point(92, 80);
            this.LEDClose.Margin = new System.Windows.Forms.Padding(4);
            this.LEDClose.Name = "LEDClose";
            this.LEDClose.Size = new System.Drawing.Size(79, 39);
            this.LEDClose.State = Helper.LEDState.LEDOff;
            this.LEDClose.TabIndex = 71;
            this.LEDClose.TabStop = false;
            // 
            // LEDOpen
            // 
            this.LEDOpen.BackColor = System.Drawing.Color.DarkGreen;
            this.LEDOpen.Location = new System.Drawing.Point(92, 26);
            this.LEDOpen.Margin = new System.Windows.Forms.Padding(4);
            this.LEDOpen.Name = "LEDOpen";
            this.LEDOpen.Size = new System.Drawing.Size(79, 39);
            this.LEDOpen.State = Helper.LEDState.LEDOff;
            this.LEDOpen.TabIndex = 70;
            this.LEDOpen.TabStop = false;
            // 
            // BTN_Close
            // 
            this.BTN_Close.Caption = "Close";
            this.BTN_Close.EnableMouseDown = false;
            this.BTN_Close.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold);
            this.BTN_Close.Formular = null;
            this.BTN_Close.Location = new System.Drawing.Point(6, 75);
            this.BTN_Close.Name = "BTN_Close";
            this.BTN_Close.Picture_0 = Helper.BtnStyle.btg_Blanko;
            this.BTN_Close.Picture_1 = Helper.BtnStyle.btg_Gelb;
            this.BTN_Close.Picture_2 = Helper.BtnStyle.btg_Blanko;
            this.BTN_Close.Picture_3 = Helper.BtnStyle.btg_Blanko;
            this.BTN_Close.Picture_4 = Helper.BtnStyle.btg_Blanko;
            this.BTN_Close.PictureNumber = 0;
            this.BTN_Close.Size = new System.Drawing.Size(79, 48);
            this.BTN_Close.Symbol = null;
            this.BTN_Close.TabIndex = 69;
            this.BTN_Close.TabStop = false;
            this.BTN_Close.MouseDown += new System.Windows.Forms.MouseEventHandler(this.BtnDown);
            this.BTN_Close.MouseUp += new System.Windows.Forms.MouseEventHandler(this.BtnUp);
            // 
            // BTNOpen
            // 
            this.BTNOpen.Caption = "Open";
            this.BTNOpen.EnableMouseDown = false;
            this.BTNOpen.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold);
            this.BTNOpen.Formular = null;
            this.BTNOpen.Location = new System.Drawing.Point(6, 21);
            this.BTNOpen.Name = "BTNOpen";
            this.BTNOpen.Picture_0 = Helper.BtnStyle.btg_Blanko;
            this.BTNOpen.Picture_1 = Helper.BtnStyle.btg_Gelb;
            this.BTNOpen.Picture_2 = Helper.BtnStyle.btg_Blanko;
            this.BTNOpen.Picture_3 = Helper.BtnStyle.btg_Blanko;
            this.BTNOpen.Picture_4 = Helper.BtnStyle.btg_Blanko;
            this.BTNOpen.PictureNumber = 0;
            this.BTNOpen.Size = new System.Drawing.Size(79, 48);
            this.BTNOpen.Symbol = null;
            this.BTNOpen.TabIndex = 68;
            this.BTNOpen.TabStop = false;
            this.BTNOpen.MouseDown += new System.Windows.Forms.MouseEventHandler(this.BtnDown);
            this.BTNOpen.MouseMove += new System.Windows.Forms.MouseEventHandler(this.BtnUp);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.LEDMode);
            this.groupBox2.Controls.Add(this.BTN_Mode);
            this.groupBox2.Location = new System.Drawing.Point(6, 149);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(180, 83);
            this.groupBox2.TabIndex = 70;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Mode";
            // 
            // LEDMode
            // 
            this.LEDMode.BackColor = System.Drawing.Color.DarkGreen;
            this.LEDMode.Location = new System.Drawing.Point(92, 26);
            this.LEDMode.Margin = new System.Windows.Forms.Padding(4);
            this.LEDMode.Name = "LEDMode";
            this.LEDMode.Size = new System.Drawing.Size(79, 39);
            this.LEDMode.State = Helper.LEDState.LEDOff;
            this.LEDMode.TabIndex = 61;
            this.LEDMode.TabStop = false;
            // 
            // BTN_Mode
            // 
            this.BTN_Mode.Caption = "Auto";
            this.BTN_Mode.EnableMouseDown = false;
            this.BTN_Mode.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold);
            this.BTN_Mode.Formular = null;
            this.BTN_Mode.Location = new System.Drawing.Point(6, 21);
            this.BTN_Mode.Name = "BTN_Mode";
            this.BTN_Mode.Picture_0 = Helper.BtnStyle.btg_Blanko;
            this.BTN_Mode.Picture_1 = Helper.BtnStyle.btg_Gelb;
            this.BTN_Mode.Picture_2 = Helper.BtnStyle.btg_Blanko;
            this.BTN_Mode.Picture_3 = Helper.BtnStyle.btg_Blanko;
            this.BTN_Mode.Picture_4 = Helper.BtnStyle.btg_Blanko;
            this.BTN_Mode.PictureNumber = 0;
            this.BTN_Mode.Size = new System.Drawing.Size(79, 48);
            this.BTN_Mode.Symbol = null;
            this.BTN_Mode.TabIndex = 0;
            this.BTN_Mode.TabStop = false;
            this.BTN_Mode.Click += new System.EventHandler(this.Toggle);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.TXTState);
            this.groupBox3.Controls.Add(this.ValveState);
            this.groupBox3.Location = new System.Drawing.Point(6, 238);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(179, 70);
            this.groupBox3.TabIndex = 71;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Status";
            // 
            // TXTState
            // 
            this.TXTState.BackColor = System.Drawing.Color.Black;
            this.TXTState.Enabled = false;
            this.TXTState.Error = false;
            this.TXTState.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold);
            this.TXTState.ForeColor = System.Drawing.Color.Yellow;
            this.TXTState.Format = null;
            this.TXTState.Location = new System.Drawing.Point(6, 22);
            this.TXTState.Margin = new System.Windows.Forms.Padding(4);
            this.TXTState.Name = "TXTState";
            this.TXTState.Size = new System.Drawing.Size(79, 35);
            this.TXTState.TabIndex = 65;
            this.TXTState.Text = "0";
            // 
            // ValveState
            // 
            this.ValveState.BackColor = System.Drawing.Color.Transparent;
            this.ValveState.Caption = null;
            this.ValveState.FlowDirection = Helper.ValveDirection.Horizontal;
            this.ValveState.Location = new System.Drawing.Point(111, 19);
            this.ValveState.Margin = new System.Windows.Forms.Padding(4);
            this.ValveState.Mode = Helper.ValveMode.Manuell;
            this.ValveState.Name = "ValveState";
            this.ValveState.Size = new System.Drawing.Size(40, 40);
            this.ValveState.State = Helper.ValveState.Undefine;
            this.ValveState.TabIndex = 64;
            this.ValveState.TabStop = false;
            this.ValveState.Type = Helper.ValveType.Normal;
            // 
            // FrmSchieber
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(198, 437);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.BTN_Ende);
            this.Controls.Add(this.hScrollBar1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "FrmSchieber";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "FrmSchieber";
            this.Load += new System.EventHandler(this.FrmSchieber_Load);
            ((System.ComponentModel.ISupportInitialize)(this.BTN_Ende)).EndInit();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.LEDClose)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LEDOpen)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BTN_Close)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BTNOpen)).EndInit();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.LEDMode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BTN_Mode)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ValveState)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.HScrollBar hScrollBar1;
        private Helper.BitButton BTN_Ende;
        private System.Windows.Forms.GroupBox groupBox1;
        private Helper.BitButton BTN_Close;
        private Helper.BitButton BTNOpen;
        private System.Windows.Forms.GroupBox groupBox2;
        private Helper.BitButton BTN_Mode;
        private System.Windows.Forms.GroupBox groupBox3;
        private Helper.TxtBox TXTState;
        private Helper.Valve ValveState;
        private Helper.LedRectangle LEDClose;
        private Helper.LedRectangle LEDOpen;
        private Helper.LedRectangle LEDMode;
    }
}