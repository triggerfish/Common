using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Triggerfish;

namespace Triggerfish.Tests
{
	[TestClass]
	public class MathsTests
	{
		[TestMethod]
		public void ShouldRoundDownZeroValue()
		{
			// act
			int i = Maths.RoundToNearest(1000, 50, RoundDirection.Down);

			// assert
			Assert.AreEqual(1000, i);
		}

		[TestMethod]
		public void ShouldRoundDownQuarterWayValue()
		{
			// act
			int i = Maths.RoundToNearest(1012, 50, RoundDirection.Down);

			// assert
			Assert.AreEqual(1000, i);
		}

		[TestMethod]
		public void ShouldRoundDownHalfWayValue()
		{
			// act
			int i = Maths.RoundToNearest(1025, 50, RoundDirection.Down);

			// assert
			Assert.AreEqual(1000, i);
		}

		[TestMethod]
		public void ShouldRoundDownThreeQuarterWayValue()
		{
			// act
			int i = Maths.RoundToNearest(1037, 50, RoundDirection.Down);

			// assert
			Assert.AreEqual(1000, i);
		}

		[TestMethod]
		public void ShouldNotRoundDownFullValue()
		{
			// act
			int i = Maths.RoundToNearest(1050, 50, RoundDirection.Down);

			// assert
			Assert.AreEqual(1050, i);
		}

		[TestMethod]
		public void ShouldNotRoundUpZeroValue()
		{
			// act
			int i = Maths.RoundToNearest(1000, 50, RoundDirection.Up);

			// assert
			Assert.AreEqual(1000, i);
		}

		[TestMethod]
		public void ShouldRoundUpQuarterWayValue()
		{
			// act
			int i = Maths.RoundToNearest(1012, 50, RoundDirection.Up);

			// assert
			Assert.AreEqual(1050, i);
		}

		[TestMethod]
		public void ShouldRoundUpHalfWayValue()
		{
			// act
			int i = Maths.RoundToNearest(1025, 50, RoundDirection.Up);

			// assert
			Assert.AreEqual(1050, i);
		}

		[TestMethod]
		public void ShouldRoundUpThreeQuarterWayValue()
		{
			// act
			int i = Maths.RoundToNearest(1037, 50, RoundDirection.Up);

			// assert
			Assert.AreEqual(1050, i);
		}

		[TestMethod]
		public void ShouldRoundUpFullValue()
		{
			// act
			int i = Maths.RoundToNearest(1050, 50, RoundDirection.Up);

			// assert
			Assert.AreEqual(1050, i);
		}

		[TestMethod]
		public void ShouldConvertPercentageToBasisPoints()
		{
			decimal pc = 17.5m;
			Assert.AreEqual(1750, Maths.ToBasisPoints(pc));
		}

		[TestMethod]
		public void ShouldConvertBasisPointsToPercentage()
		{
			int bp = 1750;
			Assert.AreEqual(17.5m, Maths.ToPercentage(bp));
		}

		[TestMethod]
		public void ShouldConvertBasisPointsToPercentageMultiplier()
		{
			int bp = 1750;
			Assert.AreEqual(0.175m, Maths.ToPercentageMultiplier(bp));
		}
	}
}
