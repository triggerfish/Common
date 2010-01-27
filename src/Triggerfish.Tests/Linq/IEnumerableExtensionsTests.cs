using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Triggerfish.Tests.Linq
{
	[TestClass]
	public class IEnumerableExtensionsTests
	{
		[TestMethod]
		public void ShouldIterateOverList()
		{
			// arrange
			List<int> expected = new List<int> {
				0, 1, 2, 3, 4
			};
			List<int> actual = new List<int>();

			// act
			expected.ForEach(i => actual.Add(i));

			// assert
			Assert.AreEqual(expected.Count, actual.Count);
			for (int i = 0; i < expected.Count; i++)
			{
				Assert.AreEqual(expected[i], actual[i]);
			}
		}
	}
}
