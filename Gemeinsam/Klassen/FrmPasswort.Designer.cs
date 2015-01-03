namespace Helper
{
    partial class FrmPasswort
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
            this.GB_Passwort = new System.Windows.Forms.GroupBox();
            this.BTN_Ok1 = new Helper.CompBitButton();
            this.BTN_Abbruch = new Helper.CompBitButton();
            this.TB_Passwort = new System.Windows.Forms.TextBox();
            this.LBL_Passwort = new System.Windows.Forms.Label();
            this.GB_Passwort.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BTN_Ok1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BTN_Abbruch)).BeginInit();
            this.SuspendLayout();
            // 
            // GB_Passwort
            // 
            this.GB_Passwort.Controls.Add(this.BTN_Ok1);
            this.GB_Passwort.Controls.Add(this.BTN_Abbruch);
            this.GB_Passwort.Controls.Add(this.TB_Passwort);
            this.GB_Passwort.Controls.Add(this.LBL_Passwort);
            this.GB_Passwort.Location = new System.Drawing.Point(6, 6);
            this.GB_Passwort.Name = "GB_Passwort";
            this.GB_Passwort.Size = new System.Drawing.Size(219, 146);
            this.GB_Passwort.TabIndex = 0;
            this.GB_Passwort.TabStop = false;
            // 
            // BTN_Ok1
            // 
            this.BTN_Ok1.Caption = "";
            this.BTN_Ok1.EnableMouseDown = false;
            this.BTN_Ok1.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold);
            this.BTN_Ok1.Formular = null;
            this.BTN_Ok1.Location = new System.Drawing.Point(8, 86);
            this.BTN_Ok1.Name = "BTN_Ok1";
            this.BTN_Ok1.Picture_0 = Helper.CompBitButtonStyle.btg_Ok;
            this.BTN_Ok1.Picture_1 = Helper.CompBitButtonStyle.btg_Blanko;
            this.BTN_Ok1.Picture_2 = Helper.CompBitButtonStyle.btg_Blanko;
            this.BTN_Ok1.Picture_3 = Helper.CompBitButtonStyle.btg_Blanko;
            this.BTN_Ok1.Picture_4 = Helper.CompBitButtonStyle.btg_Blanko;
            this.BTN_Ok1.PictureNumber = 0;
            this.BTN_Ok1.Size = new System.Drawing.Size(79, 48);
            this.BTN_Ok1.Symbol = null;
            this.BTN_Ok1.TabIndex = 8;
            this.BTN_Ok1.TabStop = false;
            this.BTN_Ok1.Click += new System.EventHandler(this.BTN_Ok_Click);
            // 
            // BTN_Abbruch
            // 
            this.BTN_Abbruch.Caption = "";
            this.BTN_Abbruch.EnableMouseDown = false;
            this.BTN_Abbruch.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold);
            this.BTN_Abbruch.Formular = null;
            this.BTN_Abbruch.Location = new System.Drawing.Point(130, 86);
            this.BTN_Abbruch.Name = "BTN_Abbruch";
            this.BTN_Abbruch.Picture_0 = Helper.CompBitButtonStyle.btg_Esc;
            this.BTN_Abbruch.Picture_1 = Helper.CompBitButtonStyle.btg_Blanko;
            this.BTN_Abbruch.Picture_2 = Helper.CompBitButtonStyle.btg_Blanko;
            this.BTN_Abbruch.Picture_3 = Helper.CompBitButtonStyle.btg_Blanko;
            this.BTN_Abbruch.Picture_4 = Helper.CompBitButtonStyle.btg_Blanko;
            this.BTN_Abbruch.PictureNumber = 0;
            this.BTN_Abbruch.Size = new System.Drawing.Size(79, 48);
            this.BTN_Abbruch.Symbol = null;
            this.BTN_Abbruch.TabIndex = 7;
            this.BTN_Abbruch.TabStop = false;
            this.BTN_Abbruch.Click += new System.EventHandler(this.BTN_Abbruch_Click);
            // 
            // TB_Passwort
            // 
            this.TB_Passwort.Location = new System.Drawing.Point(8, 50);
            this.TB_Passwort.Name = "TB_Passwort";
            this.TB_Passwort.PasswordChar = '*';
            this.TB_Passwort.Size = new System.Drawing.Size(201, 22);
            this.TB_Passwort.TabIndex = 4;
            this.TB_Passwort.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TB_Passwort_KeyDown);
            // 
            // LBL_Passwort
            // 
            this.LBL_Passwort.Location = new System.Drawing.Point(8, 20);
            this.LBL_Passwort.Name = "LBL_Passwort";
            this.LBL_Passwort.Size = new System.Drawing.Size(201, 22);
            this.LBL_Passwort.TabIndex = 3;
            this.LBL_Passwort.Text = "Passwort:";
            this.LBL_Passwort.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // FrmPasswort
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Silver;
            this.ClientSize = new System.Drawing.Size(230, 158);
            this.Controls.Add(this.GB_Passwort);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FrmPasswort";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Passworteingabe";
            this.Deactivate += new System.EventHandler(this.FRM_Password_Deactivate);
            this.Load += new System.EventHandler(this.FrmPasswort_Load);
            this.GB_Passwort.ResumeLayout(false);
            this.GB_Passwort.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BTN_Ok1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BTN_Abbruch)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox GB_Passwort;
        private System.Windows.Forms.TextBox TB_Passwort;
        private System.Windows.Forms.Label LBL_Passwort;
        private CompBitButton BTN_Ok1;
        private CompBitButton BTN_Abbruch;
    }
}