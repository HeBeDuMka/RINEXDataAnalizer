using RINEXDataAnaliser.DataStructures;
using ScottPlot;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RINEXDataAnaliser.Gui
{
    public partial class XYZCoordinatesGraph : Form
    {
        public XYZCoordinatesGraph()
        {
            InitializeComponent();
        }

        public void plotGraph(List<XYZCoordinates> reciverXYZCoordinates, int pointSize, float LineThickness, XYZCoordinates ethalonValue)
        {
            double[] x = new double[reciverXYZCoordinates.Count()], y = new double[reciverXYZCoordinates.Count()], z = new double[reciverXYZCoordinates.Count()],
            time = new double[reciverXYZCoordinates.Count()];
            var indexedCoords = reciverXYZCoordinates.Select((item, index) => new { Index = index, Value = item });

            foreach (var data in indexedCoords)
            {
                time[data.Index] = data.Index;
                x[data.Index] = data.Value.X;
                y[data.Index] = data.Value.Y;
                z[data.Index] = data.Value.Z;
            }

            var plot = ReciverXCoordinate.Plot.Add.Scatter(time, x);
            ReciverXCoordinate.Plot.Add.HorizontalLine(ethalonValue.X);
            ReciverXCoordinate.Plot.Title("Координата X приемника");
            plot.LineWidth = LineThickness;
            plot.MarkerSize = pointSize;

            plot = ReciverYCoordinate.Plot.Add.Scatter(time, y);
            ReciverYCoordinate.Plot.Add.HorizontalLine(ethalonValue.Y);
            ReciverYCoordinate.Plot.Title("Координата Y приемника");
            plot.LineWidth = LineThickness;
            plot.MarkerSize = pointSize;

            plot = ReciverZCoordinate.Plot.Add.Scatter(time, z);
            ReciverZCoordinate.Plot.Add.HorizontalLine(ethalonValue.Z);
            ReciverZCoordinate.Plot.Title("Координата Z приемника");
            plot.LineWidth = LineThickness;
            plot.MarkerSize = pointSize;
        }
    }
}
