namespace RINEXDataAnaliser.Gui
{
    partial class XYZCoordinatesGraph
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
            ReciverYCoordinate = new ScottPlot.WinForms.FormsPlot();
            ReciverZCoordinate = new ScottPlot.WinForms.FormsPlot();
            ReciverXCoordinate = new ScottPlot.WinForms.FormsPlot();
            SuspendLayout();
            // 
            // ReciverYCoordinate
            // 
            ReciverYCoordinate.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            ReciverYCoordinate.DisplayScale = 1F;
            ReciverYCoordinate.Location = new Point(0, 0);
            ReciverYCoordinate.Name = "ReciverYCoordinate";
            ReciverYCoordinate.Size = new Size(784, 177);
            ReciverYCoordinate.TabIndex = 1;
            // 
            // ReciverZCoordinate
            // 
            ReciverZCoordinate.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            ReciverZCoordinate.DisplayScale = 1F;
            ReciverZCoordinate.Location = new Point(0, 177);
            ReciverZCoordinate.Name = "ReciverZCoordinate";
            ReciverZCoordinate.Size = new Size(784, 177);
            ReciverZCoordinate.TabIndex = 2;
            // 
            // ReciverXCoordinate
            // 
            ReciverXCoordinate.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            ReciverXCoordinate.DisplayScale = 1F;
            ReciverXCoordinate.Location = new Point(0, 354);
            ReciverXCoordinate.Name = "ReciverXCoordinate";
            ReciverXCoordinate.Size = new Size(784, 177);
            ReciverXCoordinate.TabIndex = 0;
            // 
            // XYZCoordinatesGraph
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(784, 561);
            Controls.Add(ReciverXCoordinate);
            Controls.Add(ReciverZCoordinate);
            Controls.Add(ReciverYCoordinate);
            Name = "XYZCoordinatesGraph";
            Text = "Координаты приемника в геоцентрической системе координат";
            ResumeLayout(false);
        }

        #endregion
        private ScottPlot.WinForms.FormsPlot ReciverYCoordinate;
        private ScottPlot.WinForms.FormsPlot ReciverZCoordinate;
        private ScottPlot.WinForms.FormsPlot ReciverXCoordinate;
    }
}