using System.Text.RegularExpressions;
using System.Globalization;

namespace RINEXDataAnaliser.DataStructures
{
    /// <summary>
    /// Класс для хранения данных о спутниках в пределах одной эпохи
    /// </summary>
    public class RINEX_obs_epoch_data
    {
        /// <summary>
        /// Время эпохи
        /// </summary>
        public DateTime epoch_time;

        /// <summary>
        /// Словарь хранящий данные всех спутников в эпохе
        /// </summary>
        public Dictionary<string, RINEX_obs_satelite_data> satelites_data = new Dictionary<string, RINEX_obs_satelite_data>();
    }

    /// <summary>
    /// Класс для хранения данных одного спутника в эпохе
    /// </summary>
    public class RINEX_obs_satelite_data
    {
        /// <summary>
        /// Словарь хранящий псевдофазы и псевдодальности спутника
        /// </summary>
        public Dictionary<string, double> data = new Dictionary<string, double>();

        /// <summary>
        /// Словарь хранящий качество псевдофаз
        /// </summary>
        public Dictionary<string, int> phase_quality = new Dictionary<string, int>();

        /// <summary>
        /// Конструктор класс для парсинга данных одного спутника
        /// </summary>
        /// <param name="dataToParse">Данные для парсинга</param>
        /// <param name="regularException">Регулярное выражения для парсинга</param>
        public RINEX_obs_satelite_data(string dataToParse, Regex regularException)
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
                        phase_quality.Add(group.Name, Convert.ToInt32(group.Value));
                    // Если нет, то это данные
                    else
                        // Записываем в соответствующий массив
                        data.Add(group.Name, Convert.ToDouble(group.Value, CultureInfo.InvariantCulture));
                }
            }
        }
    }
}
