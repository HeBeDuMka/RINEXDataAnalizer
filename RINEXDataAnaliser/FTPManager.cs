using System.Net;

namespace RINEXDataAnaliser
{
    /// <summary>
    /// Класс для общения с FTP сервером
    /// </summary>
    public class FTPManager
    {
        /// <summary>
        /// Строка для хранения адреса FTP сервера
        /// </summary>
        private string ftpUrl = "";

        /// <summary>
        /// Строка для хранения логина от FTP сервера
        /// </summary>
        private string login = "";

        /// <summary>
        /// Строка для хранения пароля от FTP сервера
        /// </summary>
        private string password = "";

        private string curentWorkingDirectory = "/";

        /// <summary>
        /// Конструктор для задания основных параметров FTP сервера
        /// </summary>
        /// <param name="_ftpUrl">Адрес FTP сервера</param>
        /// <param name="_login">Логин от FTP сервера</param>
        /// <param name="_password">Пароль от FTP сервера</param>
        public FTPManager(string _ftpUrl, string _login, string _password)
        {
            ftpUrl = _ftpUrl;
            login = _login;
            password = _password;
        }

        /// <summary>
        /// Метод для смены текущей рабочей директории
        /// </summary>
        /// <param name="dirToChange">Путь к файлу/папке</param>
        public void ChangeWorkingDir(string dirToChange)
        {
            if (dirToChange[0] == '/')
                curentWorkingDirectory = dirToChange;
            else
                curentWorkingDirectory += dirToChange;
        }

        /// <summary>
        /// Метод для скачивания файла с FTP сервера
        /// </summary>
        /// <param name="fileName">Имя файла для скачивания (из текущей рабочей директории)</param>
        /// <param name="localFilePath">Локальный путь к папке в которую будет сохранен файл</param>
        public string DownloadFile(string fileName, string localFilePath)
        {
            try
            {
                // Создаем объект FtpWebRequest для загрузки файла
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(ftpUrl + curentWorkingDirectory + fileName);
                request.Method = WebRequestMethods.Ftp.DownloadFile;
                request.Credentials = new NetworkCredential(login, password);
                request.UsePassive = true;
                request.UseBinary = true;
                request.KeepAlive = true;
                request.Timeout = Timeout.Infinite;
                // Получаем директорию из полного пути
                string directoryPath = Path.GetDirectoryName(Path.Combine(localFilePath, fileName));

                if (directoryPath != null)
                {
                    // Создаем директории, если их нет
                    Directory.CreateDirectory(directoryPath);
                }

                // Получаем ответ от сервера
                using (FtpWebResponse response = (FtpWebResponse)request.GetResponse())
                using (Stream responseStream = response.GetResponseStream())
                using (FileStream outputStream = new FileStream(Path.Combine(localFilePath, fileName), FileMode.Create))
                {
                    byte[] buffer = new byte[1024];
                    int bytesRead;
                    while ((bytesRead = responseStream.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        outputStream.Write(buffer, 0, bytesRead);
                    }
                }

                Console.WriteLine($"Файл скачан: {Path.Combine(localFilePath, fileName)}");
                return Path.GetFullPath(Path.Combine(localFilePath, fileName));
            }
            catch (WebException ex)
            {
                Console.WriteLine($"Ошибка при загрузке файла: {ex.Message}");
                return "";
            }
        }

        public List<string> GetObsFilesPathByDate(DateTime dateTimeStart, DateTime dateTimeEnd)
        {
            List<string> fileNames = new();

            TimeSpan deltaTimeObs = dateTimeEnd - dateTimeStart;

            for (var iterDateTime = dateTimeStart; iterDateTime < dateTimeEnd; iterDateTime += new TimeSpan(1, 0, 0))
            {
                fileNames.Add($"{iterDateTime.Year}/{iterDateTime.Month.ToString().PadLeft(2, '0')}/SU5200RUS_R_{iterDateTime.Year}{iterDateTime.DayOfYear}{iterDateTime.Hour.ToString().PadLeft(2, '0')}00_01H_01S_MO.crx.gz");
            }

            return fileNames;
        }

        public List<string> GetNavFilesPathByDate(DateTime dateTimeStart, DateTime dateTimeEnd)
        {
            List<string> fileNames = new();
            TimeSpan deltaTimeObs = dateTimeEnd - dateTimeStart;

            for (var iterDateTime = dateTimeStart; iterDateTime < dateTimeEnd; iterDateTime += new TimeSpan(1, 0, 0, 0))
            {
                fileNames.Add($"{iterDateTime.Year}/Brdc{iterDateTime.DayOfYear.ToString().PadLeft(3, '0')}0.{iterDateTime.Year - 2000}n");
                fileNames.Add($"{iterDateTime.Year}/Brdc{iterDateTime.DayOfYear.ToString().PadLeft(3, '0')}0.{iterDateTime.Year - 2000}g");
                fileNames.Add($"{iterDateTime.Year}/Brdc{iterDateTime.DayOfYear.ToString().PadLeft(3, '0')}0.{iterDateTime.Year - 2000}l");
                fileNames.Add($"{iterDateTime.Year}/Brdc{iterDateTime.DayOfYear.ToString().PadLeft(3, '0')}0.{iterDateTime.Year - 2000}f");
            }

            if (dateTimeEnd.DayOfYear != dateTimeStart.DayOfYear)
            {
                fileNames.Add($"{dateTimeEnd.Year}/Brdc{dateTimeEnd.DayOfYear.ToString().PadLeft(3, '0')}0.{dateTimeEnd.Year - 2000}n");
                fileNames.Add($"{dateTimeEnd.Year}/Brdc{dateTimeEnd.DayOfYear.ToString().PadLeft(3, '0')}0.{dateTimeEnd.Year - 2000}g");
                fileNames.Add($"{dateTimeEnd.Year}/Brdc{dateTimeEnd.DayOfYear.ToString().PadLeft(3, '0')}0.{dateTimeEnd.Year - 2000}l");
                fileNames.Add($"{dateTimeEnd.Year}/Brdc{dateTimeEnd.DayOfYear.ToString().PadLeft(3, '0')}0.{dateTimeEnd.Year - 2000}f");
            }

            return fileNames;
        }

        public List<string> DownloadFiles(List<string> filesNames)
        {
            List<string> downloadedFiles = new();
            Directory.CreateDirectory(@"E:\Projects\Visual_studio\RINEXDataAnaliser\Data/Ftp");

            foreach (string fileName in filesNames)
            {
                downloadedFiles.Add(DownloadFile(fileName, @"E:\Projects\Visual_studio\RINEXDataAnaliser\Data/Ftp"));
            }

            return downloadedFiles;
        }
    }
}
