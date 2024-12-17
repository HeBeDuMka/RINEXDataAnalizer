using RINEXDataAnaliser.DataStructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RINEXDataAnaliser.Tests
{
    public class FTPManagerTests
    {
        [Fact]
        public void GetFilesListNoError()
        {
            List<string> data = new List<string>();
            data.Add(".");
            data.Add("..");
            data.Add("htdocs");
            data.Add(".override");
            data.Add("DO NOT UPLOAD FILES HERE");
            data.Sort();

            FTPManager ftpManager = new FTPManager("ftp://ftpupload.net", "b7_33706431", "logitech");

            List<string> fileList = ftpManager.GetFileList();
            fileList.Sort();

            Assert.Equal(data, fileList);
        }

        [Fact]
        public void GetFilesListError()
        {
            List<string> data = new List<string>();

            FTPManager ftpManager = new FTPManager("ftp://ftpupload", "b7_33706431", "logitech");

            List<string> fileList = ftpManager.GetFileList();

            Assert.Equal(data, fileList);
        }

        [Fact]
        public void GetObsFilesListFromDate()
        {
            DateTime startDateTime = new DateTime(2023, 12, 31, 0, 0, 0);
            DateTime endDateTime = new DateTime(2023, 12, 31, 4, 0, 0);
            FTPManager ftpManager = new("ftp://ftpupload", "b7_33706431", "logitech");

            List<string> result = new List<string>
            {
            "SU5200RUS_R_20233650000_01H_01S_MO.crx.gz",
            "SU5200RUS_R_20233650100_01H_01S_MO.crx.gz",
            "SU5200RUS_R_20233650200_01H_01S_MO.crx.gz",
            "SU5200RUS_R_20233650300_01H_01S_MO.crx.gz"
            };

            Assert.Equal(result, ftpManager.GetObsFilePathByDate(startDateTime, endDateTime));
        }

        [Fact]
        public void GetNavFilesListFromDate1()
        {
            DateTime startDateTime = new DateTime(2023, 12, 31, 0, 0, 0);
            DateTime endDateTime = new DateTime(2023, 12, 31, 4, 0, 0);
            FTPManager ftpManager = new("ftp://ftpupload", "b7_33706431", "logitech");

            List<string> result = new List<string>
            {
            "Brdc3650.23n",
            "Brdc3650.23g",
            "Brdc3650.23l",
            "Brdc3650.23f"
            };

            Assert.Equal(result, ftpManager.GetNavFilesPathByDate(startDateTime, endDateTime));
        }

        [Fact]
        public void GetNavFilesListFromDate2()
        {
            DateTime startDateTime = new DateTime(2023, 12, 31, 23, 0, 0);
            DateTime endDateTime = new DateTime(2024, 1, 1, 3, 0, 0);
            FTPManager ftpManager = new("ftp://ftpupload", "b7_33706431", "logitech");

            List<string> result = new List<string>
            {
            "Brdc3650.23n",
            "Brdc3650.23g",
            "Brdc3650.23l",
            "Brdc3650.23f",
            "Brdc0010.24n",
            "Brdc0010.24g",
            "Brdc0010.24l",
            "Brdc0010.24f"
            };

            Assert.Equal(result, ftpManager.GetNavFilesPathByDate(startDateTime, endDateTime));
        }

        /*
        [Fact]
        public void ChangeWorkingDirNoErrorRelativePath()
        {
            string data = "/test";

            FTPManager ftpManager = new FTPManager("ftp://ftpupload", "b7_33706431", "logitech");

            ftpManager.ChangeWorkingDir("/test");

            Assert.Equal(data, ftpManager.curentWorkingDirectory);
        }

        [Fact]
        public void ChangeWorkingDirNoErrorAbsolutePath()
        {
            string data = "/test/dir";

            FTPManager ftpManager = new FTPManager("ftp://ftpupload", "b7_33706431", "logitech");

            ftpManager.ChangeWorkingDir("/test/dir");

            Assert.Equal(data, ftpManager.curentWorkingDirectory);
        }
        */
    }
}
