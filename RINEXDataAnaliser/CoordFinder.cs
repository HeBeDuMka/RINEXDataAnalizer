using RinexDataAnaliser.DataStructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MathNet.Numerics;
using MathNet.Numerics.LinearAlgebra.Solvers;
using MathNet.Numerics.LinearAlgebra.Double;
using System.Runtime.Serialization.Formatters;

namespace RinexDataAnaliser
{
    [Flags]
    public enum GNSSSystem
    {
        None = 0,
        GPS = 0b00000001,
        GLONASS = 0b00000010,
        GALILEO = 0b00000100,
        BEIDOU = 0b00001000
    }

    public class CalcEpoch
    {
        public DateTime epochDate = new();
        public Dictionary<string, SatData> satelitesData = new();
    }

    public class SatData
    {
        public Dictionary<string, RinexObsSateliteMeasuring> pseudoranges = new();
        public Dictionary<string, RinexObsSateliteMeasuring> pseudophases = new();
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
        public static XYZCoordinates CalcGLONASSSateliteCoordinates(double _x, double _y, double _z, double _vx, double _vy, double _vz, double _ax, double _ay, double _az,
            double clockBias, double frequencyBias, double tow, double toe, double pRangeL1, double pRangeL2, bool useRelativeDelay, bool useIonoDelay,
            bool useTropoDelay, GNSSSystem gnssSystem, string f1BandName, string f2BandName)
        {
            double x, y, z, vx, vy, vz, ax, ay, az;

            x = _x;
            y = _y;
            z = _z;
            vx = _vx;
            vy = _vy;
            vz = _vz;
            ax = _ax;
            ay = _ay;
            az = _az;

            double pDelay;
            if (useIonoDelay)
            {
                double f1sq, f2sq, f1, f2;
                (f1, f2) = FrequencyManager.getTwoFreq(gnssSystem, f1BandName, f2BandName);
                f1sq = Math.Pow(f1, 2);
                f2sq = Math.Pow(f2, 2);
                pDelay = ((f1sq * pRangeL1 - f2sq * pRangeL2) / (f1sq - f2sq)) / speedOfLight;
            }
            else
            {
                pDelay = pRangeL1 / speedOfLight;
            }

            double Tj = (tow - pDelay) % 86400;

            double dt = Tj + clockBias - frequencyBias * (Tj - toe);
            if (dt < 0)
            {
                dt += 86400;
            }

            double h;
            if (dt > tow)
            {
                h = -1;
            }
            else
            {
                h = 1;
            }

            int i = 0;
            DenseVector s = DenseVector.OfArray([x, y, z, vx, vy, vz]), ds;
            while (Math.Abs(i * h) < Math.Abs(dt - (tow % 86400) + h))
            {
                var arg = s;
                var k1 = h * glonassSatelliteMotion(arg, ax, ay, az);
                arg = s + 0.5 * k1;
                var k2 = h * glonassSatelliteMotion(arg, ax, ay, az);
                arg = s + 0.5 * k2;
                var k3 = h * glonassSatelliteMotion(arg, ax, ay, az);
                arg = s + 0.5 * k3;
                var k4 = h * glonassSatelliteMotion(arg, ax, ay, az);
                ds = (k1 + 2 * k2 + 2 * k3 + k4) / 6;
                s += ds;

                i++;
            }

            x = s[0];
            y = s[1];
            z = s[2];
            vx = s[3];
            vy = s[4];
            vz = s[5];

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
        private static DenseVector glonassSatelliteMotion(DenseVector arg, double ax, double ay, double az)
        {
            double x, y, z, vx, vy, vz;
            x = arg[0];
            y = arg[1];
            z = arg[2];
            vx = arg[3];
            vy = arg[4];
            vz = arg[5];

            double mu = 3.9860044e14;
            double R = 6378136;
            double C20 = -1082.62e-6;
            double OmegaEDot = 7.292115e-5;
            double r = Math.Sqrt(Math.Pow(x, 2) + Math.Pow(y, 2) + Math.Pow(z, 2));
            double A = mu / Math.Pow(r, 3);

            double dx = vx;
            double dy = vy;
            double dz = vz;

            double dvx = (Math.Pow(OmegaEDot, 2) - A) * x + 2 * omegaDotE * vz + 1.5 * C20 * ((mu * Math.Pow(R, 2)) / Math.Pow(r, 5)) * x * (1 - ((5 * Math.Pow(z, 2)) / Math.Pow(r, 2))) + ax;
            double dvy = (Math.Pow(OmegaEDot, 2) - A) * y + 2 * omegaDotE * vy + 1.5 * C20 * ((mu * Math.Pow(R, 2)) / Math.Pow(r, 5)) * y * (1 - ((5 * Math.Pow(z, 2)) / Math.Pow(r, 2))) + ay;
            double dvz = - A * z + 1.5 * C20 * ((mu * Math.Pow(R, 2)) / Math.Pow(r, 5)) * z * (1 - ((5 * Math.Pow(z, 2)) / Math.Pow(r, 2))) + az;

            return DenseVector.OfArray([dx, dy, dz, dvx, dvy, dvz]);
        }

        #endregion

        #region GALILEO, GPS, Beidou

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
        /// <param name="tr">Время применика в момент прешествия (в формате времени недели GALILEO)</param>
        /// <param name="pRange">Псевдодальность в диапазоне L1</param>
        /// <param name="af0">Сдвиг шкалы времени спутника T^j (t_OC) относительно шкалы времени системы GPS T_GPS (t_OC ) в момент, когда показания часов системы GPS равны значению t_OC</param>
        /// <param name="af1">Коэффициент при линейном члене в полиномиальной модели расхождения показаний часов спутника T^j (t_OC) относительно показаний часов системы GPS T_GPS (t_OC )</param>
        /// <param name="af2">Коэффициент при квадратичном члене в полиномиальной модели расхождения показаний часов спутника T^j (t_OC) относительно показаний часов системы GPS T_GPS (t_OC )</param>
        /// <param name="t0c">Показания часов системы GPS на узловой момент расчета поправок к показаниям спутниковых часов</param>
        /// <param name="tgd"></param>
        /// <param name="useRelativeDelay">Использовать ли в расчетах релятивийскую поправку</param>
        /// <returns>Координаты спутника и смещение часов спутника</returns>
        public static (XYZCoordinates, double) CalcGalileoGpsBeidousateliteCoordinates(double sqrtA, double deltaN, double M0, double ecc, double omega,
            double cus, double cuc, double crs, double crc, double cis, double cic, double i0, double iDot, double Omega0,
            double omegaDot, double t0e, double t0c, double tgd, double tr, double pRangeL1, double pRangeL2, double af0, double af1, double af2,
            bool useRelativeDelay, bool useIonoDelay, bool useTropoDelay, GNSSSystem gnssSystem, string f1BandName, string f2BandName)
        {
            double Ek1, Ek2, sinnu, cosnu, nu, phi, du, dr, di, u, r, i, xo, yo, Omega, x, y, z;
            double mu = 3.986005e14;
            double omegaE = 7.2921151467e-5;
            double C = -4.442807633e-10;

            // Формула 1.65
            double Tj, pDelay;
            if (useIonoDelay)
            {
                double f1sq, f2sq, f1, f2;
                (f1, f2) = FrequencyManager.getTwoFreq(gnssSystem, f1BandName, f2BandName);
                f1sq = Math.Pow(f1, 2);
                f2sq = Math.Pow(f2, 2);
                pDelay = ((f1sq * pRangeL1 - f2sq * pRangeL2) / (f1sq - f2sq)) / speedOfLight;
            }
            else
            {
                pDelay = pRangeL1 / speedOfLight;
            }
            Tj = (tr - pDelay) % 604800;

            // Формула 1.69
            double tk;
            tk = Tj - t0e;

            // Формула 1.70, 1.71
            double n, M, dTj, TGPS;
            n = deltaN + Math.Sqrt(mu) / Math.Pow(sqrtA, 3);
            M = M0 + n * tk;

            // Формулы 1.66 - 1.68
            dTj = af0 + af1 * (Tj - t0c) + af2 * Math.Pow(Tj - t0c, 2) * tgd;
            if (useRelativeDelay)
            {
                double dTr;
                dTr = C * ecc * sqrtA * Math.Sin(M);
                dTj += dTr;
            }
            TGPS = Tj - dTj;
            tk = TGPS - t0e;
            
            if (tk > 302400)
            {
                tk -= 604800;
            }
            else if (tk < -302400)
            {
                tk += 604800;
            }

            // Формула 1.71
            M = M0 + n * tk;

            // Формула 1.72
            Ek1 = 0.0;
            Ek2 = M;
            while (Math.Abs(Ek1 - Ek2) > 1e-9)
            {
                Ek1 = Ek2;
                Ek2 -= (Ek1 - ecc * Math.Sin(Ek1) - M) / (1 - ecc * Math.Cos(Ek1));
            }

            // Формула 1.73
            sinnu = (Math.Sqrt(1 - Math.Pow(ecc, 2)) * Math.Sin(Ek2)) / (1 - ecc * Math.Cos(Ek2));
            cosnu = (Math.Cos(Ek2) - ecc) / (1 - ecc * Math.Cos(Ek2));
            nu = Math.Atan2(sinnu, cosnu);

            // Формулы 1.74
            phi = nu + omega;

            // Формулы 1.75 - 1.77
            du = cus * Math.Sin(2 * phi) + cuc * Math.Cos(2 * phi);
            dr = crs * Math.Sin(2 * phi) + crc * Math.Cos(2 * phi);
            di = cis * Math.Sin(2 * phi) + cic * Math.Cos(2 * phi);
            u = phi + du;
            r = Math.Pow(sqrtA, 2) * (1 - ecc * Math.Cos(Ek2)) + dr;
            i = i0 + di + iDot * tk;

            if (gnssSystem == GNSSSystem.BEIDOU)
            {
                Omega = Omega0 + omegaDot * tk - omegaE * t0e;
                xo = r * Math.Cos(u);
                yo = r * Math.Sin(u);
                double xg, yg, zg;
                xg = xo * Math.Cos(Omega) - yo * Math.Sin(Omega) * Math.Cos(i);
                yg = xo * Math.Sin(Omega) + yo * Math.Cos(Omega) * Math.Cos(i);
                zg = yo * Math.Sin(i);
                x = xg * Math.Cos(Omega) + yg * Math.Sin(Omega) * Math.Cos(-5) + zg * Math.Sin(Omega) * Math.Sin(-5);
                y = -xg * Math.Sin(Omega) + yg * Math.Cos(Omega) * Math.Cos(-5) + zg * Math.Cos(Omega) * Math.Sin(-5);
                z = -yg * Math.Sin(-5) + zg * Math.Cos(-5);
            }
            else
            {
                // Формула 1.78
                Omega = Omega0 + (omegaDot - omegaE) * tk - omegaE * t0e;

                // Формула 1.79
                xo = r * Math.Cos(u);
                yo = r * Math.Sin(u);
                x = xo * Math.Cos(Omega) - yo * Math.Sin(Omega) * Math.Cos(i);
                y = xo * Math.Sin(Omega) + yo * Math.Cos(Omega) * Math.Cos(i);
                z = yo * Math.Sin(i);
            }
            return (new XYZCoordinates(x ,y, z), dTj);
        }

        #endregion

        public static List<CalcEpoch> FindSateliteCoord(RinexObsFile obsFile, RINEXNavGPSFile navGPSFile, RINEXNavGLONASSFile navGLONASSFile, RINEXNavGALILEOFile navGALILEOFile,
            RINEXNavBeidouFile navBeidouFile, GNSSSystem calcOptions,
            bool useRelativeCorr, bool useIonoDelayCorr, bool useTropoDelayCorr)
        {
            List<CalcEpoch> calcEpoches = new();
            CalcEpoch calcEpoch;

            foreach (RinexObsEpochData reciverEpohData in obsFile.epochDatas)
            {
                calcEpoch = new();
                Dictionary<string, SatData> satelitesData = new();

                foreach (var (sateliteNumber, sateliteData) in reciverEpohData.satelitesData)
                {
                    SatData satData = new();

                    if (sateliteNumber.StartsWith("G") && ((calcOptions & GNSSSystem.GPS) == GNSSSystem.GPS))
                    {
                        RINEXNavGPSData gpsEpoch = navGPSFile.findNeedEpoch(sateliteNumber, reciverEpohData.epochTime);

                        if ((gpsEpoch != null) && (gpsEpoch.svHealth == 0))
                        {
                            XYZCoordinates coordinates;
                            double tow, dtsv;
                            double af0 = gpsEpoch.clockBias, af1 = gpsEpoch.clockDrift, af2 = gpsEpoch.clockDriftRate;
                            // Время приемника в системе времени GPS
                            (_, tow) = GNSSTime.calcGNSSWeekandTow(GNSSSystem.GPS, reciverEpohData.epochTime);

                            (coordinates, dtsv) = CalcGalileoGpsBeidousateliteCoordinates(gpsEpoch.sqrtA, gpsEpoch.deltaN, gpsEpoch.m0, gpsEpoch.e,
                                gpsEpoch.omega, gpsEpoch.cus, gpsEpoch.cuc, gpsEpoch.crs, gpsEpoch.crc, gpsEpoch.cis, gpsEpoch.cic,
                                gpsEpoch.i0, gpsEpoch.iDot, gpsEpoch.omega0, gpsEpoch.omegaDot, gpsEpoch.ttoe, gpsEpoch.ttoe, gpsEpoch.tgd,
                                tow, sateliteData.pseudoranges.Where(kvp => kvp.Key.StartsWith("C1")).First().Value.value, sateliteData.pseudoranges.Where(kvp => kvp.Key.StartsWith("C2")).First().Value.value,
                                af0, af1, af2, useRelativeCorr, useIonoDelayCorr, useTropoDelayCorr, GNSSSystem.GPS, "L1", "L2");

                            satData.coordinates = coordinates;
                            satData.pseudoranges = sateliteData.pseudoranges;
                            satData.pseudophases = sateliteData.pseudophases;
                            satData.deltaSysTime = dtsv;

                            satelitesData.Add(sateliteNumber, satData);
                        }
                    }
                    else if (sateliteNumber.StartsWith("R") && ((calcOptions & GNSSSystem.GLONASS) == GNSSSystem.GLONASS))
                    {
                        RINEXNavGLONASSData gloEpoch = navGLONASSFile.findNeedEpoch(sateliteNumber, reciverEpohData.epochTime);

                        if (gloEpoch != null)
                        {
                            //double timeR = epochData.epochTime.Hour * 3600 + epochData.epochTime.Minute * 60 + epochData.epochTime.Second;
                            //double timeK = timeR - sateliteData.pseudoranges.Where(kvp => kvp.Key.StartsWith("C1")).First().Value.value / speedOfLight;
                            double obsWeekNumber, tow, navWeekNumber, tb, tk;
                            (obsWeekNumber, tow) = GNSSTime.calcGNSSWeekandTow(GNSSSystem.GLONASS, reciverEpohData.epochTime);
                            (navWeekNumber, tb) = GNSSTime.calcGNSSWeekandTow(GNSSSystem.GLONASS, gloEpoch.epochTime);

                            satData.coordinates = CalcGLONASSSateliteCoordinates(gloEpoch.satelitePos.X, gloEpoch.satelitePos.Y, gloEpoch.satelitePos.Z,
                                gloEpoch.sateliteVelocity.X, gloEpoch.sateliteVelocity.Y, gloEpoch.sateliteVelocity.Z,
                                gloEpoch.sateliteAcceleration.X, gloEpoch.sateliteAcceleration.Y, gloEpoch.sateliteAcceleration.Z, gloEpoch.clockBias,
                                gloEpoch.frequencyBias, tow, tb, sateliteData.pseudoranges.Where(kvp => kvp.Key.StartsWith("C1")).First().Value.value, sateliteData.pseudoranges.Where(kvp => kvp.Key.StartsWith("C2")).First().Value.value,
                                useRelativeCorr, useIonoDelayCorr, useTropoDelayCorr, GNSSSystem.GLONASS, "G1", "G2");

                            satData.pseudoranges = sateliteData.pseudoranges;
                            satData.pseudophases = sateliteData.pseudophases;
                            satData.deltaSysTime = gloEpoch.clockBias;
                            satelitesData.Add(sateliteNumber, satData);
                        }
                    }
                    else if (sateliteNumber.StartsWith("E") && ((calcOptions & GNSSSystem.GALILEO) == GNSSSystem.GALILEO))
                    {
                        RINEXNavGALILEOData galEpoch = navGALILEOFile.findNeedEpoch(sateliteNumber, reciverEpohData.epochTime);

                        if (galEpoch != null)
                        {
                            XYZCoordinates coordinates;
                            double tow, dtsv;
                            double af0 = galEpoch.clockBias, af1 = galEpoch.clockDrift, af2 = galEpoch.clockDriftRate;
                            // Время приемника в системе времени GPS
                            (_, tow) = GNSSTime.calcGNSSWeekandTow(GNSSSystem.GALILEO, reciverEpohData.epochTime);

                            (coordinates, dtsv) = CalcGalileoGpsBeidousateliteCoordinates(galEpoch.sqrtA, galEpoch.deltaN, galEpoch.m0, galEpoch.e,
                                galEpoch.omega, galEpoch.cus, galEpoch.cuc, galEpoch.crs, galEpoch.crc, galEpoch.cis, galEpoch.cic,
                                galEpoch.i0, galEpoch.iDot, galEpoch.omega0, galEpoch.omegaDot, galEpoch.ttoe, galEpoch.ttoe, galEpoch.ttoe,
                                tow, sateliteData.pseudoranges.Where(kvp => kvp.Key.StartsWith("C1")).First().Value.value, sateliteData.pseudoranges.Where(kvp => kvp.Key.StartsWith("C5")).First().Value.value,
                                af0, af1, af2, useRelativeCorr, useIonoDelayCorr, useTropoDelayCorr, GNSSSystem.GALILEO, "E1", "E5a");

                            satData.coordinates = coordinates;
                            satData.pseudoranges = sateliteData.pseudoranges;
                            satData.pseudophases = sateliteData.pseudophases;
                            satData.deltaSysTime = dtsv;

                            satelitesData.Add(sateliteNumber, satData);
                        }
                    }
                    else if (sateliteNumber.StartsWith("C") && ((calcOptions & GNSSSystem.BEIDOU) == GNSSSystem.BEIDOU))
                    {
                        RINEXNavBeidouData bdsEpoch = navBeidouFile.findNeedEpoch(sateliteNumber, reciverEpohData.epochTime);

                        if (bdsEpoch != null)
                        {
                            XYZCoordinates coordinates;
                            double tow, dtsv;
                            double af0 = bdsEpoch.clockBias, af1 = bdsEpoch.clockDrift, af2 = bdsEpoch.clockDriftRate;
                            // Время приемника в системе времени GPS
                            (_, tow) = GNSSTime.calcGNSSWeekandTow(GNSSSystem.BEIDOU, reciverEpohData.epochTime);

                            (coordinates, dtsv) = CalcGalileoGpsBeidousateliteCoordinates(bdsEpoch.sqrtA, bdsEpoch.deltaN, bdsEpoch.m0, bdsEpoch.e,
                                bdsEpoch.omega, bdsEpoch.cus, bdsEpoch.cuc, bdsEpoch.crs, bdsEpoch.crc, bdsEpoch.cis, bdsEpoch.cic,
                                bdsEpoch.i0, bdsEpoch.iDot, bdsEpoch.omega0, bdsEpoch.omegaDot, bdsEpoch.ttoe, bdsEpoch.ttoe, bdsEpoch.tgd1,
                                tow, sateliteData.pseudoranges.Where(kvp => kvp.Key.StartsWith("C1")).First().Value.value, sateliteData.pseudoranges.Where(kvp => kvp.Key.StartsWith("C5")).First().Value.value,
                                af0, af1, af2, useRelativeCorr, useIonoDelayCorr, useTropoDelayCorr, GNSSSystem.BEIDOU, "B1", "B2");

                            satData.coordinates = coordinates;
                            satData.pseudoranges = sateliteData.pseudoranges;
                            satData.pseudophases = sateliteData.pseudophases;
                            satData.deltaSysTime = dtsv;

                            satelitesData.Add(sateliteNumber, satData);
                        }
                    }
                }

                calcEpoch.satelitesData = satelitesData;
                calcEpoch.epochDate = reciverEpohData.epochTime;
                calcEpoches.Add(calcEpoch);

            }
            return calcEpoches;
        }

        public static Dictionary<string, double> FindSatelitesAngle(CalcEpoch curentEpoch)
        {
            Dictionary<string, double> angles = new();
            XYZCoordinates reciverCoorinates = FindRawReciverCoordinates(curentEpoch);

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

        public static XYZCoordinates FindRawReciverCoordinates(CalcEpoch curentEpoh, double tolerance = 1, int maxIterations = 100)
        {
            double x = 0, y = 0, z = 0, dt = 0;
            double dx, dy, dz, ddt;
            int iterationNumber = 0;

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

                    Es[lineNumber] = sateliteData.pseudoranges.Where(kvp => kvp.Key.StartsWith("C1")).First().Value.value + speedOfLight * sateliteData.deltaSysTime - distance - dt;
                    lineNumber++;
                }

                var Hs_matrix = DenseMatrix.OfArray(Hs);
                var Es_matrix = DenseVector.OfArray(Es);
                var dOs = (Hs_matrix.Transpose() * Hs_matrix).Inverse() * Hs_matrix.Transpose() * Es_matrix;

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

            return new XYZCoordinates(x, y, z);
        }

        public static XYZCoordinates FindReciverCoordinatesOneEpoch(CalcEpoch curentEpoh, bool useWeightMatrix = false, double sateliteAngle = 0, double tolerance = 1,
            int maxIterations = 100)
        {
            double x = 0, y = 0, z = 0, dt = 0;
            double dx, dy, dz, ddt;
            int iterationNumber = 0;
            Dictionary<string, double> sateliteAngles = FindSatelitesAngle(curentEpoh);

            do
            {
                int satelitesCount = curentEpoh.satelitesData.Count;
                List<List<double>> Hs = new List<List<double>>();
                List<double> B = new List<double>();
                List<double> Es = new List<double>();
                int lineNumber = 0;

                foreach (var (sateliteNumber, sateliteData) in curentEpoh.satelitesData)
                {
                    if (sateliteAngles[sateliteNumber] >= sateliteAngle)
                    {
                        double x_s = sateliteData.coordinates.X;
                        double y_s = sateliteData.coordinates.Y;
                        double z_s = sateliteData.coordinates.Z;

                        double distance = Math.Sqrt(Math.Pow(x - x_s, 2) + Math.Pow(y - y_s, 2) + Math.Pow(z - z_s, 2));

                        Hs.Add(new List<double> { (x - x_s) / distance, (y - y_s) / distance, (z - z_s) / distance, 1 });

                        if (useWeightMatrix)
                        {
                            B.Add((1 - (sateliteData.pseudoranges.Where(kvp => kvp.Key.StartsWith("C5")).First().Value.SSI / 9.0)) * (1 - (sateliteAngles[sateliteNumber] / 90)));
                        }
                        else
                        {
                            B.Add(1);
                        }

                        if (sateliteNumber.StartsWith("C") || sateliteNumber.StartsWith("E") || sateliteNumber.StartsWith("G"))
                        {
                            Es.Add(sateliteData.pseudoranges.Where(kvp => kvp.Key.StartsWith("C1")).First().Value.value + speedOfLight * sateliteData.deltaSysTime - distance - dt);
                        }
                        else if (sateliteNumber.StartsWith("R"))
                        {
                            Es.Add(sateliteData.pseudoranges.Where(kvp => kvp.Key.StartsWith("C1")).First().Value.value - speedOfLight * sateliteData.deltaSysTime - distance - dt);
                        }

                        lineNumber++;
                    }
                }

                var Hs_matrix = DenseMatrix.OfRows(Hs);
                var Es_matrix = DenseVector.OfEnumerable(Es);
                var B_matrix = Matrix.Build.Diagonal(B.ToArray());
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
            } while ((Math.Abs(dx) > tolerance || Math.Abs(dy) > tolerance || Math.Abs(dz) > tolerance || Math.Abs(ddt) > tolerance) && iterationNumber < maxIterations);

            return new XYZCoordinates(x, y, z);
        }

        public static List<XYZCoordinates> FindReciverCoordinates(List<CalcEpoch> epochsData, bool useWeightMatrix = false, double minEvaluationAngle = 0, double tolerance = 1, int maxIterations = 100)
        {
            List<XYZCoordinates> pointCoordinates = new();

            foreach (var epochData in epochsData)
            {
                bool earthSpeedTaked = false;
                XYZCoordinates coordinates = new XYZCoordinates();

            start:
                coordinates = FindReciverCoordinatesOneEpoch(epochData, true, minEvaluationAngle);

                if (!earthSpeedTaked)
                {
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

                    goto start;
                }
                pointCoordinates.Add(coordinates);
            }

            return pointCoordinates;
        }
    }
}
