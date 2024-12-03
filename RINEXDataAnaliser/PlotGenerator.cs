using RINEXDataAnaliser.DataStructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Formatters;
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
            sp.LineWidth = 0.5f;
            sp.MarkerSize = 10;

            myPlot.SavePng("!Plots/" + fileName + ".png", 1600, 900);
        }

        public static void PlotSatsTrack(List<CalcEpoch> satelitesCoords)
        {
            Dictionary<string, List<XYZCoordinates>> satelitesCoordsDict = new();
            foreach (var sateliteCoords in satelitesCoords)
            {
                foreach (var (sateliteNumber, sateliteData) in sateliteCoords.satelitesData)
                {
                    if (!satelitesCoordsDict.ContainsKey(sateliteNumber))
                    {
                        List<XYZCoordinates> tempList = new List<XYZCoordinates>();
                        tempList.Add(sateliteData.coordinates);
                        satelitesCoordsDict.Add(sateliteNumber, tempList);
                    }
                    else
                    {
                        satelitesCoordsDict[sateliteNumber].Add(sateliteData.coordinates);
                    }
                }
            }

            foreach (var (sateliteNumber, sateliteData) in satelitesCoordsDict)
            {
                ScottPlot.Plot myPlot = new();
                List<double> lat = new(), lon = new();

                foreach (var sateliteCoords in sateliteData)
                {
                    ELLCoordinates coordinates = new();
                    coordinates.fromXYZ(new XYZCoordinates(sateliteCoords.X, sateliteCoords.Y, sateliteCoords.Z));

                    lat.Add(coordinates.latitude);
                    lon.Add(coordinates.longitude);
                }

                var sp = myPlot.Add.Scatter(lon, lat);
                myPlot.Axes.SetLimits(-180, 180, -90, 90);
                sp.LegendText = "Smooth";
                sp.LineWidth = 0.5f;
                sp.MarkerSize = 10;

                myPlot.SavePng("!Plots/" + sateliteNumber + ".png", 1600, 900);
            }
        }
    }
}
