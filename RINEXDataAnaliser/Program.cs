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
            string workingDir = @$"E:\Projects\Visual_studio\RINEXDataAnaliser\Data\{DateTime.Now:yyyy-MM-dd-HH-mm-ss}";
            Directory.CreateDirectory(workingDir);

            //FTPManager ftpManager = new FTPManager("ftp://ftpupload.net", "b7_33706431", "logitech");
            //ftpManager.ChangeWorkingDir("/htdocs/GNSS/SU52/2023/12");
            //List<string> fileList = ftpManager.GetFileList();
            //ftpManager.DownloadFile(fileList[2], "");

            RegexManager regexManager = new RegexManager();
            RINEXObsFile obsFile = new RINEXObsFile();
            obsFile.ParseFile(Path.Combine(workingDir, @"..\SU5200RUS_R_20233610000_01H_01S_MO.obs"), regexManager);
            RINEXNavGPSFile gpsFile = new();
            gpsFile.ParceFile(Path.Combine(workingDir, @"..\Brdc3610.23n"), regexManager);
            RINEXNavGLONASSFile glonassFile = new();
            glonassFile.parceFile(Path.Combine(workingDir, @"..\Brdc3600.23g"), regexManager);
            glonassFile.parceFile(Path.Combine(workingDir, @"..\Brdc3610.23g"), regexManager);
            RINEXNavGALILEOFile galileoFile = new();
            galileoFile.ParseFile(Path.Combine(workingDir, @"..\Brdc3610.23l"), regexManager);
            List<CalcEpoch> satsCoords = CoordFinder.FindSateliteCoord(obsFile, gpsFile, glonassFile, galileoFile, CalcOptions.GPS);
            //PlotGenerator.PlotSatsTrack(satsCoords, workingDir);
            List<XYZCoordinates> pointXYZCoords = CoordFinder.FindReciverCoordinates(satsCoords);

            List<ELLCoordinates> pointELLCoords = new();
            foreach (XYZCoordinates coords in pointXYZCoords)
            {
                ELLCoordinates ellCoordinates = new ELLCoordinates();
                ellCoordinates.fromXYZ(coords);
                pointELLCoords.Add(ellCoordinates);
            }
            PlotGenerator.PlotXYZCoords(pointXYZCoords, "reciverPosition", workingDir);
            PlotGenerator.PlotEllCoords(pointELLCoords, "reciverPosition", workingDir);
            return 0;
        }
    }
}