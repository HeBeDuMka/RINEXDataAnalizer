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
    public enum GNSSSystem
    {
        GPS,
        GLONASS,
        GALILEO,
        BDS,
        QZSS,
        SBAS
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

        /// <summary>
        /// Функция для расчета координат спутника ГЛОНАСС
        /// </summary>
        /// <param name="_x">Координата x спутника из эфемерид</param>
        /// <param name="_y">Координата y спутника из эфемерид</param>
        /// <param name="_z">Координата z спутника из эфемерид</param>
        /// <param name="_vx">Скорость спутника по оси x из эфемерид</param>
        /// <param name="_vy">Скорость спутника по оси y из эфемерид</param>
        /// <param name="_vz">Скороать спутника по оси z из эфемерид</param>
        /// <param name="_ax">Ускорение спутника по оси x из эфемерид</param>
        /// <param name="_ay">Ускорение спутника по оси y из эфемерид</param>
        /// <param name="_az">Ускорение спутника по оси z из эфемерид</param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public XYZCoordinates CalcGLONASSSateliteCoordinates(double _x, double _y, double _z, double _vx, double _vy, double _vz, double _ax, double _ay, double _az, double dt)
        {
            double x, y, z, vx, vy, vz, ax, ay, az;

            x = _x * 1e3;
            y = _y * 1e3;
            z = _z * 1e3;
            vx = _vx * 1e3;
            vy = _vy * 1e3;
            vz = _vz * 1e3;
            ax = _ax * 1e3;
            ay = _ay * 1e3;
            az = _az * 1e3;

            double k1dx, k1dy, k1dz, k1dvx, k1dvy, k1dvz;
            double k2dx, k2dy, k2dz, k2dvx, k2dvy, k2dvz;
            double k3dx, k3dy, k3dz, k3dvx, k3dvy, k3dvz;
            double k4dx, k4dy, k4dz, k4dvx, k4dvy, k4dvz;

            (k1dx, k1dy, k1dz, k1dvx, k1dvy, k1dvz) = glonassSatelliteMotion(x, y, z, vx, vy, vz, ax, ay, az);
            (k2dx, k2dy, k2dz, k2dvx, k2dvy, k2dvz) = glonassSatelliteMotion(x + k1dx * dt / 2, y + k1dy * dt / 2, z + k1dz * dt / 2, vx + k1dvx * dt / 2, vy + k1dvy * dt / 2, vz + k1dvz * dt / 2, ax, ay, az);
            (k3dx, k3dy, k3dz, k3dvx, k3dvy, k3dvz) = glonassSatelliteMotion(x + k2dx * dt / 2, y + k2dy * dt / 2, z + k2dz * dt / 2, vx + k2dvx * dt / 2, vy + k2dvy * dt / 2, vz + k2dvz * dt / 2, ax, ay, az);
            (k4dx, k4dy, k4dz, k4dvx, k4dvy, k4dvz) = glonassSatelliteMotion(x + k3dx * dt, y + k3dy * dt, z + k3dz * dt, vx + k3dvx * dt, vy + k3dvy * dt, vz + k3dvz * dt, ax, ay, az);

            x = x + (dt / 6) * (k1dx + 2 * k2dx + 2 * k3dx + k4dx);
            y = y + (dt / 6) * (k1dy + 2 * k2dy + 2 * k3dy + k4dy);
            z = z + (dt / 6) * (k1dz + 2 * k2dz + 2 * k3dz + k4dz);
            vx = vx + (dt / 6) * (k1dvx + 2 * k2dvx + 2 * k3dvx + k4dvx);
            vy = vy + (dt / 6) * (k1dvy + 2 * k2dvy + 2 * k3dvy + k4dvy);
            vz = vz + (dt / 6) * (k1dvz + 2 * k2dvz + 2 * k3dvz + k4dvz);

            return new XYZCoordinates(x, y, z);
        }

        public List<CalcEpoch> FindSateliteCoord(RINEXObsFile obsFile, RINEXNavGPSFile navGPSFile, RINEXNavGLONASSFile navGLONASSFile, CalcOptions calcOptions)
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
                            double tk = (epochData.epochTime - gpsEpoch.dateTime).TotalSeconds + 18 - (sateliteData.pseudoranges["C1C"].value / speedOfLight); // <- Косяк тут
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
                            satData.deltaSysTime = gpsEpoch.clockBias;
                            satelitesData.Add(sateliteNumber, satData);
                        }
                    }
                    else if (sateliteNumber.StartsWith("R") && ((calcOptions & CalcOptions.GLONASS) == CalcOptions.GLONASS))
                    {
                        RINEXNavGLONASSData gloEpoch = navGLONASSFile.findNeedEpoch(sateliteNumber, epochData.epochTime);

                        if (gloEpoch != null)
                        {
                            //double timeR = epochData.epochTime.Hour * 3600 + epochData.epochTime.Minute * 60 + epochData.epochTime.Second;
                            //double timeK = timeR - sateliteData.pseudoranges["C1C"].value / speedOfLight;
                            double obsWeekNumber, tow, navWeekNumber, t0e, tk1, tk, dtk;
                            const double tauSys = -1.87661499e-7;
                            (obsWeekNumber, tow) = GNSSTime.calcGNSSWeekandTow(GNSSSystem.GLONASS, epochData.epochTime);
                            (navWeekNumber, t0e) = GNSSTime.calcGNSSWeekandTow(GNSSSystem.GLONASS, gloEpoch.epochTime);
                            tk1 = tow - sateliteData.pseudoranges["C1C"].value / speedOfLight;
                            dtk = tauSys + gloEpoch.clockBias - gloEpoch.frequencyBias * (tk1 - t0e);
                            tk = tk1 + dtk - t0e;

                            satData.coordinates = CalcGLONASSSateliteCoordinates(gloEpoch.satelitePos.X, gloEpoch.satelitePos.Y, gloEpoch.satelitePos.Z,
                                gloEpoch.sateliteVelocity.X, gloEpoch.sateliteVelocity.Y, gloEpoch.sateliteVelocity.Z,
                                gloEpoch.sateliteAcceleration.X, gloEpoch.sateliteAcceleration.Y, gloEpoch.sateliteAcceleration.Z, tk);

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

        /// <summary>
        /// Функция для расчета движения спутника ГЛОНАСС
        /// </summary>
        /// <param name="x">Координата x спутника из эфемерид</param>
        /// <param name="y">Координата y спутника из эфемерид</param>
        /// <param name="z">Координата z спутника из эфемерид</param>
        /// <param name="vx">Скорость спутника по оси x из эфемерид</param>
        /// <param name="vy">Скорость спутника по оси y из эфемерид</param>
        /// <param name="vz">Скороать спутника по оси z из эфемерид</param>
        /// <param name="ax">Ускорение спутника по оси x из эфемерид</param>
        /// <param name="ay">Ускорение спутника по оси y из эфемерид</param>
        /// <param name="az">Ускорение спутника по оси z из эфемерид</param>
        /// <returns></returns>
        private (double, double, double, double, double, double) glonassSatelliteMotion(double x, double y, double z,
            double vx, double vy, double vz, double ax, double ay, double az)
        {
            double mu = 3.9860044e14;
            double ae = 6378136;
            double J02 = 1082625.7e-9;
            double OmegaEDot = 7.292115e-5;
            double r = Math.Sqrt(Math.Pow(x, 2) + Math.Pow(y, 2) + Math.Pow(z, 2));

            double dx = vx;
            double dy = vy;
            double dz = vz;

            double dvx = -mu / Math.Pow(r, 3) * x - 1.5 * Math.Pow(J02, 2) * (mu * Math.Pow(ae, 2)) / Math.Pow(r, 5) * x * (1 - 5 * Math.Pow(z, 2) / Math.Pow(r, 2)) + Math.Pow(OmegaEDot, 2) * x + 2 * OmegaEDot * vy + ax;
            double dvy = -mu / Math.Pow(r, 3) * y - 1.5 * Math.Pow(J02, 2) * (mu * Math.Pow(ae, 2)) / Math.Pow(r, 5) * y * (1 - 5 * Math.Pow(z, 2) / Math.Pow(r, 2)) + Math.Pow(OmegaEDot, 2) * y + 2 * OmegaEDot * vx + ay;
            double dvz = -mu / Math.Pow(r, 3) * z - 1.5 * Math.Pow(J02, 2) * (mu * Math.Pow(ae, 2)) / Math.Pow(r, 5) * z * (1 - 5 * Math.Pow(z, 2) / Math.Pow(r, 2)) + az;

            return (dx, dy, dz, dvx, dvy, dvz);
        }

        public List<XYZCoordinates> findPointCoordinates(List<CalcEpoch> epochsData, double tolerance = 1e-12, int maxIterations = 1000)
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
                        //double angle = Math.Asin(z_s / sateliteData.pseudoranges["C1C"].value);

                        double distance = Math.Sqrt(Math.Pow(x - x_s, 2) + Math.Pow(y - y_s, 2) + Math.Pow(z - z_s, 2));

                        Hs[lineNumber, 0] = (x - x_s) / distance;
                        Hs[lineNumber, 1] = (y - y_s) / distance;
                        Hs[lineNumber, 2] = (z - z_s) / distance;
                        Hs[lineNumber, 3] = 1;

                        Es[lineNumber] = sateliteData.pseudoranges["C1C"].value + speedOfLight * sateliteData.deltaSysTime - distance - dt;
                        lineNumber++;
                    }

                    var Hs_matrix = DenseMatrix.OfArray(Hs);
                    var Es_matrix = DenseVector.OfArray(Es);
                    var dOs = Hs_matrix.PseudoInverse() * Es_matrix;

                    dx = dOs[0];
                    dy = dOs[1];
                    dz = dOs[2];
                    ddt = dOs[3];

                    x += dOs[0];
                    y += dOs[1];
                    z += dOs[2];
                    dt += dOs[3];
                    iterationNumber++;
                } while ((Math.Abs(dx) > tolerance || Math.Abs(dy) > tolerance || Math.Abs(dz) > tolerance || Math.Abs(ddt) > tolerance) && iterationNumber < maxIterations);

                pointCoordinates.Add(new XYZCoordinates(x, y, z));
            }

            return pointCoordinates;
        }
    }
}
