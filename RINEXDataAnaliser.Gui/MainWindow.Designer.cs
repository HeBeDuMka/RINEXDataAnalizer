namespace RINEXDataAnaliser.Gui
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
            tabControl1 = new TabControl();
            InputDataTab = new TabPage();
            CalculateButton = new Button();
            ExitButton = new Button();
            groupBox2 = new GroupBox();
            DeleteNavFileButton = new Button();
            AddNavFileButton = new Button();
            NavFilesListBox = new ListBox();
            groupBox1 = new GroupBox();
            SelectObsFileButton = new Button();
            SelectObsFileTextBox = new TextBox();
            SettingsTab = new TabPage();
            CorrectionsGroupBox = new GroupBox();
            TroposphericCorrection = new CheckBox();
            IonosphericCorrectionCheckBox = new CheckBox();
            RelativicCorrectionCheckBox = new CheckBox();
            label1 = new Label();
            minSateliteAngleUpDown = new NumericUpDown();
            GNSSGroupBox = new GroupBox();
            BeidouCheckBox = new CheckBox();
            GalileoCheckBox = new CheckBox();
            GLONASSCheckBox = new CheckBox();
            GPSCheckbox = new CheckBox();
            GraphTab = new TabPage();
            tabControl1.SuspendLayout();
            InputDataTab.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox1.SuspendLayout();
            SettingsTab.SuspendLayout();
            CorrectionsGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)minSateliteAngleUpDown).BeginInit();
            GNSSGroupBox.SuspendLayout();
            SuspendLayout();
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(InputDataTab);
            tabControl1.Controls.Add(SettingsTab);
            tabControl1.Controls.Add(GraphTab);
            tabControl1.Location = new Point(12, 12);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(411, 426);
            tabControl1.TabIndex = 0;
            // 
            // InputDataTab
            // 
            InputDataTab.Controls.Add(CalculateButton);
            InputDataTab.Controls.Add(ExitButton);
            InputDataTab.Controls.Add(groupBox2);
            InputDataTab.Controls.Add(groupBox1);
            InputDataTab.Location = new Point(4, 24);
            InputDataTab.Name = "InputDataTab";
            InputDataTab.Padding = new Padding(3);
            InputDataTab.Size = new Size(403, 398);
            InputDataTab.TabIndex = 0;
            InputDataTab.Text = "Входные данные";
            InputDataTab.UseVisualStyleBackColor = true;
            // 
            // CalculateButton
            // 
            CalculateButton.Location = new Point(12, 363);
            CalculateButton.Name = "CalculateButton";
            CalculateButton.Size = new Size(156, 23);
            CalculateButton.TabIndex = 4;
            CalculateButton.Text = "Рассчитать";
            CalculateButton.UseVisualStyleBackColor = true;
            CalculateButton.Click += CalculateButton_Click;
            // 
            // ExitButton
            // 
            ExitButton.Location = new Point(231, 363);
            ExitButton.Name = "ExitButton";
            ExitButton.Size = new Size(156, 23);
            ExitButton.TabIndex = 3;
            ExitButton.Text = "Выход";
            ExitButton.UseVisualStyleBackColor = true;
            ExitButton.Click += ExitButton_Click;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(DeleteNavFileButton);
            groupBox2.Controls.Add(AddNavFileButton);
            groupBox2.Controls.Add(NavFilesListBox);
            groupBox2.Location = new Point(6, 60);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(390, 294);
            groupBox2.TabIndex = 1;
            groupBox2.TabStop = false;
            groupBox2.Text = "Файлы эфемерид";
            // 
            // DeleteNavFileButton
            // 
            DeleteNavFileButton.Location = new Point(306, 23);
            DeleteNavFileButton.Name = "DeleteNavFileButton";
            DeleteNavFileButton.Size = new Size(75, 23);
            DeleteNavFileButton.TabIndex = 3;
            DeleteNavFileButton.Text = "Удалить";
            DeleteNavFileButton.UseVisualStyleBackColor = true;
            DeleteNavFileButton.Click += DeleteNavFileButton_Click;
            // 
            // AddNavFileButton
            // 
            AddNavFileButton.Location = new Point(225, 23);
            AddNavFileButton.Name = "AddNavFileButton";
            AddNavFileButton.Size = new Size(75, 23);
            AddNavFileButton.TabIndex = 2;
            AddNavFileButton.Text = "Добавить";
            AddNavFileButton.UseVisualStyleBackColor = true;
            AddNavFileButton.Click += AddNavFileButton_Click;
            // 
            // NavFilesListBox
            // 
            NavFilesListBox.FormattingEnabled = true;
            NavFilesListBox.ItemHeight = 15;
            NavFilesListBox.Location = new Point(6, 52);
            NavFilesListBox.Name = "NavFilesListBox";
            NavFilesListBox.Size = new Size(375, 229);
            NavFilesListBox.TabIndex = 0;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(SelectObsFileButton);
            groupBox1.Controls.Add(SelectObsFileTextBox);
            groupBox1.Location = new Point(6, 6);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(390, 48);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "Файл наблюдений";
            // 
            // SelectObsFileButton
            // 
            SelectObsFileButton.Location = new Point(306, 19);
            SelectObsFileButton.Name = "SelectObsFileButton";
            SelectObsFileButton.Size = new Size(75, 23);
            SelectObsFileButton.TabIndex = 1;
            SelectObsFileButton.Text = "Выбрать";
            SelectObsFileButton.UseVisualStyleBackColor = true;
            SelectObsFileButton.Click += SelectObsFileButton_Click;
            // 
            // SelectObsFileTextBox
            // 
            SelectObsFileTextBox.Location = new Point(6, 19);
            SelectObsFileTextBox.Name = "SelectObsFileTextBox";
            SelectObsFileTextBox.Size = new Size(294, 23);
            SelectObsFileTextBox.TabIndex = 0;
            // 
            // SettingsTab
            // 
            SettingsTab.Controls.Add(CorrectionsGroupBox);
            SettingsTab.Controls.Add(GNSSGroupBox);
            SettingsTab.Location = new Point(4, 24);
            SettingsTab.Name = "SettingsTab";
            SettingsTab.Padding = new Padding(3);
            SettingsTab.Size = new Size(403, 398);
            SettingsTab.TabIndex = 1;
            SettingsTab.Text = "Параметры расчета";
            SettingsTab.UseVisualStyleBackColor = true;
            // 
            // CorrectionsGroupBox
            // 
            CorrectionsGroupBox.Controls.Add(TroposphericCorrection);
            CorrectionsGroupBox.Controls.Add(IonosphericCorrectionCheckBox);
            CorrectionsGroupBox.Controls.Add(RelativicCorrectionCheckBox);
            CorrectionsGroupBox.Controls.Add(label1);
            CorrectionsGroupBox.Controls.Add(minSateliteAngleUpDown);
            CorrectionsGroupBox.Location = new Point(6, 119);
            CorrectionsGroupBox.Name = "CorrectionsGroupBox";
            CorrectionsGroupBox.Size = new Size(391, 165);
            CorrectionsGroupBox.TabIndex = 1;
            CorrectionsGroupBox.TabStop = false;
            CorrectionsGroupBox.Text = "Коррекции";
            // 
            // TroposphericCorrection
            // 
            TroposphericCorrection.AutoSize = true;
            TroposphericCorrection.Location = new Point(19, 128);
            TroposphericCorrection.Name = "TroposphericCorrection";
            TroposphericCorrection.Size = new Size(169, 19);
            TroposphericCorrection.TabIndex = 5;
            TroposphericCorrection.Text = "Тропосферная коррекция";
            TroposphericCorrection.UseVisualStyleBackColor = true;
            // 
            // IonosphericCorrectionCheckBox
            // 
            IonosphericCorrectionCheckBox.AutoSize = true;
            IonosphericCorrectionCheckBox.Location = new Point(19, 95);
            IonosphericCorrectionCheckBox.Name = "IonosphericCorrectionCheckBox";
            IonosphericCorrectionCheckBox.Size = new Size(165, 19);
            IonosphericCorrectionCheckBox.TabIndex = 4;
            IonosphericCorrectionCheckBox.Text = "Ионосферная коррекция";
            IonosphericCorrectionCheckBox.UseVisualStyleBackColor = true;
            // 
            // RelativicCorrectionCheckBox
            // 
            RelativicCorrectionCheckBox.AutoSize = true;
            RelativicCorrectionCheckBox.Location = new Point(19, 62);
            RelativicCorrectionCheckBox.Name = "RelativicCorrectionCheckBox";
            RelativicCorrectionCheckBox.Size = new Size(170, 19);
            RelativicCorrectionCheckBox.TabIndex = 3;
            RelativicCorrectionCheckBox.Text = "Релятивийская коррекция";
            RelativicCorrectionCheckBox.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(19, 31);
            label1.Name = "label1";
            label1.Size = new Size(180, 15);
            label1.TabIndex = 1;
            label1.Text = "Минимальный угол места НКА";
            // 
            // minSateliteAngleUpDown
            // 
            minSateliteAngleUpDown.Location = new Point(218, 27);
            minSateliteAngleUpDown.Maximum = new decimal(new int[] { 90, 0, 0, 0 });
            minSateliteAngleUpDown.Name = "minSateliteAngleUpDown";
            minSateliteAngleUpDown.Size = new Size(44, 23);
            minSateliteAngleUpDown.TabIndex = 0;
            // 
            // GNSSGroupBox
            // 
            GNSSGroupBox.Controls.Add(BeidouCheckBox);
            GNSSGroupBox.Controls.Add(GalileoCheckBox);
            GNSSGroupBox.Controls.Add(GLONASSCheckBox);
            GNSSGroupBox.Controls.Add(GPSCheckbox);
            GNSSGroupBox.Location = new Point(6, 6);
            GNSSGroupBox.Name = "GNSSGroupBox";
            GNSSGroupBox.Size = new Size(391, 107);
            GNSSGroupBox.TabIndex = 0;
            GNSSGroupBox.TabStop = false;
            GNSSGroupBox.Text = "Спутниковые системы";
            // 
            // BeidouCheckBox
            // 
            BeidouCheckBox.AutoSize = true;
            BeidouCheckBox.Enabled = false;
            BeidouCheckBox.Location = new Point(19, 73);
            BeidouCheckBox.Name = "BeidouCheckBox";
            BeidouCheckBox.Size = new Size(63, 19);
            BeidouCheckBox.TabIndex = 4;
            BeidouCheckBox.Text = "Beidou";
            BeidouCheckBox.UseVisualStyleBackColor = true;
            // 
            // GalileoCheckBox
            // 
            GalileoCheckBox.AutoSize = true;
            GalileoCheckBox.Enabled = false;
            GalileoCheckBox.Location = new Point(269, 31);
            GalileoCheckBox.Name = "GalileoCheckBox";
            GalileoCheckBox.Size = new Size(62, 19);
            GalileoCheckBox.TabIndex = 3;
            GalileoCheckBox.Text = "Galileo";
            GalileoCheckBox.UseVisualStyleBackColor = true;
            // 
            // GLONASSCheckBox
            // 
            GLONASSCheckBox.AutoSize = true;
            GLONASSCheckBox.Enabled = false;
            GLONASSCheckBox.Location = new Point(145, 31);
            GLONASSCheckBox.Name = "GLONASSCheckBox";
            GLONASSCheckBox.Size = new Size(82, 19);
            GLONASSCheckBox.TabIndex = 2;
            GLONASSCheckBox.Text = "ГЛОНАСС";
            GLONASSCheckBox.UseVisualStyleBackColor = true;
            // 
            // GPSCheckbox
            // 
            GPSCheckbox.AutoSize = true;
            GPSCheckbox.Enabled = false;
            GPSCheckbox.Location = new Point(19, 31);
            GPSCheckbox.Name = "GPSCheckbox";
            GPSCheckbox.Size = new Size(47, 19);
            GPSCheckbox.TabIndex = 0;
            GPSCheckbox.Text = "GPS";
            GPSCheckbox.UseVisualStyleBackColor = true;
            // 
            // GraphTab
            // 
            GraphTab.Location = new Point(4, 24);
            GraphTab.Name = "GraphTab";
            GraphTab.Size = new Size(403, 398);
            GraphTab.TabIndex = 2;
            GraphTab.Text = "Результат";
            GraphTab.UseVisualStyleBackColor = true;
            // 
            // MainWindow
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(435, 450);
            Controls.Add(tabControl1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Name = "MainWindow";
            Text = "Form1";
            Load += Form1_Load;
            tabControl1.ResumeLayout(false);
            InputDataTab.ResumeLayout(false);
            groupBox2.ResumeLayout(false);
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            SettingsTab.ResumeLayout(false);
            CorrectionsGroupBox.ResumeLayout(false);
            CorrectionsGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)minSateliteAngleUpDown).EndInit();
            GNSSGroupBox.ResumeLayout(false);
            GNSSGroupBox.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TabControl tabControl1;
        private TabPage InputDataTab;
        private TabPage SettingsTab;
        private TabPage GraphTab;
        private GroupBox groupBox1;
        private TextBox SelectObsFileTextBox;
        private Button SelectObsFileButton;
        private GroupBox groupBox2;
        private ListBox NavFilesListBox;
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
        private Label label1;
        private NumericUpDown minSateliteAngleUpDown;
    }
}
