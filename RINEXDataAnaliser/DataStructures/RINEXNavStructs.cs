using System.Text.RegularExpressions;
using System.Globalization;

namespace RINEXDataAnaliser.DataStructures
{

    public enum ParseStates
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

    #region GPS

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

        public RINEXNavGPSData findNeedEpoch(string sateliteNumber, DateTime needEpochTime)
        {
            TimeSpan ephemStep = new TimeSpan(2, 0, 0);
            for (int i = 0; i < data.Count; i++)
            {
                if (sateliteNumber == data[i].sateliteNumber &&
                    (needEpochTime - ephemStep) < data[i].dateTime)
                {
                    return data[i];
                }
            }

            return null;
        }

        public void ParceFile(string filePath, RegexManager regexManager)
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
                                navGPSData.clockDrift = Convert.ToDouble(match.Groups["ClockDrift"].Value.Replace("D", "E"), CultureInfo.InvariantCulture);
                                navGPSData.clockDriftRate = Convert.ToDouble(match.Groups["ClockDriftRate"].Value.Replace("D", "E"), CultureInfo.InvariantCulture);
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

    #endregion

    #region GLOANSS

    public class RINEXNavGLONASSFile
    {
        public RINEXNavGLONASSHeader header = new();
        public List<RINEXNavGLONASSData> data = new();

        public RINEXNavGLONASSData findNeedEpoch(string sateliteNumber, DateTime needEpochTime)
        {
            TimeSpan maxTimeDelta = new TimeSpan(0, 30, 0);
            bool sateliteExist = false;
            foreach (var epoh in data)
            {
                if (sateliteNumber == epoh.sateliteNumber)
                {
                    sateliteExist = true;
                    if (needEpochTime >= epoh.epochTime &&
                       (needEpochTime - epoh.epochTime) <= maxTimeDelta)
                    {
                        return epoh;
                    }
                }
            }

            if (needEpochTime > data.Last().epochTime)
            {
                Console.WriteLine("Эфемериды слишком старые");
            }
            else if (sateliteExist == false)
            {
                Console.WriteLine("Спутник " + sateliteNumber + " отсутствует в списке");
            }
            else
            {
                Console.WriteLine("Эфемериды силшком новые");
            }
            return null;
        }

        public void parceFile(string filePath, RegexManager regexManager)
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
                            match = regexManager.gloNavEpohs["SV / EPOCH / SV CLK"].Match(fileLine);
                            if (match.Success)
                            {
                                RINEXNavGLONASSData navGLONASSData = new();
                                navGLONASSData.sateliteNumber = match.Groups["SateliteNumber"].Value;
                                navGLONASSData.epochTime = new DateTime(Convert.ToInt16(match.Groups["Year"].Value),
                                                                        Convert.ToInt16(match.Groups["Month"].Value),
                                                                        Convert.ToInt16(match.Groups["Day"].Value),
                                                                        Convert.ToInt16(match.Groups["Hour"].Value),
                                                                        Convert.ToInt16(match.Groups["Minute"].Value),
                                                                        Convert.ToInt16(match.Groups["Second"].Value));
                                navGLONASSData.clockBias = Convert.ToDouble(match.Groups["ClockBias"].Value.Replace("D", "E"), CultureInfo.InvariantCulture);
                                navGLONASSData.frequencyBias = Convert.ToDouble(match.Groups["FreqBias"].Value.Replace("D", "E"), CultureInfo.InvariantCulture);
                                navGLONASSData.messageFrameTime = Convert.ToDouble(match.Groups["MessageFrameTime"].Value.Replace("D", "E"), CultureInfo.InvariantCulture);
                                data.Add(navGLONASSData);
                                parseState = ParseStates.BroadcastOrbit1;
                            }
                            break;
                        case ParseStates.BroadcastOrbit1:
                            match = regexManager.gloNavEpohs["BROADCAST ORBIT - 1"].Match(fileLine);
                            if (match.Success)
                            {
                                data.Last().satelitePos.X = Convert.ToDouble(match.Groups["SatelitePosX"].Value.Replace("D", "E"), CultureInfo.InvariantCulture);
                                data.Last().sateliteVelocity.X = Convert.ToDouble(match.Groups["VelocityX"].Value.Replace("D", "E"), CultureInfo.InvariantCulture);
                                data.Last().sateliteAcceleration.X = Convert.ToDouble(match.Groups["AccelerationX"].Value.Replace("D", "E"), CultureInfo.InvariantCulture);
                                data.Last().health = Convert.ToDouble(match.Groups["Health"].Value.Replace("D", "E"), CultureInfo.InvariantCulture);
                                parseState = ParseStates.BroadcastOrbit2;
                            }
                            break;
                        case ParseStates.BroadcastOrbit2:
                            match = regexManager.gloNavEpohs["BROADCAST ORBIT - 2"].Match(fileLine);
                            if (match.Success)
                            {
                                data.Last().satelitePos.Y = Convert.ToDouble(match.Groups["SatelitePosY"].Value.Replace("D", "E"), CultureInfo.InvariantCulture);
                                data.Last().sateliteVelocity.Y = Convert.ToDouble(match.Groups["VelocityY"].Value.Replace("D", "E"), CultureInfo.InvariantCulture);
                                data.Last().sateliteAcceleration.Y = Convert.ToDouble(match.Groups["AccelerationY"].Value.Replace("D", "E"), CultureInfo.InvariantCulture);
                                data.Last().freqencyNumber = Convert.ToDouble(match.Groups["FrequencyNumber"].Value.Replace("D", "E"), CultureInfo.InvariantCulture);
                                parseState = ParseStates.BroadcastOrbit3;
                            }
                            break;
                        case ParseStates.BroadcastOrbit3:
                            match = regexManager.gloNavEpohs["BROADCAST ORBIT - 3"].Match(fileLine);
                            if (match.Success)
                            {
                                data.Last().satelitePos.Z = Convert.ToDouble(match.Groups["SatelitePosZ"].Value.Replace("D", "E"), CultureInfo.InvariantCulture);
                                data.Last().sateliteVelocity.Z = Convert.ToDouble(match.Groups["VelocityZ"].Value.Replace("D", "E"), CultureInfo.InvariantCulture);
                                data.Last().sateliteAcceleration.Z = Convert.ToDouble(match.Groups["AccelerationZ"].Value.Replace("D", "E"), CultureInfo.InvariantCulture);
                                data.Last().ageOfOperInfo = Convert.ToDouble(match.Groups["AgeOfOperInfo"].Value.Replace("D", "E"), CultureInfo.InvariantCulture);
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

    public class RINEXNavGLONASSHeader
    {

    }

    public class RINEXNavGLONASSData
    {
        public string sateliteNumber = "";
        public DateTime epochTime;
        public double clockBias = 0.0;
        public double frequencyBias = 0.0;
        public double messageFrameTime = 0.0;
        public XYZCoordinates satelitePos = new();
        public XYZCoordinates sateliteVelocity = new();
        public XYZCoordinates sateliteAcceleration = new();
        public double health = 0.0;
        public double freqencyNumber = 0.0;
        public double ageOfOperInfo= 0.0;
    }

    #endregion

    #region GALILEO

    public class RINEXNavGALILEOFile
    {
        public RINEXNavGALILEOHeader header = new();
        public List<RINEXNavGALILEOData> data = new();

        public RINEXNavGALILEOData findNeedEpoch(string sateliteNumber, DateTime needEpochTime)
        {
            TimeSpan epochUpdateTime = new TimeSpan(0, 10, 0);
            TimeSpan halfEpochUpdateTime = new TimeSpan(epochUpdateTime.Ticks / 2);
            bool sateliteExist = false;
            foreach (var epoh in data)
            {
                if (sateliteNumber == epoh.sateliteNumber)
                {
                    sateliteExist = true;
                    if (needEpochTime >= epoh.dateTime - halfEpochUpdateTime &&
                       needEpochTime <= epoh.dateTime + halfEpochUpdateTime)
                    {
                        return epoh;
                    }
                }
            }

            if (needEpochTime > data.Last().dateTime)
            {
                Console.WriteLine("Эфемериды слишком старые");
            }
            else if (sateliteExist == false)
            {
                Console.WriteLine("Спутник " + sateliteNumber + " отсутствует в списке");
            }
            else
            {
                Console.WriteLine("Эфемериды для спутника " + sateliteNumber + " слишком новые");
            }
            return null;
        }

        public void ParseFile(string filePath, RegexManager regexManager)
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
                            match = regexManager.galNavEpohs["SV / EPOCH / SV CLK"].Match(fileLine);
                            if (match.Success)
                            {
                                RINEXNavGALILEOData navGALILEOData = new();
                                navGALILEOData.sateliteNumber = match.Groups["SateliteNumber"].Value;
                                navGALILEOData.dateTime = new DateTime(Convert.ToInt16(match.Groups["Year"].Value),
                                                                   Convert.ToInt16(match.Groups["Month"].Value),
                                                                   Convert.ToInt16(match.Groups["Day"].Value),
                                                                   Convert.ToInt16(match.Groups["Hour"].Value),
                                                                   Convert.ToInt16(match.Groups["Minute"].Value),
                                                                   Convert.ToInt16(match.Groups["Second"].Value));
                                navGALILEOData.clockBias = Convert.ToDouble(match.Groups["ClockBias"].Value.Replace("D", "E"), CultureInfo.InvariantCulture);
                                navGALILEOData.clockDrift = Convert.ToDouble(match.Groups["ClockDrift"].Value.Replace("D", "E"), CultureInfo.InvariantCulture);
                                navGALILEOData.clockDriftRate = Convert.ToDouble(match.Groups["ClockDriftRate"].Value.Replace("D", "E"), CultureInfo.InvariantCulture);
                                data.Add(navGALILEOData);
                                parseState = ParseStates.BroadcastOrbit1;
                            }
                            break;
                        case ParseStates.BroadcastOrbit1:
                            match = regexManager.galNavEpohs["BROADCAST ORBIT - 1"].Match(fileLine);
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
                            match = regexManager.galNavEpohs["BROADCAST ORBIT - 2"].Match(fileLine);
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
                            match = regexManager.galNavEpohs["BROADCAST ORBIT - 3"].Match(fileLine);
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
                            match = regexManager.galNavEpohs["BROADCAST ORBIT - 4"].Match(fileLine);
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
                            match = regexManager.galNavEpohs["BROADCAST ORBIT - 5"].Match(fileLine);
                            if (match.Success)
                            {
                                data.Last().iDot = Convert.ToDouble(match.Groups["IDot"].Value.Replace("D", "E"), CultureInfo.InvariantCulture);
                                data.Last().dataSources = Convert.ToDouble(match.Groups["DataSources"].Value.Replace("D", "E"), CultureInfo.InvariantCulture);
                                data.Last().galWeek = Convert.ToDouble(match.Groups["GalWeek"].Value.Replace("D", "E"), CultureInfo.InvariantCulture);
                                parseState = ParseStates.BroadcastOrbit6;
                            }
                            break;
                        case ParseStates.BroadcastOrbit6:
                            match = regexManager.galNavEpohs["BROADCAST ORBIT - 6"].Match(fileLine);
                            if (match.Success)
                            {
                                data.Last().signalAccuracy = Convert.ToDouble(match.Groups["SISA"].Value.Replace("D", "E"), CultureInfo.InvariantCulture);
                                data.Last().svHealth = Convert.ToDouble(match.Groups["SVHealth"].Value.Replace("D", "E"), CultureInfo.InvariantCulture);
                                data.Last().BGDE5a_E1 = Convert.ToDouble(match.Groups["BGDE5a_E1"].Value.Replace("D", "E"), CultureInfo.InvariantCulture);
                                data.Last().BGDE5b_E1 = Convert.ToDouble(match.Groups["BGDE5b_E1"].Value.Replace("D", "E"), CultureInfo.InvariantCulture);
                                parseState = ParseStates.BroadcastOrbit7;
                            }
                            break;
                        case ParseStates.BroadcastOrbit7:
                            match = regexManager.galNavEpohs["BROADCAST ORBIT - 7"].Match(fileLine);
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

    public class RINEXNavGALILEOHeader
    {

    }

    public class RINEXNavGALILEOData
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

        public double dataSources = 0.0;

        public double galWeek = 0.0;

        public double signalAccuracy = 0.0;

        public double svHealth = 0.0;

        public double BGDE5a_E1 = 0.0;

        public double BGDE5b_E1 = 0.0;

        public double transmitionTimeOfMessage = 0.0;
    }

    #endregion
}
