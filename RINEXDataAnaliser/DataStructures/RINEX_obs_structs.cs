namespace RINEXDataAnaliser.DataStructures
{
    /// <summary>
    /// Класс для хранения данных о спутниках в пределах одной эпохи
    /// </summary>
    class RINEX_obs_epoch_data
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
    class RINEX_obs_satelite_data
    {
        /// <summary>
        /// Словарь хранящий псевдофазы и псевдодальности спутника
        /// </summary>
        public Dictionary<string, double> data = new Dictionary<string, double>();

        /// <summary>
        /// Словарь хранящий качество псевдофаз
        /// </summary>
        public Dictionary<string, int> phase_quality = new Dictionary<string, int>();
    }
}
