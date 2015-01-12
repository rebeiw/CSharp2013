namespace KalibHelper
{
    partial class FrmMainScreen
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.BTN_NeueSuche_1 = new Helper.BitButtonSmall();
            this.BTN_Suchen_1 = new Helper.BitButtonSmall();
            this.TXT_Link_Eingabe = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.DBGRID_Eingabe = new System.Windows.Forms.DataGridView();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.BTN_NeueSuche_2 = new Helper.BitButtonSmall();
            this.BTN_Suchen_2 = new Helper.BitButtonSmall();
            this.DBGRID_Ergebnis = new System.Windows.Forms.DataGridView();
            this.TXT_Link_Ergebnis = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.BTN_NeueSuche_3 = new Helper.BitButtonSmall();
            this.BTN_Suchen_3 = new Helper.BitButtonSmall();
            this.DBGRID_Messwerte = new System.Windows.Forms.DataGridView();
            this.TXT_Link_Messwerte = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.BTN_Suchen = new Helper.BitButton();
            this.BTN_Excel = new Helper.BitButton();
            this.BTN_NeueSuche = new Helper.BitButton();
            this.BTN_OpenFolderDocumente = new Helper.BitButton();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.suchenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.oeffnenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bitButton1 = new Helper.BitButton();
            ((System.ComponentModel.ISupportInitialize)(this.BTN_Close)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BTN_Keyboard)).BeginInit();
            this.GB_Menu.SuspendLayout();
            this.GB_Datenausgabe.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BTN_Menu)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BTN_NeueSuche_1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BTN_Suchen_1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DBGRID_Eingabe)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BTN_NeueSuche_2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BTN_Suchen_2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DBGRID_Ergebnis)).BeginInit();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BTN_NeueSuche_3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BTN_Suchen_3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DBGRID_Messwerte)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BTN_Suchen)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BTN_Excel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BTN_NeueSuche)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BTN_OpenFolderDocumente)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bitButton1)).BeginInit();
            this.SuspendLayout();
            // 
            // GB_Menu
            // 
            this.GB_Menu.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.GB_Menu.Controls.Add(this.bitButton1);
            this.GB_Menu.Controls.Add(this.BTN_OpenFolderDocumente);
            this.GB_Menu.Controls.Add(this.BTN_NeueSuche);
            this.GB_Menu.Controls.Add(this.BTN_Excel);
            this.GB_Menu.Controls.Add(this.BTN_Suchen);
            this.GB_Menu.Location = new System.Drawing.Point(904, 5);
            this.GB_Menu.Size = new System.Drawing.Size(99, 720);
            this.GB_Menu.Controls.SetChildIndex(this.BTN_Close, 0);
            this.GB_Menu.Controls.SetChildIndex(this.BTN_Menu, 0);
            this.GB_Menu.Controls.SetChildIndex(this.BTN_Keyboard, 0);
            this.GB_Menu.Controls.SetChildIndex(this.BTN_Suchen, 0);
            this.GB_Menu.Controls.SetChildIndex(this.BTN_Excel, 0);
            this.GB_Menu.Controls.SetChildIndex(this.BTN_NeueSuche, 0);
            this.GB_Menu.Controls.SetChildIndex(this.BTN_OpenFolderDocumente, 0);
            this.GB_Menu.Controls.SetChildIndex(this.bitButton1, 0);
            // 
            // GB_Datenausgabe
            // 
            this.GB_Datenausgabe.Controls.Add(this.tabControl1);
            this.GB_Datenausgabe.Size = new System.Drawing.Size(896, 720);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.ItemSize = new System.Drawing.Size(72, 39);
            this.tabControl1.Location = new System.Drawing.Point(7, 21);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(883, 692);
            this.tabControl1.TabIndex = 4;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.Silver;
            this.tabPage1.Controls.Add(this.BTN_NeueSuche_1);
            this.tabPage1.Controls.Add(this.BTN_Suchen_1);
            this.tabPage1.Controls.Add(this.TXT_Link_Eingabe);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.DBGRID_Eingabe);
            this.tabPage1.Location = new System.Drawing.Point(4, 43);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(875, 645);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Eingabe";
            // 
            // BTN_NeueSuche_1
            // 
            this.BTN_NeueSuche_1.Caption = "";
            this.BTN_NeueSuche_1.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold);
            this.BTN_NeueSuche_1.Formular = null;
            this.BTN_NeueSuche_1.Location = new System.Drawing.Point(404, 4);
            this.BTN_NeueSuche_1.Name = "BTN_NeueSuche_1";
            this.BTN_NeueSuche_1.Picture_0 = Helper.BtnStyleSmall.btk_New;
            this.BTN_NeueSuche_1.Picture_1 = Helper.BtnStyleSmall.btk_Blanko;
            this.BTN_NeueSuche_1.Picture_2 = Helper.BtnStyleSmall.btk_Blanko;
            this.BTN_NeueSuche_1.Picture_3 = Helper.BtnStyleSmall.btk_Blanko;
            this.BTN_NeueSuche_1.Picture_4 = Helper.BtnStyleSmall.btk_Blanko;
            this.BTN_NeueSuche_1.PictureNumber = 0;
            this.BTN_NeueSuche_1.Size = new System.Drawing.Size(40, 24);
            this.BTN_NeueSuche_1.Symbol = null;
            this.BTN_NeueSuche_1.TabIndex = 9;
            this.BTN_NeueSuche_1.TabStop = false;
            this.BTN_NeueSuche_1.Visible = false;
            this.BTN_NeueSuche_1.Click += new System.EventHandler(this.BTN_NeueSuche_Click);
            // 
            // BTN_Suchen_1
            // 
            this.BTN_Suchen_1.Caption = "";
            this.BTN_Suchen_1.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold);
            this.BTN_Suchen_1.Formular = null;
            this.BTN_Suchen_1.Location = new System.Drawing.Point(358, 4);
            this.BTN_Suchen_1.Name = "BTN_Suchen_1";
            this.BTN_Suchen_1.Picture_0 = Helper.BtnStyleSmall.btk_Refresh;
            this.BTN_Suchen_1.Picture_1 = Helper.BtnStyleSmall.btk_Blanko;
            this.BTN_Suchen_1.Picture_2 = Helper.BtnStyleSmall.btk_Blanko;
            this.BTN_Suchen_1.Picture_3 = Helper.BtnStyleSmall.btk_Blanko;
            this.BTN_Suchen_1.Picture_4 = Helper.BtnStyleSmall.btk_Blanko;
            this.BTN_Suchen_1.PictureNumber = 0;
            this.BTN_Suchen_1.Size = new System.Drawing.Size(40, 24);
            this.BTN_Suchen_1.Symbol = null;
            this.BTN_Suchen_1.TabIndex = 8;
            this.BTN_Suchen_1.TabStop = false;
            this.BTN_Suchen_1.Click += new System.EventHandler(this.BTN_Suchen_Click);
            // 
            // TXT_Link_Eingabe
            // 
            this.TXT_Link_Eingabe.Location = new System.Drawing.Point(85, 5);
            this.TXT_Link_Eingabe.Name = "TXT_Link_Eingabe";
            this.TXT_Link_Eingabe.Size = new System.Drawing.Size(267, 22);
            this.TXT_Link_Eingabe.TabIndex = 7;
            this.TXT_Link_Eingabe.TextChanged += new System.EventHandler(this.TXT_Link_Changed);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(5, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 22);
            this.label1.TabIndex = 5;
            this.label1.Text = "Link:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // DBGRID_Eingabe
            // 
            this.DBGRID_Eingabe.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DBGRID_Eingabe.Location = new System.Drawing.Point(5, 38);
            this.DBGRID_Eingabe.Name = "DBGRID_Eingabe";
            this.DBGRID_Eingabe.ReadOnly = true;
            this.DBGRID_Eingabe.Size = new System.Drawing.Size(863, 602);
            this.DBGRID_Eingabe.TabIndex = 4;
            this.DBGRID_Eingabe.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DBGRID_Eingabe_CellClick);
            this.DBGRID_Eingabe.SelectionChanged += new System.EventHandler(this.DBGRID_Eingabe_SelectionChanged);
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.Color.Silver;
            this.tabPage2.Controls.Add(this.BTN_NeueSuche_2);
            this.tabPage2.Controls.Add(this.BTN_Suchen_2);
            this.tabPage2.Controls.Add(this.DBGRID_Ergebnis);
            this.tabPage2.Controls.Add(this.TXT_Link_Ergebnis);
            this.tabPage2.Controls.Add(this.label2);
            this.tabPage2.Location = new System.Drawing.Point(4, 43);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(875, 645);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Ergebnis";
            // 
            // BTN_NeueSuche_2
            // 
            this.BTN_NeueSuche_2.Caption = "";
            this.BTN_NeueSuche_2.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold);
            this.BTN_NeueSuche_2.Formular = null;
            this.BTN_NeueSuche_2.Location = new System.Drawing.Point(404, 4);
            this.BTN_NeueSuche_2.Name = "BTN_NeueSuche_2";
            this.BTN_NeueSuche_2.Picture_0 = Helper.BtnStyleSmall.btk_New;
            this.BTN_NeueSuche_2.Picture_1 = Helper.BtnStyleSmall.btk_Blanko;
            this.BTN_NeueSuche_2.Picture_2 = Helper.BtnStyleSmall.btk_Blanko;
            this.BTN_NeueSuche_2.Picture_3 = Helper.BtnStyleSmall.btk_Blanko;
            this.BTN_NeueSuche_2.Picture_4 = Helper.BtnStyleSmall.btk_Blanko;
            this.BTN_NeueSuche_2.PictureNumber = 0;
            this.BTN_NeueSuche_2.Size = new System.Drawing.Size(40, 24);
            this.BTN_NeueSuche_2.Symbol = null;
            this.BTN_NeueSuche_2.TabIndex = 12;
            this.BTN_NeueSuche_2.TabStop = false;
            this.BTN_NeueSuche_2.Visible = false;
            this.BTN_NeueSuche_2.Click += new System.EventHandler(this.BTN_NeueSuche_Click);
            // 
            // BTN_Suchen_2
            // 
            this.BTN_Suchen_2.Caption = "";
            this.BTN_Suchen_2.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold);
            this.BTN_Suchen_2.Formular = null;
            this.BTN_Suchen_2.Location = new System.Drawing.Point(358, 4);
            this.BTN_Suchen_2.Name = "BTN_Suchen_2";
            this.BTN_Suchen_2.Picture_0 = Helper.BtnStyleSmall.btk_Refresh;
            this.BTN_Suchen_2.Picture_1 = Helper.BtnStyleSmall.btk_Blanko;
            this.BTN_Suchen_2.Picture_2 = Helper.BtnStyleSmall.btk_Blanko;
            this.BTN_Suchen_2.Picture_3 = Helper.BtnStyleSmall.btk_Blanko;
            this.BTN_Suchen_2.Picture_4 = Helper.BtnStyleSmall.btk_Blanko;
            this.BTN_Suchen_2.PictureNumber = 0;
            this.BTN_Suchen_2.Size = new System.Drawing.Size(40, 24);
            this.BTN_Suchen_2.Symbol = null;
            this.BTN_Suchen_2.TabIndex = 11;
            this.BTN_Suchen_2.TabStop = false;
            this.BTN_Suchen_2.Click += new System.EventHandler(this.BTN_Suchen_Click);
            // 
            // DBGRID_Ergebnis
            // 
            this.DBGRID_Ergebnis.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DBGRID_Ergebnis.Location = new System.Drawing.Point(5, 38);
            this.DBGRID_Ergebnis.Name = "DBGRID_Ergebnis";
            this.DBGRID_Ergebnis.ReadOnly = true;
            this.DBGRID_Ergebnis.Size = new System.Drawing.Size(863, 602);
            this.DBGRID_Ergebnis.TabIndex = 10;
            // 
            // TXT_Link_Ergebnis
            // 
            this.TXT_Link_Ergebnis.Location = new System.Drawing.Point(85, 5);
            this.TXT_Link_Ergebnis.Name = "TXT_Link_Ergebnis";
            this.TXT_Link_Ergebnis.Size = new System.Drawing.Size(267, 22);
            this.TXT_Link_Ergebnis.TabIndex = 9;
            this.TXT_Link_Ergebnis.TextChanged += new System.EventHandler(this.TXT_Link_Changed);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(5, 5);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 22);
            this.label2.TabIndex = 8;
            this.label2.Text = "Link:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tabPage3
            // 
            this.tabPage3.BackColor = System.Drawing.Color.Silver;
            this.tabPage3.Controls.Add(this.BTN_NeueSuche_3);
            this.tabPage3.Controls.Add(this.BTN_Suchen_3);
            this.tabPage3.Controls.Add(this.DBGRID_Messwerte);
            this.tabPage3.Controls.Add(this.TXT_Link_Messwerte);
            this.tabPage3.Controls.Add(this.label3);
            this.tabPage3.Location = new System.Drawing.Point(4, 43);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(875, 645);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Messwerte";
            // 
            // BTN_NeueSuche_3
            // 
            this.BTN_NeueSuche_3.Caption = "";
            this.BTN_NeueSuche_3.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold);
            this.BTN_NeueSuche_3.Formular = null;
            this.BTN_NeueSuche_3.Location = new System.Drawing.Point(404, 4);
            this.BTN_NeueSuche_3.Name = "BTN_NeueSuche_3";
            this.BTN_NeueSuche_3.Picture_0 = Helper.BtnStyleSmall.btk_New;
            this.BTN_NeueSuche_3.Picture_1 = Helper.BtnStyleSmall.btk_Blanko;
            this.BTN_NeueSuche_3.Picture_2 = Helper.BtnStyleSmall.btk_Blanko;
            this.BTN_NeueSuche_3.Picture_3 = Helper.BtnStyleSmall.btk_Blanko;
            this.BTN_NeueSuche_3.Picture_4 = Helper.BtnStyleSmall.btk_Blanko;
            this.BTN_NeueSuche_3.PictureNumber = 0;
            this.BTN_NeueSuche_3.Size = new System.Drawing.Size(40, 24);
            this.BTN_NeueSuche_3.Symbol = null;
            this.BTN_NeueSuche_3.TabIndex = 14;
            this.BTN_NeueSuche_3.TabStop = false;
            this.BTN_NeueSuche_3.Visible = false;
            this.BTN_NeueSuche_3.Click += new System.EventHandler(this.BTN_NeueSuche_Click);
            // 
            // BTN_Suchen_3
            // 
            this.BTN_Suchen_3.Caption = "";
            this.BTN_Suchen_3.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold);
            this.BTN_Suchen_3.Formular = null;
            this.BTN_Suchen_3.Location = new System.Drawing.Point(358, 4);
            this.BTN_Suchen_3.Name = "BTN_Suchen_3";
            this.BTN_Suchen_3.Picture_0 = Helper.BtnStyleSmall.btk_Refresh;
            this.BTN_Suchen_3.Picture_1 = Helper.BtnStyleSmall.btk_Blanko;
            this.BTN_Suchen_3.Picture_2 = Helper.BtnStyleSmall.btk_Blanko;
            this.BTN_Suchen_3.Picture_3 = Helper.BtnStyleSmall.btk_Blanko;
            this.BTN_Suchen_3.Picture_4 = Helper.BtnStyleSmall.btk_Blanko;
            this.BTN_Suchen_3.PictureNumber = 0;
            this.BTN_Suchen_3.Size = new System.Drawing.Size(40, 24);
            this.BTN_Suchen_3.Symbol = null;
            this.BTN_Suchen_3.TabIndex = 13;
            this.BTN_Suchen_3.TabStop = false;
            this.BTN_Suchen_3.Click += new System.EventHandler(this.BTN_Suchen_Click);
            // 
            // DBGRID_Messwerte
            // 
            this.DBGRID_Messwerte.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DBGRID_Messwerte.Location = new System.Drawing.Point(5, 38);
            this.DBGRID_Messwerte.Name = "DBGRID_Messwerte";
            this.DBGRID_Messwerte.ReadOnly = true;
            this.DBGRID_Messwerte.Size = new System.Drawing.Size(863, 602);
            this.DBGRID_Messwerte.TabIndex = 12;
            // 
            // TXT_Link_Messwerte
            // 
            this.TXT_Link_Messwerte.Location = new System.Drawing.Point(85, 5);
            this.TXT_Link_Messwerte.Name = "TXT_Link_Messwerte";
            this.TXT_Link_Messwerte.Size = new System.Drawing.Size(267, 22);
            this.TXT_Link_Messwerte.TabIndex = 11;
            this.TXT_Link_Messwerte.TextChanged += new System.EventHandler(this.TXT_Link_Changed);
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(5, 5);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(76, 22);
            this.label3.TabIndex = 10;
            this.label3.Text = "Link:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // BTN_Suchen
            // 
            this.BTN_Suchen.Caption = "";
            this.BTN_Suchen.EnableMouseDown = false;
            this.BTN_Suchen.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold);
            this.BTN_Suchen.Formular = null;
            this.BTN_Suchen.Location = new System.Drawing.Point(10, 175);
            this.BTN_Suchen.Name = "BTN_Suchen";
            this.BTN_Suchen.Picture_0 = Helper.BtnStyle.btg_Requery;
            this.BTN_Suchen.Picture_1 = Helper.BtnStyle.btg_New;
            this.BTN_Suchen.Picture_2 = Helper.BtnStyle.btg_Blanko;
            this.BTN_Suchen.Picture_3 = Helper.BtnStyle.btg_Blanko;
            this.BTN_Suchen.Picture_4 = Helper.BtnStyle.btg_Blanko;
            this.BTN_Suchen.PictureNumber = 0;
            this.BTN_Suchen.Size = new System.Drawing.Size(79, 48);
            this.BTN_Suchen.Symbol = null;
            this.BTN_Suchen.TabIndex = 3;
            this.BTN_Suchen.TabStop = false;
            this.BTN_Suchen.Click += new System.EventHandler(this.BTN_Suchen_Click);
            // 
            // BTN_Excel
            // 
            this.BTN_Excel.Caption = "";
            this.BTN_Excel.EnableMouseDown = false;
            this.BTN_Excel.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold);
            this.BTN_Excel.Formular = null;
            this.BTN_Excel.Location = new System.Drawing.Point(10, 227);
            this.BTN_Excel.Name = "BTN_Excel";
            this.BTN_Excel.Picture_0 = Helper.BtnStyle.btg_Excel;
            this.BTN_Excel.Picture_1 = Helper.BtnStyle.btg_New;
            this.BTN_Excel.Picture_2 = Helper.BtnStyle.btg_Blanko;
            this.BTN_Excel.Picture_3 = Helper.BtnStyle.btg_Blanko;
            this.BTN_Excel.Picture_4 = Helper.BtnStyle.btg_Blanko;
            this.BTN_Excel.PictureNumber = 0;
            this.BTN_Excel.Size = new System.Drawing.Size(79, 48);
            this.BTN_Excel.Symbol = null;
            this.BTN_Excel.TabIndex = 4;
            this.BTN_Excel.TabStop = false;
            this.BTN_Excel.Visible = false;
            this.BTN_Excel.Click += new System.EventHandler(this.BTN_Excel_Click);
            // 
            // BTN_NeueSuche
            // 
            this.BTN_NeueSuche.Caption = "";
            this.BTN_NeueSuche.EnableMouseDown = false;
            this.BTN_NeueSuche.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold);
            this.BTN_NeueSuche.Formular = null;
            this.BTN_NeueSuche.Location = new System.Drawing.Point(10, 279);
            this.BTN_NeueSuche.Name = "BTN_NeueSuche";
            this.BTN_NeueSuche.Picture_0 = Helper.BtnStyle.btg_New;
            this.BTN_NeueSuche.Picture_1 = Helper.BtnStyle.btg_Blanko;
            this.BTN_NeueSuche.Picture_2 = Helper.BtnStyle.btg_Blanko;
            this.BTN_NeueSuche.Picture_3 = Helper.BtnStyle.btg_Blanko;
            this.BTN_NeueSuche.Picture_4 = Helper.BtnStyle.btg_Blanko;
            this.BTN_NeueSuche.PictureNumber = 0;
            this.BTN_NeueSuche.Size = new System.Drawing.Size(79, 48);
            this.BTN_NeueSuche.Symbol = null;
            this.BTN_NeueSuche.TabIndex = 5;
            this.BTN_NeueSuche.TabStop = false;
            this.BTN_NeueSuche.Visible = false;
            this.BTN_NeueSuche.Click += new System.EventHandler(this.BTN_NeueSuche_Click);
            // 
            // BTN_OpenFolderDocumente
            // 
            this.BTN_OpenFolderDocumente.Caption = "";
            this.BTN_OpenFolderDocumente.EnableMouseDown = false;
            this.BTN_OpenFolderDocumente.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold);
            this.BTN_OpenFolderDocumente.Formular = null;
            this.BTN_OpenFolderDocumente.Location = new System.Drawing.Point(10, 331);
            this.BTN_OpenFolderDocumente.Name = "BTN_OpenFolderDocumente";
            this.BTN_OpenFolderDocumente.Picture_0 = Helper.BtnStyle.btg_Folder;
            this.BTN_OpenFolderDocumente.Picture_1 = Helper.BtnStyle.btg_Blanko;
            this.BTN_OpenFolderDocumente.Picture_2 = Helper.BtnStyle.btg_Blanko;
            this.BTN_OpenFolderDocumente.Picture_3 = Helper.BtnStyle.btg_Blanko;
            this.BTN_OpenFolderDocumente.Picture_4 = Helper.BtnStyle.btg_Blanko;
            this.BTN_OpenFolderDocumente.PictureNumber = 0;
            this.BTN_OpenFolderDocumente.Size = new System.Drawing.Size(79, 48);
            this.BTN_OpenFolderDocumente.Symbol = null;
            this.BTN_OpenFolderDocumente.TabIndex = 6;
            this.BTN_OpenFolderDocumente.TabStop = false;
            this.BTN_OpenFolderDocumente.Click += new System.EventHandler(this.BTN_OpenFolderDocumente_Click);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.toolStripMenuItem2,
            this.suchenToolStripMenuItem,
            this.oeffnenToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(121, 92);
            this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Enabled = false;
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(120, 22);
            this.toolStripMenuItem1.Text = "Export";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(120, 22);
            this.toolStripMenuItem2.Text = "Beenden";
            this.toolStripMenuItem2.Click += new System.EventHandler(this.toolStripMenuItem2_Click);
            // 
            // suchenToolStripMenuItem
            // 
            this.suchenToolStripMenuItem.Name = "suchenToolStripMenuItem";
            this.suchenToolStripMenuItem.Size = new System.Drawing.Size(120, 22);
            this.suchenToolStripMenuItem.Text = "Suchen";
            this.suchenToolStripMenuItem.Click += new System.EventHandler(this.suchenToolStripMenuItem_Click);
            // 
            // oeffnenToolStripMenuItem
            // 
            this.oeffnenToolStripMenuItem.Name = "oeffnenToolStripMenuItem";
            this.oeffnenToolStripMenuItem.Size = new System.Drawing.Size(120, 22);
            this.oeffnenToolStripMenuItem.Text = "Oeffnen";
            this.oeffnenToolStripMenuItem.Click += new System.EventHandler(this.oeffnenToolStripMenuItem_Click);
            // 
            // bitButton1
            // 
            this.bitButton1.Caption = "Kill";
            this.bitButton1.EnableMouseDown = false;
            this.bitButton1.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold);
            this.bitButton1.Formular = null;
            this.bitButton1.Location = new System.Drawing.Point(10, 386);
            this.bitButton1.Name = "bitButton1";
            this.bitButton1.Picture_0 = Helper.BtnStyle.btg_Excel;
            this.bitButton1.Picture_1 = Helper.BtnStyle.btg_Blanko;
            this.bitButton1.Picture_2 = Helper.BtnStyle.btg_Blanko;
            this.bitButton1.Picture_3 = Helper.BtnStyle.btg_Blanko;
            this.bitButton1.Picture_4 = Helper.BtnStyle.btg_Blanko;
            this.bitButton1.PictureNumber = 0;
            this.bitButton1.Size = new System.Drawing.Size(79, 48);
            this.bitButton1.Symbol = null;
            this.bitButton1.TabIndex = 7;
            this.bitButton1.TabStop = false;
            this.bitButton1.Click += new System.EventHandler(this.bitButton1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 730);
            this.ContextMenuStrip = this.contextMenuStrip1;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "KalibHelper 2.00";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Controls.SetChildIndex(this.GB_Menu, 0);
            this.Controls.SetChildIndex(this.GB_Datenausgabe, 0);
            ((System.ComponentModel.ISupportInitialize)(this.BTN_Close)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BTN_Keyboard)).EndInit();
            this.GB_Menu.ResumeLayout(false);
            this.GB_Datenausgabe.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.BTN_Menu)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BTN_NeueSuche_1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BTN_Suchen_1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DBGRID_Eingabe)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BTN_NeueSuche_2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BTN_Suchen_2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DBGRID_Ergebnis)).EndInit();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BTN_NeueSuche_3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BTN_Suchen_3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DBGRID_Messwerte)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BTN_Suchen)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BTN_Excel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BTN_NeueSuche)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BTN_OpenFolderDocumente)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.bitButton1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.DataGridView DBGRID_Eingabe;
        private System.Windows.Forms.TextBox TXT_Link_Eingabe;
        private System.Windows.Forms.Label label1;
        private Helper.BitButton BTN_Suchen;
        private Helper.BitButton BTN_Excel;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.DataGridView DBGRID_Ergebnis;
        private System.Windows.Forms.TextBox TXT_Link_Ergebnis;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.DataGridView DBGRID_Messwerte;
        private System.Windows.Forms.TextBox TXT_Link_Messwerte;
        private System.Windows.Forms.Label label3;
        private Helper.BitButton BTN_NeueSuche;
        private Helper.BitButton BTN_OpenFolderDocumente;
        private Helper.BitButtonSmall BTN_Suchen_1;
        private Helper.BitButtonSmall BTN_Suchen_2;
        private Helper.BitButtonSmall BTN_Suchen_3;
        private Helper.BitButtonSmall BTN_NeueSuche_1;
        private Helper.BitButtonSmall BTN_NeueSuche_2;
        private Helper.BitButtonSmall BTN_NeueSuche_3;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem suchenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem oeffnenToolStripMenuItem;
        private Helper.BitButton bitButton1;

    }
}

