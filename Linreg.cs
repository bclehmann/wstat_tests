using Microsoft.VisualStudio.TestTools.UnitTesting;
using Xunit.Sdk;
using Where1.wstat;
using System.Collections.Generic;
using System;

namespace Where1.wstat_tests
{
	[TestClass]
	public class Linreg
	{
		[TestMethod]
		public void TwoDimensionalLinregTest()//This test takes around 7s on my machine, it's a long one
		{//n-dimensional linreg is handled by a separate library, which I trust to have been tested
			int runs = 1000;
			int[] listLength = { 10, 50, 67, 75, 100, 1000 };
			Random rand = new Random();


			double threshold = Math.Pow(10, -6);
			for (int i = 0; i < runs; i++)
			{
				for (int j = 0; j < listLength.Length; j++)
				{
					double slope = rand.NextDouble() * rand.Next(0, 100);
					slope *= rand.Next(0, 2) == 1 ? -1 : 1;

					double intercept = rand.NextDouble() * rand.Next(0, 100);
					intercept *= rand.Next(0, 2) == 1 ? -1 : 1;

					List<double> xList = new List<double>();
					List<double> yList = new List<double>();
					for (int k = 0; k < listLength[j]; k++)
					{
						xList.Add(k);

						double num = k * slope + intercept;
						yList.Add(num);
					}
					var linreg = new wstat.Regression.LinearRegressionLine();
					var coefficients = linreg.Calculate(new VectorSet(new DataSet(xList), new DataSet(yList)));


					if (slope != 0)
					{
						double slopeError = Math.Abs((slope - coefficients[1]) / slope);
						Assert.IsTrue(slopeError < threshold);
					}


					if (intercept != 0)
					{
						double interceptError = Math.Abs((intercept - coefficients[0]) / intercept);
						Assert.IsTrue(interceptError < threshold);
					}
				}
			}
		}
	}
}
