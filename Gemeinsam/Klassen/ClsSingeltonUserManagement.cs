using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;
using System.Data;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Xml;
using System.IO;
using System.ComponentModel;
using System.Drawing;
using Helper;

namespace Helper
{
    public class ClsSingeltonUserManagement
    {
        public enum UserRight { None = 0x00, Logoff = 0x01, Operator = 0x02, Setter = 0x04, Service = 0x08 ,AllUser = 0x0F}

        public struct UserEnableVisible
        {
            public UserRight Enable;
            public UserRight Visible;
        }

        private System.Windows.Forms.Timer m_timerLogin;
        private ClsSingeltonFormularManager m_formularManager;
        private static ClsSingeltonUserManagement m_instance;
        private Hashtable m_tableComponents;
        private ClsSingeltonParameter m_parameter;
        private int m_LoginTime;
        private int m_maxTimeLogin;

        private ClsSingeltonUserManagement()
        {
            this.m_formularManager = ClsSingeltonFormularManager.CreateInstance();
            this.m_parameter = ClsSingeltonParameter.CreateInstance();
            this.m_tableComponents = new Hashtable();
            this.m_timerLogin = new Timer();
            this.m_timerLogin.Interval = 1000;
            this.m_timerLogin.Tick += this.TimerLogin_Tick;
            this.m_timerLogin.Enabled = false;
            this.m_LoginTime = 0;
            this.m_maxTimeLogin = Convert.ToInt32(this.m_parameter.MaxTimeLogin);
        }

        private void TimerLogin_Tick(object sender, EventArgs e)
        {
            this.m_LoginTime++;
            if(this.m_LoginTime>this.m_maxTimeLogin)
            {
                this.m_LoginTime = 0;
                this.m_timerLogin.Enabled = false;
                this.m_parameter.ActualUser = "0";
                this.m_parameter.PasswordOk = false;
                FrmMenu frm = (FrmMenu)this.m_formularManager.GetFormular("FrmMenu");
                frm.SetBtnUser();
                this.SetUserRight();
            }
        }

        public static ClsSingeltonUserManagement CreateInstance()
        {
            if (m_instance == null)
            {
                m_instance = new ClsSingeltonUserManagement();
            }
            return m_instance;
        }

        public static ClsSingeltonUserManagement CreateInstance(Form formular)
        {
            if (m_instance == null)
            {
                m_instance = new ClsSingeltonUserManagement();
            }
            m_instance.AddAllComponents(formular.Controls);
            return m_instance;
        }

        private void AddAllComponents(Control.ControlCollection controls)
        {
            foreach (Control control in controls)
            {
                this.AddControl(control);
                if (control.Controls.Count > 0)
                {
                    AddAllComponents(control.Controls);
                }
            }
        }

        public void StartTimer()
        {
            this.m_timerLogin.Enabled = true;
        }

        public void ResetEnable(Control component, UserRight userRight, bool SetUserRight = false)
        {
            if (this.m_tableComponents.ContainsKey(component))
            {
                UserEnableVisible enable_visible = (UserEnableVisible)this.m_tableComponents[component];
                enable_visible.Enable &= ~userRight;
                this.m_tableComponents[component] = enable_visible;
            }
            if (SetUserRight)
            {
                this.SetUserRight();
            }
        }


        public void SetEnable(Control component,UserRight userRight,bool SetUserRight=false)
        {
            if(this.m_tableComponents.ContainsKey(component))
            {
                UserEnableVisible enable_visible = (UserEnableVisible)this.m_tableComponents[component];
                enable_visible.Enable |= userRight;
                this.m_tableComponents[component] = enable_visible;
            }
            if(SetUserRight)
            {
                this.SetUserRight();
            }
        }

        public void SetUserRight()
        {
            UserRight aktual_user = (UserRight)Math.Pow(2, Convert.ToInt32(this.m_parameter.ActualUser));
            foreach (DictionaryEntry pair in this.m_tableComponents)
            {
                Control component = (Control)pair.Key;
                UserEnableVisible enable_visible = (UserEnableVisible)pair.Value;
                component.Enabled = Convert.ToBoolean((enable_visible.Enable & aktual_user));
                //component.Visible = Convert.ToBoolean((enable_visible.Visible & aktual_user));
            }
        }

        private void AddControl(Control component)
        {
            UserEnableVisible enable_visible;
            enable_visible.Enable = UserRight.AllUser;
            enable_visible.Visible = UserRight.AllUser;
            string component_type=component.GetType().ToString();

            if (component_type =="Helper.CompBitButton")
            {
                enable_visible.Enable = UserRight.Logoff | UserRight.Operator | UserRight.Setter | UserRight.Service;
                enable_visible.Visible = UserRight.Logoff | UserRight.Operator | UserRight.Setter | UserRight.Service;
                if (!this.m_tableComponents.ContainsKey(component))
                {
                    this.m_tableComponents.Add(component, enable_visible);
                }
            }
            if (component_type == "System.Windows.Forms.TextBox")
            {
                enable_visible.Enable = UserRight.None | UserRight.Operator | UserRight.Setter | UserRight.Service;
                enable_visible.Visible = UserRight.Logoff | UserRight.Operator | UserRight.Setter | UserRight.Service;
                if (!this.m_tableComponents.ContainsKey(component))
                {
                    this.m_tableComponents.Add(component, enable_visible);
                }
            }
        }
    }
}
