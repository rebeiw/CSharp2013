using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

    public static class RotaHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="FL20"></param>
        /// <param name="KD"></param>
        /// <param name="FTC1"></param>
        /// <param name="FTCK"></param>
        /// <param name="RCTemp"></param>
        /// <param name="RCFreq"></param>
        /// <returns></returns>
        public static double Berechne_Dichte(double FL20, double KD, double FTC1, double FTCK, double RCTemp, double RCFreq)
        {
            double retval               = 0.0;
            double rc20Freq             = Berechne_RC20Frequenz(RCTemp,RCFreq,FTC1,FTCK);
            double rc20FreqQuadrat      = rc20FreqQuadrat = Math.Pow(rc20Freq, 2.0);
            double fl20Quadrat          = fl20Quadrat = Math.Pow(FL20, 2.0);

            retval                      = KD * (fl20Quadrat / rc20FreqQuadrat - 1.0);
            return retval;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="RCTemp"></param>
        /// <param name="RCFreq"></param>
        /// <param name="FTC1"></param>
        /// <param name="FTCK"></param>
        /// <returns></returns>
        public static double Berechne_RC20Frequenz(double RCTemp, double RCFreq, double FTC1, double FTCK)
        {
            double retval               = 0.0;
            double delta20Kelvin        = 0.0;
            double delta20KelvinQuadrat = 0.0;

            delta20Kelvin = RCTemp - 20.0;
            delta20KelvinQuadrat = Math.Pow(delta20Kelvin, 2.0);
            retval = RCFreq / (1.0 + FTC1 * delta20Kelvin + FTCK * delta20KelvinQuadrat);

            return retval;
        }
    }