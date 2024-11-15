using RINEXDataAnaliser.DataStructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MathNet.Numerics;
using MathNet.Numerics.LinearAlgebra.Solvers;
using MathNet.Numerics.LinearAlgebra.Double;

namespace RINEXDataAnaliser
{
    public class CalcEpoch
    {
        public DateTime epochDate = new();
        public Dictionary<string, SatData> satelitesData = new();
    }

    public class SatData
    {
        public Dictionary<string, RINEXObsSateliteMeasuring> pseudoranges = new();
        public Dictionary<string, RINEXObsSateliteMeasuring> pseudophases = new();
        public XYZCoordinates coordinates = new();
    }

    public class CoordFinder
    {
        private const double mu = 3.986005e14;
        private const double speedOfLight = 299792458.0;
        private const double omegaDotE = 7.2921151467e-5;
        public List<CalcEpoch> findSateliteCoord(RINEXObsFile obsFile, RINEXNavGPSFile navGPSFile)
        {
            List<CalcEpoch> calcEpoches = new();
            CalcEpoch calcEpoch;

            foreach (RINEXObsEpochData epochData in obsFile.epochDatas)
            {
                calcEpoch = new();
                Dictionary<string, SatData> satelitesData = new();

                foreach (var (sateliteNumber, sateliteData) in epochData.satelitesData)
                {
                    SatData satData = new();

                    RINEXNavGPSData gpsEpoch = navGPSFile.findNeedEpoch(sateliteNumber, epochData.epochTime);

                    if (gpsEpoch != null)
                    {
                        XYZCoordinates sateliteCoords = new();

                        double A = Math.Pow(gpsEpoch.sqrtA, 2);
                        double tk = (gpsEpoch.dateTime - epochData.epochTime).TotalSeconds;
                        double Mk = gpsEpoch.m0 + (Math.Sqrt(mu) / Math.Pow(gpsEpoch.sqrtA, 3) + gpsEpoch.deltaN) * tk;
                        double Ek = 0.0;
                        double E = Mk;

                        for (int i = 0; i < 10; i++)
                        {
                            Ek = E;
                            E -= (Ek - gpsEpoch.e * Math.Sin(Ek) - Mk) / (1 - gpsEpoch.e * Math.Cos(Ek));
                        }

                        double nuk = Math.Atan2(Math.Sqrt(1 - Math.Pow(gpsEpoch.e, 2)) * Math.Sin(Ek), Math.Cos(Ek) - gpsEpoch.e) + gpsEpoch.omega;
                        double deltauk = gpsEpoch.cus * Math.Sin(2 * nuk) + gpsEpoch.cuc * Math.Cos(2 * nuk);
                        double deltark = gpsEpoch.crs * Math.Sin(2 * nuk) + gpsEpoch.crc * Math.Cos(2 * nuk);
                        double deltaik = gpsEpoch.cis * Math.Sin(2 * nuk) + gpsEpoch.cic * Math.Cos(2 * nuk);
                        double uk = nuk + deltauk;
                        double rk = A * (1 - gpsEpoch.e * Math.Cos(Ek)) + deltark;
                        double ik = gpsEpoch.i0 + deltaik + gpsEpoch.iDot * tk;
                        double xko = rk * Math.Cos(uk);
                        double yko = rk * Math.Sin(uk);
                        double Omegak = gpsEpoch.omega0 + (gpsEpoch.omegaDot - omegaDotE) * tk - omegaDotE * gpsEpoch.ttoe;
                        double xk = xko * Math.Cos(Omegak) - yko * Math.Cos(ik) * Math.Cos(Omegak);
                        double yk = xko * Math.Sin(Omegak) - yko * Math.Cos(ik) * Math.Sin(Omegak);
                        double zk = yko * Math.Sin(ik);

                        sateliteCoords.X = xk;
                        sateliteCoords.Y = yk;
                        sateliteCoords.Z = zk;
                        satData.coordinates = sateliteCoords;
                        satData.pseudoranges = sateliteData.pseudoranges;
                        satData.pseudophases = sateliteData.pseudophases;
                        satelitesData.Add(sateliteNumber, satData);
                    }
                }

                calcEpoch.satelitesData = satelitesData;
                calcEpoch.epochDate = epochData.epochTime;
                calcEpoches.Add(calcEpoch);
            }
            return calcEpoches;
        }

        public List<CalcEpoch> findSateliteCoordsAlm(RINEXObsFile obsFile, RINEXNavGPSFile navGPSFile)
        {
            List<CalcEpoch> calcEpoches = new();
            CalcEpoch calcEpoch;

            foreach (RINEXObsEpochData epochData in obsFile.epochDatas)
            {
                calcEpoch = new();
                Dictionary<string, SatData> satelitesData = new();

                foreach (var (sateliteNumber, sateliteData) in epochData.satelitesData)
                {
                    SatData satData = new();

                    RINEXNavGPSData gpsEpoch = navGPSFile.findNeedEpoch(sateliteNumber, epochData.epochTime);

                    if (gpsEpoch != null)
                    {
                        XYZCoordinates sateliteCoords = new();

                        double sqrtA = gpsEpoch.sqrtA;
                        double e = gpsEpoch.e;
                        double i0 = gpsEpoch.i0;
                        double Omega0 = gpsEpoch.omega0;
                        double OmegaDot = gpsEpoch.omegaDot;
                        double m0 = gpsEpoch.m0;

                        double A = Math.Pow(sqrtA, 2);
                        double n0 = Math.Sqrt(mu / Math.Pow(A, 3));
                        double tk = (gpsEpoch.dateTime - epochData.epochTime).TotalSeconds;
                        double M = m0 + n0 * tk;

                        double E = 0, Ek = M;
                        for (int i = 0; i < 30; i++)
                        {
                            E = Ek;
                            Ek += (M - E + e * Math.Sin(E)) / (1 - e * Math.Cos(E));
                        }

                        double nuk = 2 * Math.Atan(Math.Sqrt((1 + e) / (1 - e)) * Math.Tan(E / 2));
                        double r = A * (1 - e * Math.Cos(E));
                        double xko = r * Math.Cos(nuk);
                        double yko = r * Math.Sin(nuk);
                        double Omega = Omega0 + OmegaDot * tk;

                        double xk = xko * Math.Cos(Omega) - yko * Math.Cos(i0) * Math.Cos(Omega);
                        double yk = xko * Math.Sin(Omega) - yko * Math.Cos(i0) * Math.Sin(Omega);
                        double zk = yko * Math.Sin(i0);

                        sateliteCoords.X = xk;
                        sateliteCoords.Y = yk;
                        sateliteCoords.Z = zk;
                        satData.coordinates = sateliteCoords;
                        satData.pseudoranges = sateliteData.pseudoranges;
                        satData.pseudophases = sateliteData.pseudophases;
                        satelitesData.Add(sateliteNumber, satData);
                    }
                }

                calcEpoch.satelitesData = satelitesData;
                calcEpoch.epochDate = epochData.epochTime;
                calcEpoches.Add(calcEpoch);
            }

            return calcEpoches;
        }

        public List<XYZCoordinates> findPointCoordinates(List<CalcEpoch> epochsData, double tolerance = 1e-4, int maxIterations = 100)
        {
            List<XYZCoordinates> pointCoordinates = new();

            // Начальные приближения координат
            double x = 0, y = 0, z = 0, dt = 0;

            foreach (var epochData in epochsData)
            {
                int numSatelites = epochData.satelitesData.Count;
                double[] residuals = new double[numSatelites];
                double[,] A = new double[numSatelites, 4];
                int i = 0;

                foreach (var (sateliteNumber, sateliteData) in epochData.satelitesData)
                {
                    double x_i = sateliteData.coordinates.X;
                    double y_i = sateliteData.coordinates.Y;
                    double z_i = sateliteData.coordinates.Z;

                    double distance = Math.Sqrt(Math.Pow(x - x_i, 2) + Math.Pow(y - y_i, 2) + Math.Pow(z - z_i, 2));

                    residuals[i] = sateliteData.pseudoranges["C1C"].value - (distance + dt * speedOfLight);

                    A[i, 0] = (x - x_i) / distance;
                    A[i, 1] = (y - y_i) / distance;
                    A[i, 2] = (z - z_i) / distance;
                    A[i, 3] = speedOfLight;
                    i++;
                }

                var A_matrix = DenseMatrix.OfArray(A);
                var b_matrix = DenseVector.OfArray(residuals);

                // Применение метода наименьших квадратов для нахождения корректировок
                var coords = (A_matrix.Transpose() * A_matrix).Inverse() * A_matrix.Transpose() * b_matrix;

                x = coords[0];
                y = coords[1];
                z = coords[2];

                pointCoordinates.Add(new XYZCoordinates(x, y, z));
            }

            return pointCoordinates;
        }
    }
}
