namespace PrjKalibSimu
{
    partial class FrmService
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmService));
            ((System.ComponentModel.ISupportInitialize)(this.BTN_Close)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BTN_Keyboard)).BeginInit();
            this.GB_Menu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BTN_Menu)).BeginInit();
            this.SuspendLayout();
            // 
            // GB_Menu
            // 
            this.GB_Menu.Location = new System.Drawing.Point(903, 5);
            this.GB_Menu.Size = new System.Drawing.Size(99, 719);
            // 
            // GB_Datenausgabe
            // 
            this.GB_Datenausgabe.Size = new System.Drawing.Size(892, 719);
            // 
            // FrmService
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.ClientSize = new System.Drawing.Size(1008, 730);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmService";
            this.Text = "Service";
            this.Load += new System.EventHandler(this.FrmService_Load);
            ((System.ComponentModel.ISupportInitialize)(this.BTN_Close)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BTN_Keyboard)).EndInit();
            this.GB_Menu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.BTN_Menu)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
    }
}
