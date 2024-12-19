using System.Windows.Forms;
using RinexDataAnaliser;
using RinexDataAnaliser.DataStructures;
using System.Diagnostics;

namespace RinexDataAnaliser.Gui
{
    public partial class MainWindow : Form
    {
        RINEXNavGALILEOFile galileoNavFile = new();
        RINEXNavGLONASSFile glonassNavFile = new();
        RINEXNavGPSFile gpsNavFile = new();
        RINEXNavBeidouFile beidouFile = new();
        RinexObsFile obsFile = new();
        RegexManager regexManager = new();
        List<CalcEpoch> satelitesCoordsAndPseudoranges = new();
        List<XYZCoordinates> reciverCoordinates = new();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void AddNavFileButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "����� �������� RINEX (*.*n;*.*f;*.*g;*.*l)|*.*n;*.*f;*.*g;*.*l|��� ����� (*.*)|*.*";
            fileDialog.Title = "�������� ���� ����������";

            // ���� ������������ ������ ����, ���������� ����
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                DownloadedFilesListBox.Items.Add($"{fileDialog.FileName}");

                if (fileDialog.FileName.EndsWith("f"))
                {
                    BeidouCheckBox.Enabled = true;
                    BeidouCheckBox.Checked = true;
                    beidouFile.ParceFile(fileDialog.FileName, regexManager);
                }
                else if (fileDialog.FileName.EndsWith("g"))
                {
                    GLONASSCheckBox.Enabled = true;
                    GLONASSCheckBox.Checked = true;
                    glonassNavFile.parceFile(fileDialog.FileName, regexManager);
                }
                else if (fileDialog.FileName.EndsWith("l"))
                {
                    GalileoCheckBox.Enabled = true;
                    GalileoCheckBox.Checked = true;
                    galileoNavFile.ParseFile(fileDialog.FileName, regexManager);
                }
                else if (fileDialog.FileName.EndsWith("n"))
                {
                    GPSCheckbox.Enabled = true;
                    GPSCheckbox.Checked = true;
                    gpsNavFile.ParceFile(fileDialog.FileName, regexManager);
                }
            }
        }

        private void DeleteNavFileButton_Click(object sender, EventArgs e)
        {
            if (DownloadedFilesListBox.SelectedItem != null)
            {
                DownloadedFilesListBox.Items.Remove(DownloadedFilesListBox.SelectedItem);
            }
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void CalculateButton_Click(object sender, EventArgs e)
        {
            var calculationOptions = GNSSSystem.None;
            if (GPSCheckbox.Checked)
            {
                calculationOptions |= GNSSSystem.GPS;
            }
            if (GalileoCheckBox.Checked)
            {
                calculationOptions |= GNSSSystem.GALILEO;
            }
            if (GLONASSCheckBox.Checked)
            {
                calculationOptions |= GNSSSystem.GLONASS;
            }
            if (BeidouCheckBox.Checked)
            {
                calculationOptions |= GNSSSystem.BEIDOU;
            }

            satelitesCoordsAndPseudoranges = CoordFinder.FindSateliteCoord(obsFile, gpsNavFile, glonassNavFile, galileoNavFile, beidouFile, calculationOptions,
                RelativicCorrectionCheckBox.Checked, IonosphericCorrectionCheckBox.Checked, TroposphericCorrection.Checked);
            reciverCoordinates = CoordFinder.FindReciverCoordinates(satelitesCoordsAndPseudoranges, true, Convert.ToDouble(minSateliteAngleUpDown.Value));
        }

        private void PlotEllCoordinatesButton_Click(object sender, EventArgs e)
        {
            EllCoordinatesGraph ellCoordinatesGraph = new();
            ellCoordinatesGraph.plotGraph(reciverCoordinates, Convert.ToInt32(PointSizeUpDown.Value), Convert.ToSingle(LineThicknessUpDown.Value));
            ellCoordinatesGraph.Show();
        }

        private void PlotXYZCoordinatesButton_Click(object sender, EventArgs e)
        {
            XYZCoordinatesGraph xyzCoordinatesGraph = new();
            xyzCoordinatesGraph.plotGraph(reciverCoordinates, Convert.ToInt32(PointSizeUpDown.Value), Convert.ToSingle(LineThicknessUpDown.Value), obsFile.header.approxPosition);
            xyzCoordinatesGraph.Show();
        }

        private void LoadButton_Click(object sender, EventArgs e)
        {
            FTPManager ftpObsManager = new(UserServerAdressTextBox.Text, UserServerLoginTextBox.Text,
                UserServerPasswordTextBox.Text);
            ftpObsManager.ChangeWorkingDir(UserServerDirectoryTextBox.Text);
            FTPManager ftpNavManager = new(IacServerAdressTextBox.Text, "", "");
            ftpNavManager.ChangeWorkingDir(IacServerDirectoryTextBox.Text);

            List<string> navFilesNames = ftpNavManager.GetNavFilesPathByDate(ObsFileDateStartTimePicker.Value,
                ObsFileDateEndTimePicker.Value);
            List<string> obsFilesNames = ftpObsManager.GetObsFilesPathByDate(ObsFileDateStartTimePicker.Value,
                ObsFileDateEndTimePicker.Value);

            List<string> navDownloadedFilesNames = ftpNavManager.DownloadFiles(navFilesNames);
            List<string> obsDownloadedFilesNames = ftpObsManager.DownloadFiles(obsFilesNames);
            List<string> obsUnzipedFilesNames = new();

            LogTextBox.AppendText($"��������� ����� ����������\n");
            foreach (var fileName in obsDownloadedFilesNames)
            {
                LogTextBox.AppendText($"{fileName}\n");
            }
            LogTextBox.AppendText("\n");

            LogTextBox.AppendText($"����� ���������� ���������������\n");
            foreach (var fileName in obsDownloadedFilesNames)
            {
                string tempFileName = Path.GetFileNameWithoutExtension(fileName);
                string tempDirectoryName = Path.GetDirectoryName(fileName);
                obsUnzipedFilesNames.Add(Path.Combine(tempDirectoryName, Path.GetFileNameWithoutExtension(tempFileName) + ".rnx"));
                LogTextBox.AppendText($"{obsUnzipedFilesNames.Last()}\n");
                DownloadedFilesListBox.Items.Add(obsUnzipedFilesNames.Last());

                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    FileName = @".\CRZ2RNX.BAT", // ���� � ���������
                    Arguments = fileName, // ��������� ��������� ������
                    UseShellExecute = false, // �� ������������ �������� Windows
                    RedirectStandardOutput = true, // ������������� ����������� ����� (�����������)
                    RedirectStandardError = true,  // ������������� ������ (�����������)
                    CreateNoWindow = true         // �� ��������� ���� �������
                };

                try
                {
                    // ������ ��������
                    using (Process process = Process.Start(startInfo))
                    {
                        // ��������� ����������� ����� (���� �����)
                        string output = process.StandardOutput.ReadToEnd();
                        string error = process.StandardError.ReadToEnd();

                        // ���� ���������� ��������
                        process.WaitForExit();

                        Console.WriteLine("��������� ���������.");
                        Console.WriteLine($"����� ���������: {output}");
                        if (!string.IsNullOrEmpty(error))
                        {
                            Console.WriteLine($"������: {error}");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"������ ��� ������� ���������: {ex.Message}");
                }
            }
            LogTextBox.AppendText("\n");

            LogTextBox.AppendText($"��������� ����� ��������\n");
            foreach (var fileName in navDownloadedFilesNames)
            {
                LogTextBox.AppendText($"{fileName}\n");
                DownloadedFilesListBox.Items.Add(fileName);
            }
            LogTextBox.AppendText("\n");
        }
    }
}
