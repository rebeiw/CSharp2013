using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Helper
{

    public enum CompPipeState
    {
        FlowOff=0,
        FlowOn
    }

    public class CompPipe : PictureBox
    {
        public CompPipe()
        {
            this.Height = 10;
            this.Width = 100;
            this.ColorFlowOn = Color.Aqua;
            this.ColorFlowOff = Color.Blue;
            this.Flow = CompPipeState.FlowOff;
        }
        [Category("Default"), Description("")]

        private Color m_ColorFlowOn;
        public Color ColorFlowOn
        {
            set { if (m_ColorFlowOn != value) { m_ColorFlowOn = value; this.Invalidate(); } }
            get { return this.m_ColorFlowOn; }
        }
        private Color m_ColorFlowOff;
        public Color ColorFlowOff
        {
            set { if (m_ColorFlowOff != value) { m_ColorFlowOff = value; this.Invalidate(); } }
            get { return this.m_ColorFlowOff; }
        }

        [Category("Default"), Description("")]

        private CompPipeState m_Flow;
        public CompPipeState Flow
        {
            set { if (m_Flow != value) { m_Flow = value; this.Invalidate(); } }
            get { return this.m_Flow; }
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
            if(this.m_Flow==CompPipeState.FlowOff)
            {
                this.BackColor=this.m_ColorFlowOff;
            }
            if(this.m_Flow==CompPipeState.FlowOn)
            {
                this.BackColor = this.m_ColorFlowOn;
            }
        }
    }

}