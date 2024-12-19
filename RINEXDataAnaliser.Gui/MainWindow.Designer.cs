namespace RinexDataAnaliser.Gui
{
    partial class MainWindow
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            WindowTabControl = new TabControl();
            InputDataTab = new TabPage();
            tableLayoutPanel10 = new TableLayoutPanel();
            ObsFileGroupBox = new GroupBox();
            ObsDateTimeTableLayoutPanel = new TableLayoutPanel();
            ObsFileDateEndTimePicker = new DateTimePicker();
            ObsFileDateEndLabel = new Label();
            ObsFileDateStartTimePicker = new DateTimePicker();
            ObsFileDateStartLabel = new Label();
            DownloadedFilesGroupBox = new GroupBox();
            tableLayoutPanel8 = new TableLayoutPanel();
            tableLayoutPanel9 = new TableLayoutPanel();
            DeleteNavFileButton = new Button();
            AddNavFileButton = new Button();
            DownloadedFilesListBox = new ListBox();
            ActionButtonsLableLayoutPanel = new TableLayoutPanel();
            LoadButton = new Button();
            CalculateButton = new Button();
            ExitButton = new Button();
            FtpServerSettingsTab = new TabPage();
            tableLayoutPanel2 = new TableLayoutPanel();
            UserFtpServerSettingsGroupBox = new GroupBox();
            tableLayoutPanel1 = new TableLayoutPanel();
            tableLayoutPanel16 = new TableLayoutPanel();
            UserServerDirectoryTextBox = new TextBox();
            UserServerDirectoryLabel = new Label();
            tableLayoutPanel5 = new TableLayoutPanel();
            UserServerPasswordTextBox = new TextBox();
            UserServerPasswordLabel = new Label();
            tableLayoutPanel4 = new TableLayoutPanel();
            UserServerLoginTextBox = new TextBox();
            UserServerLoginLabel = new Label();
            tableLayoutPanel3 = new TableLayoutPanel();
            UserServerAdressTextBox = new TextBox();
            UserServerAdressLabel = new Label();
            IacFtpServerSettingsGroupBox = new GroupBox();
            tableLayoutPanel6 = new TableLayoutPanel();
            tableLayoutPanel17 = new TableLayoutPanel();
            IacServerDirectoryTextBox = new TextBox();
            IacServerDirectoryLabel = new Label();
            tableLayoutPanel7 = new TableLayoutPanel();
            IacServerAdressTextBox = new TextBox();
            IacServerAdressLabel = new Label();
            SettingsTab = new TabPage();
            tableLayoutPanel11 = new TableLayoutPanel();
            CorrectionsGroupBox = new GroupBox();
            tableLayoutPanel13 = new TableLayoutPanel();
            RelativicCorrectionCheckBox = new CheckBox();
            IonosphericCorrectionCheckBox = new CheckBox();
            TroposphericCorrection = new CheckBox();
            tableLayoutPanel14 = new TableLayoutPanel();
            minSateliteAngleUpDown = new NumericUpDown();
            MinSateliteAngleLabel = new Label();
            GNSSGroupBox = new GroupBox();
            tableLayoutPanel12 = new TableLayoutPanel();
            BeidouCheckBox = new CheckBox();
            GalileoCheckBox = new CheckBox();
            GLONASSCheckBox = new CheckBox();
            GPSCheckbox = new CheckBox();
            GraphTab = new TabPage();
            GraphsSettingsGroupBox = new GroupBox();
            tableLayoutPanel15 = new TableLayoutPanel();
            PointSizeUpDown = new NumericUpDown();
            PointSizeLabel = new Label();
            LineThicknessUpDown = new NumericUpDown();
            LineThicknessLabel = new Label();
            PlotXYZCoordinatesButton = new Button();
            PlotEllCoordinatesButton = new Button();
            WindowLayoutPanel = new TableLayoutPanel();
            LogTextBox = new RichTextBox();
            WindowTabControl.SuspendLayout();
            InputDataTab.SuspendLayout();
            tableLayoutPanel10.SuspendLayout();
            ObsFileGroupBox.SuspendLayout();
            ObsDateTimeTableLayoutPanel.SuspendLayout();
            DownloadedFilesGroupBox.SuspendLayout();
            tableLayoutPanel8.SuspendLayout();
            tableLayoutPanel9.SuspendLayout();
            ActionButtonsLableLayoutPanel.SuspendLayout();
            FtpServerSettingsTab.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            UserFtpServerSettingsGroupBox.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            tableLayoutPanel16.SuspendLayout();
            tableLayoutPanel5.SuspendLayout();
            tableLayoutPanel4.SuspendLayout();
            tableLayoutPanel3.SuspendLayout();
            IacFtpServerSettingsGroupBox.SuspendLayout();
            tableLayoutPanel6.SuspendLayout();
            tableLayoutPanel17.SuspendLayout();
            tableLayoutPanel7.SuspendLayout();
            SettingsTab.SuspendLayout();
            tableLayoutPanel11.SuspendLayout();
            CorrectionsGroupBox.SuspendLayout();
            tableLayoutPanel13.SuspendLayout();
            tableLayoutPanel14.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)minSateliteAngleUpDown).BeginInit();
            GNSSGroupBox.SuspendLayout();
            tableLayoutPanel12.SuspendLayout();
            GraphTab.SuspendLayout();
            GraphsSettingsGroupBox.SuspendLayout();
            tableLayoutPanel15.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PointSizeUpDown).BeginInit();
            ((System.ComponentModel.ISupportInitialize)LineThicknessUpDown).BeginInit();
            WindowLayoutPanel.SuspendLayout();
            SuspendLayout();
            // 
            // WindowTabControl
            // 
            WindowTabControl.Controls.Add(InputDataTab);
            WindowTabControl.Controls.Add(FtpServerSettingsTab);
            WindowTabControl.Controls.Add(SettingsTab);
            WindowTabControl.Controls.Add(GraphTab);
            WindowTabControl.Dock = DockStyle.Fill;
            WindowTabControl.Location = new Point(3, 3);
            WindowTabControl.Name = "WindowTabControl";
            WindowTabControl.SelectedIndex = 0;
            WindowTabControl.Size = new Size(409, 444);
            WindowTabControl.TabIndex = 0;
            // 
            // InputDataTab
            // 
            InputDataTab.Controls.Add(tableLayoutPanel10);
            InputDataTab.Location = new Point(4, 24);
            InputDataTab.Name = "InputDataTab";
            InputDataTab.Padding = new Padding(3);
            InputDataTab.Size = new Size(401, 416);
            InputDataTab.TabIndex = 0;
            InputDataTab.Text = "Входные данные";
            InputDataTab.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel10
            // 
            tableLayoutPanel10.ColumnCount = 1;
            tableLayoutPanel10.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel10.Controls.Add(ObsFileGroupBox, 0, 0);
            tableLayoutPanel10.Controls.Add(DownloadedFilesGroupBox, 0, 1);
            tableLayoutPanel10.Controls.Add(ActionButtonsLableLayoutPanel, 0, 2);
            tableLayoutPanel10.Dock = DockStyle.Fill;
            tableLayoutPanel10.Location = new Point(3, 3);
            tableLayoutPanel10.Name = "tableLayoutPanel10";
            tableLayoutPanel10.RowCount = 3;
            tableLayoutPanel10.RowStyles.Add(new RowStyle(SizeType.Percent, 22F));
            tableLayoutPanel10.RowStyles.Add(new RowStyle(SizeType.Percent, 65F));
            tableLayoutPanel10.RowStyles.Add(new RowStyle(SizeType.Percent, 13F));
            tableLayoutPanel10.Size = new Size(395, 410);
            tableLayoutPanel10.TabIndex = 5;
            // 
            // ObsFileGroupBox
            // 
            ObsFileGroupBox.Controls.Add(ObsDateTimeTableLayoutPanel);
            ObsFileGroupBox.Dock = DockStyle.Fill;
            ObsFileGroupBox.Location = new Point(3, 3);
            ObsFileGroupBox.Name = "ObsFileGroupBox";
            ObsFileGroupBox.Size = new Size(389, 84);
            ObsFileGroupBox.TabIndex = 0;
            ObsFileGroupBox.TabStop = false;
            ObsFileGroupBox.Text = "Дата и время наблюдений";
            // 
            // ObsDateTimeTableLayoutPanel
            // 
            ObsDateTimeTableLayoutPanel.ColumnCount = 2;
            ObsDateTimeTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 61.3577042F));
            ObsDateTimeTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 38.6422958F));
            ObsDateTimeTableLayoutPanel.Controls.Add(ObsFileDateEndTimePicker, 1, 1);
            ObsDateTimeTableLayoutPanel.Controls.Add(ObsFileDateEndLabel, 0, 1);
            ObsDateTimeTableLayoutPanel.Controls.Add(ObsFileDateStartTimePicker, 1, 0);
            ObsDateTimeTableLayoutPanel.Controls.Add(ObsFileDateStartLabel, 0, 0);
            ObsDateTimeTableLayoutPanel.Dock = DockStyle.Fill;
            ObsDateTimeTableLayoutPanel.Location = new Point(3, 19);
            ObsDateTimeTableLayoutPanel.Name = "ObsDateTimeTableLayoutPanel";
            ObsDateTimeTableLayoutPanel.RowCount = 2;
            ObsDateTimeTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            ObsDateTimeTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            ObsDateTimeTableLayoutPanel.Size = new Size(383, 62);
            ObsDateTimeTableLayoutPanel.TabIndex = 0;
            // 
            // ObsFileDateEndTimePicker
            // 
            ObsFileDateEndTimePicker.CustomFormat = "dd.MM.yy HH:mm:ss'";
            ObsFileDateEndTimePicker.Dock = DockStyle.Fill;
            ObsFileDateEndTimePicker.Format = DateTimePickerFormat.Custom;
            ObsFileDateEndTimePicker.Location = new Point(238, 34);
            ObsFileDateEndTimePicker.Name = "ObsFileDateEndTimePicker";
            ObsFileDateEndTimePicker.Size = new Size(142, 23);
            ObsFileDateEndTimePicker.TabIndex = 6;
            ObsFileDateEndTimePicker.Value = new DateTime(2023, 12, 27, 0, 0, 0, 0);
            // 
            // ObsFileDateEndLabel
            // 
            ObsFileDateEndLabel.Anchor = AnchorStyles.None;
            ObsFileDateEndLabel.AutoSize = true;
            ObsFileDateEndLabel.Location = new Point(10, 39);
            ObsFileDateEndLabel.Name = "ObsFileDateEndLabel";
            ObsFileDateEndLabel.Size = new Size(215, 15);
            ObsFileDateEndLabel.TabIndex = 5;
            ObsFileDateEndLabel.Text = "Дата и время окончания наблюдений";
            // 
            // ObsFileDateStartTimePicker
            // 
            ObsFileDateStartTimePicker.CustomFormat = "dd.MM.yy HH:mm:ss'";
            ObsFileDateStartTimePicker.Dock = DockStyle.Fill;
            ObsFileDateStartTimePicker.Format = DateTimePickerFormat.Custom;
            ObsFileDateStartTimePicker.Location = new Point(238, 3);
            ObsFileDateStartTimePicker.Name = "ObsFileDateStartTimePicker";
            ObsFileDateStartTimePicker.Size = new Size(142, 23);
            ObsFileDateStartTimePicker.TabIndex = 3;
            ObsFileDateStartTimePicker.Value = new DateTime(2023, 12, 27, 0, 0, 0, 0);
            // 
            // ObsFileDateStartLabel
            // 
            ObsFileDateStartLabel.Anchor = AnchorStyles.None;
            ObsFileDateStartLabel.AutoSize = true;
            ObsFileDateStartLabel.Location = new Point(20, 8);
            ObsFileDateStartLabel.Name = "ObsFileDateStartLabel";
            ObsFileDateStartLabel.Size = new Size(194, 15);
            ObsFileDateStartLabel.TabIndex = 4;
            ObsFileDateStartLabel.Text = "Дата и время начала наблюдений";
            // 
            // DownloadedFilesGroupBox
            // 
            DownloadedFilesGroupBox.Controls.Add(tableLayoutPanel8);
            DownloadedFilesGroupBox.Location = new Point(3, 93);
            DownloadedFilesGroupBox.Name = "DownloadedFilesGroupBox";
            DownloadedFilesGroupBox.Size = new Size(389, 258);
            DownloadedFilesGroupBox.TabIndex = 1;
            DownloadedFilesGroupBox.TabStop = false;
            DownloadedFilesGroupBox.Text = "Загруженные файлы";
            // 
            // tableLayoutPanel8
            // 
            tableLayoutPanel8.ColumnCount = 1;
            tableLayoutPanel8.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel8.Controls.Add(tableLayoutPanel9, 0, 0);
            tableLayoutPanel8.Controls.Add(DownloadedFilesListBox, 0, 1);
            tableLayoutPanel8.Dock = DockStyle.Fill;
            tableLayoutPanel8.Location = new Point(3, 19);
            tableLayoutPanel8.Name = "tableLayoutPanel8";
            tableLayoutPanel8.RowCount = 2;
            tableLayoutPanel8.RowStyles.Add(new RowStyle(SizeType.Percent, 16.1016941F));
            tableLayoutPanel8.RowStyles.Add(new RowStyle(SizeType.Percent, 83.89831F));
            tableLayoutPanel8.Size = new Size(383, 236);
            tableLayoutPanel8.TabIndex = 0;
            // 
            // tableLayoutPanel9
            // 
            tableLayoutPanel9.ColumnCount = 3;
            tableLayoutPanel9.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 70.7903748F));
            tableLayoutPanel9.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 29.2096214F));
            tableLayoutPanel9.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 86F));
            tableLayoutPanel9.Controls.Add(DeleteNavFileButton, 2, 0);
            tableLayoutPanel9.Controls.Add(AddNavFileButton, 1, 0);
            tableLayoutPanel9.Dock = DockStyle.Fill;
            tableLayoutPanel9.Location = new Point(3, 3);
            tableLayoutPanel9.Name = "tableLayoutPanel9";
            tableLayoutPanel9.RowCount = 1;
            tableLayoutPanel9.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel9.Size = new Size(377, 31);
            tableLayoutPanel9.TabIndex = 0;
            // 
            // DeleteNavFileButton
            // 
            DeleteNavFileButton.Anchor = AnchorStyles.None;
            DeleteNavFileButton.Location = new Point(294, 4);
            DeleteNavFileButton.Name = "DeleteNavFileButton";
            DeleteNavFileButton.Size = new Size(79, 23);
            DeleteNavFileButton.TabIndex = 3;
            DeleteNavFileButton.Text = "Удалить";
            DeleteNavFileButton.UseVisualStyleBackColor = true;
            DeleteNavFileButton.Click += DeleteNavFileButton_Click;
            // 
            // AddNavFileButton
            // 
            AddNavFileButton.Anchor = AnchorStyles.None;
            AddNavFileButton.Location = new Point(211, 4);
            AddNavFileButton.Name = "AddNavFileButton";
            AddNavFileButton.Size = new Size(75, 23);
            AddNavFileButton.TabIndex = 2;
            AddNavFileButton.Text = "Добавить";
            AddNavFileButton.UseVisualStyleBackColor = true;
            AddNavFileButton.Click += AddNavFileButton_Click;
            // 
            // DownloadedFilesListBox
            // 
            DownloadedFilesListBox.Dock = DockStyle.Fill;
            DownloadedFilesListBox.FormattingEnabled = true;
            DownloadedFilesListBox.HorizontalScrollbar = true;
            DownloadedFilesListBox.ItemHeight = 15;
            DownloadedFilesListBox.Location = new Point(3, 40);
            DownloadedFilesListBox.Name = "DownloadedFilesListBox";
            DownloadedFilesListBox.Size = new Size(377, 193);
            DownloadedFilesListBox.TabIndex = 0;
            // 
            // ActionButtonsLableLayoutPanel
            // 
            ActionButtonsLableLayoutPanel.ColumnCount = 3;
            ActionButtonsLableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            ActionButtonsLableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            ActionButtonsLableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            ActionButtonsLableLayoutPanel.Controls.Add(LoadButton, 0, 0);
            ActionButtonsLableLayoutPanel.Controls.Add(CalculateButton, 1, 0);
            ActionButtonsLableLayoutPanel.Controls.Add(ExitButton, 2, 0);
            ActionButtonsLableLayoutPanel.Dock = DockStyle.Fill;
            ActionButtonsLableLayoutPanel.Location = new Point(3, 359);
            ActionButtonsLableLayoutPanel.Name = "ActionButtonsLableLayoutPanel";
            ActionButtonsLableLayoutPanel.RowCount = 1;
            ActionButtonsLableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            ActionButtonsLableLayoutPanel.Size = new Size(389, 48);
            ActionButtonsLableLayoutPanel.TabIndex = 2;
            // 
            // LoadButton
            // 
            LoadButton.Anchor = AnchorStyles.None;
            LoadButton.Location = new Point(3, 12);
            LoadButton.Name = "LoadButton";
            LoadButton.Size = new Size(123, 23);
            LoadButton.TabIndex = 5;
            LoadButton.Text = "Загрузить";
            LoadButton.UseVisualStyleBackColor = true;
            LoadButton.Click += LoadButton_Click;
            // 
            // CalculateButton
            // 
            CalculateButton.Anchor = AnchorStyles.None;
            CalculateButton.Location = new Point(132, 12);
            CalculateButton.Name = "CalculateButton";
            CalculateButton.Size = new Size(123, 23);
            CalculateButton.TabIndex = 4;
            CalculateButton.Text = "Рассчитать";
            CalculateButton.UseVisualStyleBackColor = true;
            CalculateButton.Click += CalculateButton_Click;
            // 
            // ExitButton
            // 
            ExitButton.Anchor = AnchorStyles.None;
            ExitButton.Location = new Point(261, 12);
            ExitButton.Name = "ExitButton";
            ExitButton.Size = new Size(125, 23);
            ExitButton.TabIndex = 3;
            ExitButton.Text = "Выход";
            ExitButton.UseVisualStyleBackColor = true;
            ExitButton.Click += ExitButton_Click;
            // 
            // FtpServerSettingsTab
            // 
            FtpServerSettingsTab.Controls.Add(tableLayoutPanel2);
            FtpServerSettingsTab.Location = new Point(4, 24);
            FtpServerSettingsTab.Name = "FtpServerSettingsTab";
            FtpServerSettingsTab.Size = new Size(401, 416);
            FtpServerSettingsTab.TabIndex = 3;
            FtpServerSettingsTab.Text = "Настройки FTP";
            FtpServerSettingsTab.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.ColumnCount = 1;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.Controls.Add(UserFtpServerSettingsGroupBox, 0, 0);
            tableLayoutPanel2.Controls.Add(IacFtpServerSettingsGroupBox, 0, 1);
            tableLayoutPanel2.Dock = DockStyle.Fill;
            tableLayoutPanel2.Location = new Point(0, 0);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 2;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.Size = new Size(401, 416);
            tableLayoutPanel2.TabIndex = 0;
            // 
            // UserFtpServerSettingsGroupBox
            // 
            UserFtpServerSettingsGroupBox.Controls.Add(tableLayoutPanel1);
            UserFtpServerSettingsGroupBox.Dock = DockStyle.Fill;
            UserFtpServerSettingsGroupBox.Location = new Point(3, 3);
            UserFtpServerSettingsGroupBox.Name = "UserFtpServerSettingsGroupBox";
            UserFtpServerSettingsGroupBox.Size = new Size(395, 202);
            UserFtpServerSettingsGroupBox.TabIndex = 0;
            UserFtpServerSettingsGroupBox.TabStop = false;
            UserFtpServerSettingsGroupBox.Text = "Пользовательский FTP-сервер";
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Controls.Add(tableLayoutPanel16, 0, 3);
            tableLayoutPanel1.Controls.Add(tableLayoutPanel5, 0, 2);
            tableLayoutPanel1.Controls.Add(tableLayoutPanel4, 0, 1);
            tableLayoutPanel1.Controls.Add(tableLayoutPanel3, 0, 0);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(3, 19);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 5;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 22F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 22F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 22F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 22F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 12F));
            tableLayoutPanel1.Size = new Size(389, 180);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // tableLayoutPanel16
            // 
            tableLayoutPanel16.ColumnCount = 2;
            tableLayoutPanel16.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 21.4099216F));
            tableLayoutPanel16.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 78.59008F));
            tableLayoutPanel16.Controls.Add(UserServerDirectoryTextBox, 1, 0);
            tableLayoutPanel16.Controls.Add(UserServerDirectoryLabel, 0, 0);
            tableLayoutPanel16.Dock = DockStyle.Fill;
            tableLayoutPanel16.Location = new Point(3, 120);
            tableLayoutPanel16.Name = "tableLayoutPanel16";
            tableLayoutPanel16.RowCount = 1;
            tableLayoutPanel16.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel16.Size = new Size(383, 33);
            tableLayoutPanel16.TabIndex = 3;
            // 
            // UserServerDirectoryTextBox
            // 
            UserServerDirectoryTextBox.Anchor = AnchorStyles.None;
            UserServerDirectoryTextBox.Location = new Point(85, 5);
            UserServerDirectoryTextBox.Name = "UserServerDirectoryTextBox";
            UserServerDirectoryTextBox.Size = new Size(294, 23);
            UserServerDirectoryTextBox.TabIndex = 3;
            UserServerDirectoryTextBox.Text = "/htdocs/GNSS/SU52/";
            // 
            // UserServerDirectoryLabel
            // 
            UserServerDirectoryLabel.Anchor = AnchorStyles.None;
            UserServerDirectoryLabel.AutoSize = true;
            UserServerDirectoryLabel.ImageAlign = ContentAlignment.BottomLeft;
            UserServerDirectoryLabel.Location = new Point(20, 9);
            UserServerDirectoryLabel.Name = "UserServerDirectoryLabel";
            UserServerDirectoryLabel.Size = new Size(41, 15);
            UserServerDirectoryLabel.TabIndex = 0;
            UserServerDirectoryLabel.Text = "Папка";
            // 
            // tableLayoutPanel5
            // 
            tableLayoutPanel5.ColumnCount = 2;
            tableLayoutPanel5.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 21.4099216F));
            tableLayoutPanel5.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 78.59008F));
            tableLayoutPanel5.Controls.Add(UserServerPasswordTextBox, 1, 0);
            tableLayoutPanel5.Controls.Add(UserServerPasswordLabel, 0, 0);
            tableLayoutPanel5.Dock = DockStyle.Fill;
            tableLayoutPanel5.Location = new Point(3, 81);
            tableLayoutPanel5.Name = "tableLayoutPanel5";
            tableLayoutPanel5.RowCount = 1;
            tableLayoutPanel5.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel5.Size = new Size(383, 33);
            tableLayoutPanel5.TabIndex = 2;
            // 
            // UserServerPasswordTextBox
            // 
            UserServerPasswordTextBox.Anchor = AnchorStyles.None;
            UserServerPasswordTextBox.Location = new Point(85, 5);
            UserServerPasswordTextBox.Name = "UserServerPasswordTextBox";
            UserServerPasswordTextBox.PasswordChar = '*';
            UserServerPasswordTextBox.Size = new Size(294, 23);
            UserServerPasswordTextBox.TabIndex = 3;
            UserServerPasswordTextBox.Text = "logitech";
            // 
            // UserServerPasswordLabel
            // 
            UserServerPasswordLabel.Anchor = AnchorStyles.None;
            UserServerPasswordLabel.AutoSize = true;
            UserServerPasswordLabel.ImageAlign = ContentAlignment.BottomLeft;
            UserServerPasswordLabel.Location = new Point(16, 9);
            UserServerPasswordLabel.Name = "UserServerPasswordLabel";
            UserServerPasswordLabel.Size = new Size(49, 15);
            UserServerPasswordLabel.TabIndex = 0;
            UserServerPasswordLabel.Text = "Пароль";
            // 
            // tableLayoutPanel4
            // 
            tableLayoutPanel4.ColumnCount = 2;
            tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 21.4099216F));
            tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 78.59008F));
            tableLayoutPanel4.Controls.Add(UserServerLoginTextBox, 1, 0);
            tableLayoutPanel4.Controls.Add(UserServerLoginLabel, 0, 0);
            tableLayoutPanel4.Dock = DockStyle.Fill;
            tableLayoutPanel4.Location = new Point(3, 42);
            tableLayoutPanel4.Name = "tableLayoutPanel4";
            tableLayoutPanel4.RowCount = 1;
            tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel4.Size = new Size(383, 33);
            tableLayoutPanel4.TabIndex = 1;
            // 
            // UserServerLoginTextBox
            // 
            UserServerLoginTextBox.Anchor = AnchorStyles.None;
            UserServerLoginTextBox.Location = new Point(85, 5);
            UserServerLoginTextBox.Name = "UserServerLoginTextBox";
            UserServerLoginTextBox.Size = new Size(294, 23);
            UserServerLoginTextBox.TabIndex = 3;
            UserServerLoginTextBox.Text = "b7_33706431";
            // 
            // UserServerLoginLabel
            // 
            UserServerLoginLabel.Anchor = AnchorStyles.None;
            UserServerLoginLabel.AutoSize = true;
            UserServerLoginLabel.ImageAlign = ContentAlignment.BottomLeft;
            UserServerLoginLabel.Location = new Point(20, 9);
            UserServerLoginLabel.Name = "UserServerLoginLabel";
            UserServerLoginLabel.Size = new Size(41, 15);
            UserServerLoginLabel.TabIndex = 0;
            UserServerLoginLabel.Text = "Логин";
            // 
            // tableLayoutPanel3
            // 
            tableLayoutPanel3.ColumnCount = 2;
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 21.4099216F));
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 78.59008F));
            tableLayoutPanel3.Controls.Add(UserServerAdressTextBox, 1, 0);
            tableLayoutPanel3.Controls.Add(UserServerAdressLabel, 0, 0);
            tableLayoutPanel3.Dock = DockStyle.Fill;
            tableLayoutPanel3.Location = new Point(3, 3);
            tableLayoutPanel3.Name = "tableLayoutPanel3";
            tableLayoutPanel3.RowCount = 1;
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel3.Size = new Size(383, 33);
            tableLayoutPanel3.TabIndex = 0;
            // 
            // UserServerAdressTextBox
            // 
            UserServerAdressTextBox.Anchor = AnchorStyles.None;
            UserServerAdressTextBox.Location = new Point(85, 5);
            UserServerAdressTextBox.Name = "UserServerAdressTextBox";
            UserServerAdressTextBox.Size = new Size(294, 23);
            UserServerAdressTextBox.TabIndex = 3;
            UserServerAdressTextBox.Text = "ftp://ftpupload.net";
            // 
            // UserServerAdressLabel
            // 
            UserServerAdressLabel.Anchor = AnchorStyles.None;
            UserServerAdressLabel.AutoSize = true;
            UserServerAdressLabel.ImageAlign = ContentAlignment.BottomLeft;
            UserServerAdressLabel.Location = new Point(21, 9);
            UserServerAdressLabel.Name = "UserServerAdressLabel";
            UserServerAdressLabel.Size = new Size(40, 15);
            UserServerAdressLabel.TabIndex = 0;
            UserServerAdressLabel.Text = "Адрес";
            // 
            // IacFtpServerSettingsGroupBox
            // 
            IacFtpServerSettingsGroupBox.Controls.Add(tableLayoutPanel6);
            IacFtpServerSettingsGroupBox.Dock = DockStyle.Fill;
            IacFtpServerSettingsGroupBox.Location = new Point(3, 211);
            IacFtpServerSettingsGroupBox.Name = "IacFtpServerSettingsGroupBox";
            IacFtpServerSettingsGroupBox.Size = new Size(395, 202);
            IacFtpServerSettingsGroupBox.TabIndex = 1;
            IacFtpServerSettingsGroupBox.TabStop = false;
            IacFtpServerSettingsGroupBox.Text = "FTP-сервер ИАК ГЛОНАСС";
            // 
            // tableLayoutPanel6
            // 
            tableLayoutPanel6.ColumnCount = 1;
            tableLayoutPanel6.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel6.Controls.Add(tableLayoutPanel17, 0, 1);
            tableLayoutPanel6.Controls.Add(tableLayoutPanel7, 0, 0);
            tableLayoutPanel6.Dock = DockStyle.Fill;
            tableLayoutPanel6.Location = new Point(3, 19);
            tableLayoutPanel6.Name = "tableLayoutPanel6";
            tableLayoutPanel6.RowCount = 3;
            tableLayoutPanel6.RowStyles.Add(new RowStyle(SizeType.Percent, 22F));
            tableLayoutPanel6.RowStyles.Add(new RowStyle(SizeType.Percent, 22F));
            tableLayoutPanel6.RowStyles.Add(new RowStyle(SizeType.Percent, 56F));
            tableLayoutPanel6.Size = new Size(389, 180);
            tableLayoutPanel6.TabIndex = 0;
            // 
            // tableLayoutPanel17
            // 
            tableLayoutPanel17.ColumnCount = 2;
            tableLayoutPanel17.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 21.4099216F));
            tableLayoutPanel17.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 78.59008F));
            tableLayoutPanel17.Controls.Add(IacServerDirectoryTextBox, 1, 0);
            tableLayoutPanel17.Controls.Add(IacServerDirectoryLabel, 0, 0);
            tableLayoutPanel17.Dock = DockStyle.Fill;
            tableLayoutPanel17.Location = new Point(3, 42);
            tableLayoutPanel17.Name = "tableLayoutPanel17";
            tableLayoutPanel17.RowCount = 1;
            tableLayoutPanel17.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel17.Size = new Size(383, 33);
            tableLayoutPanel17.TabIndex = 4;
            // 
            // IacServerDirectoryTextBox
            // 
            IacServerDirectoryTextBox.Anchor = AnchorStyles.None;
            IacServerDirectoryTextBox.Location = new Point(85, 5);
            IacServerDirectoryTextBox.Name = "IacServerDirectoryTextBox";
            IacServerDirectoryTextBox.Size = new Size(294, 23);
            IacServerDirectoryTextBox.TabIndex = 3;
            IacServerDirectoryTextBox.Text = "/MCC/BRDC/";
            // 
            // IacServerDirectoryLabel
            // 
            IacServerDirectoryLabel.Anchor = AnchorStyles.None;
            IacServerDirectoryLabel.AutoSize = true;
            IacServerDirectoryLabel.ImageAlign = ContentAlignment.BottomLeft;
            IacServerDirectoryLabel.Location = new Point(20, 9);
            IacServerDirectoryLabel.Name = "IacServerDirectoryLabel";
            IacServerDirectoryLabel.Size = new Size(41, 15);
            IacServerDirectoryLabel.TabIndex = 0;
            IacServerDirectoryLabel.Text = "Папка";
            // 
            // tableLayoutPanel7
            // 
            tableLayoutPanel7.ColumnCount = 2;
            tableLayoutPanel7.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 21.4099216F));
            tableLayoutPanel7.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 78.59008F));
            tableLayoutPanel7.Controls.Add(IacServerAdressTextBox, 1, 0);
            tableLayoutPanel7.Controls.Add(IacServerAdressLabel, 0, 0);
            tableLayoutPanel7.Dock = DockStyle.Fill;
            tableLayoutPanel7.Location = new Point(3, 3);
            tableLayoutPanel7.Name = "tableLayoutPanel7";
            tableLayoutPanel7.RowCount = 1;
            tableLayoutPanel7.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel7.Size = new Size(383, 33);
            tableLayoutPanel7.TabIndex = 1;
            // 
            // IacServerAdressTextBox
            // 
            IacServerAdressTextBox.Anchor = AnchorStyles.None;
            IacServerAdressTextBox.Location = new Point(85, 5);
            IacServerAdressTextBox.Name = "IacServerAdressTextBox";
            IacServerAdressTextBox.Size = new Size(294, 23);
            IacServerAdressTextBox.TabIndex = 3;
            IacServerAdressTextBox.Text = "ftp://ftp.glonass-iac.ru";
            // 
            // IacServerAdressLabel
            // 
            IacServerAdressLabel.Anchor = AnchorStyles.None;
            IacServerAdressLabel.AutoSize = true;
            IacServerAdressLabel.ImageAlign = ContentAlignment.BottomLeft;
            IacServerAdressLabel.Location = new Point(21, 9);
            IacServerAdressLabel.Name = "IacServerAdressLabel";
            IacServerAdressLabel.Size = new Size(40, 15);
            IacServerAdressLabel.TabIndex = 0;
            IacServerAdressLabel.Text = "Адрес";
            // 
            // SettingsTab
            // 
            SettingsTab.Controls.Add(tableLayoutPanel11);
            SettingsTab.Location = new Point(4, 24);
            SettingsTab.Name = "SettingsTab";
            SettingsTab.Padding = new Padding(3);
            SettingsTab.Size = new Size(401, 416);
            SettingsTab.TabIndex = 1;
            SettingsTab.Text = "Параметры расчета";
            SettingsTab.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel11
            // 
            tableLayoutPanel11.ColumnCount = 1;
            tableLayoutPanel11.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel11.Controls.Add(CorrectionsGroupBox, 0, 1);
            tableLayoutPanel11.Controls.Add(GNSSGroupBox, 0, 0);
            tableLayoutPanel11.Dock = DockStyle.Fill;
            tableLayoutPanel11.Location = new Point(3, 3);
            tableLayoutPanel11.Name = "tableLayoutPanel11";
            tableLayoutPanel11.RowCount = 2;
            tableLayoutPanel11.RowStyles.Add(new RowStyle(SizeType.Percent, 30F));
            tableLayoutPanel11.RowStyles.Add(new RowStyle(SizeType.Percent, 70F));
            tableLayoutPanel11.Size = new Size(395, 410);
            tableLayoutPanel11.TabIndex = 2;
            // 
            // CorrectionsGroupBox
            // 
            CorrectionsGroupBox.Controls.Add(tableLayoutPanel13);
            CorrectionsGroupBox.Dock = DockStyle.Fill;
            CorrectionsGroupBox.Location = new Point(3, 126);
            CorrectionsGroupBox.Name = "CorrectionsGroupBox";
            CorrectionsGroupBox.Size = new Size(389, 281);
            CorrectionsGroupBox.TabIndex = 1;
            CorrectionsGroupBox.TabStop = false;
            CorrectionsGroupBox.Text = "Коррекции";
            // 
            // tableLayoutPanel13
            // 
            tableLayoutPanel13.ColumnCount = 1;
            tableLayoutPanel13.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel13.Controls.Add(RelativicCorrectionCheckBox, 0, 3);
            tableLayoutPanel13.Controls.Add(IonosphericCorrectionCheckBox, 0, 2);
            tableLayoutPanel13.Controls.Add(TroposphericCorrection, 0, 1);
            tableLayoutPanel13.Controls.Add(tableLayoutPanel14, 0, 0);
            tableLayoutPanel13.Dock = DockStyle.Fill;
            tableLayoutPanel13.Location = new Point(3, 19);
            tableLayoutPanel13.Name = "tableLayoutPanel13";
            tableLayoutPanel13.RowCount = 5;
            tableLayoutPanel13.RowStyles.Add(new RowStyle(SizeType.Percent, 15F));
            tableLayoutPanel13.RowStyles.Add(new RowStyle(SizeType.Percent, 15F));
            tableLayoutPanel13.RowStyles.Add(new RowStyle(SizeType.Percent, 15F));
            tableLayoutPanel13.RowStyles.Add(new RowStyle(SizeType.Percent, 15F));
            tableLayoutPanel13.RowStyles.Add(new RowStyle(SizeType.Percent, 40F));
            tableLayoutPanel13.Size = new Size(383, 259);
            tableLayoutPanel13.TabIndex = 6;
            // 
            // RelativicCorrectionCheckBox
            // 
            RelativicCorrectionCheckBox.AutoSize = true;
            RelativicCorrectionCheckBox.Dock = DockStyle.Fill;
            RelativicCorrectionCheckBox.Location = new Point(3, 117);
            RelativicCorrectionCheckBox.Name = "RelativicCorrectionCheckBox";
            RelativicCorrectionCheckBox.Size = new Size(377, 32);
            RelativicCorrectionCheckBox.TabIndex = 3;
            RelativicCorrectionCheckBox.Text = "Релятивийская коррекция";
            RelativicCorrectionCheckBox.UseVisualStyleBackColor = true;
            // 
            // IonosphericCorrectionCheckBox
            // 
            IonosphericCorrectionCheckBox.AutoSize = true;
            IonosphericCorrectionCheckBox.Dock = DockStyle.Fill;
            IonosphericCorrectionCheckBox.Location = new Point(3, 79);
            IonosphericCorrectionCheckBox.Name = "IonosphericCorrectionCheckBox";
            IonosphericCorrectionCheckBox.Size = new Size(377, 32);
            IonosphericCorrectionCheckBox.TabIndex = 4;
            IonosphericCorrectionCheckBox.Text = "Ионосферная коррекция";
            IonosphericCorrectionCheckBox.UseVisualStyleBackColor = true;
            // 
            // TroposphericCorrection
            // 
            TroposphericCorrection.AutoSize = true;
            TroposphericCorrection.Dock = DockStyle.Fill;
            TroposphericCorrection.Location = new Point(3, 41);
            TroposphericCorrection.Name = "TroposphericCorrection";
            TroposphericCorrection.Size = new Size(377, 32);
            TroposphericCorrection.TabIndex = 5;
            TroposphericCorrection.Text = "Тропосферная коррекция";
            TroposphericCorrection.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel14
            // 
            tableLayoutPanel14.ColumnCount = 2;
            tableLayoutPanel14.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 82.49337F));
            tableLayoutPanel14.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 17.5066319F));
            tableLayoutPanel14.Controls.Add(minSateliteAngleUpDown, 1, 0);
            tableLayoutPanel14.Controls.Add(MinSateliteAngleLabel, 0, 0);
            tableLayoutPanel14.Dock = DockStyle.Fill;
            tableLayoutPanel14.Location = new Point(3, 3);
            tableLayoutPanel14.Name = "tableLayoutPanel14";
            tableLayoutPanel14.RowCount = 1;
            tableLayoutPanel14.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel14.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel14.Size = new Size(377, 32);
            tableLayoutPanel14.TabIndex = 0;
            // 
            // minSateliteAngleUpDown
            // 
            minSateliteAngleUpDown.Dock = DockStyle.Fill;
            minSateliteAngleUpDown.Location = new Point(314, 3);
            minSateliteAngleUpDown.Maximum = new decimal(new int[] { 90, 0, 0, 0 });
            minSateliteAngleUpDown.Name = "minSateliteAngleUpDown";
            minSateliteAngleUpDown.Size = new Size(60, 23);
            minSateliteAngleUpDown.TabIndex = 0;
            // 
            // MinSateliteAngleLabel
            // 
            MinSateliteAngleLabel.Anchor = AnchorStyles.Left;
            MinSateliteAngleLabel.AutoSize = true;
            MinSateliteAngleLabel.Location = new Point(3, 8);
            MinSateliteAngleLabel.Name = "MinSateliteAngleLabel";
            MinSateliteAngleLabel.Size = new Size(180, 15);
            MinSateliteAngleLabel.TabIndex = 1;
            MinSateliteAngleLabel.Text = "Минимальный угол места НКА";
            // 
            // GNSSGroupBox
            // 
            GNSSGroupBox.Controls.Add(tableLayoutPanel12);
            GNSSGroupBox.Dock = DockStyle.Fill;
            GNSSGroupBox.Location = new Point(3, 3);
            GNSSGroupBox.Name = "GNSSGroupBox";
            GNSSGroupBox.Size = new Size(389, 117);
            GNSSGroupBox.TabIndex = 0;
            GNSSGroupBox.TabStop = false;
            GNSSGroupBox.Text = "Спутниковые системы";
            // 
            // tableLayoutPanel12
            // 
            tableLayoutPanel12.ColumnCount = 3;
            tableLayoutPanel12.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel12.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel12.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel12.Controls.Add(BeidouCheckBox, 0, 1);
            tableLayoutPanel12.Controls.Add(GalileoCheckBox, 2, 0);
            tableLayoutPanel12.Controls.Add(GLONASSCheckBox, 1, 0);
            tableLayoutPanel12.Controls.Add(GPSCheckbox, 0, 0);
            tableLayoutPanel12.Dock = DockStyle.Fill;
            tableLayoutPanel12.Location = new Point(3, 19);
            tableLayoutPanel12.Name = "tableLayoutPanel12";
            tableLayoutPanel12.RowCount = 2;
            tableLayoutPanel12.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel12.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel12.Size = new Size(383, 95);
            tableLayoutPanel12.TabIndex = 5;
            // 
            // BeidouCheckBox
            // 
            BeidouCheckBox.AutoSize = true;
            BeidouCheckBox.Dock = DockStyle.Fill;
            BeidouCheckBox.Enabled = false;
            BeidouCheckBox.Location = new Point(3, 50);
            BeidouCheckBox.Name = "BeidouCheckBox";
            BeidouCheckBox.Size = new Size(121, 42);
            BeidouCheckBox.TabIndex = 4;
            BeidouCheckBox.Text = "Beidou";
            BeidouCheckBox.UseVisualStyleBackColor = true;
            // 
            // GalileoCheckBox
            // 
            GalileoCheckBox.AutoSize = true;
            GalileoCheckBox.Dock = DockStyle.Fill;
            GalileoCheckBox.Enabled = false;
            GalileoCheckBox.Location = new Point(257, 3);
            GalileoCheckBox.Name = "GalileoCheckBox";
            GalileoCheckBox.Size = new Size(123, 41);
            GalileoCheckBox.TabIndex = 3;
            GalileoCheckBox.Text = "Galileo";
            GalileoCheckBox.UseVisualStyleBackColor = true;
            // 
            // GLONASSCheckBox
            // 
            GLONASSCheckBox.AutoSize = true;
            GLONASSCheckBox.Dock = DockStyle.Fill;
            GLONASSCheckBox.Enabled = false;
            GLONASSCheckBox.Location = new Point(130, 3);
            GLONASSCheckBox.Name = "GLONASSCheckBox";
            GLONASSCheckBox.Size = new Size(121, 41);
            GLONASSCheckBox.TabIndex = 2;
            GLONASSCheckBox.Text = "ГЛОНАСС";
            GLONASSCheckBox.UseVisualStyleBackColor = true;
            // 
            // GPSCheckbox
            // 
            GPSCheckbox.AutoSize = true;
            GPSCheckbox.Dock = DockStyle.Fill;
            GPSCheckbox.Enabled = false;
            GPSCheckbox.Location = new Point(3, 3);
            GPSCheckbox.Name = "GPSCheckbox";
            GPSCheckbox.Size = new Size(121, 41);
            GPSCheckbox.TabIndex = 0;
            GPSCheckbox.Text = "GPS";
            GPSCheckbox.UseVisualStyleBackColor = true;
            // 
            // GraphTab
            // 
            GraphTab.Controls.Add(GraphsSettingsGroupBox);
            GraphTab.Controls.Add(PlotXYZCoordinatesButton);
            GraphTab.Controls.Add(PlotEllCoordinatesButton);
            GraphTab.Location = new Point(4, 24);
            GraphTab.Name = "GraphTab";
            GraphTab.Size = new Size(401, 416);
            GraphTab.TabIndex = 2;
            GraphTab.Text = "Результат";
            GraphTab.UseVisualStyleBackColor = true;
            // 
            // GraphsSettingsGroupBox
            // 
            GraphsSettingsGroupBox.Controls.Add(tableLayoutPanel15);
            GraphsSettingsGroupBox.Location = new Point(3, 3);
            GraphsSettingsGroupBox.Name = "GraphsSettingsGroupBox";
            GraphsSettingsGroupBox.Size = new Size(397, 181);
            GraphsSettingsGroupBox.TabIndex = 4;
            GraphsSettingsGroupBox.TabStop = false;
            GraphsSettingsGroupBox.Text = "Параметры графиков";
            // 
            // tableLayoutPanel15
            // 
            tableLayoutPanel15.ColumnCount = 2;
            tableLayoutPanel15.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 49.9999962F));
            tableLayoutPanel15.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50.0000076F));
            tableLayoutPanel15.Controls.Add(PointSizeUpDown, 1, 1);
            tableLayoutPanel15.Controls.Add(PointSizeLabel, 0, 1);
            tableLayoutPanel15.Controls.Add(LineThicknessUpDown, 1, 0);
            tableLayoutPanel15.Controls.Add(LineThicknessLabel, 0, 0);
            tableLayoutPanel15.Dock = DockStyle.Fill;
            tableLayoutPanel15.Location = new Point(3, 19);
            tableLayoutPanel15.Name = "tableLayoutPanel15";
            tableLayoutPanel15.RowCount = 3;
            tableLayoutPanel15.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            tableLayoutPanel15.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            tableLayoutPanel15.RowStyles.Add(new RowStyle(SizeType.Percent, 60F));
            tableLayoutPanel15.Size = new Size(391, 159);
            tableLayoutPanel15.TabIndex = 6;
            // 
            // PointSizeUpDown
            // 
            PointSizeUpDown.Location = new Point(198, 34);
            PointSizeUpDown.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
            PointSizeUpDown.Name = "PointSizeUpDown";
            PointSizeUpDown.Size = new Size(44, 23);
            PointSizeUpDown.TabIndex = 2;
            PointSizeUpDown.Value = new decimal(new int[] { 3, 0, 0, 0 });
            // 
            // PointSizeLabel
            // 
            PointSizeLabel.Anchor = AnchorStyles.None;
            PointSizeLabel.AutoSize = true;
            PointSizeLabel.Location = new Point(56, 39);
            PointSizeLabel.Name = "PointSizeLabel";
            PointSizeLabel.Size = new Size(82, 15);
            PointSizeLabel.TabIndex = 3;
            PointSizeLabel.Text = "Размер точки";
            // 
            // LineThicknessUpDown
            // 
            LineThicknessUpDown.Increment = new decimal(new int[] { 5, 0, 0, 65536 });
            LineThicknessUpDown.Location = new Point(198, 3);
            LineThicknessUpDown.Maximum = new decimal(new int[] { 5, 0, 0, 0 });
            LineThicknessUpDown.Name = "LineThicknessUpDown";
            LineThicknessUpDown.Size = new Size(44, 23);
            LineThicknessUpDown.TabIndex = 4;
            LineThicknessUpDown.Value = new decimal(new int[] { 5, 0, 0, 65536 });
            // 
            // LineThicknessLabel
            // 
            LineThicknessLabel.Anchor = AnchorStyles.None;
            LineThicknessLabel.AutoSize = true;
            LineThicknessLabel.Location = new Point(49, 8);
            LineThicknessLabel.Name = "LineThicknessLabel";
            LineThicknessLabel.Size = new Size(96, 15);
            LineThicknessLabel.TabIndex = 5;
            LineThicknessLabel.Text = "Толщина линии";
            // 
            // PlotXYZCoordinatesButton
            // 
            PlotXYZCoordinatesButton.Location = new Point(3, 219);
            PlotXYZCoordinatesButton.Name = "PlotXYZCoordinatesButton";
            PlotXYZCoordinatesButton.Size = new Size(397, 23);
            PlotXYZCoordinatesButton.TabIndex = 3;
            PlotXYZCoordinatesButton.Text = "Открыть график в геоцентрической системе координат";
            PlotXYZCoordinatesButton.UseVisualStyleBackColor = true;
            PlotXYZCoordinatesButton.Click += PlotXYZCoordinatesButton_Click;
            // 
            // PlotEllCoordinatesButton
            // 
            PlotEllCoordinatesButton.Location = new Point(3, 190);
            PlotEllCoordinatesButton.Name = "PlotEllCoordinatesButton";
            PlotEllCoordinatesButton.Size = new Size(397, 23);
            PlotEllCoordinatesButton.TabIndex = 2;
            PlotEllCoordinatesButton.Text = "Открыть график в геодезической системе координат";
            PlotEllCoordinatesButton.UseVisualStyleBackColor = true;
            PlotEllCoordinatesButton.Click += PlotEllCoordinatesButton_Click;
            // 
            // WindowLayoutPanel
            // 
            WindowLayoutPanel.ColumnCount = 2;
            WindowLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 55F));
            WindowLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 45F));
            WindowLayoutPanel.Controls.Add(WindowTabControl, 0, 0);
            WindowLayoutPanel.Controls.Add(LogTextBox, 1, 0);
            WindowLayoutPanel.Dock = DockStyle.Fill;
            WindowLayoutPanel.Location = new Point(0, 0);
            WindowLayoutPanel.Name = "WindowLayoutPanel";
            WindowLayoutPanel.RowCount = 1;
            WindowLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            WindowLayoutPanel.Size = new Size(756, 450);
            WindowLayoutPanel.TabIndex = 1;
            // 
            // LogTextBox
            // 
            LogTextBox.Anchor = AnchorStyles.None;
            LogTextBox.Location = new Point(418, 3);
            LogTextBox.Name = "LogTextBox";
            LogTextBox.ReadOnly = true;
            LogTextBox.Size = new Size(335, 444);
            LogTextBox.TabIndex = 1;
            LogTextBox.Text = "";
            // 
            // MainWindow
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(756, 450);
            Controls.Add(WindowLayoutPanel);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Name = "MainWindow";
            Text = "RINEX Analiser";
            WindowTabControl.ResumeLayout(false);
            InputDataTab.ResumeLayout(false);
            tableLayoutPanel10.ResumeLayout(false);
            ObsFileGroupBox.ResumeLayout(false);
            ObsDateTimeTableLayoutPanel.ResumeLayout(false);
            ObsDateTimeTableLayoutPanel.PerformLayout();
            DownloadedFilesGroupBox.ResumeLayout(false);
            tableLayoutPanel8.ResumeLayout(false);
            tableLayoutPanel9.ResumeLayout(false);
            ActionButtonsLableLayoutPanel.ResumeLayout(false);
            FtpServerSettingsTab.ResumeLayout(false);
            tableLayoutPanel2.ResumeLayout(false);
            UserFtpServerSettingsGroupBox.ResumeLayout(false);
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel16.ResumeLayout(false);
            tableLayoutPanel16.PerformLayout();
            tableLayoutPanel5.ResumeLayout(false);
            tableLayoutPanel5.PerformLayout();
            tableLayoutPanel4.ResumeLayout(false);
            tableLayoutPanel4.PerformLayout();
            tableLayoutPanel3.ResumeLayout(false);
            tableLayoutPanel3.PerformLayout();
            IacFtpServerSettingsGroupBox.ResumeLayout(false);
            tableLayoutPanel6.ResumeLayout(false);
            tableLayoutPanel17.ResumeLayout(false);
            tableLayoutPanel17.PerformLayout();
            tableLayoutPanel7.ResumeLayout(false);
            tableLayoutPanel7.PerformLayout();
            SettingsTab.ResumeLayout(false);
            tableLayoutPanel11.ResumeLayout(false);
            CorrectionsGroupBox.ResumeLayout(false);
            tableLayoutPanel13.ResumeLayout(false);
            tableLayoutPanel13.PerformLayout();
            tableLayoutPanel14.ResumeLayout(false);
            tableLayoutPanel14.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)minSateliteAngleUpDown).EndInit();
            GNSSGroupBox.ResumeLayout(false);
            tableLayoutPanel12.ResumeLayout(false);
            tableLayoutPanel12.PerformLayout();
            GraphTab.ResumeLayout(false);
            GraphsSettingsGroupBox.ResumeLayout(false);
            tableLayoutPanel15.ResumeLayout(false);
            tableLayoutPanel15.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)PointSizeUpDown).EndInit();
            ((System.ComponentModel.ISupportInitialize)LineThicknessUpDown).EndInit();
            WindowLayoutPanel.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TabControl WindowTabControl;
        private TabPage InputDataTab;
        private TabPage SettingsTab;
        private TabPage GraphTab;
        private GroupBox ObsFileGroupBox;
        private GroupBox DownloadedFilesGroupBox;
        private ListBox DownloadedFilesListBox;
        private Button DeleteNavFileButton;
        private Button AddNavFileButton;
        private Button CalculateButton;
        private Button ExitButton;
        private GroupBox GNSSGroupBox;
        private GroupBox CorrectionsGroupBox;
        private CheckBox BeidouCheckBox;
        private CheckBox GalileoCheckBox;
        private CheckBox GLONASSCheckBox;
        private CheckBox GPSCheckbox;
        private CheckBox TroposphericCorrection;
        private CheckBox IonosphericCorrectionCheckBox;
        private CheckBox RelativicCorrectionCheckBox;
        private Label MinSateliteAngleLabel;
        private NumericUpDown minSateliteAngleUpDown;
        private GroupBox GraphsSettingsGroupBox;
        private Button PlotXYZCoordinatesButton;
        private Button PlotEllCoordinatesButton;
        private Label LineThicknessLabel;
        private NumericUpDown LineThicknessUpDown;
        private Label PointSizeLabel;
        private NumericUpDown PointSizeUpDown;
        private TabPage FtpServerSettingsTab;
        private TableLayoutPanel WindowLayoutPanel;
        private RichTextBox LogTextBox;
        private TableLayoutPanel tableLayoutPanel2;
        private GroupBox UserFtpServerSettingsGroupBox;
        private GroupBox IacFtpServerSettingsGroupBox;
        private TableLayoutPanel tableLayoutPanel1;
        private TableLayoutPanel tableLayoutPanel3;
        private TextBox UserServerAdressTextBox;
        private Label UserServerAdressLabel;
        private TableLayoutPanel tableLayoutPanel4;
        private TextBox UserServerLoginTextBox;
        private Label UserServerLoginLabel;
        private TableLayoutPanel tableLayoutPanel5;
        private TextBox UserServerPasswordTextBox;
        private Label UserServerPasswordLabel;
        private TableLayoutPanel tableLayoutPanel6;
        private TableLayoutPanel tableLayoutPanel7;
        private TextBox IacServerAdressTextBox;
        private Label IacServerAdressLabel;
        private DateTimePicker ObsFileDateStartTimePicker;
        private TableLayoutPanel ObsDateTimeTableLayoutPanel;
        private Label ObsFileDateStartLabel;
        private DateTimePicker ObsFileDateEndTimePicker;
        private Label ObsFileDateEndLabel;
        private TableLayoutPanel tableLayoutPanel8;
        private TableLayoutPanel tableLayoutPanel9;
        private TableLayoutPanel tableLayoutPanel10;
        private TableLayoutPanel ActionButtonsLableLayoutPanel;
        private Button LoadButton;
        private TableLayoutPanel tableLayoutPanel11;
        private TableLayoutPanel tableLayoutPanel13;
        private TableLayoutPanel tableLayoutPanel14;
        private TableLayoutPanel tableLayoutPanel12;
        private TableLayoutPanel tableLayoutPanel15;
        private TableLayoutPanel tableLayoutPanel16;
        private TextBox UserServerDirectoryTextBox;
        private Label UserServerDirectoryLabel;
        private TableLayoutPanel tableLayoutPanel17;
        private TextBox IacServerDirectoryTextBox;
        private Label IacServerDirectoryLabel;
    }
}
