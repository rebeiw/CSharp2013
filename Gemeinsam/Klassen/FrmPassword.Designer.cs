namespace Helper
{
    partial class FrmPassword
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
            this.GbxPassword = new System.Windows.Forms.GroupBox();
            this.CbxUser = new System.Windows.Forms.ComboBox();
            this.LblUser = new System.Windows.Forms.Label();
            this.BtnOk = new Helper.CompBitButton();
            this.BtnCancel = new Helper.CompBitButton();
            this.TxtPassword = new System.Windows.Forms.TextBox();
            this.LblPassword = new System.Windows.Forms.Label();
            this.GbxPassword.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BtnOk)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BtnCancel)).BeginInit();
            this.SuspendLayout();
            // 
            // GbxPassword
            // 
            this.GbxPassword.Controls.Add(this.CbxUser);
            this.GbxPassword.Controls.Add(this.LblUser);
            this.GbxPassword.Controls.Add(this.BtnOk);
            this.GbxPassword.Controls.Add(this.BtnCancel);
            this.GbxPassword.Controls.Add(this.TxtPassword);
            this.GbxPassword.Controls.Add(this.LblPassword);
            this.GbxPassword.Location = new System.Drawing.Point(5, 6);
            this.GbxPassword.Name = "GbxPassword";
            this.GbxPassword.Size = new System.Drawing.Size(219, 193);
            this.GbxPassword.TabIndex = 0;
            this.GbxPassword.TabStop = false;
            // 
            // CbxUser
            // 
            this.CbxUser.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CbxUser.FormattingEnabled = true;
            this.CbxUser.Location = new System.Drawing.Point(10, 44);
            this.CbxUser.Name = "CbxUser";
            this.CbxUser.Size = new System.Drawing.Size(199, 24);
            this.CbxUser.TabIndex = 10;
            this.CbxUser.SelectedIndexChanged += new System.EventHandler(this.CbxUser_SelectedIndexChanged);
            // 
            // LblUser
            // 
            this.LblUser.Location = new System.Drawing.Point(9, 14);
            this.LblUser.Name = "LblUser";
            this.LblUser.Size = new System.Drawing.Size(201, 22);
            this.LblUser.TabIndex = 9;
            this.LblUser.Text = "Benutzer:";
            this.LblUser.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // BtnOk
            // 
            this.BtnOk.Caption = "";
            this.BtnOk.EnableMouseDown = false;
            this.BtnOk.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold);
            this.BtnOk.Formular = null;
            this.BtnOk.Location = new System.Drawing.Point(8, 136);
            this.BtnOk.Name = "BtnOk";
            this.BtnOk.Picture_0 = Helper.CompBitButtonStyle.btg_Ok;
            this.BtnOk.Picture_1 = Helper.CompBitButtonStyle.btg_Blanko;
            this.BtnOk.Picture_2 = Helper.CompBitButtonStyle.btg_Blanko;
            this.BtnOk.Picture_3 = Helper.CompBitButtonStyle.btg_Blanko;
            this.BtnOk.Picture_4 = Helper.CompBitButtonStyle.btg_Blanko;
            this.BtnOk.PictureNumber = 0;
            this.BtnOk.Size = new System.Drawing.Size(79, 48);
            this.BtnOk.Symbol = null;
            this.BtnOk.TabIndex = 8;
            this.BtnOk.TabStop = false;
            this.BtnOk.Click += new System.EventHandler(this.BtnOk_Click);
            // 
            // BtnCancel
            // 
            this.BtnCancel.Caption = "";
            this.BtnCancel.EnableMouseDown = false;
            this.BtnCancel.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold);
            this.BtnCancel.Formular = null;
            this.BtnCancel.Location = new System.Drawing.Point(130, 136);
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.Picture_0 = Helper.CompBitButtonStyle.btg_Esc;
            this.BtnCancel.Picture_1 = Helper.CompBitButtonStyle.btg_Blanko;
            this.BtnCancel.Picture_2 = Helper.CompBitButtonStyle.btg_Blanko;
            this.BtnCancel.Picture_3 = Helper.CompBitButtonStyle.btg_Blanko;
            this.BtnCancel.Picture_4 = Helper.CompBitButtonStyle.btg_Blanko;
            this.BtnCancel.PictureNumber = 0;
            this.BtnCancel.Size = new System.Drawing.Size(79, 48);
            this.BtnCancel.Symbol = null;
            this.BtnCancel.TabIndex = 7;
            this.BtnCancel.TabStop = false;
            this.BtnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // TxtPassword
            // 
            this.TxtPassword.Location = new System.Drawing.Point(8, 106);
            this.TxtPassword.Name = "TxtPassword";
            this.TxtPassword.PasswordChar = '*';
            this.TxtPassword.Size = new System.Drawing.Size(201, 22);
            this.TxtPassword.TabIndex = 4;
            this.TxtPassword.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TbxPassword_KeyDown);
            // 
            // LblPassword
            // 
            this.LblPassword.Location = new System.Drawing.Point(8, 76);
            this.LblPassword.Name = "LblPassword";
            this.LblPassword.Size = new System.Drawing.Size(201, 22);
            this.LblPassword.TabIndex = 3;
            this.LblPassword.Text = "Passwort:";
            this.LblPassword.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // FrmPassword
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Silver;
            this.ClientSize = new System.Drawing.Size(229, 203);
            this.Controls.Add(this.GbxPassword);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FrmPassword";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Passworteingabe";
            this.Activated += new System.EventHandler(this.FrmPassword_Activated);
            this.Deactivate += new System.EventHandler(this.FrmPassword_Deactivate);
            this.Load += new System.EventHandler(this.FrmPasswort_Load);
            this.GbxPassword.ResumeLayout(false);
            this.GbxPassword.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BtnOk)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BtnCancel)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox GbxPassword;
        private System.Windows.Forms.TextBox TxtPassword;
        private System.Windows.Forms.Label LblPassword;
        private CompBitButton BtnOk;
        private CompBitButton BtnCancel;
        private System.Windows.Forms.ComboBox CbxUser;
        private System.Windows.Forms.Label LblUser;
    }
}