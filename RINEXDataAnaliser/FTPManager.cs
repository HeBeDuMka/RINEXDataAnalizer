using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

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
        /// Метод для подключения к FTP серверу
        /// </summary>
        public List<string> GetFileList()
        {
            List<string> result = new List<string>();

            try
            {
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(ftpUrl + curentWorkingDirectory);
                request.Method = WebRequestMethods.Ftp.ListDirectory;
                request.Credentials = new NetworkCredential(login, password);

                // Задаем параметры соединения
                request.UsePassive = true;
                request.UseBinary = true;
                request.KeepAlive = false;

                // Получаем ответ от сервера
                using (FtpWebResponse response = (FtpWebResponse)request.GetResponse())
                {
                    // Открываем поток для чтения данных с FTP сервера
                    using (Stream responseStream = response.GetResponseStream())
                    {
                        // Читаем данные из потока
                        using (StreamReader reader = new StreamReader(responseStream))
                        {
                            string line;
                            Console.WriteLine("Список файлов и папок на FTP сервере:");
                            while ((line = reader.ReadLine()) != null)
                            {
                                result.Add(line);
                            }
                        }
                    }

                    // Выводим статус операции
                    Console.WriteLine($"Операция завершена, статус: {response.StatusDescription}");
                }

                return result;
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");

                return new List<string>();
            }
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
    }
}
