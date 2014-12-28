namespace Helper
{
    partial class FrmVorlageMenu
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
            this.GbxMenu = new System.Windows.Forms.GroupBox();
            this.BtnKeyboard = new Helper.CompBitButton();
            this.BtnMenu = new Helper.CompBitButton();
            this.BtnClose = new Helper.CompBitButton();
            this.GbxOutput = new System.Windows.Forms.GroupBox();
            this.GbxMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BtnKeyboard)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BtnMenu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BtnClose)).BeginInit();
            this.SuspendLayout();
            // 
            // GbxMenu
            // 
            this.GbxMenu.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GbxMenu.Controls.Add(this.BtnKeyboard);
            this.GbxMenu.Controls.Add(this.BtnMenu);
            this.GbxMenu.Controls.Add(this.BtnClose);
            this.GbxMenu.Location = new System.Drawing.Point(663, 5);
            this.GbxMenu.Name = "GbxMenu";
            this.GbxMenu.Size = new System.Drawing.Size(99, 517);
            this.GbxMenu.TabIndex = 0;
            this.GbxMenu.TabStop = false;
            this.GbxMenu.Text = "Menue";
            // 
            // BtnKeyboard
            // 
            this.BtnKeyboard.Caption = "";
            this.BtnKeyboard.EnableMouseDown = false;
            this.BtnKeyboard.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold);
            this.BtnKeyboard.Formular = null;
            this.BtnKeyboard.Location = new System.Drawing.Point(10, 123);
            this.BtnKeyboard.Name = "BtnKeyboard";
            this.BtnKeyboard.Picture_0 = Helper.CompBitButtonStyle.btg_Keyboard;
            this.BtnKeyboard.Picture_1 = Helper.CompBitButtonStyle.btg_Blanko;
            this.BtnKeyboard.Picture_2 = Helper.CompBitButtonStyle.btg_Blanko;
            this.BtnKeyboard.Picture_3 = Helper.CompBitButtonStyle.btg_Blanko;
            this.BtnKeyboard.Picture_4 = Helper.CompBitButtonStyle.btg_Blanko;
            this.BtnKeyboard.PictureNumber = 0;
            this.BtnKeyboard.Size = new System.Drawing.Size(79, 48);
            this.BtnKeyboard.Symbol = null;
            this.BtnKeyboard.TabIndex = 2;
            this.BtnKeyboard.TabStop = false;
            this.BtnKeyboard.Click += new System.EventHandler(this.BtnKeyboard_Click);
            // 
            // BtnMenu
            // 
            this.BtnMenu.Caption = "";
            this.BtnMenu.EnableMouseDown = false;
            this.BtnMenu.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold);
            this.BtnMenu.Formular = null;
            this.BtnMenu.Location = new System.Drawing.Point(10, 71);
            this.BtnMenu.Name = "BtnMenu";
            this.BtnMenu.Picture_0 = Helper.CompBitButtonStyle.btg_Menue;
            this.BtnMenu.Picture_1 = Helper.CompBitButtonStyle.btg_Blanko;
            this.BtnMenu.Picture_2 = Helper.CompBitButtonStyle.btg_Blanko;
            this.BtnMenu.Picture_3 = Helper.CompBitButtonStyle.btg_Blanko;
            this.BtnMenu.Picture_4 = Helper.CompBitButtonStyle.btg_Blanko;
            this.BtnMenu.PictureNumber = 0;
            this.BtnMenu.Size = new System.Drawing.Size(79, 48);
            this.BtnMenu.Symbol = null;
            this.BtnMenu.TabIndex = 1;
            this.BtnMenu.TabStop = false;
            this.BtnMenu.Click += new System.EventHandler(this.BtnMenu_Click);
            // 
            // BtnClose
            // 
            this.BtnClose.Caption = "";
            this.BtnClose.EnableMouseDown = false;
            this.BtnClose.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold);
            this.BtnClose.Formular = null;
            this.BtnClose.Location = new System.Drawing.Point(10, 19);
            this.BtnClose.Name = "BtnClose";
            this.BtnClose.Picture_0 = Helper.CompBitButtonStyle.btg_Exit;
            this.BtnClose.Picture_1 = Helper.CompBitButtonStyle.btg_Blanko;
            this.BtnClose.Picture_2 = Helper.CompBitButtonStyle.btg_Blanko;
            this.BtnClose.Picture_3 = Helper.CompBitButtonStyle.btg_Blanko;
            this.BtnClose.Picture_4 = Helper.CompBitButtonStyle.btg_Blanko;
            this.BtnClose.PictureNumber = 0;
            this.BtnClose.Size = new System.Drawing.Size(79, 48);
            this.BtnClose.Symbol = null;
            this.BtnClose.TabIndex = 0;
            this.BtnClose.TabStop = false;
            this.BtnClose.Click += new System.EventHandler(this.BtnClose_Click);
            // 
            // GbxOutput
            // 
            this.GbxOutput.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GbxOutput.Location = new System.Drawing.Point(5, 5);
            this.GbxOutput.Name = "GbxOutput";
            this.GbxOutput.Size = new System.Drawing.Size(652, 517);
            this.GbxOutput.TabIndex = 1;
            this.GbxOutput.TabStop = false;
            this.GbxOutput.Text = "Datenausgabe";
            // 
            // FrmVorlageMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.ClientSize = new System.Drawing.Size(768, 528);
            this.Controls.Add(this.GbxOutput);
            this.Controls.Add(this.GbxMenu);
            this.Name = "FrmVorlageMenu";
            this.GbxMenu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.BtnKeyboard)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BtnMenu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BtnClose)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        protected CompBitButton BtnClose;
        protected CompBitButton BtnKeyboard;
        protected System.Windows.Forms.GroupBox GbxMenu;
        protected System.Windows.Forms.GroupBox GbxOutput;
        protected CompBitButton BtnMenu;
    }
}
