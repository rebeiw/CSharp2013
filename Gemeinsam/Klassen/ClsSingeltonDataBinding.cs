using System;
using System.Collections;
using System.Windows.Forms;

namespace Helper
{
    public class ClsSingeltonDataBinding
    {

        public struct ClsSingeltonDataBindingList
        {
            public object Form;
            public string Component;
            public string Propertie;
            public string VarName;
        };

        private static ClsSingeltonDataBinding m_instance;

        private ClsSingeltonVariablesCollecter m_VariablesCollecter;

        private Hashtable m_Variables;
        private Hashtable m_VariablesNo;
        private Hashtable m_Components;

        private ClsSingeltonDataBinding()
        {
            this.m_Variables = new Hashtable();
            this.m_VariablesNo = new Hashtable();
            this.m_Components = new Hashtable();
            m_VariablesCollecter = ClsSingeltonVariablesCollecter.CreateInstance();
        }
 
        public static ClsSingeltonDataBinding CreateInstance()
        {
            if (m_instance == null)
            {
                m_instance = new ClsSingeltonDataBinding();
            }
            return m_instance;
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
                        m_VariablesCollecter.WriteValue(var_name, obj_text_box.Text, true);

                        this.Dispatch(var_name, true);
                        obj_text_box.SelectAll();
                    }
                }
            }
        }
        public void AddList(object Form, string Component, string Propertie, string VarName)
        {
            ClsSingeltonDataBindingList data;
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
                        ClsSingeltonDataBindingList data = (ClsSingeltonDataBindingList)this.m_Variables[key];

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
                                            obj_text_box.Text = m_VariablesCollecter.ReadValueString(data.VarName);
                                        }
                                    }
                                }

                                if (obj_type == "Helper.CompTxtBox")
                                {
                                    CompTxtBox obj_text_box;
                                    obj_text_box = (CompTxtBox)obj;
                                    if (data.Propertie == "Text")
                                    {
                                        if (!obj_text_box.Focused || Override)
                                        {
                                            obj_text_box.Text = m_VariablesCollecter.ReadValueString(data.VarName);
                                        }
                                    }
                                }
                                
                                if (obj_type == "Helper.CompVentil")
                                {
                                    CompVentil obj_ventil;
                                    obj_ventil = (CompVentil)obj;
                                    if (data.Propertie == "State")
                                    {
                                        int convert_value=0;
                                        string val = m_VariablesCollecter.ReadValueString(data.VarName);
                                        convert_value=Convert.ToInt32(val);
                                        CompVentil.CompVentilState state = (CompVentil.CompVentilState)convert_value;
                                        obj_ventil.State = state;
                                    }
                                }

                                if (obj_type == "Helper.CompPipe")
                                {
                                    CompPipe obj_comp;
                                    obj_comp = (CompPipe)obj;
                                    if (data.Propertie == "Flow")
                                    {
                                        int convert_value = 0;
                                        string val = m_VariablesCollecter.ReadValueString(data.VarName);
                                        convert_value = Convert.ToInt32(val);
                                        CompPipe.CompPipeFlow flow = (CompPipe.CompPipeFlow)convert_value;
                                        obj_comp.Flow = flow;
                                    }
                                }
                                
                                if (obj_type == "Helper.CompLedRectangle")
                                {
                                    CompLedRectangle obj_comp;
                                    obj_comp = (CompLedRectangle)obj;
                                    if (data.Propertie == "State")
                                    {
                                        int convert_value = 0;
                                        string val = m_VariablesCollecter.ReadValueString(data.VarName);
                                        convert_value = Convert.ToInt32(val);
                                        CompLedRectangle.CompLedState state = (CompLedRectangle.CompLedState)convert_value;
                                        obj_comp.State = state;
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
