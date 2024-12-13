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
            ReciverXCoordinate = new ScottPlot.WinForms.FormsPlot();
            ReciverYCoordinate = new ScottPlot.WinForms.FormsPlot();
            ReciverZCoordinate = new ScottPlot.WinForms.FormsPlot();
            SuspendLayout();
            // 
            // ReciverXCoordinate
            // 
            ReciverXCoordinate.DisplayScale = 1F;
            ReciverXCoordinate.Location = new Point(12, 12);
            ReciverXCoordinate.Name = "ReciverXCoordinate";
            ReciverXCoordinate.Size = new Size(760, 177);
            ReciverXCoordinate.TabIndex = 0;
            // 
            // ReciverYCoordinate
            // 
            ReciverYCoordinate.DisplayScale = 1F;
            ReciverYCoordinate.Location = new Point(12, 195);
            ReciverYCoordinate.Name = "ReciverYCoordinate";
            ReciverYCoordinate.Size = new Size(760, 177);
            ReciverYCoordinate.TabIndex = 1;
            // 
            // ReciverZCoordinate
            // 
            ReciverZCoordinate.DisplayScale = 1F;
            ReciverZCoordinate.Location = new Point(12, 378);
            ReciverZCoordinate.Name = "ReciverZCoordinate";
            ReciverZCoordinate.Size = new Size(760, 177);
            ReciverZCoordinate.TabIndex = 2;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(784, 561);
            Controls.Add(ReciverZCoordinate);
            Controls.Add(ReciverYCoordinate);
            Controls.Add(ReciverXCoordinate);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
        }

        #endregion

        private ScottPlot.WinForms.FormsPlot ReciverXCoordinate;
        private ScottPlot.WinForms.FormsPlot ReciverYCoordinate;
        private ScottPlot.WinForms.FormsPlot ReciverZCoordinate;
    }
}