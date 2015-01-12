namespace PrjDandEReport
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMainScreen));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rotaBitButton1 = new Helper.RotaBitButton();
            this.txtFilter06 = new System.Windows.Forms.TextBox();
            this.CbxSpalte06 = new System.Windows.Forms.ComboBox();
            this.CbxTabelle06 = new System.Windows.Forms.ComboBox();
            this.txtFilter05 = new System.Windows.Forms.TextBox();
            this.CbxSpalte05 = new System.Windows.Forms.ComboBox();
            this.CbxTabelle05 = new System.Windows.Forms.ComboBox();
            this.txtFilter04 = new System.Windows.Forms.TextBox();
            this.CbxSpalte04 = new System.Windows.Forms.ComboBox();
            this.CbxTabelle04 = new System.Windows.Forms.ComboBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.cbxAndOr02 = new System.Windows.Forms.ComboBox();
            this.cbxAndOr01 = new System.Windows.Forms.ComboBox();
            this.txtFilter03 = new System.Windows.Forms.TextBox();
            this.txtFilter02 = new System.Windows.Forms.TextBox();
            this.txtFilter01 = new System.Windows.Forms.TextBox();
            this.CbxSpalte03 = new System.Windows.Forms.ComboBox();
            this.CbxSpalte02 = new System.Windows.Forms.ComboBox();
            this.CbxTabelle03 = new System.Windows.Forms.ComboBox();
            this.CbxTabelle02 = new System.Windows.Forms.ComboBox();
            this.DATEPICKER_Bis = new System.Windows.Forms.DateTimePicker();
            this.DATEPICKER_Von = new System.Windows.Forms.DateTimePicker();
            this.CbxSpalte01 = new System.Windows.Forms.ComboBox();
            this.CbxTabelle01 = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.DBGRID_Auswahl = new System.Windows.Forms.DataGridView();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.btnStartQuery = new Helper.BitButton();
            this.bitButton1 = new Helper.BitButton();
            this.Btn_OpenFolderDocuments = new Helper.RotaBitButton();
            ((System.ComponentModel.ISupportInitialize)(this.BTN_Close)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BTN_Keyboard)).BeginInit();
            this.GB_Menu.SuspendLayout();
            this.GB_Datenausgabe.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BTN_Menu)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rotaBitButton1)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DBGRID_Auswahl)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnStartQuery)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bitButton1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Btn_OpenFolderDocuments)).BeginInit();
            this.SuspendLayout();
            // 
            // GB_Menu
            // 
            this.GB_Menu.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
            this.GB_Menu.Controls.Add(this.Btn_OpenFolderDocuments);
            this.GB_Menu.Controls.Add(this.bitButton1);
            this.GB_Menu.Controls.Add(this.btnStartQuery);
            this.GB_Menu.Location = new System.Drawing.Point(1479, 5);
            this.GB_Menu.Size = new System.Drawing.Size(99, 853);
            this.GB_Menu.Controls.SetChildIndex(this.BTN_Close, 0);
            this.GB_Menu.Controls.SetChildIndex(this.BTN_Menu, 0);
            this.GB_Menu.Controls.SetChildIndex(this.BTN_Keyboard, 0);
            this.GB_Menu.Controls.SetChildIndex(this.btnStartQuery, 0);
            this.GB_Menu.Controls.SetChildIndex(this.bitButton1, 0);
            this.GB_Menu.Controls.SetChildIndex(this.Btn_OpenFolderDocuments, 0);
            // 
            // GB_Datenausgabe
            // 
            this.GB_Datenausgabe.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
            this.GB_Datenausgabe.Controls.Add(this.tabControl1);
            this.GB_Datenausgabe.Size = new System.Drawing.Size(1468, 853);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.ItemSize = new System.Drawing.Size(72, 39);
            this.tabControl1.Location = new System.Drawing.Point(7, 21);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1453, 824);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.Silver;
            this.tabPage1.Controls.Add(this.groupBox2);
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Location = new System.Drawing.Point(4, 43);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1445, 777);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Daten";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rotaBitButton1);
            this.groupBox2.Controls.Add(this.txtFilter06);
            this.groupBox2.Controls.Add(this.CbxSpalte06);
            this.groupBox2.Controls.Add(this.CbxTabelle06);
            this.groupBox2.Controls.Add(this.txtFilter05);
            this.groupBox2.Controls.Add(this.CbxSpalte05);
            this.groupBox2.Controls.Add(this.CbxTabelle05);
            this.groupBox2.Controls.Add(this.txtFilter04);
            this.groupBox2.Controls.Add(this.CbxSpalte04);
            this.groupBox2.Controls.Add(this.CbxTabelle04);
            this.groupBox2.Controls.Add(this.checkBox2);
            this.groupBox2.Controls.Add(this.checkBox1);
            this.groupBox2.Controls.Add(this.cbxAndOr02);
            this.groupBox2.Controls.Add(this.cbxAndOr01);
            this.groupBox2.Controls.Add(this.txtFilter03);
            this.groupBox2.Controls.Add(this.txtFilter02);
            this.groupBox2.Controls.Add(this.txtFilter01);
            this.groupBox2.Controls.Add(this.CbxSpalte03);
            this.groupBox2.Controls.Add(this.CbxSpalte02);
            this.groupBox2.Controls.Add(this.CbxTabelle03);
            this.groupBox2.Controls.Add(this.CbxTabelle02);
            this.groupBox2.Controls.Add(this.DATEPICKER_Bis);
            this.groupBox2.Controls.Add(this.DATEPICKER_Von);
            this.groupBox2.Controls.Add(this.CbxSpalte01);
            this.groupBox2.Controls.Add(this.CbxTabelle01);
            this.groupBox2.Location = new System.Drawing.Point(6, 7);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1433, 227);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Filter";
            // 
            // rotaBitButton1
            // 
            this.rotaBitButton1.Caption = "";
            this.rotaBitButton1.EnableMouseDown = false;
            this.rotaBitButton1.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold);
            this.rotaBitButton1.Formular = null;
            this.rotaBitButton1.Location = new System.Drawing.Point(805, 125);
            this.rotaBitButton1.Name = "rotaBitButton1";
            this.rotaBitButton1.Picture_0 = Helper.BtnStyle.btg_Blanko;
            this.rotaBitButton1.Picture_1 = Helper.BtnStyle.btg_Blanko;
            this.rotaBitButton1.Picture_2 = Helper.BtnStyle.btg_Blanko;
            this.rotaBitButton1.Picture_3 = Helper.BtnStyle.btg_Blanko;
            this.rotaBitButton1.Picture_4 = Helper.BtnStyle.btg_Blanko;
            this.rotaBitButton1.PictureNumber = 0;
            this.rotaBitButton1.Size = new System.Drawing.Size(79, 48);
            this.rotaBitButton1.Symbol = null;
            this.rotaBitButton1.TabIndex = 35;
            this.rotaBitButton1.TabStop = false;
            this.rotaBitButton1.Click += new System.EventHandler(this.rotaBitButton1_Click);
            // 
            // txtFilter06
            // 
            this.txtFilter06.Location = new System.Drawing.Point(471, 194);
            this.txtFilter06.Name = "txtFilter06";
            this.txtFilter06.Size = new System.Drawing.Size(213, 22);
            this.txtFilter06.TabIndex = 34;
            // 
            // CbxSpalte06
            // 
            this.CbxSpalte06.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CbxSpalte06.FormattingEnabled = true;
            this.CbxSpalte06.Location = new System.Drawing.Point(471, 166);
            this.CbxSpalte06.Name = "CbxSpalte06";
            this.CbxSpalte06.Size = new System.Drawing.Size(213, 23);
            this.CbxSpalte06.TabIndex = 33;
            // 
            // CbxTabelle06
            // 
            this.CbxTabelle06.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CbxTabelle06.FormattingEnabled = true;
            this.CbxTabelle06.Location = new System.Drawing.Point(471, 138);
            this.CbxTabelle06.Name = "CbxTabelle06";
            this.CbxTabelle06.Size = new System.Drawing.Size(213, 23);
            this.CbxTabelle06.TabIndex = 32;
            // 
            // txtFilter05
            // 
            this.txtFilter05.Location = new System.Drawing.Point(239, 194);
            this.txtFilter05.Name = "txtFilter05";
            this.txtFilter05.Size = new System.Drawing.Size(213, 22);
            this.txtFilter05.TabIndex = 31;
            // 
            // CbxSpalte05
            // 
            this.CbxSpalte05.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CbxSpalte05.FormattingEnabled = true;
            this.CbxSpalte05.Location = new System.Drawing.Point(239, 166);
            this.CbxSpalte05.Name = "CbxSpalte05";
            this.CbxSpalte05.Size = new System.Drawing.Size(213, 23);
            this.CbxSpalte05.TabIndex = 30;
            // 
            // CbxTabelle05
            // 
            this.CbxTabelle05.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CbxTabelle05.FormattingEnabled = true;
            this.CbxTabelle05.Location = new System.Drawing.Point(239, 138);
            this.CbxTabelle05.Name = "CbxTabelle05";
            this.CbxTabelle05.Size = new System.Drawing.Size(213, 23);
            this.CbxTabelle05.TabIndex = 29;
            // 
            // txtFilter04
            // 
            this.txtFilter04.Location = new System.Drawing.Point(7, 194);
            this.txtFilter04.Name = "txtFilter04";
            this.txtFilter04.Size = new System.Drawing.Size(213, 22);
            this.txtFilter04.TabIndex = 28;
            // 
            // CbxSpalte04
            // 
            this.CbxSpalte04.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CbxSpalte04.FormattingEnabled = true;
            this.CbxSpalte04.Location = new System.Drawing.Point(7, 166);
            this.CbxSpalte04.Name = "CbxSpalte04";
            this.CbxSpalte04.Size = new System.Drawing.Size(213, 23);
            this.CbxSpalte04.TabIndex = 27;
            // 
            // CbxTabelle04
            // 
            this.CbxTabelle04.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CbxTabelle04.FormattingEnabled = true;
            this.CbxTabelle04.Location = new System.Drawing.Point(7, 138);
            this.CbxTabelle04.Name = "CbxTabelle04";
            this.CbxTabelle04.Size = new System.Drawing.Size(213, 23);
            this.CbxTabelle04.TabIndex = 26;
            // 
            // checkBox2
            // 
            this.checkBox2.Location = new System.Drawing.Point(732, 78);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(55, 26);
            this.checkBox2.TabIndex = 25;
            this.checkBox2.Text = "bis:";
            this.checkBox2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.checkBox2.UseVisualStyleBackColor = true;
            // 
            // checkBox1
            // 
            this.checkBox1.Location = new System.Drawing.Point(732, 21);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(55, 26);
            this.checkBox1.TabIndex = 24;
            this.checkBox1.Text = "von:";
            this.checkBox1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // cbxAndOr02
            // 
            this.cbxAndOr02.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxAndOr02.FormattingEnabled = true;
            this.cbxAndOr02.Items.AddRange(new object[] {
            "AND",
            "OR"});
            this.cbxAndOr02.Location = new System.Drawing.Point(433, 109);
            this.cbxAndOr02.Name = "cbxAndOr02";
            this.cbxAndOr02.Size = new System.Drawing.Size(69, 23);
            this.cbxAndOr02.TabIndex = 23;
            // 
            // cbxAndOr01
            // 
            this.cbxAndOr01.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxAndOr01.FormattingEnabled = true;
            this.cbxAndOr01.Items.AddRange(new object[] {
            "AND",
            "OR"});
            this.cbxAndOr01.Location = new System.Drawing.Point(199, 109);
            this.cbxAndOr01.Name = "cbxAndOr01";
            this.cbxAndOr01.Size = new System.Drawing.Size(69, 23);
            this.cbxAndOr01.TabIndex = 22;
            // 
            // txtFilter03
            // 
            this.txtFilter03.Location = new System.Drawing.Point(471, 80);
            this.txtFilter03.Name = "txtFilter03";
            this.txtFilter03.Size = new System.Drawing.Size(213, 22);
            this.txtFilter03.TabIndex = 21;
            // 
            // txtFilter02
            // 
            this.txtFilter02.Location = new System.Drawing.Point(239, 80);
            this.txtFilter02.Name = "txtFilter02";
            this.txtFilter02.Size = new System.Drawing.Size(213, 22);
            this.txtFilter02.TabIndex = 20;
            // 
            // txtFilter01
            // 
            this.txtFilter01.Location = new System.Drawing.Point(7, 79);
            this.txtFilter01.Name = "txtFilter01";
            this.txtFilter01.Size = new System.Drawing.Size(213, 22);
            this.txtFilter01.TabIndex = 19;
            // 
            // CbxSpalte03
            // 
            this.CbxSpalte03.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CbxSpalte03.FormattingEnabled = true;
            this.CbxSpalte03.Location = new System.Drawing.Point(471, 51);
            this.CbxSpalte03.Name = "CbxSpalte03";
            this.CbxSpalte03.Size = new System.Drawing.Size(213, 23);
            this.CbxSpalte03.TabIndex = 18;
            // 
            // CbxSpalte02
            // 
            this.CbxSpalte02.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CbxSpalte02.FormattingEnabled = true;
            this.CbxSpalte02.Location = new System.Drawing.Point(239, 51);
            this.CbxSpalte02.Name = "CbxSpalte02";
            this.CbxSpalte02.Size = new System.Drawing.Size(213, 23);
            this.CbxSpalte02.TabIndex = 17;
            // 
            // CbxTabelle03
            // 
            this.CbxTabelle03.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CbxTabelle03.FormattingEnabled = true;
            this.CbxTabelle03.Location = new System.Drawing.Point(471, 23);
            this.CbxTabelle03.Name = "CbxTabelle03";
            this.CbxTabelle03.Size = new System.Drawing.Size(213, 23);
            this.CbxTabelle03.TabIndex = 16;
            // 
            // CbxTabelle02
            // 
            this.CbxTabelle02.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CbxTabelle02.FormattingEnabled = true;
            this.CbxTabelle02.Location = new System.Drawing.Point(239, 23);
            this.CbxTabelle02.Name = "CbxTabelle02";
            this.CbxTabelle02.Size = new System.Drawing.Size(213, 23);
            this.CbxTabelle02.TabIndex = 15;
            // 
            // DATEPICKER_Bis
            // 
            this.DATEPICKER_Bis.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DATEPICKER_Bis.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.DATEPICKER_Bis.Location = new System.Drawing.Point(805, 78);
            this.DATEPICKER_Bis.Name = "DATEPICKER_Bis";
            this.DATEPICKER_Bis.Size = new System.Drawing.Size(120, 26);
            this.DATEPICKER_Bis.TabIndex = 14;
            // 
            // DATEPICKER_Von
            // 
            this.DATEPICKER_Von.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DATEPICKER_Von.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.DATEPICKER_Von.Location = new System.Drawing.Point(805, 21);
            this.DATEPICKER_Von.Name = "DATEPICKER_Von";
            this.DATEPICKER_Von.Size = new System.Drawing.Size(120, 26);
            this.DATEPICKER_Von.TabIndex = 13;
            // 
            // CbxSpalte01
            // 
            this.CbxSpalte01.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CbxSpalte01.FormattingEnabled = true;
            this.CbxSpalte01.Location = new System.Drawing.Point(7, 51);
            this.CbxSpalte01.Name = "CbxSpalte01";
            this.CbxSpalte01.Size = new System.Drawing.Size(213, 23);
            this.CbxSpalte01.TabIndex = 12;
            // 
            // CbxTabelle01
            // 
            this.CbxTabelle01.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CbxTabelle01.FormattingEnabled = true;
            this.CbxTabelle01.Location = new System.Drawing.Point(7, 23);
            this.CbxTabelle01.Name = "CbxTabelle01";
            this.CbxTabelle01.Size = new System.Drawing.Size(213, 23);
            this.CbxTabelle01.TabIndex = 11;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.DBGRID_Auswahl);
            this.groupBox1.Location = new System.Drawing.Point(6, 240);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1433, 531);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Auswahl";
            // 
            // DBGRID_Auswahl
            // 
            this.DBGRID_Auswahl.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DBGRID_Auswahl.Location = new System.Drawing.Point(7, 20);
            this.DBGRID_Auswahl.Name = "DBGRID_Auswahl";
            this.DBGRID_Auswahl.ReadOnly = true;
            this.DBGRID_Auswahl.Size = new System.Drawing.Size(1418, 504);
            this.DBGRID_Auswahl.TabIndex = 1;
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.Color.Silver;
            this.tabPage2.Controls.Add(this.textBox2);
            this.tabPage2.Controls.Add(this.textBox1);
            this.tabPage2.Controls.Add(this.richTextBox1);
            this.tabPage2.Location = new System.Drawing.Point(4, 43);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1445, 777);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Konfiguration";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(6, 294);
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(1433, 343);
            this.textBox2.TabIndex = 8;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(6, 657);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(1433, 114);
            this.textBox1.TabIndex = 7;
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(6, 7);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(1194, 270);
            this.richTextBox1.TabIndex = 4;
            this.richTextBox1.Text = "\n";
            // 
            // btnStartQuery
            // 
            this.btnStartQuery.Caption = "";
            this.btnStartQuery.EnableMouseDown = false;
            this.btnStartQuery.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold);
            this.btnStartQuery.Formular = null;
            this.btnStartQuery.Location = new System.Drawing.Point(10, 178);
            this.btnStartQuery.Name = "btnStartQuery";
            this.btnStartQuery.Picture_0 = Helper.BtnStyle.btg_Requery;
            this.btnStartQuery.Picture_1 = Helper.BtnStyle.btg_Blanko;
            this.btnStartQuery.Picture_2 = Helper.BtnStyle.btg_Blanko;
            this.btnStartQuery.Picture_3 = Helper.BtnStyle.btg_Blanko;
            this.btnStartQuery.Picture_4 = Helper.BtnStyle.btg_Blanko;
            this.btnStartQuery.PictureNumber = 0;
            this.btnStartQuery.Size = new System.Drawing.Size(79, 48);
            this.btnStartQuery.Symbol = null;
            this.btnStartQuery.TabIndex = 3;
            this.btnStartQuery.TabStop = false;
            this.btnStartQuery.Click += new System.EventHandler(this.btnStartQuery_Click);
            // 
            // bitButton1
            // 
            this.bitButton1.Caption = "";
            this.bitButton1.EnableMouseDown = false;
            this.bitButton1.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold);
            this.bitButton1.Formular = null;
            this.bitButton1.Location = new System.Drawing.Point(10, 233);
            this.bitButton1.Name = "bitButton1";
            this.bitButton1.Picture_0 = Helper.BtnStyle.btg_Diskette;
            this.bitButton1.Picture_1 = Helper.BtnStyle.btg_Blanko;
            this.bitButton1.Picture_2 = Helper.BtnStyle.btg_Blanko;
            this.bitButton1.Picture_3 = Helper.BtnStyle.btg_Blanko;
            this.bitButton1.Picture_4 = Helper.BtnStyle.btg_Blanko;
            this.bitButton1.PictureNumber = 0;
            this.bitButton1.Size = new System.Drawing.Size(79, 48);
            this.bitButton1.Symbol = null;
            this.bitButton1.TabIndex = 6;
            this.bitButton1.TabStop = false;
            this.bitButton1.Click += new System.EventHandler(this.bitButton1_Click);
            // 
            // Btn_OpenFolderDocuments
            // 
            this.Btn_OpenFolderDocuments.Caption = "";
            this.Btn_OpenFolderDocuments.EnableMouseDown = false;
            this.Btn_OpenFolderDocuments.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold);
            this.Btn_OpenFolderDocuments.Formular = null;
            this.Btn_OpenFolderDocuments.Location = new System.Drawing.Point(10, 288);
            this.Btn_OpenFolderDocuments.Name = "Btn_OpenFolderDocuments";
            this.Btn_OpenFolderDocuments.Picture_0 = Helper.BtnStyle.btg_Folder;
            this.Btn_OpenFolderDocuments.Picture_1 = Helper.BtnStyle.btg_Blanko;
            this.Btn_OpenFolderDocuments.Picture_2 = Helper.BtnStyle.btg_Blanko;
            this.Btn_OpenFolderDocuments.Picture_3 = Helper.BtnStyle.btg_Blanko;
            this.Btn_OpenFolderDocuments.Picture_4 = Helper.BtnStyle.btg_Blanko;
            this.Btn_OpenFolderDocuments.PictureNumber = 0;
            this.Btn_OpenFolderDocuments.Size = new System.Drawing.Size(79, 48);
            this.Btn_OpenFolderDocuments.Symbol = null;
            this.Btn_OpenFolderDocuments.TabIndex = 7;
            this.Btn_OpenFolderDocuments.TabStop = false;
            this.Btn_OpenFolderDocuments.Click += new System.EventHandler(this.Btn_OpenFolderDocuments_Click);
            // 
            // FrmMainScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1584, 862);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "FrmMainScreen";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "D&E Helper";
            this.Load += new System.EventHandler(this.FrmHauptmenu_Load);
            ((System.ComponentModel.ISupportInitialize)(this.BTN_Close)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BTN_Keyboard)).EndInit();
            this.GB_Menu.ResumeLayout(false);
            this.GB_Datenausgabe.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.BTN_Menu)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rotaBitButton1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DBGRID_Auswahl)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnStartQuery)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bitButton1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Btn_OpenFolderDocuments)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView DBGRID_Auswahl;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DateTimePicker DATEPICKER_Bis;
        private System.Windows.Forms.DateTimePicker DATEPICKER_Von;
        private System.Windows.Forms.ComboBox CbxSpalte01;
        private System.Windows.Forms.ComboBox CbxTabelle01;
        private System.Windows.Forms.ComboBox CbxTabelle03;
        private System.Windows.Forms.ComboBox CbxTabelle02;
        private System.Windows.Forms.ComboBox CbxSpalte03;
        private System.Windows.Forms.ComboBox CbxSpalte02;
        private System.Windows.Forms.TextBox txtFilter03;
        private System.Windows.Forms.TextBox txtFilter02;
        private System.Windows.Forms.TextBox txtFilter01;
        private System.Windows.Forms.ComboBox cbxAndOr01;
        private System.Windows.Forms.ComboBox cbxAndOr02;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.TextBox txtFilter06;
        private System.Windows.Forms.ComboBox CbxSpalte06;
        private System.Windows.Forms.ComboBox CbxTabelle06;
        private System.Windows.Forms.TextBox txtFilter05;
        private System.Windows.Forms.ComboBox CbxSpalte05;
        private System.Windows.Forms.ComboBox CbxTabelle05;
        private System.Windows.Forms.TextBox txtFilter04;
        private System.Windows.Forms.ComboBox CbxSpalte04;
        private System.Windows.Forms.ComboBox CbxTabelle04;
        private Helper.BitButton btnStartQuery;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.TextBox textBox1;
        private Helper.RotaBitButton rotaBitButton1;
        private Helper.BitButton bitButton1;
        private System.Windows.Forms.TextBox textBox2;
        private Helper.RotaBitButton Btn_OpenFolderDocuments;
    }
}