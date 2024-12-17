namespace RINEXDataAnaliser.Gui
{
    partial class EllCoordinatesGraph
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            EllCoordinatesPlot = new ScottPlot.WinForms.FormsPlot();
            SuspendLayout();
            // 
            // EllCoordinatesPlot
            // 
            EllCoordinatesPlot.DisplayScale = 1F;
            EllCoordinatesPlot.Dock = DockStyle.Fill;
            EllCoordinatesPlot.Location = new Point(0, 0);
            EllCoordinatesPlot.Margin = new Padding(0);
            EllCoordinatesPlot.Name = "EllCoordinatesPlot";
            EllCoordinatesPlot.Size = new Size(484, 461);
            EllCoordinatesPlot.TabIndex = 0;
            // 
            // EllCoordinatesGraph
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(484, 461);
            Controls.Add(EllCoordinatesPlot);
            Name = "EllCoordinatesGraph";
            Text = "Координаты приемника в геодезической системе координат";
            ResumeLayout(false);
        }

        #endregion

        private ScottPlot.WinForms.FormsPlot EllCoordinatesPlot;
    }
}