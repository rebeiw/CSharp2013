using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassDevelopment
{

    public enum DataType {Null = 0, Int, Double, String }
    public class ClsVarCollect
    {

        public event EventHandler<CustomEventArgs> ValueChanged;

        private static ClsVarCollect instance;

        private Hashtable m_Variables;
        private Hashtable m_VariablesType;

        private CustomEventArgs m_CustomEventArgs;

        private ClsVarCollect()
        {
            this.m_Variables = new Hashtable();
            this.m_VariablesType = new Hashtable();
            this.m_CustomEventArgs = new CustomEventArgs("");
        }
        public static ClsVarCollect CreateInstance()
        {
            if (instance == null)
            {
                instance = new ClsVarCollect();
            }
            return instance;
        }

        public double ReadValueDouble(string VarName)
        {
            double retval = 0.0;
            DataType var_tof=DataType.Null;

            if (this.m_Variables[VarName] != null || this.m_VariablesType[VarName] != null)
            {
                if (this.m_VariablesType[VarName] != null)
                {
                    var_tof = (DataType)this.m_VariablesType[VarName];
                    if(var_tof==DataType.Double)
                    {
                        retval = Convert.ToDouble(this.m_Variables[VarName]);
                    }
                }
            }

            return retval;
        }

        public int ReadValueInt(string VarName)
        {
            int retval = 0;
            DataType var_tof = DataType.Null;

            if (this.m_Variables[VarName] != null || this.m_VariablesType[VarName] != null)
            {
                if (this.m_VariablesType[VarName] != null)
                {
                    var_tof = (DataType)this.m_VariablesType[VarName];
                    if (var_tof == DataType.Int)
                    {
                        retval = Convert.ToInt32(this.m_Variables[VarName]);
                    }
                }
            }
            return retval;
        }

        public object ReadValue(string VarName)
        {
            object retval = null;
            retval=this.m_Variables[VarName];
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

        public void WriteValue(string VarName,object Value,bool DoConvert=false)
        {
            bool write_ok = false;
            DataType var_tof = DataType.Null;

            if (this.m_VariablesType[VarName] != null)
            {
                var_tof = (DataType)this.m_VariablesType[VarName];
            }
            string tof = "";
            if (DoConvert == false)
            {
                tof = Value.GetType().ToString();
            }
            else
            {
                string val=Value.ToString();
                if (var_tof == DataType.Int)
                {
                    int new_val;
                    int.TryParse(val, out new_val);
                    Value = new_val;
                }
                if (var_tof == DataType.Double)
                {
                    double new_val;
                    double.TryParse(val, out new_val);
                    Value = new_val;
                }
            }
            tof = Value.GetType().ToString();

            if (tof == "System.Int32" && var_tof == DataType.Int)
            {
                write_ok = true;
            }

            if (tof == "System.Double" && var_tof == DataType.Double)
            {
                write_ok = true;
            }

            if (write_ok == true)
            {
                this.m_Variables[VarName] = Value;
                this.m_CustomEventArgs.Message = VarName;
                //this.OnValueChanged(this.m_CustomEventArgs);
            }
        }

        public void CreateVariable(string VarName,DataType DatType)
        {
            if (this.m_Variables[VarName] == null)
            {
                if (DatType == DataType.Int)
                {
                    int Var = 0;
                    this.m_Variables.Add(VarName, Var);
                }
                if (DatType == DataType.Double)
                {
                    double Var = 0.0;
                    this.m_Variables.Add(VarName, Var);
                }
                if (DatType == DataType.String)
                {
                    string Var = "";
                    this.m_Variables.Add(VarName, Var);
                }
                this.m_VariablesType.Add(VarName, DatType);
            }
        }

        protected virtual void OnValueChanged(CustomEventArgs e)
        {
            EventHandler<CustomEventArgs> handler = ValueChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }
    }

    public class CustomEventArgs : EventArgs
    {
        public CustomEventArgs(string s)
        {
            message = s;
        }
        private string message;

        public string Message
        {
            get { return message; }
            set { message = value; }
        }
    }
}
