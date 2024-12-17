using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RINEXDataAnaliser;
using RINEXDataAnaliser.DataStructures;
using ScottPlot;

namespace RINEXDataAnaliser.Tests
{
    //public class Coordinates_test
    //{
    //    [Fact]
    //    public void testCalculateTowAndGNSSWeek()
    //    {
    //        DateTime dateTime = new DateTime(2023, 12, 27);

    //        int gnssWeek, tow;
    //        (gnssWeek, tow) = GNSSTime.calcGNSSWeekandTow(GNSSSystem.GPS, dateTime);

    //        Assert.Equal((gnssWeek, tow), (2294, 259218));
    //    }

    //    [Fact]
    //    public void testCalcutateSatelitesCoordinates_1()
    //    {
    //        double tow = 381600, towWeekNumber = 751, toc = 388800, tk = 380620, dtsv, tsv,
    //            af0 = 0.000101, af1 = 0, af2 = 0, Mk, deltaN = 0, sqrtA = 5153.687231, M0 = 2.590622299, ecc = 0.002659071;
    //        Mk = M0 + (Math.Sqrt(3.986005e14) / Math.Pow(sqrtA, 3) + deltaN) * tk;
    //        tk -= 0.068934152536;
    //        dtsv = af0 + af1 * (tk - toc) + af2 * Math.Pow(tk - toc, 2) + (-4.442807633e-10 * ecc * sqrtA * Math.Sin(Mk));
    //        tsv = tk - dtsv;

    //        XYZCoordinates coordinates = CoordFinder.CalcGALILEOsateliteCoordinates(
    //            5153.687231, 0, 2.590622299, 0.002659071, 0.408346989, 0.000010, 0.000001, 10.031250, 188.000000,
    //            0, 0, 0.960764427, 0, 0.683556631, 0, 388800, tsv - toc);

    //        XYZCoordinates etalonCoordinates = new XYZCoordinates(16056398.452, 579228.485, 21134571.741);

    //        Assert.Equal(380619.930965038366, tsv, 1e-5);
    //        Assert.Equal(etalonCoordinates.X, coordinates.X, 300f);
    //        Assert.Equal(etalonCoordinates.Y, coordinates.Y, 300f);
    //        Assert.Equal(etalonCoordinates.Z, coordinates.Z, 300f);
    //    }

    //    [Fact]
    //    public void testCalcutateReciverCoordinates_1()
    //    {
    //        string workingDir = @"E:\Projects\Visual_studio\RINEXDataAnaliser\Data\";
    //        Logger.OpenLogFile(Path.Combine(workingDir, $"{DateTime.Now:yyyy-MM-dd-HH-mm-ss}_test.txt"));
    //        List<CalcEpoch> epoches = new();
    //        CalcEpoch epoh = new CalcEpoch();
    //        epoh.epochDate = new DateTime(0);
    //        SatData satData = new SatData();
    //        RINEXObsSateliteMeasuring measuring = new();

    //        satData = new SatData();
    //        satData.deltaSysTime = 380620.0 - 380619.930965038366;
    //        satData.coordinates.X = 16056398.452;
    //        satData.coordinates.Y = 579228.485;
    //        satData.coordinates.Z = 21134571.741;
    //        measuring.value = 0.068934152536 * 299792458.0;
    //        satData.pseudoranges.Add("C1C", measuring);
    //        epoh.satelitesData.Add("G01", satData);

    //        satData = new SatData();
    //        satData.deltaSysTime = 380620.0 - 380619.930659384816;
    //        satData.coordinates.X = 20820911.820;
    //        satData.coordinates.Y = 6730763.874;
    //        satData.coordinates.Z = 14719963.263;
    //        measuring.value = 0.069796642572 * 299792458.0;
    //        satData.pseudoranges.Add("C1C", measuring);
    //        epoh.satelitesData.Add("G11", satData);

    //        satData = new SatData();
    //        satData.deltaSysTime = 380620.0 - 380619.928062231105;
    //        satData.coordinates.X = -1480487.219;
    //        satData.coordinates.Y = 15088485.358;
    //        satData.coordinates.Z = 22027822.508;
    //        measuring.value = 0.071733292440 * 299792458.0;
    //        satData.pseudoranges.Add("C1C", measuring);
    //        epoh.satelitesData.Add("G14", satData);

    //        satData = new SatData();
    //        satData.deltaSysTime = 380620.0 - 380619.918944607081;
    //        satData.coordinates.X = 425029.645;
    //        satData.coordinates.Y = -15319179.907;
    //        satData.coordinates.Z = 21952403.331;
    //        measuring.value = 0.081115057551 * 299792458.0;
    //        satData.pseudoranges.Add("C1C", measuring);
    //        epoh.satelitesData.Add("G17", satData);

    //        satData = new SatData();
    //        satData.deltaSysTime = 380620.0 - 380619.918927558756;
    //        satData.coordinates.X = 25780555.811;
    //        satData.coordinates.Y = 7253207.115;
    //        satData.coordinates.Z = -1122911.829;
    //        measuring.value = 0.081504991989 * 299792458.0;
    //        satData.pseudoranges.Add("C1C", measuring);
    //        epoh.satelitesData.Add("G19", satData);

    //        satData = new SatData();
    //        satData.deltaSysTime = 380620.0 - 380619.924100632255;
    //        satData.coordinates.X = 20015678.283;
    //        satData.coordinates.Y = -9805991.030;
    //        satData.coordinates.Z = 14196315.806;
    //        measuring.value = 0.075745936146 * 299792458.0;
    //        satData.pseudoranges.Add("C1C", measuring);
    //        epoh.satelitesData.Add("G20", satData);

    //        satData = new SatData();
    //        satData.deltaSysTime = 380620.0 - 380619.919706250890;
    //        satData.coordinates.X = -8132799.544;
    //        satData.coordinates.Y = 22998268.460;
    //        satData.coordinates.Z = 10517193.150;
    //        measuring.value = 0.080086552836 * 299792458.0;
    //        satData.pseudoranges.Add("C1C", measuring);
    //        epoh.satelitesData.Add("G22", satData);

    //        satData = new SatData();
    //        satData.deltaSysTime = 380620.0 - 380619.921671936638;
    //        satData.coordinates.X = 6606972.272;
    //        satData.coordinates.Y = 25340032.273;
    //        satData.coordinates.Z = 3457260.692;
    //        measuring.value = 0.077995667166 * 299792458.0;
    //        satData.pseudoranges.Add("C1C", measuring);
    //        epoh.satelitesData.Add("G31", satData);

    //        satData = new SatData();
    //        satData.deltaSysTime = 380620.0 - 380619.931699985464;
    //        satData.coordinates.X = 16824925.570;
    //        satData.coordinates.Y = 3385377.209;
    //        satData.coordinates.Z = 20202374.054;
    //        measuring.value = 0.068801027302 * 299792458.0;
    //        satData.pseudoranges.Add("C1C", measuring);
    //        epoh.satelitesData.Add("G32", satData);

    //        epoches.Add(epoh);

    //        var coords = CoordFinder.FindReciverCoordinatesOneEpoch(epoh);

    //        Assert.Equal(2846217.471, coords.X, 10f);
    //        Assert.Equal(2201619.070, coords.Y, 10f);
    //        Assert.Equal(5248769.341, coords.Z, 10f);
    //    }

    //    [Fact]
    //    public void testCalcutateReciverCoordinates4Satelites()
    //    {
    //        string workingDir = @"E:\Projects\Visual_studio\RINEXDataAnaliser\Data\";
    //        Logger.OpenLogFile(Path.Combine(workingDir, $"{DateTime.Now:yyyy-MM-dd-HH-mm-ss}_test.txt"));
    //        List<CalcEpoch> epoches = new();
    //        CalcEpoch epoh = new CalcEpoch();
    //        epoh.epochDate = new DateTime(0);
    //        SatData satData = new SatData();
    //        RINEXObsSateliteMeasuring measuring = new();

    //        satData = new SatData();
    //        satData.deltaSysTime = 380620.0 - 380619.930965038366;
    //        satData.coordinates.X = 16056398.452;
    //        satData.coordinates.Y = 579228.485;
    //        satData.coordinates.Z = 21134571.741;
    //        measuring.value = 0.068934152536 * 299792458.0;
    //        satData.pseudoranges.Add("C1C", measuring);
    //        epoh.satelitesData.Add("G01", satData);

    //        satData = new SatData();
    //        satData.deltaSysTime = 380620.0 - 380619.930659384816;
    //        satData.coordinates.X = 20820911.820;
    //        satData.coordinates.Y = 6730763.874;
    //        satData.coordinates.Z = 14719963.263;
    //        measuring.value = 0.069796642572 * 299792458.0;
    //        satData.pseudoranges.Add("C1C", measuring);
    //        epoh.satelitesData.Add("G11", satData);

    //        satData = new SatData();
    //        satData.deltaSysTime = 380620.0 - 380619.928062231105;
    //        satData.coordinates.X = -1480487.219;
    //        satData.coordinates.Y = 15088485.358;
    //        satData.coordinates.Z = 22027822.508;
    //        measuring.value = 0.071733292440 * 299792458.0;
    //        satData.pseudoranges.Add("C1C", measuring);
    //        epoh.satelitesData.Add("G14", satData);

    //        satData = new SatData();
    //        satData.deltaSysTime = 380620.0 - 380619.918944607081;
    //        satData.coordinates.X = 425029.645;
    //        satData.coordinates.Y = -15319179.907;
    //        satData.coordinates.Z = 21952403.331;
    //        measuring.value = 0.081115057551 * 299792458.0;
    //        satData.pseudoranges.Add("C1C", measuring);
    //        epoh.satelitesData.Add("G17", satData);

    //        satData = new SatData();
    //        satData.deltaSysTime = 380620.0 - 380619.918927558756;
    //        satData.coordinates.X = 25780555.811;
    //        satData.coordinates.Y = 7253207.115;
    //        satData.coordinates.Z = -1122911.829;
    //        measuring.value = 0.081504991989 * 299792458.0;
    //        satData.pseudoranges.Add("C1C", measuring);
    //        epoh.satelitesData.Add("G19", satData);

    //        satData = new SatData();
    //        satData.deltaSysTime = 380620.0 - 380619.924100632255;
    //        satData.coordinates.X = 20015678.283;
    //        satData.coordinates.Y = -9805991.030;
    //        satData.coordinates.Z = 14196315.806;
    //        measuring.value = 0.075745936146 * 299792458.0;
    //        satData.pseudoranges.Add("C1C", measuring);
    //        epoh.satelitesData.Add("G20", satData);

    //        satData = new SatData();
    //        satData.deltaSysTime = 380620.0 - 380619.919706250890;
    //        satData.coordinates.X = -8132799.544;
    //        satData.coordinates.Y = 22998268.460;
    //        satData.coordinates.Z = 10517193.150;
    //        measuring.value = 0.080086552836 * 299792458.0;
    //        satData.pseudoranges.Add("C1C", measuring);
    //        epoh.satelitesData.Add("G22", satData);

    //        satData = new SatData();
    //        satData.deltaSysTime = 380620.0 - 380619.921671936638;
    //        satData.coordinates.X = 6606972.272;
    //        satData.coordinates.Y = 25340032.273;
    //        satData.coordinates.Z = 3457260.692;
    //        measuring.value = 0.077995667166 * 299792458.0;
    //        satData.pseudoranges.Add("C1C", measuring);
    //        epoh.satelitesData.Add("G31", satData);

    //        satData = new SatData();
    //        satData.deltaSysTime = 380620.0 - 380619.931699985464;
    //        satData.coordinates.X = 16824925.570;
    //        satData.coordinates.Y = 3385377.209;
    //        satData.coordinates.Z = 20202374.054;
    //        measuring.value = 0.068801027302 * 299792458.0;
    //        satData.pseudoranges.Add("C1C", measuring);
    //        epoh.satelitesData.Add("G32", satData);

    //        epoches.Add(epoh);

    //        var coords = CoordFinder.FindReciverCoordinates(epoches)[0];

    //        Assert.Equal(2846217.471, coords.X, 1e-2f);
    //        Assert.Equal(2201619.070, coords.Y, 1e-2f);
    //        Assert.Equal(5248769.341, coords.Z, 1e-2f);
    //    }
    //}
}
