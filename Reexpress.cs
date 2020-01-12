using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using Where1.wstat;

namespace Where1.wstat_tests
{
	[TestClass]
	public class Reexpress
	{
		[TestMethod]
		public void ZScoreTest()//About 1 second on my machine
		{
			double threshold = Math.Pow(10, -12);

			int listLength = 500;
			int[] dimensions = { 1, 2, 3, 10 };
			int runs = 1000;

			Random rand = new Random();

			for (int i = 0; i < runs; i++)
			{
				for (int j = 0; j < dimensions.Length; j++)
				{
					List<double> unwoundSet = new List<double>();
					for (int k = 0; k < listLength * dimensions[j]; k++)
					{
						double num = rand.NextDouble() * 10000;
						num *= rand.Next(0, 2) == 1 ? 1 : -1;

						unwoundSet.Add(num);
					}

					VectorSet original = VectorSet.CreateVectorSetFromList(unwoundSet, dimensions[j]);
					VectorSet standadrized = original.StandardizeSet(true);

					for (int k = 0; k < dimensions[j]; k++)
					{
						DataSet set = standadrized.DataSets[k];

						Assert.IsTrue(Math.Abs(set.Mean) < threshold);

						Assert.IsTrue(Math.Abs(set.PopulationStandardDeviation - 1) < threshold);
					}
				}
			}
		}
	}
}
