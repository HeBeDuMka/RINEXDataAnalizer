using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RINEXDataAnaliser.DataStructures
{
    /// <summary>
    /// Класс для хранения координат в системе XYZ
    /// </summary>
    public class XYZCoordinates
    {
        /// <summary>
        /// Координата X
        /// </summary>
        public double X;

        /// <summary>
        /// Координата Y
        /// </summary>
        public double Y;

        /// <summary>
        /// Координата Z
        /// </summary>
        public double Z;

        public XYZCoordinates()
        {
            X = 0;
            Y = 0;
            Z = 0;
        }

        public XYZCoordinates(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }
    }

    public class ELLCoordinates
    {
        // Параметры эллипсоида WGS-84
        private const double a = 6378137.0;              // Большая полуось (метры)
        private const double f = 1 / 298.257223563;      // Сжатие
        private const double e2 = f * (2 - f);           // Квадрат эксцентриситета

        public double latitude;
        public double longitude;
        public double height;

        public ELLCoordinates()
        {
            latitude = 0;
            longitude = 0;
            height = 0;
        }

        public ELLCoordinates(double b, double l, double h)
        {
            latitude = b;
            longitude = l;
            height = h;
        }

        public void fromXYZ(XYZCoordinates xyzCoordinates)
        {
            // Вычисление долготы
            double _longitude = Math.Atan2(xyzCoordinates.Y, xyzCoordinates.X);

            // Параметры для итерационного метода
            double p = Math.Sqrt(xyzCoordinates.X * xyzCoordinates.X + xyzCoordinates.Y * xyzCoordinates.Y);
            double B = Math.Atan2(xyzCoordinates.Z, p * (1 - e2));
            double B_prev;
            double N;

            // Итерационный процесс для нахождения широты и высоты
            do
            {
                B_prev = B;
                N = a / Math.Sqrt(1 - e2 * Math.Sin(B) * Math.Sin(B));
                height = p / Math.Cos(B) - N;
                B = Math.Atan2(xyzCoordinates.Z + e2 * N * Math.Sin(B), p);
            } while (Math.Abs(B - B_prev) > 1e-12); // Точность сходимости

            // Перевод широты и долготы из радиан в градусы
            latitude = RadToDeg(B);
            longitude = RadToDeg(_longitude);
        }

        private static double RadToDeg(double radians)
        {
            return radians * 180.0 / Math.PI;
        }
    }
}
