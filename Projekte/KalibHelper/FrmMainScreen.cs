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
using Helper;
using System.IO;

namespace KalibHelper
{
    public partial class FrmMainScreen : FrmVorlageMenu
    {

        private BindingSource BS_Eingabe;
        private BindingSource BS_Messwerte;
        private BindingSource BS_Ergebnis;

        //private string m_ConnectionString =
        //    "Data Source=3x75002;" +
        //    "Initial Catalog=Kalibwin;" +
        //    "User ID=sa;" +
        //    "Password='Sqladmin!'";


        private string m_ConnectionString =
            "Data Source=WE1S043;" +
            "Initial Catalog=Kalibwin;" +
            "User ID=Kalibuser;" +
            "Password=''";

        //private string m_ConnectionString =
        //    "Data Source=H4T9FV1;" +
        //    "Initial Catalog=Kalibwin;" +
        //    "Integrated Security=True";

        private string query = "";
        public FrmMainScreen()
        {
            InitializeComponent();
            this.BTN_NeueSuche.Top = this.BTN_Suchen.Top;
            this.BTN_NeueSuche.Left = this.BTN_Suchen.Left;
            this.BTN_NeueSuche_1.Left = this.BTN_Suchen_1.Left;
            this.BTN_NeueSuche_2.Left = this.BTN_Suchen_2.Left;
            this.BTN_NeueSuche_3.Left = this.BTN_Suchen_3.Left;

            this.BTN_OpenFolderDocumente.Top = this.BTN_Excel.Top;
            this.BTN_OpenFolderDocumente.Left = this.BTN_Excel.Left;

            FuncGeneral.Start();
            this.BS_Eingabe = new BindingSource();
            this.BS_Messwerte = new BindingSource();
            this.BS_Ergebnis = new BindingSource();

            this.DBGRID_Eingabe.DataSource = this.BS_Eingabe;
            this.DBGRID_Messwerte.DataSource = this.BS_Messwerte;
            this.DBGRID_Ergebnis.DataSource = this.BS_Ergebnis;

            this.toolStripMenuItem2.Image = Bmp.btg_exit;
            this.toolStripMenuItem1.Image = Bmp.btg_Excel;
            this.suchenToolStripMenuItem.Image = Bmp.btg_Requery;
            this.oeffnenToolStripMenuItem.Image = Bmp.btg_folder;


            this.TXT_Link_Eingabe.Focus();

        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Dispose();
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


        private void TXT_Link_Changed(object sender, EventArgs e)
        {
            TextBox txtBox = (TextBox)sender;
            TextBox txtBoxMesswerte = (TextBox)FuncGeneral.GetControlByName(this, "TXT_Link_Messwerte");
            TextBox txtBoxErgebnis = (TextBox)FuncGeneral.GetControlByName(this, "TXT_Link_Ergebnis");
            TextBox txtBoxEingabe = (TextBox)FuncGeneral.GetControlByName(this, "TXT_Link_Eingabe");
            if (txtBox.Name.ToString() == "TXT_Link_Messwerte")
            {
                txtBoxErgebnis.Text=txtBox.Text;
                txtBoxEingabe.Text = txtBox.Text;
            }
            if (txtBox.Name.ToString() == "TXT_Link_Ergebnis")
            {
                txtBoxMesswerte.Text = txtBox.Text;
                txtBoxEingabe.Text = txtBox.Text;
            }
            if (txtBox.Name.ToString() == "TXT_Link_Eingabe")
            {
                txtBoxMesswerte.Text = txtBox.Text;
                txtBoxErgebnis.Text = txtBox.Text;
            }
        }

        private void BTN_Suchen_Click(object sender, EventArgs e)
        {
            this.tabControl1.SelectedTab = this.tabPage1;

            string link = this.TXT_Link_Eingabe.Text;
            if (link.Length > 0)
            {
                query = "SELECT LINK, Datum, Zeit, MS_Code, Modell, Modell2 FROM MW_KENNDATEN_EINGABE Where Link like '" + link + "%' order by link";
                int anz=this.BindData(query, this.BS_Eingabe);

                if (anz == 1)
                {
                    this.toolStripMenuItem1.Enabled = true;
                    this.BTN_Excel.Visible = true;
                    this.BTN_NeueSuche.Visible = true;
                    this.BTN_NeueSuche_1.Visible = true;
                    this.BTN_NeueSuche_2.Visible = true;
                    this.BTN_NeueSuche_3.Visible = true;
                    this.BTN_Suchen.Visible = false;
                    this.BTN_Suchen_1.Visible = false;
                    this.BTN_Suchen_2.Visible = false;
                    this.BTN_Suchen_3.Visible = false;
                    this.BTN_OpenFolderDocumente.Visible = false;

                    query = "SELECT * FROM MW_KENNDATEN_EINGABE Where Link='" + link + "' order by link";
                    this.BindData(query, this.BS_Eingabe);

                    query = "SELECT * FROM MW_WERTE_MESSUNG Where Link='" + link + "' order by link, messnr";

                    this.BindData(query, this.BS_Messwerte);

                    query = "SELECT * FROM MW_ERGEBNIS Where Link='" + link + "' order by link";
                    this.BindData(query, this.BS_Ergebnis);
                    this.DBGRID_Eingabe.DataSource = this.BS_Eingabe;
                    this.DBGRID_Messwerte.DataSource = this.BS_Messwerte;
                    this.DBGRID_Ergebnis.DataSource = this.BS_Ergebnis;


                }
                else
                {
                    this.DBGRID_Eingabe.DataSource = this.BS_Eingabe;
                    this.DBGRID_Messwerte.DataSource = null;
                    this.DBGRID_Ergebnis.DataSource = null;

                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.TXT_Link_Eingabe.Focus();
            this.ShowClose = true;
        }

        private void BTN_Excel_Click(object sender, EventArgs e)
        {
            if (FuncGeneral.ProgramRunning("EXCEL") == true)
            {
                MessageBox.Show("Excel ist noch offen. Bitte alle Exceldateien schliessen und Export nochmal starten.");
            }
            else
            {
                string link = this.TXT_Link_Eingabe.Text;
                ExcelHelper xl = new ExcelHelper();
                xl.Workbook_Hinzufuegen();
                query = "SELECT * FROM MW_KENNDATEN_EINGABE Where Link='" + link + "' order by link";

                xl.Setze_TabelleBlatt("Sheet1");
                xl.Setze_TabelleBlattName("Sheet1", "Eingabe");
                xl.SQL2Excel(query, this.m_ConnectionString);

                query = "SELECT * FROM MW_ERGEBNIS Where Link='" + link + "' order by link";
                xl.Setze_TabelleBlatt("Sheet2");
                xl.Setze_TabelleBlattName("Sheet2", "Ergebnis");
                xl.SQL2Excel(query, this.m_ConnectionString);


                query = "SELECT * FROM MW_WERTE_MESSUNG Where Link='" + link + "' order by link, messnr";
                xl.Setze_TabelleBlatt("Sheet3");
                xl.Setze_TabelleBlattName("Sheet3", "Messwerte");
                xl.SQL2Excel(query, this.m_ConnectionString);



                xl.DateiSpeichern(link.TrimEnd());

                xl.ExcelHelperClose();
                GC.Collect();
            }

        }

        private void BTN_NeueSuche_Click(object sender, EventArgs e)
        {
            this.BTN_NeueSuche.Visible = false;
            this.BTN_NeueSuche_1.Visible = false;
            this.BTN_NeueSuche_2.Visible = false;
            this.BTN_NeueSuche_3.Visible = false;
            this.BTN_Excel.Visible = false;
            this.BTN_Suchen.Visible = true;
            this.BTN_Suchen_1.Visible = true;
            this.BTN_Suchen_2.Visible = true;
            this.BTN_Suchen_3.Visible = true;
            this.BTN_OpenFolderDocumente.Visible = true;
            
            //this.TXT_Link_Eingabe.Text="";
            this.DBGRID_Eingabe.DataSource = null;
            this.DBGRID_Messwerte.DataSource = null;
            this.DBGRID_Ergebnis.DataSource = null;
            this.TXT_Link_Eingabe.Focus();

        }

        private void BTN_OpenFolderDocumente_Click(object sender, EventArgs e)
        {
            string pfad=Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            System.Diagnostics.Process.Start("explorer", pfad);
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.BTN_Excel_Click(sender, e);
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void DBGRID_Eingabe_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int asd = e.RowIndex;
            //var wert=null;
            if (e.RowIndex >= 0)
            {
                string wert = this.DBGRID_Eingabe.Rows[e.RowIndex].Cells["Link"].Value.ToString().TrimEnd() + " ";
                this.TXT_Link_Eingabe.Text = wert;
            }

        }

        private void suchenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.BTN_Suchen_Click(sender, e);
        }

        private void DBGRID_Eingabe_SelectionChanged(object sender, EventArgs e)
        {
//            this.DBGRID_Eingabe_CellClick(sender, e);
            //int asd = 0;
        }

        private void oeffnenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string fileName = TXT_Link_Eingabe.Text.TrimEnd();
            string pfad = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string excelFile=pfad+"\\"+fileName+".xlsx";
            if(System.IO.File.Exists(excelFile))
            {
                System.Diagnostics.Process.Start("excel", excelFile);
            }

        }

        private void bitButton1_Click(object sender, EventArgs e)
        {
            DialogResult msg=MessageBox.Show("Wollen Sie wirklich alle Excel-Instanzen beenden.", "Excel", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);

            if (msg == System.Windows.Forms.DialogResult.Yes)
            {
                FuncGeneral.KillProgram("EXCEL");
            }
        }
    }
}
