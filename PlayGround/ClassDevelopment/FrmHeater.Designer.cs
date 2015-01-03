namespace ClassDevelopment
{
    partial class FrmHeater
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
            this.Txt01 = new Helper.CompTxtBox();
            ((System.ComponentModel.ISupportInitialize)(this.BtnClose)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BtnKeyboard)).BeginInit();
            this.GbxMenu.SuspendLayout();
            this.GbxOutput.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BtnMenu)).BeginInit();
            this.SuspendLayout();
            // 
            // GbxMenu
            // 
            this.GbxMenu.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
            this.GbxMenu.Location = new System.Drawing.Point(666, 5);
            this.GbxMenu.Size = new System.Drawing.Size(99, 267);
            // 
            // GbxOutput
            // 
            this.GbxOutput.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
            this.GbxOutput.Controls.Add(this.Txt01);
            this.GbxOutput.Size = new System.Drawing.Size(652, 267);
            // 
            // Txt01
            // 
            this.Txt01.BackColor = System.Drawing.Color.Black;
            this.Txt01.Enabled = false;
            this.Txt01.Error = false;
            this.Txt01.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Bold);
            this.Txt01.ForeColor = System.Drawing.Color.Yellow;
            this.Txt01.Format = null;
            this.Txt01.Location = new System.Drawing.Point(49, 31);
            this.Txt01.Name = "Txt01";
            this.Txt01.Size = new System.Drawing.Size(59, 29);
            this.Txt01.TabIndex = 0;
            this.Txt01.Text = "0";
            this.Txt01.TextboxSize = Helper.CompTxtBox.CompTxtBoxSize.Small;
            this.Txt01.TextBoxType = Helper.CompTxtBox.CompTxtBoxType.ProcessVariable;
            // 
            // FrmHeater
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(775, 279);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "FrmHeater";
            this.Text = "FrmHeater";
            this.Load += new System.EventHandler(this.FrmHaeter_Load);
            ((System.ComponentModel.ISupportInitialize)(this.BtnClose)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BtnKeyboard)).EndInit();
            this.GbxMenu.ResumeLayout(false);
            this.GbxOutput.ResumeLayout(false);
            this.GbxOutput.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BtnMenu)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Helper.CompTxtBox Txt01;
    }
}