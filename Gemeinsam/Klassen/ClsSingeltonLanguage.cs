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

namespace Helper
{
    public class ClsSingeltonLanguage
    {
        public struct TableLanguage
        {
            public string NativeText;
            public string De;
            public string En;
            public string Fr;
            public string Sp;
            public string Ru;
        }

        public struct TableComponents
        {
            public Control Component;
            public string Propertie;
            public string De;
            public string En;
            public string Fr;
            public string Sp;
            public string Ru;
        }

        private static ClsSingeltonLanguage m_instance;

        private SQLiteCommand m_sqliteCommand;
        private SQLiteConnection m_sqliteConnection;
        private SQLiteDataReader m_sqliteDataReader;
        private Hashtable m_tableLanguage;
        private List<TableComponents> m_tableComponents;
        private List<Control> m_tableComponentIgnore;

        private ClsSingeltonParameter m_parameter;

        private ClsSingeltonLanguage()
        {
            this.m_parameter = ClsSingeltonParameter.CreateInstance();
            this.m_tableComponentIgnore = new List<Control>();
            this.m_tableComponents = new List<TableComponents>();
            this.m_tableLanguage = new Hashtable();

            this.m_sqliteConnection = new SQLiteConnection();
            this.m_sqliteConnection.ConnectionString = this.m_parameter.ConnectionString;
            this.m_sqliteConnection.Open();

            this.m_sqliteCommand = new SQLiteCommand(this.m_sqliteConnection);
            this.LoadLanguage();
        }

        public static ClsSingeltonLanguage CreateInstance()
        {
            if (m_instance == null)
            {
                m_instance = new ClsSingeltonLanguage();
            }
            return m_instance;
        }

        public static ClsSingeltonLanguage CreateInstance(Form formular)
        {
            if (m_instance == null)
            {
                m_instance = new ClsSingeltonLanguage();
            }
            m_instance.AddAllComponents(formular.Controls);
            return m_instance;
        }

        public void AddAllComponents(Form formular)
        {
            this.AddControl(formular);
            this.AddAllComponents(formular.Controls);
        }

        private void AddAllComponents(Control.ControlCollection controls)
        {
            foreach (Control control in controls) 
            {
                this.AddControl(control);
                if (control.Controls.Count>0) 
                { 
                    AddAllComponents(control.Controls); 
                }
            }
         }

        public void AddToIgnoreList(Control Ignore)
        {
            this.m_tableComponentIgnore.Add(Ignore);
        }

        public void SetLanguage()
        {
            string language = this.m_parameter.Language;
            string text="";
            foreach(TableComponents table_component in this.m_tableComponents)
            {
                if (language == "De")
                    text = table_component.De;
                if (language == "En")
                    text = table_component.En;
                if (language == "Fr")
                    text = table_component.Fr;
                if (language == "Sp")
                    text = table_component.Sp;
                if (language == "Ru")
                    text = table_component.Ru;

                this.SetObjectProperty(table_component.Propertie,text ,table_component.Component);
            }

        }

        private void SetObjectProperty(string propertyName, string value, object obj)
        {
            PropertyInfo propertyInfo = obj.GetType().GetProperty(propertyName);
            if (propertyInfo != null)
            {
                propertyInfo.SetValue(obj, value, null);
            }
        }

        private void AddControl(Control controlToTranslate) 
        {
            if(!this.m_tableComponentIgnore.Contains(controlToTranslate))
            {
                if (controlToTranslate is Form || controlToTranslate is Label || controlToTranslate is GroupBox)
                {
                    string native_text = controlToTranslate.Text;
                    this.AddControl(controlToTranslate, "Text", native_text);
                }
            }
        }

        private void AddControl(Control control, string propertie, string nativeText)
        {
            TableComponents table_component;
            TableLanguage table_language;

            if (this.m_tableLanguage.ContainsKey(nativeText) == true)
            {
                table_language = (TableLanguage)this.m_tableLanguage[nativeText];
                table_component.Component = control;
                table_component.Propertie = propertie;
                table_component.De = table_language.De;
                table_component.En = table_language.En;
                table_component.Fr = table_language.Fr;
                table_component.Sp = table_language.Sp;
                table_component.Ru = table_language.Ru;
                this.m_tableComponents.Add(table_component);
            }
        }
        public string GetTranslation(string nativeText)
        {
            string retval = nativeText;
            if (this.m_tableLanguage.ContainsKey(nativeText))
            {
                TableLanguage table_labnguage;
                table_labnguage = (TableLanguage)this.m_tableLanguage[nativeText];
                if (this.m_parameter.Language == "De")
                    retval = table_labnguage.De;
                if (this.m_parameter.Language == "En")
                    retval = table_labnguage.En;
                if (this.m_parameter.Language == "Fr")
                    retval = table_labnguage.Fr;
                if (this.m_parameter.Language == "Sp")
                    retval = table_labnguage.Sp;
                if (this.m_parameter.Language == "Ru")
                    retval = table_labnguage.Ru;
            }
            return retval;
        }

        private void AddLanguage(String native, string de ="", string en = "", string fr = "", string sp = "", string ru = "")
        {
            if (!this.m_tableLanguage.ContainsKey(native))
            {
                TableLanguage addTable;
                addTable.NativeText = native.Replace("[nl]", "\r\n");
                addTable.De = de.Replace("[nl]", "\r\n");
                addTable.En = en.Replace("[nl]", "\r\n");
                addTable.Fr = fr.Replace("[nl]", "\r\n");
                addTable.Sp = sp.Replace("[nl]", "\r\n");
                addTable.Ru = ru.Replace("[nl]", "\r\n");

                if(addTable.En=="")
                {
                    addTable.En = native;
                }
                if (addTable.Fr == "")
                {
                    addTable.Fr = native;
                }
                if (addTable.Sp == "")
                {
                    addTable.Sp = native;
                }
                if (addTable.De == "")
                {
                    addTable.De = native;
                }
                if (addTable.Ru == "")
                {
                    addTable.Ru = native;
                }
                this.m_tableLanguage.Add(addTable.NativeText, addTable);
            }
        }

        private void LoadLanguage()
        {
            this.m_sqliteCommand.CommandText = "Select * from language";

            if(this.m_sqliteDataReader!=null)
            {
                this.m_sqliteDataReader.Close();
                this.m_sqliteDataReader=null;
            }
            this.m_sqliteDataReader = this.m_sqliteCommand.ExecuteReader();
            while (this.m_sqliteDataReader.Read())
            {
                this.AddLanguage(this.m_sqliteDataReader.GetValue(1).ToString(), this.m_sqliteDataReader.GetValue(2).ToString(), this.m_sqliteDataReader.GetValue(3).ToString(), this.m_sqliteDataReader.GetValue(4).ToString(), this.m_sqliteDataReader.GetValue(5).ToString(), this.m_sqliteDataReader.GetValue(6).ToString());
            }
            this.m_sqliteDataReader.Close();
            this.m_sqliteDataReader = null;
        }
    }
}
