using Microsoft.VisualStudio.TestTools.UnitTesting;
using Xunit.Sdk;
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Where1.wstat_tests
{
	[TestClass]
	public class NormalDistribution
	{
		[TestMethod]
		public void NormalDistributionTest() {//Takes less than 20ms on my machine
			double threshold = Math.Pow(10, -4); //Greater than 1 ten-thousandth accuracy is difficult when the table is precise 1 ten-thousandth

			StreamReader f = new StreamReader("../../../data/normCdfTable.json");

			Dictionary<string, double> cdfTable = System.Text.Json.JsonSerializer.Deserialize<Dictionary<string,double>>(f.ReadToEnd());

			foreach (KeyValuePair<string,double> curr in cdfTable) {
				double key = double.Parse(curr.Key);

				double calculated = wstat.Distribution.NormalDistribution.Cdf(key);
				double error = Math.Abs(calculated - curr.Value);

				Assert.IsTrue(error < threshold);
			}



		}

		[TestMethod]
		public void InverseNormalDistributionTest()//About 5s on my machine
		{
			int runs = 10000;
			Random rand = new Random();

			for (int i = 0; i < runs; i++)
			{
				double parameter = rand.NextDouble() * rand.Next(0, 4);
				parameter *= rand.Next(0, 2) == 1 ? -1 : 1;

				double threshold = Math.Pow(10, -3);


				if (parameter != 0)
				{
					double calculated = wstat.Distribution.NormalDistribution.InvCdf(wstat.Distribution.NormalDistribution.Cdf(parameter));
					double error = Math.Abs((calculated - parameter) / parameter);
					Assert.IsTrue(error < threshold);
				}
			}
		}
	}
}
