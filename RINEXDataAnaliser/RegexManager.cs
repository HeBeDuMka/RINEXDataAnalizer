using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RinexDataAnaliser
{
    /// <summary>
    /// Класс хранящий в себе прекомпилированные регулярные выражения
    /// </summary>
    public class RegexManager
    {
        /// <summary>
        /// Список регулярных выражений для парсинга заголовка файла с наблюдениями
        /// </summary>
        public Dictionary<string, Regex> obsHeader = new Dictionary<string, Regex>();

        /// <summary>
        /// Список регулярных выражений для парсинга данных спутников в эпохе файла с наблюдениями
        /// Первое из них для парсинга даты и времени эпохи
        /// </summary>
        public Dictionary<string, Regex> obsEpohs = new Dictionary<string, Regex>();

        /// <summary>
        /// Список регулярных выражений для парсинга данных спутников в эпохе файла с эфимеридами GPS
        /// </summary>
        public Dictionary<string, Regex> gpsNavEpohs = new();

        public Dictionary<string, Regex> gloNavEpohs = new();

        public Dictionary<string, Regex> galNavEpohs = new();

        public Dictionary<string, Regex> bdsNavEpohs = new();

        public RegexManager()
        {
            obsHeader.Add("RINEX VERSION / TYPE", new Regex(@"\s+(?<Rinex_version>\d{1}\.\d{2})\s+(?<Rinex_type>\w+\s\w+)\s+(?<Satelite_system>\w)\s+", RegexOptions.Compiled));
            obsHeader.Add("PGM / RUN BY / DATE", new Regex(@"(?<Record_program_name>[a-zA-Z0-9.-]*)\s+(?<Record_agency_name>[a-zA-Z0-9.-])?\s+(?<Record_date_time>\d+\s\d+)\s(?<Time_zone>\w+)\s+", RegexOptions.Compiled));
            obsHeader.Add("COMMENT", new Regex("[a-zA-Z0-9\\(\\) .-]+", RegexOptions.Compiled));
            obsHeader.Add("MARKER NAME", new Regex(@"(?<Marker_name>[a-zA-Z0-9\(\) .-]+)\s*", RegexOptions.Compiled));
            obsHeader.Add("MARKER NUMBER", new Regex(@"(?<Marker_number>[a-zA-Z0-9\(\) .-]+)\s*", RegexOptions.Compiled));
            obsHeader.Add("MARKER TYPE", new Regex(@"(?<Marker_type>[a-zA-Z0-9\(\) .-]+)\s*", RegexOptions.Compiled));
            obsHeader.Add("OBSERVER / AGENCY", new Regex(@"(?<Observer_name>\w+)\s+(?<Agency_name>\w+)\s+", RegexOptions.Compiled));
            obsHeader.Add("REC # / TYPE / VERS", new Regex(@"(?<Reciver_number>\w+)\s+(?<Reciver_type>\w+\s\w+)\s+(?<Reciver_version>[0-9.]+)\s+", RegexOptions.Compiled));
            obsHeader.Add("ANT # / TYPE", new Regex(@"(?<Antenna_number>\w+)\s+(?<Antenna_type_1>\w+)\s+(?<Antenna_type_2>\w+)\s+", RegexOptions.Compiled));
            obsHeader.Add("APPROX POSITION XYZ", new Regex(@"\s+(?<Antenna_position_x>\w+\.\w+)\s+(?<Antenna_position_y>\w+\.\w+)\s+(?<Antenna_position_z>\w+\.\w+)\s+", RegexOptions.Compiled));
            obsHeader.Add("ANTENNA: DELTA H/E/N", new Regex(@"\s+(?<Antenna_delta_h>\w+\.\w+)\s+(?<Antenna_delta_e>\w+\.\w+)\s+(?<Antenna_delta_n>\w+\.\w+)\s+", RegexOptions.Compiled));
            obsHeader.Add("SYS / # / OBS TYPES", new Regex(@"(?<Satelite_group>\w)\s+(?<Sat_count>\w+)\s+(?<Obs_descriptors>[A-Z0-9 ]+)\s*SYS / # / OBS TYPES", RegexOptions.Compiled));
            obsHeader.Add("SYS / PHASE SHIFT", new Regex(@"(?<Satelite_group>\w)\s(?<Descriptor>\w+)\s+(?<Phase_shift>\d+.\d+)?\s+", RegexOptions.Compiled));
            obsHeader.Add("TIME OF FIRST OBS", new Regex(@"\s+(?<First_obs_date>\d+\s+\d+\s+\d+)\s+(?<First_obs_time>\d+\s+\d+\s+[0-9.]+)\s+(?<Time_system>\w+)\s+", RegexOptions.Compiled));

            obsEpohs.Add("Date", new Regex(@">\s(?<Year>\d{4})\s(?<Month>\d{2})\s(?<Day>\d{2})\s(?<Hour>\d{2})\s(?<Minute>\d{2})\s+(?<Second>\d+).\d+\s+(?<Epoch_flag>\d{1})\s(?<Satelites_count>\d+)", RegexOptions.Compiled));

            //https://regex101.com/r/KIdcVR/1
            gpsNavEpohs.Add("SV / EPOCH / SV CLK", new Regex(@"(?<SateliteNumber>\w{3})\s(?<Year>\d{4})\s(?<Month>\d{2})\s(?<Day>\d{2})\s+(?<Hour>\d{1,2})\s+(?<Minute>\d{1,2})\s+(?<Second>\d{1,2})(?<ClockBias>[0-9\.\-\D]{19})(?<ClockDrift>[0-9\.\-\D]{19})(?<ClockDriftRate>[0-9\.\-\D]{19})", RegexOptions.Compiled));
            gpsNavEpohs.Add("BROADCAST ORBIT - 1", new Regex(@"\s{4}(?<IODE>[0-9\.\-\D]{19})(?<Crs>[0-9\.\-\D]{19})(?<DeltaN>[0-9\.\-\D]{19})(?<M0>[0-9\.\-\D]{19})", RegexOptions.Compiled));
            gpsNavEpohs.Add("BROADCAST ORBIT - 2", new Regex(@"\s{4}(?<Cuc>[0-9\.\-\D]{19})(?<E>[0-9\.\-\D]{19})(?<Cus>[0-9\.\-\D]{19})(?<SqrtA>[0-9\.\-\D]{19})", RegexOptions.Compiled));
            gpsNavEpohs.Add("BROADCAST ORBIT - 3", new Regex(@"\s{4}(?<TTOE>[0-9\.\-\D]{19})(?<Cic>[0-9\.\-\D]{19})(?<Omega0>[0-9\.\-\D]{19})(?<Cis>[0-9\.\-\D]{19})", RegexOptions.Compiled));
            gpsNavEpohs.Add("BROADCAST ORBIT - 4", new Regex(@"\s{4}(?<I0>[0-9\.\-\D]{19})(?<Crc>[0-9\.\-\D]{19})(?<Omega>[0-9\.\-\D]{19})(?<OmegaDot>[0-9\.\-\D]{19})", RegexOptions.Compiled));
            gpsNavEpohs.Add("BROADCAST ORBIT - 5", new Regex(@"\s{4}(?<IDot>[0-9\.\-\D]{19})(?<CodesOnL2Chanel>[0-9\.\-\D]{19})(?<GpsWeek>[0-9\.\-\D]{19})(?<L2PDataFlag>[0-9\.\-\D]{19})", RegexOptions.Compiled));
            gpsNavEpohs.Add("BROADCAST ORBIT - 6", new Regex(@"\s{4}(?<SVAccuracy>[0-9\.\-\D]{19})(?<SVHealth>[0-9\.\-\D]{19})(?<TGD>[0-9\.\-\D]{19})(?<IODC>[0-9\.\-\D]{19})", RegexOptions.Compiled));
            gpsNavEpohs.Add("BROADCAST ORBIT - 7", new Regex(@"\s{4}(?<TransmitionTimeOfMessage>[0-9\.\-\D]{19})", RegexOptions.Compiled));

            gloNavEpohs.Add("SV / EPOCH / SV CLK", new Regex(@"(?<SateliteNumber>\w{3})\s(?<Year>\d{4})\s(?<Month>\d{2})\s(?<Day>\d{2})\s+(?<Hour>\d{1,2})\s+(?<Minute>\d{1,2})\s+(?<Second>\d{1,2})(?<ClockBias>[0-9\.\-\D]{19})(?<FreqBias>[0-9\.\-\D]{19})(?<MessageFrameTime>[0-9\.\-\D]{19})", RegexOptions.Compiled));
            gloNavEpohs.Add("BROADCAST ORBIT - 1", new Regex(@"\s{4}(?<SatelitePosX>[0-9\.\-\D]{19})(?<VelocityX>[0-9\.\-\D]{19})(?<AccelerationX>[0-9\.\-\D]{19})(?<Health>[0-9\.\-\D]{19})", RegexOptions.Compiled));
            gloNavEpohs.Add("BROADCAST ORBIT - 2", new Regex(@"\s{4}(?<SatelitePosY>[0-9\.\-\D]{19})(?<VelocityY>[0-9\.\-\D]{19})(?<AccelerationY>[0-9\.\-\D]{19})(?<FrequencyNumber>[0-9\.\-\D]{19})", RegexOptions.Compiled));
            gloNavEpohs.Add("BROADCAST ORBIT - 3", new Regex(@"\s{4}(?<SatelitePosZ>[0-9\.\-\D]{19})(?<VelocityZ>[0-9\.\-\D]{19})(?<AccelerationZ>[0-9\.\-\D]{19})(?<AgeOfOperInfo>[0-9\.\-\D]{19})", RegexOptions.Compiled));

            galNavEpohs.Add("SV / EPOCH / SV CLK", new Regex(@"(?<SateliteNumber>\w{3})\s(?<Year>\d{4})\s(?<Month>\d{2})\s(?<Day>\d{2})\s+(?<Hour>\d{1,2})\s+(?<Minute>\d{1,2})\s+(?<Second>\d{1,2})(?<ClockBias>[0-9\.\-\D]{19})(?<ClockDrift>[0-9\.\-\D]{19})(?<ClockDriftRate>[0-9\.\-\D]{19})", RegexOptions.Compiled));
            galNavEpohs.Add("BROADCAST ORBIT - 1", new Regex(@"\s{4}(?<IODE>[0-9\.\-\D]{19})(?<Crs>[0-9\.\-\D]{19})(?<DeltaN>[0-9\.\-\D]{19})(?<M0>[0-9\.\-\D]{19})", RegexOptions.Compiled));
            galNavEpohs.Add("BROADCAST ORBIT - 2", new Regex(@"\s{4}(?<Cuc>[0-9\.\-\D]{19})(?<E>[0-9\.\-\D]{19})(?<Cus>[0-9\.\-\D]{19})(?<SqrtA>[0-9\.\-\D]{19})", RegexOptions.Compiled));
            galNavEpohs.Add("BROADCAST ORBIT - 3", new Regex(@"\s{4}(?<TTOE>[0-9\.\-\D]{19})(?<Cic>[0-9\.\-\D]{19})(?<Omega0>[0-9\.\-\D]{19})(?<Cis>[0-9\.\-\D]{19})", RegexOptions.Compiled));
            galNavEpohs.Add("BROADCAST ORBIT - 4", new Regex(@"\s{4}(?<I0>[0-9\.\-\D]{19})(?<Crc>[0-9\.\-\D]{19})(?<Omega>[0-9\.\-\D]{19})(?<OmegaDot>[0-9\.\-\D]{19})", RegexOptions.Compiled));
            galNavEpohs.Add("BROADCAST ORBIT - 5", new Regex(@"\s{4}(?<IDot>[0-9\.\-\D]{19})(?<DataSources>[0-9\.\-\D]{19})(?<GalWeek>[0-9\.\-\D]{19})", RegexOptions.Compiled));
            galNavEpohs.Add("BROADCAST ORBIT - 6", new Regex(@"\s{4}(?<SISA>[0-9\.\-\D]{19})(?<SVHealth>[0-9\.\-\D]{19})(?<BGDE5a_E1>[0-9\.\-\D]{19})(?<BGDE5b_E1>[0-9\.\-\D]{19})", RegexOptions.Compiled));
            galNavEpohs.Add("BROADCAST ORBIT - 7", new Regex(@"\s{4}(?<TransmitionTimeOfMessage>[0-9\.\-\D]{19})", RegexOptions.Compiled));

            bdsNavEpohs.Add("SV / EPOCH / SV CLK", new Regex(@"(?<SateliteNumber>\w{3})\s(?<Year>\d{4})\s(?<Month>\d{2})\s(?<Day>\d{2})\s+(?<Hour>\d{1,2})\s+(?<Minute>\d{1,2})\s+(?<Second>\d{1,2})(?<ClockBias>[0-9\.\-\D]{19})(?<ClockDrift>[0-9\.\-\D]{19})(?<ClockDriftRate>[0-9\.\-\D]{19})", RegexOptions.Compiled));
            bdsNavEpohs.Add("BROADCAST ORBIT - 1", new Regex(@"\s{4}(?<IODE>[0-9\.\-\D]{19})(?<Crs>[0-9\.\-\D]{19})(?<DeltaN>[0-9\.\-\D]{19})(?<M0>[0-9\.\-\D]{19})", RegexOptions.Compiled));
            bdsNavEpohs.Add("BROADCAST ORBIT - 2", new Regex(@"\s{4}(?<Cuc>[0-9\.\-\D]{19})(?<E>[0-9\.\-\D]{19})(?<Cus>[0-9\.\-\D]{19})(?<SqrtA>[0-9\.\-\D]{19})", RegexOptions.Compiled));
            bdsNavEpohs.Add("BROADCAST ORBIT - 3", new Regex(@"\s{4}(?<TTOE>[0-9\.\-\D]{19})(?<Cic>[0-9\.\-\D]{19})(?<Omega0>[0-9\.\-\D]{19})(?<Cis>[0-9\.\-\D]{19})", RegexOptions.Compiled));
            bdsNavEpohs.Add("BROADCAST ORBIT - 4", new Regex(@"\s{4}(?<I0>[0-9\.\-\D]{19})(?<Crc>[0-9\.\-\D]{19})(?<Omega>[0-9\.\-\D]{19})(?<OmegaDot>[0-9\.\-\D]{19})", RegexOptions.Compiled));
            bdsNavEpohs.Add("BROADCAST ORBIT - 5", new Regex(@"\s{4}(?<IDot>[0-9\.\-\D]{19})([0-9\.\-\D]{19})(?<BDTWeek>[0-9\.\-\D]{19})([0-9\.\-\D]{19})", RegexOptions.Compiled));
            bdsNavEpohs.Add("BROADCAST ORBIT - 6", new Regex(@"\s{4}(?<SVAccuracy>[0-9\.\-\D]{19})(?<SVHealth>[0-9\.\-\D]{19})(?<TGD1>[0-9\.\-\D]{19})(?<TGD2>[0-9\.\-\D]{19})", RegexOptions.Compiled));
            bdsNavEpohs.Add("BROADCAST ORBIT - 7", new Regex(@"\s{4}(?<TransmitionTimeOfMessage>[0-9\.\-\D]{19})(?<IODC>[0-9\.\-\D]{19})", RegexOptions.Compiled));
        }
    }
}
