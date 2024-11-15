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

            //FTPManager ftpManager = new FTPManager("ftp://ftpupload.net", "b7_33706431", "logitech");
            //ftpManager.ChangeWorkingDir("/htdocs/GNSS/SU52/2023/12");
            //List<string> fileList = ftpManager.GetFileList();
            //ftpManager.DownloadFile(fileList[2], "");

            RegexManager regexManager = new RegexManager();
            RINEXObsFile obsFile = new RINEXObsFile();
            obsFile.ParseFile(@"E:\Projects\Visual_studio\RINEXDataAnaliser\Data\SU5200RUS_R_20233610000_01H_01S_MO.obs", regexManager);
            RINEXNavGPSFile gpsFile = new();
            gpsFile.ParceFile(@"E:\Projects\Visual_studio\RINEXDataAnaliser\Data\Brdc3610.23n", regexManager);
            CoordFinder coordFinder = new CoordFinder();
            List<CalcEpoch> satsCoords = coordFinder.findSateliteCoord(obsFile, gpsFile);
            List<CalcEpoch> satsCoordsAlm = coordFinder.findSateliteCoordsAlm(obsFile, gpsFile);
            List<XYZCoordinates> pointXYZCoords = coordFinder.findPointCoordinates(satsCoords);
            List<XYZCoordinates> pointXYZCoordsAlm = coordFinder.findPointCoordinates(satsCoordsAlm);

            List<ELLCoordinates> pointELLCoords = new();
            List<ELLCoordinates> pointELLCoordsAlm = new();
            foreach (XYZCoordinates coords in pointXYZCoords)
            {
                ELLCoordinates ellCoordinates = new ELLCoordinates();
                ellCoordinates.fromXYZ(coords);
                pointELLCoords.Add(ellCoordinates);
                ELLCoordinates ellCoordinatesAlm = new ELLCoordinates();
                ellCoordinatesAlm.fromXYZ(coords);
                pointELLCoordsAlm.Add(ellCoordinates);
            }

            return 0;
        }
    }
}