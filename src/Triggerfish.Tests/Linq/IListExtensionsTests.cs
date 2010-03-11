using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Triggerfish.Linq;

namespace Triggerfish.Tests.Linq
{
	[TestClass]
	public class IListExtensionsTests
	{
		[TestMethod]
		public void ShouldTestPositive()
		{
			// arrange
			List<int> list = new List<int> {
				0, 1, 2, 3, 4
			};

			// act and assert
			Assert.IsTrue(list.InBounds(4));
		}

		[TestMethod]
		public void ShouldTestNegative()
		{
			// arrange
			List<int> list = new List<int> {
				0, 1, 2, 3, 4
			};

			// act and assert
			Assert.IsFalse(list.InBounds(5));
		}

		[TestMethod]
		public void ShouldTestNegativeIfListNull()
		{
			// arrange
			List<int> list = null;

			// act and assert
			Assert.IsFalse(list.InBounds(4));
		}
	}
}
