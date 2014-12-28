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
            this.GbxOutput = new System.Windows.Forms.GroupBox();
            this.LblMessage = new System.Windows.Forms.Label();
            this.BtnOk = new Helper.CompBitButton();
            this.BtnCancel = new Helper.CompBitButton();
            this.GbxOutput.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BtnOk)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BtnCancel)).BeginInit();
            this.SuspendLayout();
            // 
            // GbxOutput
            // 
            this.GbxOutput.Controls.Add(this.LblMessage);
            this.GbxOutput.Controls.Add(this.BtnOk);
            this.GbxOutput.Controls.Add(this.BtnCancel);
            this.GbxOutput.Location = new System.Drawing.Point(5, 5);
            this.GbxOutput.Name = "GbxOutput";
            this.GbxOutput.Size = new System.Drawing.Size(197, 129);
            this.GbxOutput.TabIndex = 2;
            this.GbxOutput.TabStop = false;
            // 
            // LblMessage
            // 
            this.LblMessage.Location = new System.Drawing.Point(7, 18);
            this.LblMessage.Name = "LblMessage";
            this.LblMessage.Size = new System.Drawing.Size(182, 44);
            this.LblMessage.TabIndex = 4;
            this.LblMessage.Text = "Programm beenden?";
            this.LblMessage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // BtnOk
            // 
            this.BtnOk.Caption = "";
            this.BtnOk.EnableMouseDown = false;
            this.BtnOk.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold);
            this.BtnOk.Formular = null;
            this.BtnOk.Location = new System.Drawing.Point(8, 73);
            this.BtnOk.Name = "BtnOk";
            this.BtnOk.Picture_0 = Helper.CompBitButtonStyle.btg_Ok;
            this.BtnOk.Picture_1 = Helper.CompBitButtonStyle.btg_Blanko;
            this.BtnOk.Picture_2 = Helper.CompBitButtonStyle.btg_Blanko;
            this.BtnOk.Picture_3 = Helper.CompBitButtonStyle.btg_Blanko;
            this.BtnOk.Picture_4 = Helper.CompBitButtonStyle.btg_Blanko;
            this.BtnOk.PictureNumber = 0;
            this.BtnOk.Size = new System.Drawing.Size(79, 48);
            this.BtnOk.Symbol = null;
            this.BtnOk.TabIndex = 3;
            this.BtnOk.TabStop = false;
            this.BtnOk.Click += new System.EventHandler(this.BtnOk_Click);
            // 
            // BtnCancel
            // 
            this.BtnCancel.Caption = "";
            this.BtnCancel.EnableMouseDown = false;
            this.BtnCancel.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold);
            this.BtnCancel.Formular = null;
            this.BtnCancel.Location = new System.Drawing.Point(110, 73);
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.Picture_0 = Helper.CompBitButtonStyle.btg_Esc;
            this.BtnCancel.Picture_1 = Helper.CompBitButtonStyle.btg_Blanko;
            this.BtnCancel.Picture_2 = Helper.CompBitButtonStyle.btg_Blanko;
            this.BtnCancel.Picture_3 = Helper.CompBitButtonStyle.btg_Blanko;
            this.BtnCancel.Picture_4 = Helper.CompBitButtonStyle.btg_Blanko;
            this.BtnCancel.PictureNumber = 0;
            this.BtnCancel.Size = new System.Drawing.Size(79, 48);
            this.BtnCancel.Symbol = null;
            this.BtnCancel.TabIndex = 2;
            this.BtnCancel.TabStop = false;
            this.BtnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // FrmProgEnd
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.ClientSize = new System.Drawing.Size(210, 139);
            this.Controls.Add(this.GbxOutput);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FrmProgEnd";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Programm beenden";
            this.GbxOutput.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.BtnOk)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BtnCancel)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox GbxOutput;
        private System.Windows.Forms.Label LblMessage;
        private CompBitButton BtnOk;
        private CompBitButton BtnCancel;
    }
}
