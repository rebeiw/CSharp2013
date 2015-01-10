namespace ClassDevelopment
{
    partial class FrmO2Curve
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.compMultiBar3 = new Helper.CompMultiBar();
            this.compMultiBar2 = new Helper.CompMultiBar();
            this.compBitButton1 = new Helper.CompBitButton();
            this.compInputBox1 = new Helper.CompInputBox();
            this.compMultiBar1 = new Helper.CompMultiBar();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            ((System.ComponentModel.ISupportInitialize)(this.BtnClose)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BtnKeyboard)).BeginInit();
            this.GbxMenu.SuspendLayout();
            this.GbxOutput.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BtnMenu)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.compMultiBar3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.compMultiBar2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.compBitButton1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.compMultiBar1)).BeginInit();
            this.SuspendLayout();
            // 
            // GbxMenu
            // 
            this.GbxMenu.Location = new System.Drawing.Point(976, 5);
            this.GbxMenu.Size = new System.Drawing.Size(99, 519);
            // 
            // GbxOutput
            // 
            this.GbxOutput.Controls.Add(this.tabControl1);
            this.GbxOutput.Size = new System.Drawing.Size(965, 519);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(7, 21);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(952, 490);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.compMultiBar3);
            this.tabPage1.Controls.Add(this.compMultiBar2);
            this.tabPage1.Controls.Add(this.compBitButton1);
            this.tabPage1.Controls.Add(this.compInputBox1);
            this.tabPage1.Controls.Add(this.compMultiBar1);
            this.tabPage1.Controls.Add(this.label9);
            this.tabPage1.Controls.Add(this.label8);
            this.tabPage1.Controls.Add(this.label7);
            this.tabPage1.Controls.Add(this.label6);
            this.tabPage1.Controls.Add(this.label5);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(944, 461);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // compMultiBar3
            // 
            this.compMultiBar3.ColorBar1 = System.Drawing.SystemColors.ActiveCaption;
            this.compMultiBar3.ColorBar2 = System.Drawing.Color.Orange;
            this.compMultiBar3.ColorBar3 = System.Drawing.Color.RoyalBlue;
            this.compMultiBar3.ColorBar4 = System.Drawing.Color.Blue;
            this.compMultiBar3.ColorBar5 = System.Drawing.Color.Green;
            this.compMultiBar3.Direction = Helper.CompMultiBar.CompVanDirection.Down;
            this.compMultiBar3.Location = new System.Drawing.Point(424, 140);
            this.compMultiBar3.MaxValue = 10D;
            this.compMultiBar3.Name = "compMultiBar3";
            this.compMultiBar3.NumberOfBars = 5;
            this.compMultiBar3.Select = true;
            this.compMultiBar3.Size = new System.Drawing.Size(246, 246);
            this.compMultiBar3.TabIndex = 15;
            this.compMultiBar3.TabStop = false;
            this.compMultiBar3.Value1 = 1D;
            this.compMultiBar3.Value2 = 1D;
            this.compMultiBar3.Value3 = 2D;
            this.compMultiBar3.Value4 = 8D;
            this.compMultiBar3.Value5 = 10D;
            this.compMultiBar3.Click += new System.EventHandler(this.compMultiBar3_Click);
            // 
            // compMultiBar2
            // 
            this.compMultiBar2.ColorBar1 = System.Drawing.SystemColors.ButtonHighlight;
            this.compMultiBar2.ColorBar2 = System.Drawing.Color.Empty;
            this.compMultiBar2.ColorBar3 = System.Drawing.Color.Red;
            this.compMultiBar2.ColorBar4 = System.Drawing.Color.Empty;
            this.compMultiBar2.ColorBar5 = System.Drawing.Color.Empty;
            this.compMultiBar2.Direction = Helper.CompMultiBar.CompVanDirection.Right;
            this.compMultiBar2.Location = new System.Drawing.Point(442, 163);
            this.compMultiBar2.MaxValue = 10D;
            this.compMultiBar2.Name = "compMultiBar2";
            this.compMultiBar2.NumberOfBars = 1;
            this.compMultiBar2.Select = false;
            this.compMultiBar2.Size = new System.Drawing.Size(63, 197);
            this.compMultiBar2.TabIndex = 14;
            this.compMultiBar2.TabStop = false;
            this.compMultiBar2.Value1 = 2D;
            this.compMultiBar2.Value2 = 0D;
            this.compMultiBar2.Value3 = 0D;
            this.compMultiBar2.Value4 = 0D;
            this.compMultiBar2.Value5 = 0D;
            // 
            // compBitButton1
            // 
            this.compBitButton1.Caption = "";
            this.compBitButton1.EnableMouseDown = false;
            this.compBitButton1.Formular = null;
            this.compBitButton1.Location = new System.Drawing.Point(292, 66);
            this.compBitButton1.Name = "compBitButton1";
            this.compBitButton1.Picture_0 = Helper.CompBitButtonStyle.btg_Blanko;
            this.compBitButton1.Picture_1 = Helper.CompBitButtonStyle.btg_Blanko;
            this.compBitButton1.Picture_2 = Helper.CompBitButtonStyle.btg_Blanko;
            this.compBitButton1.Picture_3 = Helper.CompBitButtonStyle.btg_Blanko;
            this.compBitButton1.Picture_4 = Helper.CompBitButtonStyle.btg_Blanko;
            this.compBitButton1.PictureNumber = 0;
            this.compBitButton1.Size = new System.Drawing.Size(79, 48);
            this.compBitButton1.Symbol = null;
            this.compBitButton1.TabIndex = 13;
            this.compBitButton1.TabStop = false;
            this.compBitButton1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.compBitButton1_MouseDown);
            // 
            // compInputBox1
            // 
            this.compInputBox1.BackColor = System.Drawing.Color.White;
            this.compInputBox1.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold);
            this.compInputBox1.ForeColor = System.Drawing.Color.Black;
            this.compInputBox1.Format = null;
            this.compInputBox1.Location = new System.Drawing.Point(315, 140);
            this.compInputBox1.Name = "compInputBox1";
            this.compInputBox1.Size = new System.Drawing.Size(89, 35);
            this.compInputBox1.Symbol = null;
            this.compInputBox1.TabIndex = 12;
            this.compInputBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.compInputBox1.Enter += new System.EventHandler(this.compInputBox1_Enter);
            // 
            // compMultiBar1
            // 
            this.compMultiBar1.ColorBar1 = System.Drawing.Color.Blue;
            this.compMultiBar1.ColorBar2 = System.Drawing.Color.Red;
            this.compMultiBar1.ColorBar3 = System.Drawing.Color.Black;
            this.compMultiBar1.ColorBar4 = System.Drawing.Color.Empty;
            this.compMultiBar1.ColorBar5 = System.Drawing.Color.Empty;
            this.compMultiBar1.Direction = Helper.CompMultiBar.CompVanDirection.Right;
            this.compMultiBar1.Location = new System.Drawing.Point(77, 163);
            this.compMultiBar1.MaxValue = 20D;
            this.compMultiBar1.Name = "compMultiBar1";
            this.compMultiBar1.NumberOfBars = 5;
            this.compMultiBar1.Select = false;
            this.compMultiBar1.Size = new System.Drawing.Size(30, 250);
            this.compMultiBar1.TabIndex = 11;
            this.compMultiBar1.TabStop = false;
            this.compMultiBar1.Value1 = 5D;
            this.compMultiBar1.Value2 = 1D;
            this.compMultiBar1.Value3 = 6D;
            this.compMultiBar1.Value4 = 0D;
            this.compMultiBar1.Value5 = 0D;
            this.compMultiBar1.Click += new System.EventHandler(this.compMultiBar1_Click);
            // 
            // label9
            // 
            this.label9.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(79, 420);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(18, 20);
            this.label9.TabIndex = 10;
            this.label9.Text = "0";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(6, 399);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(60, 20);
            this.label8.TabIndex = 7;
            this.label8.Text = "0,00-";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(7, 366);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(60, 20);
            this.label7.TabIndex = 6;
            this.label7.Text = "1,00-";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(7, 333);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(60, 20);
            this.label6.TabIndex = 5;
            this.label6.Text = "2,00-";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(7, 300);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(60, 20);
            this.label5.TabIndex = 4;
            this.label5.Text = "10,00-";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(7, 267);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(60, 20);
            this.label4.TabIndex = 3;
            this.label4.Text = "10,00-";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(7, 234);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 20);
            this.label3.TabIndex = 2;
            this.label3.Text = "10,00-";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(7, 201);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "10,00-";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(7, 168);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "10,00-";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(944, 461);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // FrmO2Curve
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1081, 528);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "FrmO2Curve";
            this.Text = "FrmO2Curve";
            this.Load += new System.EventHandler(this.FrmO2Curve_Load);
            ((System.ComponentModel.ISupportInitialize)(this.BtnClose)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BtnKeyboard)).EndInit();
            this.GbxMenu.ResumeLayout(false);
            this.GbxOutput.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.BtnMenu)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.compMultiBar3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.compMultiBar2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.compBitButton1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.compMultiBar1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabPage tabPage2;
        private Helper.CompMultiBar compMultiBar1;
        private Helper.CompInputBox compInputBox1;
        private Helper.CompBitButton compBitButton1;
        private Helper.CompMultiBar compMultiBar2;
        private Helper.CompMultiBar compMultiBar3;
    }
}