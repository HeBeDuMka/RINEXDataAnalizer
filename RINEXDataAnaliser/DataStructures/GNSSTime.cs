using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RINEXDataAnaliser.DataStructures
{

    public class GNSSTime
    {
        private static DateTime getGNNSStartTime(GNSSSystem system)
        {
            switch (system)
            {
                case GNSSSystem.GPS:
                    return new DateTime(1980, 1, 6, 0, 0, 0);
                case GNSSSystem.GLONASS:
                    return new DateTime(1980, 1, 6, 0, 0, 0);
                case GNSSSystem.BEIDOU:
                    return new DateTime(1980, 1, 6, 0, 0, 0);
                case GNSSSystem.GALILEO:
                    return new DateTime(1980, 1, 6, 0, 0, 0);
                default:
                    return new DateTime(1970, 1, 1, 0, 0, 0);
            }
        }

        public static (int, int) calcGNSSWeekandTow(GNSSSystem system, DateTime curentTime)
        {
            int gnssWeek, tow, secondsInWeek = 604800;

            DateTime gnssStartTime = getGNNSStartTime(system);
            gnssWeek = Convert.ToInt32(Math.Floor((curentTime - gnssStartTime).TotalSeconds / secondsInWeek));
            tow = Convert.ToInt32(Math.Floor((curentTime - gnssStartTime).TotalSeconds % secondsInWeek));

            return (gnssWeek, tow);
        }


    }
}
