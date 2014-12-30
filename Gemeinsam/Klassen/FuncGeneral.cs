using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Drawing;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using System.Collections;
using System.ComponentModel.Design.Serialization;
using System.Reflection;
using System.Runtime;
using System.Runtime.InteropServices;
using System.Windows.Forms.Layout;
using System.Text.RegularExpressions;

namespace Helper
{
    public static class FuncGeneral
    {

        public static int komCounter = 0;

        public static string DeleteLeft(string Suchen, int Anzahl)
        {
            string retval = "";
            if (Suchen.Length > Anzahl)
            {
                retval = Suchen.Substring(Anzahl);
            }
            else
            {
                retval = Suchen;
            }
            return retval;
        }

        public static string DeleteRight(string Suchen, int Anzahl)
        {
            string retval = "";
            int laenge = Suchen.Length;

            if (laenge > Anzahl)
            {
                retval = Suchen.Substring(0,laenge-Anzahl);
            }
            else
            {
                retval = Suchen;
            }

            return retval;
        }

        public static string Parse(string Suchen,ref string Text)
        {
            string retval = "";
            string match="";
            string text = Text;
            
            Regex regEx = new Regex(Suchen);

            text = regEx.Replace(text, "");
            match = regEx.Match(Text).ToString();

            if (match.Length > 0)
            {
                retval = DeleteLeft(match, 3);
                retval = DeleteRight(retval, 1);


            }
            else
            {
                retval = match;
            }

            Text = text;

            return retval;
        }

        public static void Start()
        {
            FrmMenu frm = new FrmMenu();
            FrmPasswort frm1 = new FrmPasswort();
            FrmLanguage frm2 = new FrmLanguage();
            FrmKeyBoard frm3 = new FrmKeyBoard();
            FrmProgEnd frm4 = new FrmProgEnd();
        }

        public static string GetControlName(Control c)
        {
            komCounter++;
            string retval = "";
            string cType=c.GetType().ToString();
            string[] typeGroups = cType.Split('.');
            retval = "dyn" + typeGroups[typeGroups.Count() - 1] + komCounter;
            return retval;

        }

        public static void KillProgram(string ProgName, bool allProgs = true)
        {
            string pname = "";
            Process[] pp = Process.GetProcesses();
            int anz = pp.Count();
            for (int i = 0; i < anz; i++)
            {
                pname = pp[i].ProcessName.ToString();
                if (pname == ProgName)
                {
                    pp[i].Kill();
                    if (!allProgs)
                    {
                        break;
                    }
                }
            }
        }

        public static Control GetControlByName(Control container, string name)
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

        public static bool ProgramRunning(string ProgName)
        {
            bool retval = false;
            string pname = "";
            Process[] pp = Process.GetProcesses();
            for (int i = 0; i < pp.Count(); i++)
            {
                pname = pp[i].ProcessName.ToString();
                if (pname == ProgName)
                {
                    retval = true;
                    break;
                }
            }
            return retval;
        }

        



        public static void RotatePoints(ref List<PointF> pointToRotate, PointF centerPoint, float angleInDegrees)
        {
            for (int i = 0; i < pointToRotate.Count; i++)
            {
                pointToRotate[i]=RotatePointF(pointToRotate[i], centerPoint, angleInDegrees);
            }
        }

        public static PointF RotatePointF(PointF pointToRotate, PointF centerPoint, float angleInDegrees)
        {
            double angleInRadians = angleInDegrees * (Math.PI / 180);
            double cosTheta = Math.Cos(angleInRadians);
            double sinTheta = Math.Sin(angleInRadians);
            return new PointF
            {
                X = (float)(cosTheta * (pointToRotate.X - centerPoint.X) - sinTheta * (pointToRotate.Y - centerPoint.Y) + centerPoint.X),
                Y = (float)(sinTheta * (pointToRotate.X - centerPoint.X) + cosTheta * (pointToRotate.Y - centerPoint.Y) + centerPoint.Y)
            };
        }


        public static Point RotatePoint(Point pointToRotate, Point centerPoint, double angleInDegrees)
        {
            double angleInRadians = angleInDegrees * (Math.PI / 180);
            double cosTheta = Math.Cos(angleInRadians);
            double sinTheta = Math.Sin(angleInRadians);
            return new Point
            {
                X = (int)(cosTheta * (pointToRotate.X - centerPoint.X) - sinTheta * (pointToRotate.Y - centerPoint.Y) + centerPoint.X),
                Y = (int)(sinTheta * (pointToRotate.X - centerPoint.X) + cosTheta * (pointToRotate.Y - centerPoint.Y) + centerPoint.Y)
            };
        }



        public static ValveState GetState(string Input)
        {
            ValveState retval = ValveState.Close;
            if (Input == "0")
            {
                retval = ValveState.Close;
            }
            if (Input == "1")
            {
                retval = ValveState.Open;
            }
            if (Input == "2")
            {
                retval = ValveState.Undefine;
            }
            if (Input == "3")
            {
                retval = ValveState.Error;
            }
            return retval;
        }

        public static ValveMode GetMode(string Input)
        {
            ValveMode retval = ValveMode.Manuell;
            if (Input == "0")
            {
                retval = ValveMode.Manuell;
            }
            if (Input == "1")
            {
                retval = ValveMode.Automatic;
            }
            return retval;
        }

        public static PumpState GetStatePump(string Input)
        {
            PumpState retval = PumpState.NotUsed;
            if (Input == "0")
            {
                retval = PumpState.NotUsed;
            }
            if (Input == "1")
            {
                retval = PumpState.Ready;
            }
            if (Input == "2")
            {
                retval = PumpState.Running;
            }
            if (Input == "3")
            {
                retval = PumpState.Error;
            }
            return retval;
        }

        public static CompLedRectangle.CompLedState GetSateLED(string Input)
        {
            CompLedRectangle.CompLedState retval = CompLedRectangle.CompLedState.LedOff;
            if (Input == "0")
            {
                retval = CompLedRectangle.CompLedState.LedOff;
            }
            if (Input == "1")
            {
                retval = CompLedRectangle.CompLedState.LedOn;
            }
            return retval;
        }

        public static SwitchState GetSateToggleSwitch(string Input)
        {
            SwitchState retval = SwitchState.Off;
            if (Input == "0")
            {
                retval = SwitchState.Off;
            }
            if (Input == "1")
            {
                retval = SwitchState.On;
            }
            return retval;
        }

        public static LifterState GetStateLift(string Input)
        {
            LifterState retval = LifterState.Down;
            if (Input == "0")
            {
                retval = LifterState.Down;
            }
            if (Input == "1")
            {
                retval = LifterState.Sinking;
            }
            if (Input == "2")
            {
                retval = LifterState.Lifting;
            }
            if (Input == "3")
            {
                retval = LifterState.Up;
            }
            if (Input == "4")
            {
                retval = LifterState.Error;
            }
            return retval;
        }

        public static Point GetMiddle(Control Cntrl)
        {
            Point retval=new Point(0,0);
            retval.X = Cntrl.Width / 2 - 1;
            retval.Y = Cntrl.Height / 2 - 1;
            return retval;
        }

        public static void CreateValve(Valve Component, int Left, int Top)
        {
            komCounter++;
            string compName = "valveDyn_" + komCounter;
            Component.Left = Left;
            Component.Top = Top;
        }


        public static void CreateLabel(Label Component, int Left, int Top, int Width, int Height, string Text)
        {
            komCounter++;
            string compName = "lblDyn_" + komCounter;
            Component.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            Component.Name = compName;
            Component.AutoSize = false;
            Component.Height = Height;
            Component.Width = Width;
            Component.Left = Left;
            Component.Top = Top;
            Component.Text = Text;
        }



        public static void CreateLedRound(LedRound Component, int Left, int Top, Helper.LEDType LedType)
        {
            komCounter++;
            string compName = "ledRoundDyn_" + komCounter;
            Component.Left = Left;
            Component.Top = Top;
            Component.Margin = new System.Windows.Forms.Padding(4);
            Component.Name = compName;
            Component.Size = new System.Drawing.Size(25, 25);
            Component.State = Helper.CompLedRectangle.CompLedState.LedOff;
            Component.Type = LedType;
        }

        public static void Resize2D<T>(ref T[,] array, int newSizeX, int newSizeY)
        {
            int sizeX = array.GetLength(0);
            int sizeY = array.GetLength(1);
            if (sizeX == newSizeX && sizeY == newSizeY)
            {
                return;
            }
            T[,] newArray = new T[newSizeX, newSizeY];
            for (int i = 0; i < sizeX; i++)
            {
                for (int j = 0; j < sizeY; j++)
                {
                    if (i < newSizeX && j < newSizeY) newArray[i, j] = array[i, j];
                }
            }
            array = newArray;
        }
    }
}
