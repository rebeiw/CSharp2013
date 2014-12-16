namespace Helper
{
    partial class FRM_Menu
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
            this.BTN_KeyBoard = new Helper.RotaBitButton();
            this.BTN_PrintScreen = new Helper.RotaBitButton();
            this.BTN_Sprache = new Helper.RotaBitButton();
            this.BTN_Quit = new Helper.RotaBitButton();
            this.BTN_User = new Helper.RotaBitButton();
            this.BTN_Ende = new Helper.RotaBitButton();
            this.GB_Menu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BTN_KeyBoard)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BTN_PrintScreen)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BTN_Sprache)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BTN_Quit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BTN_User)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BTN_Ende)).BeginInit();
            this.SuspendLayout();
            // 
            // GB_Menu
            // 
            this.GB_Menu.Controls.Add(this.BTN_KeyBoard);
            this.GB_Menu.Controls.Add(this.BTN_PrintScreen);
            this.GB_Menu.Controls.Add(this.BTN_Sprache);
            this.GB_Menu.Controls.Add(this.BTN_Quit);
            this.GB_Menu.Controls.Add(this.BTN_User);
            this.GB_Menu.Controls.Add(this.BTN_Ende);
            this.GB_Menu.Location = new System.Drawing.Point(5, 0);
            this.GB_Menu.Name = "GB_Menu";
            this.GB_Menu.Size = new System.Drawing.Size(99, 335);
            this.GB_Menu.TabIndex = 0;
            this.GB_Menu.TabStop = false;
            this.GB_Menu.Enter += new System.EventHandler(this.GB_Menu_Enter);
            // 
            // BTN_KeyBoard
            // 
            this.BTN_KeyBoard.Caption = "";
            this.BTN_KeyBoard.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold);
            this.BTN_KeyBoard.Formular = null;
            this.BTN_KeyBoard.Location = new System.Drawing.Point(10, 227);
            this.BTN_KeyBoard.Name = "BTN_KeyBoard";
            this.BTN_KeyBoard.Picture_0 = Helper.BtnStyle.btg_Keyboard;
            this.BTN_KeyBoard.Picture_1 = Helper.BtnStyle.btg_Blanko;
            this.BTN_KeyBoard.Picture_2 = Helper.BtnStyle.btg_Blanko;
            this.BTN_KeyBoard.Picture_3 = Helper.BtnStyle.btg_Blanko;
            this.BTN_KeyBoard.Picture_4 = Helper.BtnStyle.btg_Blanko;
            this.BTN_KeyBoard.PictureNumber = 0;
            this.BTN_KeyBoard.Size = new System.Drawing.Size(79, 48);
            this.BTN_KeyBoard.Symbol = null;
            this.BTN_KeyBoard.TabIndex = 10;
            this.BTN_KeyBoard.TabStop = false;
            this.BTN_KeyBoard.Click += new System.EventHandler(this.BTN_KeyBoard_Click);
            // 
            // BTN_PrintScreen
            // 
            this.BTN_PrintScreen.Caption = "";
            this.BTN_PrintScreen.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold);
            this.BTN_PrintScreen.Formular = null;
            this.BTN_PrintScreen.Location = new System.Drawing.Point(10, 175);
            this.BTN_PrintScreen.Name = "BTN_PrintScreen";
            this.BTN_PrintScreen.Picture_0 = Helper.BtnStyle.btg_ScreenShot;
            this.BTN_PrintScreen.Picture_1 = Helper.BtnStyle.btg_Blanko;
            this.BTN_PrintScreen.Picture_2 = Helper.BtnStyle.btg_Blanko;
            this.BTN_PrintScreen.Picture_3 = Helper.BtnStyle.btg_Blanko;
            this.BTN_PrintScreen.Picture_4 = Helper.BtnStyle.btg_Blanko;
            this.BTN_PrintScreen.PictureNumber = 0;
            this.BTN_PrintScreen.Size = new System.Drawing.Size(79, 48);
            this.BTN_PrintScreen.Symbol = null;
            this.BTN_PrintScreen.TabIndex = 9;
            this.BTN_PrintScreen.TabStop = false;
            this.BTN_PrintScreen.Click += new System.EventHandler(this.BTN_PrintScreen_Click);
            // 
            // BTN_Sprache
            // 
            this.BTN_Sprache.Caption = "";
            this.BTN_Sprache.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold);
            this.BTN_Sprache.Formular = null;
            this.BTN_Sprache.Location = new System.Drawing.Point(10, 123);
            this.BTN_Sprache.Name = "BTN_Sprache";
            this.BTN_Sprache.Picture_0 = Helper.BtnStyle.btg_De;
            this.BTN_Sprache.Picture_1 = Helper.BtnStyle.btg_Blanko;
            this.BTN_Sprache.Picture_2 = Helper.BtnStyle.btg_Blanko;
            this.BTN_Sprache.Picture_3 = Helper.BtnStyle.btg_Blanko;
            this.BTN_Sprache.Picture_4 = Helper.BtnStyle.btg_Blanko;
            this.BTN_Sprache.PictureNumber = 0;
            this.BTN_Sprache.Size = new System.Drawing.Size(79, 48);
            this.BTN_Sprache.Symbol = null;
            this.BTN_Sprache.TabIndex = 8;
            this.BTN_Sprache.TabStop = false;
            this.BTN_Sprache.Click += new System.EventHandler(this.BTN_Sprache_Click);
            // 
            // BTN_Quit
            // 
            this.BTN_Quit.Caption = "";
            this.BTN_Quit.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold);
            this.BTN_Quit.Formular = null;
            this.BTN_Quit.Location = new System.Drawing.Point(10, 279);
            this.BTN_Quit.Name = "BTN_Quit";
            this.BTN_Quit.Picture_0 = Helper.BtnStyle.btg_Switchoff;
            this.BTN_Quit.Picture_1 = Helper.BtnStyle.btg_Blanko;
            this.BTN_Quit.Picture_2 = Helper.BtnStyle.btg_Blanko;
            this.BTN_Quit.Picture_3 = Helper.BtnStyle.btg_Blanko;
            this.BTN_Quit.Picture_4 = Helper.BtnStyle.btg_Blanko;
            this.BTN_Quit.PictureNumber = 0;
            this.BTN_Quit.Size = new System.Drawing.Size(79, 48);
            this.BTN_Quit.Symbol = null;
            this.BTN_Quit.TabIndex = 7;
            this.BTN_Quit.TabStop = false;
            this.BTN_Quit.Click += new System.EventHandler(this.bitButton1_Click);
            // 
            // BTN_User
            // 
            this.BTN_User.Caption = "";
            this.BTN_User.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold);
            this.BTN_User.Formular = null;
            this.BTN_User.Location = new System.Drawing.Point(10, 71);
            this.BTN_User.Name = "BTN_User";
            this.BTN_User.Picture_0 = Helper.BtnStyle.btg_UserNok;
            this.BTN_User.Picture_1 = Helper.BtnStyle.btg_UserOk;
            this.BTN_User.Picture_2 = Helper.BtnStyle.btg_Benutzer;
            this.BTN_User.Picture_3 = Helper.BtnStyle.btg_Blanko;
            this.BTN_User.Picture_4 = Helper.BtnStyle.btg_Blanko;
            this.BTN_User.PictureNumber = 2;
            this.BTN_User.Size = new System.Drawing.Size(79, 48);
            this.BTN_User.Symbol = null;
            this.BTN_User.TabIndex = 6;
            this.BTN_User.TabStop = false;
            this.BTN_User.Click += new System.EventHandler(this.BTN_User_Click);
            // 
            // BTN_Ende
            // 
            this.BTN_Ende.Caption = "";
            this.BTN_Ende.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold);
            this.BTN_Ende.Formular = null;
            this.BTN_Ende.Location = new System.Drawing.Point(10, 19);
            this.BTN_Ende.Name = "BTN_Ende";
            this.BTN_Ende.Picture_0 = Helper.BtnStyle.btg_Exit;
            this.BTN_Ende.Picture_1 = Helper.BtnStyle.btg_Blanko;
            this.BTN_Ende.Picture_2 = Helper.BtnStyle.btg_Blanko;
            this.BTN_Ende.Picture_3 = Helper.BtnStyle.btg_Blanko;
            this.BTN_Ende.Picture_4 = Helper.BtnStyle.btg_Blanko;
            this.BTN_Ende.PictureNumber = 0;
            this.BTN_Ende.Size = new System.Drawing.Size(79, 48);
            this.BTN_Ende.Symbol = null;
            this.BTN_Ende.TabIndex = 5;
            this.BTN_Ende.TabStop = false;
            this.BTN_Ende.Click += new System.EventHandler(this.BTN_Ende_Click);
            // 
            // FRM_Menu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Silver;
            this.ClientSize = new System.Drawing.Size(110, 345);
            this.Controls.Add(this.GB_Menu);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FRM_Menu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Menu";
            this.Activated += new System.EventHandler(this.Form2_Activated);
            this.Deactivate += new System.EventHandler(this.FRM_Menu_Deactivate);
            this.Load += new System.EventHandler(this.FRM_Menu_Load);
            this.GB_Menu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.BTN_KeyBoard)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BTN_PrintScreen)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BTN_Sprache)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BTN_Quit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BTN_User)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BTN_Ende)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox GB_Menu;
        private RotaBitButton BTN_User;
        private RotaBitButton BTN_Ende;
        private RotaBitButton BTN_Quit;
        private RotaBitButton BTN_KeyBoard;
        private RotaBitButton BTN_PrintScreen;
        private RotaBitButton BTN_Sprache;
    }
}