namespace Helper
{
    partial class FrmDarstellung
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmDarstellung));
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.bitButton1 = new Helper.BitButton();
            this.bitButton2 = new Helper.BitButton();
            ((System.ComponentModel.ISupportInitialize)(this.BTN_Close)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BTN_Keyboard)).BeginInit();
            this.GB_Menu.SuspendLayout();
            this.GB_Datenausgabe.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BTN_Menu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bitButton1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bitButton2)).BeginInit();
            this.SuspendLayout();
            // 
            // GB_Menu
            // 
            this.GB_Menu.Controls.Add(this.bitButton2);
            this.GB_Menu.Controls.Add(this.bitButton1);
            this.GB_Menu.Location = new System.Drawing.Point(903, 5);
            this.GB_Menu.Size = new System.Drawing.Size(99, 719);
            this.GB_Menu.Controls.SetChildIndex(this.BTN_Close, 0);
            this.GB_Menu.Controls.SetChildIndex(this.BTN_Menu, 0);
            this.GB_Menu.Controls.SetChildIndex(this.BTN_Keyboard, 0);
            this.GB_Menu.Controls.SetChildIndex(this.bitButton1, 0);
            this.GB_Menu.Controls.SetChildIndex(this.bitButton2, 0);
            // 
            // GB_Datenausgabe
            // 
            this.GB_Datenausgabe.Controls.Add(this.chart1);
            this.GB_Datenausgabe.Size = new System.Drawing.Size(892, 719);
            // 
            // chart1
            // 
            this.chart1.BackColor = System.Drawing.Color.Transparent;
            chartArea1.BackColor = System.Drawing.Color.Transparent;
            chartArea1.CursorX.IsUserEnabled = true;
            chartArea1.CursorX.IsUserSelectionEnabled = true;
            chartArea1.CursorY.IsUserEnabled = true;
            chartArea1.CursorY.IsUserSelectionEnabled = true;
            chartArea1.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea1);
            legend1.BackColor = System.Drawing.Color.Transparent;
            legend1.Name = "Legend1";
            this.chart1.Legends.Add(legend1);
            this.chart1.Location = new System.Drawing.Point(7, 21);
            this.chart1.Name = "chart1";
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.chart1.Series.Add(series1);
            this.chart1.Size = new System.Drawing.Size(879, 692);
            this.chart1.TabIndex = 0;
            this.chart1.Text = "chart1";
            this.chart1.Click += new System.EventHandler(this.chart1_Click);
            // 
            // bitButton1
            // 
            this.bitButton1.Caption = "";
            this.bitButton1.EnableMouseDown = false;
            this.bitButton1.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold);
            this.bitButton1.Formular = null;
            this.bitButton1.Location = new System.Drawing.Point(10, 178);
            this.bitButton1.Name = "bitButton1";
            this.bitButton1.Picture_0 = Helper.BtnStyle.btg_Blanko;
            this.bitButton1.Picture_1 = Helper.BtnStyle.btg_Blanko;
            this.bitButton1.Picture_2 = Helper.BtnStyle.btg_Blanko;
            this.bitButton1.Picture_3 = Helper.BtnStyle.btg_Blanko;
            this.bitButton1.Picture_4 = Helper.BtnStyle.btg_Blanko;
            this.bitButton1.PictureNumber = 0;
            this.bitButton1.Size = new System.Drawing.Size(79, 48);
            this.bitButton1.Symbol = null;
            this.bitButton1.TabIndex = 3;
            this.bitButton1.TabStop = false;
            this.bitButton1.Click += new System.EventHandler(this.bitButton1_Click);
            // 
            // bitButton2
            // 
            this.bitButton2.Caption = "";
            this.bitButton2.EnableMouseDown = false;
            this.bitButton2.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold);
            this.bitButton2.Formular = null;
            this.bitButton2.Location = new System.Drawing.Point(10, 233);
            this.bitButton2.Name = "bitButton2";
            this.bitButton2.Picture_0 = Helper.BtnStyle.btg_Blanko;
            this.bitButton2.Picture_1 = Helper.BtnStyle.btg_Blanko;
            this.bitButton2.Picture_2 = Helper.BtnStyle.btg_Blanko;
            this.bitButton2.Picture_3 = Helper.BtnStyle.btg_Blanko;
            this.bitButton2.Picture_4 = Helper.BtnStyle.btg_Blanko;
            this.bitButton2.PictureNumber = 0;
            this.bitButton2.Size = new System.Drawing.Size(79, 48);
            this.bitButton2.Symbol = null;
            this.bitButton2.TabIndex = 4;
            this.bitButton2.TabStop = false;
            this.bitButton2.Click += new System.EventHandler(this.bitButton2_Click);
            // 
            // FrmDarstellung
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.ClientSize = new System.Drawing.Size(1008, 730);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmDarstellung";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.FrmDarstellung_Load);
            ((System.ComponentModel.ISupportInitialize)(this.BTN_Close)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BTN_Keyboard)).EndInit();
            this.GB_Menu.ResumeLayout(false);
            this.GB_Datenausgabe.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.BTN_Menu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bitButton1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bitButton2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private BitButton bitButton1;
        private BitButton bitButton2;
    }
}
