using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;
using Helper;

namespace Datenauswertung
{
    public partial class FrmDatenauswertung : FrmVorlageMenu
    {


        private BindingSource BS_Source;
        private BindingSource BS_Drain;

        private SQLHelper sqlHelper;


        private string m_ConnectionString =
            "Data Source=H4T9FV1;" +
            "Initial Catalog=Kalibwin;" +
            "Integrated Security=True";

        ClsFormularManager glb_FrmMng;

        FrmDarstellung frm1;
        public FrmDatenauswertung()
        {

            InitializeComponent();

            GlobalVar.Glb_SQLConnecton = m_ConnectionString;
            frm1 = new FrmDarstellung();
            this.BS_Source = new BindingSource();
            this.BS_Drain = new BindingSource();

            this.sqlHelper = new SQLHelper(m_ConnectionString);

            List<SQLHelper.SQLSpalten> felder = new List<SQLHelper.SQLSpalten>();
            SQLHelper.SQLSpalten feld;

            feld.Feldname = "Spalte";
            feld.Feldtype = "nvarchar(50)";
            felder.Add(feld);

            feld.Feldname = "Von";
            feld.Feldtype = "datetime";
            felder.Add(feld);

            feld.Feldname = "Bis";
            feld.Feldtype = "datetime";
            felder.Add(feld);
            this.sqlHelper.CheckTableSpalten("Auswahl", felder);


            this.DBGRID_Source.DataSource = this.BS_Source;
            this.DBGRID_Drain.DataSource = this.BS_Drain;
            FuncGeneral.Start();
            glb_FrmMng = ClsFormularManager.CreateInstance();
            glb_FrmMng.AddBitButtonCloseAll();
            glb_FrmMng.AddButton(BtnStyle.btg_Graph, frm1.Name);

            this.ShowClose = true;

            ToolTip toolTip1 = new ToolTip();

            toolTip1.AutoPopDelay = 0;
            toolTip1.ShowAlways = true;
            toolTip1.UseAnimation = false;

            toolTip1.SetToolTip(this.BTN_Menu, "Menue");
            toolTip1.SetToolTip(this.BTN_Keyboard, "Tastatur");
            toolTip1.SetToolTip(this.BTN_Close, "Programm beenden");
            toolTip1.SetToolTip(this.BTN_Darstellung, "Grafik anzeigen");
            toolTip1.SetToolTip(this.BTN_Delete, "Auswahl loeschen");
            toolTip1.SetToolTip(this.BTN_SuchenLoeschen, "Suchfeld loeschen");
            toolTip1.SetToolTip(this.BTN_Auswaehlen, "Auswaehlen");

            this.toolStripMenuItem1.Image = Bmp.btg_esc;
            this.wwwwToolStripMenuItem.Image = Bmp.btg_pfeilre;


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

        private void DBGridSourceLoad()
        {
            string query="";
            string TableName = "MaschDaten";
            if (this.TXT_Suchen.Text.Length > 0)
            {
                query = "SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '" + TableName + "'AND (COLUMN_NAME LIKE N'%" + this.TXT_Suchen.Text +"%') order by COLUMN_NAME"; ;
            }
            else
            {
                query = "SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '" + TableName + "' order by COLUMN_NAME"; ;
            }
            int anz = this.BindData(query, this.BS_Source);
            this.DBGRID_Source.Columns[0].Width = 300;

        }

        private void DBGridDrainLoad()
        {
            string query = "";
            query = "SELECT spalte from auswahl order by spalte"; ;
            int anz = this.BindData(query, this.BS_Drain);
            this.DBGRID_Drain.Columns[0].Width = 300;

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.DBGridSourceLoad();
            this.DBGridDrainLoad();

            string von = sqlHelper.SQL2StringList("Select top 1 von from auswahl")[0];
            string bis = sqlHelper.SQL2StringList("Select top 1 bis from auswahl")[0];

            if (von.Length>0)
            {
                this.DATEPICKER_Von.Value = Convert.ToDateTime(von);
                this.TIMEPICKER_Von.Value = this.DATEPICKER_Von.Value;
            }
            else
            {
            }

            if (bis.Length>0)
            {
                this.DATEPICKER_Bis.Value = Convert.ToDateTime(bis);
                this.TIMEPICKER_Bis.Value = this.DATEPICKER_Bis.Value;
            }
            else
            {
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Dispose();
        }

        private void BTN_Menu_Click(object sender, EventArgs e)
        {
            this.ShowMenu();
        }

        private void bitButton4_Click(object sender, EventArgs e)
        {

            frm1.PrepareGraph(sqlHelper.SQL2StringList("select spalte from auswahl order by spalte", false), sqlHelper.SQL2StringList("Select top 1 von from auswahl")[0], sqlHelper.SQL2StringList("Select top 1 bis from auswahl")[0]);
            glb_FrmMng.FormularShow(frm1.Name);
        }

        private void bitButton3_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewCell cell in this.DBGRID_Source.SelectedCells)
            {
                if (cell.Value != null)
                {
                    string spaltenAuswahl = cell.Value.ToString();
                    sqlHelper.SQLBuilderHinzufuegen("IF NOT EXISTS", SQLHelper.SQLBuilder.DeleteSQL);
                    sqlHelper.SQLBuilderHinzufuegen("(");
                    sqlHelper.SQLBuilderHinzufuegen("SELECT 1 FROM Auswahl");
                    sqlHelper.SQLBuilderHinzufuegen("WHERE");
                    sqlHelper.SQLBuilderHinzufuegen("Spalte='", SQLHelper.SQLBuilder.NoSpace);
                    sqlHelper.SQLBuilderHinzufuegen(spaltenAuswahl, SQLHelper.SQLBuilder.NoSpace);
                    sqlHelper.SQLBuilderHinzufuegen("'");
                    sqlHelper.SQLBuilderHinzufuegen(")");
                    sqlHelper.SQLBuilderHinzufuegen("INSERT INTO Auswahl");
                    sqlHelper.SQLBuilderHinzufuegen("(");
                    sqlHelper.SQLBuilderHinzufuegen("Spalte");
                    sqlHelper.SQLBuilderHinzufuegen(")");
                    sqlHelper.SQLBuilderHinzufuegen("VALUES");
                    sqlHelper.SQLBuilderHinzufuegen("(");
                    sqlHelper.SQLBuilderHinzufuegen("'", SQLHelper.SQLBuilder.NoSpace);
                    sqlHelper.SQLBuilderHinzufuegen(spaltenAuswahl, SQLHelper.SQLBuilder.NoSpace);
                    sqlHelper.SQLBuilderHinzufuegen("'");
                    string sql = sqlHelper.SQLBuilderHinzufuegen(")");
                    sqlHelper.SQLExec(sql);
                }
            }
            this.UpdateTimeStamp(sender,e);
            this.DBGridDrainLoad();
        }

        private void TXT_Suchen_TextChanged(object sender, EventArgs e)
        {
            this.DBGridSourceLoad();
        }

        private void bitButton1_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewCell cell in this.DBGRID_Drain.SelectedCells)
            {
                if (cell.Value != null)
                {
                    string spaltenAuswahl = cell.Value.ToString();
                    sqlHelper.SQLBuilderHinzufuegen("DELETE", SQLHelper.SQLBuilder.DeleteSQL);
                    sqlHelper.SQLBuilderHinzufuegen("FROM");
                    sqlHelper.SQLBuilderHinzufuegen("Auswahl");
                    sqlHelper.SQLBuilderHinzufuegen("WHERE");
                    sqlHelper.SQLBuilderHinzufuegen("Spalte='", SQLHelper.SQLBuilder.NoSpace);
                    sqlHelper.SQLBuilderHinzufuegen(spaltenAuswahl, SQLHelper.SQLBuilder.NoSpace);
                    string sql = sqlHelper.SQLBuilderHinzufuegen("'");
                    sqlHelper.SQLExec(sql);
                }
            }
            this.DBGridDrainLoad();
        }

        private void UpdateTimeStamp(object sender, EventArgs e)
        {
            DateTime datumVon = this.DATEPICKER_Von.Value;
            DateTime uhrzeitVon = this.TIMEPICKER_Von.Value;

            string timestamp = "";
            timestamp = datumVon.Year + "-" + datumVon.Month + "-" + datumVon.Day + " " + uhrzeitVon.ToLongTimeString();

            sqlHelper.SQLBuilderHinzufuegen("UPDATE Auswahl", SQLHelper.SQLBuilder.DeleteSQL);
            sqlHelper.SQLBuilderHinzufuegen("SET Von = '", SQLHelper.SQLBuilder.NoSpace);
            sqlHelper.SQLBuilderHinzufuegen(timestamp, SQLHelper.SQLBuilder.NoSpace);
            sqlHelper.SQLExec(sqlHelper.SQLBuilderHinzufuegen("'", SQLHelper.SQLBuilder.NoSpace));

            DateTime datumBis = this.DATEPICKER_Bis.Value;
            DateTime uhrzeitBis = this.TIMEPICKER_Bis.Value;

            timestamp = datumBis.Year + "-" + datumBis.Month + "-" + datumBis.Day + " " + uhrzeitBis.ToLongTimeString();

            sqlHelper.SQLBuilderHinzufuegen("UPDATE Auswahl", SQLHelper.SQLBuilder.DeleteSQL);
            sqlHelper.SQLBuilderHinzufuegen("SET Bis = '", SQLHelper.SQLBuilder.NoSpace);
            sqlHelper.SQLBuilderHinzufuegen(timestamp, SQLHelper.SQLBuilder.NoSpace);
            sqlHelper.SQLExec(sqlHelper.SQLBuilderHinzufuegen("'", SQLHelper.SQLBuilder.NoSpace));
        }

        private void bitButtonSmall2_Click(object sender, EventArgs e)
        {
            this.TXT_Suchen.Text = "";
            this.TXT_Suchen.Focus();
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }

    }
}
