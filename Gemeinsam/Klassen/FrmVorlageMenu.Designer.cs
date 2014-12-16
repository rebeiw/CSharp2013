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
            
            this.GB_Menu = new System.Windows.Forms.GroupBox();
            this.BTN_Keyboard = new Helper.RotaBitButton();
            this.BTN_Menu = new Helper.RotaBitButton();
            this.BTN_Close = new Helper.RotaBitButton();
            this.GB_Datenausgabe = new System.Windows.Forms.GroupBox();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.GB_Menu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BTN_Keyboard)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BTN_Menu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BTN_Close)).BeginInit();
            this.SuspendLayout();
            // 
            // GB_Menu
            // 
            this.GB_Menu.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GB_Menu.Controls.Add(this.BTN_Keyboard);
            this.GB_Menu.Controls.Add(this.BTN_Menu);
            this.GB_Menu.Controls.Add(this.BTN_Close);
            this.GB_Menu.Location = new System.Drawing.Point(663, 5);
            this.GB_Menu.Name = "GB_Menu";
            this.GB_Menu.Size = new System.Drawing.Size(99, 517);
            this.GB_Menu.TabIndex = 0;
            this.GB_Menu.TabStop = false;
            this.GB_Menu.Text = "Menue";
            // 
            // BTN_Keyboard
            // 
            this.BTN_Keyboard.Caption = "";
            this.BTN_Keyboard.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold);
            this.BTN_Keyboard.Formular = null;
            this.BTN_Keyboard.Location = new System.Drawing.Point(10, 123);
            this.BTN_Keyboard.Name = "BTN_Keyboard";
            this.BTN_Keyboard.Picture_0 = Helper.BtnStyle.btg_Keyboard;
            this.BTN_Keyboard.Picture_1 = Helper.BtnStyle.btg_Blanko;
            this.BTN_Keyboard.Picture_2 = Helper.BtnStyle.btg_Blanko;
            this.BTN_Keyboard.Picture_3 = Helper.BtnStyle.btg_Blanko;
            this.BTN_Keyboard.Picture_4 = Helper.BtnStyle.btg_Blanko;
            this.BTN_Keyboard.PictureNumber = 0;
            this.BTN_Keyboard.Size = new System.Drawing.Size(79, 48);
            this.BTN_Keyboard.Symbol = null;
            this.BTN_Keyboard.TabIndex = 2;
            this.BTN_Keyboard.TabStop = false;
            this.BTN_Keyboard.Click += new System.EventHandler(this.BTN_Keyboard_Click);
            // 
            // BTN_Menu
            // 
            this.BTN_Menu.Caption = "";
            this.BTN_Menu.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold);
            this.BTN_Menu.Formular = null;
            this.BTN_Menu.Location = new System.Drawing.Point(10, 71);
            this.BTN_Menu.Name = "BTN_Menu";
            this.BTN_Menu.Picture_0 = Helper.BtnStyle.btg_Menue;
            this.BTN_Menu.Picture_1 = Helper.BtnStyle.btg_Blanko;
            this.BTN_Menu.Picture_2 = Helper.BtnStyle.btg_Blanko;
            this.BTN_Menu.Picture_3 = Helper.BtnStyle.btg_Blanko;
            this.BTN_Menu.Picture_4 = Helper.BtnStyle.btg_Blanko;
            this.BTN_Menu.PictureNumber = 0;
            this.BTN_Menu.Size = new System.Drawing.Size(79, 48);
            this.BTN_Menu.Symbol = null;
            this.BTN_Menu.TabIndex = 1;
            this.BTN_Menu.TabStop = false;
            this.BTN_Menu.Click += new System.EventHandler(this.BTN_Menu_Click);
            // 
            // BTN_Close
            // 
            this.BTN_Close.Caption = "";
            this.BTN_Close.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold);
            this.BTN_Close.Formular = null;
            this.BTN_Close.Location = new System.Drawing.Point(10, 19);
            this.BTN_Close.Name = "BTN_Close";
            this.BTN_Close.Picture_0 = Helper.BtnStyle.btg_Exit;
            this.BTN_Close.Picture_1 = Helper.BtnStyle.btg_Blanko;
            this.BTN_Close.Picture_2 = Helper.BtnStyle.btg_Blanko;
            this.BTN_Close.Picture_3 = Helper.BtnStyle.btg_Blanko;
            this.BTN_Close.Picture_4 = Helper.BtnStyle.btg_Blanko;
            this.BTN_Close.PictureNumber = 0;
            this.BTN_Close.Size = new System.Drawing.Size(79, 48);
            this.BTN_Close.Symbol = null;
            this.BTN_Close.TabIndex = 0;
            this.BTN_Close.TabStop = false;
            this.BTN_Close.Click += new System.EventHandler(this.BTN_Close_Click);
            // 
            // GB_Datenausgabe
            // 
            this.GB_Datenausgabe.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GB_Datenausgabe.Location = new System.Drawing.Point(5, 5);
            this.GB_Datenausgabe.Name = "GB_Datenausgabe";
            this.GB_Datenausgabe.Size = new System.Drawing.Size(652, 517);
            this.GB_Datenausgabe.TabIndex = 1;
            this.GB_Datenausgabe.TabStop = false;
            this.GB_Datenausgabe.Text = "Datenausgabe";
            // 
            // FrmVorlageMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.ClientSize = new System.Drawing.Size(768, 528);
            this.Controls.Add(this.GB_Datenausgabe);
            this.Controls.Add(this.GB_Menu);
            this.Name = "FrmVorlageMenu";
            this.Load += new System.EventHandler(this.FrmVorlageMenu_Load);
            this.GB_Menu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.BTN_Keyboard)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BTN_Menu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BTN_Close)).EndInit();
            this.ResumeLayout(false);
            

        }

        #endregion

        protected RotaBitButton BTN_Close;
        protected RotaBitButton BTN_Keyboard;
        protected System.Windows.Forms.GroupBox GB_Menu;
        protected System.Windows.Forms.GroupBox GB_Datenausgabe;
        protected System.ComponentModel.BackgroundWorker backgroundWorker1;
        protected RotaBitButton BTN_Menu;
    }
}
