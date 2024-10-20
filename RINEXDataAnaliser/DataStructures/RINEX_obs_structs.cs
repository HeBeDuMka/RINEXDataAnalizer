using System.Text.RegularExpressions;
using System.Globalization;

namespace RINEXDataAnaliser.DataStructures
{
    /// <summary>
    /// Класс для хранения данных о спутниках в пределах одной эпохи
    /// </summary>
    public class RINEXObsEpochData
    {
        /// <summary>
        /// Время эпохи
        /// </summary>
        public DateTime epochTime;

        /// <summary>
        /// Словарь хранящий данные всех спутников в эпохе
        /// </summary>
        public Dictionary<string, RINEXObsSateliteData> satelitesData = new Dictionary<string, RINEXObsSateliteData>();
    }

    /// <summary>
    /// Класс для хранения данных одного спутника в эпохе
    /// </summary>
    public class RINEXObsSateliteData
    {
        /// <summary>
        /// Словарь хранящий псевдофазы и псевдодальности спутника
        /// </summary>
        public Dictionary<string, double> data = new Dictionary<string, double>();

        /// <summary>
        /// Словарь хранящий качество псевдофаз
        /// </summary>
        public Dictionary<string, int> phaseQuality = new Dictionary<string, int>();

        /// <summary>
        /// Конструктор класс для парсинга данных одного спутника
        /// </summary>
        /// <param name="dataToParse">Данные для парсинга</param>
        /// <param name="regularException">Регулярное выражения для парсинга</param>
        public RINEXObsSateliteData(string dataToParse, Regex regularException)
        {
            // С помощью регулярного выражения разбиваем строку
            Match match = regularException.Match(dataToParse);

            // Запускаем цикл по полученным результатам
            foreach (Group group in match.Groups)
            {
                // Первым элементом результатов будет изначальная строка, поэтому пропускаем её,
                // также пропускаем пустые значения
                if (group.Value != dataToParse && group.Value != "")
                {
                    // Если в названии параметра присутствует "quality", то это значение качества
                    // псевдофазы или псевдодальности
                    if (group.Name.Contains("quality"))
                        // Записываем в соответствующий массив
                        phaseQuality.Add(group.Name, Convert.ToInt32(group.Value));
                    // Если нет, то это данные
                    else
                        // Записываем в соответствующий массив
                        data.Add(group.Name, Convert.ToDouble(group.Value, CultureInfo.InvariantCulture));
                }
            }
        }
    }

    /// <summary>
    /// Класс для хранения данных заголовка файла наблюдений
    /// </summary>
    public class RINEXObsHeaderData
    {
        /// <summary>
        /// Строка с текущей версией RINEX файла
        /// </summary>
        public string fileVersion = "";

        /// <summary>
        /// Спутниковые системы представленные в файле
        /// </summary>
        public string sateliteSystems = "";

        /// <summary>
        /// Имя программы создавшей файл
        /// </summary>
        public string programNameCreate = "";

        /// <summary>
        /// Имя агенства создавшего файл
        /// </summary>
        public string agencyNameCreate = "";

        /// <summary>
        /// Дата и время начала измерений
        /// </summary>
        public DateTime dateTimeCreate;

        /// <summary>
        /// Временная зона в которой был создан файл
        /// </summary>
        public string timeZone = "";

        /// <summary>
        /// Все коментарии из заголовка
        /// </summary>
        public List<string> comments = new();

        /// <summary>
        /// Название маркера антенны
        /// </summary>
        public string antennaMarkerName = "";

        /// <summary>
        /// Номер маркера антенны (опционально)
        /// </summary>
        public string antennaMarkerNumber = "";

        /// <summary>
        /// Тип маркера (геодезический, негеодезический и другие)
        /// </summary>
        public string antennaMarkerType = "";

        /// <summary>
        /// Имя наблюдателя
        /// </summary>
        public string observerName = "";

        /// <summary>
        /// Имя агенства
        /// </summary>
        public string agencyName = "";

        /// <summary>
        /// Имя приемника
        /// </summary>
        public string reciverNumber = "";

        /// <summary>
        /// Тип приемника
        /// </summary>
        public string reciverType = "";

        /// <summary>
        /// Версия приемника
        /// </summary>
        public string reciverVersion = "";

        /// <summary>
        /// Номер антенны
        /// </summary>
        public string antennaNumber = "";

        /// <summary>
        /// Тип антенны
        /// </summary>
        public string antennaType = "";

        /// <summary>
        /// Положение маркера в геоцентрической системе координат, в метрах
        /// </summary>
        public XYZCoordinates approxPosition = new XYZCoordinates();

        /// <summary>
        /// Список наблюдаемых типов
        /// </summary>
        public List<RINEXObsTypes> obsTypes = new List<RINEXObsTypes>();
    }

    /// <summary>
    /// Класс для хранения наблюдаемых типов
    /// </summary>
    public class RINEXObsTypes
    {
        /// <summary>
        /// Код спутниковой группировки
        /// </summary>
        public string systemCode = "";

        /// <summary>
        /// Количество типов наблюдений
        /// </summary>
        public int typesCount = 0;

        /// <summary>
        /// Список наблюдаемых типов
        /// </summary>
        public List<string> typesDescriptors = new();
    }

    public class RINEXObsFile
    {
        /// <summary>
        /// Заголовок RINEX файла
        /// </summary>
        public RINEXObsHeaderData header = new();

        /// <summary>
        /// Данные RINEX файла
        /// </summary>
        public List<RINEXObsEpochData> epochDatas = new();

        public void ParseFile(string filePath)
        {
            // Считываем все строки из файла
            string[] fileLines = File.ReadAllLines(filePath);
            bool isHeader = true;
            RegexManager regexManager = new RegexManager();
            foreach(string line in fileLines)
            {
                if (isHeader)
                {
                    Match match;
                    if (line.EndsWith("APPROX POSITION XYZ"))
                    {
                        match = regexManager.obsHeader["APPROX POSITION XYZ"].Match(line);
                        if (match.Success){
                            header.approxPosition.X = Convert.ToDouble(match.Groups["Antenna_position_x"].Value, CultureInfo.InvariantCulture);
                            header.approxPosition.Y = Convert.ToDouble(match.Groups["Antenna_position_y"].Value, CultureInfo.InvariantCulture);
                            header.approxPosition.Z = Convert.ToDouble(match.Groups["Antenna_position_z"].Value, CultureInfo.InvariantCulture);
                        }
                    }
                    else if (line.EndsWith("SYS / # / OBS TYPES"))
                    {
                        match = regexManager.obsHeader["SYS / # / OBS TYPES"].Match(line);
                        if (match.Success)
                        {
                            RINEXObsTypes obsTypes = new();
                            obsTypes.systemCode = match.Groups["Satelite_group"].Value;
                            obsTypes.typesCount = Convert.ToInt32(match.Groups["Sat_count"].Value);
                            obsTypes.typesDescriptors = match.Groups["Obs_descriptors"].Value.Split(" ", StringSplitOptions.RemoveEmptyEntries).ToList<string>();
                            header.obsTypes.Add(obsTypes);
                        }
                    }
                    else if (line.EndsWith("END OF HEADER"))
                    {
                        isHeader = false;
                        foreach (RINEXObsTypes obsType in header.obsTypes)
                        {
                            string regexString = @"(?<Satelite_number>\w\d{2})";

                            foreach (string descriptor in obsType.typesDescriptors)
                            {
                                regexString += @"\s+(?<{}>[0-9\-]+\.\d{3})(?<{}_LLI>\d)?\s?(?<{}_SSI>\d)?".Replace("{}", descriptor);
                            }

                            regexManager.obsEpohs.Add(obsType.systemCode, new Regex(regexString, RegexOptions.Compiled));
                        }
                    }
                    //if (line.EndsWith("RINEX VERSION / TYPE"))
                    //{
                    //    match = regexManager.obsHeader["RINEX VERSION / TYPE"].Match(line);
                    //    header.fileVersion = match.Groups["Rinex_version"].Value;
                    //    header.sateliteSystems = match.Groups["Satelite_system"].Value;
                    //}
                    //else if (line.EndsWith("PGM / RUN BY / DATE"))
                    //{
                    //    match = regexManager.obsHeader["PGM / RUN BY / DATE"].Match(line);
                    //    header.programNameCreate = match.Groups["Record_program_name"].Value;
                    //    header.dateTimeCreate = DateTime.ParseExact(match.Groups["Record_date_time"].Value, "yyyyMMdd hhmmss", CultureInfo.InvariantCulture);
                    //    header.timeZone = match.Groups["Time_zone"].Value;
                    //}
                    //else if (line.EndsWith("COMMENT"))
                    //{
                    //    match = regexManager.obsHeader["COMMENT"].Match(line);
                    //    header.comments.Add(match.Groups[0].Value);
                    //}
                    //else if (line.EndsWith("MARKER NAME"))
                    //{
                    //    match = regexManager.obsHeader["MARKER_NAME"].Match(line);
                    //    header.antennaMarkerName = match.Groups["Marker_name"].Value;
                    //}
                    //else if (line.EndsWith("MARKER NUMBER"))
                    //{
                    //    match = regexManager.obsHeader["MARKER_NUMBER"].Match(line);
                    //    header.antennaMarkerNumber = match.Groups["Marker_number"].Value;
                    //}
                    //else if (line.EndsWith("MARKER TYPE"))
                    //{
                    //    match = regexManager.obsHeader["MARKER TYPE"].Match(line);
                    //    header.antennaMarkerNumber = match.Groups["Marker_type"].Value;
                    //}
                    //else if (line.EndsWith("OBSERVER / AGENCY"))
                    //{
                    //    match = regexManager.obsHeader["MARKER_NUMBER"].Match(line);
                    //    header.observerName = match.Groups["Marker_number"].Value;
                    //}
                }
                else
                {
                    if (line.StartsWith(">"))
                    {
                        Match match = regexManager.obsEpohs["Data"].Match(line);
                        RINEXObsEpochData epochData = new RINEXObsEpochData();
                    }
                }
            }
        }
    }
}
