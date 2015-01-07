using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;


namespace Helper
{
    public class ClsCreateParameter
    {
        private Label m_Text;
        private CompToggleSwitch m_ToogleSwitch;
        private CompLedRectangle m_LedRectangle;
        private Control m_Komponente;
        private int m_Zeile;
        private PLCDatas m_PlcData;

        public ClsCreateParameter(Control Komponente,int Zeile,PLCDatas PlcData)
        {
            this.m_Komponente = Komponente;
            this.m_Zeile = Zeile;
            this.m_PlcData = PlcData;
        }
        ~ClsCreateParameter()
        {
            this.m_Text.Dispose();
            this.m_ToogleSwitch.Dispose();
            this.m_LedRectangle.Dispose();
        }
        public void Dispose()
        {
            this.m_Text.Dispose();
            this.m_ToogleSwitch.Dispose();
            this.m_LedRectangle.Dispose();
        }
        public void CreateParameter()
        {
            int abstandX = 5;
            int abstandY = 5;

            Form frm = new Form();

            int breite = this.m_Komponente.ClientRectangle.Width;
            this.m_ToogleSwitch = new CompToggleSwitch();
            this.m_LedRectangle = new CompLedRectangle();

            int hoeheZeile=this.m_LedRectangle.Height;

            int oben = this.m_Zeile * (hoeheZeile + abstandY) + abstandY;


            this.m_Text = new Label();
            this.m_Text.Text=this.m_PlcData.Comment;
            this.m_Text.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.m_Text.Height = hoeheZeile;
            this.m_Text.BackColor = Color.Red;
            this.m_Text.AutoSize = false;
            this.m_Text.Top = oben;
            this.m_Text.Width = breite - 4 * abstandX - this.m_ToogleSwitch.Width - this.m_LedRectangle.Width;
            this.m_Text.Left = abstandX;

            this.m_Komponente.Controls.Add(this.m_Text);

            this.m_ToogleSwitch.Left = this.m_Text.Left + this.m_Text.Width + abstandX;
            this.m_ToogleSwitch.Top = oben;
            this.m_Komponente.Controls.Add(this.m_ToogleSwitch);

            this.m_LedRectangle.Left = this.m_ToogleSwitch.Left + this.m_ToogleSwitch.Width + abstandX;
            this.m_LedRectangle.Top = oben;
            this.m_Komponente.Controls.Add(this.m_LedRectangle);
        }
    }
}
