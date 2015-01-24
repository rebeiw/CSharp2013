using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AutoIt;
using AutoIt.Properties;
using AutoItX3Lib;

namespace AutoIt
{
    public partial class Form1 : Form
    {
        private AutoItX3Class m_AutoIt;
        public Form1()
        {
            InitializeComponent();
            m_AutoIt = new AutoItX3Class();
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string window_class = "[CLASS:SimViewApp]";
            string prog_name = "C:\\Program Files\\SIEMENS\\Plcsim\\s7wsi\\s7wsvapx.exe";
            string title = "S7-PLCSIM";
            string file_name = @"e:\DropBox\Dropbox\Repos\CSharp2013\PlayGround\ClassDevelopment\plcSimi";
            this.Run(prog_name,window_class,title);
            if(this.CheckTitle(window_class,"plcSimi")==false)
            {
                this.m_AutoIt.WinActivate(window_class);
                window_class = "Öffnen";
                this.m_AutoIt.Send("!sf");
                this.m_AutoIt.WinWait(window_class);
                this.m_AutoIt.ControlSetText(window_class, "", "Edit1", file_name);
                this.m_AutoIt.ControlClick(window_class, "", "Button2");
            }
            window_class = "[CLASS:SimViewApp]";
            string text_status=this.m_AutoIt.StatusbarGetText(window_class);
            //CPU/CP:  MPI=2 DP=2 IP=192.168.2.118
            this.m_AutoIt.WinSetState(window_class, "", this.m_AutoIt.SW_MINIMIZE);
            this.Close();

        }
        private bool CheckTitle(string windowClass,string title)
        {
            bool retval = false;
            string prog_title = this.m_AutoIt.WinGetTitle(windowClass, "");
            int pos = prog_title.IndexOf(title);
            if(pos<0)
            {
                retval = false;
            }
            else
            {
                retval = true;
            }
            return retval;
        }

        private void Run(string progName,string windowClass,string title)
        {
            int is_running = 0;
            string prog_title="";
            is_running=this.m_AutoIt.WinExists(windowClass, "");
            if (is_running==0)
            {
                this.m_AutoIt.Run(progName);
                do
                {
                    is_running = this.m_AutoIt.WinExists(windowClass, "");
                } while (is_running==0);

                do
                {
                    prog_title=this.m_AutoIt.WinGetTitle(windowClass, "");
                }while (prog_title.IndexOf(title)<0);
            }
        }
    }
}
