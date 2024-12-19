using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RinexDataAnaliser
{
    public class FrequencyManager
    {
        private static readonly Dictionary<string, double> GPSFrequencyRanges = new Dictionary<string, double>
        {
            { "L1", 1575.42e6 },
            { "L2", 1227.60e6 },
            { "L5", 1176.45e6 }
        };

        private static readonly Dictionary<string, double> GLONASSFrequencyRanges = new Dictionary<string, double>
        {
            { "G1", 1602.0 },
            { "G2", 1246.0 }
        };

        private static readonly Dictionary<string, double> GalileoFrequencyRanges = new Dictionary<string, double>
        {
            { "E1", 1575.42e6 },
            { "E6", 1278.75e6 },
            { "E5a", 1176.45e6 },
            { "E5b", 1207.14e6 }
        };

        private static readonly Dictionary<string, double> BeidouFrequencyRanges = new Dictionary<string, double>
        {
            { "B1", 1575.42 },
            { "B2", 1191.795 },
            { "B3", 1268.52 }
        };

        public static (double, double) getTwoFreq(GNSSSystem gnssSystem, string f1Name, string f2Name)
        {
            switch (gnssSystem)
            {
                case GNSSSystem.GPS:
                    return (GPSFrequencyRanges[f1Name], GPSFrequencyRanges[f2Name]);
                case GNSSSystem.GLONASS:
                    return (GLONASSFrequencyRanges[f1Name], GLONASSFrequencyRanges[f2Name]);
                case GNSSSystem.GALILEO:
                    return (GalileoFrequencyRanges[f1Name], GalileoFrequencyRanges[f2Name]);
                case GNSSSystem.BEIDOU:
                    return (BeidouFrequencyRanges[f1Name], BeidouFrequencyRanges[f2Name]);
                default:
                    return (0, 0);
            }
        }
    }
}
