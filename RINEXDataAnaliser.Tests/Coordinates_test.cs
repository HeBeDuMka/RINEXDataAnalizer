using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RINEXDataAnaliser;
using RINEXDataAnaliser.DataStructures;

namespace RINEXDataAnaliser.Tests
{
    public class Coordinates_test
    {
        [Fact]
        public void testCalculateTowAndGNSSWeek()
        {
            DateTime dateTime = new DateTime(2023, 12, 27);

            int gnssWeek, tow;
            (gnssWeek, tow) = GNSSTime.getGPSTime(dateTime);

            Assert.Equal((gnssWeek, tow), (2294, 259218));
        }

        [Fact]
        public void testCalcutateCoordinates()
        {
            XYZCoordinates coordinates = CoordFinder.findGPSSateliteCoordinates(
                new DateTime(2023, 12, 27), 2294, 0, 5154.02282906, 0.038236139157200,
                4.217675683120000e-09, 0.013043788960200, 0.999264255230000, -5.457550287250000e-07,
                -5.774199962620000e-08, -2.71875, 4.034687500000000e+02, 1.527369022370000e-07,
                -8.940696716310000e-08, 0.990266675599000, -1.007184810430000e-10, -1.485797943940000,
                -8.458923776700000e-09, 7.292115146700000e-05, 259200);

            XYZCoordinates etalonCoordinates = new XYZCoordinates(1.282810461996727e+07, -1.283969262405461e+07, 1.8919359130518474e+07);
            XYZCoordinates precision =  new XYZCoordinates(1e-5, 1e-5, 1e-5);

            Assert.Equal(coordinates.X, etalonCoordinates.X, 1e-5);
            Assert.Equal(coordinates.Y, etalonCoordinates.Y, 1e-5);
            Assert.Equal(coordinates.Z, etalonCoordinates.Z, 1e-5);
        }
    }
}
