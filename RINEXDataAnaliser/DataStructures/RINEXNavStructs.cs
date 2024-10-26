using System.Text.RegularExpressions;
using System.Globalization;

namespace RINEXDataAnaliser.DataStructures
{
    /// <summary>
    /// Класс для хранения эфимерид из GPS файла
    /// </summary>
    public class RINEXNavGPSFile
    {
        /// <summary>
        /// Поле хранящее заголовок файла эфемерид
        /// </summary>
        public RINEXNavGPSHeader header = new();

        /// <summary>
        /// Поле хранящее данные эпох файла эфемерид
        /// </summary>
        public List<RINEXNavGPSData> data = new();

        private enum ParseStates
        {
            EpochStart,
            BroadcastOrbit1,
            BroadcastOrbit2,
            BroadcastOrbit3,
            BroadcastOrbit4,
            BroadcastOrbit5,
            BroadcastOrbit6,
            BroadcastOrbit7
        }

        public void ParceNavFile(string filePath, RegexManager regexManager)
        {
            bool isHeader = true;
            ParseStates parseState = ParseStates.EpochStart;
            foreach (string fileLine in File.ReadLines(filePath))
            {
                if (isHeader)
                {
                    if (fileLine.Contains("END OF HEADER"))
                    {
                        isHeader = false;
                    }
                }
                else
                {
                    Match match;
                    switch (parseState)
                    {
                        case ParseStates.EpochStart:
                            match = regexManager.gpsNavEpohs["SV / EPOCH / SV CLK"].Match(fileLine);
                            if (match.Success)
                            {
                                RINEXNavGPSData navGPSData = new();
                                navGPSData.sateliteNumber = match.Groups["SateliteNumber"].Value;
                                navGPSData.dateTime = new DateTime(Convert.ToInt16(match.Groups["Year"].Value),
                                                                   Convert.ToInt16(match.Groups["Month"].Value),
                                                                   Convert.ToInt16(match.Groups["Day"].Value),
                                                                   Convert.ToInt16(match.Groups["Hour"].Value),
                                                                   Convert.ToInt16(match.Groups["Minute"].Value),
                                                                   Convert.ToInt16(match.Groups["Second"].Value));
                                navGPSData.clockBias = Convert.ToDouble(match.Groups["ClockBias"].Value.Replace("D", "E"), CultureInfo.InvariantCulture);
                                navGPSData.clockDrift = Convert.ToDouble(match.Groups["ClockDrift"].Value.Replace("D", "E").Replace("D", "E"), CultureInfo.InvariantCulture);
                                navGPSData.clockDriftRate = Convert.ToDouble(match.Groups["ClockDriftRate"].Value.Replace("D", "E").Replace("D", "E"), CultureInfo.InvariantCulture);
                                data.Add(navGPSData);
                                parseState = ParseStates.BroadcastOrbit1;
                            }
                            break;
                        case ParseStates.BroadcastOrbit1:
                            match = regexManager.gpsNavEpohs["BROADCAST ORBIT - 1"].Match(fileLine);
                            if (match.Success)
                            {
                                data.Last().iode = Convert.ToDouble(match.Groups["IODE"].Value.Replace("D", "E"), CultureInfo.InvariantCulture);
                                data.Last().crs = Convert.ToDouble(match.Groups["Crs"].Value.Replace("D", "E"), CultureInfo.InvariantCulture);
                                data.Last().deltaN = Convert.ToDouble(match.Groups["DeltaN"].Value.Replace("D", "E"), CultureInfo.InvariantCulture);
                                data.Last().m0 = Convert.ToDouble(match.Groups["M0"].Value.Replace("D", "E"), CultureInfo.InvariantCulture);
                                parseState = ParseStates.BroadcastOrbit2;
                            }
                            break;
                        case ParseStates.BroadcastOrbit2:
                            match = regexManager.gpsNavEpohs["BROADCAST ORBIT - 2"].Match(fileLine);
                            if (match.Success)
                            {
                                data.Last().cuc = Convert.ToDouble(match.Groups["Cuc"].Value.Replace("D", "E"), CultureInfo.InvariantCulture);
                                data.Last().e = Convert.ToDouble(match.Groups["E"].Value.Replace("D", "E"), CultureInfo.InvariantCulture);
                                data.Last().cus = Convert.ToDouble(match.Groups["Cus"].Value.Replace("D", "E"), CultureInfo.InvariantCulture);
                                data.Last().sqrtA = Convert.ToDouble(match.Groups["SqrtA"].Value.Replace("D", "E"), CultureInfo.InvariantCulture);
                                parseState = ParseStates.BroadcastOrbit3;
                            }
                            break;
                        case ParseStates.BroadcastOrbit3:
                            match = regexManager.gpsNavEpohs["BROADCAST ORBIT - 3"].Match(fileLine);
                            if (match.Success)
                            {
                                data.Last().ttoe = Convert.ToDouble(match.Groups["TTOE"].Value.Replace("D", "E"), CultureInfo.InvariantCulture);
                                data.Last().cic = Convert.ToDouble(match.Groups["Cic"].Value.Replace("D", "E"), CultureInfo.InvariantCulture);
                                data.Last().omega0 = Convert.ToDouble(match.Groups["Omega0"].Value.Replace("D", "E"), CultureInfo.InvariantCulture);
                                data.Last().cis = Convert.ToDouble(match.Groups["Cis"].Value.Replace("D", "E"), CultureInfo.InvariantCulture);
                                parseState = ParseStates.BroadcastOrbit4;
                            }
                            break;
                        case ParseStates.BroadcastOrbit4:
                            match = regexManager.gpsNavEpohs["BROADCAST ORBIT - 4"].Match(fileLine);
                            if (match.Success)
                            {
                                data.Last().i0 = Convert.ToDouble(match.Groups["I0"].Value.Replace("D", "E"), CultureInfo.InvariantCulture);
                                data.Last().crc = Convert.ToDouble(match.Groups["Crc"].Value.Replace("D", "E"), CultureInfo.InvariantCulture);
                                data.Last().omega = Convert.ToDouble(match.Groups["Omega"].Value.Replace("D", "E"), CultureInfo.InvariantCulture);
                                data.Last().omegaDot = Convert.ToDouble(match.Groups["OmegaDot"].Value.Replace("D", "E"), CultureInfo.InvariantCulture);
                                parseState = ParseStates.BroadcastOrbit5;
                            }
                            break;
                        case ParseStates.BroadcastOrbit5:
                            match = regexManager.gpsNavEpohs["BROADCAST ORBIT - 5"].Match(fileLine);
                            if (match.Success)
                            {
                                data.Last().iDot = Convert.ToDouble(match.Groups["IDot"].Value.Replace("D", "E"), CultureInfo.InvariantCulture);
                                data.Last().codesInL2Chanel = Convert.ToDouble(match.Groups["CodesOnL2Chanel"].Value.Replace("D", "E"), CultureInfo.InvariantCulture);
                                data.Last().gpsWeek = Convert.ToDouble(match.Groups["GpsWeek"].Value.Replace("D", "E"), CultureInfo.InvariantCulture);
                                data.Last().l2PDataFlag = Convert.ToDouble(match.Groups["L2PDataFlag"].Value.Replace("D", "E"), CultureInfo.InvariantCulture);
                                parseState = ParseStates.BroadcastOrbit6;
                            }
                            break;
                        case ParseStates.BroadcastOrbit6:
                            match = regexManager.gpsNavEpohs["BROADCAST ORBIT - 6"].Match(fileLine);
                            if (match.Success)
                            {
                                data.Last().svAccuracy = Convert.ToDouble(match.Groups["SVAccuracy"].Value.Replace("D", "E"), CultureInfo.InvariantCulture);
                                data.Last().svHealth = Convert.ToDouble(match.Groups["SVHealth"].Value.Replace("D", "E"), CultureInfo.InvariantCulture);
                                data.Last().tgd = Convert.ToDouble(match.Groups["TGD"].Value.Replace("D", "E"), CultureInfo.InvariantCulture);
                                data.Last().iodc = Convert.ToDouble(match.Groups["IODC"].Value.Replace("D", "E"), CultureInfo.InvariantCulture);
                                parseState = ParseStates.BroadcastOrbit7;
                            }
                            break;
                        case ParseStates.BroadcastOrbit7:
                            match = regexManager.gpsNavEpohs["BROADCAST ORBIT - 7"].Match(fileLine);
                            if (match.Success)
                            {
                                data.Last().transmitionTimeOfMessage = Convert.ToDouble(match.Groups["TransmitionTimeOfMessage"].Value.Replace("D", "E"), CultureInfo.InvariantCulture);
                                parseState = ParseStates.EpochStart;
                            }
                            break;
                        default:
                            break;
                    }
                }
            }
        }
    }

    /// <summary>
    /// Класс для хранения информации из заголовка файла эфемерид
    /// </summary>
    public class RINEXNavGPSHeader
    {

    }

    public class RINEXNavGPSData
    {
        /// <summary>
        /// Номер спутника
        /// </summary>
        public string sateliteNumber = "";

        /// <summary>
        /// Время эпохи
        /// </summary>
        public DateTime dateTime = new();

        /// <summary>
        /// Смещение часов
        /// </summary>
        public double clockBias = 0.0;

        /// <summary>
        /// Дрейф часов
        /// </summary>
        public double clockDrift = 0.0;

        /// <summary>
        /// Скорость дрейфа часов
        /// </summary>
        public double clockDriftRate = 0.0;

        /// <summary>
        /// Уникальный номер данных в эпохе
        /// </summary>
        public double iode = 0.0;

        public double crs = 0.0;

        public double deltaN = 0.0;

        public double m0 = 0.0;

        public double cuc = 0.0;

        public double e = 0.0;

        public double cus = 0.0;

        public double sqrtA = 0.0;

        public double ttoe = 0.0;

        public double cic = 0.0;

        public double omega0 = 0.0;

        public double cis = 0.0;

        public double i0 = 0.0;

        public double crc = 0.0;

        public double omega = 0.0;

        public double omegaDot = 0.0;

        public double iDot = 0.0;

        public double codesInL2Chanel = 0.0;

        public double gpsWeek = 0.0;

        public double l2PDataFlag = 0.0;

        public double svAccuracy = 0.0;

        public double svHealth = 0.0;

        public double tgd = 0.0;

        public double iodc = 0.0;

        public double transmitionTimeOfMessage = 0.0;
    }
}
