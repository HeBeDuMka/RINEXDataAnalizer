using System.Windows.Forms;
using RINEXDataAnaliser;
using RINEXDataAnaliser.DataStructures;

namespace RINEXDataAnaliser.Gui
{
    public partial class MainWindow : Form
    {
        RINEXNavGALILEOFile galileoNavFile = new();
        RINEXNavGLONASSFile glonassNavFile = new();
        RINEXNavGPSFile gpsNavFile = new();
        RINEXObsFile obsFile = new();
        RegexManager regexManager = new();
        List<CalcEpoch> satelitesCoordsAndPseudoranges = new();
        List<XYZCoordinates> reciverCoordinates = new();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void SelectObsFileButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "Файлы наблюдений RINEX (*.rnx)|*.rnx|Все файлы (*.*)|*.*";
            fileDialog.Title = "Выберите файл наблюдений";

            // Если пользователь выбрал файл, отображаем путь
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                SelectObsFileTextBox.Text = $"{fileDialog.FileName}";
                obsFile.ParseFile(fileDialog.FileName, regexManager);
            }
        }

        private void AddNavFileButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "Файлы эфемерид RINEX (*.*n;*.*f;*.*g;*.*l)|*.*n;*.*f;*.*g;*.*l|Все файлы (*.*)|*.*";
            fileDialog.Title = "Выберите файл наблюдений";

            // Если пользователь выбрал файл, отображаем путь
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                NavFilesListBox.Items.Add($"{fileDialog.FileName}");

                if (fileDialog.FileName.EndsWith("f"))
                {
                    BeidouCheckBox.Enabled = true;
                }
                else if (fileDialog.FileName.EndsWith("g"))
                {
                    GLONASSCheckBox.Enabled = true;
                    glonassNavFile.parceFile(fileDialog.FileName, regexManager);
                }
                else if (fileDialog.FileName.EndsWith("l"))
                {
                    GalileoCheckBox.Enabled = true;
                    galileoNavFile.ParseFile(fileDialog.FileName, regexManager);
                }
                else if (fileDialog.FileName.EndsWith("n"))
                {
                    GPSCheckbox.Enabled = true;
                    gpsNavFile.ParceFile(fileDialog.FileName, regexManager);
                }
            }
        }

        private void DeleteNavFileButton_Click(object sender, EventArgs e)
        {
            if (NavFilesListBox.SelectedItem != null)
            {
                NavFilesListBox.Items.Remove(NavFilesListBox.SelectedItem);
            }
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void CalculateButton_Click(object sender, EventArgs e)
        {
            var calculationOptions = CalcOptions.None;
            if (GPSCheckbox.Checked)
            {
                calculationOptions |= CalcOptions.GPS;
            }
            if (GalileoCheckBox.Checked)
            {
                calculationOptions |= CalcOptions.GALILEO;
            }
            if (GLONASSCheckBox.Checked)
            {
                calculationOptions |= CalcOptions.GLONASS;
            }
            if (BeidouCheckBox.Checked)
            {
                calculationOptions |= CalcOptions.BEIDOU;
            }

            satelitesCoordsAndPseudoranges = CoordFinder.FindSateliteCoord(obsFile, gpsNavFile, glonassNavFile, galileoNavFile, calculationOptions);
            reciverCoordinates = CoordFinder.FindReciverCoordinates(satelitesCoordsAndPseudoranges, true, Convert.ToDouble(minSateliteAngleUpDown.Value));
        }
    }
}
