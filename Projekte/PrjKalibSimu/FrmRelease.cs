using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Helper;
using System.Linq;
using System.Threading.Tasks;
using System.Collections;
using System.Xml;
using System.IO;

namespace PrjKalibSimu
{
    public partial class FrmRelease : Helper.FrmVorlageMenu
    {
        private ClsFormularManager m_glb_FormularManager;
        private ClsPLC m_glb_PLC;
        private ClsDataBinding m_glb_DataBinding;

        private TabControl m_TabControlDaten;
        private Dictionary<string, string> m_Releases;
        public FrmRelease()
        {

            InitializeComponent();
            this.Width = 1024;
            this.Height = 768;
            this.Text = "Freigaben";
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            m_Releases = new Dictionary<string, string>();
            m_glb_FormularManager = ClsFormularManager.CreateInstance();
            m_glb_FormularManager.FormularAdd(this, this.Name.ToString());
            m_glb_PLC = ClsPLC.CreateInstance();
            m_glb_DataBinding = ClsDataBinding.CreateInstance();

            this.CreateTabControl();
            this.CreatePage();
            this.CreateSymbols();

        }

        private void CreateSymbols()
        {
            int pageNr = 0;
            string symbolName = "";
            string groupName = "";
            string comment = "";
            string groupNameBefore = "";
            int row = 0;
            GroupBox groupBoxBefore = null;
            GroupBox groupBox = null;
            foreach (PLCDatas data in m_glb_PLC.Daten)
            {
                if (data.DatabaseNumber == 54)
                {
                    m_Releases.Add(data.Symbolname, data.Comment);
                }
            }
            List<string> sortList = m_Releases.Keys.ToList();
            sortList.Sort();
            groupNameBefore = "";
            int topGroupBox = 0;
            int colomnGroupBox = 0;

            int tabPagegheight = 0;
            int rest = 0;

            foreach (string symbol in sortList)
            {
                symbolName = symbol;
                comment = m_Releases[symbolName];
                string[] kom = comment.Split(':');
                comment = kom[1];
                groupName = kom[0];
                if (groupNameBefore != groupName)
                {
                    row = 0;
                    if (groupBoxBefore == null)
                    {
                        topGroupBox = GlobalVar.Const_ControlSpacing;
                        colomnGroupBox = 0;
                    }
                    else
                    {
                        tabPagegheight = this.m_TabControlDaten.Height - this.m_TabControlDaten.ItemSize.Height - 8;
                        rest = tabPagegheight - groupBoxBefore.Top - groupBoxBefore.Height - GlobalVar.Const_GroupBoxOffsetRest;
                        if (rest < 0)
                        {
                            topGroupBox = GlobalVar.Const_ControlSpacing;
                            colomnGroupBox++;
                            if (colomnGroupBox > 1)
                            {
                                colomnGroupBox = 0;
                                groupBoxBefore = null;
                                row = 0;
                                this.CreatePage();
                                pageNr++;
                            }
                        }
                        else
                        {
                            topGroupBox = groupBoxBefore.Top + groupBoxBefore.Height + GlobalVar.Const_ControlSpacing;
                        }
                    }
                    groupBox = this.CreateGB(pageNr);
                    groupBox.Top = topGroupBox;
                    groupBox.Left = colomnGroupBox * (GlobalVar.Const_GroupBoxWidth + GlobalVar.Const_ControlSpacing) + GlobalVar.Const_ControlSpacing;
                    groupBox.Width = GlobalVar.Const_GroupBoxWidth;
                    groupBox.Text = groupName;
                    groupNameBefore = groupName;
                    groupBoxBefore = groupBox;
                }

                tabPagegheight = this.m_TabControlDaten.Height - this.m_TabControlDaten.ItemSize.Height - 8;
                rest = tabPagegheight - groupBox.Top - groupBox.Height - GlobalVar.Const_GroupBoxOffsetRest; ;
                if (rest < 0)
                {
                    colomnGroupBox++;
                    if (colomnGroupBox > 1)
                    {
                        colomnGroupBox = 0;
                        groupBoxBefore = null;
                        row = 0;
                        this.CreatePage();
                        pageNr++;
                    }
                    row = 0;

                    topGroupBox = GlobalVar.Const_ControlSpacing;
                    groupBox = this.CreateGB(pageNr);
                    groupBox.Top = topGroupBox;
                    groupBox.Left = colomnGroupBox * (GlobalVar.Const_GroupBoxWidth + GlobalVar.Const_ControlSpacing) + GlobalVar.Const_ControlSpacing;
                    groupBox.Width = GlobalVar.Const_GroupBoxWidth;
                    groupBox.Text = groupName;
                    groupNameBefore = groupName;
                    groupBoxBefore = groupBox;

                }


                int ledTop = row * (GlobalVar.Const_LedHeigt + GlobalVar.Const_LedSpacing) + GlobalVar.Const_GroupBoxOffset;
                LedRound led = this.CreateLED(groupBox);
                led.Type = LEDType.Release;
                led.Name = FuncGeneral.GetControlName(led);
                led.Top = ledTop;
                led.Left = GlobalVar.Const_ControlSpacing;
                m_glb_DataBinding.AddList(this.Name, led.Name.ToString(), "State", "DB54." + symbolName);
                row++;
                groupBox.Height = row * (GlobalVar.Const_LedHeigt + GlobalVar.Const_LedSpacing) + GlobalVar.Const_GroupBoxOffset + GlobalVar.Const_LedSpacing;

                Label lbl = this.CreateLBL(groupBox);
                lbl.Top = led.Top;
                lbl.Height = led.Height;
                lbl.Left = led.Left + led.Width + GlobalVar.Const_ControlSpacing;
                lbl.Width = GlobalVar.Const_GroupBoxWidth - lbl.Left - GlobalVar.Const_ControlSpacing;
                lbl.Text = comment;
            }
        }

        private void CreateTabControl()
        {
            int tabControlWidth = this.GB_Datenausgabe.Width - (2 * GlobalVar.Const_ControlSpacing + 1);     // 2 *  Abstand + 1 pixel
            int tabControlHeight = this.GB_Datenausgabe.Height - 25;            // Top + Abstand
            this.m_TabControlDaten = new TabControl();
            this.m_TabControlDaten.Top = 20;
            this.m_TabControlDaten.Left = GlobalVar.Const_ControlSpacing;
            this.m_TabControlDaten.Width = tabControlWidth;
            this.m_TabControlDaten.Height = tabControlHeight;
            this.m_TabControlDaten.ItemSize = new System.Drawing.Size(0, 39);
            this.m_TabControlDaten.Name = FuncGeneral.GetControlName(this.m_TabControlDaten);
            this.GB_Datenausgabe.Controls.Add(this.m_TabControlDaten);
        }

        private void CreatePage()
        {
            int pageNumber = this.m_TabControlDaten.TabPages.Count;
            string textPage = "Seite " + (pageNumber + 1);
            this.m_TabControlDaten.TabPages.Add(textPage);
            this.m_TabControlDaten.TabPages[pageNumber].Name = FuncGeneral.GetControlName(this.m_TabControlDaten.TabPages[pageNumber]);
            this.m_TabControlDaten.TabPages[pageNumber].BackColor = Color.Silver;
        }

        private GroupBox CreateGB(int PageNr)
        {
            GroupBox gb = new GroupBox();
            gb.Name = FuncGeneral.GetControlName(gb);
            this.m_TabControlDaten.TabPages[PageNr].Controls.Add(gb);
            return gb;
        }

        private Label CreateLBL(Control c)
        {
            Label lbl = new Label();
            lbl.Name = FuncGeneral.GetControlName(lbl);
            lbl.AutoSize = false;
            lbl.Text = lbl.Name;
            lbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            c.Controls.Add(lbl);

            return lbl;
        }

        private LedRound CreateLED(Control c)
        {
            LedRound led = new LedRound();
            led.Name = FuncGeneral.GetControlName(led);
            c.Controls.Add(led);
            return led;
        }

        private void FrmRelease_Load(object sender, EventArgs e)
        {

        }

    }
}
