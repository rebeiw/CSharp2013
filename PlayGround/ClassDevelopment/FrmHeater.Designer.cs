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
            this.Txt02 = new Helper.CompTxtBox();
            this.Txt03 = new Helper.CompTxtBox();
            this.Led01 = new Helper.CompLedRectangle();
            this.Pip01 = new Helper.CompPipe();
            this.Vnt01 = new Helper.CompVentil();
            this.textBox1 = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.BtnClose)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BtnKeyboard)).BeginInit();
            this.GbxMenu.SuspendLayout();
            this.GbxOutput.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BtnMenu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Led01)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Pip01)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Vnt01)).BeginInit();
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
            this.GbxOutput.Controls.Add(this.textBox1);
            this.GbxOutput.Controls.Add(this.Vnt01);
            this.GbxOutput.Controls.Add(this.Pip01);
            this.GbxOutput.Controls.Add(this.Led01);
            this.GbxOutput.Controls.Add(this.Txt03);
            this.GbxOutput.Controls.Add(this.Txt02);
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
            this.Txt01.TextSymbol = "";
            // 
            // Txt02
            // 
            this.Txt02.BackColor = System.Drawing.Color.Black;
            this.Txt02.Enabled = false;
            this.Txt02.Error = false;
            this.Txt02.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Bold);
            this.Txt02.ForeColor = System.Drawing.Color.Yellow;
            this.Txt02.Format = null;
            this.Txt02.Location = new System.Drawing.Point(297, 119);
            this.Txt02.Name = "Txt02";
            this.Txt02.Size = new System.Drawing.Size(59, 29);
            this.Txt02.TabIndex = 1;
            this.Txt02.Text = "0";
            this.Txt02.TextboxSize = Helper.CompTxtBox.CompTxtBoxSize.Small;
            this.Txt02.TextBoxType = Helper.CompTxtBox.CompTxtBoxType.ProcessVariable;
            this.Txt02.TextSymbol = "";
            // 
            // Txt03
            // 
            this.Txt03.BackColor = System.Drawing.Color.Black;
            this.Txt03.Enabled = false;
            this.Txt03.Error = false;
            this.Txt03.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Bold);
            this.Txt03.ForeColor = System.Drawing.Color.Yellow;
            this.Txt03.Format = null;
            this.Txt03.Location = new System.Drawing.Point(297, 58);
            this.Txt03.Name = "Txt03";
            this.Txt03.Size = new System.Drawing.Size(59, 29);
            this.Txt03.TabIndex = 2;
            this.Txt03.Text = "0";
            this.Txt03.TextboxSize = Helper.CompTxtBox.CompTxtBoxSize.Small;
            this.Txt03.TextBoxType = Helper.CompTxtBox.CompTxtBoxType.ProcessVariable;
            this.Txt03.TextSymbol = "DB50.P1_Qmin_1";
            // 
            // Led01
            // 
            this.Led01.Location = new System.Drawing.Point(164, 31);
            this.Led01.Name = "Led01";
            this.Led01.Size = new System.Drawing.Size(79, 39);
            this.Led01.State = Helper.CompLedRectangle.CompLedState.LedOff;
            this.Led01.TabIndex = 3;
            this.Led01.TabStop = false;
            // 
            // Pip01
            // 
            this.Pip01.BackColor = System.Drawing.Color.Blue;
            this.Pip01.ColorFlowOff = System.Drawing.Color.Blue;
            this.Pip01.ColorFlowOn = System.Drawing.Color.Aqua;
            this.Pip01.Flow = Helper.CompPipe.CompPipeFlow.FlowOff;
            this.Pip01.Location = new System.Drawing.Point(310, 169);
            this.Pip01.Name = "Pip01";
            this.Pip01.Size = new System.Drawing.Size(100, 10);
            this.Pip01.TabIndex = 4;
            this.Pip01.TabStop = false;
            // 
            // Vnt01
            // 
            this.Vnt01.ColorClose = System.Drawing.Color.Aqua;
            this.Vnt01.ColorOpen = System.Drawing.Color.Blue;
            this.Vnt01.Direction = Helper.CompVentil.CompVentilDirection.Horizontal;
            this.Vnt01.Location = new System.Drawing.Point(145, 139);
            this.Vnt01.Name = "Vnt01";
            this.Vnt01.Size = new System.Drawing.Size(40, 40);
            this.Vnt01.State = Helper.CompVentil.CompVentilState.Open;
            this.Vnt01.TabIndex = 5;
            this.Vnt01.TabStop = false;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(452, 66);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 22);
            this.textBox1.TabIndex = 6;
            // 
            // FrmHeater
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(775, 279);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "FrmHeater";
            this.Text = "Ich find";
            this.Activated += new System.EventHandler(this.FrmHeater_Activated);
            this.Load += new System.EventHandler(this.FrmHaeter_Load);
            ((System.ComponentModel.ISupportInitialize)(this.BtnClose)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BtnKeyboard)).EndInit();
            this.GbxMenu.ResumeLayout(false);
            this.GbxOutput.ResumeLayout(false);
            this.GbxOutput.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BtnMenu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Led01)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Pip01)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Vnt01)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Helper.CompTxtBox Txt01;
        private Helper.CompTxtBox Txt03;
        private Helper.CompTxtBox Txt02;
        private Helper.CompLedRectangle Led01;
        private Helper.CompVentil Vnt01;
        private Helper.CompPipe Pip01;
        private System.Windows.Forms.TextBox textBox1;
    }
}