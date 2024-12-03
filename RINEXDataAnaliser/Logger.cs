using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RINEXDataAnaliser
{
    public static class Logger
    {
        public static FileStream file;
        public static void OpenLogFile(string pathToLog)
        {
            file = File.OpenWrite(pathToLog);
        }

        public static void CloseLogFile()
        {
            file.Close();
        }

        public static void WriteLineToLog(string line)
        {
            byte[] byteArray = Encoding.UTF8.GetBytes(line + '\n');
            file.Write(byteArray);
        }
    }
}
