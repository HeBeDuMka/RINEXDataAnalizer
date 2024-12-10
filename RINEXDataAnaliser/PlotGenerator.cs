﻿using RINEXDataAnaliser.DataStructures;
using ScottPlot;
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
        public static void PlotEllCoords(List<ELLCoordinates> coordinates, string fileName, string workingDir)
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
            myPlot.Title("Координаты приемника в геоцентрической системе координат");
            sp.LineWidth = 0.5f;
            sp.MarkerSize = 10;

            myPlot.SavePng(Path.Combine(workingDir, fileName + "ELL.png"), 1000, 1000);
        }

        public static void PlotXYZCoords(List<XYZCoordinates> coordinates, string fileName, string workingDir)
        {
            double[] x = new double[coordinates.Count()], y = new double[coordinates.Count()], z = new double[coordinates.Count()],
                time = new double[coordinates.Count()];
            var indexedCoords = coordinates.Select((item, index) => new { Index = index, Value = item });

            foreach (var data in indexedCoords)
            {
                time[data.Index] = data.Index;
                x[data.Index] = data.Value.X;
                y[data.Index] = data.Value.Y;
                z[data.Index] = data.Value.Z;
            }

            ScottPlot.Plot plot = new();
            plot.Add.Scatter(time, x);
            plot.Title("Координата X приемника");
            plot.SavePng(Path.Combine(workingDir, fileName + "X.png"), 1600, 900);

            plot = new();
            plot.Add.Scatter(time, y);
            plot.Title("Координата Y приемника");
            plot.SavePng(Path.Combine(workingDir, fileName + "Y.png"), 1600, 900);

            plot = new();
            plot.Add.Scatter(time, z);
            plot.Title("Координата Z приемника");
            plot.SavePng(Path.Combine(workingDir, fileName + "Z.png"), 1600, 900);
        }

        public static void PlotSatsTrack(List<CalcEpoch> satelitesCoords, string workingDir)
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
                sp.LineWidth = 0.5f;
                sp.MarkerSize = 10;

                myPlot.SavePng(Path.Combine(workingDir, sateliteNumber + ".png"), 1600, 900);
            }
        }
    }
}
