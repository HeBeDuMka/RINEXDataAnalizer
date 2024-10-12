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
}
