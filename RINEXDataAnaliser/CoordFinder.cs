using RINEXDataAnaliser.DataStructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MathNet.Numerics;
using MathNet.Numerics.LinearAlgebra.Solvers;
using MathNet.Numerics.LinearAlgebra.Double;
using System.Runtime.Serialization.Formatters;

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
        private const double mu_1 = 3.986005e14;
        private const double speedOfLight = 299792458.0;
        private const double omegaDotE = 7.2921151467e-5;

        #region ГЛОНАСС

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
        public static XYZCoordinates CalcGLONASSSateliteCoordinates(double _x, double _y, double _z, double _vx, double _vy, double _vz, double _ax, double _ay, double _az, double dt)
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
        private static (double, double, double, double, double, double) glonassSatelliteMotion(double x, double y, double z,
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

        #endregion

        #region GALILEO

        /// <summary>
        /// Функция для расчета координат спутника GALILEO
        /// </summary>
        /// <param name="sqrtA">Корень из длины большей полуоси орбиты</param>
        /// <param name="deltaN">Разница среднего движения от вычисленного значения</param>
        /// <param name="M0">Средняя аномалия в момент излучения</param>
        /// <param name="ecc">Эксцентриситет</param>
        /// <param name="omega">Аргумент перигея</param>
        /// <param name="cus">Амплитуда косинусного поправочного члена к аргументу широты</param>
        /// <param name="cuc">Амплитуда синусного поправочного члена к аргументу широты</param>
        /// <param name="crs">Амплитуда косинусного поправочного члена к радиусу орбиты</param>
        /// <param name="crc">Амплитуда синусного поправочного члена к радиусу орбиты</param>
        /// <param name="cis">Амплитуда косинусного поправочного члена к углу наклонения</param>
        /// <param name="cic">Амплитуда синусного поправочного члена к углу наклонения</param>
        /// <param name="i0">Угол наклонения в момент излучения</param>
        /// <param name="iDot">Скорость изменения угла наклонения</param>
        /// <param name="Omega0">Долгота восходящего узла орбитальной плоскости в недельную эпоху</param>
        /// <param name="omegaDot">Скорость изменения угла прямого восхождения</param>
        /// <param name="t0e">Время эфемерид в момент излучения</param>
        /// <param name="tk">Время применика в момент прешествия (в формате времени недели GALILEO)</param>
        /// <returns></returns>
        public static XYZCoordinates CalcGALILEOsateliteCoordinates(double sqrtA, double deltaN, double M0, double ecc, double omega,
            double cus, double cuc, double crs, double crc, double cis, double cic, double i0, double iDot, double Omega0,
            double omegaDot, double t0e, double tk)
        {
            double A, n0, n, M, Ek1, Ek2, sinnu, cosnu, nu, phi, du, dr, di, u, r, i, xo, yo, Omega, x, y, z;
            double mu = 3.986005e14;
            double omegaE = 7.2921151467e-5;

            A = Math.Pow(sqrtA, 2);
            n0 = Math.Sqrt(mu / Math.Pow(A, 3));
            n = n0 + deltaN;
            M = M0 + n * tk;
            Ek1 = 0.0;
            Ek2 = M;

            while (Math.Abs(Ek1 - Ek2) > 1e-9)
            {
                Ek1 = Ek2;
                Ek2 -= (Ek1 - ecc * Math.Sin(Ek1) - M) / (1 - ecc * Math.Cos(Ek1));
            }

            sinnu = (Math.Sqrt(1 - Math.Pow(ecc, 2)) * Math.Sin(Ek2)) / (1 - ecc * Math.Cos(Ek2));
            cosnu = (Math.Cos(Ek2) - ecc) / (1 - ecc * Math.Cos(Ek2));
            nu = Math.Atan2(sinnu, cosnu);
            phi = nu + omega;
            du = cus * Math.Sin(2 * phi) + cuc * Math.Cos(2 * phi);
            dr = crs * Math.Sin(2 * phi) + crc * Math.Cos(2 * phi);
            di = cis * Math.Sin(2 * phi) + cic * Math.Cos(2 * phi);
            u = phi + du;
            r = A * (1 - ecc * Math.Cos(Ek2)) + dr;
            i = i0 + di + iDot * tk;
            xo = r * Math.Cos(u);
            yo = r * Math.Sin(u);
            Omega = Omega0 + (omegaDot - omegaE) * tk - omegaE * t0e;
            x = xo * Math.Cos(Omega) - yo * Math.Sin(Omega) * Math.Cos(i);
            y = xo * Math.Sin(Omega) + yo * Math.Cos(Omega) * Math.Cos(i);
            z = yo * Math.Sin(i);

            return new XYZCoordinates(x ,y, z);
        }

        #endregion

        public static List<CalcEpoch> FindSateliteCoord(RINEXObsFile obsFile, RINEXNavGPSFile navGPSFile, RINEXNavGLONASSFile navGLONASSFile, RINEXNavGALILEOFile navGALILEOFile, CalcOptions calcOptions)
        {
            List<CalcEpoch> calcEpoches = new();
            CalcEpoch calcEpoch;

            foreach (RINEXObsEpochData epochData in obsFile.epochDatas)
            {
                calcEpoch = new();
                Dictionary<string, SatData> satelitesData = new();
                Logger.WriteLineToLog($"Расчет координат спутников для времени {epochData.epochTime:yyyy-MM-dd-HH-mm-ss}\n");

                foreach (var (sateliteNumber, sateliteData) in epochData.satelitesData)
                {
                    SatData satData = new();

                    if (sateliteNumber.StartsWith("G") && ((calcOptions & CalcOptions.GPS) == CalcOptions.GPS))
                    {
                        RINEXNavGPSData gpsEpoch = navGPSFile.findNeedEpoch(sateliteNumber, epochData.epochTime);

                        if ((gpsEpoch != null) && (gpsEpoch.svHealth == 0))
                        {
                            Logger.WriteLineToLog($"Для спутника {sateliteNumber} выбрана эпоха за {gpsEpoch.dateTime:yyyy-MM-dd-HH-mm-ss}");
                            double obsWeekNumber, tow, toe, toc, dtsv, tsv, tk;
                            double af0 = gpsEpoch.clockBias, af1 = gpsEpoch.clockDrift, af2 = gpsEpoch.clockDriftRate;
                            (obsWeekNumber, tow) = GNSSTime.calcGNSSWeekandTow(GNSSSystem.GPS, epochData.epochTime);
                            (_, toc) = GNSSTime.calcGNSSWeekandTow(GNSSSystem.GPS, gpsEpoch.dateTime);
                            toe = gpsEpoch.ttoe;
                            tk = ((obsWeekNumber - gpsEpoch.gpsWeek) * 604800 + tow) - toe;
                            dtsv = af0 + af1 * (tk - toc) + af2 * Math.Pow(tk - toc, 2);
                            tsv = tk - dtsv;
                            Logger.WriteLineToLog($"Время для спутника {sateliteNumber}: {tsv}");

                            satData.coordinates = CalcGALILEOsateliteCoordinates(gpsEpoch.sqrtA, gpsEpoch.deltaN, gpsEpoch.m0, gpsEpoch.e,
                                gpsEpoch.omega, gpsEpoch.cus, gpsEpoch.cuc, gpsEpoch.crs, gpsEpoch.crc, gpsEpoch.cis, gpsEpoch.cic,
                                gpsEpoch.i0, gpsEpoch.iDot, gpsEpoch.omega0, gpsEpoch.omegaDot, gpsEpoch.ttoe, tsv);

                            Logger.WriteLineToLog($"Параметры эфемерид спутника {sateliteNumber}: sqrtA={gpsEpoch.sqrtA}, deltaN={gpsEpoch.deltaN},\n" +
                                $"m0={gpsEpoch.m0}, ecc={gpsEpoch.e}, omega={gpsEpoch.omega}, cus={gpsEpoch.cus},\n" +
                                $"cuc={gpsEpoch.cuc}, crs={gpsEpoch.crs}, crc={gpsEpoch.crc}, cis={gpsEpoch.cis}, cic={gpsEpoch.cic},\n" +
                                $"i0={gpsEpoch.i0}, idot={gpsEpoch.iDot}, Omega0={gpsEpoch.omega0}, OmegaDot={gpsEpoch.omegaDot},\n" +
                                $"toe={gpsEpoch.ttoe}, af0={gpsEpoch.clockBias}, af1={gpsEpoch.clockDrift}, af2={gpsEpoch.clockDriftRate}");
                            Logger.WriteLineToLog($"Координаты спутника {sateliteNumber}: x={satData.coordinates.X}, y={satData.coordinates.Y}, z={satData.coordinates.Z}");
                            
                            satData.pseudoranges = sateliteData.pseudoranges;
                            satData.pseudophases = sateliteData.pseudophases;
                            satData.deltaSysTime = dtsv;
                            Logger.WriteLineToLog($"Псевдодальность до спутника {sateliteNumber}: p={satData.pseudoranges["C1C"].value}");

                            satelitesData.Add(sateliteNumber, satData);
                        }
                        else
                        {
                            Logger.WriteLineToLog($"Спутник {sateliteNumber} пропущен");
                        }
                        Logger.WriteLineToLog("");
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
                            satData.deltaSysTime = gloEpoch.clockBias;
                            satelitesData.Add(sateliteNumber, satData);
                        }
                    }
                    else if (sateliteNumber.StartsWith("E") && ((calcOptions & CalcOptions.GALILEO) == CalcOptions.GALILEO))
                    {
                        RINEXNavGALILEOData galEpoch = navGALILEOFile.findNeedEpoch(sateliteNumber, epochData.epochTime);

                        if (galEpoch != null)
                        {
                            double obsWeekNumber, tow, toe, tsv, tk;
                            (obsWeekNumber, tow) = GNSSTime.calcGNSSWeekandTow(GNSSSystem.GALILEO, epochData.epochTime);
                            toe = galEpoch.ttoe;
                            tk = ((obsWeekNumber - galEpoch.galWeek) * 604800 + tow) - toe;
                            tsv = tk - sateliteData.pseudoranges["C1C"].value / speedOfLight;

                            satData.coordinates = CalcGALILEOsateliteCoordinates(galEpoch.sqrtA, galEpoch.deltaN, galEpoch.m0, galEpoch.e,
                                galEpoch.omega, galEpoch.cus, galEpoch.cuc, galEpoch.crs, galEpoch.crc, galEpoch.cis, galEpoch.cic,
                                galEpoch.i0, galEpoch.iDot, galEpoch.omega0, galEpoch.omegaDot, galEpoch.ttoe, tk);

                            satData.pseudoranges = sateliteData.pseudoranges;
                            satData.pseudophases = sateliteData.pseudophases;
                            satData.deltaSysTime = galEpoch.clockBias;
                            satelitesData.Add(sateliteNumber, satData);
                        }
                    }
                }

                calcEpoch.satelitesData = satelitesData;
                calcEpoch.epochDate = epochData.epochTime;
                calcEpoches.Add(calcEpoch);

                Logger.WriteLineToLog($"------------------------------------------------------------------------------\n");
            }
            return calcEpoches;
        }

        public static Dictionary<string, double> FindSatelitesAngle(CalcEpoch curentEpoch)
        {
            Dictionary<string, double> angles = new();
            XYZCoordinates reciverCoorinates = FindReciverCoordinatesOneEpoch(curentEpoch);

            foreach (var (sateliteNumber, sateliteData) in curentEpoch.satelitesData)
            {
                DenseVector reciverCoorinatesVector = DenseVector.OfArray([reciverCoorinates.X, reciverCoorinates.Y, reciverCoorinates.Z]);
                DenseVector sateliteCoorinatesVector = DenseVector.OfArray([sateliteData.coordinates.X, sateliteData.coordinates.Y, sateliteData.coordinates.Z]);

                var lineOfSightECEF = sateliteCoorinatesVector - reciverCoorinatesVector;
                double range = lineOfSightECEF.Norm(1);
                var unitLineOfSightECEF = lineOfSightECEF / range;
                double cosLat = Math.Cos(Math.Atan2(reciverCoorinates.Z, Math.Sqrt(Math.Pow(reciverCoorinates.X, 2) + Math.Pow(reciverCoorinates.Y, 2))));
                double sinLat = Math.Sin(Math.Atan2(reciverCoorinates.Z, Math.Sqrt(Math.Pow(reciverCoorinates.X, 2) + Math.Pow(reciverCoorinates.Y, 2))));
                double cosLon = Math.Cos(Math.Atan2(reciverCoorinates.Z, reciverCoorinates.X));
                double sinLon = Math.Sin(Math.Atan2(reciverCoorinates.Z, reciverCoorinates.X));

                double[,] rotECEF2NED = new double[3, 3]
                {
                    { -sinLat * cosLon, -sinLat * sinLon, cosLat },
                    { -sinLon, cosLon, 0 },
                    { -cosLat * cosLon, -cosLat * sinLon, -sinLat }
                };

                double[] unitLineOfSightNED = new double[3];
                for (int i = 0; i < rotECEF2NED.GetLength(0); i++)
                {
                    for (int j = 0; j < rotECEF2NED.GetLength(1); j++)
                    {
                        unitLineOfSightNED[i] += rotECEF2NED[i, j] * unitLineOfSightECEF[j];
                    }
                }
                angles.Add(sateliteNumber, -Math.Asin(unitLineOfSightNED[2]) * (180.0 / Math.PI));
            }

            return angles;
        }

        public static XYZCoordinates FindReciverCoordinatesOneEpoch(CalcEpoch curentEpoh, bool useWeightMatrix = false, double sateliteAngle = 0, double tolerance = 1, int maxIterations = 100)
        {
            double x = 0, y = 0, z = 0, dt = 0;
            double dx, dy, dz, ddt;
            int iterationNumber = 0;
            Dictionary<string, double> sateliteAngles = new();
            if (useWeightMatrix)
            {
                Logger.WriteLineToLog($"\nРасчет углов места спутников {curentEpoh.epochDate:yyyy-MM-dd-HH-mm-ss}");
                sateliteAngles = FindSatelitesAngle(curentEpoh);
            }
            else
            {
                Logger.WriteLineToLog($"\nРасчет эпохи {curentEpoh.epochDate:yyyy-MM-dd-HH-mm-ss}");
            }

            do
            {
                int satelitesCount = curentEpoh.satelitesData.Count;
                double[,] Hs = new double[satelitesCount, 4], B = new double[satelitesCount, satelitesCount];
                double[] Es = new double[satelitesCount];
                int lineNumber = 0;

                foreach (var (sateliteNumber, sateliteData) in curentEpoh.satelitesData)
                {

                    double x_s = sateliteData.coordinates.X;
                    double y_s = sateliteData.coordinates.Y;
                    double z_s = sateliteData.coordinates.Z;

                    double distance = Math.Sqrt(Math.Pow(x - x_s, 2) + Math.Pow(y - y_s, 2) + Math.Pow(z - z_s, 2));

                    Hs[lineNumber, 0] = (x - x_s) / distance;
                    Hs[lineNumber, 1] = (y - y_s) / distance;
                    Hs[lineNumber, 2] = (z - z_s) / distance;
                    Hs[lineNumber, 3] = 1;

                    if (useWeightMatrix)
                    {
                        B[lineNumber, lineNumber] = (sateliteData.pseudoranges["C1C"].SSI / 9.0) * (sateliteAngles[sateliteNumber] / 90);
                    }
                    else
                    {
                        B[lineNumber, lineNumber] = 1;
                    }
                    Es[lineNumber] = sateliteData.pseudoranges["C1C"].value + speedOfLight * sateliteData.deltaSysTime - distance - dt;
                    lineNumber++;
                }

                var Hs_matrix = DenseMatrix.OfArray(Hs);
                var Es_matrix = DenseVector.OfArray(Es);
                var B_matrix = DenseMatrix.OfArray(B);
                var dOs = (Hs_matrix.Transpose() * B_matrix * Hs_matrix).Inverse() * Hs_matrix.Transpose() * B_matrix * Es_matrix;

                dx = dOs[0];
                dy = dOs[1];
                dz = dOs[2];
                ddt = dOs[3];

                x += dOs[0];
                y += dOs[1];
                z += dOs[2];
                dt += dOs[3];
                iterationNumber++;
                Logger.WriteLineToLog($"Поправки для координат на {iterationNumber} итерации: dx={dx}, dy={dy}, dz={dz}, ddt={ddt}");
            } while ((Math.Abs(dx) > tolerance || Math.Abs(dy) > tolerance || Math.Abs(dz) > tolerance || Math.Abs(ddt) > tolerance) && iterationNumber < maxIterations);

            return new XYZCoordinates(x, y, z);
        }

        public static List<XYZCoordinates> findPointCoordinates(List<CalcEpoch> epochsData, bool useWeightMatrix = false, double tolerance = 1, int maxIterations = 100)
        {
            List<XYZCoordinates> pointCoordinates = new();
            Logger.WriteLineToLog($"\n\nНачат расчет координат");

            foreach (var epochData in epochsData)
            {
                bool earthSpeedTaked = false;
                XYZCoordinates coordinates = new XYZCoordinates();

            start:
                coordinates = FindReciverCoordinatesOneEpoch(epochData, true);

                if (!earthSpeedTaked)
                {
                    Logger.WriteLineToLog($"Учет вращения Земли");
                    foreach (var (sateliteNumber, sateliteData) in epochData.satelitesData)
                    {
                        double x_s = sateliteData.coordinates.X;
                        double y_s = sateliteData.coordinates.Y;
                        double z_s = sateliteData.coordinates.Z;

                        double distance = Math.Sqrt(Math.Pow(coordinates.X - x_s, 2) + Math.Pow(coordinates.Y - y_s, 2) + Math.Pow(coordinates.Z - z_s, 2));

                        double alphaJ = omegaDotE * distance / speedOfLight;
                        sateliteData.coordinates.X = x_s + y_s * alphaJ;
                        sateliteData.coordinates.Y = y_s - x_s * alphaJ;
                        sateliteData.coordinates.Z = z_s;

                        earthSpeedTaked = true;
                    }

                    Logger.WriteLineToLog($"Перерасчет координат приемника");
                    goto start;
                }
                Logger.WriteLineToLog($"Итоговые координаты приемника: x={coordinates.X}, y={coordinates.Y}, z={coordinates.Z}");
                pointCoordinates.Add(coordinates);
            }

            return pointCoordinates;
        }
    }
}
