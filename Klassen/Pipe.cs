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

    public enum PipeState
    {
        FlowOff=0,
        FlowOn
    }

    public class Pipe : PictureBox
    {
        public Pipe()
        {
            this.Height = 10;
            this.Width = 100;
            this.Flow = PipeState.FlowOff;
        }
        [Category("Default"), Description("")]

        private PipeState m_Flow;
        public PipeState Flow
        {
            set { if (m_Flow != value) { m_Flow = value; this.Invalidate(); } }
            get { return this.m_Flow; }
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
            if(this.m_Flow==PipeState.FlowOff)
            {
                this.BackColor=Color.Blue;
            }
            if(this.m_Flow==PipeState.FlowOn)
            {
                this.BackColor=Color.Aqua;
            }
        }
    }

}