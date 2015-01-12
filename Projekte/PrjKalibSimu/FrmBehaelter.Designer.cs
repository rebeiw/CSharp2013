namespace PrjKalibSimu
{
    partial class FrmBehaelter
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

            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmBehaelter));
            this.hScrollBar1 = new System.Windows.Forms.HScrollBar();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.BTN_Unten = new Helper.BitButton();
            this.BTN_Oben = new Helper.BitButton();
            this.LEDUnten = new Helper.LedRectangle();
            this.LEDOben = new Helper.LedRectangle();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.BTN_Mode = new Helper.BitButton();
            this.LEDMode = new Helper.LedRectangle();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.TXTState = new Helper.TxtBox();
            this.LifterState = new Helper.Lifter();
            this.BTN_FrmClose = new Helper.BitButton();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BTN_Unten)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BTN_Oben)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LEDUnten)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LEDOben)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BTN_Mode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LEDMode)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.LifterState)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BTN_FrmClose)).BeginInit();
            this.SuspendLayout();
            // 
            // hScrollBar1
            // 
            this.hScrollBar1.LargeChange = 1;
            this.hScrollBar1.Location = new System.Drawing.Point(12, 406);
            this.hScrollBar1.Minimum = 50;
            this.hScrollBar1.Name = "hScrollBar1";
            this.hScrollBar1.Size = new System.Drawing.Size(193, 37);
            this.hScrollBar1.TabIndex = 75;
            this.hScrollBar1.Value = 50;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.BTN_Unten);
            this.groupBox1.Controls.Add(this.BTN_Oben);
            this.groupBox1.Controls.Add(this.LEDUnten);
            this.groupBox1.Controls.Add(this.LEDOben);
            this.groupBox1.Location = new System.Drawing.Point(5, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(200, 132);
            this.groupBox1.TabIndex = 76;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Kommando";
            // 
            // BTN_Unten
            // 
            this.BTN_Unten.Caption = "Unten";
            this.BTN_Unten.EnableMouseDown = false;
            this.BTN_Unten.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold);
            this.BTN_Unten.Formular = null;
            this.BTN_Unten.Location = new System.Drawing.Point(11, 78);
            this.BTN_Unten.Name = "BTN_Unten";
            this.BTN_Unten.Picture_0 = Helper.BtnStyle.btg_Blanko;
            this.BTN_Unten.Picture_1 = Helper.BtnStyle.btg_Blanko;
            this.BTN_Unten.Picture_2 = Helper.BtnStyle.btg_Blanko;
            this.BTN_Unten.Picture_3 = Helper.BtnStyle.btg_Blanko;
            this.BTN_Unten.Picture_4 = Helper.BtnStyle.btg_Blanko;
            this.BTN_Unten.PictureNumber = 0;
            this.BTN_Unten.Size = new System.Drawing.Size(79, 48);
            this.BTN_Unten.Symbol = null;
            this.BTN_Unten.TabIndex = 70;
            this.BTN_Unten.TabStop = false;
            this.BTN_Unten.Click += new System.EventHandler(this.Toggle);
            // 
            // BTN_Oben
            // 
            this.BTN_Oben.Caption = "Oben";
            this.BTN_Oben.EnableMouseDown = false;
            this.BTN_Oben.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold);
            this.BTN_Oben.Formular = null;
            this.BTN_Oben.Location = new System.Drawing.Point(11, 21);
            this.BTN_Oben.Name = "BTN_Oben";
            this.BTN_Oben.Picture_0 = Helper.BtnStyle.btg_Blanko;
            this.BTN_Oben.Picture_1 = Helper.BtnStyle.btg_Blanko;
            this.BTN_Oben.Picture_2 = Helper.BtnStyle.btg_Blanko;
            this.BTN_Oben.Picture_3 = Helper.BtnStyle.btg_Blanko;
            this.BTN_Oben.Picture_4 = Helper.BtnStyle.btg_Blanko;
            this.BTN_Oben.PictureNumber = 0;
            this.BTN_Oben.Size = new System.Drawing.Size(79, 48);
            this.BTN_Oben.Symbol = null;
            this.BTN_Oben.TabIndex = 69;
            this.BTN_Oben.TabStop = false;
            this.BTN_Oben.Click += new System.EventHandler(this.Toggle);
            // 
            // LEDUnten
            // 
            this.LEDUnten.BackColor = System.Drawing.Color.DarkGreen;
            this.LEDUnten.Location = new System.Drawing.Point(114, 67);
            this.LEDUnten.Margin = new System.Windows.Forms.Padding(4);
            this.LEDUnten.Name = "LEDUnten";
            this.LEDUnten.Size = new System.Drawing.Size(79, 39);
            this.LEDUnten.State = Helper.LEDState.LEDOff;
            this.LEDUnten.TabIndex = 68;
            this.LEDUnten.TabStop = false;
            // 
            // LEDOben
            // 
            this.LEDOben.BackColor = System.Drawing.Color.DarkGreen;
            this.LEDOben.Location = new System.Drawing.Point(114, 20);
            this.LEDOben.Margin = new System.Windows.Forms.Padding(4);
            this.LEDOben.Name = "LEDOben";
            this.LEDOben.Size = new System.Drawing.Size(79, 39);
            this.LEDOben.State = Helper.LEDState.LEDOff;
            this.LEDOben.TabIndex = 66;
            this.LEDOben.TabStop = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.BTN_Mode);
            this.groupBox2.Controls.Add(this.LEDMode);
            this.groupBox2.Location = new System.Drawing.Point(5, 141);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(200, 78);
            this.groupBox2.TabIndex = 77;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Mode";
            // 
            // BTN_Mode
            // 
            this.BTN_Mode.Caption = "Auto";
            this.BTN_Mode.EnableMouseDown = false;
            this.BTN_Mode.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold);
            this.BTN_Mode.Formular = null;
            this.BTN_Mode.Location = new System.Drawing.Point(11, 24);
            this.BTN_Mode.Name = "BTN_Mode";
            this.BTN_Mode.Picture_0 = Helper.BtnStyle.btg_Blanko;
            this.BTN_Mode.Picture_1 = Helper.BtnStyle.btg_Blanko;
            this.BTN_Mode.Picture_2 = Helper.BtnStyle.btg_Blanko;
            this.BTN_Mode.Picture_3 = Helper.BtnStyle.btg_Blanko;
            this.BTN_Mode.Picture_4 = Helper.BtnStyle.btg_Blanko;
            this.BTN_Mode.PictureNumber = 0;
            this.BTN_Mode.Size = new System.Drawing.Size(79, 48);
            this.BTN_Mode.Symbol = null;
            this.BTN_Mode.TabIndex = 73;
            this.BTN_Mode.TabStop = false;
            this.BTN_Mode.Click += new System.EventHandler(this.Toggle);
            // 
            // LEDMode
            // 
            this.LEDMode.BackColor = System.Drawing.Color.DarkGreen;
            this.LEDMode.Location = new System.Drawing.Point(114, 24);
            this.LEDMode.Margin = new System.Windows.Forms.Padding(4);
            this.LEDMode.Name = "LEDMode";
            this.LEDMode.Size = new System.Drawing.Size(79, 39);
            this.LEDMode.State = Helper.LEDState.LEDOff;
            this.LEDMode.TabIndex = 72;
            this.LEDMode.TabStop = false;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.TXTState);
            this.groupBox3.Controls.Add(this.LifterState);
            this.groupBox3.Location = new System.Drawing.Point(5, 225);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(200, 100);
            this.groupBox3.TabIndex = 78;
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
            this.TXTState.Location = new System.Drawing.Point(7, 32);
            this.TXTState.Margin = new System.Windows.Forms.Padding(4);
            this.TXTState.Name = "TXTState";
            this.TXTState.Size = new System.Drawing.Size(79, 35);
            this.TXTState.TabIndex = 74;
            this.TXTState.Text = "0";
            // 
            // LifterState
            // 
            this.LifterState.BackColor = System.Drawing.Color.Transparent;
            this.LifterState.Location = new System.Drawing.Point(146, 20);
            this.LifterState.Name = "LifterState";
            this.LifterState.Size = new System.Drawing.Size(23, 74);
            this.LifterState.State = Helper.LifterState.Down;
            this.LifterState.TabIndex = 1;
            this.LifterState.TabStop = false;
            // 
            // BTN_FrmClose
            // 
            this.BTN_FrmClose.Caption = "";
            this.BTN_FrmClose.EnableMouseDown = false;
            this.BTN_FrmClose.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold);
            this.BTN_FrmClose.Formular = null;
            this.BTN_FrmClose.Location = new System.Drawing.Point(71, 343);
            this.BTN_FrmClose.Name = "BTN_FrmClose";
            this.BTN_FrmClose.Picture_0 = Helper.BtnStyle.btg_Exit;
            this.BTN_FrmClose.Picture_1 = Helper.BtnStyle.btg_Blanko;
            this.BTN_FrmClose.Picture_2 = Helper.BtnStyle.btg_Blanko;
            this.BTN_FrmClose.Picture_3 = Helper.BtnStyle.btg_Blanko;
            this.BTN_FrmClose.Picture_4 = Helper.BtnStyle.btg_Blanko;
            this.BTN_FrmClose.PictureNumber = 0;
            this.BTN_FrmClose.Size = new System.Drawing.Size(79, 48);
            this.BTN_FrmClose.Symbol = null;
            this.BTN_FrmClose.TabIndex = 79;
            this.BTN_FrmClose.TabStop = false;
            this.BTN_FrmClose.Click += new System.EventHandler(this.BTN_FrmClose_Click);
            // 
            // FrmBehaelter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.ClientSize = new System.Drawing.Size(218, 450);
            this.Controls.Add(this.BTN_FrmClose);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.hScrollBar1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmBehaelter";
            this.Text = "Behaelter";
            this.Load += new System.EventHandler(this.FrmBehaelter_Load);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.BTN_Unten)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BTN_Oben)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LEDUnten)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LEDOben)).EndInit();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.BTN_Mode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LEDMode)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.LifterState)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BTN_FrmClose)).EndInit();
            this.ResumeLayout(false);
            
        }

        #endregion

        private System.Windows.Forms.HScrollBar hScrollBar1;
        private System.Windows.Forms.GroupBox groupBox1;
        private Helper.BitButton BTN_Unten;
        private Helper.BitButton BTN_Oben;
        private Helper.LedRectangle LEDUnten;
        private Helper.LedRectangle LEDOben;
        private System.Windows.Forms.GroupBox groupBox2;
        private Helper.BitButton BTN_Mode;
        private Helper.LedRectangle LEDMode;
        private System.Windows.Forms.GroupBox groupBox3;
        private Helper.TxtBox TXTState;
        private Helper.Lifter LifterState;
        private Helper.BitButton BTN_FrmClose;
    }
}
