using Xunit;
using RINEXDataAnaliser.DataStructures;
using System.Text.RegularExpressions;

namespace RINEXDataAnaliser.Tests
{
	public class DataStructuresTests
	{
		[Fact]
		public void ObsDataParseTest()
		{
			Dictionary<string, double> dataDict = new Dictionary<string, double>();
			dataDict.Add("C1C", 24007193.157);
            dataDict.Add("L1C", 126158657.09105);
            dataDict.Add("C1W", 24007192.907);
            dataDict.Add("C2W", 24007190.898);
            dataDict.Add("L2W", 98305441.35502);
            dataDict.Add("C2L", 24007190.822);
            dataDict.Add("L2L", 98305447.33105);
            dataDict.Add("C5Q", 24007193.934);
            dataDict.Add("L5Q", 94209395.09307);
            dataDict.Add("C1L", 24007192.547);
            dataDict.Add("L1L", 126158648.07305);

            string data = "G23  24007193.157 5 126158657.09105  24007192.907 2  24007190.898 2  98305441.35502  24007190.822 5  98305447.33105  24007193.934 7  94209395.09307  24007192.547 5 126158648.07305";
			string regexString = @"G\d{2}\s{1}(?<C1C_quality>\d{1})?\s*(?<C1C>\d{8}.\d{3})\s{1}(?<L1C_quality>\d{1})?\s*(?<L1C>\d{1,9}.\d{5})\s{1}(?<C1W_quality>\d{1})?\s*(?<C1W>\d{8}.\d{3})\s{1}(?<C2W_quality>\d{1})?\s*(?<C2W>\d{8}.\d{3})\s{1}(?<L2W_quality>\d{1})?\s*(?<L2W>\d{1,9}.\d{5})\s{1}(?<C2L_quality>\d{1})?\s*(?<C2L>\d{8}.\d{3})\s{1}(?<L2L_quality>\d{1})?\s*(?<L2L>\d{1,9}.\d{5})\s{1}(?<C5Q_quality>\d{1})?\s*(?<C5Q>\d{8}.\d{3})\s{1}(?<L5Q_quality>\d{1})?\s*(?<L5Q>\d{1,9}.\d{5})\s{1}(?<C1L_quality>\d{1})?\s*(?<C1L>\d{8}.\d{3})\s{1}(?<L1L_quality>\d{1})?\s*(?<L1L>\d{1,9}.\d{5})";

			Regex regex = new Regex(regexString, RegexOptions.Compiled);

			RINEX_obs_satelite_data obs_Satelite_Data = new RINEX_obs_satelite_data(data, regex);

            Assert.Equal(dataDict, obs_Satelite_Data.data);
        }
	}
}
