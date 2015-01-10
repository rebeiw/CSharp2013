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
using System.Data.SqlClient;
using System.Data.SQLite;

namespace ClassDevelopment
{
    public partial class FrmErrorReportingSystem : FrmVorlageMenu
    {

        private ClsSingeltonFormularManager m_formularManager;
        private ClsSingeltonParameter m_parameter;
        private ClsSingeltonLanguage m_language;//!<Instanz auf die Klasse Spracheumschaltung
        private ClsSingeltonPlc m_plc;


        private SQLiteConnection m_sqliteConnection;
        private SQLiteCommand m_sqliteCommandActual;
        private SQLiteCommand m_sqliteCommandHistorie;
        private DataSet m_dataSetActual;
        private DataSet m_dataSetHistorie;
        private SQLiteDataAdapter m_sqliteDataAdapterActual;
        private SQLiteDataAdapter m_sqliteDataAdapterHistorie;

        public FrmErrorReportingSystem()
        {
            InitializeComponent();
            this.m_language = ClsSingeltonLanguage.CreateInstance(this);
            this.m_plc=ClsSingeltonPlc.GetInstance();


            this.tabControl1.DrawMode = TabDrawMode.OwnerDrawFixed;
            this.tabControl1.DrawItem += new DrawItemEventHandler(this.TabControl_DrawItem);

            this.Width = 800;
            this.Height = 600;
            this.m_parameter = ClsSingeltonParameter.CreateInstance();
            this.m_formularManager = ClsSingeltonFormularManager.CreateInstance(this, this.Name);
            this.m_sqliteConnection = new SQLiteConnection();
            this.m_sqliteConnection.ConnectionString = this.m_parameter.ConnectionString;
            this.m_sqliteConnection.Open();


            this.m_sqliteCommandActual=new SQLiteCommand();
            this.m_sqliteCommandActual.CommandText=this.m_plc.GetSqlAkuell();
            this.m_sqliteCommandActual.Connection=this.m_sqliteConnection;
            this.m_sqliteDataAdapterActual = new SQLiteDataAdapter(this.m_sqliteCommandActual);
            this.m_sqliteDataAdapterActual.MissingSchemaAction = MissingSchemaAction.AddWithKey;
            this.m_dataSetActual = new DataSet();
            this.m_sqliteDataAdapterActual.Fill(this.m_dataSetActual, "Aktuell");
            this.dataGridView1.DataSource = this.m_dataSetActual;
            this.dataGridView1.DataMember = "Aktuell";
            this.dataGridView1.ReadOnly = true;
            dataGridView1.Rows[0].DefaultCellStyle.BackColor = Color.Red;
            this.dataGridView1.Columns[0].Width = 130;
            this.dataGridView1.Columns[1].Width = 90;
            this.dataGridView1.Columns[2].Width = 1000;

            this.m_sqliteCommandHistorie = new SQLiteCommand();
            this.m_sqliteCommandHistorie.CommandText = this.GetSqlHistorie();
            this.m_sqliteCommandHistorie.Connection = this.m_sqliteConnection;

            this.m_sqliteDataAdapterHistorie = new SQLiteDataAdapter(this.m_sqliteCommandHistorie);
            this.m_sqliteDataAdapterHistorie.MissingSchemaAction = MissingSchemaAction.AddWithKey;
            this.m_dataSetHistorie = new DataSet();
            this.m_sqliteDataAdapterHistorie.Fill(this.m_dataSetHistorie, "Historie");
            this.dataGridView2.DataSource = this.m_dataSetHistorie;
            this.dataGridView2.DataMember = "Historie";
            this.dataGridView2.ReadOnly = true;
            dataGridView2.Rows[0].DefaultCellStyle.BackColor = Color.Aqua;
            this.dataGridView2.Columns[0].Width = 130;
            this.dataGridView2.Columns[1].Width = 130;
            this.dataGridView2.Columns[2].Width = 90;
            this.dataGridView2.Columns[3].Width = 1000;

            this.timer1.Interval = 1000;
            this.timer1.Enabled = true;
        }


        private string GetSqlHistorie()
        {
            string language = this.m_parameter.Language;
            string sql_historie = "";
            sql_historie += "SELECT ";
            sql_historie += "Errors.InComing, ";
            sql_historie += "Errors.OutGoing, ";
            sql_historie += "Errors.VisuSymbol,  ";
            sql_historie += "Language." + language + " || ': ' || Language_1." + language + " AS " + this.m_language.GetTranslation("Kommentar") + " ";
            sql_historie += "FROM  ";
            sql_historie += "Errors  ";
            sql_historie += "INNER JOIN  ";
            sql_historie += "Plcitems  ";
            sql_historie += "ON  ";
            sql_historie += "Errors.VisuSymbol = Plcitems.VisuSymbol ";
            sql_historie += "INNER JOIN  ";
            sql_historie += "Language ";
            sql_historie += "ON  ";
            sql_historie += "Plcitems.GroupComment = Language.NativeText ";
            sql_historie += "INNER JOIN  ";
            sql_historie += "Language AS Language_1 ";
            sql_historie += "ON ";
            sql_historie += "Plcitems.Comment = Language_1.NativeText ";
            sql_historie += "where outgoing is not null ";
            sql_historie += "order by errors.id desc ";
            sql_historie += "Limit 5000";
            return sql_historie;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (this.m_dataSetActual!=null)
            {
                this.m_dataSetActual.Dispose();
                this.m_dataSetActual = null;
            }
            this.m_dataSetActual = new DataSet();
            this.m_sqliteDataAdapterActual.Fill(this.m_dataSetActual, "Aktuell");
            this.dataGridView1.DataSource = this.m_dataSetActual;
            this.dataGridView1.DataMember = "Aktuell";
            this.dataGridView1.Columns[0].Width = 130;
            this.dataGridView1.Columns[1].Width = 90;
            this.dataGridView1.Columns[2].Width = 1000;

            if (this.m_dataSetHistorie != null)
            {
                this.m_dataSetHistorie.Dispose();
                this.m_dataSetHistorie = null;
            }
            this.m_dataSetHistorie = new DataSet();
            this.m_sqliteDataAdapterHistorie.Fill(this.m_dataSetHistorie, "Historie");
            this.dataGridView2.DataSource = this.m_dataSetHistorie;
            this.dataGridView2.DataMember = "Historie";
            this.dataGridView2.Columns[0].Width = 130;
            this.dataGridView2.Columns[1].Width = 130;
            this.dataGridView2.Columns[2].Width = 90;
            this.dataGridView2.Columns[3].Width = 1000;
        }
        public override void SetLanguage()
        {
            this.timer1.Enabled=false;
            this.m_sqliteConnection.Close();
            this.m_sqliteCommandActual.CommandText = this.m_plc.GetSqlAkuell();
            this.m_sqliteCommandHistorie.CommandText = this.GetSqlHistorie();
            this.m_sqliteConnection.Open();
            this.timer1.Enabled = true;
        }


        private void FrmErrorReportingSystem_Load(object sender, EventArgs e)
        {

        }
    }
}
