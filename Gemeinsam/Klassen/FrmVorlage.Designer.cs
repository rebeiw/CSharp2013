namespace Helper
{
    partial class FrmVorlage
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmVorlage));
            this.compInputBox1 = new Helper.CompInputBox();
            this.SuspendLayout();
            // 
            // compInputBox1
            // 
            this.compInputBox1.BackColor = System.Drawing.Color.White;
            this.compInputBox1.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold);
            this.compInputBox1.ForeColor = System.Drawing.Color.Black;
            this.compInputBox1.Format = null;
            this.compInputBox1.Location = new System.Drawing.Point(331, 209);
            this.compInputBox1.Name = "compInputBox1";
            this.compInputBox1.Size = new System.Drawing.Size(79, 35);
            this.compInputBox1.Symbol = null;
            this.compInputBox1.TabIndex = 0;
            this.compInputBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // FrmVorlage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Silver;
            this.ClientSize = new System.Drawing.Size(613, 528);
            this.Controls.Add(this.compInputBox1);
            this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmVorlage";
            this.Text = "FrmVorlage";
            this.Load += new System.EventHandler(this.FrmVorlage_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private CompInputBox compInputBox1;




    }
}