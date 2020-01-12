using Microsoft.VisualStudio.TestTools.UnitTesting;
using Xunit.Sdk;
using System;
using System.Collections.Generic;
using System.Text;

namespace Where1.wstat_tests
{
	[TestClass]
	public class NormalDistribution
	{
		[TestMethod]
		public void NormalDistributionTest()//About 5s on my machine
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
