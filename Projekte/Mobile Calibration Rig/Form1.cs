using Mobile_Calibration_Rig.Properties;
using PdfSharp.Pdf;
using PdfSharp;
using PdfSharp.Drawing;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;


namespace Mobile_Calibration_Rig
{
    public partial class frmMain : Form
    {
        //####################################################################################################################
        Modbus MB = new Modbus();
        FA_M3_COM FA_M3 = new FA_M3_COM();
        bool IsCalibStarted = false;
        int Count = 0;
        DTM5080 CustTemp = new DTM5080();
        Log_HC2 AmbLog = new Log_HC2();
        Settings setting = new Settings();
        frmEnterValue EnterValue = new frmEnterValue();
        public static string MFCust = string.Empty;
        int TestTime = 0;
        int TestTimeExp = 0;
        float CurrentValue = 0;
        string[] arrTextComment = new string[10];
        byte DevAdr = 0;
        bool AbortTest = false;
        bool interrupt = false;
        bool IsPump1 = false;
        bool IsPump2=false;
        float AmbientPressure = 0;
        float AmbientHumidity = 0;
        float AmbientTemperature = 0;
        //####################################################################################################################
        float[] density = new float[21];
        string[] strWaterTempVessel = new string[10];
        string[] strWaterTempCustDevice = new string[10];
        string[] strWaterTempRig = new string[10];
        string[] strAmbTemp = new string[10];
        string[] strAmbHumidity = new string[10];
        string[] strAmbBaroPress = new string[10];

        #region functions

        public frmMain()
        {
            InitializeComponent();
        }

        /// <summary>
        /// calculate pressure from pressure transmitter values
        /// </summary>
        /// <param name="LRV">LRV of pressure transmitter</param>
        /// <param name="URV">URV of pressure transitter</param>
        /// <param name="Value">current value of PLC</param>
        /// <returns>pressure value</returns>
        private float CalculatePressure(float LRV, float URV, float Value)
        {
            return (((URV - LRV) / 16000) * Value) + (LRV - (URV - LRV) / 4);
        }

        /// <summary>
        /// calculate mass flow from current output
        /// </summary>
        /// <param name="LRV">LRV of device</param>
        /// <param name="URV">URV of device</param>
        /// <param name="Value">current value of PLC</param>
        /// <returns>mass flow value</returns>
        private float CalculateMassFlow(float LRVCurrent, float URVCurrent, float Value)
        {
            //Markus Formel
            return (((URVCurrent - LRVCurrent) / 16) * Value) + (LRVCurrent - (URVCurrent - LRVCurrent) / 4);
        }
        /// <summary>
        /// calculate mass flow of current
        /// </summary>
        /// <param name="LRVFlow">LRV value flow</param>
        /// <param name="URVFlow">URV value flow</param>
        /// <param name="LRVCurrent">LRV value current</param>
        /// <param name="URVCurrent">URV value current</param>
        /// <param name="Value">current value</param>
        /// <returns>Mass flow value</returns>
        private float CalculateMassflowCurrent(float LRVFlow,float URVFlow,float LRVCurrent, float URVCurrent, float Value)
        {
            //Stefan Formel
            return (((URVFlow - LRVFlow) / (URVCurrent - LRVCurrent)) * (Value - LRVCurrent)) + LRVFlow;
        }
        /// <summary>
        /// calculate mass flow of current
        /// </summary>
        /// <param name="LRVFlow">LRV value flow</param>
        /// <param name="URVFlow">URV value flow</param>
        /// <param name="LRVfrequency">LRV value frequency</param>
        /// <param name="URVfrequency">LRV value frequency</param>
        /// <param name="Value">frequency value</param>
        /// <returns>Mass flow value</returns>
        private float CalculateMassflowFrequenz(float LRVFlow, float URVFlow, float LRVfrequency, float URVfrequency, float Value)//, System.Windows.Forms.ComboBox cboFlowUnit)
        {
            //Stefan Formel
            //if (cboFlowUnit.SelectedText == "kg/h" || cboFlowUnit.SelectedText == "kg/h")
                return (((URVFlow - LRVFlow) / (URVfrequency - LRVfrequency)) * (Value - LRVfrequency)) + LRVFlow;
            //else if (cboFlowUnit.SelectedText == "t/h" || cboFlowUnit.SelectedText == "m³/h")
            //    return (((URVFlow - LRVFlow) / (URVfrequency - LRVfrequency)) * (Value - LRVfrequency)) + LRVFlow;
            //else return 0;
        }
        /// <summary>
        /// select reference device
        /// </summary>
        /// <param name="CalibPoint">calibration point in kg/h</param>
        /// <returns>ref device</returns>
        private byte GetReferenceDevice(double CalibPoint)
        {
            if(CalibPoint<=50000)
            {
                if (cboFlowUnit.SelectedIndex == 0 || cboFlowUnit.SelectedIndex == 2)       //kg/h and l/h 
                {
                    if (CalibPoint >= setting.CS32LowRange && CalibPoint < setting.CS32HighRange)
                        return 32;
                    else if (CalibPoint >= setting.CS34LowRange && CalibPoint < setting.CS34HighRange)
                        return 34;
                    else if (CalibPoint >= setting.CS36LowRange && CalibPoint < setting.CS36HighRange)
                        return 36;
                    else if (CalibPoint >= setting.CS38LowRange && CalibPoint <= setting.CS38HighRange)
                        return 38;
                }
                if (cboFlowUnit.SelectedIndex == 1 || cboFlowUnit.SelectedIndex == 3)       //t/h and m³/h
                {
                    if (CalibPoint >= setting.CS32LowRange / (float)1000 && CalibPoint < setting.CS32HighRange / (float)1000)
                        return 32;
                    else if (CalibPoint >= setting.CS34LowRange / (float)1000 && CalibPoint < setting.CS34HighRange / (float)1000)
                        return 34;
                    else if (CalibPoint >= setting.CS36LowRange / (float)1000 && CalibPoint < setting.CS36HighRange / (float)1000)
                        return 36;
                    else if (CalibPoint >= setting.CS38LowRange / (float)1000 && CalibPoint < setting.CS38HighRange / (float)1000)
                        return 38;
                }
            }
            else
                MessageBox.Show("Calibration point out of range", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return 0;
        }

        /// <summary>
        /// calculates mass flow to base unit kg/h
        /// </summary>
        /// <param name="Value">mass flow</param>
        /// <param name="Unit">unit</param>
        /// <returns>massflow in kg/h</returns>
        private double CalculateMassFlowTokg_h(double Value, string Unit)
        {
            if (Unit == "kg/h")
                return Value;
            else if (Unit == "t/h")
                return Value;
            //return Value / 1000;
            else if (Unit == "l/h")
                return Value;
            //return Value * 1000;
            else if (Unit == "m³/h")
                return Value;

            return 0;
        }

        /// <summary>
        /// calculates mass flow from base unit kg/h
        /// </summary>
        /// <param name="Value">mass flow</param>
        /// <param name="Unit">unit</param>
        /// <returns>massflow in selected unit</returns>
        private double CalculateMassFlowFromkg_h(double Value, string Unit)
        {
            if (Unit == "kg/h")
                return Value;
            else if (Unit == "t/h")
                return Value / 1000;
            else if (Unit == "l/h")
                return Value * 1000;
            else if (Unit == "m³/h")
                return Value;

            return 0;
        }

        /// <summary>
        /// Calculate percentage value of pressure
        /// </summary>
        /// <param name="LRV">lower limit</param>
        /// <param name="URV">upper limit</param>
        /// <param name="value">actual value</param>
        /// <returns>percentage value</returns>
        private double CalculatePercentage(double LRV, double URV, double value)
        {
            return (100 * (value - LRV)) / (URV - LRV);
        }

        private float CalcDensityMean()
        {
            //20 value from 0 to 19
            float temp=0;
            float mean_density=0;
            for(int i=0;i<21;i++)
            {
                if(density[i]!=0)
                {
                    temp = temp + (float)density[i];
                }
                else
                {
                    mean_density = temp / (i);
                    return mean_density;
                }
            }
            return mean_density;
        }

        private float CalcMean(float[] Value,int values)
        {
            float temp = 0;
            float mean = 0;
            for (int i = 0; i < values; i++)
            {
                temp += Value[i];
            }
            mean = temp / (float)4;  
            
            return mean;
        }

        #region Certificate
        /// <summary>
        /// create material certificate
        /// </summary>
        private void MakeCalCertPDF()
        {
            int leftborder = 40;
            int upperborder = 30;
            int upperoffset = 150;
            // Create a new PDF document
            PdfDocument document = new PdfDocument();

            // Create a font
            XFont font = new XFont("Arial", 22, XFontStyle.Bold);
            XFont font1 = new XFont("Arial", 14, XFontStyle.Bold);
            XFont font2 = new XFont("Arial", 10, XFontStyle.Regular);
            XFont font3 = new XFont("Arial", 10, XFontStyle.Bold);
            XFont font4 = new XFont("Arial", 8, XFontStyle.Regular);
            XFont font5 = new XFont("Arial", 8, XFontStyle.Bold);
            XFont font6 = new XFont("Arial", 12, XFontStyle.Bold);

            PdfPage page = document.AddPage();
            page.Size = PageSize.A4;
            XGraphics gfx = XGraphics.FromPdfPage(page);
            XImage image = XImage.FromFile(@"Yoko.jpg");
            //TODO: Bilder auswählen und hinzufügen
            //XImage image2 = XImage.FromFile(@"DPHarp EJX.jpg");

            System.Drawing.Pen pen = new Pen(Color.Black, 4);
            System.Drawing.Pen pen1 = new Pen(Color.Black, 1);

            //#################### Header ########################################################
            gfx.DrawImage(image, leftborder + 20, upperborder, 160, 25);
            gfx.DrawLine(pen, leftborder, upperborder + 25, 575, upperborder + 25);
            gfx.DrawString("Calibration", font, XBrushes.Black, leftborder + 20, upperborder + 50);
            gfx.DrawString("Certificate", font, XBrushes.Black, leftborder + 20, upperborder + 75);
            if(cboDevice.SelectedIndex!=4)
                gfx.DrawString(cboDevice.Text.Trim(), font, XBrushes.Black, leftborder + 290, upperborder + 50);
            else
                gfx.DrawString(txtDeviceType.Text.Trim(), font, XBrushes.Black, leftborder + 290, upperborder + 50);
            gfx.DrawLine(pen, leftborder, upperborder + 90, 575, upperborder + 90);
            //#################### Custmer Data ############line 120 ############################################
            gfx.DrawString("Customer Data", font1, XBrushes.Black, leftborder + 20, upperborder + 120);
            gfx.DrawString("Name:", font2, XBrushes.Black, leftborder + 20, upperborder + 135);
            gfx.DrawString(txtName.Text, font3, XBrushes.Black, leftborder + 110, upperborder + 135);
            gfx.DrawString("Address:", font2, XBrushes.Black, leftborder + 20, upperborder + 150);
            gfx.DrawString(txtAdress.Text, font3, XBrushes.Black, leftborder + 110, upperborder + 150);
            gfx.DrawString("Zip Code/City:", font2, XBrushes.Black, leftborder + 20, upperborder + 165);   //180
            gfx.DrawString(txtCity.Text, font3, XBrushes.Black, leftborder + 110, upperborder + 165);  //180
            //#################### Device Data ##########line 185 ##############################################
            //gfx.DrawImage(image2, leftborder + 300, upperborder + 35, 200, 50);
            gfx.DrawString("Device Data", font1, XBrushes.Black, leftborder + 20, upperborder + 185);       //120
            gfx.DrawString("Product Name:", font2, XBrushes.Black, leftborder + 20, upperborder + 200);     //135       
            if (cboDevice.SelectedIndex != 4)
                gfx.DrawString(cboDevice.Text.Trim() + " " + txtDeviceType.Text, font3, XBrushes.Black, leftborder + 110, upperborder + 200);  //135
            else
                gfx.DrawString(txtDeviceType.Text.Trim(), font3, XBrushes.Black, leftborder + 110, upperborder + 200);      //135
            //gfx.DrawString(cboDevice.Text.Trim(), font3, XBrushes.Black, leftborder + 105, upperborder + 135);
            gfx.DrawString("Model Code:", font2, XBrushes.Black, leftborder + 20, upperborder + 215);       //150
            if (txtModelcode.Text.Contains("/"))
            {
                gfx.DrawString(txtModelcode.Text.Substring(0, txtModelcode.Text.IndexOf('/')), font3, XBrushes.Black, leftborder + 110, upperborder + 215);     //150
                gfx.DrawString(txtModelcode.Text.Substring(txtModelcode.Text.IndexOf('/'), txtModelcode.Text.Length - txtModelcode.Text.IndexOf('/')), font3, XBrushes.Black, leftborder + 110, upperborder + 230);     //165
            }
            else
                gfx.DrawString(txtModelcode.Text, font3, XBrushes.Black, leftborder + 110, upperborder + 215);  
            
            //gfx.DrawString(txtOrderNo.Text, font3, XBrushes.Black, leftborder + 110, upperborder + 230);  
            gfx.DrawString("Serial No.Detector:", font2, XBrushes.Black, leftborder + 20, upperborder + 245);      
            gfx.DrawString(txtSerialNo.Text, font3, XBrushes.Black, leftborder + 110, upperborder + 245);       
            gfx.DrawString("Serial No.Converter:", font2, XBrushes.Black, leftborder + 20, upperborder + 260);      
            gfx.DrawString(txtSerialNoConvert.Text, font3, XBrushes.Black, leftborder + 110, upperborder + 260);      
            gfx.DrawString("Tag No.Detector:", font2, XBrushes.Black, leftborder + 20, upperborder + 275);         
            gfx.DrawString(txtTagNo.Text, font3, XBrushes.Black, leftborder + 110, upperborder + 275);      
            gfx.DrawString("Tag No.Converter:", font2, XBrushes.Black, leftborder + 20, upperborder + 290);         
            gfx.DrawString(txtTagNoConvert.Text, font3, XBrushes.Black, leftborder + 110, upperborder + 290);     
            gfx.DrawString("Measuring Range:", font2, XBrushes.Black, leftborder + 20, upperborder + 305);      
            gfx.DrawString(txtFlowLRV.Text + " - " + txtFlowURV.Text + " " + cboFlowUnit.Text, font3, XBrushes.Black, leftborder + 110, upperborder + 305);        //225
            gfx.DrawString("Condition DUT:", font2, XBrushes.Black, leftborder + 20, upperborder + 320);
            gfx.DrawString("As found:   "+ txtMFLRV.Text + " - " + txtMFURV.Text + " " + cboDUTFlowUnit.Text + " = " + txtPulseCurLRV.Text + " - " + txtPulseCurURV.Text + " " + lblDUT_Unit.Text, font3, XBrushes.Black, leftborder + 110, upperborder + 320);
            //#################### Calibration Information ########################################################
            gfx.DrawString("Calibration Information", font1, XBrushes.Black, leftborder + 290, upperborder + 120);
            gfx.DrawString("Calibration Method:", font2, XBrushes.Black, leftborder + 290, upperborder + 135);
            gfx.DrawString("Comparison Meter", font3, XBrushes.Black, leftborder + 390, upperborder + 135);
            gfx.DrawString("Calibration No:", font2, XBrushes.Black, leftborder + 290, upperborder + 150);
            gfx.DrawString(txtSerialNo.Text + "-M-" + txtCalibNo.Text, font3, XBrushes.Black, leftborder + 390, upperborder + 150);
            gfx.DrawString("Test Unit Id No:", font2, XBrushes.Black, leftborder + 290, upperborder + 165);
            gfx.DrawString("MCR1", font3, XBrushes.Black, leftborder + 390, upperborder + 165);
            gfx.DrawString("Ref. Id No:", font2, XBrushes.Black, leftborder + 290, upperborder + 180);
            gfx.DrawString("See actual flow test table", font3, XBrushes.Black, leftborder + 390, upperborder + 180);
            gfx.DrawString("Uncert. of Ref.Flow:", font2, XBrushes.Black, leftborder + 290, upperborder + 195);
            gfx.DrawString("<0.03% of reading", font3, XBrushes.Black, leftborder + 390, upperborder + 195);
            gfx.DrawString("Uncert. of Temp.Ref:", font2, XBrushes.Black, leftborder + 290, upperborder + 210);
            gfx.DrawString("<0.2 K", font3, XBrushes.Black, leftborder + 390, upperborder + 210);
            
            gfx.DrawString("Ambient Conditions:", font2, XBrushes.Black, leftborder + 290, upperborder + 240);  //225
            gfx.DrawString(txtAmbTemp.Text.Substring(0, 2) + " °C, " + txtAmbHumi.Text.Substring(0, 2) + " %r.H., " + txtAmbBaroPress.Text.Substring(0, 3) + " mbar", font3, XBrushes.Black, leftborder + 390, upperborder + 240);
            gfx.DrawString("Medium:", font2, XBrushes.Black, leftborder + 290, upperborder + 255);
            gfx.DrawString("Water", font3, XBrushes.Black, leftborder + 390, upperborder + 255);
            gfx.DrawString("Temperature", font2, XBrushes.Black, leftborder + 290, upperborder + 270);
            gfx.DrawString(txtWaterTempRig1.Text + " °C", font3, XBrushes.Black, leftborder + 390, upperborder + 270);
            gfx.DrawString("Density", font2, XBrushes.Black, leftborder + 290, upperborder + 285);
            gfx.DrawString(txtDensityRef1.Text + " kg/l", font3, XBrushes.Black, leftborder + 390, upperborder + 285);
            //########## Comment ##########line 410 ########################################################
            if (cboDevice.SelectedIndex == 0)           //RotaMass
            {
                gfx.DrawString("Settings at Calibration:", font6, XBrushes.Black, leftborder + 20, upperborder + 335);
                gfx.DrawString("Flow Settings", font3, XBrushes.Black, leftborder + 110, upperborder + 350);
                gfx.DrawString("Sensor Coefficient SK20: " + txtSensorSK20.Text, font2, XBrushes.Black, leftborder + 110, upperborder + 365);
                gfx.DrawString("Auto Zero Value: " + txtAutoZeroValue.Text + " kg/h", font2, XBrushes.Black, leftborder + 110, upperborder + 380);
                if (cboFlowUnit.SelectedIndex == 2 || cboFlowUnit.SelectedIndex == 3)       //VolumeFlow
                {
                    gfx.DrawString("Density Settings", font3, XBrushes.Black, leftborder + 325, upperborder + 350);
                    gfx.DrawString("Density Coefficient KD: " + txtDensityKD.Text + " kg/l", font2, XBrushes.Black, leftborder + 325, upperborder + 365);
                    gfx.DrawString("Frequency FL20:  " + txtFL20.Text + " Hz", font2, XBrushes.Black, leftborder + 325, upperborder + 380);
                }
            }
            else if (cboDevice.SelectedIndex == 2)      //Yewflow
            {
                gfx.DrawString("Settings at Calibration:", font6, XBrushes.Black, leftborder + 20, upperborder + 335);
                gfx.DrawString("Mean Flow Coefficient: " +txtMeanFlowCoef.Text+ " P/L", font3, XBrushes.Black, leftborder + 110, upperborder + 350);
            }
            else if (cboDevice.SelectedIndex == 3)      //ADMAG
            {
                gfx.DrawString("Settings at Calibration:", font6, XBrushes.Black, leftborder + 20, upperborder + 335);
                gfx.DrawString("Meter Factors: " + "LMF= " +txtLMF.Text +"      " +"HMF= "+txtHMF.Text, font3, XBrushes.Black, leftborder + 110, upperborder + 350);
            }
            else if (cboDevice.SelectedIndex == 4)      //OtherDevice
            {
                gfx.DrawString("Condition DUT:", font2, XBrushes.Black, leftborder + 20, upperborder + 320);
                //gfx.DrawString("As found ", font3, XBrushes.Black, leftborder + 110, upperborder + 320);
                //gfx.DrawString(txtMFLRV.Text + "-" + txtMFURV.Text + " " + cboDUTFlowUnit.Text + " = " + txtPulseCurLRV.Text + "-" + txtPulseCurURV.Text +" "+ lblDUT_Unit.Text, font3, XBrushes.Black, leftborder + 110, upperborder + 335);
                gfx.DrawString("As found:   " + txtMFLRV.Text + "-" + txtMFURV.Text + " " + cboDUTFlowUnit.Text + " = " + txtPulseCurLRV.Text + "-" + txtPulseCurURV.Text + " " + lblDUT_Unit.Text, font3, XBrushes.Black, leftborder + 110, upperborder + 320);
                gfx.DrawString(txtConditionDUT.Lines[0], font3, XBrushes.Black, leftborder + 110, upperborder + 335);
                gfx.DrawString(txtConditionDUT.Lines[1], font3, XBrushes.Black, leftborder + 110, upperborder + 350);
                gfx.DrawString(txtConditionDUT.Lines[2], font3, XBrushes.Black, leftborder + 110, upperborder + 365);
                gfx.DrawString(txtConditionDUT.Lines[3], font3, XBrushes.Black, leftborder + 110, upperborder + 380);
            }
            //########## TABLE ##########line 340 ########################################################
            gfx.DrawRectangle(pen1, leftborder + 20, upperborder + 390, 500, 390);      //240,500,520       //320,500,450
            gfx.DrawString("Actual Flow Test", font1, XBrushes.Black, leftborder + 30, upperborder + 410);
            gfx.DrawRectangle(pen1, leftborder + 30, upperborder + 420, 480, 60);
            gfx.DrawLine(pen1, leftborder + 30, upperborder + 440, leftborder + 510, upperborder + 440);        
            gfx.DrawLine(pen1, leftborder + 30, upperborder +460, leftborder + 510, upperborder + 460);        
            gfx.DrawLine(pen1, leftborder + 100, upperborder + 440, leftborder + 100, upperborder + 480);       
            gfx.DrawLine(pen1, leftborder + 210, upperborder + 420, leftborder + 210, upperborder + 480);

            //####################line 480 ########################################################
            gfx.DrawLine(pen1, leftborder + 440, upperborder + 420, leftborder + 440, upperborder +480);        //410/350/440/410
            gfx.DrawLine(pen1, leftborder + 210, upperborder + 440, leftborder + 210, upperborder + 480);
            gfx.DrawLine(pen1, leftborder + 320, upperborder + 440, leftborder + 320, upperborder + 480);
            gfx.DrawString("Reference", font3, XBrushes.Black, leftborder + 40, upperborder + 435);
            gfx.DrawString("RotaMass", font3, XBrushes.Black, leftborder + 130, upperborder + 435);
            if(cboDevice.SelectedIndex!=4)
                gfx.DrawString(cboDevice.Text.Trim() + " " + txtDeviceType.Text, font3, XBrushes.Black, leftborder + 245, upperborder + 435);
            else
                gfx.DrawString(txtDeviceType.Text, font3, XBrushes.Black, leftborder + 245, upperborder + 435); //365
            gfx.DrawString("Specification", font3, XBrushes.Black, leftborder + 445, upperborder + 435);

            gfx.DrawString("Ref. Device", font3, XBrushes.Black, leftborder + 40, upperborder + 455);
            gfx.DrawString("Act. Qm", font3, XBrushes.Black, leftborder + 130, upperborder + 455);
            gfx.DrawString("Error", font3, XBrushes.Black, leftborder + 365, upperborder + 455);
            gfx.DrawString("Cust. Qm", font3, XBrushes.Black, leftborder + 245, upperborder + 455);
            gfx.DrawString("Error max", font3, XBrushes.Black, leftborder + 450, upperborder + 455);
            gfx.DrawString("RCCS", font3, XBrushes.Black, leftborder + 50, upperborder + 475);
            //gfx.DrawString(cboFlowUnit.Text, font3, XBrushes.Black, leftborder + 45, upperborder + 405);
            gfx.DrawString(cboFlowUnit.Text, font3, XBrushes.Black, leftborder + 140, upperborder + 475);
            gfx.DrawString("% of reading", font3, XBrushes.Black, leftborder + 350, upperborder + 475);
            gfx.DrawString(cboFlowUnit.Text, font3, XBrushes.Black, leftborder + 245, upperborder + 475);
            gfx.DrawString("% of reading", font3, XBrushes.Black, leftborder + 445, upperborder + 475);
            //####################### End Header ##############################################################

            if (txtPoint1.Text != string.Empty)
            {
                gfx.DrawRectangle(pen1, leftborder + 30, upperborder + 330 + upperoffset, 480, 20);
                gfx.DrawLine(pen1, leftborder + 210, upperborder + 330 + upperoffset, leftborder + 210, upperborder + 350 + upperoffset);//430
                gfx.DrawLine(pen1, leftborder + 320, upperborder + 330 + upperoffset, leftborder + 320, upperborder + 350 + upperoffset);
                gfx.DrawLine(pen1, leftborder + 100, upperborder + 330 + upperoffset, leftborder + 100, upperborder + 350 + upperoffset);
                gfx.DrawLine(pen1, leftborder + 440, upperborder + 330 + upperoffset, leftborder + 440, upperborder + 350 + upperoffset);
                gfx.DrawString(txtRefNo1.Text, font3, XBrushes.Black, leftborder + 55, upperborder + 345 + upperoffset);        //425
                //gfx.DrawString(CalculateMassFlowFromkg_h(Convert.ToDouble(txtPoint1.Text), cboFlowUnit.Text).ToString("0.00"), font2, XBrushes.Black, leftborder + 45, upperborder + 425);
                //gfx.DrawString(CalculateMassFlowFromkg_h(Convert.ToDouble(txtPoint1Ref.Text), cboFlowUnit.Text).ToString("0.00"), font2, XBrushes.Black, leftborder + 140, upperborder + 425);
                gfx.DrawString(txtPoint1Ref.Text, font2, XBrushes.Black, leftborder + 140, upperborder + 345 + upperoffset);
                //gfx.DrawString(CalculateMassFlowFromkg_h(Convert.ToDouble(txtPoint1Cust.Text), cboFlowUnit.Text).ToString("0.00"), font2, XBrushes.Black, leftborder + 250, upperborder + 425);
                gfx.DrawString(txtPoint1Cust.Text, font2, XBrushes.Black, leftborder + 250, upperborder + 345 + upperoffset);
                gfx.DrawString(txtPoint1ErrRel.Text, font2, XBrushes.Black, leftborder + 365, upperborder + 345 + upperoffset);
                gfx.DrawString(txtPoint1MaxErr.Text, font2, XBrushes.Black, leftborder + 465, upperborder + 345 + upperoffset);
            }
            if (txtPoint2.Text != string.Empty)
            {
                gfx.DrawRectangle(pen1, leftborder + 30, upperborder + 350 + upperoffset, 480, 20);
                gfx.DrawLine(pen1, leftborder + 210, upperborder + 350 + upperoffset, leftborder + 210, upperborder + 370 + upperoffset); 
                gfx.DrawLine(pen1, leftborder + 320, upperborder + 350 + upperoffset, leftborder + 320, upperborder + 370 + upperoffset);
                gfx.DrawLine(pen1, leftborder + 100, upperborder + 350 + upperoffset, leftborder + 100, upperborder + 370 + upperoffset);
                gfx.DrawLine(pen1, leftborder + 440, upperborder + 350 + upperoffset, leftborder + 440, upperborder + 370 + upperoffset);
                gfx.DrawString(txtRefNo2.Text, font3, XBrushes.Black, leftborder + 55, upperborder + 365 + upperoffset);      //445
                //gfx.DrawString(CalculateMassFlowFromkg_h(Convert.ToDouble(txtPoint2.Text), cboFlowUnit.Text).ToString("0.00"), font2, XBrushes.Black, leftborder + 45, upperborder + 365);
                //gfx.DrawString(CalculateMassFlowFromkg_h(Convert.ToDouble(txtPoint2Ref.Text), cboFlowUnit.Text).ToString("0.00"), font2, XBrushes.Black, leftborder + 140, upperborder + 445);
                gfx.DrawString(txtPoint2Ref.Text, font2, XBrushes.Black, leftborder + 140, upperborder + 365 + upperoffset);
                //gfx.DrawString(CalculateMassFlowFromkg_h(Convert.ToDouble(txtPoint2Cust.Text), cboFlowUnit.Text).ToString("0.00"), font2, XBrushes.Black, leftborder + 250, upperborder + 445);
                gfx.DrawString(txtPoint2Cust.Text, font2, XBrushes.Black, leftborder + 250, upperborder + 365 + upperoffset);
                gfx.DrawString(txtPoint2ErrRel.Text, font2, XBrushes.Black, leftborder + 365, upperborder + 365 + upperoffset);
                gfx.DrawString(txtPoint2MaxErr.Text, font2, XBrushes.Black, leftborder + 465, upperborder + 365 + upperoffset);
            }
            if (txtPoint3.Text != string.Empty)
            {
                gfx.DrawRectangle(pen1, leftborder + 30, upperborder + 370 + upperoffset, 480, 20);
                gfx.DrawLine(pen1, leftborder + 210, upperborder + 370 + upperoffset, leftborder + 210, upperborder + 390 + upperoffset);
                gfx.DrawLine(pen1, leftborder + 320, upperborder + 370 + upperoffset, leftborder + 320, upperborder + 390 + upperoffset);
                gfx.DrawLine(pen1, leftborder + 100, upperborder + 370 + upperoffset, leftborder + 100, upperborder + 390 + upperoffset);
                gfx.DrawLine(pen1, leftborder + 440, upperborder + 370 + upperoffset, leftborder + 440, upperborder + 390 + upperoffset);
                gfx.DrawString(txtRefNo3.Text, font3, XBrushes.Black, leftborder + 55, upperborder + 385 + upperoffset);
                //gfx.DrawString(CalculateMassFlowFromkg_h(Convert.ToDouble(txtPoint3.Text), cboFlowUnit.Text).ToString("0.00"), font2, XBrushes.Black, leftborder + 45, upperborder + 385 + upperoffset);
                //gfx.DrawString(CalculateMassFlowFromkg_h(Convert.ToDouble(txtPoint3Ref.Text), cboFlowUnit.Text).ToString("0.00"), font2, XBrushes.Black, leftborder + 140, upperborder + 385 + upperoffset);
                gfx.DrawString(txtPoint3Ref.Text, font2, XBrushes.Black, leftborder + 140, upperborder + 385 + upperoffset);
                //gfx.DrawString(CalculateMassFlowFromkg_h(Convert.ToDouble(txtPoint3Cust.Text), cboFlowUnit.Text).ToString("0.00"), font2, XBrushes.Black, leftborder + 250, upperborder + 385 + upperoffset);
                gfx.DrawString(txtPoint3Cust.Text, font2, XBrushes.Black, leftborder + 250, upperborder + 385 + upperoffset);
                gfx.DrawString(txtPoint3ErrRel.Text, font2, XBrushes.Black, leftborder + 365, upperborder + 385 + upperoffset);
                gfx.DrawString(txtPoint3MaxErr.Text, font2, XBrushes.Black, leftborder + 465, upperborder + 385 + upperoffset);
            }
            if (txtPoint4.Text != string.Empty)
            {
                gfx.DrawRectangle(pen1, leftborder + 30, upperborder + 390 + upperoffset, 480, 20);
                gfx.DrawLine(pen1, leftborder + 210, upperborder + 390 + upperoffset, leftborder + 210, upperborder + 410 + upperoffset);
                gfx.DrawLine(pen1, leftborder + 320, upperborder + 390 + upperoffset, leftborder + 320, upperborder + 410 + upperoffset);
                gfx.DrawLine(pen1, leftborder + 100, upperborder + 390 + upperoffset, leftborder + 100, upperborder + 410 + upperoffset);
                gfx.DrawLine(pen1, leftborder + 440, upperborder + 390 + upperoffset, leftborder + 440, upperborder + 410 + upperoffset);
                gfx.DrawString(txtRefNo4.Text, font3, XBrushes.Black, leftborder + 55, upperborder + 405 + upperoffset);
                //gfx.DrawString(CalculateMassFlowFromkg_h(Convert.ToDouble(txtPoint4.Text), cboFlowUnit.Text).ToString("0.00"), font2, XBrushes.Black, leftborder + 45, upperborder + 405 + upperoffset);
                //gfx.DrawString(CalculateMassFlowFromkg_h(Convert.ToDouble(txtPoint4Ref.Text), cboFlowUnit.Text).ToString("0.00"), font2, XBrushes.Black, leftborder + 140, upperborder + 405 + upperoffset);
                gfx.DrawString(txtPoint4Ref.Text, font2, XBrushes.Black, leftborder + 140, upperborder + 405 + upperoffset);
                //gfx.DrawString(CalculateMassFlowFromkg_h(Convert.ToDouble(txtPoint4Cust.Text), cboFlowUnit.Text).ToString("0.00"), font2, XBrushes.Black, leftborder + 250, upperborder + 405 + upperoffset);
                gfx.DrawString(txtPoint4Cust.Text, font2, XBrushes.Black, leftborder + 250, upperborder + 405 + upperoffset);
                gfx.DrawString(txtPoint4ErrRel.Text, font2, XBrushes.Black, leftborder + 365, upperborder + 405 + upperoffset);
                gfx.DrawString(txtPoint4MaxErr.Text, font2, XBrushes.Black, leftborder + 465, upperborder + 405 + upperoffset);
            }
            if (txtPoint5.Text != string.Empty)
            {
                gfx.DrawRectangle(pen1, leftborder + 30, upperborder + 410 + upperoffset, 480, 20);
                gfx.DrawLine(pen1, leftborder + 210, upperborder + 410 + upperoffset, leftborder + 210, upperborder + 430 + upperoffset);
                gfx.DrawLine(pen1, leftborder + 320, upperborder + 410 + upperoffset, leftborder + 320, upperborder + 430 + upperoffset);
                gfx.DrawLine(pen1, leftborder + 100, upperborder + 410 + upperoffset, leftborder + 100, upperborder + 430 + upperoffset);
                gfx.DrawLine(pen1, leftborder + 440, upperborder + 410 + upperoffset, leftborder + 440, upperborder + 430 + upperoffset);
                gfx.DrawString(txtRefNo5.Text, font3, XBrushes.Black, leftborder + 55, upperborder + 425 + upperoffset);
                //gfx.DrawString(CalculateMassFlowFromkg_h(Convert.ToDouble(txtPoint5.Text), cboFlowUnit.Text).ToString("0.00"), font2, XBrushes.Black, leftborder + 45, upperborder + 425 + upperoffset);
                //gfx.DrawString(CalculateMassFlowFromkg_h(Convert.ToDouble(txtPoint5Ref.Text), cboFlowUnit.Text).ToString("0.00"), font2, XBrushes.Black, leftborder + 140, upperborder + 425 + upperoffset);
                gfx.DrawString(txtPoint5Ref.Text, font2, XBrushes.Black, leftborder + 140, upperborder + 425 + upperoffset);
                //gfx.DrawString(CalculateMassFlowFromkg_h(Convert.ToDouble(txtPoint5Cust.Text), cboFlowUnit.Text).ToString("0.00"), font2, XBrushes.Black, leftborder + 250, upperborder + 425 + upperoffset);
                gfx.DrawString(txtPoint5Cust.Text, font2, XBrushes.Black, leftborder + 250, upperborder + 425 + upperoffset);
                gfx.DrawString(txtPoint5ErrRel.Text, font2, XBrushes.Black, leftborder + 365, upperborder + 425 + upperoffset);
                gfx.DrawString(txtPoint5MaxErr.Text, font2, XBrushes.Black, leftborder + 465, upperborder + 425 + upperoffset);
            }
            if (txtPoint6.Text != string.Empty)
            {
                gfx.DrawRectangle(pen1, leftborder + 30, upperborder + 430 + upperoffset, 480, 20);
                gfx.DrawLine(pen1, leftborder + 210, upperborder + 430 + upperoffset, leftborder + 210, upperborder + 450 + upperoffset);
                gfx.DrawLine(pen1, leftborder + 320, upperborder + 430 + upperoffset, leftborder + 320, upperborder + 450 + upperoffset);
                gfx.DrawLine(pen1, leftborder + 100, upperborder + 430 + upperoffset, leftborder + 100, upperborder + 450 + upperoffset);
                gfx.DrawLine(pen1, leftborder + 440, upperborder + 430 + upperoffset, leftborder + 440, upperborder + 450 + upperoffset);
                gfx.DrawString(txtRefNo6.Text, font3, XBrushes.Black, leftborder + 55, upperborder + 445 + upperoffset);
                //gfx.DrawString(CalculateMassFlowFromkg_h(Convert.ToDouble(txtPoint6.Text), cboFlowUnit.Text).ToString("0.00"), font2, XBrushes.Black, leftborder + 45, upperborder + 445 + upperoffset);
                //gfx.DrawString(CalculateMassFlowFromkg_h(Convert.ToDouble(txtPoint6Ref.Text), cboFlowUnit.Text).ToString("0.00"), font2, XBrushes.Black, leftborder + 140, upperborder + 445 + upperoffset);
                gfx.DrawString(txtPoint6Ref.Text, font2, XBrushes.Black, leftborder + 140, upperborder + 445 + upperoffset);
                //gfx.DrawString(CalculateMassFlowFromkg_h(Convert.ToDouble(txtPoint6Cust.Text), cboFlowUnit.Text).ToString("0.00"), font2, XBrushes.Black, leftborder + 250, upperborder + 445 + upperoffset);
                gfx.DrawString(txtPoint6Cust.Text, font2, XBrushes.Black, leftborder + 250, upperborder + 445 + upperoffset);
                gfx.DrawString(txtPoint6ErrRel.Text, font2, XBrushes.Black, leftborder + 365, upperborder + 445 + upperoffset);
                gfx.DrawString(txtPoint6MaxErr.Text, font2, XBrushes.Black, leftborder + 465, upperborder + 445 + upperoffset);
            }
            if (txtPoint7.Text != string.Empty)
            {
                gfx.DrawRectangle(pen1, leftborder + 30, upperborder + 450 + upperoffset, 480, 20);
                gfx.DrawLine(pen1, leftborder + 210, upperborder + 450 + upperoffset, leftborder + 210, upperborder + 470 + upperoffset);
                gfx.DrawLine(pen1, leftborder + 320, upperborder + 450 + upperoffset, leftborder + 320, upperborder + 470 + upperoffset);
                gfx.DrawLine(pen1, leftborder + 100, upperborder + 450 + upperoffset, leftborder + 100, upperborder + 470 + upperoffset);
                gfx.DrawLine(pen1, leftborder + 440, upperborder + 450 + upperoffset, leftborder + 440, upperborder + 470 + upperoffset);
                gfx.DrawString(txtRefNo7.Text, font3, XBrushes.Black, leftborder + 55, upperborder + 465 + upperoffset);
                //gfx.DrawString(CalculateMassFlowFromkg_h(Convert.ToDouble(txtPoint7.Text), cboFlowUnit.Text).ToString("0.00"), font2, XBrushes.Black, leftborder + 45, upperborder + 465 + upperoffset);
                //gfx.DrawString(CalculateMassFlowFromkg_h(Convert.ToDouble(txtPoint7Ref.Text), cboFlowUnit.Text).ToString("0.00"), font2, XBrushes.Black, leftborder + 140, upperborder + 465 + upperoffset);
                gfx.DrawString(txtPoint7Ref.Text, font2, XBrushes.Black, leftborder + 140, upperborder + 465 + upperoffset);
                //gfx.DrawString(CalculateMassFlowFromkg_h(Convert.ToDouble(txtPoint7Cust.Text), cboFlowUnit.Text).ToString("0.00"), font2, XBrushes.Black, leftborder + 250, upperborder + 465 + upperoffset);
                gfx.DrawString(txtPoint7Cust.Text, font2, XBrushes.Black, leftborder + 250, upperborder + 465 + upperoffset);
                gfx.DrawString(txtPoint7ErrRel.Text, font2, XBrushes.Black, leftborder + 365, upperborder + 465 + upperoffset);
                gfx.DrawString(txtPoint7MaxErr.Text, font2, XBrushes.Black, leftborder + 465, upperborder + 465 + upperoffset);
            }
            if (txtPoint8.Text != string.Empty)
            {
                gfx.DrawRectangle(pen1, leftborder + 30, upperborder + 470 + upperoffset, 480, 20);
                gfx.DrawLine(pen1, leftborder + 210, upperborder + 470 + upperoffset, leftborder + 210, upperborder + 490 + upperoffset);
                gfx.DrawLine(pen1, leftborder + 320, upperborder + 470 + upperoffset, leftborder + 320, upperborder + 490 + upperoffset);
                gfx.DrawLine(pen1, leftborder + 100, upperborder + 470 + upperoffset, leftborder + 100, upperborder + 490 + upperoffset);
                gfx.DrawLine(pen1, leftborder + 440, upperborder + 470 + upperoffset, leftborder + 440, upperborder + 490 + upperoffset);
                gfx.DrawString(txtRefNo8.Text, font3, XBrushes.Black, leftborder + 55, upperborder + 485 + upperoffset);
                //gfx.DrawString(CalculateMassFlowFromkg_h(Convert.ToDouble(txtPoint8.Text), cboFlowUnit.Text).ToString("0.00"), font2, XBrushes.Black, leftborder + 45, upperborder + 485 + upperoffset);
                //gfx.DrawString(CalculateMassFlowFromkg_h(Convert.ToDouble(txtPoint8Ref.Text), cboFlowUnit.Text).ToString("0.00"), font2, XBrushes.Black, leftborder + 140, upperborder + 485 + upperoffset);
                gfx.DrawString(txtPoint8Ref.Text, font2, XBrushes.Black, leftborder + 140, upperborder + 485 + upperoffset);
                //gfx.DrawString(CalculateMassFlowFromkg_h(Convert.ToDouble(txtPoint8Cust.Text), cboFlowUnit.Text).ToString("0.00"), font2, XBrushes.Black, leftborder + 250, upperborder + 485 + upperoffset);
                gfx.DrawString(txtPoint8Cust.Text, font2, XBrushes.Black, leftborder + 250, upperborder + 485 + upperoffset);
                gfx.DrawString(txtPoint8ErrRel.Text, font2, XBrushes.Black, leftborder + 365, upperborder + 485 + upperoffset);
                gfx.DrawString(txtPoint8MaxErr.Text, font2, XBrushes.Black, leftborder + 465, upperborder + 485 + upperoffset);
            }
            if (txtPoint9.Text != string.Empty)
            {
                gfx.DrawRectangle(pen1, leftborder + 30, upperborder + 490 + upperoffset, 480, 20);
                gfx.DrawLine(pen1, leftborder + 210, upperborder + 490 + upperoffset, leftborder + 210, upperborder + 510 + upperoffset);
                gfx.DrawLine(pen1, leftborder + 320, upperborder + 490 + upperoffset, leftborder + 320, upperborder + 510 + upperoffset);
                gfx.DrawLine(pen1, leftborder + 100, upperborder + 490 + upperoffset, leftborder + 100, upperborder + 510 + upperoffset);
                gfx.DrawLine(pen1, leftborder + 440, upperborder + 490 + upperoffset, leftborder + 440, upperborder + 510 + upperoffset);
                gfx.DrawString(txtRefNo9.Text, font3, XBrushes.Black, leftborder + 55, upperborder + 505 + upperoffset);
                //gfx.DrawString(CalculateMassFlowFromkg_h(Convert.ToDouble(txtPoint9.Text), cboFlowUnit.Text).ToString("0.00"), font2, XBrushes.Black, leftborder + 45, upperborder + 505 + upperoffset);
                //gfx.DrawString(CalculateMassFlowFromkg_h(Convert.ToDouble(txtPoint9Ref.Text), cboFlowUnit.Text).ToString("0.00"), font2, XBrushes.Black, leftborder + 140, upperborder + 505 + upperoffset);
                gfx.DrawString(txtPoint9Ref.Text, font2, XBrushes.Black, leftborder + 140, upperborder + 505 + upperoffset);
                //gfx.DrawString(CalculateMassFlowFromkg_h(Convert.ToDouble(txtPoint9Cust.Text), cboFlowUnit.Text).ToString("0.00"), font2, XBrushes.Black, leftborder + 250, upperborder + 505 + upperoffset);
                gfx.DrawString(txtPoint9Cust.Text, font2, XBrushes.Black, leftborder + 250, upperborder + 505 + upperoffset);
                gfx.DrawString(txtPoint9ErrRel.Text, font2, XBrushes.Black, leftborder + 365, upperborder + 505 + upperoffset);
                gfx.DrawString(txtPoint9MaxErr.Text, font2, XBrushes.Black, leftborder + 465, upperborder + 505 + upperoffset);
            }
            if (txtPoint10.Text != string.Empty)
            {
                gfx.DrawRectangle(pen1, leftborder + 30, upperborder + 510 + upperoffset, 480, 20);
                gfx.DrawLine(pen1, leftborder + 210, upperborder + 510 + upperoffset, leftborder + 210, upperborder + 530 + upperoffset);
                gfx.DrawLine(pen1, leftborder + 320, upperborder + 510 + upperoffset, leftborder + 320, upperborder + 530 + upperoffset);
                gfx.DrawLine(pen1, leftborder + 100, upperborder + 510 + upperoffset, leftborder + 100, upperborder + 530 + upperoffset);
                gfx.DrawLine(pen1, leftborder + 440, upperborder + 510 + upperoffset, leftborder + 440, upperborder + 530 + upperoffset);
                gfx.DrawString(txtRefNo10.Text, font3, XBrushes.Black, leftborder + 55, upperborder + 525 + upperoffset);
                //gfx.DrawString(CalculateMassFlowFromkg_h(Convert.ToDouble(txtPoint10.Text), cboFlowUnit.Text).ToString("0.00"), font2, XBrushes.Black, leftborder + 45, upperborder + 525 + upperoffset);
                //gfx.DrawString(CalculateMassFlowFromkg_h(Convert.ToDouble(txtPoint10Ref.Text), cboFlowUnit.Text).ToString("0.00"), font2, XBrushes.Black, leftborder + 140, upperborder + 525 + upperoffset);
                gfx.DrawString(txtPoint10Ref.Text, font2, XBrushes.Black, leftborder + 140, upperborder + 525 + upperoffset);
                //gfx.DrawString(CalculateMassFlowFromkg_h(Convert.ToDouble(txtPoint10Cust.Text), cboFlowUnit.Text).ToString("0.00"), font2, XBrushes.Black, leftborder + 250, upperborder + 525 + upperoffset);
                gfx.DrawString(txtPoint10Cust.Text, font2, XBrushes.Black, leftborder + 250, upperborder + 525 + upperoffset);
                gfx.DrawString(txtPoint10ErrRel.Text, font2, XBrushes.Black, leftborder + 365, upperborder + 525 + upperoffset);
                gfx.DrawString(txtPoint10MaxErr.Text, font2, XBrushes.Black, leftborder + 465, upperborder + 525 + upperoffset);
            }
            //gfx.DrawString("The transmitter was calibrated according to all applicable Yokogawa Quality Inspection Rules.", font2, XBrushes.Black, leftborder + 30, upperborder + 565 + upperoffset);       //540
            //gfx.DrawString("The calibration result is within all specified limits.", font2, XBrushes.Black, leftborder + 30, upperborder + 580 + upperoffset);      //555
            //################################
            
            gfx.DrawString("Date: " + DateTime.Now.ToShortDateString(), font2, XBrushes.Black, leftborder + 30, upperborder + 710);
            gfx.DrawString("Inspector", font2, XBrushes.Black, leftborder + 305, upperborder + 720);
            gfx.DrawLine(pen1, leftborder + 240, upperborder + 710, leftborder + 440, upperborder + 710);
            gfx.DrawString("Remark:", font5, XBrushes.Black, leftborder + 30, upperborder + 740);
            gfx.DrawString(txtComment.Lines[0], font4, XBrushes.Black, leftborder + 100, upperborder + 740);
            gfx.DrawString(txtComment.Lines[1], font4, XBrushes.Black, leftborder + 100, upperborder + 750);
            gfx.DrawString(txtComment.Lines[2], font4, XBrushes.Black, leftborder + 100, upperborder + 760);
            gfx.DrawString(txtComment.Lines[3], font4, XBrushes.Black, leftborder + 100, upperborder + 770);
            gfx.DrawString(txtComment.Lines[4], font4, XBrushes.Black, leftborder + 100, upperborder + 780);
            //gfx.DrawString(txtComment.Lines[5], font4, XBrushes.Black, leftborder + 100, upperborder + 760);
            //gfx.DrawString(txtComment.Lines[6], font4, XBrushes.Black, leftborder + 100, upperborder + 770);
            //gfx.DrawString(txtComment.Lines[7], font4, XBrushes.Black, leftborder + 100, upperborder + 780);
            //gfx.DrawString(txtComment.Lines[8], font4, XBrushes.Black, leftborder + 100, upperborder + 790);
            //gfx.DrawString(txtComment.Lines[9], font4, XBrushes.Black, leftborder + 100, upperborder + 800);

            gfx.DrawString("ROTA YOKOGAWA GmbH & Co. KG, Wehr, Germany", font4, XBrushes.Black, leftborder + 335, upperborder + 790);
            gfx.Dispose();

            // Save the document...
            string filename = setting.CalCertPath + txtSerialNo.Text.Trim() + "-M-" + txtCalibNo.Text + ".pdf";
            document.Save(filename);
            // ...and start a viewer.
            Process.Start(filename);
        }
        #endregion cetificate

        #endregion

        #region Events

        private void frmMain_Load(object sender, EventArgs e)
        {
            try
            {
                if (!ComPortModBus.IsOpen)      //COM4
                    ComPortModBus.Open();
                if (!ComPortFA_M3.IsOpen)
                    ComPortFA_M3.Open();
                if (!COMPortCustTemp.IsOpen)
                    COMPortCustTemp.Open();
                if (!COMPortAmbientTemp.IsOpen)
                    COMPortAmbientTemp.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
                gboxRotamassDta.Visible = false;
                gboxAdmagData.Visible = false;
                gboxYewflow.Visible = false;
                gboxOther.Visible = false;
                Application.DoEvents();
                RbtnOnePump.Select();
                RbtnPump1.Select();
                oleDbDACalibData.Fill(dsCalibData1, "Calib data");
                oleDbDADeviceData.Fill(dsDeviceData1, "Device data");
                oleDbDATesting.Fill(dsTesting1, "Testing");

            
            try
            {
                cboTestTime.SelectedIndex = 0;
                cboTestTime.Text = "60";
                cboFlowUnit.SelectedIndex = 0;
                cboFlowUnit.Text = "kg/h";

                CustTemp.Set_Temp_Resistance(COMPortCustTemp);
                FA_M3.OpenAllRelays(2, ComPortFA_M3);
                AmbLog.Initialize(COMPortAmbientTemp);   
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


                arrTextComment[0] = txtComment.Lines[0];
                arrTextComment[2] = txtComment.Lines[2];


        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            FA_M3.OpenAllRelays(2, ComPortFA_M3);
            tmrTempMeasurement.Enabled = false;
            tmrMFMeasurement.Enabled = false;
            tmrMeasurements.Enabled = false;
            if (ComPortModBus.IsOpen)
                ComPortModBus.Close();
            if (ComPortFA_M3.IsOpen)
                ComPortFA_M3.Close();
            if (COMPortCustTemp.IsOpen)
                COMPortCustTemp.Close();
            if (COMPortAmbientTemp.IsOpen)
                COMPortAmbientTemp.Close();
        }

        private void tmrMeasurements_Tick(object sender, EventArgs e)
        {
            #region Dev1
            if (chkDev1.Checked)
            {
                //Task<float> task3 = Task<float>.Factory.StartNew(() =>
                //     {
                //         return MB.ReadMassFlow(1, ComPortModBus);
                //     });
                //txtMassFlowRef1.Text = task3.Result.ToString("0.00");

                //Task<string> task4 = Task<string>.Factory.StartNew(() =>
                //    {
                //        return MB.ReadMassFlowUnit(1, ComPortModBus);
                //    });
                //txtMassFlowRef1.Text += " " + task4.Result;

                //Task<float> task5 = Task<float>.Factory.StartNew(() =>
                //    {
                //        return MB.ReadTemperature(1, ComPortModBus);
                //    });
                //txtTemperatureRef1.Text = task5.Result.ToString("0.00");

                //Task<string> task6 = Task<string>.Factory.StartNew(() =>
                //    {
                //        return MB.ReadTemperatureUnit(1, ComPortModBus);
                //    });
                //txtTemperatureRef1.Text += " " + task6.Result;

                //Task<float> task7 = Task<float>.Factory.StartNew(() =>
                //    {
                //        return MB.ReadMassFTotal(1, ComPortModBus);
                //    });
                //txtFTotalMassRef1.Text = task7.Result.ToString("0.00");

                //Task<string> task8 = Task<string>.Factory.StartNew(() =>
                //   {
                //       return MB.ReadTotalMassUnit(1, ComPortModBus);
                //   });
                //txtFTotalMassRef1.Text += " " + task8.Result;

                //Task<float> task9 = Task<float>.Factory.StartNew(() =>
                //    {
                //        return MB.ReadVolumeFlow(1, ComPortModBus);
                //    });
                //txtVolumeFlowRef1.Text = task9.Result.ToString("0.00");

                //Task<string> task10 = Task<string>.Factory.StartNew(() =>
                //    {
                //        return MB.ReadVolumeFlowUnit(1, ComPortModBus);
                //    });
                //txtVolumeFlowRef1.Text += " " + task10.Result;

                //Task<float> task11 = Task<float>.Factory.StartNew(() =>
                //   {
                //       return MB.ReadDensity(1, ComPortModBus);
                //   });
                //txtDensityRef1.Text = task11.Result.ToString("0.00");

                //Task<string> task12 = Task<string>.Factory.StartNew(() =>
                //    {
                //        return MB.ReadDensityUnit(1, ComPortModBus);
                //    });
                //txtDensityRef1.Text += " " + task12.Result;

                //Task<float> task13 = Task<float>.Factory.StartNew(() =>
                //    {
                //        return MB.ReadVolFTotal(1, ComPortModBus);
                //    });
                //txtFTotalVolRef1.Text = task13.Result.ToString("0.00");

                //Task<string> task14 = Task<string>.Factory.StartNew(() =>
                //    {
                //        return MB.ReadTotalVolumeUnit(1, ComPortModBus);
                //    });
                //txtFTotalVolRef1.Text += " " + task14.Result;

                //Task<float> task15 = Task<float>.Factory.StartNew(() =>
                //    {
                //        return MB.ReadVelocity(1, ComPortModBus);
                //    });
                //txtVelocityRef1.Text = task15.Result.ToString("0.00");

                //Task<string> task16 = Task<string>.Factory.StartNew(() =>
                //   {
                //       return MB.ReadVelocityUnit(1, ComPortModBus);
                //   });
                //txtVelocityRef1.Text += " " + task16.Result;
                //tabCalibration.Invalidate();

                txtMassFlowRef1.Text = MB.ReadMassFlow(1, ComPortModBus).ToString("0.00");
                txtTemperatureRef1.Text = MB.ReadTemperature(1, ComPortModBus).ToString("0.00");
                txtDensityRef1.Text = MB.ReadDensity(1, ComPortModBus).ToString("0.00");
                Application.DoEvents();
                tabCalibration.Invalidate();

            }
            #endregion

            #region Dev2
            if(chkDev2.Checked)
            {
            txtMassFlowRef2.Text = MB.ReadMassFlow(2, ComPortModBus).ToString("0.00");
            //txtMassFlowRef2.Text += " " + MB.ReadMassFlowUnit(2, ComPortModBus);
            txtTemperatureRef2.Text = MB.ReadTemperature(2, ComPortModBus).ToString("0.00");
            //txtTemperatureRef2.Text += " " + MB.ReadTemperatureUnit(2, ComPortModBus);
            //txtFTotalMassRef2.Text = MB.ReadMassFTotal(2, ComPortModBus).ToString("0.00");
            //txtFTotalMassRef2.Text += " " + MB.ReadTotalMassUnit(2, ComPortModBus);
            //txtVolumeFlowRef2.Text = MB.ReadVolumeFlow(2, ComPortModBus).ToString("0.00");
            //txtVolumeFlowRef2.Text += " " + MB.ReadVolumeFlowUnit(2, ComPortModBus);
            txtDensityRef2.Text = MB.ReadDensity(2, ComPortModBus).ToString("0.00");
            //txtDensityRef2.Text += " " + MB.ReadDensityUnit(2, ComPortModBus);
            //txtFTotalVolRef2.Text = MB.ReadVolFTotal(2, ComPortModBus).ToString("0.00");
            //txtFTotalVolRef2.Text += " " + MB.ReadTotalVolumeUnit(2, ComPortModBus);
            //txtVelocityRef2.Text = MB.ReadVelocity(2, ComPortModBus).ToString("0.00");
            //txtVelocityRef2.Text += " " + MB.ReadVelocityUnit(2, ComPortModBus);
            Application.DoEvents();
            tabCalibration.Invalidate();
            }
            #endregion

            #region Dev3
                if (chkDev3.Checked)
	                {
		                    txtMassFlowRef3.Text = MB.ReadMassFlow(3, ComPortModBus).ToString("0.00");
                            //txtMassFlowRef3.Text += " " + MB.ReadMassFlowUnit(3, ComPortModBus);
                            txtTemperatureRef3.Text = MB.ReadTemperature(3, ComPortModBus).ToString("0.00");
                            //txtTemperatureRef3.Text += " " + MB.ReadTemperatureUnit(3, ComPortModBus);
                            //txtFTotalMassRef3.Text = MB.ReadMassFTotal(3, ComPortModBus).ToString("0.00");
                            //txtFTotalMassRef3.Text += " " + MB.ReadTotalMassUnit(3, ComPortModBus);
                            //txtVolumeFlowRef3.Text = MB.ReadVolumeFlow(3, ComPortModBus).ToString("0.00");
                            //txtVolumeFlowRef3.Text += " " + MB.ReadVolumeFlowUnit(3, ComPortModBus);
                            txtDensityRef3.Text = MB.ReadDensity(3, ComPortModBus).ToString("0.00");
                            //txtDensityRef3.Text += " " + MB.ReadDensityUnit(3, ComPortModBus);
                            //txtFTotalVolRef3.Text = MB.ReadVolFTotal(3, ComPortModBus).ToString("0.00");
                            //txtFTotalVolRef3.Text += " " + MB.ReadTotalVolumeUnit(3, ComPortModBus);
                            //txtVelocityRef3.Text = MB.ReadVelocity(3, ComPortModBus).ToString("0.00");
                            //txtVelocityRef3.Text += " " + MB.ReadVelocityUnit(3, ComPortModBus);
                            Application.DoEvents();
                            tabCalibration.Invalidate();
 
	                }            
            #endregion

            #region Dev4
                if (chkDev4.Checked)
                {
                    txtMassFlowRef4.Text = MB.ReadMassFlow(4, ComPortModBus).ToString("0.00");
                    //txtMassFlowRef4.Text += " " + MB.ReadMassFlowUnit(4, ComPortModBus);
                    txtTemperatureRef4.Text = MB.ReadTemperature(4, ComPortModBus).ToString("0.00");
                    //txtTemperatureRef4.Text += " " + MB.ReadTemperatureUnit(4, ComPortModBus);
                    //txtFTotalMassRef4.Text = MB.ReadMassFTotal(4, ComPortModBus).ToString("0.00");
                    //txtFTotalMassRef4.Text += " " + MB.ReadTotalMassUnit(4, ComPortModBus);
                    //txtVolumeFlowRef4.Text = MB.ReadVolumeFlow(4, ComPortModBus).ToString("0.00");
                    //txtVolumeFlowRef4.Text += " " + MB.ReadVolumeFlowUnit(4, ComPortModBus);
                    txtDensityRef4.Text = MB.ReadDensity(4, ComPortModBus).ToString("0.00");
                    //txtDensityRef4.Text += " " + MB.ReadDensityUnit(4, ComPortModBus);
                    //txtFTotalVolRef4.Text = MB.ReadVolFTotal(4, ComPortModBus).ToString("0.00");
                    //txtFTotalVolRef4.Text += " " + MB.ReadTotalVolumeUnit(4, ComPortModBus);
                    //txtVelocityRef4.Text = MB.ReadVelocity(4, ComPortModBus).ToString("0.00");
                    //txtVelocityRef4.Text += " " + MB.ReadVelocityUnit(4, ComPortModBus);
                    Application.DoEvents();
                    tabCalibration.Invalidate();
                }
            #endregion

                //txtSucPress1.Text = MB.GetSuctionPressure(6, ComPortModBus).ToString("0.00");
                //txtSucPress2.Text = MB.GetSuctionPressure(8, ComPortModBus).ToString("0.00");
                //txtEndPress1.Text = MB.GetEndPressure(6, ComPortModBus).ToString("0.00");
                //txtEndPress2.Text = MB.GetEndPressure(8, ComPortModBus).ToString("0.00");

        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if(!tmrMeasurements.Enabled)
                tmrMeasurements.Enabled = true;
            IsCalibStarted = false;
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            if (tmrMeasurements.Enabled)
                tmrMeasurements.Enabled = false;
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            MB.EnableCounterReset(1, ComPortModBus);
            MB.ResetCounter(1, Modbus.CounterTypesReset.All_totals, ComPortModBus);
            MB.EnableCounterReset(2, ComPortModBus);
            MB.ResetCounter(2, Modbus.CounterTypesReset.All_totals, ComPortModBus);
            MB.EnableCounterReset(3, ComPortModBus);
            MB.ResetCounter(3, Modbus.CounterTypesReset.All_totals, ComPortModBus);
            MB.EnableCounterReset(4, ComPortModBus);
            MB.ResetCounter(4, Modbus.CounterTypesReset.All_totals, ComPortModBus);
        }

        private void btnAutozero_Click(object sender, EventArgs e)
        {
            MB.ExecuteAutozero(1, ComPortModBus);
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 2) //selectedindex = 1     //check if calibration tab is aktive
            {
                if (!tmrTempMeasurement.Enabled)
                    tmrTempMeasurement.Enabled = true;

                if (rbtncustomerDefined.Checked == false)      //check if customerdefined calibration is selected
                {
                    if (rbtn3Point.Checked && txtFlowLRV.Text != string.Empty && txtFlowURV.Text != string.Empty)   //check if 3 point calibration is selected
                    {
                        if (chboSkipZero.Checked)
                        {
                            txtPoint1.Text = (Convert.ToDouble(txtFlowURV.Text) * 1 / 3).ToString("0.00");
                            txtPoint2.Text = (Convert.ToDouble(txtFlowURV.Text) * 2 / 3).ToString("0.00");
                            txtPoint3.Text = Convert.ToDouble(txtFlowURV.Text).ToString("0.00");
                        }
                        else
                        {
                            txtPoint1.Text = Convert.ToDouble(txtFlowLRV.Text).ToString("0.00");
                            txtPoint2.Text = (Convert.ToDouble(txtFlowURV.Text) * 0.5).ToString("0.00");
                            txtPoint3.Text = Convert.ToDouble(txtFlowURV.Text).ToString("0.00");
                        }
                    }
                    else if (rbtn5Point.Checked && txtFlowLRV.Text != string.Empty && txtFlowURV.Text != string.Empty)  //check if 5 point calibration is selected
                    {
                        if (chboSkipZero.Checked)
                        {
                            txtPoint1.Text = (Convert.ToDouble(txtFlowURV.Text) * 0.2).ToString("0.00");
                            txtPoint2.Text = (Convert.ToDouble(txtFlowURV.Text) * 0.4).ToString("0.00");
                            txtPoint3.Text = (Convert.ToDouble(txtFlowURV.Text) * 0.6).ToString("0.00");
                            txtPoint4.Text = (Convert.ToDouble(txtFlowURV.Text) * 0.8).ToString("0.00");
                            txtPoint5.Text = Convert.ToDouble(txtFlowURV.Text).ToString("0.00");
                        }
                        else
                        {
                            txtPoint1.Text = Convert.ToDouble(txtFlowLRV.Text).ToString("0.00");
                            txtPoint2.Text = (Convert.ToDouble(txtFlowURV.Text) * 0.25).ToString("0.00");
                            txtPoint3.Text = (Convert.ToDouble(txtFlowURV.Text) * 0.5).ToString("0.00");
                            txtPoint4.Text = (Convert.ToDouble(txtFlowURV.Text) * 0.75).ToString("0.00");
                            txtPoint5.Text = Convert.ToDouble(txtFlowURV.Text).ToString("0.00");
                        }
                    }
                    else if (rbtn10Point.Checked && txtFlowLRV.Text != string.Empty && txtFlowURV.Text != string.Empty) //check if 10 point calibration is selected
                    {
                        if (chboSkipZero.Checked)
                        {
                            txtPoint1.Text = (Convert.ToDouble(txtFlowURV.Text) * 1 / 10).ToString("0.00");
                            txtPoint2.Text = (Convert.ToDouble(txtFlowURV.Text) * 2 / 10).ToString("0.00");
                            txtPoint3.Text = (Convert.ToDouble(txtFlowURV.Text) * 3 / 10).ToString("0.00");
                            txtPoint4.Text = (Convert.ToDouble(txtFlowURV.Text) * 4 / 10).ToString("0.00");
                            txtPoint5.Text = (Convert.ToDouble(txtFlowURV.Text) * 5 / 10).ToString("0.00");
                            txtPoint6.Text = (Convert.ToDouble(txtFlowURV.Text) * 6 / 10).ToString("0.00");
                            txtPoint7.Text = (Convert.ToDouble(txtFlowURV.Text) * 7 / 10).ToString("0.00");
                            txtPoint8.Text = (Convert.ToDouble(txtFlowURV.Text) * 8 / 10).ToString("0.00");
                            txtPoint9.Text = (Convert.ToDouble(txtFlowURV.Text) * 9 / 10).ToString("0.00");
                            txtPoint10.Text = Convert.ToDouble(txtFlowURV.Text).ToString("0.00");
                        }
                        else
                        {
                            txtPoint1.Text = Convert.ToDouble(txtFlowLRV.Text).ToString("0.00");
                            txtPoint2.Text = (Convert.ToDouble(txtFlowURV.Text) * 1 / 9).ToString("0.00");
                            txtPoint3.Text = (Convert.ToDouble(txtFlowURV.Text) * 2 / 9).ToString("0.00");
                            txtPoint4.Text = (Convert.ToDouble(txtFlowURV.Text) * 3 / 9).ToString("0.00");
                            txtPoint5.Text = (Convert.ToDouble(txtFlowURV.Text) * 4 / 9).ToString("0.00");
                            txtPoint6.Text = (Convert.ToDouble(txtFlowURV.Text) * 5 / 9).ToString("0.00");
                            txtPoint7.Text = (Convert.ToDouble(txtFlowURV.Text) * 6 / 9).ToString("0.00");
                            txtPoint8.Text = (Convert.ToDouble(txtFlowURV.Text) * 7 / 9).ToString("0.00");
                            txtPoint9.Text = (Convert.ToDouble(txtFlowURV.Text) * 8 / 9).ToString("0.00");
                            txtPoint10.Text = Convert.ToDouble(txtFlowURV.Text).ToString("0.00");
                        }
                    }
                }
            }
            else
            {
                if (tmrMeasurements.Enabled)
                    tmrMeasurements.Enabled = false;
            }
  
        }

        private void rbtnCurrentActive_CheckedChanged(object sender, EventArgs e)
        {
            chboxextCurrent.Visible = false;
            if(rbtnCurrentActive.Checked)
            {
                FA_M3.OpenRelay(2, 2, ComPortFA_M3);
                arrTextComment[1] = "The calibration data are based on the current output of the device.";
                txtComment.Lines = arrTextComment;
                lblDUT_LRV.Text = "Current LRV:";
                txtPulseCurLRV.Text = "4";
                lblDUT_URV.Text = "Current URV:";
                txtPulseCurURV.Text = "20";
                lblDUT_Unit.Text = "mA";
            }
        }

        private void rbtnCurrentPassive_CheckedChanged(object sender, EventArgs e)
        {
            chboxextCurrent.Visible = false;
            if (rbtnCurrentPassive.Checked)
            {
                FA_M3.CloseRelay(2, 2, ComPortFA_M3);
                arrTextComment[1] = "The calibration data are based on the current output of the device.";
                txtComment.Lines = arrTextComment;
                lblDUT_LRV.Text = "Current LRV:";
                txtPulseCurLRV.Text = "4";
                lblDUT_URV.Text = "Current URV:";
                txtPulseCurURV.Text = "20";
                lblDUT_Unit.Text = "mA";
            }

        }

        private void rbtnPulseActive_CheckedChanged(object sender, EventArgs e)
        {
            chboxextCurrent.Visible =false;
            if(rbtnPulseActive.Checked)
            {
                chboxextCurrent.Visible=true;
                FA_M3.CloseRelay(2, 1, ComPortFA_M3);
                arrTextComment[1] = "The calibration data are based on the pulse output of the device.";
                txtComment.Lines = arrTextComment;
                lblDUT_LRV.Text = "Pulse LRV:";
                txtPulseCurLRV.Text = "0";
                lblDUT_URV.Text = "Pulse URV:";
                txtPulseCurURV.Text = "10000";
                lblDUT_Unit.Text = "Hz";
            }
        }

        private void rbtnPulsePassive_CheckedChanged(object sender, EventArgs e)
        {
            chboxextCurrent.Visible = false;
            if (rbtnPulsePassive.Checked)
            {
                chboxextCurrent.Visible = true;
                FA_M3.OpenRelay(2, 1, ComPortFA_M3);
                arrTextComment[1] = "The calibration data are based on the pulse output of the device.";
                txtComment.Lines = arrTextComment;
                lblDUT_LRV.Text = "Pulse LRV:";
                txtPulseCurLRV.Text = "0";
                lblDUT_URV.Text = "Pulse URV:";
                txtPulseCurURV.Text = "10000";
                lblDUT_Unit.Text = "Hz";
            }
        }

        private void btnPrintCertificate_Click(object sender, EventArgs e)
        {
            txtDensityRef1.Text = CalcDensityMean().ToString("0.000");
            MakeCalCertPDF();
        }

        private void cboTestTime_SelectedIndexChanged(object sender, EventArgs e)
        {
            TestTime = Convert.ToInt32(cboTestTime.Text);
            toolStripProgressBar1.Maximum = TestTime;
        }

        private void cboDevice_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cboDevice.SelectedIndex)
            {
                case 0:
                    txtPoint1MaxErr.Text = setting.MaxErrorRotamass.ToString();
                    txtPoint2MaxErr.Text = setting.MaxErrorRotamass.ToString();
                    txtPoint3MaxErr.Text = setting.MaxErrorRotamass.ToString();
                    txtPoint4MaxErr.Text = setting.MaxErrorRotamass.ToString();
                    txtPoint5MaxErr.Text = setting.MaxErrorRotamass.ToString();
                    txtPoint6MaxErr.Text = setting.MaxErrorRotamass.ToString();
                    txtPoint7MaxErr.Text = setting.MaxErrorRotamass.ToString();
                    txtPoint8MaxErr.Text = setting.MaxErrorRotamass.ToString();
                    txtPoint9MaxErr.Text = setting.MaxErrorRotamass.ToString();
                    txtPoint10MaxErr.Text = setting.MaxErrorRotamass.ToString();
                    txtManufacturer.Text = "Yokogawa";
                    txtDeviceType.Text = "Coriolis";
                    break;
                case 1:
                    txtPoint1MaxErr.Text = setting.MaxErrorRotameter.ToString();
                    txtPoint2MaxErr.Text = setting.MaxErrorRotameter.ToString();
                    txtPoint3MaxErr.Text = setting.MaxErrorRotameter.ToString();
                    txtPoint4MaxErr.Text = setting.MaxErrorRotameter.ToString();
                    txtPoint5MaxErr.Text = setting.MaxErrorRotameter.ToString();
                    txtPoint6MaxErr.Text = setting.MaxErrorRotameter.ToString();
                    txtPoint7MaxErr.Text = setting.MaxErrorRotameter.ToString();
                    txtPoint8MaxErr.Text = setting.MaxErrorRotameter.ToString();
                    txtPoint9MaxErr.Text = setting.MaxErrorRotameter.ToString();
                    txtPoint10MaxErr.Text = setting.MaxErrorRotameter.ToString();
                    txtManufacturer.Text = "Yokogawa";
                    txtDeviceType.Text = "Variable-area flowmeter";
                    break;
                case 2:
                    txtPoint1MaxErr.Text = setting.MaxErrorDY.ToString();
                    txtPoint2MaxErr.Text = setting.MaxErrorDY.ToString();
                    txtPoint3MaxErr.Text = setting.MaxErrorDY.ToString();
                    txtPoint4MaxErr.Text = setting.MaxErrorDY.ToString();
                    txtPoint5MaxErr.Text = setting.MaxErrorDY.ToString();
                    txtPoint6MaxErr.Text = setting.MaxErrorDY.ToString();
                    txtPoint7MaxErr.Text = setting.MaxErrorDY.ToString();
                    txtPoint8MaxErr.Text = setting.MaxErrorDY.ToString();
                    txtPoint9MaxErr.Text = setting.MaxErrorDY.ToString();
                    txtPoint10MaxErr.Text = setting.MaxErrorDY.ToString();
                    txtManufacturer.Text = "Yokogawa";
                    txtDeviceType.Text = "Vortex";
                    break;
                case 3:
                    txtPoint1MaxErr.Text = setting.MaxErrorAXF.ToString();
                    txtPoint2MaxErr.Text = setting.MaxErrorAXF.ToString();
                    txtPoint3MaxErr.Text = setting.MaxErrorAXF.ToString();
                    txtPoint4MaxErr.Text = setting.MaxErrorAXF.ToString();
                    txtPoint5MaxErr.Text = setting.MaxErrorAXF.ToString();
                    txtPoint6MaxErr.Text = setting.MaxErrorAXF.ToString();
                    txtPoint7MaxErr.Text = setting.MaxErrorAXF.ToString();
                    txtPoint8MaxErr.Text = setting.MaxErrorAXF.ToString();
                    txtPoint9MaxErr.Text = setting.MaxErrorAXF.ToString();
                    txtPoint10MaxErr.Text = setting.MaxErrorAXF.ToString();
                    txtManufacturer.Text = "Yokogawa";
                    txtDeviceType.Text = "MAG meter";
                    break;
                case 4:
                    txtPoint1MaxErr.Text = string.Empty;
                    txtPoint2MaxErr.Text = string.Empty;
                    txtPoint3MaxErr.Text = string.Empty;
                    txtPoint4MaxErr.Text = string.Empty;
                    txtPoint5MaxErr.Text = string.Empty;
                    txtPoint6MaxErr.Text = string.Empty;
                    txtPoint7MaxErr.Text = string.Empty;
                    txtPoint8MaxErr.Text = string.Empty;
                    txtPoint9MaxErr.Text = string.Empty;
                    txtPoint10MaxErr.Text = string.Empty;
                    txtManufacturer.Text = string.Empty;
                    txtDeviceType.Text = string.Empty;
                    break;
                default:
                    break;
            }
            if (cboDevice.SelectedIndex == 0)
                gboxRotamassDta.Visible = true;
            else
                gboxRotamassDta.Visible = false;
            if (cboDevice.SelectedIndex == 2)
                gboxYewflow.Visible = true;
            else
                gboxYewflow.Visible = false;
            if (cboDevice.SelectedIndex == 3)
                gboxAdmagData.Visible = true;
            else
                gboxAdmagData.Visible = false;
            if (cboDevice.SelectedIndex == 4)
                gboxOther.Visible = true;
            else
                gboxOther.Visible = false;
        }


        private void btnSaveData_Click(object sender, EventArgs e)
        {
                DataRow newCalibDataRow = dsCalibData1.Tables[0].NewRow();
                DataRow newDeviceDataRow = dsDeviceData1.Tables[0].NewRow();

                newDeviceDataRow["SerialNo"] = txtSerialNo.Text.Trim() + "-M-" + txtCalibNo.Text.Trim();
                newDeviceDataRow["Modelcode"] = txtModelcode.Text.Trim();
                newDeviceDataRow["DateTime"] = DateTime.Now;
                newDeviceDataRow["TAGNo"] = txtTagNo.Text.Trim();
                newDeviceDataRow["CustomerName"] = txtName.Text.Trim();
                newDeviceDataRow["CustomerCity"] = txtCity.Text.Trim();
                newDeviceDataRow["CustomerAdress"] = txtAdress.Text.Trim();
                newDeviceDataRow["Device"] = cboDevice.Text.Trim();
                if (txtFlowURV.Text != string.Empty)
                    newDeviceDataRow["MFURV"] = Convert.ToDouble(txtFlowURV.Text);
                if (txtFlowLRV.Text != string.Empty)
                    newDeviceDataRow["MFLRV"] = Convert.ToDouble(txtFlowLRV.Text);
                newDeviceDataRow["MFUnit"] = cboFlowUnit.Text.Trim();

                dsDeviceData1.Tables[0].Rows.Add(newDeviceDataRow);
                oleDbDADeviceData.Update(dsDeviceData1, "Device data");

                if (txtPoint1.Text != string.Empty && txtPoint1Ref.Text != string.Empty && txtPoint1MaxErr.Text != string.Empty)
                {
                    try
                    {
                        if (txtSerialNo.Text != string.Empty)
                            newCalibDataRow["SerialNo"] = txtSerialNo.Text.Trim() + "-M-" + txtCalibNo.Text.Trim();
                        newCalibDataRow["NominalFlow1"] = Convert.ToDouble(txtPoint1.Text);
                        newCalibDataRow["ReferenceFlow1"] = Convert.ToDouble(txtPoint1Ref.Text);
                        newCalibDataRow["CustomerFlow1"] = Convert.ToDouble(txtPoint1Cust.Text);
                        newCalibDataRow["ErrorAbs1"] = Convert.ToDouble(txtPoint1ErrAbs.Text);
                        newCalibDataRow["ErrorRel1"] = Convert.ToDouble(txtPoint1ErrRel.Text);
                        newCalibDataRow["MaxError1"] = Convert.ToDouble(txtPoint1MaxErr.Text);
                        newCalibDataRow["WaterTempVessel1"] = Convert.ToDouble(strWaterTempVessel[0]);
                        newCalibDataRow["WaterTempRig1_1"] = Convert.ToDouble(strWaterTempRig[0]);
                        newCalibDataRow["WaterTempCustomer1"] = Convert.ToDouble(strWaterTempCustDevice[0]);
                        newCalibDataRow["AmbientTemp1"] = Convert.ToDouble(strAmbTemp[0]);
                        newCalibDataRow["AmbientHumidity1"] = Convert.ToDouble(strAmbHumidity[0]);
                        newCalibDataRow["AtmosphericPressure1"] = Convert.ToDouble(strAmbBaroPress[0]);
                        newCalibDataRow["Density1"] = Convert.ToDouble(density[0]);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }

                if (txtPoint2.Text != string.Empty && txtPoint2Ref.Text != string.Empty && txtPoint2MaxErr.Text != string.Empty)
                {
                    try
                    {
                        newCalibDataRow["NominalFlow2"] = Convert.ToDouble(txtPoint2.Text);
                        newCalibDataRow["ReferenceFlow2"] = Convert.ToDouble(txtPoint2Ref.Text);
                        newCalibDataRow["CustomerFlow2"] = Convert.ToDouble(txtPoint2Cust.Text);
                        newCalibDataRow["ErrorAbs2"] = Convert.ToDouble(txtPoint2ErrAbs.Text);
                        newCalibDataRow["ErrorRel2"] = Convert.ToDouble(txtPoint2ErrRel.Text);
                        newCalibDataRow["MaxError2"] = Convert.ToDouble(txtPoint2MaxErr.Text);
                        newCalibDataRow["WaterTempVessel2"] = Convert.ToDouble(strWaterTempVessel[1]);
                        newCalibDataRow["WaterTempRig1_2"] = Convert.ToDouble(strWaterTempRig[1]);
                        newCalibDataRow["WaterTempCustomer2"] = Convert.ToDouble(strWaterTempCustDevice[1]);
                        newCalibDataRow["AmbientTemp2"] = Convert.ToDouble(strAmbTemp[1]);
                        newCalibDataRow["AmbientHumidity2"] = Convert.ToDouble(strAmbHumidity[1]);
                        newCalibDataRow["AtmosphericPressure2"] = Convert.ToDouble(strAmbBaroPress[1]);
                        newCalibDataRow["Density2"] = Convert.ToDouble(density[2]);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }

                if (txtPoint3.Text != string.Empty && txtPoint3Ref.Text != string.Empty && txtPoint3MaxErr.Text != string.Empty)
                {
                    try
                    {
                        newCalibDataRow["NominalFlow3"] = Convert.ToDouble(txtPoint3.Text);
                        newCalibDataRow["ReferenceFlow3"] = Convert.ToDouble(txtPoint3Ref.Text);
                        newCalibDataRow["CustomerFlow3"] = Convert.ToDouble(txtPoint3Cust.Text);
                        newCalibDataRow["ErrorAbs3"] = Convert.ToDouble(txtPoint3ErrAbs.Text);
                        newCalibDataRow["ErrorRel3"] = Convert.ToDouble(txtPoint3ErrRel.Text);
                        newCalibDataRow["MaxError3"] = Convert.ToDouble(txtPoint3MaxErr.Text);
                        newCalibDataRow["WaterTempVessel3"] = Convert.ToDouble(strWaterTempVessel[2]);
                        newCalibDataRow["WaterTempRig1_3"] = Convert.ToDouble(strWaterTempRig[2]);
                        newCalibDataRow["WaterTempCustomer3"] = Convert.ToDouble(strWaterTempCustDevice[2]);
                        newCalibDataRow["AmbientTemp3"] = Convert.ToDouble(strAmbTemp[2]);
                        newCalibDataRow["AmbientHumidity3"] = Convert.ToDouble(strAmbHumidity[2]);
                        newCalibDataRow["AtmosphericPressure3"] = Convert.ToDouble(strAmbBaroPress[2]);
                        newCalibDataRow["Density3"] = Convert.ToDouble(density[4]);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }

                if (txtPoint4.Text != string.Empty && txtPoint4Ref.Text != string.Empty && txtPoint4MaxErr.Text != string.Empty)
                {
                    try
                    {
                        newCalibDataRow["NominalFlow4"] = Convert.ToDouble(txtPoint4.Text);
                        newCalibDataRow["ReferenceFlow4"] = Convert.ToDouble(txtPoint4Ref.Text);
                        newCalibDataRow["CustomerFlow4"] = Convert.ToDouble(txtPoint4Cust.Text);
                        newCalibDataRow["ErrorAbs4"] = Convert.ToDouble(txtPoint4ErrAbs.Text);
                        newCalibDataRow["ErrorRel4"] = Convert.ToDouble(txtPoint4ErrRel.Text);
                        newCalibDataRow["MaxError4"] = Convert.ToDouble(txtPoint4MaxErr.Text);
                        newCalibDataRow["WaterTempVessel4"] = Convert.ToDouble(strWaterTempVessel[3]);
                        newCalibDataRow["WaterTempRig1_4"] = Convert.ToDouble(strWaterTempRig[3]);
                        newCalibDataRow["WaterTempCustomer4"] = Convert.ToDouble(strWaterTempCustDevice[3]);
                        newCalibDataRow["AmbientTemp4"] = Convert.ToDouble(strAmbTemp[3]);
                        newCalibDataRow["AmbientHumidity4"] = Convert.ToDouble(strAmbHumidity[3]);
                        newCalibDataRow["AtmosphericPressure4"] = Convert.ToDouble(strAmbBaroPress[3]);
                        newCalibDataRow["Density4"] = Convert.ToDouble(density[6]);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }

                if (txtPoint5.Text != string.Empty && txtPoint5Ref.Text != string.Empty && txtPoint5MaxErr.Text != string.Empty)
                {
                    newCalibDataRow["NominalFlow5"] = Convert.ToDouble(txtPoint5.Text);
                    newCalibDataRow["ReferenceFlow5"] = Convert.ToDouble(txtPoint5Ref.Text);
                    newCalibDataRow["CustomerFlow5"] = Convert.ToDouble(txtPoint5Cust.Text);
                    newCalibDataRow["ErrorAbs5"] = Convert.ToDouble(txtPoint5ErrAbs.Text);
                    newCalibDataRow["ErrorRel5"] = Convert.ToDouble(txtPoint5ErrRel.Text);
                    newCalibDataRow["MaxError5"] = Convert.ToDouble(txtPoint5MaxErr.Text);
                    newCalibDataRow["WaterTempVessel5"] = Convert.ToDouble(strWaterTempVessel[4]);
                    newCalibDataRow["WaterTempRig1_5"] = Convert.ToDouble(strWaterTempRig[4]);
                    newCalibDataRow["WaterTempCustomer5"] = Convert.ToDouble(strWaterTempCustDevice[4]);
                    newCalibDataRow["AmbientTemp5"] = Convert.ToDouble(strAmbTemp[4]);
                    newCalibDataRow["AmbientHumidity5"] = Convert.ToDouble(strAmbHumidity[4]);
                    newCalibDataRow["AtmosphericPressure5"] = Convert.ToDouble(strAmbBaroPress[4]);
                    newCalibDataRow["Density5"] = Convert.ToDouble(density[8]);
                }

                if (txtPoint6.Text != string.Empty && txtPoint6Ref.Text != string.Empty && txtPoint6MaxErr.Text != string.Empty)
                {
                    try
                    {
                        newCalibDataRow["NominalFlow6"] = Convert.ToDouble(txtPoint6.Text);
                        newCalibDataRow["ReferenceFlow6"] = Convert.ToDouble(txtPoint6Ref.Text);
                        newCalibDataRow["CustomerFlow6"] = Convert.ToDouble(txtPoint6Cust.Text);
                        newCalibDataRow["ErrorAbs6"] = Convert.ToDouble(txtPoint6ErrAbs.Text);
                        newCalibDataRow["ErrorRel6"] = Convert.ToDouble(txtPoint6ErrRel.Text);
                        newCalibDataRow["MaxError6"] = Convert.ToDouble(txtPoint6MaxErr.Text);
                        newCalibDataRow["WaterTempVessel6"] = Convert.ToDouble(strWaterTempVessel[5]);
                        newCalibDataRow["WaterTempRig1_6"] = Convert.ToDouble(strWaterTempRig[5]);
                        newCalibDataRow["WaterTempCustomer6"] = Convert.ToDouble(strWaterTempCustDevice[5]);
                        newCalibDataRow["AmbientTemp6"] = Convert.ToDouble(strAmbTemp[5]);
                        newCalibDataRow["AmbientHumidity6"] = Convert.ToDouble(strAmbHumidity[5]);
                        newCalibDataRow["AtmosphericPressure6"] = Convert.ToDouble(strAmbBaroPress[5]);
                        newCalibDataRow["Density6"] = Convert.ToDouble(density[10]);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }

                if (txtPoint7.Text != string.Empty && txtPoint7Ref.Text != string.Empty && txtPoint7MaxErr.Text != string.Empty)
                {
                    newCalibDataRow["NominalFlow7"] = Convert.ToDouble(txtPoint7.Text);
                    newCalibDataRow["ReferenceFlow7"] = Convert.ToDouble(txtPoint7Ref.Text);
                    newCalibDataRow["CustomerFlow7"] = Convert.ToDouble(txtPoint7Cust.Text);
                    newCalibDataRow["ErrorAbs7"] = Convert.ToDouble(txtPoint7ErrAbs.Text);
                    newCalibDataRow["ErrorRel7"] = Convert.ToDouble(txtPoint7ErrRel.Text);
                    newCalibDataRow["WaterTempVessel7"] = Convert.ToDouble(strWaterTempVessel[6]);
                    newCalibDataRow["WaterTempRig1_7"] = Convert.ToDouble(strWaterTempRig[6]);
                    newCalibDataRow["WaterTempCustomer7"] = Convert.ToDouble(strWaterTempCustDevice[6]);
                    newCalibDataRow["AmbientTemp7"] = Convert.ToDouble(strAmbTemp[6]);
                    newCalibDataRow["AmbientHumidity7"] = Convert.ToDouble(strAmbHumidity[6]);
                    newCalibDataRow["AtmosphericPressure7"] = Convert.ToDouble(strAmbBaroPress[6]);
                    newCalibDataRow["Density7"] = Convert.ToDouble(density[12]);
                }

                if (txtPoint8.Text != string.Empty && txtPoint8Ref.Text != string.Empty && txtPoint8MaxErr.Text != string.Empty)
                {
                    try
                    {
                        newCalibDataRow["NominalFlow8"] = Convert.ToDouble(txtPoint8.Text);
                        newCalibDataRow["ReferenceFlow8"] = Convert.ToDouble(txtPoint8Ref.Text);
                        newCalibDataRow["CustomerFlow8"] = Convert.ToDouble(txtPoint8Cust.Text);
                        newCalibDataRow["ErrorAbs8"] = Convert.ToDouble(txtPoint8ErrAbs.Text);
                        newCalibDataRow["ErrorRel8"] = Convert.ToDouble(txtPoint8ErrRel.Text);
                        newCalibDataRow["WaterTempVessel8"] = Convert.ToDouble(strWaterTempVessel[7]);
                        newCalibDataRow["WaterTempRig1_8"] = Convert.ToDouble(strWaterTempRig[7]);
                        newCalibDataRow["WaterTempCustomer8"] = Convert.ToDouble(strWaterTempCustDevice[7]);
                        newCalibDataRow["AmbientTemp8"] = Convert.ToDouble(strAmbTemp[7]);
                        newCalibDataRow["AmbientHumidity8"] = Convert.ToDouble(strAmbHumidity[7]);
                        newCalibDataRow["AtmosphericPressure8"] = Convert.ToDouble(strAmbBaroPress[7]);
                        newCalibDataRow["Density8"] = Convert.ToDouble(density[14]);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }

                if (txtPoint9.Text != string.Empty && txtPoint9Ref.Text != string.Empty && txtPoint9MaxErr.Text != string.Empty)
                {
                    try
                    {
                        newCalibDataRow["NominalFlow9"] = Convert.ToDouble(txtPoint9.Text);
                        newCalibDataRow["ReferenceFlow9"] = Convert.ToDouble(txtPoint9Ref.Text);
                        newCalibDataRow["CustomerFlow9"] = Convert.ToDouble(txtPoint9Cust.Text);
                        newCalibDataRow["ErrorAbs9"] = Convert.ToDouble(txtPoint9ErrAbs.Text);
                        newCalibDataRow["ErrorRel9"] = Convert.ToDouble(txtPoint9ErrRel.Text);
                        newCalibDataRow["MaxError9"] = Convert.ToDouble(txtPoint9MaxErr.Text);
                        newCalibDataRow["WaterTempVessel9"] = Convert.ToDouble(strWaterTempVessel[8]);
                        newCalibDataRow["WaterTempRig1_9"] = Convert.ToDouble(strWaterTempRig[8]);
                        newCalibDataRow["WaterTempCustomer9"] = Convert.ToDouble(strWaterTempCustDevice[8]);
                        newCalibDataRow["AmbientTemp9"] = Convert.ToDouble(strAmbTemp[8]);
                        newCalibDataRow["AmbientHumidity9"] = Convert.ToDouble(strAmbHumidity[8]);
                        newCalibDataRow["AtmosphericPressure9"] = Convert.ToDouble(strAmbBaroPress[8]);
                        newCalibDataRow["Density9"] = Convert.ToDouble(density[16]);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }

                if (txtPoint10.Text != string.Empty && txtPoint10Ref.Text != string.Empty && txtPoint10MaxErr.Text != string.Empty)
                {
                    try
                    {
                        newCalibDataRow["NominalFlow10"] = Convert.ToDouble(txtPoint10.Text);
                        newCalibDataRow["ReferenceFlow10"] = Convert.ToDouble(txtPoint10Ref.Text);
                        newCalibDataRow["CustomerFlow10"] = Convert.ToDouble(txtPoint10Cust.Text);
                        newCalibDataRow["ErrorAbs10"] = Convert.ToDouble(txtPoint10ErrAbs.Text);
                        newCalibDataRow["ErrorRel10"] = Convert.ToDouble(txtPoint10ErrRel.Text);
                        newCalibDataRow["WaterTempVessel10"] = Convert.ToDouble(strWaterTempVessel[9]);
                        newCalibDataRow["WaterTempRig1_10"] = Convert.ToDouble(strWaterTempRig[9]);
                        newCalibDataRow["WaterTempCustomer10"] = Convert.ToDouble(strWaterTempCustDevice[9]);
                        newCalibDataRow["AmbientTemp10"] = Convert.ToDouble(strAmbTemp[9]);
                        newCalibDataRow["AmbientHumidity10"] = Convert.ToDouble(strAmbHumidity[9]);
                        newCalibDataRow["AtmosphericPressure10"] = Convert.ToDouble(strAmbBaroPress[9]);
                        newCalibDataRow["Density10"] = Convert.ToDouble(density[18]);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }

                dsCalibData1.Tables[0].Rows.Add(newCalibDataRow);
                oleDbDACalibData.Update(dsCalibData1, "Calib data");
        }

        private void CalibButtonDisable ()
        {
            btnCalibPoint1.Enabled = false;
            btnCalibPoint2.Enabled = false;
            btnCalibPoint3.Enabled = false;
            btnCalibPoint4.Enabled = false;
            btnCalibPoint5.Enabled = false;
            btnCalibPoint6.Enabled = false;
            btnCalibPoint7.Enabled = false;
            btnCalibPoint8.Enabled = false;
            btnCalibPoint9.Enabled = false;
            btnCalibPoint10.Enabled = false;
        }
        private void CalibButtonEnable()
        {
            btnCalibPoint1.Enabled = true;
            btnCalibPoint2.Enabled = true;
            btnCalibPoint3.Enabled = true;
            btnCalibPoint4.Enabled = true;
            btnCalibPoint5.Enabled = true;
            btnCalibPoint6.Enabled = true;
            btnCalibPoint7.Enabled = true;
            btnCalibPoint8.Enabled = true;
            btnCalibPoint9.Enabled = true;
            btnCalibPoint10.Enabled = true;
        }
        #region calibpoint 1
        private void btnCalibPoint1_Click(object sender, EventArgs e)
        {

            if(txtPoint1.Text != string.Empty && !(rbtnCurrentActive.Checked==false && rbtnCurrentPassive.Checked==false 
                && rbtnPulseActive.Checked==false && rbtnPulsePassive.Checked==false))
            {
                CalibButtonDisable();
                float[] arrFreq = new float[5];
                string unit = string.Empty;
                byte RefDevice = 0;
                double CustValue = 0, RefValue = 0;
                float Temp1 = 0, Temp2 = 0, Temp3 = 0, Temp4 = 0;
                float AmbTemp = 0, AmbPres = 0, Ambhumi = 0;
                float Val1 = 0, Val2 = 0, Val3 = 0, Val4 = 0, Val5 = 0, Val6 = 0, Val7 = 0, Val8 = 0;
                float[] PressDN25 = new float[4];
                float[] PressDN50 = new float[4];
                float pressureDN25 = 0;
                float pressureDN50 = 0;

                tmrMFMeasurement.Enabled = false;
                tmrMeasurements.Enabled = false;
                tmrTempMeasurement.Enabled = false;
                FA_M3.IsCalibCanceled = false;
                btnCancelCalib.Focus();

                FA_M3.Read4TempValues(3, ComPortFA_M3, ref Temp1, ref Temp2, ref Temp3, ref Temp4);
                strWaterTempVessel[0] = Temp1.ToString("0.00");
                strWaterTempRig[0] = Temp2.ToString("0.00");
                txtTempCabinet1.Text = Temp3.ToString("0.00");
                if (Temp2 < 15 || Temp2 > 35)
                {
                    MessageBox.Show("Media temperature outside calibration range!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                //####################################################
                //get 4 Pressure values
                for (int j = 0; j < 4; j++)
                {
                    FA_M3.Read8AnalogInputValues(4, ComPortFA_M3, ref Val1, ref Val2, ref Val3, ref Val4, ref Val5, ref Val6, ref Val7, ref Val8);
                    PressDN25[j] = CalculatePressure(0, 35, Val1);
                    PressDN50[j] = CalculatePressure(0, 35, Val2);
                }
                RefDevice = GetReferenceDevice(Convert.ToDouble(txtPoint1.Text));
                //TODO einfügen wenn Hardware vollständig
                //if (RefDevice == 32 || RefDevice == 34)
                //{
                //    pressureDN25 = CalcMean(PressDN25,4);
                //    MB.WritPressure(1, pressureDN25, ComPortModBus);
                //    MB.WritPressure(2, pressureDN25, ComPortModBus);
                //}
                //else if (RefDevice == 36 || RefDevice == 38)
                //{
                //    pressureDN50 = CalcMean(PressDN50,4);
                //    MB.WritPressure(3, pressureDN50, ComPortModBus);
                //    MB.WritPressure(4, pressureDN50, ComPortModBus);
                //}
                //######################################################    
                
                //Ambient Conditions
                AmbLog.TaskStart(COMPortAmbientTemp);
                System.Threading.Thread.Sleep(1000);        //wait for updates
                AmbLog.ReadMesureData(COMPortAmbientTemp, ref AmbPres, ref Ambhumi, ref AmbTemp, 0, 5, 6);
                strAmbBaroPress[0] = AmbPres.ToString("0.0");
                strAmbHumidity[0] = Ambhumi.ToString("0.0");
                strAmbTemp[0] = AmbTemp.ToString("0.0");
                strWaterTempCustDevice[0] = CustTemp.Get_Temp(COMPortCustTemp).ToString("0.00");

                //get density from selected device
                if (RefDevice == 32)
                    DevAdr = 1;
                else if (RefDevice == 34)
                    DevAdr = 2;
                else if (RefDevice == 36)
                    DevAdr = 3;
                else if (RefDevice == 38)
                    DevAdr = 4;
                density[0] = MB.ReadDensity(DevAdr, ComPortModBus);     //density measure
                //#############

                FA_M3.GetFrequency(ref arrFreq[0], ref arrFreq[1], ref arrFreq[2], ref arrFreq[3], ref arrFreq[4], TestTime, ref CurrentValue, ComPortFA_M3, toolStripProgressBar1);
                //#############
                float temp_refval = 0;
                //check Reference Device
                if (RefDevice == 32)
                    temp_refval = CalculateMassflowFrequenz(Convert.ToSingle(setting.CS32LowRange), Convert.ToSingle(setting.CS32HighRange),0, Convert.ToSingle(setting.CS32PulseRate), arrFreq[0]);
                else if (RefDevice == 34)
                    temp_refval = CalculateMassflowFrequenz(Convert.ToSingle(setting.CS34LowRange), Convert.ToSingle(setting.CS34HighRange),0, Convert.ToSingle(setting.CS34PulseRate), arrFreq[1]);
                else if (RefDevice == 36)
                    temp_refval = CalculateMassflowFrequenz(Convert.ToSingle(setting.CS36LowRange), Convert.ToSingle(setting.CS36HighRange), 0, Convert.ToSingle(setting.CS36PulseRate), arrFreq[2]);
                else if (RefDevice == 38)
                    temp_refval = CalculateMassflowFrequenz(Convert.ToSingle(setting.CS38LowRange), Convert.ToSingle(setting.CS38HighRange), 0, Convert.ToSingle(setting.CS38PulseRate), arrFreq[3]);
                
                if (cboFlowUnit.SelectedIndex == 1 || cboFlowUnit.SelectedIndex == 3)
                    temp_refval = temp_refval / (float)1000;
                
                if (temp_refval > (float)999.999)     //check the length of the number
                    txtPoint1Ref.Text = temp_refval.ToString("0.00");          //show the number in textbox
                else
                    txtPoint1Ref.Text = temp_refval.ToString("0.000");
                //############
                density[1] = MB.ReadDensity(DevAdr, ComPortModBus);     //density measure
                //############
                float temp_custval = 0;
                if (rbtnDisplay.Checked)
                {
                    EnterValue.ShowDialog();
                    txtPoint1Cust.Text = MFCust;
                }
                else if (rbtnHART.Checked)
                { }
                else if (rbtnBRAIN.Checked)
                { }
                else if (rbtnPulseActive.Checked || rbtnPulsePassive.Checked)
                    temp_custval = CalculateMassflowFrequenz(Convert.ToSingle(txtMFLRV.Text), Convert.ToSingle(txtMFURV.Text), Convert.ToSingle(txtPulseCurLRV.Text), Convert.ToSingle(txtPulseCurURV.Text), arrFreq[4]);
                else if (rbtnCurrentActive.Checked || rbtnCurrentPassive.Checked)
                    temp_custval = CalculateMassflowCurrent(Convert.ToSingle(txtMFLRV.Text), Convert.ToSingle(txtMFURV.Text), Convert.ToSingle(txtPulseCurLRV.Text), Convert.ToSingle(txtPulseCurURV.Text), CurrentValue);
                else
                    MessageBox.Show("No output selected", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                if (temp_custval > (float)999.999)
                    txtPoint1Cust.Text = temp_custval.ToString("0.00");
                else
                    txtPoint1Cust.Text = temp_custval.ToString("0.000");
                
                RefValue = Convert.ToDouble(txtPoint1Ref.Text);
                CustValue = Convert.ToDouble(txtPoint1Cust.Text);

                txtPoint1ErrAbs.Text = (CustValue - RefValue).ToString("0.000");
                txtPoint1ErrRel.Text = (Convert.ToDouble(txtPoint1ErrAbs.Text) * 100.0 / Convert.ToDouble(txtPoint1Ref.Text)).ToString("0.00");

                //MB.WritPressure(1, 0, ComPortModBus);
                //MB.WritPressure(2, 0, ComPortModBus);
                //MB.WritPressure(3, 0, ComPortModBus);
                //MB.WritPressure(4, 0, ComPortModBus);

                tmrMFMeasurement.Enabled = true;
                tmrTempMeasurement.Enabled = true;
                CalibButtonEnable();
            }
            else
                MessageBox.Show("Please insert the Nominal Value" + "\n" + "or no output selected", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);          
        }
        #endregion calibpoint 1

        #region calibpoint 2
        private void btncalibPoint2_Click(object sender, EventArgs e)
        {
            if (txtPoint2.Text != string.Empty && !(rbtnCurrentActive.Checked == false && rbtnCurrentPassive.Checked == false
                && rbtnPulseActive.Checked == false && rbtnPulsePassive.Checked == false))
            {
                CalibButtonDisable();
                float[] arrFreq = new float[5];
                string unit = string.Empty;
                byte RefDevice = 0;
                double CustValue = 0, RefValue = 0;
                float Temp1 = 0, Temp2 = 0, Temp3 = 0, Temp4 = 0;
                float AmbTemp = 0, AmbPres = 0, Ambhumi = 0;
                float Val1 = 0, Val2 = 0, Val3 = 0, Val4 = 0, Val5 = 0, Val6 = 0, Val7 = 0, Val8 = 0;
                float[] PressDN25 = new float[4];
                float[] PressDN50 = new float[4];
                float pressureDN25 = 0;
                float pressureDN50 = 0;
                tmrMFMeasurement.Enabled = false;
                tmrMeasurements.Enabled = false;
                tmrTempMeasurement.Enabled = false;
                FA_M3.IsCalibCanceled = false;
                btnCancelCalib.Focus();

                FA_M3.Read4TempValues(3, ComPortFA_M3, ref Temp1, ref Temp2, ref Temp3, ref Temp4);
                strWaterTempVessel[1] = Temp1.ToString("0.00");
                strWaterTempRig[1] = Temp2.ToString("0.00");
                txtTempCabinet1.Text = Temp3.ToString("0.00");
                if (Temp2 < 15 || Temp2 > 35)
                {
                    MessageBox.Show("Media temperature outside calibration range!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                //####################################################
                //get 4 Pressure values
                for (int j = 0; j < 4; j++)
                {
                    FA_M3.Read8AnalogInputValues(4, ComPortFA_M3, ref Val1, ref Val2, ref Val3, ref Val4, ref Val5, ref Val6, ref Val7, ref Val8);
                    PressDN25[j] = CalculatePressure(0, 35, Val1);
                    PressDN50[j] = CalculatePressure(0, 35, Val2);
                }
                RefDevice = GetReferenceDevice(Convert.ToDouble(txtPoint1.Text));
                //if (RefDevice == 32 || RefDevice == 34)
                //{
                //    pressureDN25 = CalcMean(PressDN25, 4);
                //    MB.WritPressure(1, pressureDN25, ComPortModBus);
                //    MB.WritPressure(2, pressureDN25, ComPortModBus);
                //}
                //else if (RefDevice == 36 || RefDevice == 38)
                //{
                //    pressureDN50 = CalcMean(PressDN50, 4);
                //    MB.WritPressure(3, pressureDN50, ComPortModBus);
                //    MB.WritPressure(4, pressureDN50, ComPortModBus);
                //}
                //###################################################### 
                
                //Ambient Conditions
                AmbLog.TaskStart(COMPortAmbientTemp);
                System.Threading.Thread.Sleep(1000);        //wait for updates
                AmbLog.ReadMesureData(COMPortAmbientTemp, ref AmbPres, ref Ambhumi, ref AmbTemp, 0, 5, 6);
                strAmbBaroPress[1] = AmbPres.ToString("0.0");
                strAmbHumidity[1] = Ambhumi.ToString("0.0");
                strAmbTemp[1] = AmbTemp.ToString("0.0");

                RefDevice = GetReferenceDevice(Convert.ToDouble(txtPoint2.Text));
                strWaterTempCustDevice[1] = CustTemp.Get_Temp(COMPortCustTemp).ToString("0.00");
                //get density from selected device
                if (RefDevice == 32)
                    DevAdr = 1;
                else if (RefDevice == 34)
                    DevAdr = 2;
                else if (RefDevice == 36)
                    DevAdr = 3;
                else if (RefDevice == 38)
                    DevAdr = 4;
                density[2] = MB.ReadDensity(DevAdr, ComPortModBus);     //density measure

                FA_M3.GetFrequency(ref arrFreq[0], ref arrFreq[1], ref arrFreq[2], ref arrFreq[3], ref arrFreq[4], TestTime, ref CurrentValue, ComPortFA_M3, toolStripProgressBar1);

                float temp_refval = 0;
                //check Reference Device
                if (RefDevice == 32)
                    temp_refval = CalculateMassflowFrequenz(Convert.ToSingle(setting.CS32LowRange), Convert.ToSingle(setting.CS32HighRange), 0, Convert.ToSingle(setting.CS32PulseRate), arrFreq[0]);
                else if (RefDevice == 34)
                    temp_refval = CalculateMassflowFrequenz(Convert.ToSingle(setting.CS34LowRange), Convert.ToSingle(setting.CS34HighRange), 0, Convert.ToSingle(setting.CS34PulseRate), arrFreq[1]);
                else if (RefDevice == 36)
                    temp_refval = CalculateMassflowFrequenz(Convert.ToSingle(setting.CS36LowRange), Convert.ToSingle(setting.CS36HighRange), 0, Convert.ToSingle(setting.CS36PulseRate), arrFreq[2]);
                else if (RefDevice == 38)
                    temp_refval = CalculateMassflowFrequenz(Convert.ToSingle(setting.CS38LowRange), Convert.ToSingle(setting.CS38HighRange), 0, Convert.ToSingle(setting.CS38PulseRate), arrFreq[3]);

                if (cboFlowUnit.SelectedIndex == 1 || cboFlowUnit.SelectedIndex == 3)
                    temp_refval = temp_refval / (float)1000;

                if (temp_refval > (float)999.999)     //check the length of the number
                    txtPoint2Ref.Text = temp_refval.ToString("0.00");          //show the number in textbox
                else
                    txtPoint2Ref.Text = temp_refval.ToString("0.000");
                //##################
                density[3] = MB.ReadDensity(DevAdr, ComPortModBus);     //density measure
                //############
                float temp_custval = 0;
                if (rbtnDisplay.Checked)
                {
                    EnterValue.ShowDialog();
                    txtPoint2Cust.Text = MFCust;
                }
                else if (rbtnHART.Checked)
                { }
                else if (rbtnBRAIN.Checked)
                { }
                else if (rbtnPulseActive.Checked || rbtnPulsePassive.Checked)
                    temp_custval = CalculateMassflowFrequenz(Convert.ToSingle(txtMFLRV.Text), Convert.ToSingle(txtMFURV.Text), Convert.ToSingle(txtPulseCurLRV.Text), Convert.ToSingle(txtPulseCurURV.Text), arrFreq[4]);
                else if (rbtnCurrentActive.Checked || rbtnCurrentPassive.Checked)
                    temp_custval = CalculateMassflowCurrent(Convert.ToSingle(txtMFLRV.Text), Convert.ToSingle(txtMFURV.Text),Convert.ToSingle(txtPulseCurLRV.Text),Convert.ToSingle(txtPulseCurURV.Text), CurrentValue);
                else
                    MessageBox.Show("No output selected", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                if (temp_custval > (float)999.999)
                    txtPoint2Cust.Text = temp_custval.ToString("0.00");
                else
                    txtPoint2Cust.Text = temp_custval.ToString("0.000");

                RefValue = Convert.ToDouble(txtPoint2Ref.Text);
                CustValue = Convert.ToDouble(txtPoint2Cust.Text);

                txtPoint2ErrAbs.Text = (CustValue - RefValue).ToString("0.000");
                txtPoint2ErrRel.Text = (Convert.ToDouble(txtPoint2ErrAbs.Text) * 100.0 / Convert.ToDouble(txtPoint2Ref.Text)).ToString("0.00");

                //MB.WritPressure(1, 0, ComPortModBus);
                //MB.WritPressure(2, 0, ComPortModBus);
                //MB.WritPressure(3, 0, ComPortModBus);
                //MB.WritPressure(4, 0, ComPortModBus);

                tmrMFMeasurement.Enabled = true;
                tmrTempMeasurement.Enabled = true;
                CalibButtonEnable();
            }
            else
                MessageBox.Show("Please insert the Nominal Value" + "\n" + "or no output selected", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        #endregion calibpoint 2

        #region calibpoint 3
        private void btnCalibPoint3_Click(object sender, EventArgs e)
        {
            if (txtPoint3.Text != string.Empty && !(rbtnCurrentActive.Checked == false && rbtnCurrentPassive.Checked == false
                && rbtnPulseActive.Checked == false && rbtnPulsePassive.Checked == false))
            {
                CalibButtonDisable();
                float[] arrFreq = new float[5];
                string unit = string.Empty;
                byte RefDevice = 0;
                double CustValue = 0, RefValue = 0;
                float Temp1 = 0, Temp2 = 0, Temp3 = 0, Temp4 = 0;
                float AmbTemp = 0, AmbPres = 0, Ambhumi = 0;
                float Val1 = 0, Val2 = 0, Val3 = 0, Val4 = 0, Val5 = 0, Val6 = 0, Val7 = 0, Val8 = 0;
                float[] PressDN25 = new float[4];
                float[] PressDN50 = new float[4];
                float pressureDN25 = 0;
                float pressureDN50 = 0;

                tmrMFMeasurement.Enabled = false;
                tmrMeasurements.Enabled = false;
                tmrTempMeasurement.Enabled = false;
                FA_M3.IsCalibCanceled = false;
                btnCancelCalib.Focus();

                FA_M3.Read4TempValues(3, ComPortFA_M3, ref Temp1, ref Temp2, ref Temp3, ref Temp4);
                strWaterTempVessel[2] = Temp1.ToString("0.00");
                strWaterTempRig[2] = Temp2.ToString("0.00");
                txtTempCabinet1.Text = Temp3.ToString("0.00");
                if (Temp2 < 15 || Temp2 > 35)
                {
                    MessageBox.Show("Media temperature outside calibration range!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                //####################################################
                //get 4 Pressure values
                for (int j = 0; j < 4; j++)
                {
                    FA_M3.Read8AnalogInputValues(4, ComPortFA_M3, ref Val1, ref Val2, ref Val3, ref Val4, ref Val5, ref Val6, ref Val7, ref Val8);
                    PressDN25[j] = CalculatePressure(0, 35, Val1);
                    PressDN50[j] = CalculatePressure(0, 35, Val2);
                }
                RefDevice = GetReferenceDevice(Convert.ToDouble(txtPoint1.Text));
                //if (RefDevice == 32 || RefDevice == 34)
                //{
                //    pressureDN25 = CalcMean(PressDN25, 4);
                //    MB.WritPressure(1, pressureDN25, ComPortModBus);
                //    MB.WritPressure(2, pressureDN25, ComPortModBus);
                //}
                //else if (RefDevice == 36 || RefDevice == 38)
                //{
                //    pressureDN50 = CalcMean(PressDN50, 4);
                //    MB.WritPressure(3, pressureDN50, ComPortModBus);
                //    MB.WritPressure(4, pressureDN50, ComPortModBus);
                //}
                //###################################################### 

                //Ambient Conditions
                AmbLog.TaskStart(COMPortAmbientTemp);
                System.Threading.Thread.Sleep(1000);        //wait for updates
                AmbLog.ReadMesureData(COMPortAmbientTemp, ref AmbPres, ref Ambhumi, ref AmbTemp, 0, 5, 6);
                strAmbBaroPress[2] = AmbPres.ToString("0.0");
                strAmbHumidity[2] = Ambhumi.ToString("0.0");
                strAmbTemp[2] = AmbTemp.ToString("0.0");

                RefDevice = GetReferenceDevice(Convert.ToDouble(txtPoint3.Text));
                strWaterTempCustDevice[2] = CustTemp.Get_Temp(COMPortCustTemp).ToString("0.00");

                //get density from selected device
                if (RefDevice == 32)
                    DevAdr = 1;
                else if (RefDevice == 34)
                    DevAdr = 2;
                else if (RefDevice == 36)
                    DevAdr = 3;
                else if (RefDevice == 38)
                    DevAdr = 4;
                density[4] = MB.ReadDensity(DevAdr, ComPortModBus);     //density measure

                FA_M3.GetFrequency(ref arrFreq[0], ref arrFreq[1], ref arrFreq[2], ref arrFreq[3], ref arrFreq[4], TestTime, ref CurrentValue, ComPortFA_M3, toolStripProgressBar1);

                float temp_refval = 0;
                //check Reference Device
                if (RefDevice == 32)
                    temp_refval = CalculateMassflowFrequenz(Convert.ToSingle(setting.CS32LowRange), Convert.ToSingle(setting.CS32HighRange), 0, Convert.ToSingle(setting.CS32PulseRate), arrFreq[0]);
                else if (RefDevice == 34)
                    temp_refval = CalculateMassflowFrequenz(Convert.ToSingle(setting.CS34LowRange), Convert.ToSingle(setting.CS34HighRange), 0, Convert.ToSingle(setting.CS34PulseRate), arrFreq[1]);
                else if (RefDevice == 36)
                    temp_refval = CalculateMassflowFrequenz(Convert.ToSingle(setting.CS36LowRange), Convert.ToSingle(setting.CS36HighRange), 0, Convert.ToSingle(setting.CS36PulseRate), arrFreq[2]);
                else if (RefDevice == 38)
                    temp_refval = CalculateMassflowFrequenz(Convert.ToSingle(setting.CS38LowRange), Convert.ToSingle(setting.CS38HighRange), 0, Convert.ToSingle(setting.CS38PulseRate), arrFreq[3]);

                if (cboFlowUnit.SelectedIndex == 1 || cboFlowUnit.SelectedIndex == 3)
                    temp_refval = temp_refval / (float)1000;

                if (temp_refval > (float)999.999)     //check the length of the number
                    txtPoint3Ref.Text = temp_refval.ToString("0.00");          //show the number in textbox
                else
                    txtPoint3Ref.Text = temp_refval.ToString("0.000");
                //############
                density[5] = MB.ReadDensity(DevAdr, ComPortModBus);     //density measure
                //############
                float temp_custval = 0;
                if (rbtnDisplay.Checked)
                {
                    EnterValue.ShowDialog();
                    txtPoint3Cust.Text = MFCust;
                }
                else if (rbtnHART.Checked)
                { }
                else if (rbtnBRAIN.Checked)
                { }
                else if (rbtnPulseActive.Checked || rbtnPulsePassive.Checked)
                    temp_custval = CalculateMassflowFrequenz(Convert.ToSingle(txtMFLRV.Text), Convert.ToSingle(txtMFURV.Text), Convert.ToSingle(txtPulseCurLRV.Text), Convert.ToSingle(txtPulseCurURV.Text), arrFreq[4]);
                else if (rbtnCurrentActive.Checked || rbtnCurrentPassive.Checked)
                    temp_custval = CalculateMassflowCurrent(Convert.ToSingle(txtMFLRV.Text), Convert.ToSingle(txtMFURV.Text), Convert.ToSingle(txtPulseCurLRV.Text), Convert.ToSingle(txtPulseCurURV.Text), CurrentValue);
                else
                    MessageBox.Show("No output selected", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                if (temp_custval > (float)999.999)
                    txtPoint3Cust.Text = temp_custval.ToString("0.00");
                else
                    txtPoint3Cust.Text = temp_custval.ToString("0.000");
                //############

                RefValue = Convert.ToDouble(txtPoint3Ref.Text);
                CustValue = Convert.ToDouble(txtPoint3Cust.Text);

                txtPoint3ErrAbs.Text = (CustValue - RefValue).ToString("0.000");
                txtPoint3ErrRel.Text = (Convert.ToDouble(txtPoint3ErrAbs.Text) * 100.0 / Convert.ToDouble(txtPoint3Ref.Text)).ToString("0.00");

                //MB.WritPressure(1, 0, ComPortModBus);
                //MB.WritPressure(2, 0, ComPortModBus);
                //MB.WritPressure(3, 0, ComPortModBus);
                //MB.WritPressure(4, 0, ComPortModBus);

                tmrMFMeasurement.Enabled = true;
                tmrTempMeasurement.Enabled = true;
                CalibButtonEnable();
            }
            else
                MessageBox.Show("Please insert the Nominal Value" + "\n" + "or no output selected", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        #endregion calibpoint 3

        #region claibpoint 4
        private void btnCalibPoint4_Click(object sender, EventArgs e)
        {
            if (txtPoint4.Text != string.Empty && !(rbtnCurrentActive.Checked == false && rbtnCurrentPassive.Checked == false
                && rbtnPulseActive.Checked == false && rbtnPulsePassive.Checked == false))
            {
                CalibButtonDisable();
                float[] arrFreq = new float[5];
                string unit = string.Empty;
                byte RefDevice = 0;
                double CustValue = 0, RefValue = 0;
                float Temp1 = 0, Temp2 = 0, Temp3 = 0, Temp4 = 0;
                float AmbTemp = 0, AmbPres = 0, Ambhumi = 0;
                float Val1 = 0, Val2 = 0, Val3 = 0, Val4 = 0, Val5 = 0, Val6 = 0, Val7 = 0, Val8 = 0;
                float[] PressDN25 = new float[4];
                float[] PressDN50 = new float[4];
                float pressureDN25 = 0;
                float pressureDN50 = 0;
                tmrMFMeasurement.Enabled = false;
                tmrMeasurements.Enabled = false;
                tmrTempMeasurement.Enabled = false;
                FA_M3.IsCalibCanceled = false;
                btnCancelCalib.Focus();

                FA_M3.Read4TempValues(3, ComPortFA_M3, ref Temp1, ref Temp2, ref Temp3, ref Temp4);
                strWaterTempVessel[3] = Temp1.ToString("0.00");
                strWaterTempRig[3] = Temp2.ToString("0.00");
                txtTempCabinet1.Text = Temp3.ToString("0.00");
                if (Temp2 < 15 || Temp2 > 35)
                {
                    MessageBox.Show("Media temperature outside calibration range!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                //####################################################
                //get 4 Pressure values
                for (int j = 0; j < 4; j++)
                {
                    FA_M3.Read8AnalogInputValues(4, ComPortFA_M3, ref Val1, ref Val2, ref Val3, ref Val4, ref Val5, ref Val6, ref Val7, ref Val8);
                    PressDN25[j] = CalculatePressure(0, 35, Val1);
                    PressDN50[j] = CalculatePressure(0, 35, Val2);
                }
                RefDevice = GetReferenceDevice(Convert.ToDouble(txtPoint1.Text));
                //if (RefDevice == 32 || RefDevice == 34)
                //{
                //    pressureDN25 = CalcMean(PressDN25, 4);
                //    MB.WritPressure(1, pressureDN25, ComPortModBus);
                //    MB.WritPressure(2, pressureDN25, ComPortModBus);
                //}
                //else if (RefDevice == 36 || RefDevice == 38)
                //{
                //    pressureDN50 = CalcMean(PressDN50, 4);
                //    MB.WritPressure(3, pressureDN50, ComPortModBus);
                //    MB.WritPressure(4, pressureDN50, ComPortModBus);
                //}
                //###################################################### 

                //Ambient Conditions
                AmbLog.TaskStart(COMPortAmbientTemp);
                System.Threading.Thread.Sleep(1000);        //wait for updates
                AmbLog.ReadMesureData(COMPortAmbientTemp, ref AmbPres, ref Ambhumi, ref AmbTemp, 0, 5, 6);
                strAmbBaroPress[3] = AmbPres.ToString("0.0");
                strAmbHumidity[3] = Ambhumi.ToString("0.0");
                strAmbTemp[3] = AmbTemp.ToString("0.0");

                RefDevice = GetReferenceDevice(Convert.ToDouble(txtPoint4.Text));
                strWaterTempCustDevice[3] = CustTemp.Get_Temp(COMPortCustTemp).ToString("0.00");

                //get density from selected device
                if (RefDevice == 32)
                    DevAdr = 1;
                else if (RefDevice == 34)
                    DevAdr = 2;
                else if (RefDevice == 36)
                    DevAdr = 3;
                else if (RefDevice == 38)
                    DevAdr = 4;
                density[6] = MB.ReadDensity(DevAdr, ComPortModBus);     //density measure

                FA_M3.GetFrequency(ref arrFreq[0], ref arrFreq[1], ref arrFreq[2], ref arrFreq[3], ref arrFreq[4], TestTime, ref CurrentValue, ComPortFA_M3, toolStripProgressBar1);

                float temp_refval = 0;
                //check Reference Device
                if (RefDevice == 32)
                    temp_refval = CalculateMassflowFrequenz(Convert.ToSingle(setting.CS32LowRange), Convert.ToSingle(setting.CS32HighRange), 0, Convert.ToSingle(setting.CS32PulseRate), arrFreq[0]);
                else if (RefDevice == 34)
                    temp_refval = CalculateMassflowFrequenz(Convert.ToSingle(setting.CS34LowRange), Convert.ToSingle(setting.CS34HighRange), 0, Convert.ToSingle(setting.CS34PulseRate), arrFreq[1]);
                else if (RefDevice == 36)
                    temp_refval = CalculateMassflowFrequenz(Convert.ToSingle(setting.CS36LowRange), Convert.ToSingle(setting.CS36HighRange), 0, Convert.ToSingle(setting.CS36PulseRate), arrFreq[2]);
                else if (RefDevice == 38)
                    temp_refval = CalculateMassflowFrequenz(Convert.ToSingle(setting.CS38LowRange), Convert.ToSingle(setting.CS38HighRange), 0, Convert.ToSingle(setting.CS38PulseRate), arrFreq[3]);

                if (cboFlowUnit.SelectedIndex == 1 || cboFlowUnit.SelectedIndex == 3)
                    temp_refval = temp_refval / (float)1000;

                if (temp_refval > (float)999.999)     //check the length of the number
                    txtPoint4Ref.Text = temp_refval.ToString("0.00");          //show the number in textbox
                else
                    txtPoint4Ref.Text = temp_refval.ToString("0.000");
                //############
                density[7] = MB.ReadDensity(DevAdr, ComPortModBus);     //density measure
                //############
                float temp_custval = 0;
                if (rbtnDisplay.Checked)
                {
                    EnterValue.ShowDialog();
                    txtPoint4Cust.Text = MFCust;
                }
                else if (rbtnHART.Checked)
                { }
                else if (rbtnBRAIN.Checked)
                { }
                else if (rbtnPulseActive.Checked || rbtnPulsePassive.Checked)
                    temp_custval = CalculateMassflowFrequenz(Convert.ToSingle(txtMFLRV.Text), Convert.ToSingle(txtMFURV.Text), Convert.ToSingle(txtPulseCurLRV.Text), Convert.ToSingle(txtPulseCurURV.Text), arrFreq[4]);
                else if (rbtnCurrentActive.Checked || rbtnCurrentPassive.Checked)
                    temp_custval = CalculateMassflowCurrent(Convert.ToSingle(txtMFLRV.Text), Convert.ToSingle(txtMFURV.Text), Convert.ToSingle(txtPulseCurLRV.Text), Convert.ToSingle(txtPulseCurURV.Text), CurrentValue);
                else
                    MessageBox.Show("No output selected", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                if (temp_custval > (float)999.999)
                    txtPoint4Cust.Text = temp_custval.ToString("0.00");
                else
                    txtPoint4Cust.Text = temp_custval.ToString("0.000");

                RefValue = Convert.ToDouble(txtPoint4Ref.Text);
                CustValue = Convert.ToDouble(txtPoint4Cust.Text);

                txtPoint4ErrAbs.Text = (CustValue - RefValue).ToString("0.000");
                txtPoint4ErrRel.Text = (Convert.ToDouble(txtPoint4ErrAbs.Text) * 100.0 / Convert.ToDouble(txtPoint4Ref.Text)).ToString("0.00");

                //MB.WritPressure(1, 0, ComPortModBus);
                //MB.WritPressure(2, 0, ComPortModBus);
                //MB.WritPressure(3, 0, ComPortModBus);
                //MB.WritPressure(4, 0, ComPortModBus);

                tmrMFMeasurement.Enabled = true;
                tmrTempMeasurement.Enabled = true;
                CalibButtonEnable();
            }
            else
                MessageBox.Show("Please insert the Nominal Value" + "\n" + "or no output selected", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        #endregion calibpoint 4

        #region calibpoint 5
        private void btnCalibPoint5_Click(object sender, EventArgs e)
        {
            if (txtPoint5.Text != string.Empty && !(rbtnCurrentActive.Checked == false && rbtnCurrentPassive.Checked == false
                && rbtnPulseActive.Checked == false && rbtnPulsePassive.Checked == false))
            {
                CalibButtonDisable();
                float[] arrFreq = new float[5];
                string unit = string.Empty;
                byte RefDevice = 0;
                double CustValue = 0, RefValue = 0;
                float Temp1 = 0, Temp2 = 0, Temp3 = 0, Temp4 = 0;
                float AmbTemp = 0, AmbPres = 0, Ambhumi = 0;
                float Val1 = 0, Val2 = 0, Val3 = 0, Val4 = 0, Val5 = 0, Val6 = 0, Val7 = 0, Val8 = 0;
                float[] PressDN25 = new float[4];
                float[] PressDN50 = new float[4];
                float pressureDN25 = 0;
                float pressureDN50 = 0;
                tmrMFMeasurement.Enabled = false;
                tmrMeasurements.Enabled = false;
                tmrTempMeasurement.Enabled = false;
                FA_M3.IsCalibCanceled = false;
                btnCancelCalib.Focus();

                FA_M3.Read4TempValues(3, ComPortFA_M3, ref Temp1, ref Temp2, ref Temp3, ref Temp4);
                strWaterTempVessel[4] = Temp1.ToString("0.00");
                strWaterTempRig[4] = Temp2.ToString("0.00");
                txtTempCabinet1.Text = Temp3.ToString("0.00");
                if (Temp2 < 15 || Temp2 > 35)
                {
                    MessageBox.Show("Media temperature outside calibration range!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                //####################################################
                //get 4 Pressure values
                for (int j = 0; j < 4; j++)
                {
                    FA_M3.Read8AnalogInputValues(4, ComPortFA_M3, ref Val1, ref Val2, ref Val3, ref Val4, ref Val5, ref Val6, ref Val7, ref Val8);
                    PressDN25[j] = CalculatePressure(0, 35, Val1);
                    PressDN50[j] = CalculatePressure(0, 35, Val2);
                }
                RefDevice = GetReferenceDevice(Convert.ToDouble(txtPoint1.Text));
                //if (RefDevice == 32 || RefDevice == 34)
                //{
                //    pressureDN25 = CalcMean(PressDN25, 4);
                //    MB.WritPressure(1, pressureDN25, ComPortModBus);
                //    MB.WritPressure(2, pressureDN25, ComPortModBus);
                //}
                //else if (RefDevice == 36 || RefDevice == 38)
                //{
                //    pressureDN50 = CalcMean(PressDN50, 4);
                //    MB.WritPressure(3, pressureDN50, ComPortModBus);
                //    MB.WritPressure(4, pressureDN50, ComPortModBus);
                //}
                //###################################################### 

                //Ambient Conditions
                AmbLog.TaskStart(COMPortAmbientTemp);
                System.Threading.Thread.Sleep(1000);        //wait for updates
                AmbLog.ReadMesureData(COMPortAmbientTemp, ref AmbPres, ref Ambhumi, ref AmbTemp, 0, 5, 6);
                strAmbBaroPress[4] = AmbPres.ToString("0.0");
                strAmbHumidity[4] = Ambhumi.ToString("0.0");
                strAmbTemp[4] = AmbTemp.ToString("0.0");

                RefDevice = GetReferenceDevice(Convert.ToDouble(txtPoint5.Text));
                strWaterTempCustDevice[4] = CustTemp.Get_Temp(COMPortCustTemp).ToString("0.00");

                //get density from selected device
                if (RefDevice == 32)
                    DevAdr = 1;
                else if (RefDevice == 34)
                    DevAdr = 2;
                else if (RefDevice == 36)
                    DevAdr = 3;
                else if (RefDevice == 38)
                    DevAdr = 4;
                density[8] = MB.ReadDensity(DevAdr, ComPortModBus);     //density measure

                FA_M3.GetFrequency(ref arrFreq[0], ref arrFreq[1], ref arrFreq[2], ref arrFreq[3], ref arrFreq[4], TestTime, ref CurrentValue, ComPortFA_M3, toolStripProgressBar1);

                //#############
                float temp_refval = 0;
                //check Reference Device
                if (RefDevice == 32)
                    temp_refval = CalculateMassflowFrequenz(Convert.ToSingle(setting.CS32LowRange), Convert.ToSingle(setting.CS32HighRange), 0, Convert.ToSingle(setting.CS32PulseRate), arrFreq[0]);
                else if (RefDevice == 34)
                    temp_refval = CalculateMassflowFrequenz(Convert.ToSingle(setting.CS34LowRange), Convert.ToSingle(setting.CS34HighRange), 0, Convert.ToSingle(setting.CS34PulseRate), arrFreq[1]);
                else if (RefDevice == 36)
                    temp_refval = CalculateMassflowFrequenz(Convert.ToSingle(setting.CS36LowRange), Convert.ToSingle(setting.CS36HighRange), 0, Convert.ToSingle(setting.CS36PulseRate), arrFreq[2]);
                else if (RefDevice == 38)
                    temp_refval = CalculateMassflowFrequenz(Convert.ToSingle(setting.CS38LowRange), Convert.ToSingle(setting.CS38HighRange), 0, Convert.ToSingle(setting.CS38PulseRate), arrFreq[3]);

                if (cboFlowUnit.SelectedIndex == 1 || cboFlowUnit.SelectedIndex == 3)
                    temp_refval = temp_refval / (float)1000;

                if (temp_refval > (float)999.999)     //check the length of the number
                    txtPoint5Ref.Text = temp_refval.ToString("0.00");          //show the number in textbox
                else
                    txtPoint5Ref.Text = temp_refval.ToString("0.000");
                //############
                density[9] = MB.ReadDensity(DevAdr, ComPortModBus);     //density measure
                //############
                float temp_custval = 0;
                if (rbtnDisplay.Checked)
                {
                    EnterValue.ShowDialog();
                    txtPoint5Cust.Text = MFCust;
                }
                else if (rbtnHART.Checked)
                { }
                else if (rbtnBRAIN.Checked)
                { }
                else if (rbtnPulseActive.Checked || rbtnPulsePassive.Checked)
                    temp_custval = CalculateMassflowFrequenz(Convert.ToSingle(txtMFLRV.Text), Convert.ToSingle(txtMFURV.Text), Convert.ToSingle(txtPulseCurLRV.Text), Convert.ToSingle(txtPulseCurURV.Text), arrFreq[4]);
                else if (rbtnCurrentActive.Checked || rbtnCurrentPassive.Checked)
                    temp_custval = CalculateMassflowCurrent(Convert.ToSingle(txtMFLRV.Text), Convert.ToSingle(txtMFURV.Text), Convert.ToSingle(txtPulseCurLRV.Text), Convert.ToSingle(txtPulseCurURV.Text), CurrentValue);
                else
                    MessageBox.Show("No output selected", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                if (temp_custval > (float)999.999)
                    txtPoint5Cust.Text = temp_custval.ToString("0.00");
                else
                    txtPoint5Cust.Text = temp_custval.ToString("0.000");

                RefValue = Convert.ToDouble(txtPoint5Ref.Text);
                CustValue = Convert.ToDouble(txtPoint5Cust.Text);

                txtPoint5ErrAbs.Text = (CustValue - RefValue).ToString("0.000");
                txtPoint5ErrRel.Text = (Convert.ToDouble(txtPoint5ErrAbs.Text) * 100.0 / Convert.ToDouble(txtPoint5Ref.Text)).ToString("0.00");

                //MB.WritPressure(1, 0, ComPortModBus);
                //MB.WritPressure(2, 0, ComPortModBus);
                //MB.WritPressure(3, 0, ComPortModBus);
                //MB.WritPressure(4, 0, ComPortModBus);

                tmrMFMeasurement.Enabled = true;
                tmrTempMeasurement.Enabled = true;
                CalibButtonEnable();
            }
            else
                MessageBox.Show("Please insert the Nominal Value" + "\n" + "or no output selected", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        #endregion calibpoint 5

        #region calibpoint 6
        private void btnCalibPoint6_Click(object sender, EventArgs e)
        {
            if (txtPoint6.Text != string.Empty && !(rbtnCurrentActive.Checked == false && rbtnCurrentPassive.Checked == false
                && rbtnPulseActive.Checked == false && rbtnPulsePassive.Checked == false))
            {
                CalibButtonDisable();
                float[] arrFreq = new float[5];
                string unit = string.Empty;
                byte RefDevice = 0;
                double CustValue = 0, RefValue = 0;
                float Temp1 = 0, Temp2 = 0, Temp3 = 0, Temp4 = 0;
                float AmbTemp = 0, AmbPres = 0, Ambhumi = 0;
                float Val1 = 0, Val2 = 0, Val3 = 0, Val4 = 0, Val5 = 0, Val6 = 0, Val7 = 0, Val8 = 0;
                float[] PressDN25 = new float[4];
                float[] PressDN50 = new float[4];
                float pressureDN25 = 0;
                float pressureDN50 = 0;
                tmrMFMeasurement.Enabled = false;
                tmrMeasurements.Enabled = false;
                tmrTempMeasurement.Enabled = false;
                FA_M3.IsCalibCanceled = false;
                btnCancelCalib.Focus();

                FA_M3.Read4TempValues(3, ComPortFA_M3, ref Temp1, ref Temp2, ref Temp3, ref Temp4);
                strWaterTempVessel[5] = Temp1.ToString("0.00");
                strWaterTempRig[5] = Temp2.ToString("0.00");
                txtTempCabinet1.Text = Temp3.ToString("0.00");
                if (Temp2 < 15 || Temp2 > 35)
                {
                    MessageBox.Show("Media temperature outside calibration range!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                //####################################################
                //get 4 Pressure values
                for (int j = 0; j < 4; j++)
                {
                    FA_M3.Read8AnalogInputValues(4, ComPortFA_M3, ref Val1, ref Val2, ref Val3, ref Val4, ref Val5, ref Val6, ref Val7, ref Val8);
                    PressDN25[j] = CalculatePressure(0, 35, Val1);
                    PressDN50[j] = CalculatePressure(0, 35, Val2);
                }
                RefDevice = GetReferenceDevice(Convert.ToDouble(txtPoint1.Text));
                //if (RefDevice == 32 || RefDevice == 34)
                //{
                //    pressureDN25 = CalcMean(PressDN25, 4);
                //    MB.WritPressure(1, pressureDN25, ComPortModBus);
                //    MB.WritPressure(2, pressureDN25, ComPortModBus);
                //}
                //else if (RefDevice == 36 || RefDevice == 38)
                //{
                //    pressureDN50 = CalcMean(PressDN50, 4);
                //    MB.WritPressure(3, pressureDN50, ComPortModBus);
                //    MB.WritPressure(4, pressureDN50, ComPortModBus);
                //}
                //###################################################### 

                //Ambient Conditions
                AmbLog.TaskStart(COMPortAmbientTemp);
                System.Threading.Thread.Sleep(1000);        //wait for updates
                AmbLog.ReadMesureData(COMPortAmbientTemp, ref AmbPres, ref Ambhumi, ref AmbTemp, 0, 5, 6);
                strAmbBaroPress[5] = AmbPres.ToString("0.0");
                strAmbHumidity[5] = Ambhumi.ToString("0.0");
                strAmbTemp[5] = AmbTemp.ToString("0.0");

                RefDevice = GetReferenceDevice(Convert.ToDouble(txtPoint6.Text));
                strWaterTempCustDevice[5] = CustTemp.Get_Temp(COMPortCustTemp).ToString("0.00");

                //get density from selected device
                if (RefDevice == 32)
                    DevAdr = 1;
                else if (RefDevice == 34)
                    DevAdr = 2;
                else if (RefDevice == 36)
                    DevAdr = 3;
                else if (RefDevice == 38)
                    DevAdr = 4;
                density[10] = MB.ReadDensity(DevAdr, ComPortModBus);     //density measure

                FA_M3.GetFrequency(ref arrFreq[0], ref arrFreq[1], ref arrFreq[2], ref arrFreq[3], ref arrFreq[4], TestTime, ref CurrentValue, ComPortFA_M3, toolStripProgressBar1);
                float temp_refval = 0;
                //check Reference Device
                if (RefDevice == 32)
                    temp_refval = CalculateMassflowFrequenz(Convert.ToSingle(setting.CS32LowRange), Convert.ToSingle(setting.CS32HighRange), 0, Convert.ToSingle(setting.CS32PulseRate), arrFreq[0]);
                else if (RefDevice == 34)
                    temp_refval = CalculateMassflowFrequenz(Convert.ToSingle(setting.CS34LowRange), Convert.ToSingle(setting.CS34HighRange), 0, Convert.ToSingle(setting.CS34PulseRate), arrFreq[1]);
                else if (RefDevice == 36)
                    temp_refval = CalculateMassflowFrequenz(Convert.ToSingle(setting.CS36LowRange), Convert.ToSingle(setting.CS36HighRange), 0, Convert.ToSingle(setting.CS36PulseRate), arrFreq[2]);
                else if (RefDevice == 38)
                    temp_refval = CalculateMassflowFrequenz(Convert.ToSingle(setting.CS38LowRange), Convert.ToSingle(setting.CS38HighRange), 0, Convert.ToSingle(setting.CS38PulseRate), arrFreq[3]);

                if (cboFlowUnit.SelectedIndex == 1 || cboFlowUnit.SelectedIndex == 3)
                    temp_refval = temp_refval / (float)1000;

                if (temp_refval > (float)999.999)     //check the length of the number
                    txtPoint6Ref.Text = temp_refval.ToString("0.00");          //show the number in textbox
                else
                    txtPoint6Ref.Text = temp_refval.ToString("0.000");
                //############
                density[11] = MB.ReadDensity(DevAdr, ComPortModBus);     //density measure
                //############
                float temp_custval = 0;
                if (rbtnDisplay.Checked)
                {
                    EnterValue.ShowDialog();
                    txtPoint6Cust.Text = MFCust;
                }
                else if (rbtnHART.Checked)
                { }
                else if (rbtnBRAIN.Checked)
                { }
                else if (rbtnPulseActive.Checked || rbtnPulsePassive.Checked)
                    temp_custval = CalculateMassflowFrequenz(Convert.ToSingle(txtMFLRV.Text), Convert.ToSingle(txtMFURV.Text), Convert.ToSingle(txtPulseCurLRV.Text), Convert.ToSingle(txtPulseCurURV.Text), arrFreq[4]);
                else if (rbtnCurrentActive.Checked || rbtnCurrentPassive.Checked)
                    temp_custval = CalculateMassflowCurrent(Convert.ToSingle(txtMFLRV.Text), Convert.ToSingle(txtMFURV.Text), Convert.ToSingle(txtPulseCurLRV.Text), Convert.ToSingle(txtPulseCurURV.Text), CurrentValue);
                else
                    MessageBox.Show("No output selected", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                if (temp_custval > (float)999.999)
                    txtPoint6Cust.Text = temp_custval.ToString("0.00");
                else
                    txtPoint6Cust.Text = temp_custval.ToString("0.000");

                RefValue = Convert.ToDouble(txtPoint6Ref.Text);
                CustValue = Convert.ToDouble(txtPoint6Cust.Text);

                txtPoint6ErrAbs.Text = (CustValue - RefValue).ToString("0.000");
                txtPoint6ErrRel.Text = (Convert.ToDouble(txtPoint6ErrAbs.Text) * 100.0 / Convert.ToDouble(txtPoint6Ref.Text)).ToString("0.00");

                //MB.WritPressure(1, 0, ComPortModBus);
                //MB.WritPressure(2, 0, ComPortModBus);
                //MB.WritPressure(3, 0, ComPortModBus);
                //MB.WritPressure(4, 0, ComPortModBus);

                tmrMFMeasurement.Enabled = true;
                tmrTempMeasurement.Enabled = true;
                CalibButtonEnable();
            }
            else
                MessageBox.Show("Please insert the Nominal Value" + "\n" + "or no output selected", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        #endregion calibpoint 6

        #region calibpoint 7
        private void btnCalibPoint7_Click(object sender, EventArgs e)
        {
            if (txtPoint7.Text != string.Empty && !(rbtnCurrentActive.Checked == false && rbtnCurrentPassive.Checked == false
                && rbtnPulseActive.Checked == false && rbtnPulsePassive.Checked == false))
            {
                CalibButtonDisable();
                float[] arrFreq = new float[5];
                string unit = string.Empty;
                byte RefDevice = 0;
                double CustValue = 0, RefValue = 0;
                float Temp1 = 0, Temp2 = 0, Temp3 = 0, Temp4 = 0;
                float AmbTemp = 0, AmbPres = 0, Ambhumi = 0;
                float Val1 = 0, Val2 = 0, Val3 = 0, Val4 = 0, Val5 = 0, Val6 = 0, Val7 = 0, Val8 = 0;
                float[] PressDN25 = new float[4];
                float[] PressDN50 = new float[4];
                float pressureDN25 = 0;
                float pressureDN50 = 0;
                tmrMFMeasurement.Enabled = false;
                tmrMeasurements.Enabled = false;
                tmrTempMeasurement.Enabled = false;
                FA_M3.IsCalibCanceled = false;
                btnCancelCalib.Focus();

                FA_M3.Read4TempValues(3, ComPortFA_M3, ref Temp1, ref Temp2, ref Temp3, ref Temp4);
                strWaterTempVessel[6] = Temp1.ToString("0.00");
                strWaterTempRig[6] = Temp2.ToString("0.00");
                txtTempCabinet1.Text = Temp3.ToString("0.00");
                if (Temp2 < 15 || Temp2 > 35)
                {
                    MessageBox.Show("Media temperature outside calibration range!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                //####################################################
                //get 4 Pressure values
                for (int j = 0; j < 4; j++)
                {
                    FA_M3.Read8AnalogInputValues(4, ComPortFA_M3, ref Val1, ref Val2, ref Val3, ref Val4, ref Val5, ref Val6, ref Val7, ref Val8);
                    PressDN25[j] = CalculatePressure(0, 35, Val1);
                    PressDN50[j] = CalculatePressure(0, 35, Val2);
                }
                RefDevice = GetReferenceDevice(Convert.ToDouble(txtPoint1.Text));
                //if (RefDevice == 32 || RefDevice == 34)
                //{
                //    pressureDN25 = CalcMean(PressDN25, 4);
                //    MB.WritPressure(1, pressureDN25, ComPortModBus);
                //    MB.WritPressure(2, pressureDN25, ComPortModBus);
                //}
                //else if (RefDevice == 36 || RefDevice == 38)
                //{
                //    pressureDN50 = CalcMean(PressDN50, 4);
                //    MB.WritPressure(3, pressureDN50, ComPortModBus);
                //    MB.WritPressure(4, pressureDN50, ComPortModBus);
                //}
                //###################################################### 

                //Ambient Conditions
                AmbLog.TaskStart(COMPortAmbientTemp);
                System.Threading.Thread.Sleep(1000);        //wait for updates
                AmbLog.ReadMesureData(COMPortAmbientTemp, ref AmbPres, ref Ambhumi, ref AmbTemp, 0, 5, 6);
                strAmbBaroPress[6] = AmbPres.ToString("0.0");
                strAmbHumidity[6] = Ambhumi.ToString("0.0");
                strAmbTemp[6] = AmbTemp.ToString("0.0");

                RefDevice = GetReferenceDevice(Convert.ToDouble(txtPoint7.Text));
                strWaterTempCustDevice[6] = CustTemp.Get_Temp(COMPortCustTemp).ToString("0.00");

                //get density from selected device
                if (RefDevice == 32)
                    DevAdr = 1;
                else if (RefDevice == 34)
                    DevAdr = 2;
                else if (RefDevice == 36)
                    DevAdr = 3;
                else if (RefDevice == 38)
                    DevAdr = 4;
                density[12] = MB.ReadDensity(DevAdr, ComPortModBus);     //density measure

                FA_M3.GetFrequency(ref arrFreq[0], ref arrFreq[1], ref arrFreq[2], ref arrFreq[3], ref arrFreq[4], TestTime, ref CurrentValue, ComPortFA_M3, toolStripProgressBar1);
                float temp_refval = 0;
                //check Reference Device
                if (RefDevice == 32)
                    temp_refval = CalculateMassflowFrequenz(Convert.ToSingle(setting.CS32LowRange), Convert.ToSingle(setting.CS32HighRange), 0, Convert.ToSingle(setting.CS32PulseRate), arrFreq[0]);
                else if (RefDevice == 34)
                    temp_refval = CalculateMassflowFrequenz(Convert.ToSingle(setting.CS34LowRange), Convert.ToSingle(setting.CS34HighRange), 0, Convert.ToSingle(setting.CS34PulseRate), arrFreq[1]);
                else if (RefDevice == 36)
                    temp_refval = CalculateMassflowFrequenz(Convert.ToSingle(setting.CS36LowRange), Convert.ToSingle(setting.CS36HighRange), 0, Convert.ToSingle(setting.CS36PulseRate), arrFreq[2]);
                else if (RefDevice == 38)
                    temp_refval = CalculateMassflowFrequenz(Convert.ToSingle(setting.CS38LowRange), Convert.ToSingle(setting.CS38HighRange), 0, Convert.ToSingle(setting.CS38PulseRate), arrFreq[3]);

                if (cboFlowUnit.SelectedIndex == 1 || cboFlowUnit.SelectedIndex == 3)
                    temp_refval = temp_refval / (float)1000;

                if (temp_refval > (float)999.999)     //check the length of the number
                    txtPoint7Ref.Text = temp_refval.ToString("0.00");          //show the number in textbox
                else
                    txtPoint7Ref.Text = temp_refval.ToString("0.000");
                //############
                density[13] = MB.ReadDensity(DevAdr, ComPortModBus);     //density measure
                //############
                float temp_custval = 0;
                if (rbtnDisplay.Checked)
                {
                    EnterValue.ShowDialog();
                    txtPoint7Cust.Text = MFCust;
                }
                else if (rbtnHART.Checked)
                { }
                else if (rbtnBRAIN.Checked)
                { }
                else if (rbtnPulseActive.Checked || rbtnPulsePassive.Checked)
                    temp_custval = CalculateMassflowFrequenz(Convert.ToSingle(txtMFLRV.Text), Convert.ToSingle(txtMFURV.Text), Convert.ToSingle(txtPulseCurLRV.Text), Convert.ToSingle(txtPulseCurURV.Text), arrFreq[4]);
                else if (rbtnCurrentActive.Checked || rbtnCurrentPassive.Checked)
                    temp_custval = CalculateMassflowCurrent(Convert.ToSingle(txtMFLRV.Text), Convert.ToSingle(txtMFURV.Text), Convert.ToSingle(txtPulseCurLRV.Text), Convert.ToSingle(txtPulseCurURV.Text), CurrentValue);
                else
                    MessageBox.Show("No output selected", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                if (temp_custval > (float)999.999)
                    txtPoint7Cust.Text = temp_custval.ToString("0.00");
                else
                    txtPoint7Cust.Text = temp_custval.ToString("0.000");

                RefValue = Convert.ToDouble(txtPoint7Ref.Text);
                CustValue = Convert.ToDouble(txtPoint7Cust.Text);

                txtPoint7ErrAbs.Text = (CustValue - RefValue).ToString("0.000");
                txtPoint7ErrRel.Text = (Convert.ToDouble(txtPoint7ErrAbs.Text) * 100.0 / Convert.ToDouble(txtPoint7Ref.Text)).ToString("0.00");

                //MB.WritPressure(1, 0, ComPortModBus);
                //MB.WritPressure(2, 0, ComPortModBus);
                //MB.WritPressure(3, 0, ComPortModBus);
                //MB.WritPressure(4, 0, ComPortModBus);

                tmrMFMeasurement.Enabled = true;
                tmrTempMeasurement.Enabled = true;
                CalibButtonEnable();
            }
            else
                MessageBox.Show("Please insert the Nominal Value" + "\n" + "or no output selected", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        #endregion calibpoint 7

        #region calibpoint 8
        private void btnCalibPoint8_Click(object sender, EventArgs e)
        {
            if (txtPoint8.Text != string.Empty && !(rbtnCurrentActive.Checked == false && rbtnCurrentPassive.Checked == false
                && rbtnPulseActive.Checked == false && rbtnPulsePassive.Checked == false))
            {
                CalibButtonDisable();
                float[] arrFreq = new float[5];
                string unit = string.Empty;
                byte RefDevice = 0;
                double CustValue = 0, RefValue = 0;
                float Temp1 = 0, Temp2 = 0, Temp3 = 0, Temp4 = 0;
                float AmbTemp = 0, AmbPres = 0, Ambhumi = 0;
                float Val1 = 0, Val2 = 0, Val3 = 0, Val4 = 0, Val5 = 0, Val6 = 0, Val7 = 0, Val8 = 0;
                float[] PressDN25 = new float[4];
                float[] PressDN50 = new float[4];
                float pressureDN25 = 0;
                float pressureDN50 = 0;
                tmrMFMeasurement.Enabled = false;
                tmrMeasurements.Enabled = false;
                tmrTempMeasurement.Enabled = false;
                FA_M3.IsCalibCanceled = false;
                btnCancelCalib.Focus();

                FA_M3.Read4TempValues(3, ComPortFA_M3, ref Temp1, ref Temp2, ref Temp3, ref Temp4);
                strWaterTempVessel[7] = Temp1.ToString("0.00");
                strWaterTempRig[7] = Temp2.ToString("0.00");
                txtTempCabinet1.Text = Temp3.ToString("0.00");
                if (Temp2 < 15 || Temp2 > 35)
                {
                    MessageBox.Show("Media temperature outside calibration range!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                //####################################################
                //get 4 Pressure values
                for (int j = 0; j < 4; j++)
                {
                    FA_M3.Read8AnalogInputValues(4, ComPortFA_M3, ref Val1, ref Val2, ref Val3, ref Val4, ref Val5, ref Val6, ref Val7, ref Val8);
                    PressDN25[j] = CalculatePressure(0, 35, Val1);
                    PressDN50[j] = CalculatePressure(0, 35, Val2);
                }
                RefDevice = GetReferenceDevice(Convert.ToDouble(txtPoint1.Text));
                //if (RefDevice == 32 || RefDevice == 34)
                //{
                //    pressureDN25 = CalcMean(PressDN25, 4);
                //    MB.WritPressure(1, pressureDN25, ComPortModBus);
                //    MB.WritPressure(2, pressureDN25, ComPortModBus);
                //}
                //else if (RefDevice == 36 || RefDevice == 38)
                //{
                //    pressureDN50 = CalcMean(PressDN50, 4);
                //    MB.WritPressure(3, pressureDN50, ComPortModBus);
                //    MB.WritPressure(4, pressureDN50, ComPortModBus);
                //}
                //###################################################### 

                //Ambient Conditions
                AmbLog.TaskStart(COMPortAmbientTemp);
                System.Threading.Thread.Sleep(1000);        //wait for updates
                AmbLog.ReadMesureData(COMPortAmbientTemp, ref AmbPres, ref Ambhumi, ref AmbTemp, 0, 5, 6);
                strAmbBaroPress[7] = AmbPres.ToString("0.0");
                strAmbHumidity[7] = Ambhumi.ToString("0.0");
                strAmbTemp[7] = AmbTemp.ToString("0.0");

                RefDevice = GetReferenceDevice(Convert.ToDouble(txtPoint8.Text));
                strWaterTempCustDevice[7] = CustTemp.Get_Temp(COMPortCustTemp).ToString("0.00");

                //get density from selected devicealues(3, ComPortFA_M3, ref Temp1, ref Temp2, ref Temp3, ref Temp4);

                if (RefDevice == 32)
                    DevAdr = 1;
                else if (RefDevice == 34)
                    DevAdr = 2;
                else if (RefDevice == 36)
                    DevAdr = 3;
                else if (RefDevice == 38)
                    DevAdr = 4;
                density[14] = MB.ReadDensity(DevAdr, ComPortModBus);     //density measure

                FA_M3.GetFrequency(ref arrFreq[0], ref arrFreq[1], ref arrFreq[2], ref arrFreq[3], ref arrFreq[4], TestTime, ref CurrentValue, ComPortFA_M3, toolStripProgressBar1);
                float temp_refval = 0;
                //check Reference Device
                if (RefDevice == 32)
                    temp_refval = CalculateMassflowFrequenz(Convert.ToSingle(setting.CS32LowRange), Convert.ToSingle(setting.CS32HighRange), 0, Convert.ToSingle(setting.CS32PulseRate), arrFreq[0]);
                else if (RefDevice == 34)
                    temp_refval = CalculateMassflowFrequenz(Convert.ToSingle(setting.CS34LowRange), Convert.ToSingle(setting.CS34HighRange), 0, Convert.ToSingle(setting.CS34PulseRate), arrFreq[1]);
                else if (RefDevice == 36)
                    temp_refval = CalculateMassflowFrequenz(Convert.ToSingle(setting.CS36LowRange), Convert.ToSingle(setting.CS36HighRange), 0, Convert.ToSingle(setting.CS36PulseRate), arrFreq[2]);
                else if (RefDevice == 38)
                    temp_refval = CalculateMassflowFrequenz(Convert.ToSingle(setting.CS38LowRange), Convert.ToSingle(setting.CS38HighRange), 0, Convert.ToSingle(setting.CS38PulseRate), arrFreq[3]);

                if (cboFlowUnit.SelectedIndex == 1 || cboFlowUnit.SelectedIndex == 3)
                    temp_refval = temp_refval / (float)1000;

                if (temp_refval > (float)999.999)     //check the length of the number
                    txtPoint8Ref.Text = temp_refval.ToString("0.00");          //show the number in textbox
                else
                    txtPoint8Ref.Text = temp_refval.ToString("0.000");
                //############
                density[15] = MB.ReadDensity(DevAdr, ComPortModBus);     //density measure
                //############
                float temp_custval = 0;
                if (rbtnDisplay.Checked)
                {
                    EnterValue.ShowDialog();
                    txtPoint8Cust.Text = MFCust;
                }
                else if (rbtnHART.Checked)
                { }
                else if (rbtnBRAIN.Checked)
                { }
                else if (rbtnPulseActive.Checked || rbtnPulsePassive.Checked)
                    temp_custval = CalculateMassflowFrequenz(Convert.ToSingle(txtMFLRV.Text), Convert.ToSingle(txtMFURV.Text), Convert.ToSingle(txtPulseCurLRV.Text), Convert.ToSingle(txtPulseCurURV.Text), arrFreq[4]);
                else if (rbtnCurrentActive.Checked || rbtnCurrentPassive.Checked)
                    temp_custval = CalculateMassflowCurrent(Convert.ToSingle(txtMFLRV.Text), Convert.ToSingle(txtMFURV.Text), Convert.ToSingle(txtPulseCurLRV.Text), Convert.ToSingle(txtPulseCurURV.Text), CurrentValue);
                else
                    MessageBox.Show("No output selected", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                if (temp_custval > (float)999.999)
                    txtPoint8Cust.Text = temp_custval.ToString("0.00");
                else
                    txtPoint8Cust.Text = temp_custval.ToString("0.000");

                RefValue = Convert.ToDouble(txtPoint8Ref.Text);
                CustValue = Convert.ToDouble(txtPoint8Cust.Text);

                txtPoint8ErrAbs.Text = (CustValue - RefValue).ToString("0.000");
                txtPoint8ErrRel.Text = (Convert.ToDouble(txtPoint8ErrAbs.Text) * 100.0 / Convert.ToDouble(txtPoint8Ref.Text)).ToString("0.00");

                //MB.WritPressure(1, 0, ComPortModBus);
                //MB.WritPressure(2, 0, ComPortModBus);
                //MB.WritPressure(3, 0, ComPortModBus);
                //MB.WritPressure(4, 0, ComPortModBus);

                tmrMFMeasurement.Enabled = true;
                tmrTempMeasurement.Enabled = true;
                CalibButtonEnable();
            }
            else
                MessageBox.Show("Please insert the Nominal Value" + "\n" + "or no output selected", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        #endregion calibpoint 8

        #region calibpoint 9
        private void btnCalibPoint9_Click(object sender, EventArgs e)
        {
            if (txtPoint9.Text != string.Empty && !(rbtnCurrentActive.Checked == false && rbtnCurrentPassive.Checked == false
                && rbtnPulseActive.Checked == false && rbtnPulsePassive.Checked == false))
            {
                CalibButtonDisable();
                float[] arrFreq = new float[5];
                string unit = string.Empty;
                byte RefDevice = 0;
                double CustValue = 0, RefValue = 0;
                float Temp1 = 0, Temp2 = 0, Temp3 = 0, Temp4 = 0;
                float AmbTemp = 0, AmbPres = 0, Ambhumi = 0;
                float Val1 = 0, Val2 = 0, Val3 = 0, Val4 = 0, Val5 = 0, Val6 = 0, Val7 = 0, Val8 = 0;
                float[] PressDN25 = new float[4];
                float[] PressDN50 = new float[4];
                float pressureDN25 = 0;
                float pressureDN50 = 0;
                tmrMFMeasurement.Enabled = false;
                tmrMeasurements.Enabled = false;
                tmrTempMeasurement.Enabled = false;
                FA_M3.IsCalibCanceled = false;
                btnCancelCalib.Focus();

                FA_M3.Read4TempValues(3, ComPortFA_M3, ref Temp1, ref Temp2, ref Temp3, ref Temp4);
                strWaterTempVessel[8] = Temp1.ToString("0.00");
                strWaterTempRig[8] = Temp2.ToString("0.00");
                txtTempCabinet1.Text = Temp3.ToString("0.00");
                if (Temp2 < 15 || Temp2 > 35)
                {
                    MessageBox.Show("Media temperature outside calibration range!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                //####################################################
                //get 4 Pressure values
                for (int j = 0; j < 4; j++)
                {
                    FA_M3.Read8AnalogInputValues(4, ComPortFA_M3, ref Val1, ref Val2, ref Val3, ref Val4, ref Val5, ref Val6, ref Val7, ref Val8);
                    PressDN25[j] = CalculatePressure(0, 35, Val1);
                    PressDN50[j] = CalculatePressure(0, 35, Val2);
                }
                RefDevice = GetReferenceDevice(Convert.ToDouble(txtPoint1.Text));
                //if (RefDevice == 32 || RefDevice == 34)
                //{
                //    pressureDN25 = CalcMean(PressDN25, 4);
                //    MB.WritPressure(1, pressureDN25, ComPortModBus);
                //    MB.WritPressure(2, pressureDN25, ComPortModBus);
                //}
                //else if (RefDevice == 36 || RefDevice == 38)
                //{
                //    pressureDN50 = CalcMean(PressDN50, 4);
                //    MB.WritPressure(3, pressureDN50, ComPortModBus);
                //    MB.WritPressure(4, pressureDN50, ComPortModBus);
                //}
                //###################################################### 

                //Ambient Conditions
                AmbLog.TaskStart(COMPortAmbientTemp);
                System.Threading.Thread.Sleep(1000);        //wait for updates
                AmbLog.ReadMesureData(COMPortAmbientTemp, ref AmbPres, ref Ambhumi, ref AmbTemp, 0, 5, 6);
                strAmbBaroPress[8] = AmbPres.ToString("0.0");
                strAmbHumidity[8] = Ambhumi.ToString("0.0");
                strAmbTemp[8] = AmbTemp.ToString("0.0");

                RefDevice = GetReferenceDevice(Convert.ToDouble(txtPoint9.Text));
                strWaterTempCustDevice[8] = CustTemp.Get_Temp(COMPortCustTemp).ToString("0.00");

                //get density from selected device

                if (RefDevice == 32)
                    DevAdr = 1;
                else if (RefDevice == 34)
                    DevAdr = 2;
                else if (RefDevice == 36)
                    DevAdr = 3;
                else if (RefDevice == 38)
                    DevAdr = 4;
                density[16] = MB.ReadDensity(DevAdr, ComPortModBus);     //density measure

                FA_M3.GetFrequency(ref arrFreq[0], ref arrFreq[1], ref arrFreq[2], ref arrFreq[3], ref arrFreq[4], TestTime, ref CurrentValue, ComPortFA_M3, toolStripProgressBar1);
                float temp_refval = 0;
                //check Reference Device
                if (RefDevice == 32)
                    temp_refval = CalculateMassflowFrequenz(Convert.ToSingle(setting.CS32LowRange), Convert.ToSingle(setting.CS32HighRange), 0, Convert.ToSingle(setting.CS32PulseRate), arrFreq[0]);
                else if (RefDevice == 34)
                    temp_refval = CalculateMassflowFrequenz(Convert.ToSingle(setting.CS34LowRange), Convert.ToSingle(setting.CS34HighRange), 0, Convert.ToSingle(setting.CS34PulseRate), arrFreq[1]);
                else if (RefDevice == 36)
                    temp_refval = CalculateMassflowFrequenz(Convert.ToSingle(setting.CS36LowRange), Convert.ToSingle(setting.CS36HighRange), 0, Convert.ToSingle(setting.CS36PulseRate), arrFreq[2]);
                else if (RefDevice == 38)
                    temp_refval = CalculateMassflowFrequenz(Convert.ToSingle(setting.CS38LowRange), Convert.ToSingle(setting.CS38HighRange), 0, Convert.ToSingle(setting.CS38PulseRate), arrFreq[3]);

                if (cboFlowUnit.SelectedIndex == 1 || cboFlowUnit.SelectedIndex == 3)
                    temp_refval = temp_refval / (float)1000;

                if (temp_refval > (float)999.999)     //check the length of the number
                    txtPoint9Ref.Text = temp_refval.ToString("0.00");          //show the number in textbox
                else
                    txtPoint9Ref.Text = temp_refval.ToString("0.000");
                //############
                density[17] = MB.ReadDensity(DevAdr, ComPortModBus);     //density measure
                //############
                float temp_custval = 0;
                if (rbtnDisplay.Checked)
                {
                    EnterValue.ShowDialog();
                    txtPoint9Cust.Text = MFCust;
                }
                else if (rbtnHART.Checked)
                { }
                else if (rbtnBRAIN.Checked)
                { }
                else if (rbtnPulseActive.Checked || rbtnPulsePassive.Checked)
                    temp_custval = CalculateMassflowFrequenz(Convert.ToSingle(txtMFLRV.Text), Convert.ToSingle(txtMFURV.Text), Convert.ToSingle(txtPulseCurLRV.Text), Convert.ToSingle(txtPulseCurURV.Text), arrFreq[4]);
                else if (rbtnCurrentActive.Checked || rbtnCurrentPassive.Checked)
                    temp_custval = CalculateMassflowCurrent(Convert.ToSingle(txtMFLRV.Text), Convert.ToSingle(txtMFURV.Text), Convert.ToSingle(txtPulseCurLRV.Text), Convert.ToSingle(txtPulseCurURV.Text), CurrentValue);
                else
                    MessageBox.Show("No output selected", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                if (temp_custval > (float)999.999)
                    txtPoint9Cust.Text = temp_custval.ToString("0.00");
                else
                    txtPoint9Cust.Text = temp_custval.ToString("0.000");

                RefValue = Convert.ToDouble(txtPoint9Ref.Text);
                CustValue = Convert.ToDouble(txtPoint9Cust.Text);

                txtPoint9ErrAbs.Text = (CustValue - RefValue).ToString("0.000");
                txtPoint9ErrRel.Text = (Convert.ToDouble(txtPoint9ErrAbs.Text) * 100.0 / Convert.ToDouble(txtPoint9Ref.Text)).ToString("0.00");

                //MB.WritPressure(1, 0, ComPortModBus);
                //MB.WritPressure(2, 0, ComPortModBus);
                //MB.WritPressure(3, 0, ComPortModBus);
                //MB.WritPressure(4, 0, ComPortModBus); 

                tmrMFMeasurement.Enabled = true;
                tmrTempMeasurement.Enabled = true;
                CalibButtonEnable();
            }
            else
                MessageBox.Show("Please insert the Nominal Value" + "\n" + "or no output selected", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        #endregion calibpoint 9

        #region calibpoint 10
        private void btnCalibPoint10_Click(object sender, EventArgs e)
        {
            if (txtPoint10.Text != string.Empty && !(rbtnCurrentActive.Checked == false && rbtnCurrentPassive.Checked == false
                && rbtnPulseActive.Checked == false && rbtnPulsePassive.Checked == false))
            {
                CalibButtonDisable();
                float[] arrFreq = new float[5];
                string unit = string.Empty;
                byte RefDevice = 0;
                double CustValue = 0, RefValue = 0;
                float Temp1 = 0, Temp2 = 0, Temp3 = 0, Temp4 = 0;
                float AmbTemp = 0, AmbPres = 0, Ambhumi = 0;
                float Val1 = 0, Val2 = 0, Val3 = 0, Val4 = 0, Val5 = 0, Val6 = 0, Val7 = 0, Val8 = 0;
                float[] PressDN25 = new float[4];
                float[] PressDN50 = new float[4];
                float pressureDN25 = 0;
                float pressureDN50 = 0;

                tmrMFMeasurement.Enabled = false;
                tmrMeasurements.Enabled = false;
                tmrTempMeasurement.Enabled = false;
                FA_M3.IsCalibCanceled = false;
                btnCancelCalib.Focus();

                FA_M3.Read4TempValues(3, ComPortFA_M3, ref Temp1, ref Temp2, ref Temp3, ref Temp4);
                strWaterTempVessel[9] = Temp1.ToString("0.00");
                strWaterTempRig[9] = Temp2.ToString("0.00");
                txtTempCabinet1.Text = Temp3.ToString("0.00");
                if (Temp2 < 15 || Temp2 > 35)
                {
                    MessageBox.Show("Media temperature outside calibration range!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                //####################################################
                //get 4 Pressure values
                for (int j = 0; j < 4; j++)
                {
                    FA_M3.Read8AnalogInputValues(4, ComPortFA_M3, ref Val1, ref Val2, ref Val3, ref Val4, ref Val5, ref Val6, ref Val7, ref Val8);
                    PressDN25[j] = CalculatePressure(0, 35, Val1);
                    PressDN50[j] = CalculatePressure(0, 35, Val2);
                }
                RefDevice = GetReferenceDevice(Convert.ToDouble(txtPoint1.Text));
                //if (RefDevice == 32 || RefDevice == 34)
                //{
                //    pressureDN25 = CalcMean(PressDN25, 4);
                //    MB.WritPressure(1, pressureDN25, ComPortModBus);
                //    MB.WritPressure(2, pressureDN25, ComPortModBus);
                //}
                //else if (RefDevice == 36 || RefDevice == 38)
                //{
                //    pressureDN50 = CalcMean(PressDN50, 4);
                //    MB.WritPressure(3, pressureDN50, ComPortModBus);
                //    MB.WritPressure(4, pressureDN50, ComPortModBus);
                //}
                //######################################################     

                //Ambient Conditions
                AmbLog.TaskStart(COMPortAmbientTemp);
                System.Threading.Thread.Sleep(1000);        //wait for updates
                AmbLog.ReadMesureData(COMPortAmbientTemp, ref AmbPres, ref Ambhumi, ref AmbTemp, 0, 5, 6);
                strAmbBaroPress[9] = AmbPres.ToString("0.0");
                strAmbHumidity[9] = Ambhumi.ToString("0.0");
                strAmbTemp[9] = AmbTemp.ToString("0.0");

                RefDevice = GetReferenceDevice(Convert.ToDouble(txtPoint10.Text));
                strWaterTempCustDevice[9] = CustTemp.Get_Temp(COMPortCustTemp).ToString("0.00");
                FA_M3.Read4TempValues(3, ComPortFA_M3, ref Temp1, ref Temp2, ref Temp3, ref Temp4);
                strWaterTempVessel[9] = Temp1.ToString("0.00");
                strWaterTempRig[9] = Temp2.ToString("0.00");
                txtTempCabinet1.Text = Temp3.ToString("0.00");
                //get density from selected device
                if (RefDevice == 32)
                    DevAdr = 1;
                else if (RefDevice == 34)
                    DevAdr = 2;
                else if (RefDevice == 36)
                    DevAdr = 3;
                else if (RefDevice == 38)
                    DevAdr = 4;
                density[18] = MB.ReadDensity(DevAdr, ComPortModBus);     //density measure

                FA_M3.GetFrequency(ref arrFreq[0], ref arrFreq[1], ref arrFreq[2], ref arrFreq[3], ref arrFreq[4], TestTime, ref CurrentValue, ComPortFA_M3, toolStripProgressBar1);
                float temp_refval = 0;
                //check Reference Device
                if (RefDevice == 32)
                    temp_refval = CalculateMassflowFrequenz(Convert.ToSingle(setting.CS32LowRange), Convert.ToSingle(setting.CS32HighRange), 0, Convert.ToSingle(setting.CS32PulseRate), arrFreq[0]);
                else if (RefDevice == 34)
                    temp_refval = CalculateMassflowFrequenz(Convert.ToSingle(setting.CS34LowRange), Convert.ToSingle(setting.CS34HighRange), 0, Convert.ToSingle(setting.CS34PulseRate), arrFreq[1]);
                else if (RefDevice == 36)
                    temp_refval = CalculateMassflowFrequenz(Convert.ToSingle(setting.CS36LowRange), Convert.ToSingle(setting.CS36HighRange), 0, Convert.ToSingle(setting.CS36PulseRate), arrFreq[2]);
                else if (RefDevice == 38)
                    temp_refval = CalculateMassflowFrequenz(Convert.ToSingle(setting.CS38LowRange), Convert.ToSingle(setting.CS38HighRange), 0, Convert.ToSingle(setting.CS38PulseRate), arrFreq[3]);

                if (cboFlowUnit.SelectedIndex == 1 || cboFlowUnit.SelectedIndex == 3)
                    temp_refval = temp_refval / (float)1000;

                if (temp_refval > (float)999.999)     //check the length of the number
                    txtPoint10Ref.Text = temp_refval.ToString("0.00");          //show the number in textbox
                else
                    txtPoint10Ref.Text = temp_refval.ToString("0.000");
                //############
                density[19] = MB.ReadDensity(DevAdr, ComPortModBus);     //density measure
                //############
                float temp_custval = 0;
                if (rbtnDisplay.Checked)
                {
                    EnterValue.ShowDialog();
                    txtPoint10Cust.Text = MFCust;
                }
                else if (rbtnHART.Checked)
                { }
                else if (rbtnBRAIN.Checked)
                { }
                else if (rbtnPulseActive.Checked || rbtnPulsePassive.Checked)
                    temp_custval = CalculateMassflowFrequenz(Convert.ToSingle(txtMFLRV.Text), Convert.ToSingle(txtMFURV.Text), Convert.ToSingle(txtPulseCurLRV.Text), Convert.ToSingle(txtPulseCurURV.Text), arrFreq[4]);
                else if (rbtnCurrentActive.Checked || rbtnCurrentPassive.Checked)
                    temp_custval = CalculateMassflowCurrent(Convert.ToSingle(txtMFLRV.Text), Convert.ToSingle(txtMFURV.Text), Convert.ToSingle(txtPulseCurLRV.Text), Convert.ToSingle(txtPulseCurURV.Text), CurrentValue);
                else
                    MessageBox.Show("No output selected", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                if (temp_custval > (float)999.999)
                    txtPoint10Cust.Text = temp_custval.ToString("0.00");
                else
                    txtPoint10Cust.Text = temp_custval.ToString("0.000");

                RefValue = Convert.ToDouble(txtPoint10Ref.Text);
                CustValue = Convert.ToDouble(txtPoint10Cust.Text);

                txtPoint10ErrAbs.Text = (CustValue - RefValue).ToString("0.000");
                txtPoint10ErrRel.Text = (Convert.ToDouble(txtPoint10ErrAbs.Text) * 100.0 / Convert.ToDouble(txtPoint10Ref.Text)).ToString("0.00");

                //MB.WritPressure(1, 0, ComPortModBus);
                //MB.WritPressure(2, 0, ComPortModBus);
                //MB.WritPressure(3, 0, ComPortModBus);
                //MB.WritPressure(4, 0, ComPortModBus);

                tmrMFMeasurement.Enabled = true;
                tmrTempMeasurement.Enabled = true;
                CalibButtonEnable();
            }
            else
                MessageBox.Show("Please insert the Nominal Value" + "\n" + "or no output selected", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        #endregion calibpoint 10

        private void txtPoint1_TextChanged(object sender, EventArgs e)
        {
            bool no = false;
            if (txtPoint1.Text != string.Empty)
            {
                    foreach (char numChar in txtPoint1.Text.ToCharArray())
                    {
                        if (Char.IsNumber(numChar) || numChar == 44)
                            no=true;
                        else
                        {
                            no = false;
                            MessageBox.Show("Please insert a number", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                    if(no)
                        txtRefNo1.Text = GetReferenceDevice(Convert.ToDouble(txtPoint1.Text)).ToString();
            }
            else
                txtRefNo1.Text = string.Empty;
        }

        private void txtPoint2_TextChanged(object sender, EventArgs e)
        {
            bool no = false;
            if (txtPoint2.Text != string.Empty)
            {
                foreach (char numChar in txtPoint2.Text.ToCharArray())
                {
                    if (Char.IsNumber(numChar) || numChar == 44)
                        no = true;
                    else
                    {
                        no = false;
                        MessageBox.Show("Please insert a number", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                if (no)
                    txtRefNo2.Text = GetReferenceDevice(Convert.ToDouble(txtPoint2.Text)).ToString();
            }
            else
                txtRefNo2.Text = string.Empty;
        }

        private void txtPoint3_TextChanged(object sender, EventArgs e)
        {
            bool no = false;
            if (txtPoint3.Text != string.Empty)
            {
                foreach (char numChar in txtPoint3.Text.ToCharArray())
                {
                    if (Char.IsNumber(numChar) || numChar == 44)
                        no = true;
                    else
                    {
                        no = false;
                        MessageBox.Show("Please insert a number", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                if (no)
                    txtRefNo3.Text = GetReferenceDevice(Convert.ToDouble(txtPoint3.Text)).ToString();
            }
            else
                txtRefNo3.Text = string.Empty;
        }

        private void txtPoint4_TextChanged(object sender, EventArgs e)
        {
            bool no = false;
            if (txtPoint4.Text != string.Empty)
            {
                foreach (char numChar in txtPoint4.Text.ToCharArray())
                {
                    if (Char.IsNumber(numChar) || numChar == 44)
                        no = true;
                    else
                    {
                        no = false;
                        MessageBox.Show("Please insert a number", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                if (no)
                    txtRefNo4.Text = GetReferenceDevice(Convert.ToDouble(txtPoint4.Text)).ToString();
            }
            else
                txtRefNo4.Text = string.Empty;
        }

        private void txtPoint5_TextChanged(object sender, EventArgs e)
        {
            bool no = false;
            if (txtPoint5.Text != string.Empty)
            {
                foreach (char numChar in txtPoint5.Text.ToCharArray())
                {
                    if (Char.IsNumber(numChar) || numChar == 44)
                        no = true;
                    else
                    {
                        no = false;
                        MessageBox.Show("Please insert a number", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                if (no)
                    txtRefNo5.Text = GetReferenceDevice(Convert.ToDouble(txtPoint5.Text)).ToString();
            }
            else
                txtRefNo5.Text = string.Empty;
        }

        private void txtPoint6_TextChanged(object sender, EventArgs e)
        {
            bool no = false;
            if (txtPoint6.Text != string.Empty)
            {
                foreach (char numChar in txtPoint6.Text.ToCharArray())
                {
                    if (Char.IsNumber(numChar) || numChar == 44)
                        no = true;
                    else
                    {
                        no = false;
                        MessageBox.Show("Please insert a number", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                if (no)
                    txtRefNo6.Text = GetReferenceDevice(Convert.ToDouble(txtPoint6.Text)).ToString();
            }
            else
                txtRefNo6.Text = string.Empty;
        }

        private void txtPoint7_TextChanged(object sender, EventArgs e)
        {
            bool no = false;
            if (txtPoint7.Text != string.Empty)
            {
                foreach (char numChar in txtPoint7.Text.ToCharArray())
                {
                    if (Char.IsNumber(numChar) || numChar == 44)
                        no = true;
                    else
                    {
                        no = false;
                        MessageBox.Show("Please insert a number", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                if (no)
                    txtRefNo7.Text = GetReferenceDevice(Convert.ToDouble(txtPoint7.Text)).ToString();
            }
            else
                txtRefNo7.Text = string.Empty;
        }

        private void txtPoint8_TextChanged(object sender, EventArgs e)
        {
            bool no = false;
            if (txtPoint8.Text != string.Empty)
            {
                foreach (char numChar in txtPoint8.Text.ToCharArray())
                {
                    if (Char.IsNumber(numChar) || numChar == 44)
                        no = true;
                    else
                    {
                        no = false;
                        MessageBox.Show("Please insert a number", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                if (no)
                    txtRefNo8.Text = GetReferenceDevice(Convert.ToDouble(txtPoint8.Text)).ToString();
            }
            else
                txtRefNo8.Text = string.Empty;
        }

        private void txtPoint9_TextChanged(object sender, EventArgs e)
        {
            bool no = false;
            if (txtPoint9.Text != string.Empty)
            {
                foreach (char numChar in txtPoint9.Text.ToCharArray())
                {
                    if (Char.IsNumber(numChar) || numChar == 44)
                        no = true;
                    else
                    {
                        no = false;
                        MessageBox.Show("Please insert a number", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                if (no)
                    txtRefNo9.Text = GetReferenceDevice(Convert.ToDouble(txtPoint9.Text)).ToString();
            }
            else
                txtRefNo9.Text = string.Empty;
        }

        private void txtPoint10_TextChanged(object sender, EventArgs e)
        {
            bool no = false;
            if (txtPoint10.Text != string.Empty)
            {
                foreach (char numChar in txtPoint10.Text.ToCharArray())
                {
                    if (Char.IsNumber(numChar) || numChar == 44)
                        no = true;
                    else
                    {
                        no = false;
                        MessageBox.Show("Please insert a number", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                if (no)
                    txtRefNo10.Text = GetReferenceDevice(Convert.ToDouble(txtPoint10.Text)).ToString();
            }
            else
                txtRefNo10.Text = string.Empty;
        }
        #endregion

        private void btnStartMFMeas_Click(object sender, EventArgs e)
        {
            tmrMFMeasurement.Enabled = true;
        }

        private void btnStopMFMeas_Click(object sender, EventArgs e)
        {
            tmrMFMeasurement.Enabled = false;
        }

        private void tmrMFMeasurement_Tick(object sender, EventArgs e)
        {
            if(tabControl1.SelectedTab==tabCalibration)//TabCalibration:2
            {
                if (cboFlowUnit.SelectedIndex == 0)       //check unit kg/h
                {
                    txtRefVal1.Text = MB.ReadMassFlow(1, ComPortModBus).ToString("0.00");
                    txtRefVal2.Text = MB.ReadMassFlow(2, ComPortModBus).ToString("0.00");
                    txtRefVal3.Text = MB.ReadMassFlow(3, ComPortModBus).ToString("0.00");
                    txtRefVal4.Text = MB.ReadMassFlow(4, ComPortModBus).ToString("0.00");
                }
                else if (cboFlowUnit.SelectedIndex == 1)      //check unit t/h
                {
                    txtRefVal1.Text = (MB.ReadMassFlow(1, ComPortModBus) / (float)1000).ToString("0.000");
                    txtRefVal2.Text = (MB.ReadMassFlow(2, ComPortModBus) / (float)1000).ToString("0.000");
                    txtRefVal3.Text = (MB.ReadMassFlow(3, ComPortModBus) / (float)1000).ToString("0.000");
                    txtRefVal4.Text = (MB.ReadMassFlow(4, ComPortModBus) / (float)1000).ToString("0.000");
                }
                else if (cboFlowUnit.SelectedIndex == 2)      //check unit t/h or m³/h
                {
                    txtRefVal1.Text = MB.ReadVolumeFlow(1, ComPortModBus).ToString("0.00");
                    txtRefVal2.Text = MB.ReadVolumeFlow(2, ComPortModBus).ToString("0.00");
                    txtRefVal3.Text = MB.ReadVolumeFlow(3, ComPortModBus).ToString("0.00");
                    txtRefVal4.Text = MB.ReadVolumeFlow(4, ComPortModBus).ToString("0.00");
                }
                else if (cboFlowUnit.SelectedIndex == 3)      //check unit t/h or m³/h
                {
                    txtRefVal1.Text = (MB.ReadVolumeFlow(1, ComPortModBus) / (float)1000).ToString("0.000");
                    txtRefVal2.Text = (MB.ReadVolumeFlow(2, ComPortModBus) / (float)1000).ToString("0.000");
                    txtRefVal3.Text = (MB.ReadVolumeFlow(3, ComPortModBus) / (float)1000).ToString("0.000");
                    txtRefVal4.Text = (MB.ReadVolumeFlow(4, ComPortModBus) / (float)1000).ToString("0.000");
                }
            }
                //Experimental
            else if (tabControl1.SelectedTab == tabExperimental)//TabExperimental:9
            {
                if (cboFlowUnitExp.SelectedIndex == 0)
                {
                    txtRCCS32EXP.Text = MB.ReadMassFlow(1, ComPortModBus).ToString("0.00");
                    txtRCCS34EXP.Text = MB.ReadMassFlow(2, ComPortModBus).ToString("0.00");
                    txtRCCS36EXP.Text = MB.ReadMassFlow(3, ComPortModBus).ToString("0.00");
                    txtRCCS38EXP.Text = MB.ReadMassFlow(4, ComPortModBus).ToString("0.00");
                    Application.DoEvents();
                }
                else if (cboFlowUnitExp.SelectedIndex == 1)
                {
                    txtRCCS32EXP.Text = MB.ReadVolumeFlow(1, ComPortModBus).ToString("0.00");
                    txtRCCS34EXP.Text = MB.ReadVolumeFlow(2, ComPortModBus).ToString("0.00");
                    txtRCCS36EXP.Text = MB.ReadVolumeFlow(3, ComPortModBus).ToString("0.00");
                    txtRCCS38EXP.Text = MB.ReadVolumeFlow(4, ComPortModBus).ToString("0.00");
                    Application.DoEvents();
                }
            }

        }


        private void rbtnDisplay_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnDisplay.Checked)
            {
                arrTextComment[1] = "The calibration data are based on the display values of the device.";
                txtComment.Lines = arrTextComment;
            }
        }

        private void btnCancelCalib_Click(object sender, EventArgs e)
        {
            Task task = Task.Factory.StartNew(() =>
            {
                FA_M3.IsCalibCanceled = true;
            });
        }

        private void cboFlowUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            ushort addr_pulse1select = 4384;
            cboDUTFlowUnit.SelectedIndex = cboFlowUnit.SelectedIndex;
            gboxNomValue.Text = "Nominal value [" + cboFlowUnit.Text + "]";
            gboxRefValue.Text = "Ref. value [" + cboFlowUnit.Text + "]";
            gboxCustValue.Text = "Customer value [" + cboFlowUnit.Text + "]";
            gboxErrorAbs.Text = "Error abs. [" + cboFlowUnit.Text + "]";

            if (cboFlowUnit.SelectedIndex == 0 || cboFlowUnit.SelectedIndex == 1)
            {
                //Pulse1select output set MassFlow
                MB.WriteHoldingRegisters(1, addr_pulse1select, 0, ComPortModBus);
                MB.WriteHoldingRegisters(2, addr_pulse1select, 0, ComPortModBus);
                MB.WriteHoldingRegisters(3, addr_pulse1select, 0, ComPortModBus);
                MB.WriteHoldingRegisters(4, addr_pulse1select, 0, ComPortModBus);

            }
            if (cboFlowUnit.SelectedIndex == 2 || cboFlowUnit.SelectedIndex == 3)
            {
                //Pulse1select output set VolumeFlow
                MB.WriteHoldingRegisters(1, addr_pulse1select, 1, ComPortModBus);
                MB.WriteHoldingRegisters(2, addr_pulse1select, 1, ComPortModBus);
                MB.WriteHoldingRegisters(3, addr_pulse1select, 1, ComPortModBus);
                MB.WriteHoldingRegisters(4, addr_pulse1select, 1, ComPortModBus);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //float wert= (float)0;
            //MB.WritPressure(1, 0, ComPortModBus);
            //MB.WritPressure(2, 0, ComPortModBus);
            //MB.ReadPressure(1, ComPortModBus);
            //MB.ReadPressure(2, ComPortModBus);
            //MB.ReadPressure(3, ComPortModBus);
            //MB.ReadPressure(4, ComPortModBus);


            //float Temp = 0, humidity = 0, pres = 0, akkucapa = 0;
            ////AmbLog.TaskInfo(COMPortAmbientTemp);
            ////AmbLog.TaskChannelInfo(COMPortAmbientTemp, 0);
            //AmbLog.TaskStart(COMPortAmbientTemp);
            //System.Threading.Thread.Sleep(1000);
            //AmbLog.ReadMesureData(COMPortAmbientTemp, ref pres, ref humidity, ref Temp, 0, 5, 6);
            //float X = 0, Y = 0, Z = 0;
            //AmbLog.ReadMesureData(COMPortAmbientTemp, ref X, ref Y, ref Z, 2, 3, 4);
            //AmbLog.ReadMesureData(COMPortAmbientTemp, ref akkucapa, ref humidity, ref Temp, 14, 5, 6);
            

            
            

            //float mist=MB.GetEndPressure(8, ComPortModBus);
            //for (int i = 0; i < 100;i++ )
            //{

                //int zahl = 1540;
                //string hexValue = zahl.ToString("X");
                //int decAgain = int.Parse(hexValue, System.Globalization.NumberStyles.HexNumber);
            
                //byte[] dat = new byte[4];
                //dat[0] = Convert.ToByte(decAgain);
                //dat[0] =    byte.Parse(hexValue.Substring(0,2), System.Globalization.NumberStyles.HexNumber);
                //dat[1] = byte.Parse(hexValue.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
                //dat[2] = byte.Parse(hexValue.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);
                //dat[3] = byte.Parse(hexValue.Substring(6, 2), System.Globalization.NumberStyles.HexNumber);
                
           
            //MB.WriteDriveModetoPumpDrive(5, 0, ComPortModBus);
            //MB.WriteSollwertBus(5, 0, ComPortModBus);
            //MB.ReadHoldingRegisters(5, 0x4446, 2, ref arrReply, ComPortModBus);
            
            //MB.ReadHoldingRegisters(5, 0x4440, 2, ref arrReply, ComPortModBus);
            //MB.WriteCommandtoPumpDrive(5, 0, ComPortModBus);
           

            //MB.ReadHoldingRegisters(5, 0x4440, 2, ref arrReply, ComPortModBus);

            
                //MB.GetEndPressure(6, ComPortModBus);
            //}

                
            
            //MB.WriteHoldingRegisters(1, 0x4814, 12, ComPortModBus);
            //MB.WriteHoldingRegisters(1, 4384, 0, ComPortModBus);
            //float mist = FA_M3.ReadAnalogInputValue(4, 3, ComPortFA_M3) / 10;
            //float zahl = 50000;
            //MB.WriteMassFlURV(4, zahl, ComPortModBus);
            
        }

        private void txtConditionDUT_TextChanged(object sender, EventArgs e)
        {
            if (txtConditionDUT.Lines.Length >= 5)
                MessageBox.Show("Only 4 lines possible", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnPortClose_Click(object sender, EventArgs e)
        {
            if (ComPortModBus.IsOpen)
                ComPortModBus.Close();
            btnPortClose.BackColor = Color.Red;
            btnPortOpen.BackColor = Color.Green;
            btnPortOpen.Select();
            var res = MessageBox.Show("Open the Port for changing calibration data", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            if (DialogResult.OK == res)
                lblPortWarning.Visible = true;
            ((Control)this.tabCalibOrder).Enabled = false;
            ((Control)this.tabCalibration).Enabled = false;
            ((Control)this.tabOverview).Enabled = false;
        }

        private void btnPortOpen_Click(object sender, EventArgs e)
        {
            if (!ComPortModBus.IsOpen)
            {
                try
                {
                    ComPortModBus.Open();
                    btnPortClose.BackColor = Color.Transparent;
                    btnPortOpen.BackColor = Color.Transparent;
                    lblPortWarning.Visible = false;
                    ((Control)this.tabCalibOrder).Enabled = true;
                    ((Control)this.tabCalibration).Enabled = true;
                    ((Control)this.tabOverview).Enabled = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void tmrTempMesurement_Tick(object sender, EventArgs e)
        {
            float Temp1 = 0, Temp2 = 0, Temp3 = 0, Temp4 = 0;
            float Val1 = 0, Val2 = 0, Val3 = 0, Val4 = 0, Val5 = 0, Val6 = 0, Val7 = 0, Val8 = 0;
            float AmbTemp = 0, AmbPres = 0, Ambhumi = 0;
            //Ambient Conditions
            AmbLog.TaskStart(COMPortAmbientTemp);
            System.Threading.Thread.Sleep(1000);        //wait for updates
            AmbLog.ReadMesureData(COMPortAmbientTemp, ref AmbPres, ref Ambhumi, ref AmbTemp, 0, 5, 6);
            txtAmbBaroPress.Text = AmbPres.ToString("0.0");
            txtAmbHumi.Text = Ambhumi.ToString("0.0");
            txtAmbTemp.Text = AmbTemp.ToString("0.0");
            Application.DoEvents();
            //Customer Water Temperaur
            if(COMPortCustTemp.IsOpen)
                txtWaterTempCustDevice.Text = CustTemp.Get_Temp(COMPortCustTemp).ToString("0.00");

            //Vessel,Rig,Cabinet
            if (ComPortFA_M3.IsOpen)
            {
                FA_M3.Read4TempValues(3, ComPortFA_M3, ref Temp1, ref Temp2, ref Temp3, ref Temp4);
                txtWaterTempVessel.Text = Temp1.ToString("0.0");
                txtWaterTempRig1.Text = Temp2.ToString("0.0");
                txtTempCabinet1.Text = Temp3.ToString("0.0");
            }
            //Flow Pressure
            if(ComPortFA_M3.IsOpen)
            {
                FA_M3.Read8AnalogInputValues(4, ComPortFA_M3, ref Val1, ref Val2, ref Val3, ref Val4, ref Val5, ref Val6, ref Val7, ref Val8);
                txtFlowPressDN25.Text = CalculatePressure(0, 35, Val2).ToString("0.00");
                txtFlowPressDN50.Text = CalculatePressure(0, 35, Val1).ToString("0.00");
            }
            Application.DoEvents();
        }

        private void btnSearchSerialNo_Click(object sender, EventArgs e)
        {
            if (txtSearchSerialNo.Text != string.Empty)
            {
                //int zeilennummer=0;
                DataRow[] CalibPointDataRow = dsTesting1.Tables[0].Select("SerialNo = '" + txtSearchSerialNo.Text.Trim() + "'");
                
                //DataRow CalibPointData = dsTesting1.Tables[0].Rows[zeilennummer];
                
                //var dsdestination = dsTesting1.Tables[0].[Select]("SerialNo = " +txtSearchSerialNo.Text) ; 
                foreach (DataRow row in CalibPointDataRow)
                {
                    txtSearchSerialNo.Text= row["NominalFlow"].ToString();
                }
                //DataRow newCalibPointDataRow = dsTesting1.Tables[0].NewRow();
                //dsDeviceData1.Tables[0].Rows.Add(newDeviceDataRow);
                //oleDbDADeviceData.Update(dsDeviceData1, "Device data");         
                
                //DataRow calib = dsTesting1.Tables[0].Rows.Find("SerialNo = '" + txtSearchSerialNo.Text.Trim() + "'");
                //dsDeviceData1.Tables[0].Rows.Add(CalibPointDataRow);
                //oleDbDADeviceData.Update(dsDeviceData1, "Device data");
                //    DataRow foundRow = dsTesting1.Tables["Calib Point data"].Rows.Find(txtSearchSerialNo.Text);
                if (CalibPointDataRow.Length > 1)
                {
                    MessageBox.Show("mehrere vorhanden, bitte Reihe wählen");
                }
                else
                    ;
                //    if (foundRow != null) 
                //    {
                //        MessageBox.Show(foundRow[0].ToString());
                //    }
                //    else
                //    {
                //        MessageBox.Show("A row with the primary key of " + txtSearchSerialNo.Text + " could not be found");
                //    }

                    //var value = dsTesting1.Tables["Calib Point data"].Rows[0];
                    //var value = dsTesting1.Tables["Calib Point data"].Columns["SerialNo"].Equals((object)txtSearchSerialNo.Text);
                    //DataRow foundRow = dsTesting1.Tables["Calib Point data"].Rows.Find("SerialNo");
                    //DataRow selectdata = dsTesting1.Tables[0].NewRow();
                    //DataRow[] result = dsTesting1.Calib_Point_data.Select(txtSearchSerialNo.Text);
                    //foreach (DataRow row in result)
                    //{
                    //    txtSearchSerialNo.Text=result.
                    //}

              //      dsTesting1.Calib_Point_data.Select(txtSearchSerialNo.Text);
 
            }
        }

        #region Experimental
        /// <summary>
        /// Select MassFlow or VolumeFlow
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cboFlowUnitExp_SelectedIndexChanged(object sender, EventArgs e)
        {
            ushort addr_pulse1select = 4384;

            if (cboFlowUnitExp.SelectedIndex == 0)
            {
                //Pulse1select output set MassFlow
                MB.WriteHoldingRegisters(1, addr_pulse1select, 0, ComPortModBus);
                MB.WriteHoldingRegisters(2, addr_pulse1select, 0, ComPortModBus);
                MB.WriteHoldingRegisters(3, addr_pulse1select, 0, ComPortModBus);
                MB.WriteHoldingRegisters(4, addr_pulse1select, 0, ComPortModBus);
                lblFlowUnitDUT.Text = "kg/h";
            }
            if (cboFlowUnitExp.SelectedIndex == 1)
            {
                //Pulse1select output set VolumeFlow
                MB.WriteHoldingRegisters(1, addr_pulse1select, 1, ComPortModBus);
                MB.WriteHoldingRegisters(2, addr_pulse1select, 1, ComPortModBus);
                MB.WriteHoldingRegisters(3, addr_pulse1select, 1, ComPortModBus);
                MB.WriteHoldingRegisters(4, addr_pulse1select, 1, ComPortModBus);
                lblFlowUnitDUT.Text = "l/h";
            }
        }
        /// <summary>
        /// start experimental function
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStartExperimental_Click(object sender, EventArgs e)
        {
            tmrMeasurements.Enabled = false;
            tmrTempMeasurement.Enabled = false;
            cboFlowUnit.Text = string.Empty;
            System.Threading.Thread.Sleep(30);
            Application.DoEvents();
            ((Control)this.tabCustomerData).Enabled = false;
            ((Control)this.tabCalibOrder).Enabled = false;
            ((Control)this.tabCalibration).Enabled = false;
            ((Control)this.tabComment).Enabled = false;
            ((Control)this.tabOverview).Enabled = false;
            ((Control)this.tabCommunication).Enabled = false;
            ((Control)this.tabDeviceData).Enabled = false;
            ((Control)this.tabCalibData).Enabled = false;
            tmrMFMeasurement.Enabled = true;
            cboFlowUnitExp.Enabled = true;
            btnStartTest.Enabled = true;
            cboTestTimeEXP.SelectedIndex = 0;
            cboTestTimeEXP.Text = "60";

        }
        /// <summary>
        /// Stop experimental function
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStopExperimental_Click(object sender, EventArgs e)
        {
            
            //Default Values
            //MB.WriteMassFlURV(1, 600, ComPortModBus);
            //MB.WriteMassFlLRV(1, 0, ComPortModBus);
            //MB.WritePulse1Rate(1, 10000, ComPortModBus);
            //MB.WriteMassFlURV(2, 5000, ComPortModBus);
            //MB.WriteMassFlLRV(2, 0, ComPortModBus);
            //MB.WritePulse1Rate(2, 10000, ComPortModBus);
            //MB.WriteMassFlURV(3, 17000, ComPortModBus);
            //MB.WriteMassFlLRV(3, 0, ComPortModBus);
            //MB.WritePulse1Rate(3, 10000, ComPortModBus);
            //MB.WriteMassFlURV(4, 50000, ComPortModBus);
            //MB.WriteMassFlLRV(4, 0, ComPortModBus);
            //MB.WritePulse1Rate(4, 10000, ComPortModBus);

            tmrMFMeasurement.Enabled = false;
            cboFlowUnitExp.Text = string.Empty;
            cboFlowUnitExp.Enabled = false;
            btnStartTest.Enabled = false;
            ((Control)this.tabCustomerData).Enabled = true;
            ((Control)this.tabCalibOrder).Enabled = true;
            ((Control)this.tabCalibration).Enabled = true;
            ((Control)this.tabComment).Enabled = true;
            ((Control)this.tabOverview).Enabled = true;
            ((Control)this.tabCommunication).Enabled = true;
            ((Control)this.tabDeviceData).Enabled = true;
            ((Control)this.tabCalibData).Enabled = true;
            
            MB.WriteHoldingRegisters(1, 4384, 0, ComPortModBus);
            MB.WriteHoldingRegisters(2, 4384, 0, ComPortModBus);
            MB.WriteHoldingRegisters(3, 4384, 0, ComPortModBus);
            MB.WriteHoldingRegisters(4, 4384, 0, ComPortModBus);
        }
        /// <summary>
        /// Read Flow URV and LRV Values
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRead_Click(object sender, EventArgs e)
        {
            if (cboFlowUnitExp.SelectedIndex == 0) // Read MassFlow URV and LRV
            {
                tmrMFMeasurement.Enabled = false;
                if (chboRCCS32.Checked)
                {
                    txt32FlowURV.Text = MB.ReadMassFlURV(1, ComPortModBus).ToString();
                    txt32FlowLRV.Text = MB.ReadMassFlLRV(1, ComPortModBus).ToString();
                    txt32PulseURV.Text = MB.ReadPulse1Rate(1, ComPortModBus).ToString();
                }
                if (chboRCCS34.Checked)
                {
                    txt34FlowURV.Text = MB.ReadMassFlURV(2, ComPortModBus).ToString();
                    txt34FlowLRV.Text = MB.ReadMassFlLRV(2, ComPortModBus).ToString();
                    txt34PulseURV.Text = MB.ReadPulse1Rate(2, ComPortModBus).ToString();
                }
                if (chboRCCS36.Checked)
                {
                    txt36FlowURV.Text = MB.ReadMassFlURV(3, ComPortModBus).ToString();
                    txt36FlowLRV.Text = MB.ReadMassFlLRV(3, ComPortModBus).ToString();
                    txt36PulseURV.Text = MB.ReadPulse1Rate(3, ComPortModBus).ToString();
                }
                if (chboRCCS38.Checked)
                {
                    txt38FlowURV.Text = MB.ReadMassFlURV(4, ComPortModBus).ToString();
                    txt38FlowLRV.Text = MB.ReadMassFlLRV(4, ComPortModBus).ToString();
                    txt38PulseURV.Text = MB.ReadPulse1Rate(4, ComPortModBus).ToString();
                }
                tmrMFMeasurement.Enabled = true;
            }
            else if (cboFlowUnitExp.SelectedIndex == 1)// Read VolumFlow URV and LRV
            {
                tmrMFMeasurement.Enabled = false;
                if (chboRCCS32.Checked)
                {
                    txt32FlowURV.Text = MB.ReadVolFlURV(1, ComPortModBus).ToString();
                    txt32FlowLRV.Text = MB.ReadVolFlLRV(1, ComPortModBus).ToString();
                    txt32PulseURV.Text = MB.ReadPulse1Rate(1, ComPortModBus).ToString();
                }
                if (chboRCCS34.Checked)
                {
                    txt34FlowURV.Text = MB.ReadVolFlURV(2, ComPortModBus).ToString();
                    txt34FlowLRV.Text = MB.ReadVolFlLRV(2, ComPortModBus).ToString();
                    txt34PulseURV.Text = MB.ReadPulse1Rate(2, ComPortModBus).ToString();
                }
                if (chboRCCS36.Checked)
                {
                    txt36FlowURV.Text = MB.ReadVolFlURV(3, ComPortModBus).ToString();
                    txt36FlowLRV.Text = MB.ReadVolFlLRV(3, ComPortModBus).ToString();
                    txt36PulseURV.Text = MB.ReadPulse1Rate(3, ComPortModBus).ToString();
                }
                if (chboRCCS38.Checked)
                {
                    txt38FlowURV.Text = MB.ReadVolFlURV(4, ComPortModBus).ToString();
                    txt38FlowLRV.Text = MB.ReadVolFlLRV(4, ComPortModBus).ToString();
                    txt38PulseURV.Text = MB.ReadPulse1Rate(4, ComPortModBus).ToString();
                }
                tmrMFMeasurement.Enabled = true;
            }
            else
                MessageBox.Show("Please start Experimental and select Flow Unit", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        /// <summary>
        /// Write Flow URV and LRV Value
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnWrite_Click(object sender, EventArgs e)
        {
            if (cboFlowUnitExp.SelectedIndex == 0) // write MassFlow URV and LRV
            {
                tmrMFMeasurement.Enabled = false;
                if (chboRCCS32.Checked)
                {
                    MB.WriteMassFlURV(1, Convert.ToSingle(txt32FlowURV.Text), ComPortModBus);
                    MB.WriteMassFlLRV(1, Convert.ToSingle(txt32FlowLRV.Text), ComPortModBus);
                    MB.WritePulse1Rate(1, Convert.ToSingle(txt32PulseURV.Text), ComPortModBus);
                }
                if (chboRCCS34.Checked)
                {
                    MB.WriteMassFlURV(2, Convert.ToSingle(txt34FlowURV.Text), ComPortModBus);
                    MB.WriteMassFlLRV(2, Convert.ToSingle(txt34FlowLRV.Text), ComPortModBus);
                    MB.WritePulse1Rate(2, Convert.ToSingle(txt34PulseURV.Text), ComPortModBus);
                }
                if (chboRCCS36.Checked)
                {
                    MB.WriteMassFlURV(3, Convert.ToSingle(txt36FlowURV.Text), ComPortModBus);
                    MB.WriteMassFlLRV(3, Convert.ToSingle(txt36FlowLRV.Text), ComPortModBus);
                    MB.WritePulse1Rate(3, Convert.ToSingle(txt36PulseURV.Text), ComPortModBus);
                }
                if (chboRCCS38.Checked)
                {
                    MB.WriteMassFlURV(4, Convert.ToSingle(txt38FlowURV.Text), ComPortModBus);
                    MB.WriteMassFlLRV(4, Convert.ToSingle(txt38FlowLRV.Text), ComPortModBus);
                    MB.WritePulse1Rate(4, Convert.ToSingle(txt38PulseURV.Text), ComPortModBus);
                }
                tmrMFMeasurement.Enabled = true;
            }
            else if(cboFlowUnitExp.SelectedIndex == 1)
            {
                tmrMFMeasurement.Enabled = false;
                if (chboRCCS32.Checked)
                {
                    MB.WritVolFlURV(1, Convert.ToSingle(txt32FlowURV.Text), ComPortModBus);
                    MB.WritVolFlLRV(1, Convert.ToSingle(txt32FlowLRV.Text), ComPortModBus);
                    MB.WritePulse1Rate(1, Convert.ToSingle(txt32PulseURV.Text), ComPortModBus);
                }
                if (chboRCCS34.Checked)
                {
                    MB.WritVolFlURV(2, Convert.ToSingle(txt34FlowURV.Text), ComPortModBus);
                    MB.WritVolFlLRV(2, Convert.ToSingle(txt34FlowLRV.Text), ComPortModBus);
                    MB.WritePulse1Rate(2, Convert.ToSingle(txt34PulseURV.Text), ComPortModBus);
                }
                if (chboRCCS36.Checked)
                {
                    MB.WritVolFlURV(3, Convert.ToSingle(txt36FlowURV.Text), ComPortModBus);
                    MB.WritVolFlLRV(3, Convert.ToSingle(txt36FlowLRV.Text), ComPortModBus);
                    MB.WritePulse1Rate(3, Convert.ToSingle(txt36PulseURV.Text), ComPortModBus);
                }
                if (chboRCCS38.Checked)
                {
                    MB.WritVolFlURV(4, Convert.ToSingle(txt38FlowURV.Text), ComPortModBus);
                    MB.WritVolFlLRV(4, Convert.ToSingle(txt38FlowLRV.Text), ComPortModBus);
                    MB.WritePulse1Rate(4, Convert.ToSingle(txt38PulseURV.Text), ComPortModBus);
                }
                tmrMFMeasurement.Enabled = true;
            }
            else
                MessageBox.Show("Please start Experimental and select Flow Unit", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnSearchData_Click(object sender, EventArgs e)
        {
            if (txtSearchDataSerialNo.Text != string.Empty)
            {
                DataRow[] CalibDataRow = dsCalibData1.Tables[0].Select("SerialNo = '" + txtSearchSerialNo.Text.Trim() + "'");
                CalibDataRow[0].BeginEdit();
                //TODO
                //var dsdestination = dsTesting1.Tables[0].[Select]("SerialNo = " +txtSearchSerialNo.Text) ; 
                foreach (DataRow row in CalibDataRow)
                {
                    txtSearchSerialNo.Text = row["NominalFlow"].ToString();
                }
            }
        }

        /// <summary>
        /// Start Experimental measurement
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStartTest_Click(object sender, EventArgs e)
        {
            FA_M3.IsCalibCanceled = false;
            btnStart.Enabled = false;
            AbortTest = false;
            tmrMFMeasurement.Enabled = false;
            btnCancelTest.Focus();
            float[] arrFreq = new float[5];
            float AmbTempStart = 0, AmbPresStart = 0, AmbhumiStart = 0;
            float AmbTempStop = 0, AmbPresStop = 0, AmbhumiStop = 0;

            float Temp1Start = 0, Temp2Start = 0, Temp3Start = 0, Temp4Start = 0;
            float Temp1Stop = 0, Temp2Stop = 0, Temp3Stop = 0, Temp4Stop = 0;
            float Val1 = 0, Val2= 0, Val3 = 0, Val4 = 0, Val5 = 0, Val6 = 0, Val7 = 0, Val8 = 0;
            float refval32 = 0, refval34 = 0, refval36 = 0, refval38 = 0, dutvalpulse = 0, dutvalcurrent = 0;

            btnCancelTest.Enabled = true;
            btnInterrupt.Enabled = true;
            int run = 0;
            double[] DUT_Temp = new double[2];
            float[] conditions = new float[8];
            float[] RefDensity = new float[8];
            float[] RefTemperature = new float[8];
            float[] PressDN25 = new float[4];
            float[] PressDN50 = new float[4];
            float pressureDN25 = 0;
            float pressureDN50 = 0;

            if(cboFlowUnitExp.SelectedIndex==-1)
            {
                MessageBox.Show("Please select the FlowUnit", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if(chboDUT.Checked && (txtMFDutLRV.Text == string.Empty || txtMFDutURV.Text ==string.Empty))
                MessageBox.Show("Please insert Dut LRV and URV","Error",MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                //################################
                //loop mode
                //################################
                if (rbtnloop.Checked)
                {
                    btnInterrupt.Enabled = true;

                    if (txtloopnumber.Text != string.Empty)
                    {
                        int loops = Convert.ToInt32(txtloopnumber.Text);
                        while (rbtnloop.Checked && 0 < loops && !AbortTest)
                        {
                            txtloopsremaining.Text = loops.ToString();
                            loops--;
                            //Get Rotamass Data Density, Temperature and multi factor
                            if (!AbortTest)
                            {
                                if (chboRCCS32.Checked)
                                {
                                    RefDensity[0] = MB.ReadDensity(1, ComPortModBus);
                                    RefTemperature[0] = MB.ReadTemperature(1, ComPortModBus);
                                }
                                if (chboRCCS34.Checked)
                                {
                                    RefDensity[2] = MB.ReadDensity(2, ComPortModBus);
                                    RefTemperature[2] = MB.ReadTemperature(2, ComPortModBus);
                                }
                                if (chboRCCS36.Checked)
                                {
                                    RefDensity[4] = MB.ReadDensity(3, ComPortModBus);
                                    RefTemperature[4] = MB.ReadTemperature(3, ComPortModBus);
                                }
                                if (chboRCCS38.Checked)
                                {
                                    RefDensity[6] = MB.ReadDensity(4, ComPortModBus);
                                    RefTemperature[6] = MB.ReadTemperature(4, ComPortModBus);
                                }
                            }

                            if (!AbortTest)
                            {
                                //Ambient Conditions
                                AmbLog.TaskStart(COMPortAmbientTemp);
                                System.Threading.Thread.Sleep(1000);        //wait for updates
                                AmbLog.ReadMesureData(COMPortAmbientTemp, ref AmbPresStart, ref AmbhumiStart, ref AmbTempStart, 0, 5, 6);

                                //WaterTempDUTRig
                                DUT_Temp[0] = CustTemp.Get_Temp(COMPortCustTemp);//TempDUT
                                //Vessel,Rig,Cabinet
                                FA_M3.Read4TempValues(3, ComPortFA_M3, ref conditions[0], ref conditions[2], ref Temp3Start, ref Temp4Start);//conditions[0]=Vessel ;conditions[2]=WaterRig1;Temp3=cabinet
                                ////Flow Pressure
                                //FA_M3.Read8AnalogInputValues(4, ComPortFA_M3, ref Val1, ref Val2, ref Val3, ref Val4, ref Val5, ref Val6, ref Val7, ref Val8);//conditions[4]=DN50;conditions[6]=DN25
                                //conditions[6] = CalculatePressure(0, 35, Val2);//conditions[6]=DN25
                                //conditions[4] = CalculatePressure(0, 35, Val1);//conditions[4]=DN50
                                //####################################################
                                //get 4 Pressure values
                                for (int j = 0; j < 4; j++)
                                {   //Flow Pressure
                                    FA_M3.Read8AnalogInputValues(4, ComPortFA_M3, ref Val1, ref Val2, ref Val3, ref Val4, ref Val5, ref Val6, ref Val7, ref Val8);
                                    PressDN25[j] = CalculatePressure(0, 35, Val1);
                                    PressDN50[j] = CalculatePressure(0, 35, Val2);
                                    System.Threading.Thread.Sleep(1000);
                                }
                                conditions[6] = CalculatePressure(0, 35, Val1);//conditions[6]=DN25
                                conditions[4] = CalculatePressure(0, 35, Val2);//conditions[4]=DN50
                                //TODO einfügen nach Hardwareverbindung
                                pressureDN25 = CalcMean(PressDN25, 4);
                                pressureDN50 = CalcMean(PressDN50, 4);
                                //if (chboRCCS32.Checked)
                                //    MB.WritPressure(1, pressureDN25, ComPortModBus);
                                //if (chboRCCS34.Checked)
                                //    MB.WritPressure(2, pressureDN25, ComPortModBus);
                                //if (chboRCCS36.Checked)
                                //    MB.WritPressure(3, pressureDN50, ComPortModBus);
                                //if (chboRCCS38.Checked)
                                //    MB.WritPressure(4, pressureDN50, ComPortModBus);
                                //######################################################
                                Application.DoEvents();
                            }

                            FA_M3.GetFrequency(ref arrFreq[0], ref arrFreq[1], ref arrFreq[2], ref arrFreq[3], ref arrFreq[4], TestTimeExp, ref CurrentValue, ComPortFA_M3, toolStripProgressBar1);

                            if (!AbortTest)
                            {
                                if (chboRCCS32.Checked)
                                    refval32 = CalculateMassflowFrequenz(Convert.ToSingle(txt32FlowLRV.Text), Convert.ToSingle(txt32FlowURV.Text), 0, Convert.ToSingle(txt32PulseURV.Text), arrFreq[0]);
                                if (chboRCCS34.Checked)
                                    refval34 = CalculateMassflowFrequenz(Convert.ToSingle(txt34FlowLRV.Text), Convert.ToSingle(txt34FlowURV.Text), 0, Convert.ToSingle(txt34PulseURV.Text), arrFreq[1]);
                                if (chboRCCS36.Checked)
                                    refval36 = CalculateMassflowFrequenz(Convert.ToSingle(txt36FlowLRV.Text), Convert.ToSingle(txt36FlowURV.Text), 0, Convert.ToSingle(txt36PulseURV.Text), arrFreq[2]);
                                if (chboRCCS38.Checked)
                                    refval38 = CalculateMassflowFrequenz(Convert.ToSingle(txt38FlowLRV.Text), Convert.ToSingle(txt38FlowURV.Text), 0, Convert.ToSingle(txt38PulseURV.Text), arrFreq[3]);
                                
                                #region runs
                                ++run;
                                if(run==1)
                                {
                                    if (chboRCCS32.Checked)
                                    {
                                        txtExpValue32_1.Text = refval32.ToString("0.00");
                                        txtPulses32_1.Text = arrFreq[0].ToString();
                                    }
                                    if (chboRCCS34.Checked)
                                    {
                                        txtExpValue34_1.Text = refval34.ToString("0.00");
                                        txtPulses34_1.Text = arrFreq[1].ToString();
                                    }
                                    if (chboRCCS36.Checked)
                                    {
                                        txtExpValue36_1.Text = refval36.ToString("0.00");
                                        txtPulses36_1.Text = arrFreq[2].ToString();
                                    }
                                    if (chboRCCS38.Checked)
                                    {
                                        txtExpValue38_1.Text = refval38.ToString("0.00");
                                        txtPulses38_1.Text = arrFreq[3].ToString();
                                    }
                                }
                                if(run==2)
                                {
                                    if (chboRCCS32.Checked)
                                    {
                                        txtExpValue32_2.Text = refval32.ToString("0.00");
                                        txtPulses32_2.Text = arrFreq[0].ToString();
                                    }
                                    if (chboRCCS34.Checked)
                                    {
                                        txtExpValue34_2.Text = refval34.ToString("0.00");
                                        txtPulses34_2.Text = arrFreq[1].ToString();
                                    }
                                    if (chboRCCS36.Checked)
                                    {
                                        txtExpValue36_2.Text = refval36.ToString("0.00");
                                        txtPulses36_2.Text = arrFreq[2].ToString();
                                    }
                                    if (chboRCCS38.Checked)
                                    {
                                        txtExpValue38_2.Text = refval38.ToString("0.00");
                                        txtPulses38_2.Text = arrFreq[3].ToString();
                                    }
                                }
                                if (run == 3)
                                {
                                    if (chboRCCS32.Checked)
                                    {
                                        txtExpValue32_3.Text = refval32.ToString("0.00");
                                        txtPulses32_3.Text = arrFreq[0].ToString();
                                    }
                                    if (chboRCCS34.Checked)
                                    {
                                        txtExpValue34_3.Text = refval34.ToString("0.00");
                                        txtPulses34_3.Text = arrFreq[1].ToString();
                                    }
                                    if (chboRCCS36.Checked)
                                    {
                                        txtExpValue36_3.Text = refval36.ToString("0.00");
                                        txtPulses36_3.Text = arrFreq[2].ToString();
                                    }
                                    if (chboRCCS38.Checked)
                                    {
                                        txtExpValue38_3.Text = refval38.ToString("0.00");
                                        txtPulses38_3.Text = arrFreq[3].ToString();
                                    }
                                }
                                if (run == 4)
                                {
                                    if (chboRCCS32.Checked)
                                    {
                                        txtExpValue32_4.Text = refval32.ToString("0.00");
                                        txtPulses32_4.Text = arrFreq[0].ToString();
                                    }
                                    if (chboRCCS34.Checked)
                                    {
                                        txtExpValue34_4.Text = refval34.ToString("0.00");
                                        txtPulses34_4.Text = arrFreq[1].ToString();
                                    }
                                    if (chboRCCS36.Checked)
                                    {
                                        txtExpValue36_4.Text = refval36.ToString("0.00");
                                        txtPulses36_4.Text = arrFreq[2].ToString();
                                    }
                                    if (chboRCCS38.Checked)
                                    {
                                        txtExpValue38_4.Text = refval38.ToString("0.00");
                                        txtPulses38_4.Text = arrFreq[3].ToString();
                                    }
                                    
                                }
                                #endregion runs

                                if (chboDUT.Checked)
                                {
                                    if (txtMFDutLRV.Text != string.Empty && txtMFDutURV.Text != string.Empty)
                                    {
                                        dutvalcurrent = CalculateMassflowCurrent(Convert.ToSingle(txtMFDutLRV.Text), Convert.ToSingle(txtMFDutURV.Text), Convert.ToSingle(txtDutCurrentLRV.Text), Convert.ToSingle(txtDutCurrentURV.Text), CurrentValue);
                                        dutvalpulse = CalculateMassflowFrequenz(Convert.ToSingle(txtMFDutLRV.Text), Convert.ToSingle(txtMFDutURV.Text), Convert.ToSingle(txtDutPulseLRV.Text), Convert.ToSingle(txtDutPulseURV.Text), arrFreq[4]);
                                    }
                                    if (run == 1)
                                    {
                                        txtDutPulseValue_1.Text = dutvalpulse.ToString("0.00");
                                        txtDUTPulses_1.Text = arrFreq[4].ToString();
                                        txtDUTCurrent_1.Text =CurrentValue.ToString();
                                        txtDutCurrentValue_1.Text = dutvalcurrent.ToString("0.00");

                                    }
                                    if(run==2)
                                    {
                                        txtDutPulseValue_2.Text = dutvalpulse.ToString("0.00");
                                        txtDUTPulses_2.Text = arrFreq[4].ToString();
                                        txtDUTCurrent_2.Text =CurrentValue.ToString();
                                        txtDutCurrentValue_2.Text = dutvalcurrent.ToString("0.00");
                                    }
                                    if(run==3)
                                    {
                                        txtDutPulseValue_3.Text = dutvalpulse.ToString("0.00");
                                        txtDUTPulses_3.Text = arrFreq[4].ToString();
                                        txtDUTCurrent_3.Text =CurrentValue.ToString();
                                        txtDutCurrentValue_3.Text = dutvalcurrent.ToString("0.00");
                                    }
                                    if(run==4)
                                    {
                                        txtDutPulseValue_4.Text = dutvalpulse.ToString("0.00");
                                        txtDUTPulses_4.Text = arrFreq[4].ToString();
                                        txtDUTCurrent_4.Text =CurrentValue.ToString();
                                        txtDutCurrentValue_4.Text = dutvalcurrent.ToString("0.00");
                                        run = 0;
                                    }
                                    if (!chboRCCS32.Checked && !chboRCCS34.Checked && !chboRCCS36.Checked && !chboRCCS38.Checked && !chboDUT.Checked)
                                        MessageBox.Show("No output selected", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                                
                            }

                            if (!AbortTest)
                            {
                                //Get Rotamass Data Density and Temperature
                                RefDensity[1] = MB.ReadDensity(1, ComPortModBus);
                                RefTemperature[1] = MB.ReadTemperature(1, ComPortModBus);
                                RefDensity[3] = MB.ReadDensity(2, ComPortModBus);
                                RefTemperature[3] = MB.ReadTemperature(2, ComPortModBus);
                                RefDensity[5] = MB.ReadDensity(3, ComPortModBus);
                                RefTemperature[5] = MB.ReadTemperature(3, ComPortModBus);
                                RefDensity[7] = MB.ReadDensity(4, ComPortModBus);
                                RefTemperature[7] = MB.ReadTemperature(4, ComPortModBus);
                                //Ambient Conditions
                                AmbLog.TaskStart(COMPortAmbientTemp);
                                System.Threading.Thread.Sleep(1000);        //wait for updates
                                AmbLog.ReadMesureData(COMPortAmbientTemp, ref AmbPresStop, ref AmbhumiStop, ref AmbTempStop, 0, 5, 6);
                                //WaterTempDUTRig
                                DUT_Temp[1] = CustTemp.Get_Temp(COMPortCustTemp);//TempDUT
                                //Vessel,Rig,Cabinet
                                FA_M3.Read4TempValues(3, ComPortFA_M3, ref conditions[1], ref conditions[3], ref Temp3Stop, ref Temp4Stop);//conditions[1]=Vessel ;conditions[3]=WaterRig1;Temp3=cabinet
                                //Flow Pressure
                                FA_M3.Read8AnalogInputValues(4, ComPortFA_M3, ref Val1, ref Val2, ref Val3, ref Val4, ref Val5, ref Val6, ref Val7, ref Val8);
                                conditions[7] = CalculatePressure(0, 35, Val1);//conditions[7]=DN25
                                conditions[5] = CalculatePressure(0, 35, Val2);//conditions[5]=DN50
                                Application.DoEvents();

                                AmbientHumidity = (AmbhumiStart + AmbhumiStop) / (float)2;
                                AmbientPressure = (AmbPresStart + AmbPresStop) / (float)2;
                                AmbientTemperature = (AmbTempStart + AmbTempStop) / (float)2;

                                saveExperimentalData(arrFreq, RefDensity, RefTemperature, CurrentValue, conditions, DUT_Temp);
                                if (interrupt)
                                {
                                    tmrMFMeasurement.Enabled = true;
                                    MessageBox.Show("Interrupt, continue with OK", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    interrupt = false;
                                    tmrMFMeasurement.Enabled = false;
                                }
                                
                            }

                        }//loops
                        MessageBox.Show("Loop test finished", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }//relplaynumber
                    else
                        MessageBox.Show("Please insert the number of replays", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }//end loopmode
                //################################
                //single mode
                //################################
                else if (rbtnsingle.Checked)
                {
                    //Get Rotamass Data Density, Temperature and multi factor
                    if(!AbortTest)
                    {
                        if (chboRCCS32.Checked)
                        {
                            RefDensity[0] = MB.ReadDensity(1, ComPortModBus);
                            RefTemperature[0] = MB.ReadTemperature(1, ComPortModBus);
                        }
                        if (chboRCCS34.Checked)
                        {
                            RefDensity[2] = MB.ReadDensity(2, ComPortModBus);
                            RefTemperature[2] = MB.ReadTemperature(2, ComPortModBus);
                        }
                        if (chboRCCS36.Checked)
                        {
                            RefDensity[4] = MB.ReadDensity(3, ComPortModBus);
                            RefTemperature[4] = MB.ReadTemperature(3, ComPortModBus);
                        }
                        if (chboRCCS38.Checked)
                        {
                            RefDensity[6] = MB.ReadDensity(4, ComPortModBus);
                            RefTemperature[6] = MB.ReadTemperature(4, ComPortModBus);
                        }
                    }
                    if (!AbortTest)
                    {
                        //Ambient Conditions
                        AmbLog.TaskStart(COMPortAmbientTemp);
                        System.Threading.Thread.Sleep(1000);        //wait for updates
                        AmbLog.ReadMesureData(COMPortAmbientTemp, ref AmbPresStart, ref AmbhumiStart, ref AmbTempStart, 0, 5, 6);

                        //WaterTempDUTRig
                        DUT_Temp[0] = CustTemp.Get_Temp(COMPortCustTemp);//TempDUT
                        //Vessel,Rig,Cabinet
                        FA_M3.Read4TempValues(3, ComPortFA_M3, ref conditions[0], ref conditions[2], ref Temp3Start, ref Temp4Start);//conditions[0]=Vessel ;conditions[2]=WaterRig1;Temp3=cabinet
                        
                        //####################################################
                        //get 4 Pressure values
                        for (int j = 0; j < 4; j++)
                        {   //Flow Pressure
                            FA_M3.Read8AnalogInputValues(4, ComPortFA_M3, ref Val1, ref Val2, ref Val3, ref Val4, ref Val5, ref Val6, ref Val7, ref Val8);
                            PressDN25[j] = CalculatePressure(0, 35, Val1);
                            PressDN50[j] = CalculatePressure(0, 35, Val2);
                        }
                        conditions[6] = CalculatePressure(0, 35, Val1);//conditions[6]=DN25
                        conditions[4] = CalculatePressure(0, 35, Val2);//conditions[4]=DN50
                        //TODO einfügen nach Hardwareverbindung
                        pressureDN25 = CalcMean(PressDN25, 4);
                        pressureDN50 = CalcMean(PressDN50, 4);
                        //if (chboRCCS32.Checked)
                        //    MB.WritPressure(1, pressureDN25, ComPortModBus);
                        //if (chboRCCS34.Checked)
                        //    MB.WritPressure(2, pressureDN25, ComPortModBus);
                        //if (chboRCCS36.Checked)
                        //    MB.WritPressure(3, pressureDN50, ComPortModBus);
                        //if (chboRCCS38.Checked)
                        //    MB.WritPressure(4, pressureDN50, ComPortModBus);
                        //######################################################
                        Application.DoEvents();
                    }

                    FA_M3.GetFrequency(ref arrFreq[0], ref arrFreq[1], ref arrFreq[2], ref arrFreq[3], ref arrFreq[4], TestTimeExp, ref CurrentValue, ComPortFA_M3, toolStripProgressBar1);

                    if(!AbortTest)
                    {
                        if (chboRCCS32.Checked)
                        {
                            txtExpValue32_1.Text = CalculateMassflowFrequenz(Convert.ToSingle(txt32FlowLRV.Text), Convert.ToSingle(txt32FlowURV.Text), 0, Convert.ToSingle(txt32PulseURV.Text), arrFreq[0]).ToString("0.00");
                            txtPulses32_1.Text = arrFreq[0].ToString();
                        }
                        if (chboRCCS34.Checked)
                        {
                            txtExpValue34_1.Text = CalculateMassflowFrequenz(Convert.ToSingle(txt34FlowLRV.Text), Convert.ToSingle(txt34FlowURV.Text), 0, Convert.ToSingle(txt34PulseURV.Text), arrFreq[1]).ToString("0.00");
                            txtPulses34_1.Text = arrFreq[1].ToString();
                        }
                        if (chboRCCS36.Checked)
                        {
                            txtExpValue36_1.Text = CalculateMassflowFrequenz(Convert.ToSingle(txt36FlowLRV.Text), Convert.ToSingle(txt36FlowURV.Text), 0, Convert.ToSingle(txt36PulseURV.Text), arrFreq[2]).ToString("0.00");
                            txtPulses36_1.Text = arrFreq[2].ToString();
                        }
                        if (chboRCCS38.Checked)
                        {
                            txtExpValue38_1.Text = CalculateMassflowFrequenz(Convert.ToSingle(txt38FlowLRV.Text), Convert.ToSingle(txt38FlowURV.Text), 0, Convert.ToSingle(txt38PulseURV.Text), arrFreq[3]).ToString("0.00");
                            txtPulses38_1.Text = arrFreq[3].ToString();
                        } 

                        if(chboDUT.Checked)
                        {
                            if (txtMFDutLRV.Text != string.Empty && txtMFDutURV.Text != string.Empty)
                            {
                                txtDutCurrentValue_1.Text = CalculateMassflowCurrent(Convert.ToSingle(txtMFDutLRV.Text), Convert.ToSingle(txtMFDutURV.Text), Convert.ToSingle(txtDutCurrentLRV.Text), Convert.ToSingle(txtDutCurrentURV.Text), CurrentValue).ToString("0.00");
                                txtDutPulseValue_1.Text = CalculateMassflowFrequenz(Convert.ToSingle(txtMFDutLRV.Text), Convert.ToSingle(txtMFDutURV.Text), Convert.ToSingle(txtDutPulseLRV.Text), Convert.ToSingle(txtDutPulseURV.Text), arrFreq[4]).ToString("0.00");
                            }
                            txtDUTPulses_1.Text =  arrFreq[4].ToString();
                            txtDUTCurrent_1.Text = CurrentValue.ToString();
                        }                       
                        if (!chboRCCS32.Checked && !chboRCCS34.Checked && !chboRCCS36.Checked && !chboRCCS38.Checked)
                                MessageBox.Show("No output selected", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        
                    }

                    if (!AbortTest)
                    {
                        //Get Rotamass Data Density and Temperature
                        RefDensity[1] = MB.ReadDensity(1, ComPortModBus);
                        RefTemperature[1] = MB.ReadTemperature(1, ComPortModBus);
                        RefDensity[3] = MB.ReadDensity(2, ComPortModBus);
                        RefTemperature[3] = MB.ReadTemperature(2, ComPortModBus);
                        RefDensity[5] = MB.ReadDensity(3, ComPortModBus);
                        RefTemperature[5] = MB.ReadTemperature(3, ComPortModBus);
                        RefDensity[7] = MB.ReadDensity(4, ComPortModBus);
                        RefTemperature[7] = MB.ReadTemperature(4, ComPortModBus);
                        //Ambient Conditions
                        AmbLog.TaskStart(COMPortAmbientTemp);
                        System.Threading.Thread.Sleep(1000);        //wait for updates
                        AmbLog.ReadMesureData(COMPortAmbientTemp, ref AmbPresStop, ref AmbhumiStop, ref AmbTempStop, 0, 5, 6);
                        //WaterTempDUTRig
                        DUT_Temp[1] = CustTemp.Get_Temp(COMPortCustTemp);//TempDUT
                        //Vessel,Rig,Cabinet
                        FA_M3.Read4TempValues(3, ComPortFA_M3, ref conditions[1], ref conditions[3], ref Temp3Stop, ref Temp4Stop);//conditions[1]=Vessel ;conditions[3]=WaterRig1;Temp3=cabinet
                        //Flow Pressure
                        FA_M3.Read8AnalogInputValues(4, ComPortFA_M3, ref Val1, ref Val2, ref Val3, ref Val4, ref Val5, ref Val6, ref Val7, ref Val8);
                        conditions[7] = CalculatePressure(0, 35, Val1);//conditions[7]=DN25
                        conditions[5] = CalculatePressure(0, 35, Val2);//conditions[5]=DN50
                        Application.DoEvents();

                        AmbientHumidity = (AmbhumiStart + AmbhumiStop) / (float)2;
                        AmbientPressure = (AmbPresStart + AmbPresStop) / (float)2;
                        AmbientTemperature = (AmbTempStart + AmbTempStop) / (float)2;

                        saveExperimentalData(arrFreq, RefDensity, RefTemperature, CurrentValue, conditions, DUT_Temp);
                    }
                }//singlemode
                //TODO auskommentieren nach Hardwareverbindung
                //MB.WritPressure(1, 0, ComPortModBus);
                //MB.WritPressure(2, 0, ComPortModBus);
                //MB.WritPressure(3, 0, ComPortModBus);
                //MB.WritPressure(4, 0, ComPortModBus);
            }//DUT checked
            btnStart.Enabled = true;
        }

        private void btnCancelTest_Click(object sender, EventArgs e)
        {
            Task task = Task.Factory.StartNew(() =>
            {
                AbortTest = true;
                FA_M3.IsCalibCanceled = true;
            });
        }
        #endregion Experimental

        private void cboTestTimeEXP_SelectedIndexChanged(object sender, EventArgs e)
        {
            TestTimeExp = Convert.ToInt32(cboTestTimeEXP.Text);
            toolStripProgressBar1.Maximum = TestTime;
        }

        private void chboxextCurrent_CheckedChanged(object sender, EventArgs e)
        {
            if(rbtnPulseActive.Checked || rbtnPulsePassive.Checked)
            {
                FA_M3.CloseRelay(2, 2, ComPortFA_M3);
                MessageBox.Show("Please attach the current output wire (I out+/-)", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void saveExperimentalData(float[] arrFreq,float[] RefDensity,float[] RefTemperature,float CurrentValue,float[] conditions,double[] DUT_Temp)
        {
            DataRow newTestingDataRow =  dsTesting1.Tables[0].NewRow();
            
            newTestingDataRow["DateTime"] = DateTime.Now;
            if (txtDUTName.Text!=string.Empty)
                newTestingDataRow["TestName"] = txtDUTName.Text.Trim();
            if (txtdescription.Text != string.Empty)
                newTestingDataRow["TestDescription"] = txtdescription.Text.Trim();
            newTestingDataRow["AmbientTemp"] = AmbientTemperature;
            newTestingDataRow["AmbientPressure"] = AmbientPressure;
            newTestingDataRow["AmbientHumidity"] = AmbientHumidity;
            if(rbtnloop.Checked)
                newTestingDataRow["Loop_SingleTest"] = "Loop";
            else
                newTestingDataRow["Loop_SingleTest"] = "Single";
            newTestingDataRow["Mass_VolumeFlow"] = cboFlowUnitExp.Text.Trim();
            newTestingDataRow["TestingTime"] = cboTestTimeEXP.Text;
            if (txtPumpSettings.Text != string.Empty)
                newTestingDataRow["PumpSettings"] = txtPumpSettings.Text.Trim();
            if(chboRCCS32.Checked)
            {
                try
                {
                    newTestingDataRow["RefDeviceRCCS32"] = txtRCCS32Name.Text.Trim();
                    if (arrFreq[0].ToString() != string.Empty)
                        newTestingDataRow["PulseNumber32"] = arrFreq[0];
                    newTestingDataRow["Flow32"] = txtExpValue32_1.Text.Trim();
                    newTestingDataRow["DensityStart32"] = RefDensity[0];//.ToString()
                    newTestingDataRow["DensityStop32"] = RefDensity[1];//.ToString()
                    newTestingDataRow["TempStart32"] = RefTemperature[0];//.ToString()
                    newTestingDataRow["TempStop32"] = RefTemperature[1];//.ToString()
                    if (txt32PulseURV.Text != string.Empty)
                        newTestingDataRow["PulseURV32"] = txt32PulseURV.Text.Trim();
                    if (txt32FlowURV.Text != string.Empty)
                        newTestingDataRow["FlowURV32"] = txt32FlowURV.Text.Trim();
                    if (txt32FlowURV.Text != string.Empty)
                        newTestingDataRow["FlowLRV32"] = txt32FlowURV.Text.Trim();
                    newTestingDataRow["32UnitURV_LRV"] = cboFlowUnitExp.Text.Trim();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
               
            }
            if (chboRCCS34.Checked)
            {
                try
                {
                    newTestingDataRow["RefDeviceRCCS34"] = txtRCCS34Name.Text.Trim();
                    if (arrFreq[1].ToString() != string.Empty)
                        newTestingDataRow["PulseNumber34"] = arrFreq[1];
                    newTestingDataRow["Flow34"] = txtExpValue34_1.Text.Trim();
                    newTestingDataRow["DensityStart34"] = RefDensity[2];
                    newTestingDataRow["DensityStop34"] = RefDensity[3];
                    newTestingDataRow["TempStart34"] = RefTemperature[2];
                    newTestingDataRow["TempStop34"] = RefTemperature[3];
                    if (txt34PulseURV.Text != string.Empty)
                        newTestingDataRow["PulseURV34"] = txt34PulseURV.Text.Trim();
                    if (txt34FlowURV.Text != string.Empty)
                        newTestingDataRow["FlowURV34"] = txt34FlowURV.Text.Trim();
                    if (txt34FlowURV.Text != string.Empty)
                        newTestingDataRow["FlowLRV34"] = txt34FlowURV.Text.Trim();
                    newTestingDataRow["34UnitURV_LRV"] = cboFlowUnitExp.Text.Trim();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                
            }
            if (chboRCCS36.Checked)
            {
                try
                {
                    newTestingDataRow["RefDeviceRCCS36"] = txtRCCS36Name.Text.Trim();
                    if (arrFreq[2].ToString() != string.Empty)
                        newTestingDataRow["PulseNumber36"] = arrFreq[2];
                    newTestingDataRow["Flow36"] = txtExpValue36_1.Text.Trim();
                    newTestingDataRow["DensityStart36"] = RefDensity[4];
                    newTestingDataRow["DensityStop36"] = RefDensity[5];
                    newTestingDataRow["TempStart36"] = RefTemperature[4];
                    newTestingDataRow["TempStop36"] = RefTemperature[5];
                    if (txt36PulseURV.Text != string.Empty)
                        newTestingDataRow["PulseURV36"] = txt36PulseURV.Text.Trim();                        
                    if (txt36FlowURV.Text != string.Empty)
                        newTestingDataRow["FlowURV36"] = txt36FlowURV.Text.Trim();                        
                    if (txt36FlowURV.Text != string.Empty)
                        newTestingDataRow["FlowLRV36"] = txt36FlowURV.Text.Trim();                        
                    newTestingDataRow["36UnitURV_LRV"] = cboFlowUnitExp.Text.Trim();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
            if (chboRCCS38.Checked)
            {
                try
                {
                    newTestingDataRow["RefDeviceRCCS38"] = txtRCCS38Name.Text.Trim();
                    newTestingDataRow["PulseNumber38"] = arrFreq[3];
                    newTestingDataRow["Flow38"] = txtExpValue38_1.Text.Trim();
                    newTestingDataRow["DensityStart38"] = RefDensity[6];
                    newTestingDataRow["DensityStop38"] = RefDensity[7];
                    newTestingDataRow["TempStart38"] = RefTemperature[6];
                    newTestingDataRow["TempStop38"] = RefTemperature[7];
                    if (txt38PulseURV.Text != string.Empty)
                        newTestingDataRow["PulseURV38"] = txt38PulseURV.Text.Trim();
                    if (txt38FlowURV.Text != string.Empty)
                        newTestingDataRow["FlowURV38"] = txt38FlowURV.Text.Trim();
                    if (txt38FlowURV.Text != string.Empty)
                        newTestingDataRow["FlowLRV38"] = txt38FlowURV.Text.Trim();
                    newTestingDataRow["38UnitURV_LRV"] = cboFlowUnitExp.Text.Trim();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            try
            {
                if (txtDUTName.Text !=string.Empty)
                    newTestingDataRow["DUT"] = txtDUTName.Text.Trim();
                if (arrFreq[4].ToString() != string.Empty)
                    newTestingDataRow["PulseNumberDUT"] = arrFreq[4];
                if (txtDutPulseValue_1.Text != string.Empty)
                    newTestingDataRow["PulseFlowDUT"] = txtDutPulseValue_1.Text.Trim();
                newTestingDataRow["CurrentDUT"] = CurrentValue;
                if (txtDutCurrentValue_1.Text != string.Empty)
                    newTestingDataRow["CurrentFlowDUT"] = txtDutCurrentValue_1.Text.Trim();
                if (txtDutPulseURV.Text != string.Empty)
                    newTestingDataRow["PulseURVDUT"] = txtDutPulseURV.Text;
                if (txtMFDutURV.Text != string.Empty)
                    newTestingDataRow["FlowURVDUT"] = txtMFDutURV.Text;
                if (txtMFDutLRV.Text != string.Empty)
                    newTestingDataRow["FlowLRVDUT"] = txtMFDutLRV.Text;
                newTestingDataRow["DUTUnitURV_LRV"] = cboFlowUnitExp.Text.Trim();
                newTestingDataRow["VesselTempStart"] = conditions[0];
                newTestingDataRow["VesselTempStop"] = conditions[1];
                newTestingDataRow["TempDUTStart"] = DUT_Temp[0];
                newTestingDataRow["TempDUTStop"] = DUT_Temp[1];
                newTestingDataRow["TempRigStart"] = conditions[2];
                newTestingDataRow["TempRigStop"] = conditions[3];
                newTestingDataRow["PressureDN25Start"] = conditions[6];
                newTestingDataRow["PressureDN25Stop"] = conditions[7];
                newTestingDataRow["PressureDN50Start"] = conditions[4];
                newTestingDataRow["PressureDN50Stop"] = conditions[5];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            dsTesting1.Tables[0].Rows.Add(newTestingDataRow);
            oleDbDATesting.Update(dsTesting1, "Testing");

        }

        private void btnInterrupt_Click(object sender, EventArgs e)
        {
            interrupt = true;
        }

        private void btnClearAll_Click(object sender, EventArgs e)
        {
            txtRefVal1.Text= string.Empty;
            txtRefVal2.Text= string.Empty;
            txtRefVal3.Text= string.Empty;
            txtRefVal4.Text = string.Empty;

            txtPoint1.Text = string.Empty;
            txtPoint2.Text = string.Empty;
            txtPoint3.Text = string.Empty;
            txtPoint4.Text = string.Empty;
            txtPoint5.Text = string.Empty;
            txtPoint6.Text = string.Empty;
            txtPoint7.Text = string.Empty;
            txtPoint8.Text = string.Empty;
            txtPoint9.Text = string.Empty;
            txtPoint10.Text = string.Empty;
            
            txtPoint1Ref.Text = string.Empty;
            txtPoint2Ref.Text = string.Empty;
            txtPoint3Ref.Text = string.Empty;
            txtPoint4Ref.Text = string.Empty;
            txtPoint5Ref.Text = string.Empty;
            txtPoint6Ref.Text = string.Empty;
            txtPoint7Ref.Text = string.Empty;
            txtPoint8Ref.Text = string.Empty;
            txtPoint9Ref.Text = string.Empty;
            txtPoint10Ref.Text = string.Empty;

            txtPoint1Cust.Text = string.Empty;
            txtPoint2Cust.Text = string.Empty;
            txtPoint3Cust.Text = string.Empty;
            txtPoint4Cust.Text = string.Empty;
            txtPoint5Cust.Text = string.Empty;
            txtPoint6Cust.Text = string.Empty;
            txtPoint7Cust.Text = string.Empty;
            txtPoint8Cust.Text = string.Empty;
            txtPoint9Cust.Text = string.Empty;
            txtPoint10Cust.Text = string.Empty;

            txtPoint1ErrAbs.Text = string.Empty;
            txtPoint2ErrAbs.Text = string.Empty;
            txtPoint3ErrAbs.Text = string.Empty;
            txtPoint4ErrAbs.Text = string.Empty;
            txtPoint5ErrAbs.Text = string.Empty;
            txtPoint6ErrAbs.Text = string.Empty;
            txtPoint7ErrAbs.Text = string.Empty;
            txtPoint8ErrAbs.Text = string.Empty;
            txtPoint9ErrAbs.Text = string.Empty;
            txtPoint10ErrAbs.Text = string.Empty;

            txtPoint1ErrRel.Text = string.Empty;
            txtPoint2ErrRel.Text = string.Empty;
            txtPoint3ErrRel.Text = string.Empty;
            txtPoint4ErrRel.Text = string.Empty;
            txtPoint5ErrRel.Text = string.Empty;
            txtPoint6ErrRel.Text = string.Empty;
            txtPoint7ErrRel.Text = string.Empty;
            txtPoint8ErrRel.Text = string.Empty;
            txtPoint9ErrRel.Text = string.Empty;
            txtPoint10ErrRel.Text = string.Empty;
        }

        private void rbtnExpCurPassive_CheckedChanged(object sender, EventArgs e)
        {
            if(rbtnExpCurrentPassive.Checked)
                FA_M3.CloseRelay(2, 2, ComPortFA_M3);
        }

        private void rbtnExpCurrentAktive_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnExpCurrentAktive.Checked)
                FA_M3.OpenRelay(2, 2, ComPortFA_M3);
        }

        private void rbtnExpPulseAktive_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnExpPulseAktive.Checked)
                FA_M3.CloseRelay(2, 1, ComPortFA_M3);
        }

        private void rbtnExpPulsePassive_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnExpPulsePassive.Checked)
                FA_M3.OpenRelay(2, 1, ComPortFA_M3);
        }

        private void RbtnOnePump_CheckedChanged(object sender, EventArgs e)
        {
            if (RbtnOnePump.Checked)
            {
                GbxPumpselect.Visible = true;
                if (RbtnPump1.Checked)
                {
                    IsPump1 = true;
                    IsPump2 = false;
                }
                if (RbtnPump2.Checked)
                {
                    IsPump1 = false;
                    IsPump2 = true;
                }
            }
        }

        private void RbtnTwoPump_CheckedChanged(object sender, EventArgs e)
        {
            if (RbtnTwoPump.Checked)
            {
                IsPump1 = true;
                IsPump2 = true;
                GbxPumpselect.Visible = false;
            }
        }

        private void RbtnPump1_CheckedChanged(object sender, EventArgs e)
        {
            if (RbtnOnePump.Checked && RbtnPump1.Checked)
            {
                IsPump1 = true;
                IsPump2 = false;
            }
            if (RbtnOnePump.Checked && RbtnPump2.Checked)
            {
                IsPump1 = false;
                IsPump2 = true;
            }
        }

        private void RbtnPump2_CheckedChanged(object sender, EventArgs e)
        {
            if (RbtnOnePump.Checked && RbtnPump1.Checked)
            {
                IsPump1 = true;
                IsPump2 = false;
            }
            if (RbtnOnePump.Checked && RbtnPump2.Checked)
            {
                IsPump1 = false;
                IsPump2 = true;
            }
        }

        private void NuUD_ValueChanged(object sender, EventArgs e)
        {
            byte[] SollValue = new byte[4];
            byte[] Command = new byte[4];
            byte[] arrReply = new byte[4];
            Int32 PumpValve = Convert.ToInt32(NuUD.Value * 1000);
            string hexValue = PumpValve.ToString("X");
            float value = 0;

            if (hexValue.Length == 3)
            {
                SollValue[0] = 0x00;
                SollValue[1] = 0x00;
                SollValue[2] = byte.Parse(hexValue.Substring(0, 1), System.Globalization.NumberStyles.HexNumber);
                SollValue[3] = byte.Parse(hexValue.Substring(1, 2), System.Globalization.NumberStyles.HexNumber);
            }
            if (hexValue.Length == 4)
            {
                SollValue[0] = 0x00;
                SollValue[1] = 0x00;
                SollValue[2] = byte.Parse(hexValue.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
                SollValue[3] = byte.Parse(hexValue.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
            }
            if (hexValue.Length == 5)
            {
                SollValue[0] = 0x00;
                SollValue[1] = byte.Parse(hexValue.Substring(0, 1), System.Globalization.NumberStyles.HexNumber);
                SollValue[2] = byte.Parse(hexValue.Substring(1, 2), System.Globalization.NumberStyles.HexNumber);
                SollValue[3] = byte.Parse(hexValue.Substring(3, 2), System.Globalization.NumberStyles.HexNumber);
            }
            if (hexValue.Length == 6)
            {
                SollValue[0] = 0x00;
                SollValue[1] = byte.Parse(hexValue.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
                SollValue[2] = byte.Parse(hexValue.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
                SollValue[3] = byte.Parse(hexValue.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);
            }
            if (IsPump1)
            {
                MB.WritePumpDriveSollwertBus(5, SollValue, ComPortModBus);
                MB.ReadPumpDriveSollwertBus(5, arrReply, ComPortModBus);
            }

            if (IsPump2)
            {
                MB.WritePumpDriveSollwertBus(7, SollValue, ComPortModBus);
                MB.ReadPumpDriveSollwertBus(7, arrReply, ComPortModBus);
            }
            HexBytetoFloat(arrReply, ref value);
            Txtactualvalue.Text = Convert.ToString(value);
        }
        
        private void BtnStopPumps_Click(object sender, EventArgs e)
        {
            byte[] Command = new byte[4];
            Command[3] = 0x00;//Stop Pump
            MB.WritePumpDriveCommand(5, Command, ComPortModBus);
            MB.WritePumpDriveCommand(7, Command, ComPortModBus);
            Command[3] = 0x00;//Pump OFF
            MB.WritePumpDriveMode(5,Command,ComPortModBus);
            MB.WritePumpDriveMode(7,Command,ComPortModBus);
            GbxPumpselect.Enabled = true;
            GbxPumpMode.Enabled = true;
            NuUD.Enabled = false;
            NuUD.Value = 50;
        }

        private void BtnStartPump_Click(object sender, EventArgs e)
        {
            GbxPumpselect.Enabled = false;
            GbxPumpMode.Enabled = false;
            NuUD.Enabled = true;
            byte[] SollValue = new byte[4];
            byte[] Command = new byte[4];
            byte[] arrReplay = new byte[4];
            byte pump1 = 0;
            byte pump2 = 0;
            Txtactualvalue.Text = 50.ToString();

            if (IsPump1 && IsPump2)
            {
                SollValue[2] = 0xC3;  //50%
                SollValue[3] = 0x50;  //50%
                MB.WritePumpDriveSollwertBus(5, SollValue, ComPortModBus);
                MB.WritePumpDriveSollwertBus(7, SollValue, ComPortModBus);
                Command[2] = 0x00;
                Command[3] = 0x01;//auto
                MB.WritePumpDriveMode(5, Command, ComPortModBus);
                MB.WritePumpDriveMode(7, Command, ComPortModBus);
                MB.ReadPumpDriveStatus(5, arrReplay, ComPortModBus);
                pump1=arrReplay[3];
                MB.ReadPumpDriveStatus(7, arrReplay, ComPortModBus);
                pump2 = arrReplay[3];
                //for automatic mode
                if (pump1 == 0x03 && pump2 == 0x03)
                {
                    Command[2] = 0x00;//Start Pump
                    Command[3] = 0x04;//Start Pump
                    MB.WritePumpDriveCommand(5, Command, ComPortModBus);
                    MB.WritePumpDriveCommand(7, Command, ComPortModBus);
                }
                else
                {
                    var result = MessageBox.Show("Please release pump button on Pump/Vessel-Cart to start PumpDrive", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    if (DialogResult.OK == result)
                    {
                        NuUD.Value=50;
                        GbxPumpselect.Enabled = true;
                        GbxPumpMode.Enabled = true;
                        NuUD.Enabled = false;
                        Command[3] = 0x00;//Pump OFF
                        MB.WritePumpDriveMode(5, Command, ComPortModBus);
                        MB.WritePumpDriveMode(7, Command, ComPortModBus);
                    }
                }
            }
            else
            {
                if (IsPump1)
                {
                    SollValue[2] = 0xC3;  //50%
                    SollValue[3] = 0x50;  //50%
                    MB.WritePumpDriveSollwertBus(5, SollValue, ComPortModBus);
                    Command[2] = 0x00;
                    Command[3] = 0x01;//auto
                    MB.WritePumpDriveMode(5, Command, ComPortModBus);
                    MB.ReadPumpDriveStatus(5, arrReplay, ComPortModBus);
                    //for automatic mode
                    if (arrReplay[3] == 0x03)
                    {
                        Command[2] = 0x00;//Start Pump
                        Command[3] = 0x04;//Start Pump
                        MB.WritePumpDriveCommand(5, Command, ComPortModBus);
                    }
                    else
                    {
                        var result = MessageBox.Show("Please release pump button on Pump/Vessel-Cart to start PumpDrive", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        if (DialogResult.OK == result)
                        {
                            NuUD.Value = 50;
                            GbxPumpselect.Enabled = true;
                            GbxPumpMode.Enabled = true;
                            NuUD.Enabled = false;
                            Command[3] = 0x00;//Pump OFF
                            MB.WritePumpDriveMode(5, Command, ComPortModBus);
                        }
                    }
                }
                else if (IsPump2)
                {
                    SollValue[2] = 0xC3;  //50%
                    SollValue[3] = 0x50;  //50%
                    MB.WritePumpDriveSollwertBus(7, SollValue, ComPortModBus);
                    Command[2] = 0x00;
                    Command[3] = 0x01;//auto
                    MB.WritePumpDriveMode(7, Command, ComPortModBus);
                    MB.ReadPumpDriveStatus(7, arrReplay, ComPortModBus);
                    //for automatic mode
                    if (arrReplay[3] == 0x03)
                    {
                        Command[2] = 0x00;//Start Pump
                        Command[3] = 0x04;//Start Pump
                        MB.WritePumpDriveCommand(7, Command, ComPortModBus);
                    }
                    else
                    {
                        var result = MessageBox.Show("Please release pump button on Pump/Vessel-Cart to start PumpDrive", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        if (DialogResult.OK == result)
                        {
                            NuUD.Value = 50;
                            GbxPumpselect.Enabled = true;
                            GbxPumpMode.Enabled = true;
                            NuUD.Enabled = false;
                            Command[3] = 0x00;//Pump OFF
                            MB.WritePumpDriveMode(7, Command, ComPortModBus);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Please select Pump", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    GbxPumpselect.Enabled = true;
                    GbxPumpMode.Enabled = true;
                }
            }
        }//end pump start

        public void HexBytetoFloat(byte[] arrReply,ref float value)
        {
            string hex = BitConverter.ToString(arrReply).Replace("-", "");
            value = (System.Int32.Parse(hex,System.Globalization.NumberStyles.AllowHexSpecifier)/(float)1000);
        }
    }
}
