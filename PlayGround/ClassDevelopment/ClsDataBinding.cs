﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClassDevelopment
{
    public class ClsDataBinding
    {

        public struct DataBindingList
        {
            public object Form;
            public string Component;
            public string Propertie;
            public string VarName;
        };

        private ClsVarCollect Glb_VarCollect;

        private Hashtable m_Variables;
        private Hashtable m_VariablesNo;
        private Hashtable m_Components;


        private static ClsDataBinding instance;

        private ClsDataBinding()
        {
            this.m_Variables = new Hashtable();
            this.m_VariablesNo = new Hashtable();
            this.m_Components = new Hashtable();
            Glb_VarCollect = ClsVarCollect.CreateInstance();

        }
        public static ClsDataBinding CreateInstance()
        {
            if (instance == null)
            {
                instance = new ClsDataBinding();
            }
            return instance;
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar==13)
            {
                if (this.m_Components[sender]!=null)
                {
                    string var_name = (string)this.m_Components[sender];
                    string obj_type=sender.GetType().ToString();
                    if (obj_type == "System.Windows.Forms.TextBox")
                    {
                        TextBox obj_text_box;
                        obj_text_box = (TextBox)sender;
                        Glb_VarCollect.WriteValue(var_name, obj_text_box.Text,true);

                        this.Dispatch(var_name,true);
                        obj_text_box.SelectAll();
                    }
                }
            }
        }


        public void AddList(object Form, string Component, string Propertie, string VarName)
        {
            DataBindingList data;
            data.Form = Form;
            data.Component = Component;
            data.Propertie = Propertie;
            data.VarName = VarName;
            string key = "";

            if (this.m_VariablesNo[VarName] == null)
            {
                int i = 0;
                key = VarName + "_" + i.ToString();
                i++;
                this.m_VariablesNo.Add(VarName, i);
            }
            else
            {
                int no = (int)this.m_VariablesNo[VarName];
                key = VarName + "_" + no.ToString();
                no++;
                this.m_VariablesNo[VarName] = no;

            }
            this.m_Variables.Add(key, data);


            Form frm = (Form)Form;
            string frm_name = frm.Name;
            string cntrl_name = data.Component.ToString();

            object obj = this.GetControlByName(frm, cntrl_name);
            this.m_Components.Add(obj, VarName);
            if(obj!=null)
            {
                string obj_type = obj.GetType().ToString();
                if (obj_type == "System.Windows.Forms.TextBox")
                {
                    TextBox obj_text_box;
                    obj_text_box = (TextBox)obj;
                    obj_text_box.KeyPress+= new System.Windows.Forms.KeyPressEventHandler(this.textBox1_KeyPress);
                }
            }
        }

        public void Dispatch(string VarName, bool Override=false)
        {
            if (this.m_VariablesNo[VarName] != null)
            {

                int no = (int)this.m_VariablesNo[VarName];

                for (int i = 0; i < no; i++)
                {
                    string key = VarName + "_" + i.ToString();

                    if (this.m_Variables[key] != null)
                    {
                        DataBindingList data = (DataBindingList)this.m_Variables[key];

                        Form frm_show;
                        Form frm = (Form)data.Form;

                        string frm_name = frm.Name;
                        string cntrl_name = data.Component.ToString();

                        frm_show = Application.OpenForms[frm_name];
                        if (frm_show != null)
                        {
                            object obj = this.GetControlByName(frm_show, cntrl_name);
                            if (obj != null)
                            {
                                string obj_type = obj.GetType().ToString();
                                if (obj_type == "System.Windows.Forms.TextBox")
                                {
                                    TextBox obj_text_box;
                                    obj_text_box = (TextBox)obj;
                                    if (data.Propertie == "Text")
                                    {
                                        if (!obj_text_box.Focused || Override)
                                        {
                                            obj_text_box.Text = Glb_VarCollect.ReadValueString(data.VarName);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        public Control GetControlByName(Control container, string name)
        {
            foreach (Control c in container.Controls)
            {
                if (c.Name == name)
                {
                    return c;
                }
                if (c.HasChildren)
                {
                    Control control = GetControlByName(c, name);
                    if (control != null)
                        return control;
                }
            }
            return null;
        }
    }
}