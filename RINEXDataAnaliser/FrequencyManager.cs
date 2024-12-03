using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RINEXDataAnaliser
{
    public class FrequencyManager
    {
        private static readonly Dictionary<string, double> GPSFrequencyRanges = new Dictionary<string, double>
        {
            { "L1", 1575.42 },
            { "L2", 1227.60 },
            { "L5", 1176.45 }
        };

        public static double getGammaGPS()
        {
            return Math.Pow(GPSFrequencyRanges["L1"] / GPSFrequencyRanges["L2"], 2);
        }

        public static (double, double) getK1AndK2CoefsGPS()
        {
            double k1, k2;

            k1 = Math.Pow(GPSFrequencyRanges["L1"], 2) / (Math.Pow(GPSFrequencyRanges["L1"], 2) - Math.Pow(GPSFrequencyRanges["L2"], 2));
            k2 = Math.Pow(GPSFrequencyRanges["L2"], 2) / (Math.Pow(GPSFrequencyRanges["L1"], 2) - Math.Pow(GPSFrequencyRanges["L2"], 2));

            return (k1, k2);
        }
    }
}
