namespace Datenauswertung
{
    partial class FrmDatenauswertung
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmDatenauswertung));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.DBGRID_Source = new System.Windows.Forms.DataGridView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.wwwwToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.DATEPICKER_Von = new System.Windows.Forms.DateTimePicker();
            this.TIMEPICKER_Von = new System.Windows.Forms.DateTimePicker();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.DATEPICKER_Bis = new System.Windows.Forms.DateTimePicker();
            this.TIMEPICKER_Bis = new System.Windows.Forms.DateTimePicker();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.BTN_Auswaehlen = new Helper.BitButton();
            this.BTN_Delete = new Helper.BitButton();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.DBGRID_Drain = new System.Windows.Forms.DataGridView();
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.BTN_SuchenLoeschen = new Helper.BitButtonSmall();
            this.TXT_Suchen = new System.Windows.Forms.TextBox();
            this.BTN_Darstellung = new Helper.BitButton();
            ((System.ComponentModel.ISupportInitialize)(this.BTN_Close)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BTN_Keyboard)).BeginInit();
            this.GB_Menu.SuspendLayout();
            this.GB_Datenausgabe.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BTN_Menu)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DBGRID_Source)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BTN_Auswaehlen)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BTN_Delete)).BeginInit();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DBGRID_Drain)).BeginInit();
            this.contextMenuStrip2.SuspendLayout();
            this.groupBox6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BTN_SuchenLoeschen)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BTN_Darstellung)).BeginInit();
            this.SuspendLayout();
            // 
            // GB_Menu
            // 
            this.GB_Menu.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
            this.GB_Menu.Controls.Add(this.BTN_Darstellung);
            this.GB_Menu.Location = new System.Drawing.Point(903, 5);
            this.GB_Menu.Size = new System.Drawing.Size(99, 720);
            this.GB_Menu.Controls.SetChildIndex(this.BTN_Close, 0);
            this.GB_Menu.Controls.SetChildIndex(this.BTN_Menu, 0);
            this.GB_Menu.Controls.SetChildIndex(this.BTN_Keyboard, 0);
            this.GB_Menu.Controls.SetChildIndex(this.BTN_Darstellung, 0);
            // 
            // GB_Datenausgabe
            // 
            this.GB_Datenausgabe.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
            this.GB_Datenausgabe.Controls.Add(this.groupBox6);
            this.GB_Datenausgabe.Controls.Add(this.groupBox5);
            this.GB_Datenausgabe.Controls.Add(this.groupBox4);
            this.GB_Datenausgabe.Controls.Add(this.groupBox3);
            this.GB_Datenausgabe.Controls.Add(this.groupBox2);
            this.GB_Datenausgabe.Controls.Add(this.groupBox1);
            this.GB_Datenausgabe.Location = new System.Drawing.Point(7, 5);
            this.GB_Datenausgabe.Size = new System.Drawing.Size(891, 721);
            // 
            // BTN_Menu
            // 
            this.BTN_Menu.Click += new System.EventHandler(this.BTN_Menu_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.DBGRID_Source);
            this.groupBox1.Location = new System.Drawing.Point(7, 20);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(330, 693);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Spaltenauswahl";
            // 
            // DBGRID_Source
            // 
            this.DBGRID_Source.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DBGRID_Source.ColumnHeadersVisible = false;
            this.DBGRID_Source.ContextMenuStrip = this.contextMenuStrip1;
            this.DBGRID_Source.Location = new System.Drawing.Point(7, 21);
            this.DBGRID_Source.Name = "DBGRID_Source";
            this.DBGRID_Source.ReadOnly = true;
            this.DBGRID_Source.Size = new System.Drawing.Size(316, 666);
            this.DBGRID_Source.TabIndex = 1;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.wwwwToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(111, 26);
            // 
            // wwwwToolStripMenuItem
            // 
            this.wwwwToolStripMenuItem.Name = "wwwwToolStripMenuItem";
            this.wwwwToolStripMenuItem.Size = new System.Drawing.Size(110, 22);
            this.wwwwToolStripMenuItem.Text = "wwww";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.DATEPICKER_Von);
            this.groupBox2.Controls.Add(this.TIMEPICKER_Von);
            this.groupBox2.Location = new System.Drawing.Point(345, 333);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(200, 93);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Von";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(8, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 26);
            this.label2.TabIndex = 5;
            this.label2.Text = "Uhrzeit:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(8, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 26);
            this.label1.TabIndex = 4;
            this.label1.Text = "Datum:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // DATEPICKER_Von
            // 
            this.DATEPICKER_Von.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DATEPICKER_Von.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.DATEPICKER_Von.Location = new System.Drawing.Point(70, 20);
            this.DATEPICKER_Von.Name = "DATEPICKER_Von";
            this.DATEPICKER_Von.Size = new System.Drawing.Size(120, 26);
            this.DATEPICKER_Von.TabIndex = 3;
            this.DATEPICKER_Von.ValueChanged += new System.EventHandler(this.UpdateTimeStamp);
            // 
            // TIMEPICKER_Von
            // 
            this.TIMEPICKER_Von.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TIMEPICKER_Von.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.TIMEPICKER_Von.Location = new System.Drawing.Point(70, 56);
            this.TIMEPICKER_Von.Name = "TIMEPICKER_Von";
            this.TIMEPICKER_Von.ShowUpDown = true;
            this.TIMEPICKER_Von.Size = new System.Drawing.Size(120, 26);
            this.TIMEPICKER_Von.TabIndex = 2;
            this.TIMEPICKER_Von.ValueChanged += new System.EventHandler(this.UpdateTimeStamp);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.DATEPICKER_Bis);
            this.groupBox3.Controls.Add(this.TIMEPICKER_Bis);
            this.groupBox3.Location = new System.Drawing.Point(345, 440);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(200, 93);
            this.groupBox3.TabIndex = 5;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Bis";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(8, 56);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 26);
            this.label3.TabIndex = 5;
            this.label3.Text = "Uhrzeit:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(8, 20);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(60, 26);
            this.label4.TabIndex = 4;
            this.label4.Text = "Datum:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // DATEPICKER_Bis
            // 
            this.DATEPICKER_Bis.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DATEPICKER_Bis.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.DATEPICKER_Bis.Location = new System.Drawing.Point(70, 20);
            this.DATEPICKER_Bis.Name = "DATEPICKER_Bis";
            this.DATEPICKER_Bis.Size = new System.Drawing.Size(120, 26);
            this.DATEPICKER_Bis.TabIndex = 3;
            this.DATEPICKER_Bis.ValueChanged += new System.EventHandler(this.UpdateTimeStamp);
            // 
            // TIMEPICKER_Bis
            // 
            this.TIMEPICKER_Bis.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TIMEPICKER_Bis.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.TIMEPICKER_Bis.Location = new System.Drawing.Point(70, 56);
            this.TIMEPICKER_Bis.Name = "TIMEPICKER_Bis";
            this.TIMEPICKER_Bis.ShowUpDown = true;
            this.TIMEPICKER_Bis.Size = new System.Drawing.Size(119, 26);
            this.TIMEPICKER_Bis.TabIndex = 2;
            this.TIMEPICKER_Bis.ValueChanged += new System.EventHandler(this.UpdateTimeStamp);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.BTN_Auswaehlen);
            this.groupBox4.Controls.Add(this.BTN_Delete);
            this.groupBox4.Location = new System.Drawing.Point(345, 145);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(200, 174);
            this.groupBox4.TabIndex = 6;
            this.groupBox4.TabStop = false;
            // 
            // BTN_Auswaehlen
            // 
            this.BTN_Auswaehlen.Caption = "";
            this.BTN_Auswaehlen.EnableMouseDown = false;
            this.BTN_Auswaehlen.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold);
            this.BTN_Auswaehlen.Formular = null;
            this.BTN_Auswaehlen.Location = new System.Drawing.Point(61, 94);
            this.BTN_Auswaehlen.Name = "BTN_Auswaehlen";
            this.BTN_Auswaehlen.Picture_0 = Helper.BtnStyle.btg_PfeilRechts;
            this.BTN_Auswaehlen.Picture_1 = Helper.BtnStyle.btg_Blanko;
            this.BTN_Auswaehlen.Picture_2 = Helper.BtnStyle.btg_Blanko;
            this.BTN_Auswaehlen.Picture_3 = Helper.BtnStyle.btg_Blanko;
            this.BTN_Auswaehlen.Picture_4 = Helper.BtnStyle.btg_Blanko;
            this.BTN_Auswaehlen.PictureNumber = 0;
            this.BTN_Auswaehlen.Size = new System.Drawing.Size(79, 48);
            this.BTN_Auswaehlen.Symbol = null;
            this.BTN_Auswaehlen.TabIndex = 6;
            this.BTN_Auswaehlen.TabStop = false;
            this.BTN_Auswaehlen.Click += new System.EventHandler(this.bitButton3_Click);
            // 
            // BTN_Delete
            // 
            this.BTN_Delete.Caption = "";
            this.BTN_Delete.EnableMouseDown = false;
            this.BTN_Delete.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold);
            this.BTN_Delete.Formular = null;
            this.BTN_Delete.Location = new System.Drawing.Point(61, 42);
            this.BTN_Delete.Name = "BTN_Delete";
            this.BTN_Delete.Picture_0 = Helper.BtnStyle.btg_Esc;
            this.BTN_Delete.Picture_1 = Helper.BtnStyle.btg_Blanko;
            this.BTN_Delete.Picture_2 = Helper.BtnStyle.btg_Blanko;
            this.BTN_Delete.Picture_3 = Helper.BtnStyle.btg_Blanko;
            this.BTN_Delete.Picture_4 = Helper.BtnStyle.btg_Blanko;
            this.BTN_Delete.PictureNumber = 0;
            this.BTN_Delete.Size = new System.Drawing.Size(79, 48);
            this.BTN_Delete.Symbol = null;
            this.BTN_Delete.TabIndex = 4;
            this.BTN_Delete.TabStop = false;
            this.BTN_Delete.Click += new System.EventHandler(this.bitButton1_Click);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.DBGRID_Drain);
            this.groupBox5.Location = new System.Drawing.Point(553, 20);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(330, 693);
            this.groupBox5.TabIndex = 7;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Auswahl";
            // 
            // DBGRID_Drain
            // 
            this.DBGRID_Drain.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DBGRID_Drain.ColumnHeadersVisible = false;
            this.DBGRID_Drain.ContextMenuStrip = this.contextMenuStrip2;
            this.DBGRID_Drain.Location = new System.Drawing.Point(7, 21);
            this.DBGRID_Drain.Name = "DBGRID_Drain";
            this.DBGRID_Drain.ReadOnly = true;
            this.DBGRID_Drain.Size = new System.Drawing.Size(316, 666);
            this.DBGRID_Drain.TabIndex = 0;
            // 
            // contextMenuStrip2
            // 
            this.contextMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1});
            this.contextMenuStrip2.Name = "contextMenuStrip2";
            this.contextMenuStrip2.Size = new System.Drawing.Size(181, 26);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(180, 22);
            this.toolStripMenuItem1.Text = "toolStripMenuItem1";
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.BTN_SuchenLoeschen);
            this.groupBox6.Controls.Add(this.TXT_Suchen);
            this.groupBox6.Location = new System.Drawing.Point(345, 20);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(200, 111);
            this.groupBox6.TabIndex = 8;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Suchen";
            // 
            // BTN_SuchenLoeschen
            // 
            this.BTN_SuchenLoeschen.Caption = "";
            this.BTN_SuchenLoeschen.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold);
            this.BTN_SuchenLoeschen.Formular = null;
            this.BTN_SuchenLoeschen.Location = new System.Drawing.Point(156, 21);
            this.BTN_SuchenLoeschen.Name = "BTN_SuchenLoeschen";
            this.BTN_SuchenLoeschen.Picture_0 = Helper.BtnStyleSmall.btk_Delete;
            this.BTN_SuchenLoeschen.Picture_1 = Helper.BtnStyleSmall.btk_Blanko;
            this.BTN_SuchenLoeschen.Picture_2 = Helper.BtnStyleSmall.btk_Blanko;
            this.BTN_SuchenLoeschen.Picture_3 = Helper.BtnStyleSmall.btk_Blanko;
            this.BTN_SuchenLoeschen.Picture_4 = Helper.BtnStyleSmall.btk_Blanko;
            this.BTN_SuchenLoeschen.PictureNumber = 0;
            this.BTN_SuchenLoeschen.Size = new System.Drawing.Size(40, 24);
            this.BTN_SuchenLoeschen.Symbol = null;
            this.BTN_SuchenLoeschen.TabIndex = 2;
            this.BTN_SuchenLoeschen.TabStop = false;
            this.BTN_SuchenLoeschen.Click += new System.EventHandler(this.bitButtonSmall2_Click);
            // 
            // TXT_Suchen
            // 
            this.TXT_Suchen.Location = new System.Drawing.Point(7, 21);
            this.TXT_Suchen.Name = "TXT_Suchen";
            this.TXT_Suchen.Size = new System.Drawing.Size(143, 22);
            this.TXT_Suchen.TabIndex = 0;
            this.TXT_Suchen.TextChanged += new System.EventHandler(this.TXT_Suchen_TextChanged);
            // 
            // BTN_Darstellung
            // 
            this.BTN_Darstellung.Caption = "";
            this.BTN_Darstellung.EnableMouseDown = false;
            this.BTN_Darstellung.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold);
            this.BTN_Darstellung.Formular = null;
            this.BTN_Darstellung.Location = new System.Drawing.Point(10, 175);
            this.BTN_Darstellung.Name = "BTN_Darstellung";
            this.BTN_Darstellung.Picture_0 = Helper.BtnStyle.btg_Graph;
            this.BTN_Darstellung.Picture_1 = Helper.BtnStyle.btg_Blanko;
            this.BTN_Darstellung.Picture_2 = Helper.BtnStyle.btg_Blanko;
            this.BTN_Darstellung.Picture_3 = Helper.BtnStyle.btg_Blanko;
            this.BTN_Darstellung.Picture_4 = Helper.BtnStyle.btg_Blanko;
            this.BTN_Darstellung.PictureNumber = 0;
            this.BTN_Darstellung.Size = new System.Drawing.Size(79, 48);
            this.BTN_Darstellung.Symbol = null;
            this.BTN_Darstellung.TabIndex = 3;
            this.BTN_Darstellung.TabStop = false;
            this.BTN_Darstellung.Click += new System.EventHandler(this.bitButton4_Click);
            // 
            // FrmDatenauswertung
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 730);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "FrmDatenauswertung";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Datenauswertung";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Controls.SetChildIndex(this.GB_Menu, 0);
            this.Controls.SetChildIndex(this.GB_Datenausgabe, 0);
            ((System.ComponentModel.ISupportInitialize)(this.BTN_Close)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BTN_Keyboard)).EndInit();
            this.GB_Menu.ResumeLayout(false);
            this.GB_Datenausgabe.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.BTN_Menu)).EndInit();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DBGRID_Source)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.BTN_Auswaehlen)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BTN_Delete)).EndInit();
            this.groupBox5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DBGRID_Drain)).EndInit();
            this.contextMenuStrip2.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BTN_SuchenLoeschen)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BTN_Darstellung)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker DATEPICKER_Von;
        private System.Windows.Forms.DateTimePicker TIMEPICKER_Von;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker DATEPICKER_Bis;
        private System.Windows.Forms.DateTimePicker TIMEPICKER_Bis;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.DataGridView DBGRID_Drain;
        private System.Windows.Forms.GroupBox groupBox4;
        private Helper.BitButton BTN_Delete;
        private System.Windows.Forms.DataGridView DBGRID_Source;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.TextBox TXT_Suchen;
        private Helper.BitButton BTN_Auswaehlen;
        private Helper.BitButton BTN_Darstellung;
        private Helper.BitButtonSmall BTN_SuchenLoeschen;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem wwwwToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
    }
}

