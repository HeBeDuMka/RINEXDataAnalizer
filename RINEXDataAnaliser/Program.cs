using RINEXDataAnaliser.DataStructures;
using System.Text;
using System.Globalization;

namespace RINEXDataAnaliser
{
    public class Program
    {
        static int Main()
        {
            Console.OutputEncoding = Encoding.UTF8;
            string workingDir = @"E:\Projects\Visual_studio\RINEXDataAnaliser\Data\";
            Logger.OpenLogFile(Path.Combine(workingDir, $"{DateTime.Now:yyyy-MM-dd-HH-mm-ss}.txt"));

            //FTPManager ftpManager = new FTPManager("ftp://ftpupload.net", "b7_33706431", "logitech");
            //ftpManager.ChangeWorkingDir("/htdocs/GNSS/SU52/2023/12");
            //List<string> fileList = ftpManager.GetFileList();
            //ftpManager.DownloadFile(fileList[2], "");

            RegexManager regexManager = new RegexManager();
            RINEXObsFile obsFile = new RINEXObsFile();
            obsFile.ParseFile(workingDir + @"SU5200RUS_R_20233610000_01H_01S_MO.obs", regexManager);
            RINEXNavGPSFile gpsFile = new();
            gpsFile.ParceFile(workingDir + @"Brdc3610.23n", regexManager);
            RINEXNavGLONASSFile glonassFile = new();
            glonassFile.parceFile(workingDir + @"Brdc3600.23g", regexManager);
            glonassFile.parceFile(workingDir + @"Brdc3610.23g", regexManager);
            RINEXNavGALILEOFile galileoFile = new();
            galileoFile.ParseFile(workingDir + @"Brdc3610.23l", regexManager);
            List<CalcEpoch> satsCoords = CoordFinder.FindSateliteCoord(obsFile, gpsFile, glonassFile, galileoFile, CalcOptions.GPS);
            PlotGenerator.PlotSatsTrack(satsCoords);
            List<XYZCoordinates> pointXYZCoords = CoordFinder.findPointCoordinates(satsCoords);
            PlotGenerator.PlotXYZCoords(pointXYZCoords, "onlyGps");

            List<ELLCoordinates> pointELLCoords = new();
            foreach (XYZCoordinates coords in pointXYZCoords)
            {
                ELLCoordinates ellCoordinates = new ELLCoordinates();
                ellCoordinates.fromXYZ(coords);
                pointELLCoords.Add(ellCoordinates);
            }
            PlotGenerator.PlotEllCoords(pointELLCoords, "onlyGps");
            Logger.CloseLogFile();
            return 0;
        }
    }
}