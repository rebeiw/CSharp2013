using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data.SQLite;

namespace Helper
{
    public partial class FrmParameter : FrmVorlageMenu
    {
        TabControl tabControl1;

        private ClsSingeltonVariablesCollecter m_varCollect;
        private ClsSingeltonUserManagement m_userManagement;
        private ClsSingeltonDataBinding m_dataBinding;
        private ClsSingeltonLanguage m_language;
        private ClsSingeltonFormularManager m_formularManager;
        private ClsSingeltonParameter m_parameter;

        private SQLiteCommand m_sqliteCommand;
        private SQLiteConnection m_sqliteConnection;
        private SQLiteDataReader m_sqliteDataReader;

        private Hashtable m_plcItemList;

        public FrmParameter()
        {
            this.m_plcItemList = new Hashtable();
            this.m_parameter = ClsSingeltonParameter.CreateInstance();
            this.m_sqliteConnection = new SQLiteConnection();
            this.m_sqliteConnection.ConnectionString = this.m_parameter.ConnectionString;
            this.m_sqliteConnection.Open();

            this.m_sqliteCommand = new SQLiteCommand(this.m_sqliteConnection);

            InitializeComponent();
            this.Width = 1024;
            this.Height = 768;

            this.LoadParameter();

            this.m_formularManager = ClsSingeltonFormularManager.CreateInstance(this, this.Name);
            this.m_userManagement = ClsSingeltonUserManagement.CreateInstance(this);

            this.m_varCollect = ClsSingeltonVariablesCollecter.CreateInstance();
            this.m_dataBinding = ClsSingeltonDataBinding.CreateInstance();

            
            this.m_language = ClsSingeltonLanguage.CreateInstance(this);

        }

        public struct PlcItemList
        {
            public string S7Adress;
            public string GroupComment;

        }

        private void LoadParameter()
        {
            string sql_command="";
                               //0       , 1       , 2           , 3        , 4         , 5           , 6
            sql_command+="Select S7Adress, S7Symbol, S7SymbolType, S7Comment, SymbolType, RankingGroup, GroupComment, ";
                          //7            , 8      , 9   , 10          , 11        , 12        , 13             , 14        
            sql_command += "RankingSymbol, Comment, Unit, SymbolFormat, UpperLimit, LowerLimit, UserRightEnable, UserRightVisible ";
            
            sql_command +="from plcitems where SymbolType='P' order by RankingGroup, RankingSymbol";
            
            this.m_sqliteCommand.CommandText = sql_command;

            if(this.m_sqliteDataReader!=null)
            {
                this.m_sqliteDataReader.Close();
                this.m_sqliteDataReader=null;
            }
            this.m_sqliteDataReader = this.m_sqliteCommand.ExecuteReader();
            int i = 0;
            TabControl tab_control=this.CreateTabControl(4, 20, this.GbxOutput.Width - 8, this.GbxOutput.Height - 25, this.GbxOutput);
            TabPage tab_page = null;
            GroupBox group_box = null;
            PlcItemList pls_item_list;

            string old_group_comment = "";
            int row = 0;

            while (this.m_sqliteDataReader.Read())
            {

                pls_item_list.S7Adress = this.m_sqliteDataReader.GetValue(0).ToString();
                pls_item_list.GroupComment = this.m_sqliteDataReader.GetValue(6).ToString();

                if (pls_item_list.GroupComment != old_group_comment)
                {
                    row = 0;
                    old_group_comment = pls_item_list.GroupComment;
                    tab_page = (TabPage)this.CreateTabPage(pls_item_list.GroupComment, tab_control);
                    group_box = (GroupBox)this.CreateGroupBox(4, 4, tab_control.Width - 18, tab_control.Height - 56, old_group_comment, tab_page);

                }
                this.CreateLedRectangle(4, row * 50 + 20, group_box);
                this.CreateToggleSwitch(84, row * 50 + 20, group_box);
                this.CreateLabel(167, row * 50 + 20, 100, 39, "Pumpen 1", group_box, System.Drawing.ContentAlignment.MiddleLeft);
                row++;
                i++;
                //this.m_plcItemList.Add();
                //this.AddLanguage(, this.m_sqliteDataReader.GetValue(2).ToString(), this.m_sqliteDataReader.GetValue(3).ToString(), this.m_sqliteDataReader.GetValue(4).ToString(), this.m_sqliteDataReader.GetValue(5).ToString(), this.m_sqliteDataReader.GetValue(6).ToString());
            }
            this.m_sqliteDataReader.Close();
            this.m_sqliteDataReader = null;
        }

        private void FrmParameter_Load(object sender, EventArgs e)
        {

        }
    }
}
