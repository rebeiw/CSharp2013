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
    public partial class FrmVorlage : Form
    {
        private ClsSingeltonFormularManager m_formularManager;
        private System.Drawing.Size m_size;

        public FrmVorlage()
        {
            InitializeComponent();
            this.m_size = new System.Drawing.Size();
            this.m_formularManager = ClsSingeltonFormularManager.CreateInstance();
        }

        public void ShowMenu()
        {
            this.m_formularManager.SetFormPrintScreen(this);
            this.m_formularManager.FormularShow("FrmMenu");
        }

        public virtual void SetUserRight()
        {
        }

        public virtual void SetLanguage()
        {
        }


        protected GroupBox CreateGroupBox(int left, int top, int width, int height, string text, Control parent)
        {
            GroupBox group_box = null;
            group_box = new System.Windows.Forms.GroupBox();
            group_box.SetBounds(left, top, width, height);
            group_box.Text = text;
            group_box.Name = this.m_formularManager.GetDynamicControlName("groupBox");
            parent.Controls.Add(group_box);
            return group_box;
        }

        protected CompMultiBar CreateMultiBar(int left, int top, int width, int height, Control parent)
        {
            CompMultiBar multi_bar = null;

            multi_bar = new CompMultiBar();
            multi_bar.AutoSize = false;
            multi_bar.SetBounds(left, top, width, height);
            multi_bar.Name = this.m_formularManager.GetDynamicControlName("compMultiBar");
            parent.Controls.Add(multi_bar);
            return multi_bar;
        }


        protected Label CreateLabel(int left, int top, int width, int height, string text, Control parent, System.Drawing.ContentAlignment Align=System.Drawing.ContentAlignment.MiddleRight)
        {
            Label label = null;

            label = new System.Windows.Forms.Label();
            label.AutoSize = false;
            label.Text = text;
            label.SetBounds(left, top, width, height);
            label.TextAlign = Align;
            label.Name = this.m_formularManager.GetDynamicControlName("label");
            parent.Controls.Add(label);
            return label;
        }

        protected CompTxtBox CreateTxtBox(int left, int top, string format, Control parent)
        {
            int width = 79;
            int height = 39;
            CompTxtBox txt_box = null;
            txt_box = new Helper.CompTxtBox();
            txt_box.SetBounds(left, top, width, height);
            txt_box.Format = format;
            txt_box.Name = this.m_formularManager.GetDynamicControlName("compTxtBox");
            parent.Controls.Add(txt_box);
            return txt_box;
        }

        protected CompInputBox CreateInputBox(int left, int top, string format, Control parent)
        {
            int width = 89;
            int height = 35;
            CompInputBox txt_box = null;
            txt_box = new Helper.CompInputBox();
            txt_box.SetBounds(left, top, width, height);
            txt_box.Format = format;
            txt_box.Name = this.m_formularManager.GetDynamicControlName("compInputBox");
            parent.Controls.Add(txt_box);
            return txt_box;
        }

        protected CompToggleSwitch CreateToggleSwitch(int left, int top, Control parent)
        {
            int width = 79;
            int height = 39;
            CompToggleSwitch comp_toggle_switch = null;
            comp_toggle_switch = new Helper.CompToggleSwitch();
            comp_toggle_switch.SetBounds(left, top, width, height);
            comp_toggle_switch.Name = this.m_formularManager.GetDynamicControlName("compToggleSwitch");
            parent.Controls.Add(comp_toggle_switch);
            return comp_toggle_switch;
        }


        protected CompLedRectangle CreateLedRectangle(int left, int top, Control parent)
        {
            int width = 79;
            int height = 39;
            CompLedRectangle comp_led_rectangle = null;
            comp_led_rectangle = new Helper.CompLedRectangle();
            comp_led_rectangle.SetBounds(left, top, width, height);
            comp_led_rectangle.Name = this.m_formularManager.GetDynamicControlName("compLedRectangle");
            parent.Controls.Add(comp_led_rectangle);
            return comp_led_rectangle;
        }

        protected CompLedRound CreateLedRound(int left, int top, Control parent, Helper.CompLedRound.LEDType ledType=CompLedRound.LEDType.Normal)
        {
            int width = 25;
            int height = 25;
            CompLedRound comp_led_round = null;
            comp_led_round = new Helper.CompLedRound();
            comp_led_round.SetBounds(left, top, width, height);
            comp_led_round.Type = ledType;
            comp_led_round.Name = this.m_formularManager.GetDynamicControlName("compLedRound");
            parent.Controls.Add(comp_led_round);
            return comp_led_round;
        }


        protected TabPage CreateTabPage(string text, Control parent)
        {
            TabPage tab_page = null;
            tab_page = new System.Windows.Forms.TabPage();
            tab_page.Text = text;
            tab_page.BackColor = System.Drawing.Color.Silver;
            tab_page.Name = this.m_formularManager.GetDynamicControlName("tabPage");
            parent.Controls.Add(tab_page);
            return tab_page;
        }

        protected TabControl CreateTabControl(int left, int top, int width, int height, Control parent)
        {
            TabControl tab_control = null;
            tab_control = new System.Windows.Forms.TabControl();
            tab_control.SetBounds(left, top, width, height);
            this.m_size.Width = tab_control.ItemSize.Width;
            this.m_size.Height = 39;
            tab_control.ItemSize = this.m_size;
            tab_control.DrawMode = TabDrawMode.OwnerDrawFixed;
            tab_control.DrawItem += new DrawItemEventHandler(this.TabControl_DrawItem);
            tab_control.Name = this.m_formularManager.GetDynamicControlName("tabControl");
            tab_control.SelectedIndex = 0;
            tab_control.TabIndex = 0;
            parent.Controls.Add(tab_control);
            return tab_control;
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }

        protected void TabControl_DrawItem(object sender, System.Windows.Forms.DrawItemEventArgs e)
        {
            TabControl obj = (TabControl)sender;
            Font Font;
            Brush backBrush;
            Brush foreBrush;
            if (e.Index == obj.SelectedIndex)
            {
                Font = new Font(e.Font, FontStyle.Bold | FontStyle.Bold);
                Font = new Font(e.Font, FontStyle.Bold);

                backBrush = new System.Drawing.SolidBrush(Color.DarkGray);
                foreBrush = Brushes.Black;
            }
            else
            {
                Font = e.Font;
                backBrush = new SolidBrush(e.BackColor);
                foreBrush = new SolidBrush(e.ForeColor);
            }
            string sTabName = obj.TabPages[e.Index].Text;
            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Center;
            sf.LineAlignment = StringAlignment.Center;

            e.Graphics.FillRectangle(new SolidBrush(Color.Silver), e.Bounds);
            Rectangle rect = e.Bounds;
            rect = new Rectangle(rect.X, rect.Y + 3, rect.Width, rect.Height - 3);
            e.Graphics.DrawString(sTabName, Font, foreBrush, rect, sf);
            sf.Dispose();
        } 

        private void FrmVorlage_Load(object sender, System.EventArgs e)
        {

        }
    }
}
