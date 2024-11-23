using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RINEXDataAnaliser
{
    public class GNSSTime
    {
        public static DateTime gpsStartTime = new DateTime(1980, 1, 6, 0, 0, 0);
        public static int secondsPerWeek = 604800;

        public static (int, int) getGPSTime(DateTime curentTime)
        {
            int dateDiff = Convert.ToInt32((curentTime - gpsStartTime).TotalSeconds) + 18;
            double gnssWeekDouble = dateDiff / secondsPerWeek;
            int gnssWeek = Convert.ToInt32(Math.Floor(gnssWeekDouble));
            int tow = dateDiff - (gnssWeek * secondsPerWeek);

            return (gnssWeek, tow);
        }
    }
}
