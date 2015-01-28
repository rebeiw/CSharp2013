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

namespace ClassDevelopment
{
    public partial class FrmPara : FrmParameter
    {
        private ClsSingeltonFormularManager m_formularManager;
        private Font m_FontLabelXAxis;

        public FrmPara()
        {
            this.m_FontLabelXAxis = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            InitializeComponent();
            this.Width = 880;
            this.Height = 660;
            this.m_formularManager = ClsSingeltonFormularManager.CreateInstance(this, this.Name);
            this.CreateFormular(FormularType.Parameter);
            this.CreateO2Curve();
            this.CreateCompoundDriveGas();


        }
        private void CreateCompoundDriveGas()
        {
            string caption = "Verbund Treibgas";
            TabPage tab_page = this.CreateTabPage(caption, this.m_tabControl);
            GroupBox group_box = (GroupBox)this.CreateGroupBox(4, 4, this.m_tabControl.Width - 18, this.m_tabControl.Height - 56, caption, tab_page);
            Label label;
            CompMultiBar multi_bar;
            int off = 290;
            int width_label = 48;
            int width = 30;
            int space = 2;
            int left = 4;
            int left_multi_bar = 0;
            for (int i = 0; i < 11; i++)
            {
                string format = "{0:0.00}";
                label = this.CreateLabel(left, i * 20 + off, width_label, 20, String.Format(format, 10 - i) + "-", group_box);
                left_multi_bar = label.Left + label.Width + space;
            }
            string varname = "";
            int col = 0;
            int row = 0;
            for (int i = 0; i < 21; i++)
            {
                multi_bar = this.CreateMultiBar(i * (width + space) + left_multi_bar, off + 13, width, 200, group_box);
                multi_bar.NumberOfBars = 5;
                multi_bar.Name = "compMultiBarCompoundDriveGas" + i.ToString();
                multi_bar.Click += new System.EventHandler(this.compMultiBarDriveGas_Click);

                label = this.CreateLabel(i * (width + space) + left_multi_bar, multi_bar.Top + multi_bar.Height + space, width, 30, i.ToString(), group_box, ContentAlignment.MiddleCenter);
                varname = "DB55.Fuel" + FuncString.FillForward(i.ToString(), "0", 2);
                this.m_dataBinding.AddList(this, multi_bar.Name.ToString(), "Value1", varname);

                PlcItemList plc_item_list = this.GetPlcItemList(varname);

                CompInputBox input_box;
                left = col * (89 + 2) + 72;
                int top = row * (35 + 2) + 30;
                plc_item_list.Varname = varname;
                input_box = this.CreateInputBox(left, top, plc_item_list.Format, group_box);
                input_box.Name = "compInputBoxCompoundDriveGas" + i.ToString();
                input_box.Symbol = varname;
                input_box.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.InputBox_KeyPress);
                input_box.Leave += new System.EventHandler(this.compInputBoxDriveGas_Leave);
                input_box.Enter += new System.EventHandler(this.compInputBoxDriveGas_Enter);
                this.m_dataBinding.AddList(this, input_box.Name.ToString(), "Text", varname);
                this.m_plcItemList.Add(input_box, plc_item_list);
                this.m_userManagement.AddUserRightControl(input_box, plc_item_list.UserRightEnable);
                col++;
                if (col > 6)
                {
                    this.CreateLabel(input_box.Left + input_box.Width + space, input_box.Top, 20, input_box.Height, plc_item_list.Unit, group_box, ContentAlignment.MiddleLeft);
                    row++;
                    col = 0;
                }
            }
        }


        private void CreateO2Curve()
        {
            string caption="O2 Kurve";
            TabPage tab_page = this.CreateTabPage(caption, this.m_tabControl);
            GroupBox group_box = (GroupBox)this.CreateGroupBox(4, 4, this.m_tabControl.Width - 18, this.m_tabControl.Height - 56, caption, tab_page);
            Label label;
            CompMultiBar multi_bar;
            int off = 290;
            int width_label = 48;
            int width = 30;
            int space = 2;
            int left = 4;
            int left_multi_bar = 0;
            for (int i = 0; i < 11; i++)
            {
                string format = "{0:0.00}";
                label = this.CreateLabel(left, i * 20 + off, width_label, 20, String.Format(format, 10 - i) + "-", group_box);
                left_multi_bar = label.Left + label.Width + space;
            }
            string varname = "";
            int col = 0;
            int row = 0;
            for (int i = 0; i < 21; i++)
            {
                multi_bar = this.CreateMultiBar(i * (width + space) + left_multi_bar, off + 13, width, 200, group_box);
                multi_bar.Name = "compMultiBarBurnerLoad" + i.ToString();
                multi_bar.ColorBar1 = Color.Blue;
                multi_bar.Click += new System.EventHandler(this.compMultiBarBurnerLoad_Click);

                label = this.CreateLabel(i * (width + space) + left_multi_bar, multi_bar.Top + multi_bar.Height + space, width, 30, i.ToString(), group_box, ContentAlignment.MiddleCenter);
                varname = "DB55.Burner" + FuncString.FillForward(i.ToString(), "0", 2);
                this.m_dataBinding.AddList(this, multi_bar.Name.ToString(), "Value1", varname);

                PlcItemList plc_item_list = this.GetPlcItemList(varname);

                CompInputBox input_box;
                left = col * (89 + 2) + 72;
                int top = row * (35 + 2) + 30;
                plc_item_list.Varname = varname;
                input_box = this.CreateInputBox(left, top, plc_item_list.Format, group_box);
                input_box.Name = "compInputBoxBurnerLoad" + i.ToString();
                input_box.Symbol = varname;
                input_box.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.InputBox_KeyPress);
                input_box.Leave += new System.EventHandler(this.compInputBoxBurnerLoad_Leave);
                input_box.Enter += new System.EventHandler(this.compInputBoxBurnerLoad_Enter);
                this.m_dataBinding.AddList(this, input_box.Name.ToString(), "Text", varname);
                this.m_plcItemList.Add(input_box, plc_item_list);
                this.m_userManagement.AddUserRightControl(input_box, plc_item_list.UserRightEnable);
                col++;
                if (col > 6)
                {
                    this.CreateLabel(input_box.Left + input_box.Width + space, input_box.Top, 20, input_box.Height, plc_item_list.Unit, group_box, ContentAlignment.MiddleLeft);
                    row++;
                    col = 0;
                }
            }

        }



        private void ResetSelectAllBurnerLoad()
        {
            for(int i=0;i<21;i++)
            {
                CompMultiBar obj_set_focus = (CompMultiBar)this.m_dataBinding.GetControlByName(this, "compMultiBarBurnerLoad" + i.ToString());
                obj_set_focus.Choise = false;
            }
        }

        private void compMultiBarBurnerLoad_Click(object sender, EventArgs e)
        {
            this.ResetSelectAllBurnerLoad();
            CompMultiBar obj=(CompMultiBar)sender;
            string number = FuncString.GetOnlyNumeric(obj.Name);
            obj.Choise = true;
            CompInputBox obj_set_focus = (CompInputBox)this.m_dataBinding.GetControlByName(this, "compInputBoxBurnerLoad" + number);
            obj_set_focus.Focus();
        }

        private void compInputBoxBurnerLoad_Leave(object sender, EventArgs e)
        {
            CompInputBox obj = (CompInputBox)sender;
            string number = FuncString.GetOnlyNumeric(obj.Name);
            CompMultiBar obj_set_focus = (CompMultiBar)this.m_dataBinding.GetControlByName(this, "compMultiBarBurnerLoad" + number);
            obj_set_focus.Choise = false;
            this.InputBox_Leave(sender, e);
        }

        private void compInputBoxBurnerLoad_Enter(object sender, EventArgs e)
        {
            this.ResetSelectAllBurnerLoad();
            CompInputBox obj = (CompInputBox)sender;
            string number = FuncString.GetOnlyNumeric(obj.Name);
            CompMultiBar obj_set_focus = (CompMultiBar)this.m_dataBinding.GetControlByName(this, "compMultiBarBurnerLoad" + number);
            obj_set_focus.Choise = true;
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            this.ResetSelectAllBurnerLoad();
            this.ResetSelectAllDriveGas();
            base.OnFormClosing(e);
        }

        private void ResetSelectAllDriveGas()
        {
            for (int i = 0; i < 21; i++)
            {
                CompMultiBar obj_set_focus = (CompMultiBar)this.m_dataBinding.GetControlByName(this, "compMultiBarCompoundDriveGas" + i.ToString());
                obj_set_focus.Choise = false;
            }
        }

        private void compMultiBarDriveGas_Click(object sender, EventArgs e)
        {
            this.ResetSelectAllBurnerLoad();
            CompMultiBar obj = (CompMultiBar)sender;
            string number = FuncString.GetOnlyNumeric(obj.Name);
            obj.Choise = true;
            CompInputBox obj_set_focus = (CompInputBox)this.m_dataBinding.GetControlByName(this, "compInputBoxCompoundDriveGas" + number);
            obj_set_focus.Focus();
        }

        private void compInputBoxDriveGas_Leave(object sender, EventArgs e)
        {
            CompInputBox obj = (CompInputBox)sender;
            string number = FuncString.GetOnlyNumeric(obj.Name);
            CompMultiBar obj_set_focus = (CompMultiBar)this.m_dataBinding.GetControlByName(this, "compMultiBarCompoundDriveGas" + number);
            obj_set_focus.Choise = false;
            this.InputBox_Leave(sender, e);
        }

        private void compInputBoxDriveGas_Enter(object sender, EventArgs e)
        {
            this.ResetSelectAllDriveGas();
            CompInputBox obj = (CompInputBox)sender;
            string number = FuncString.GetOnlyNumeric(obj.Name);
            CompMultiBar obj_set_focus = (CompMultiBar)this.m_dataBinding.GetControlByName(this, "compMultiBarCompoundDriveGas" + number);
            obj_set_focus.Choise = true;
        }

        private void FrmPara_Load(object sender, EventArgs e)
        {

        }
    }
}
