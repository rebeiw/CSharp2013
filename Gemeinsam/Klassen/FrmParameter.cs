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
using System.Threading;

namespace Helper
{
    public partial class FrmParameter : FrmVorlageMenu
    {
        public enum FormularType { Parameter = 0, Info, Service,Error,Release };

        public struct PlcItemList
        {
            public string Varname;
            public string S7Adress;
            public string S7Symbol;
            public string S7SymbolType;
            public string SymbolType;
            public string GroupComment;
            public string Comment;
            public string Unit;
            public int Column;
            public string Format;
            public int RankingSymbol;
            public double UpperLimit;
            public double LowerLimit;
            public int UserRightEnable;
        }


        private ClsSingeltonVariablesCollecter m_varCollect;
        protected ClsSingeltonUserManagement m_userManagement;
        protected ClsSingeltonDataBinding m_dataBinding;
        private ClsSingeltonLanguage m_language;
        private ClsSingeltonParameter m_parameter;
        private ClsSingeltonPlc m_plc;

        private SQLiteCommand m_sqliteCommand;
        private SQLiteConnection m_sqliteConnection;
        protected SQLiteDataReader m_sqliteDataReader;

        protected Hashtable m_plcItemList;
        private int m_KeyCount;
        private FormularType m_formularType;

        protected TabControl m_tabControl;


        public FrmParameter()
        {
            InitializeComponent();
            this.m_KeyCount = 0;
            this.m_plcItemList = new Hashtable();
            this.m_userManagement = ClsSingeltonUserManagement.CreateInstance();
            this.m_parameter = ClsSingeltonParameter.CreateInstance();
            this.m_plc = ClsSingeltonPlc.GetInstance();
            this.m_sqliteConnection = new SQLiteConnection();
            this.m_sqliteConnection.ConnectionString = this.m_parameter.ConnectionString;
            this.m_sqliteConnection.Open();

            this.m_sqliteCommand = new SQLiteCommand(this.m_sqliteConnection);


            this.m_dataBinding = ClsSingeltonDataBinding.CreateInstance();

        }

        protected void CreateFormular(FormularType formularType)
        {
            this.m_formularType = formularType;

            if(formularType==FormularType.Parameter || formularType==FormularType.Info || formularType==FormularType.Service)
            {
                this.LoadParameter();
            }
            if (formularType == FormularType.Error || formularType == FormularType.Release)
            {
                this.LoadError();
            }


            this.m_varCollect = ClsSingeltonVariablesCollecter.CreateInstance();
            this.m_language = ClsSingeltonLanguage.CreateInstance(this);
        }


        protected PlcItemList GetPlcItemList(string varName)
        {
            PlcItemList plc_item_list;
            string sql_command = "";
            //0       , 1       , 2           , 3        , 4         , 5           , 6
            sql_command += "Select S7Adress, S7Symbol, S7SymbolType, S7Comment, SymbolType, RankingGroup, GroupComment, ";
            //7            , 8      , 9   , 10          , 11        , 12        , 13             , 14        
            sql_command += "RankingSymbol, Comment, Unit, SymbolFormat, UpperLimit, LowerLimit, UserRightEnable, UserRightVisible, ";
            //15
            sql_command += "Column ";
            sql_command += "from plcitems ";

            
            sql_command += "where VisuSymbol=";
            sql_command += "'" + varName + "'";
            this.m_sqliteCommand.CommandText = sql_command;

            if (this.m_sqliteDataReader != null)
            {
                this.m_sqliteDataReader.Close();
                this.m_sqliteDataReader = null;
            }
            this.m_sqliteDataReader = this.m_sqliteCommand.ExecuteReader();
            this.m_sqliteDataReader.Read();
            plc_item_list.S7Adress = this.m_sqliteDataReader.GetValue(0).ToString();
            plc_item_list.S7Symbol = this.m_sqliteDataReader.GetValue(1).ToString();
            plc_item_list.S7SymbolType = this.m_sqliteDataReader.GetValue(2).ToString();
            plc_item_list.SymbolType = this.m_sqliteDataReader.GetValue(4).ToString();
            plc_item_list.GroupComment = this.m_sqliteDataReader.GetValue(6).ToString();
            plc_item_list.RankingSymbol = Convert.ToInt32(this.m_sqliteDataReader.GetValue(7).ToString());
            plc_item_list.Comment = this.m_sqliteDataReader.GetValue(8).ToString();
            plc_item_list.Unit = this.m_sqliteDataReader.GetValue(9).ToString();
            plc_item_list.Format = this.m_sqliteDataReader.GetValue(10).ToString();
            plc_item_list.Column = Convert.ToInt32(this.m_sqliteDataReader.GetValue(15).ToString());
            plc_item_list.UpperLimit = Convert.ToDouble(this.m_sqliteDataReader.GetValue(11).ToString());
            plc_item_list.LowerLimit = Convert.ToDouble(this.m_sqliteDataReader.GetValue(12).ToString());
            plc_item_list.UserRightEnable = Convert.ToInt32(this.m_sqliteDataReader.GetValue(13).ToString());
            plc_item_list.Varname=varName;
            this.m_sqliteDataReader.Close();

            return plc_item_list;

        }
        

        private void LoadError()
        {
            string sql_command = "";
            //0       , 1       , 2           , 3        , 4         , 5           , 6
            sql_command += "Select S7Adress, S7Symbol, S7SymbolType, S7Comment, SymbolType, cast(RankingGroup as integer) as RankingGroup, GroupComment, ";
            //7            , 8      , 9   , 10          , 11        , 12        , 13             , 14        
            sql_command += "cast(RankingSymbol as integer) as RankingSymbol, Comment, Unit, SymbolFormat, UpperLimit, LowerLimit, UserRightEnable, UserRightVisible, ";
            //15
            sql_command += "Column ";
            if (this.m_formularType == FormularType.Error)
            {
                sql_command += "from plcitems where (SymbolType='E') order by RankingGroup, RankingSymbol";
            }

            if (this.m_formularType == FormularType.Release)
            {
                sql_command += "from plcitems where (SymbolType='R') order by RankingGroup, RankingSymbol";
            }

            this.m_sqliteCommand.CommandText = sql_command;

            if (this.m_sqliteDataReader != null)
            {
                this.m_sqliteDataReader.Close();
                this.m_sqliteDataReader = null;
            }
            this.m_sqliteDataReader = this.m_sqliteCommand.ExecuteReader();
            TabControl tab_control = this.CreateTabControl(4, 20, this.GbxOutput.Width - 8, this.GbxOutput.Height - 25, this.GbxOutput);
            tab_control.TabStop = false;
            this.m_tabControl = tab_control;
            TabPage tab_page = null;
            int page_no = 0;

            tab_page = this.CreateTabPage("Seite " + (page_no + 1), tab_control);
            int gbx_row = 0;
            int gbx_col = 0;
            GroupBox group_box = null;

            Label label_text = null;
            PlcItemList plc_item_list;
            CompLedRound led_round = null;
            string old_group_comment = "";
            int row_comp = 0;

            int gbx_width = (tab_control.DisplayRectangle.Width + 10) / GlobalVar.ConstNumberOfColumns - (GlobalVar.ConstNumberOfColumns+1) *4;

            while (this.m_sqliteDataReader.Read())
            {

                plc_item_list.S7Adress = this.m_sqliteDataReader.GetValue(0).ToString();
                plc_item_list.S7Symbol = this.m_sqliteDataReader.GetValue(1).ToString();
                plc_item_list.S7SymbolType = this.m_sqliteDataReader.GetValue(2).ToString();
                plc_item_list.SymbolType = this.m_sqliteDataReader.GetValue(4).ToString();
                plc_item_list.GroupComment = this.m_sqliteDataReader.GetValue(6).ToString();
                plc_item_list.RankingSymbol = Convert.ToInt32(this.m_sqliteDataReader.GetValue(7).ToString());
                plc_item_list.Comment = this.m_sqliteDataReader.GetValue(8).ToString();
                plc_item_list.Unit = this.m_sqliteDataReader.GetValue(9).ToString();
                plc_item_list.Format = this.m_sqliteDataReader.GetValue(10).ToString();
                plc_item_list.Column = Convert.ToInt32(this.m_sqliteDataReader.GetValue(15).ToString());
                plc_item_list.UpperLimit = Convert.ToDouble(this.m_sqliteDataReader.GetValue(11).ToString());
                plc_item_list.LowerLimit = Convert.ToDouble(this.m_sqliteDataReader.GetValue(12).ToString());
                plc_item_list.UserRightEnable = Convert.ToInt32(this.m_sqliteDataReader.GetValue(13).ToString());

                string[] adress_info = plc_item_list.S7Adress.Split('.');
                string db_number = adress_info[0];
                string varname = db_number + "." + plc_item_list.S7Symbol;

                plc_item_list.Varname = varname;

                int top = 0;
                int left = 0;

                if(old_group_comment!=plc_item_list.GroupComment)
                {
                    old_group_comment = plc_item_list.GroupComment;
                    top = 0;
                    left = 0;
                    if(gbx_row==0)
                    {
                        top = 4;
                        left = 4;
                        gbx_row++;
                    }
                    else
                    {
                        left = group_box.Left;
                        top = group_box.Top + group_box.Height + 4;
                    }
                    group_box = this.CreateGroupBox(left, top, gbx_width, 50, plc_item_list.GroupComment, tab_page);
                    row_comp = 0;
                }
                int height=(row_comp) * 27 + 50;
                int height_max = height + group_box.Top;
                if (height_max>tab_control.DisplayRectangle.Height)
                {
                    gbx_col++;
                    if(gbx_col>2)
                    {
                        page_no++;
                        tab_page = this.CreateTabPage("Seite " + (page_no + 1), tab_control);
                        gbx_row = 0;
                        gbx_col = 0;
                    }
                    else
                    {
                        left = gbx_col * (gbx_width + 4) + 4;
                        top = 4;
                        group_box = this.CreateGroupBox(left, top, gbx_width, 50, plc_item_list.GroupComment, tab_page);
                        row_comp = 0;
                    }
                }
                height = (row_comp) * 27 + 50;
                group_box.Height = height;

                int top_comp = row_comp * 27 + 20;
                int left_comp = 4;
                if (this.m_formularType == FormularType.Release)
                {
                    led_round = this.CreateLedRound(left_comp, top_comp, group_box, CompLedRound.LEDType.Release);
                }
                if (this.m_formularType == FormularType.Error)
                {
                    led_round = this.CreateLedRound(left_comp, top_comp, group_box, CompLedRound.LEDType.Error);
                }
                this.m_dataBinding.AddList(this, led_round.Name.ToString(), "State", varname);
                label_text = this.CreateLabel(left_comp + 40, top_comp, 200, 25, plc_item_list.Comment, group_box, ContentAlignment.MiddleLeft);
                row_comp++;;
            }
            this.m_sqliteDataReader.Close();
            this.m_sqliteDataReader = null;
        }
        private void LoadParameter()
        {
            string sql_command="";
                               //0       , 1       , 2           , 3        , 4         , 5           , 6
            sql_command += "Select S7Adress, S7Symbol, S7SymbolType, S7Comment, SymbolType, cast(RankingGroup as integer) as RankingGroup, GroupComment, ";
                          //7            , 8      , 9   , 10          , 11        , 12        ,  13             , 14        
            sql_command += "cast(RankingSymbol as integer) as RankingSymbol, Comment, Unit, SymbolFormat, UpperLimit, LowerLimit, UserRightEnable, UserRightVisible, ";
                         //15
            sql_command += "Column ";
            if(this.m_formularType==FormularType.Parameter)
            {
                sql_command += "from plcitems where (SymbolType='P' or SymbolType='PO') order by RankingGroup, RankingSymbol";
            }

            if (this.m_formularType == FormularType.Info)
            {
                sql_command += "from plcitems where (SymbolType='I') order by RankingGroup, RankingSymbol";
            }
            if (this.m_formularType == FormularType.Service)
            {
                sql_command += "from plcitems where (SymbolType='S') order by RankingGroup, RankingSymbol";
            }
            this.m_sqliteCommand.CommandText = sql_command;

            if(this.m_sqliteDataReader!=null)
            {
                this.m_sqliteDataReader.Close();
                this.m_sqliteDataReader=null;
            }
            this.m_sqliteDataReader = this.m_sqliteCommand.ExecuteReader();
            int i = 0;
            TabControl tab_control=this.CreateTabControl(4, 20, this.GbxOutput.Width - 8, this.GbxOutput.Height - 25, this.GbxOutput);
            tab_control.TabStop = false;
            this.m_tabControl = tab_control;
            TabPage tab_page = null;
            GroupBox group_box = null;
            Label label_text = null;
            Label label_unit = null;
            CompToggleSwitch toggle_switch = null;
            CompTxtBox txt_box = null;
            CompInputBox input_box = null;
            PlcItemList plc_item_list;
            CompLedRectangle led_rectangle = null;
            string old_group_comment = "";
            int row = 0;
            int old_ranging_symbol = 0;
            bool first_start = true;
            while (this.m_sqliteDataReader.Read())
            {

                plc_item_list.S7Adress = this.m_sqliteDataReader.GetValue(0).ToString();
                plc_item_list.S7Symbol = this.m_sqliteDataReader.GetValue(1).ToString();
                plc_item_list.S7SymbolType = this.m_sqliteDataReader.GetValue(2).ToString();
                plc_item_list.SymbolType = this.m_sqliteDataReader.GetValue(4).ToString();
                plc_item_list.GroupComment = this.m_sqliteDataReader.GetValue(6).ToString();
                plc_item_list.RankingSymbol = Convert.ToInt32(this.m_sqliteDataReader.GetValue(7).ToString());
                if (first_start)
                {
                    first_start = false;
                    old_ranging_symbol = plc_item_list.RankingSymbol + 1;
                }

                plc_item_list.Comment = this.m_sqliteDataReader.GetValue(8).ToString();
                plc_item_list.Unit = this.m_sqliteDataReader.GetValue(9).ToString();
                plc_item_list.Format = this.m_sqliteDataReader.GetValue(10).ToString();
                plc_item_list.Column = Convert.ToInt32(this.m_sqliteDataReader.GetValue(15).ToString());
                plc_item_list.UpperLimit = Convert.ToDouble(this.m_sqliteDataReader.GetValue(11).ToString());
                plc_item_list.LowerLimit = Convert.ToDouble(this.m_sqliteDataReader.GetValue(12).ToString());
                plc_item_list.UserRightEnable = Convert.ToInt32(this.m_sqliteDataReader.GetValue(13).ToString());

                string[] adress_info = plc_item_list.S7Adress.Split('.');
                string db_number = adress_info[0];
                string varname = db_number + "." + plc_item_list.S7Symbol;

                plc_item_list.Varname=varname;
                if (plc_item_list.GroupComment != old_group_comment)
                {
                    old_ranging_symbol = plc_item_list.RankingSymbol-1;
                    row = 0;
                    old_group_comment = plc_item_list.GroupComment;
                    tab_page = (TabPage)this.CreateTabPage(plc_item_list.GroupComment, tab_control);
                    group_box = (GroupBox)this.CreateGroupBox(4, 4, tab_control.Width - 18, tab_control.Height - 56, old_group_comment, tab_page);
                }

                int top = row * (GlobalVar.ConstParameterTextHeigt + GlobalVar.ConstControlSpacing) + GlobalVar.ConstGroupBoxOffset;

                int text_width = group_box.Width - (5 * GlobalVar.ConstControlSpacing + 
                                                        GlobalVar.ConstParameterTxtBoxWidth + 
                                                        GlobalVar.ConstParameterTxtBoxWidth + 
                                                        GlobalVar.ConstParameterTxtBoxWidth) - 
                                                        GlobalVar.ConstOffsetCorrectWidth;
                label_text = (Label)this.CreateLabel(GlobalVar.ConstControlSpacing, top, text_width, 
                                                     GlobalVar.ConstParameterTextHeigt, plc_item_list.Comment, 
                                                     group_box, System.Drawing.ContentAlignment.MiddleRight);
                int left_unit = 0;
                if (plc_item_list.S7SymbolType == "BOOL")
                {
                    if (plc_item_list.SymbolType == "P" || plc_item_list.SymbolType == "S")
                    {
                        toggle_switch = (CompToggleSwitch)this.CreateToggleSwitch(GlobalVar.ConstControlSpacing + 
                                                                                  label_text.Width + label_text.Left, top, group_box);
                        toggle_switch.Click += new System.EventHandler(this.ToggleSwitch_Click);
                        toggle_switch.Symbol = varname;
                        toggle_switch.DoSwitch = false;
                        this.m_dataBinding.AddList(this, toggle_switch.Name.ToString(), "State", varname);
                        led_rectangle = this.CreateLedRectangle(GlobalVar.ConstControlSpacing + toggle_switch.Width + toggle_switch.Left, top, group_box);
                        this.m_dataBinding.AddList(this, led_rectangle.Name.ToString(), "State", varname);
                        this.m_userManagement.AddUserRightControl(toggle_switch, plc_item_list.UserRightEnable);
                    }
                    if (plc_item_list.SymbolType == "I")
                    {
                        led_rectangle = this.CreateLedRectangle(GlobalVar.ConstControlSpacing + label_text.Width + label_text.Left, top, group_box);
                        this.m_dataBinding.AddList(this, led_rectangle.Name.ToString(), "State", varname);
                    }
                }
                else
                {
                    if (plc_item_list.SymbolType == "PO" || plc_item_list.SymbolType == "I" || plc_item_list.SymbolType == "SO")
                    {
                        int left = GlobalVar.ConstControlSpacing + (plc_item_list.Column - 1) * (GlobalVar.ConstParameterTxtBoxWidth + 
                                                                                                 GlobalVar.ConstControlSpacing) + 
                                                                                                 label_text.Left + 
                                                                                                 label_text.Width;
                        txt_box = this.CreateTxtBox(left, top, plc_item_list.Format, group_box);
                        this.m_dataBinding.AddList(this, txt_box.Name.ToString(), "Text", varname);
                        left_unit = txt_box.Left + txt_box.Width + GlobalVar.ConstControlSpacing;
                    }
                    if (plc_item_list.SymbolType == "P" || plc_item_list.SymbolType == "S")
                    {
                        int left = GlobalVar.ConstControlSpacing + (plc_item_list.Column - 1) * (GlobalVar.ConstParameterTxtBoxWidth + 
                                                                                                 GlobalVar.ConstControlSpacing) + 
                                                                                                 label_text.Left + 
                                                                                                 label_text.Width;
                        input_box = this.CreateInputBox(left, top, plc_item_list.Format, group_box);
                        input_box.Symbol = varname;
                        input_box.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.InputBox_KeyPress);
                        input_box.Leave += new System.EventHandler(this.InputBox_Leave);
                        this.m_dataBinding.AddList(this, input_box.Name.ToString(), "Text", varname);
                        left_unit = input_box.Left + input_box.Width + GlobalVar.ConstControlSpacing;
                        this.m_plcItemList.Add(input_box, plc_item_list);
                        this.m_userManagement.AddUserRightControl(input_box,plc_item_list.UserRightEnable);
                        left_unit = input_box.Left + input_box.Width + GlobalVar.ConstControlSpacing;
                    }
                }
                label_unit = (Label)this.CreateLabel(left_unit, top, GlobalVar.ConstParameterTxtBoxWidth, 
                                                                     GlobalVar.ConstParameterTextHeigt, 
                                                                     plc_item_list.Unit, 
                                                                     group_box, 
                                                                     System.Drawing.ContentAlignment.MiddleLeft);
                if (plc_item_list.RankingSymbol != old_ranging_symbol)
                {
                    old_ranging_symbol = plc_item_list.RankingSymbol;
                    row++;
                }
                i++;
            }
            this.m_sqliteDataReader.Close();
            this.m_sqliteDataReader = null;
        }

        protected void InputBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                CompInputBox comp = (CompInputBox)sender;

                PlcItemList plc_item_list = (PlcItemList)this.m_plcItemList[sender];
                string var_name = comp.Symbol;
                string obj_type = sender.GetType().ToString();

                double value = 0.0;
                double.TryParse(comp.Text, out value);
                if(value>plc_item_list.UpperLimit)
                {
                    value = plc_item_list.UpperLimit;
                }
                if (value < plc_item_list.LowerLimit)
                {
                    value = plc_item_list.LowerLimit;
                }
                comp.Text = value.ToString();
                this.m_varCollect.WriteValue(var_name, comp.Text, true);
                this.m_dataBinding.Dispatch(var_name, true);
                comp.SelectAll();
                this.m_plc.AddWriteList(var_name, comp.Text);
                this.m_KeyCount = 0;
            }
            else
            {
                this.m_KeyCount++;
            }
        }

        protected void InputBox_Leave(object sender, EventArgs e)
        {
            if(this.m_KeyCount>0)
            {
                CompInputBox comp = (CompInputBox)sender;
                string var_name = comp.Symbol;
                string obj_type = sender.GetType().ToString();
                this.m_plc.AddWriteList(var_name, comp.Text);
                this.m_KeyCount = 0;
            }
        }

        private void ToggleSwitch_Click(object sender, EventArgs e)
        {
            string symbol = "";
            CompToggleSwitch comp = (CompToggleSwitch)sender;
            symbol = comp.Symbol;
            if (comp.State==CompToggleSwitch.CompToggleSwitchState.Off)
            {
                this.m_plc.AddWriteList(symbol, "1");
            }
            else
            {
                this.m_plc.AddWriteList(symbol, "0");
            }
        }

        private void FrmParameter_Activated(object sender, EventArgs e)
        {
            this.m_parameter.ActualForm = this;
        }

        private void FrmParameter_Load(object sender, EventArgs e)
        {

        }
    }
}
