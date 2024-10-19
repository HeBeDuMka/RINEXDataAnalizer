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
}
