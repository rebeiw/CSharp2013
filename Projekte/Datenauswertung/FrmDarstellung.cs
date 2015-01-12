using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Helper;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms.DataVisualization.Charting;

namespace Helper
{
    public partial class FrmDarstellung : Helper.FrmVorlageMenu
    {
        private BindingSource BS_Graph;
        private string m_ConnectionString;

        SQLHelper sqlHelper;

        ClsFormularManager glb_FrmMng;
        public FrmDarstellung()
        {
            InitializeComponent();
            this.m_ConnectionString = GlobalVar.Glb_SQLConnecton;
            sqlHelper = new SQLHelper(this.m_ConnectionString);
            BS_Graph = new BindingSource();
            glb_FrmMng = ClsFormularManager.CreateInstance();
            glb_FrmMng.FormularAdd(this, this.Name);


        }

        public void PrepareGraph(List<string>Auswahl,string Von, string Bis)
        {
            string sqlCommand = "";
            sqlHelper.SQLBuilderHinzufuegen("SELECT ", SQLHelper.SQLBuilder.DeleteSQL);
            //sqlHelper.SQLBuilderHinzufuegen("SELECT", SQLHelper.SQLBuilder.DeleteSQL);
            int idx = 0; 

            foreach (string spalte in Auswahl)
            {
                sqlHelper.SQLBuilderHinzufuegen(spalte);
                if (idx < Auswahl.Count-1)
                {
                    sqlHelper.SQLBuilderHinzufuegen(",");
                }
                idx++;
            }
            sqlHelper.SQLBuilderHinzufuegen("FROM MaschDaten");
            sqlHelper.SQLBuilderHinzufuegen("WHERE");
            sqlHelper.SQLBuilderHinzufuegen("(");
            sqlHelper.SQLBuilderHinzufuegen("da >=");
            sqlHelper.SQLBuilderHinzufuegen("CONVERT(DATETIME,");
            sqlHelper.SQLBuilderHinzufuegen("'", SQLHelper.SQLBuilder.NoSpace);
            sqlHelper.SQLBuilderHinzufuegen(FuncString.ConvertDate2En(Von), SQLHelper.SQLBuilder.NoSpace);
            sqlHelper.SQLBuilderHinzufuegen("'");
            sqlHelper.SQLBuilderHinzufuegen(",");
            sqlHelper.SQLBuilderHinzufuegen("102)");
            sqlHelper.SQLBuilderHinzufuegen(")");
            sqlHelper.SQLBuilderHinzufuegen("AND");
            sqlHelper.SQLBuilderHinzufuegen("(");
            sqlHelper.SQLBuilderHinzufuegen("da <=");
            sqlHelper.SQLBuilderHinzufuegen("CONVERT(DATETIME,");
            sqlHelper.SQLBuilderHinzufuegen("'", SQLHelper.SQLBuilder.NoSpace);
            sqlHelper.SQLBuilderHinzufuegen(FuncString.ConvertDate2En(Bis), SQLHelper.SQLBuilder.NoSpace);
            sqlHelper.SQLBuilderHinzufuegen("'");
            sqlHelper.SQLBuilderHinzufuegen(",");
            sqlHelper.SQLBuilderHinzufuegen("102)");
            sqlHelper.SQLBuilderHinzufuegen(")");
            sqlCommand=sqlHelper.SQLBuilderHinzufuegen("");

            this.BindData(sqlCommand, Auswahl, "da", this.BS_Graph);
        }

        private int BindData(string selectCommand, List<string>Auswahl, string XMember, BindingSource BindingSource)
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
                DataView source = new DataView(table);
                
                chart1.Series.Clear();
                chart1.DataSource = source;
                int idx = 0;
                
                foreach (string spalte in Auswahl)
                {
                    chart1.Series.Add(spalte);
                    chart1.Series[idx].ChartType = SeriesChartType.Line;
                    chart1.Series[idx].YValueMembers = spalte;
                    //chart1.Series[idx].XValueType =ChartValueType.Time;
                    //chart1.Series[idx].XValueMember = XMember;
                    idx++;
                }

                chart1.DataBind();
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


        private void FrmDarstellung_Load(object sender, EventArgs e)
        {

        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }

        private void bitButton1_Click(object sender, EventArgs e)
        {
            chart1.ChartAreas[0].AxisX.ScaleView.ZoomReset();
            chart1.ChartAreas[0].AxisY.ScaleView.ZoomReset();

        }

        private void bitButton2_Click(object sender, EventArgs e)
        {
            chart1.Series[2].Enabled = !chart1.Series[2].Enabled;

        }
    }
}
