using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Helper
{
    public class ClsSingeltonParameter
    {
        private static ClsSingeltonParameter m_instance;

        SQLiteCommand m_sqliteCommand;
        SQLiteConnection m_sqliteConnection;
        SQLiteDataReader m_sqliteDataReader;


        private string m_ConnectionString;

        public string ConnectionString
        {
            get { return m_ConnectionString; }
        }

        private Form m_ActualForm;

        public Form ActualForm
        {
            get { return this.m_ActualForm; }
            set { this.m_ActualForm = value; }
        }


        private bool m_PasswordOk;

        public bool PasswordOk
        {
            get { return this.m_PasswordOk; }
            set { this.m_PasswordOk = value; }
        }

        public string LastUser
        {
            get { return this.GetParameter("LastUser", "0;User Management"); }
            set { this.SetParameter("LastUser", value, "0;User Management"); }
        }

        public string MaxTimeLogin
        {
            get { return this.GetParameter("MaxTimeLogin", "10;User Management"); }
            set { this.SetParameter("MaxTimeLogin", value, "10;User Management"); }
        }

        public string ActualUser
        {
            get { return this.GetParameter("ActualUser", "0;User Management"); }
            set { this.SetParameter("ActualUser", value, "0;User Management"); }
        }

        public string PlcIp
        {
            get { return this.GetParameter("PlcIp", "192.168.2.118;Plc"); }
            set { this.SetParameter("PlcIp", value, "192.168.2.118;Plc"); }
        }

        public string PlcRack
        {
            get { return this.GetParameter("PlcRack", "0;Plc"); }
            set { this.SetParameter("PlcRack", value, "0;Plc"); }
        }

        public string PlcSlot
        {
            get { return this.GetParameter("PlcSlot", "2;Plc"); }
            set { this.SetParameter("PlcSlot", value, "2;Plc"); }
        }

        public string Language
        {
            get { return this.GetParameter("Language","De;System"); }
            set { this.SetParameter("Language", value,"De;System"); }
        }

        private ClsSingeltonParameter(string ConnectionString)
        {
            this.m_ConnectionString = ConnectionString;
            this.m_sqliteConnection = new SQLiteConnection();
            this.m_sqliteConnection.ConnectionString = this.m_ConnectionString;
            this.m_sqliteConnection.Open();

            this.m_sqliteCommand = new SQLiteCommand(this.m_sqliteConnection);

        }


        public static ClsSingeltonParameter CreateInstance(string ConnectionString)
        {
            if (m_instance == null)
            {
                m_instance = new ClsSingeltonParameter(ConnectionString);
            }
            return m_instance;
        }

        public static ClsSingeltonParameter CreateInstance()
        {
            if (m_instance == null)
            {
                m_instance = new ClsSingeltonParameter("Data Source=Daten.db");
            }
            return m_instance;
        }

        public void SetParameter(string parameter, string value, string defaultValue)
        {
            this.CloseReader();
            this.GetParameter(parameter, defaultValue);
            string sql_cmd = "";
            sql_cmd = "update parameter set Value=";
            sql_cmd += "'" + value + "' ";
            sql_cmd += "where parameter = ";
            sql_cmd += "'" + parameter + "'";
            this.m_sqliteCommand.CommandText = sql_cmd;
            this.m_sqliteCommand.ExecuteNonQuery();
        }

        private void CloseReader()
        {
            if(this.m_sqliteDataReader!=null)
            {
                this.m_sqliteDataReader.Close();
                this.m_sqliteDataReader = null;
            }
        }


        public string GetParameter(string parameter, string defaultValue)
        {
            this.CloseReader();
            string comment = "";
            string value = "";
            string[] default_comment=defaultValue.Split(';');
            if(default_comment.Count()>1)
            {
                value = default_comment[0];
                comment = default_comment[1];
            }

            string sql_cmd = "";
            sql_cmd += "INSERT ";
            sql_cmd += "INTO ";
            sql_cmd += "Parameter(Parameter,Value,Comment) "; 
            sql_cmd += "SELECT ";
            sql_cmd += "'" + parameter + "',";
            sql_cmd += "'" + value + "',";
            sql_cmd += "'" + comment +"' "; 
            sql_cmd += "WHERE NOT EXISTS ";
            sql_cmd += "(SELECT 1 FROM Parameter WHERE Parameter = ";
            sql_cmd += "'" + parameter + "'";
            sql_cmd += ")";
            this.m_sqliteCommand.CommandText = sql_cmd;
            this.m_sqliteCommand.ExecuteNonQuery();

            sql_cmd = "";
            sql_cmd = "select Value from parameter where parameter = ";
            sql_cmd += "'" + parameter + "'";
            this.m_sqliteCommand.CommandText = sql_cmd;
            this.m_sqliteDataReader = this.m_sqliteCommand.ExecuteReader();
            this.m_sqliteDataReader.Read();

            string retval =this.m_sqliteDataReader.GetValue(0).ToString();
            this.CloseReader();
            return retval;
        }
    }
}
