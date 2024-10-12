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
    }
}
