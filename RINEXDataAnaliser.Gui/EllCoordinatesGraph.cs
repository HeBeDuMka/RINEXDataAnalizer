using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RINEXDataAnaliser;
using RINEXDataAnaliser.DataStructures;
using ScottPlot;

namespace RINEXDataAnaliser.Gui
{
    public partial class EllCoordinatesGraph : Form
    {
        public EllCoordinatesGraph()
        {
            InitializeComponent();
        }

        public void plotGraph(List<XYZCoordinates> reciverXYZCoordinates, int pointSize, float LineThickness)
        {
            List<ELLCoordinates> reciverELLCoordinates = new();
            foreach (XYZCoordinates coords in reciverXYZCoordinates)
            {
                ELLCoordinates ellCoordinates = new ELLCoordinates();
                ellCoordinates.fromXYZ(coords);
                reciverELLCoordinates.Add(ellCoordinates);
            }

            double[] lat = new double[reciverELLCoordinates.Count()], lon = new double[reciverELLCoordinates.Count()];
            var indexedCoords = reciverELLCoordinates.Select((item, index) => new { Index = index, Value = item });

            foreach (var data in indexedCoords)
            {
                lat[data.Index] = data.Value.latitude;
                lon[data.Index] = data.Value.longitude;
            }

            var sp = EllCoordinatesPlot.Plot.Add.Scatter(lat, lon);
            EllCoordinatesPlot.Plot.Title("Координаты приемника в геоцентрической системе координат");
            sp.LineWidth = LineThickness;
            sp.MarkerSize = pointSize;
        }
    }
}
