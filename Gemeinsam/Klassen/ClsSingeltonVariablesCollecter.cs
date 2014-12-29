using System;
using System.Collections;

namespace Helper
{

    public enum ClsSingeltonVariablesCollecterDataType { Null = 0, Int, Double, String }
    public class ClsSingeltonVariablesCollecter
    {
        private static ClsSingeltonVariablesCollecter m_instance;

        private Hashtable m_Variables;
        private Hashtable m_VariablesType;

        private ClsSingeltonVariablesCollecter()
        {
            this.m_Variables = new Hashtable();
            this.m_VariablesType = new Hashtable();
        }
        public static ClsSingeltonVariablesCollecter CreateInstance()
        {
            if (m_instance == null)
            {
                m_instance = new ClsSingeltonVariablesCollecter();
            }
            return m_instance;
        }

        public double ReadValueDouble(string varName)
        {
            double retval = 0.0;
            ClsSingeltonVariablesCollecterDataType var_tof = ClsSingeltonVariablesCollecterDataType.Null;

            if (this.m_Variables[varName] != null || this.m_VariablesType[varName] != null)
            {
                if (this.m_VariablesType[varName] != null)
                {
                    var_tof = (ClsSingeltonVariablesCollecterDataType)this.m_VariablesType[varName];
                    if (var_tof == ClsSingeltonVariablesCollecterDataType.Double)
                    {
                        retval = Convert.ToDouble(this.m_Variables[varName]);
                    }
                }
            }

            return retval;
        }

        public int ReadValueInt(string varName)
        {
            int retval = 0;
            ClsSingeltonVariablesCollecterDataType var_tof = ClsSingeltonVariablesCollecterDataType.Null;

            if (this.m_Variables[varName] != null || this.m_VariablesType[varName] != null)
            {
                if (this.m_VariablesType[varName] != null)
                {
                    var_tof = (ClsSingeltonVariablesCollecterDataType)this.m_VariablesType[varName];
                    if (var_tof == ClsSingeltonVariablesCollecterDataType.Int)
                    {
                        retval = Convert.ToInt32(this.m_Variables[varName]);
                    }
                }
            }
            return retval;
        }

        public object ReadValue(string varName)
        {
            object retval = null;
            retval=this.m_Variables[varName];
            return retval;
        }

        public string ReadValueString(string VarName)
        {
            string retval = "";
            if (this.m_Variables[VarName] != null)
            {
                retval=this.m_Variables[VarName].ToString();
            }
            return retval;
        }

        public void WriteValue(string varName,object value, bool doConvert=false)
        {
            bool write_ok = false;
            ClsSingeltonVariablesCollecterDataType var_tof = ClsSingeltonVariablesCollecterDataType.Null;

            if (this.m_VariablesType[varName] != null)
            {
                var_tof = (ClsSingeltonVariablesCollecterDataType)this.m_VariablesType[varName];
            }
            string tof = "";
            if (doConvert == false)
            {
                tof = value.GetType().ToString();
            }
            else
            {
                string val=value.ToString();
                if (var_tof == ClsSingeltonVariablesCollecterDataType.Int)
                {
                    int new_val;
                    int.TryParse(val, out new_val);
                    value = new_val;
                }
                if (var_tof == ClsSingeltonVariablesCollecterDataType.Double)
                {
                    double new_val;
                    double.TryParse(val, out new_val);
                    value = new_val;
                }
            }
            tof = value.GetType().ToString();

            if (tof == "System.Int32" && var_tof == ClsSingeltonVariablesCollecterDataType.Int)
            {
                write_ok = true;
            }

            if (tof == "System.Double" && var_tof == ClsSingeltonVariablesCollecterDataType.Double)
            {
                write_ok = true;
            }

            if (write_ok == true)
            {
                this.m_Variables[varName] = value;
            }
        }

        public void CreateVariable(string varName, ClsSingeltonVariablesCollecterDataType dataType)
        {
            if (this.m_Variables[varName] == null)
            {
                if (dataType == ClsSingeltonVariablesCollecterDataType.Int)
                {
                    int var = 0;
                    this.m_Variables.Add(varName, var);
                }
                if (dataType == ClsSingeltonVariablesCollecterDataType.Double)
                {
                    double var = 0.0;
                    this.m_Variables.Add(varName, var);
                }
                if (dataType == ClsSingeltonVariablesCollecterDataType.String)
                {
                    string var = "";
                    this.m_Variables.Add(varName, var);
                }
                this.m_VariablesType.Add(varName, dataType);
            }
        }
    }
}
