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
    [Flags]
    public enum CalcOptions
    {
        GPS = 0b00000001,
        GLONASS = 0b00000010,
        GALILEO = 0b00000100,
        QZSS = 0b00001000,
        BDS = 0b00010000,
        SBAS = 0b00100000,
        NAVIC = 0b01000000,
    }

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
        public double deltaSysTime;
    }

    public class CoordFinder
    {
        private const double mu = 3.986005e14;
        private const double speedOfLight = 299792458.0;
        private const double omegaDotE = 7.2921151467e-5;
        public List<CalcEpoch> findSateliteCoord(RINEXObsFile obsFile, RINEXNavGPSFile navGPSFile, RINEXNavGLONASSFile navGLONASSFile, CalcOptions calcOptions)
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

                    if (sateliteNumber.StartsWith("G") && ((calcOptions & CalcOptions.GPS) == CalcOptions.GPS))
                    {
                        RINEXNavGPSData gpsEpoch = navGPSFile.findNeedEpoch(sateliteNumber, epochData.epochTime);

                        if ((gpsEpoch != null) && (gpsEpoch.svHealth == 0))
                        {
                            XYZCoordinates sateliteCoords = new();

                            double A = Math.Pow(gpsEpoch.sqrtA, 2);
                            double tk = (gpsEpoch.dateTime - epochData.epochTime).TotalSeconds;
                            double Mk = gpsEpoch.m0 + (Math.Sqrt(mu) / Math.Pow(gpsEpoch.sqrtA, 3) + gpsEpoch.deltaN) * tk;
                            double Ek = 0.0;
                            double E = Mk;

                            while (Math.Abs(E - Ek) > 1e-13)
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
                            double xk = xko * Math.Cos(Omegak) - yko * Math.Cos(ik) * Math.Sin(Omegak);
                            double yk = xko * Math.Sin(Omegak) + yko * Math.Cos(ik) * Math.Cos(Omegak);
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
                    else if (sateliteNumber.StartsWith("R") && ((calcOptions & CalcOptions.GLONASS) == CalcOptions.GLONASS))
                    {
                        RINEXNavGLONASSData gloEpoch = navGLONASSFile.findNeedEpoch(sateliteNumber, epochData.epochTime);

                        if (gloEpoch != null)
                        {
                            double timeR = epochData.epochTime.Hour * 3600 + epochData.epochTime.Minute * 60 + epochData.epochTime.Second;

                            double timeK = timeR - sateliteData.pseudoranges["C1C"].value / speedOfLight;
                            double[] s_vec = [gloEpoch.satelitePos.X, gloEpoch.satelitePos.Y, gloEpoch.satelitePos.Z,
                                          gloEpoch.sateliteVelocity.X, gloEpoch.sateliteVelocity.Y, gloEpoch.sateliteVelocity.Z];
                            double h = -1;
                            int i = 0;
                            DenseVector arg, k1, k2, k3, k4, ds, s = DenseVector.OfArray(s_vec);
                            while (Math.Abs(i * h) < 60 + Math.Abs(h))
                            {
                                arg = s;
                                k1 = (DenseVector)f(arg, gloEpoch.sateliteAcceleration).Map(x => x * h);
                                arg = s + (DenseVector)k1.Map(x => x * 0.5);
                                k2 = (DenseVector)f(arg, gloEpoch.sateliteAcceleration).Map(x => x * h);
                                arg = s + (DenseVector)k2.Map(x => x * 0.5);
                                k3 = (DenseVector)f(arg, gloEpoch.sateliteAcceleration).Map(x => x * h);
                                arg = s + k3;
                                k4 = (DenseVector)f(arg, gloEpoch.sateliteAcceleration).Map(x => x * h);
                                ds = (k1 + 2 * k2 + 2 * k3 + k4) / 6;
                                s = s + ds;
                                i++;
                            }

                            satData.coordinates.X = s[0];
                            satData.coordinates.Y = s[1];
                            satData.coordinates.Z = s[2];
                            satData.pseudoranges = sateliteData.pseudoranges;
                            satData.pseudophases = sateliteData.pseudophases;
                            satelitesData.Add(sateliteNumber, satData);
                        }
                    }
                }

                calcEpoch.satelitesData = satelitesData;
                calcEpoch.epochDate = epochData.epochTime;
                calcEpoches.Add(calcEpoch);
            }
            return calcEpoches;
        }

        public DenseVector f(DenseVector arg, XYZCoordinates acc)
        {
            double m = 398600.44;
            double R = 6378.136;
            double C20 = -1082.63e-6;
            double w = 0.7292115e-4;
            double r = Math.Sqrt(Math.Pow(arg[0], 2) + Math.Pow(arg[1], 2) + Math.Pow(arg[2], 2));
            double A = m / Math.Pow(r, 3);

            double DU1 = (Math.Pow(w, 2) - A) * arg[0] + 2 * w * arg[4] + 1.5 * C20 * (m * Math.Pow(R, 2) / Math.Pow(r, 5)) * arg[0] * (1 - 5 * Math.Pow(arg[0], 2) / Math.Pow(r, 2)) + acc.X;
            double DU2 = (Math.Pow(w, 2) - A) * arg[1] + 2 * w * arg[3] + 1.5 * C20 * (m * Math.Pow(R, 2) / Math.Pow(r, 5)) * arg[1] * (1 - 5 * Math.Pow(arg[0], 2) / Math.Pow(r, 2)) + acc.Y;
            double DU3 = -A * arg[2] + 1.5 * C20 * (m * Math.Pow(R, 2) / Math.Pow(r, 5)) * arg[2] * (3 - 5 * Math.Pow(arg[2], 2) / Math.Pow(r, 2)) + acc.Z;
            var s = DenseVector.OfArray([arg[3], arg[4], arg[5], DU1, DU2, DU3]);

            return s;
        }

        public List<XYZCoordinates> findPointCoordinates(List<CalcEpoch> epochsData, double tolerance = 1e-12, int maxIterations = 100)
        {
            List<XYZCoordinates> pointCoordinates = new();

            foreach (var epochData in epochsData)
            {
                // Начальные приближения координат и смещение часов приемника относительно часов системы (в метрах)
                double x = 0, y = 0, z = 0, dt = 0;
                double dx = 0, dy = 0, dz = 0, ddt = 0;
                int iterationNumber = 0;

                do
                {
                    int satelitesCount = epochData.satelitesData.Count;
                    double[,] Hs = new double[satelitesCount, 4];
                    double[] Es = new double[satelitesCount];
                    int lineNumber = 0;

                    foreach (var (sateliteNumber, sateliteData) in epochData.satelitesData)
                    {

                        double x_s = sateliteData.coordinates.X;
                        double y_s = sateliteData.coordinates.Y;
                        double z_s = sateliteData.coordinates.Z;

                        double distance = Math.Sqrt(Math.Pow(x - x_s, 2) + Math.Pow(y - y_s, 2) + Math.Pow(z - z_s, 2));

                        Hs[lineNumber, 0] = (x - x_s) / distance;
                        Hs[lineNumber, 1] = (y - y_s) / distance;
                        Hs[lineNumber, 2] = (z - z_s) / distance;
                        Hs[lineNumber, 3] = 1;

                        // Вместо 0.0005 дописать clockBias
                        Es[lineNumber] = sateliteData.pseudoranges["C1C"].value + speedOfLight * 0.0005 - distance - dt;
                        lineNumber++;
                    }

                    var Hs_matrix = DenseMatrix.OfArray(Hs);
                    var Es_matrix = DenseVector.OfArray(Es);
                    var dOs = Hs_matrix.PseudoInverse() * Es_matrix;

                    x += dOs[0];
                    y += dOs[1];
                    z += dOs[2];
                    dt += dOs[3];
                    iterationNumber++;
                } while ((dx > tolerance || dy > tolerance || dz > tolerance || ddt > tolerance) && iterationNumber < maxIterations);

                pointCoordinates.Add(new XYZCoordinates(x, y, z));
            }

            return pointCoordinates;
        }
    }
}
