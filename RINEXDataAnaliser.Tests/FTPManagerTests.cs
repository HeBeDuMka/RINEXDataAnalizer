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
    }
}
