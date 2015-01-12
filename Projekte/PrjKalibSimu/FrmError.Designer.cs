namespace PrjKalibSimu
{
    partial class FrmError
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmError));
            this.BTN_RstError = new Helper.RotaBitButton();
            ((System.ComponentModel.ISupportInitialize)(this.BTN_Close)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BTN_Keyboard)).BeginInit();
            this.GB_Menu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BTN_Menu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BTN_RstError)).BeginInit();
            this.SuspendLayout();
            // 
            // GB_Menu
            // 
            this.GB_Menu.Controls.Add(this.BTN_RstError);
            this.GB_Menu.Location = new System.Drawing.Point(722, 5);
            this.GB_Menu.Size = new System.Drawing.Size(99, 518);
            this.GB_Menu.Controls.SetChildIndex(this.BTN_Close, 0);
            this.GB_Menu.Controls.SetChildIndex(this.BTN_Menu, 0);
            this.GB_Menu.Controls.SetChildIndex(this.BTN_Keyboard, 0);
            this.GB_Menu.Controls.SetChildIndex(this.BTN_RstError, 0);
            // 
            // GB_Datenausgabe
            // 
            this.GB_Datenausgabe.Size = new System.Drawing.Size(712, 518);
            // 
            // BTN_RstError
            // 
            this.BTN_RstError.Caption = "";
            this.BTN_RstError.EnableMouseDown = false;
            this.BTN_RstError.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold);
            this.BTN_RstError.Formular = null;
            this.BTN_RstError.Location = new System.Drawing.Point(10, 175);
            this.BTN_RstError.Name = "BTN_RstError";
            this.BTN_RstError.Picture_0 = Helper.BtnStyle.btg_Error;
            this.BTN_RstError.Picture_1 = Helper.BtnStyle.btg_Blanko;
            this.BTN_RstError.Picture_2 = Helper.BtnStyle.btg_Blanko;
            this.BTN_RstError.Picture_3 = Helper.BtnStyle.btg_Blanko;
            this.BTN_RstError.Picture_4 = Helper.BtnStyle.btg_Blanko;
            this.BTN_RstError.PictureNumber = 0;
            this.BTN_RstError.Size = new System.Drawing.Size(79, 48);
            this.BTN_RstError.Symbol = "DB52.Reset_Fehler";
            this.BTN_RstError.TabIndex = 3;
            this.BTN_RstError.TabStop = false;
            this.BTN_RstError.MouseDown += new System.Windows.Forms.MouseEventHandler(this.BtnDown);
            this.BTN_RstError.MouseUp += new System.Windows.Forms.MouseEventHandler(this.BtnUp);
            // 
            // FrmError
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.ClientSize = new System.Drawing.Size(826, 528);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmError";
            this.Load += new System.EventHandler(this.FrmError_Load);
            ((System.ComponentModel.ISupportInitialize)(this.BTN_Close)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BTN_Keyboard)).EndInit();
            this.GB_Menu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.BTN_Menu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BTN_RstError)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Helper.RotaBitButton BTN_RstError;

    }
}
