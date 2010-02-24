using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Triggerfish;

namespace Triggerfish.Tests
{
	[TestClass]
	public class CurrencyTests
	{
		[TestMethod]
		public void ShouldConvertPenniesToPoundAndPence()
		{
			// arrange
			int p = 1234;

			// act and assert
			Assert.AreEqual(12.34m, Currency.ToPoundsAndPence(p));
		}

		[TestMethod]
		public void ShouldConvertExactPenceDownToNearestPenny()
		{
			// arrange
			decimal d = 12.34m;

			// act and assert
			Assert.AreEqual(12.34m, Currency.RoundToPoundsAndPence(d, RoundDirection.Down));
		}

		[TestMethod]
		public void ShouldConvertExactPenceUpToNearestPenny()
		{
			// arrange
			decimal d = 12.34m;

			// act and assert
			Assert.AreEqual(12.34m, Currency.RoundToPoundsAndPence(d, RoundDirection.Up));
		}

		[TestMethod]
		public void ShouldConvertExactPenceDownToNearestPennies()
		{
			// arrange
			decimal d = 12.34m;

			// act and assert
			Assert.AreEqual(1234, Currency.ToPence(d, RoundDirection.Down));
		}

		[TestMethod]
		public void ShouldConvertExactPenceUpToNearestPennies()
		{
			// arrange
			decimal d = 12.34m;

			// act and assert
			Assert.AreEqual(1234, Currency.ToPence(d, RoundDirection.Up));
		}

		[TestMethod]
		public void ShouldConvertDownToNearestPenny()
		{
			// arrange
			decimal d = 12.34999m;

			// act and assert
			Assert.AreEqual(12.34m, Currency.RoundToPoundsAndPence(d, RoundDirection.Down));
		}

		[TestMethod]
		public void ShouldConvertUpToNearestPenny()
		{
			// arrange
			decimal d = 12.340001m;

			// act and assert
			Assert.AreEqual(12.35m, Currency.RoundToPoundsAndPence(d, RoundDirection.Up));
		}

		[TestMethod]
		public void ShouldConvertDownToNearestPennies()
		{
			// arrange
			decimal d = 12.34999m;

			// act and assert
			Assert.AreEqual(1234, Currency.ToPence(d, RoundDirection.Down));
		}

		[TestMethod]
		public void ShouldConvertUpToNearestPennies()
		{
			// arrange
			decimal d = 12.340001m;

			// act and assert
			Assert.AreEqual(1235, Currency.ToPence(d, RoundDirection.Up));
		}
	}
}
