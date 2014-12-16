namespace Helper
{
    partial class FrmProgEnd
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
            this.label1 = new System.Windows.Forms.Label();
            this.BTN_Ok = new Helper.RotaBitButton();
            this.BTN_Abbruch = new Helper.RotaBitButton();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BTN_Ok)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BTN_Abbruch)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.BTN_Ok);
            this.groupBox1.Controls.Add(this.BTN_Abbruch);
            this.groupBox1.Location = new System.Drawing.Point(5, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(197, 129);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(7, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(182, 44);
            this.label1.TabIndex = 4;
            this.label1.Text = "Programm beenden?";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // BTN_Ok
            // 
            this.BTN_Ok.Caption = "";
            this.BTN_Ok.EnableMouseDown = false;
            this.BTN_Ok.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold);
            this.BTN_Ok.Formular = null;
            this.BTN_Ok.Location = new System.Drawing.Point(8, 73);
            this.BTN_Ok.Name = "BTN_Ok";
            this.BTN_Ok.Picture_0 = Helper.BtnStyle.btg_Ok;
            this.BTN_Ok.Picture_1 = Helper.BtnStyle.btg_Blanko;
            this.BTN_Ok.Picture_2 = Helper.BtnStyle.btg_Blanko;
            this.BTN_Ok.Picture_3 = Helper.BtnStyle.btg_Blanko;
            this.BTN_Ok.Picture_4 = Helper.BtnStyle.btg_Blanko;
            this.BTN_Ok.PictureNumber = 0;
            this.BTN_Ok.Size = new System.Drawing.Size(79, 48);
            this.BTN_Ok.Symbol = null;
            this.BTN_Ok.TabIndex = 3;
            this.BTN_Ok.TabStop = false;
            this.BTN_Ok.Click += new System.EventHandler(this.BTN_Ok_Click);
            // 
            // BTN_Abbruch
            // 
            this.BTN_Abbruch.Caption = "";
            this.BTN_Abbruch.EnableMouseDown = false;
            this.BTN_Abbruch.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold);
            this.BTN_Abbruch.Formular = null;
            this.BTN_Abbruch.Location = new System.Drawing.Point(110, 73);
            this.BTN_Abbruch.Name = "BTN_Abbruch";
            this.BTN_Abbruch.Picture_0 = Helper.BtnStyle.btg_Esc;
            this.BTN_Abbruch.Picture_1 = Helper.BtnStyle.btg_Blanko;
            this.BTN_Abbruch.Picture_2 = Helper.BtnStyle.btg_Blanko;
            this.BTN_Abbruch.Picture_3 = Helper.BtnStyle.btg_Blanko;
            this.BTN_Abbruch.Picture_4 = Helper.BtnStyle.btg_Blanko;
            this.BTN_Abbruch.PictureNumber = 0;
            this.BTN_Abbruch.Size = new System.Drawing.Size(79, 48);
            this.BTN_Abbruch.Symbol = null;
            this.BTN_Abbruch.TabIndex = 2;
            this.BTN_Abbruch.TabStop = false;
            this.BTN_Abbruch.Click += new System.EventHandler(this.BTN_Abbruch_Click);
            // 
            // FrmProgEnd
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.ClientSize = new System.Drawing.Size(210, 139);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FrmProgEnd";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Programm beenden";
            this.Load += new System.EventHandler(this.FrmProgEnd_Load);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.BTN_Ok)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BTN_Abbruch)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private RotaBitButton BTN_Ok;
        private RotaBitButton BTN_Abbruch;
    }
}
