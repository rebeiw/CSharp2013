using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Helper;
using System.Collections;
using System.Data.SqlClient;
using System.Reflection;
using Excel = Microsoft.Office.Interop.Excel;


namespace PrjDandEReport
{
    public struct TableStruct
    {
        public string Table;
        public string Column;
        public string Display;
        public string Columntype;
    }

    public partial class FrmMainScreen : FrmVorlageMenu
    {
        private Hashtable m_Datas;
        private Hashtable m_TableInfo;
        private SQLHelper m_SqlHelper;
        private SQLHelper m_SqlHelperInnerJoin;
        private SQLHelper m_SqlHelperWhere;
        private SQLHelper m_SqlHelperFrom;

        private BindingSource m_BS_Auswahl;

        private List<string[]> m_Columnlist;
        private List<string[]> m_ColumnlistOutput;
        private List<string> m_Tablelist;
        private bool m_CanChange;

        private ClsIni m_ClsIni;

        private string m_ConnectionString =
            "Data Source=H4T9FV1;" +
            "Initial Catalog=Kalibwin;" +
            "Integrated Security=True";

        public FrmMainScreen()
        {


            InitializeComponent();

            this.m_ClsIni = new ClsIni("cfg.ini");
            this.m_Columnlist = new List<string[]>();
            this.m_ColumnlistOutput = new List<string[]>();
            this.m_Tablelist = new List<string>();
            this.ShowClose = true;
            this.m_CanChange = false;
            this.m_SqlHelper = new SQLHelper(m_ConnectionString);
            this.m_SqlHelperWhere = new SQLHelper(m_ConnectionString);
            this.m_SqlHelperFrom = new SQLHelper(m_ConnectionString);
            this.m_SqlHelperInnerJoin = new SQLHelper(m_ConnectionString);
            this.m_Datas = new Hashtable();
            this.m_TableInfo = new Hashtable();
            this.m_BS_Auswahl = new BindingSource();

            this.DBGRID_Auswahl.DataSource = this.m_BS_Auswahl;


            FuncGeneral.Start();
        }

        private int BindData(string selectCommand, BindingSource BindingSource)
        {
            int retval = 0;
            try
            {
                SqlDataAdapter dataAdapter;
                SqlCommandBuilder commandBuilder;
                DataTable table;
                DataSet ds;
                dataAdapter = new SqlDataAdapter(selectCommand, this.m_ConnectionString);
                commandBuilder = new SqlCommandBuilder(dataAdapter);
                table = new DataTable();
                ds = new DataSet();
                table.Locale = System.Globalization.CultureInfo.InvariantCulture;
                dataAdapter.Fill(table);
                BindingSource.DataSource = table;
                retval = table.Rows.Count;
                commandBuilder.Dispose();
                dataAdapter.Dispose();
                table.Dispose();
                GC.Collect();
            }
            catch (SqlException)
            {
                MessageBox.Show("To run this example, replace the value of the connectionString variable with a connection string that is valid for your system.");
            }
            return retval;
        }


        private void FrmHauptmenu_Load(object sender, EventArgs e)
        {
            this.m_CanChange = false;
            richTextBox1.Clear();
            richTextBox1.LoadFile("Spalten.cfg", RichTextBoxStreamType.PlainText);
            this.CreateAll();
            this.m_CanChange = true;
        }
        private void CreateAll()
        {
            this.m_Datas.Clear();
            this.CreateTableColumnList();
            string cbx_name = "";

            for (int i = 1; i <= 6; i++)
            {
                cbx_name = "CbxTabelle" + FuncString.FillForward(i.ToString(), "0", 2);
                ComboBox obj_cbx_tabelle = (ComboBox)FuncGeneral.GetControlByName(this, cbx_name);
                obj_cbx_tabelle.Items.Clear();
                obj_cbx_tabelle.Items.AddRange(this.m_Tablelist.ToArray());
                obj_cbx_tabelle.SelectedIndexChanged += new System.EventHandler(this.CbxTable_SelectedIndexChanged);
                obj_cbx_tabelle.SelectedIndex = obj_cbx_tabelle.Items.IndexOf(this.m_ClsIni.getValue("AuswahlTabelle", cbx_name));

                cbx_name = "CbxSpalte" + FuncString.FillForward(i.ToString(), "0", 2);
                ComboBox obj_cbx_column = (ComboBox)FuncGeneral.GetControlByName(this, cbx_name);
                obj_cbx_column.SelectedIndex = obj_cbx_column.Items.IndexOf(this.m_ClsIni.getValue("AuswahlTabelle", cbx_name));
                obj_cbx_column.SelectedIndexChanged += new System.EventHandler(this.CbxColumn_SelectedIndexChanged);

                cbx_name = "txtFilter" + FuncString.FillForward(i.ToString(), "0", 2);
                TextBox obj_txt_filter = (TextBox)FuncGeneral.GetControlByName(this, cbx_name);
                obj_txt_filter.Text = this.m_ClsIni.getValue("AuswahlTabelle", cbx_name);
                obj_txt_filter.TextChanged += new System.EventHandler(this.txtFilter_TextChanged);
            }

            for (int i = 1; i <= 2; i++)
            {
                cbx_name = "cbxAndOr" + FuncString.FillForward(i.ToString(), "0", 2);
                ComboBox obj_cbx_column = (ComboBox)FuncGeneral.GetControlByName(this, cbx_name);
                obj_cbx_column.SelectedIndex = obj_cbx_column.Items.IndexOf(this.m_ClsIni.getValue("AuswahlTabelle", cbx_name));
                obj_cbx_column.SelectedIndexChanged += new System.EventHandler(this.cbxAndOr_SelectedIndexChanged);
            }
        }
        
        private void FillTableInfo(string Table)
        {
            //Datentyp
            List<string> table_information = this.m_SqlHelper.SQL2StringList("SELECT TABLE_NAME,COLUMN_NAME,DATA_TYPE FROM INFORMATION_SCHEMA.COLUMNS where TABLE_NAME='" + Table + "'");
            foreach (string table_info in table_information)
            {
                string[] infos = table_info.Split('\t');
                string key = infos[0] + infos[1];
                this.m_TableInfo.Add(key,infos[2]);
            }
        }

        private void CreateTableColumnList()
        {
            this.m_TableInfo.Clear();
            string table_name="";
            this.m_Columnlist.Clear();
            this.m_ColumnlistOutput.Clear();
            List<string> columns = new List<string>();
            List<string> all_columns = new List<string>();

            this.m_Tablelist.Clear();
            bool first_start = true;
            string table = "";
            string left_txt = "";
            foreach (string txt in this.richTextBox1.Lines)
            {
                if (txt.Length > 0)
                {
                    table = txt;
                    left_txt = txt.Substring(0, 1);
                    if (left_txt == "[")
                    {
                        table = FuncGeneral.DeleteLeft(txt, 1);
                        table = FuncGeneral.DeleteRight(table, 1);
                        this.m_Tablelist.Add(table);
                        if (first_start == true)
                        {
                            first_start = false;
                        }
                        else
                        {
                            this.m_Columnlist.Add(columns.ToArray());
                            this.m_ColumnlistOutput.Add(all_columns.ToArray());
                        }
                        columns.Clear();
                        all_columns.Clear();
                        table_name = table;
                        this.FillTableInfo(table_name);
                    }
                    else
                    {
                        TableStruct table_struc;
                        table_struc.Table = table_name;
                        string display = "";
                        left_txt = txt.Substring(0, 1);
                        bool add_to_select = false;
                        if (left_txt != "#")
                        {
                            display = txt;
                            add_to_select = true;
                        }
                        else
                        {
                            display = FuncGeneral.DeleteLeft(txt, 1);
                        }
                        string[] daten=display.Split(';');
                        if (daten.Count() > 1)
                        {
                            table_struc.Column = daten[0];
                            table_struc.Display = daten[1];
                        }
                        else
                        {
                            table_struc.Column = display;
                            table_struc.Display = "";
                        }
                        if (add_to_select)
                        {
                            columns.Add(table_struc.Column);
                        }
                        string key = table_struc.Table + table_struc.Column;
                        all_columns.Add(table_struc.Column);
                        
                        table_struc.Columntype = (string)this.m_TableInfo[key];
                        this.m_Datas.Add(key, table_struc);
                    }
                }
            }
            this.m_Columnlist.Add(columns.ToArray());
            this.m_ColumnlistOutput.Add(all_columns.ToArray());
            columns = null;
        }
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Dispose();
        }

        private void bitButton1_Click(object sender, EventArgs e)
        {
            this.richTextBox1.SaveFile("Spalten.cfg", RichTextBoxStreamType.PlainText);
            this.CreateAll();
        }


        private void CbxTable_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cbx_table = (ComboBox)sender;
            string nummer = FuncString.FillForward(FuncString.GetOnlyNumeric(cbx_table.Name),"0",2);
            string obj_name="CbxSpalte"+nummer;
            ComboBox cbx_column = (ComboBox)FuncGeneral.GetControlByName(this, obj_name);
            cbx_column.Items.Clear();

            cbx_column.Items.AddRange(this.m_Columnlist[cbx_table.SelectedIndex]);

            int id = cbx_column.Items.IndexOf(this.m_ClsIni.getValue("AuswahlTabelle", obj_name));
            if (id > -1)
            {
                cbx_column.SelectedIndex = id;
            }
            else
            {
                cbx_column.Text = "";
            }
            this.m_ClsIni.setValue("AuswahlTabelle", cbx_table.Name, cbx_table.SelectedItem.ToString());
            this.m_ClsIni.Save();
        }

        private void cbxAndOr_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.m_CanChange == true)
            {
                ComboBox cbx_column = (ComboBox)sender;
                this.m_ClsIni.setValue("AuswahlTabelle", cbx_column.Name, cbx_column.SelectedItem.ToString());
                this.m_ClsIni.Save();
            }
        }


        private void CbxColumn_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.m_CanChange == true)
            {
                ComboBox cbx_column = (ComboBox)sender;
                this.m_ClsIni.setValue("AuswahlTabelle", cbx_column.Name, cbx_column.SelectedItem.ToString());
                this.m_ClsIni.Save();
            }
        }

        private void txtFilter_TextChanged(object sender, System.EventArgs e)
        {
            TextBox cbx_column = (TextBox)sender;
            this.m_ClsIni.setValue("AuswahlTabelle", cbx_column.Name, cbx_column.Text.ToString());
            this.m_ClsIni.Save();
        }


        private void btnStartQuery_Click(object sender, EventArgs e)
        {

            string cbx_name = "";
            string txt_name="";
            string conjunction = "";
            bool add_where=true;
            bool add_select = true;
            this.m_SqlHelper.SQLBuilderHinzufuegen("", SQLHelper.SQLBuilder.DeleteSQL);
            this.m_SqlHelperWhere.SQLBuilderHinzufuegen("", SQLHelper.SQLBuilder.DeleteSQL);
            for (int i = 1; i <= 6; i++)
            {
                cbx_name = "CbxTabelle" + FuncString.FillForward(i.ToString(), "0", 2);
                ComboBox obj_cbx_tabelle = (ComboBox)FuncGeneral.GetControlByName(this, cbx_name);
                cbx_name = "CbxSpalte" + FuncString.FillForward(i.ToString(), "0", 2);
                ComboBox obj_cbx_column = (ComboBox)FuncGeneral.GetControlByName(this, cbx_name);
                txt_name="txtFilter" + FuncString.FillForward(i.ToString(), "0", 2);
                TextBox obj_txt_filter = (TextBox)FuncGeneral.GetControlByName(this, txt_name);
                    if (obj_cbx_tabelle.SelectedItem != null)
                    {
                        string table = obj_cbx_tabelle.SelectedItem.ToString();
                        if (obj_cbx_column.SelectedItem != null)
                        {
                            if (add_select == true)
                            {
                                add_select = false;
                                this.m_SqlHelper.SQLBuilderHinzufuegen("SELECT TOP 100", SQLHelper.SQLBuilder.DeleteSQL);
                            }

                            string column = obj_cbx_column.SelectedItem.ToString();

                            string suchen = table + column;
                            TableStruct table_struct = (TableStruct)this.m_Datas[suchen];
                            string display = table_struct.Display;

                            this.m_SqlHelper.SQLBuilderHinzufuegen(table, SQLHelper.SQLBuilder.NoSpace);
                            this.m_SqlHelper.SQLBuilderHinzufuegen(".", SQLHelper.SQLBuilder.NoSpace);
                            this.m_SqlHelper.SQLBuilderHinzufuegen(column, SQLHelper.SQLBuilder.NoSpace);
                            if (display.Length > 0)
                            {
                                this.m_SqlHelper.SQLBuilderHinzufuegen(" AS");
                                this.m_SqlHelper.SQLBuilderHinzufuegen(display, SQLHelper.SQLBuilder.NoSpace);
                                this.m_SqlHelper.SQLBuilderHinzufuegen(",", SQLHelper.SQLBuilder.NoSpace);
                            }
                            else
                            {
                                this.m_SqlHelper.SQLBuilderHinzufuegen(",", SQLHelper.SQLBuilder.NoSpace);
                            }
                            if (obj_txt_filter.Text.Length > 0)
                            {
                                if (add_where == true)
                                {
                                    add_where = false;
                                    this.m_SqlHelperWhere.SQLBuilderHinzufuegen("WHERE", SQLHelper.SQLBuilder.DeleteSQL);
                                }

                                if (conjunction.Length > 0)
                                {
                                    this.m_SqlHelperWhere.SQLBuilderHinzufuegen(conjunction);
                                }

                                if ((table_struct.Columntype == "char") || (table_struct.Columntype == "nchar"))
                                {
                                    this.m_SqlHelperWhere.SQLBuilderHinzufuegen(table_struct.Table + "." + table_struct.Column);
                                    this.m_SqlHelperWhere.SQLBuilderHinzufuegen("LIKE");
                                    this.m_SqlHelperWhere.SQLBuilderHinzufuegen("N'%", SQLHelper.SQLBuilder.NoSpace);
                                    this.m_SqlHelperWhere.SQLBuilderHinzufuegen(obj_txt_filter.Text, SQLHelper.SQLBuilder.NoSpace);
                                    this.m_SqlHelperWhere.SQLBuilderHinzufuegen("%'", SQLHelper.SQLBuilder.NoSpace);
                                }
                                else
                                {
                                    this.m_SqlHelperWhere.SQLBuilderHinzufuegen(table_struct.Table + "." + table_struct.Column);
                                    this.m_SqlHelperWhere.SQLBuilderHinzufuegen("=");
                                    this.m_SqlHelperWhere.SQLBuilderHinzufuegen(obj_txt_filter.Text, SQLHelper.SQLBuilder.NoSpace);
                                }
                                if (i >= 1 || i < 3)
                                {
                                    conjunction = " AND";
                                }

                                if (i == 4)
                                {
                                    conjunction = " " + this.cbxAndOr01.Text;

                                }
                                if (i == 5)
                                {
                                    conjunction = " " + this.cbxAndOr02.Text;
                                }
                            }
                     }
                }
            }
            string sql = "";
            string sql_where = "";
            string sql_inner_join = "";
            sql = this.m_SqlHelper.SQLBuilderHinzufuegen("", SQLHelper.SQLBuilder.NoSpace);
            sql = FuncGeneral.DeleteRight(sql, 1);
            this.m_SqlHelper.SQLBuilderHinzufuegen(sql, SQLHelper.SQLBuilder.DeleteSQL);


            this.m_SqlHelperInnerJoin.SQLBuilderHinzufuegen("FROM", SQLHelper.SQLBuilder.DeleteSQL);
            this.m_SqlHelperInnerJoin.SQLBuilderHinzufuegen("MW_KENNDATEN_EINGABE");
            this.m_SqlHelperInnerJoin.SQLBuilderHinzufuegen("INNER JOIN");
            this.m_SqlHelperInnerJoin.SQLBuilderHinzufuegen("MW_ERGEBNIS");
            this.m_SqlHelperInnerJoin.SQLBuilderHinzufuegen("ON");
            this.m_SqlHelperInnerJoin.SQLBuilderHinzufuegen("MW_KENNDATEN_EINGABE.LINK = MW_ERGEBNIS.Link");
            this.m_SqlHelperInnerJoin.SQLBuilderHinzufuegen("INNER JOIN");
            this.m_SqlHelperInnerJoin.SQLBuilderHinzufuegen("MW_WERTE_MESSUNG");
            this.m_SqlHelperInnerJoin.SQLBuilderHinzufuegen("ON");
            this.m_SqlHelperInnerJoin.SQLBuilderHinzufuegen("MW_KENNDATEN_EINGABE.LINK = MW_WERTE_MESSUNG.Link");


            sql = this.m_SqlHelper.SQLBuilderHinzufuegen("", SQLHelper.SQLBuilder.NoSpace);
            sql_inner_join = this.m_SqlHelperInnerJoin.SQLBuilderHinzufuegen("", SQLHelper.SQLBuilder.NoSpace);
            sql_where = this.m_SqlHelperWhere.SQLBuilderHinzufuegen("", SQLHelper.SQLBuilder.NoSpace);

            this.textBox1.Text = sql + sql_inner_join + sql_where;

            this.BindData(this.textBox1.Text, this.m_BS_Auswahl);

        }

        private void rotaBitButton1_Click(object sender, EventArgs e)
        {
            FuncGeneral.KillProgram("EXCEL");
            ExcelHelper xls = new ExcelHelper();
            xls.Workbook_Hinzufuegen();

            this.m_SqlHelper.SQLBuilderHinzufuegen("", SQLHelper.SQLBuilder.NoSpace);
            int i = 0;
            bool add_select = true;
            foreach (string ert in this.m_Tablelist)
            {
                string table = m_Tablelist[i];
                string[] cols = m_ColumnlistOutput[i];
                foreach (string col in cols)
                {
                    if (add_select == true)
                    {
                        add_select = false;
                        this.m_SqlHelper.SQLBuilderHinzufuegen("SELECT TOP 100", SQLHelper.SQLBuilder.DeleteSQL);
                    }
                    string suchen = table + "." + col + "," ;
                    this.m_SqlHelper.SQLBuilderHinzufuegen(suchen, SQLHelper.SQLBuilder.NoSpace);
                }
                i++;
            }


            string sql = "";
            string sql_where = "";
            string sql_inner_join = "";
            sql = this.m_SqlHelper.SQLBuilderHinzufuegen("", SQLHelper.SQLBuilder.NoSpace);
            sql = FuncGeneral.DeleteRight(sql, 1);
            this.m_SqlHelper.SQLBuilderHinzufuegen(sql, SQLHelper.SQLBuilder.DeleteSQL);


            sql = this.m_SqlHelper.SQLBuilderHinzufuegen("", SQLHelper.SQLBuilder.NoSpace);
            sql_inner_join = this.m_SqlHelperInnerJoin.SQLBuilderHinzufuegen("", SQLHelper.SQLBuilder.NoSpace);
            sql_where = this.m_SqlHelperWhere.SQLBuilderHinzufuegen("", SQLHelper.SQLBuilder.NoSpace);

            this.textBox2.Text = sql + sql_inner_join + sql_where;
            xls.SQL2Excel(this.textBox2.Text, m_ConnectionString);
            string file_name = "Messwerte" +  FuncString.GetOnlyNumeric(FuncString.GetTimestamp());
            xls.DateiSpeichern(file_name);

            xls.ExcelHelperClose();
            xls = null;
            
        }

        private void Btn_OpenFolderDocuments_Click(object sender, EventArgs e)
        {
            string pfad = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            System.Diagnostics.Process.Start("explorer", pfad);
        }

    }
}
