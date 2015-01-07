using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Windows.Forms;
using System.Xml;
using System.IO;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using Helper;


namespace Helper
{

    /** 
    * \file ClsSingeltonFormularManager.cs
    * 
    * \brief  
    *
    **/

    /** 
    * \class ClsSingeltonFormularManager 
    * 
    * \brief Dient der Formularverwaltung. 
    * 
    * Angemeldete Formulare koennen geoeffnet oder geschlossen werden.
    * 
    */

    public class ClsSingeltonFormularManager
    {
        
        private static ClsSingeltonFormularManager m_instance;//!<Instanz auf die Klasse

        public Form m_printScreen;//!<Objekt auf das Formular zum Ausdrucken

        private Hashtable m_formulars;//!<Liste fuer die angemeldeten Formulare

        private int m_dynamicControlId;




        /** 
         * \brief Konstruktor
         * 
         * Funktionsbeschreibung: Erzeugen der Formularliste 
         */
        private ClsSingeltonFormularManager()
        {
            this.m_formulars = new Hashtable();
            this.m_dynamicControlId = 0;
        }

        /** 
         * \brief Destruktor
         * 
         * Funktionsbeschreibung: Zerstoeren der Instanz  
         */
        public static void Destroy()
        {
            if (m_instance!=null)
            {
                m_instance=null;
            }
        }

        public string GetDynamicControlName(string controlType)
        {
            string retval = controlType + this.m_dynamicControlId.ToString();
            this.m_dynamicControlId++;
            return retval;
        }


        /** 
         * \brief Instanz erzeugen
         * 
         * Funktionsbeschreibung: Erzeugen einer einmaligen Instanz  
         */
        public static ClsSingeltonFormularManager CreateInstance()
        {
            if (m_instance == null)
            {
                m_instance = new ClsSingeltonFormularManager();
            }
            return m_instance;
        }

        /** 
         * \brief Instanz erzeugen
         * 
         * Funktionsbeschreibung: Erzeugen einer einmaligen Instanz und mit Formularanmeldung  
         */
        public static ClsSingeltonFormularManager CreateInstance(Control form, string formularName)
        {
            if (m_instance == null)
            {
                m_instance = new ClsSingeltonFormularManager();
            }
            m_instance.FormularAdd(form, formularName);
            return m_instance;
        }


        /** 
         * \brief Setzen des Formulars zum Ausdrucken
         * 
         * \param   formularName    Formular zum Ausdrucken    
         *
         * Funktionsbeschreibung: 
         */
        public void SetFormPrintScreen(Form formularName)
        {
            this.m_printScreen = formularName;
        }

        /** 
         * \brief Formular der Liste hinzufuegen
         * 
         * Funktionsbeschreibung: Es werden nur nicht vorhandene Formulare der Liste hinzugefuegt
         */
        public void FormularAdd(Control form, string formularName)
        {
            if (m_formulars[formularName] == null)
            {
                if (!m_formulars.Contains(formularName))
                {
                    m_formulars.Add(formularName, form);
                }
            }
        }

        /** 
         * \brief Formular aus der Liste anzeigen
         * 
         * \param   formularName    Formular zum Anzeigen    
         * 
         * Funktionsbeschreibung: Anzeigen des gewuenschten Formulars. Die Formulare "Menu" und "Language" wird die Formularposition der aktuellen Cursorposition gleich gesetzt.
         * Das geoeffnete Formular wird im in den Vordergrund gesetzt.
         */
        public void FormularShow(string formularName)
        {
            if (this.m_formulars[formularName] != null)
            {
                Form frm = (Form)this.m_formulars[formularName];
                if (formularName == "FrmMenu")
                {
                    frm.Left = Cursor.Position.X - frm.Width;
                    frm.Top = Cursor.Position.Y;
                }
                if (formularName == "FrmLanguage")
                {
                    Form frm_menu = (Form)this.m_formulars["FrmMenu"];
                    frm.Left = Cursor.Position.X - frm.Width;
                    frm.Top = frm_menu.Top;
                }
                frm.Show();
                frm.BringToFront();
            }
        }

        public Control GetFormular(string formularName)
        {
            Control retval = null;
            if (this.m_formulars[formularName] != null)
            {
                retval = (Control)this.m_formulars[formularName];
            }
            return retval;
        }


        /** 
         * \brief Alle Formulare schliessen
         */
        public void FormularCloseAll()
        {
            foreach (DictionaryEntry pair in this.m_formulars)
            {
               Form frm = (Form)pair.Value;
               frm.Hide();
            }
        }

        /** 
         * \brief Benutzerrechte fuer alle angemeldeten Formulare setzen
         */
        public void SetUserRight()
        {
            foreach (DictionaryEntry pair in this.m_formulars)
            {
                FrmVorlage frm = (FrmVorlage)pair.Value;
                frm.SetUserRight();
            }
        }

        /** 
         * \brief Sprache fuer alle angemeldeten Formulare setzen
         */
        public void SetLanguage()
        {
            foreach (DictionaryEntry pair in this.m_formulars)
            {
                FrmVorlage frm = (FrmVorlage)pair.Value;
                frm.SetLanguage();
            }
        }

        /** 
         * \brief Dem Formular Menu der Button "Alle Formulare schliessen" hinzufuegen
         */
        public void AddBitButtonCloseAll()
        {
            FrmMenu frm = (FrmMenu)this.m_formulars["FrmMenu"];
            frm.AddBitButtonCloseAll();
        }

        /** 
         * \brief Dem Formular Menu einen Button zum Oeffnen des gewuenschten Formulars hinzufuegen
         * 
         * \param   buttonStyle    Gewuenschter Buttontype    
         * \param   formularName   Formular zum Anzeigen    
         */
        public void AddButton(CompBitButtonStyle buttonStyle, string formularName)
        {
            FrmMenu frm = (FrmMenu)this.m_formulars["FrmMenu"];
            frm.AddBitButton(buttonStyle, formularName);
        }
    }
}