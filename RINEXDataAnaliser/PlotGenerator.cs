using RINEXDataAnaliser.DataStructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RINEXDataAnaliser
{
    public class PlotGenerator
    {
        public static void PlotEllCoords(List<ELLCoordinates> coordinates, string fileName)
        {
            double[] lat = new double[coordinates.Count()], lon = new double[coordinates.Count()];
            var indexedCoords = coordinates.Select((item, index) => new { Index = index, Value = item });

            foreach (var data in indexedCoords)
            {
                lat[data.Index] = data.Value.latitude;
                lon[data.Index] = data.Value.longitude;
            }

            ScottPlot.Plot myPlot = new();

            var sp = myPlot.Add.Scatter(lat, lon);
            sp.Smooth = true;
            sp.LegendText = "Smooth";
            sp.LineWidth = 2;
            sp.MarkerSize = 10;

            myPlot.SavePng("!Plots/" + fileName + ".png", 1600, 900);
        }
    }
}
