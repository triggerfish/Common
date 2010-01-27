using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Triggerfish.Web.Routing.Testing;

namespace Triggerfish.Testing.Tests
{
	[TestClass]
	public class RouteDictionaryComparisonTests
	{
		[TestMethod]
		public void ShouldEquateSelfComparison()
		{
			object values = new {
				key1 = "Value1",
				key2 = "Value2",
				key3 = 4
			};
			RouteValueDictionary expected = new RouteValueDictionary(values);

			RoutingAssert.AreDictionariesEqual(expected, expected);
		}

		[TestMethod]
		public void ShouldEquateSameRouteValues()
		{
			object values = new
			{
				key1 = "Value1",
				key2 = "Value2",
				key3 = 4
			};
			RouteValueDictionary expected = new RouteValueDictionary(values);
			RouteValueDictionary actual = new RouteValueDictionary(values);

			RoutingAssert.AreDictionariesEqual(expected, actual);
		}

		[TestMethod]
		public void ShouldNotEquateDifferentDictionaryValues1()
		{
			object values = new
			{
				key1 = "Value1",
				key2 = "Value2",
				key3 = 4
			};
			RouteValueDictionary expected = new RouteValueDictionary(values);
			expected["key3"] = null;
			RouteValueDictionary actual = new RouteValueDictionary(values);

			try
			{
				RoutingAssert.AreDictionariesEqual(expected, actual);
			}
			catch (Exception)
			{
				// assert exception is the correct behaviour
				return;
			}
			Assert.IsTrue(false, "RoutingAssert.AreRouteValuesEqual thinks two unequal objects are equal");
		}

		[TestMethod]
		public void ShouldNotEquateDifferentDictionaryValues2()
		{
			object values = new
			{
				key1 = "Value1",
				key2 = "Value2",
				key3 = 4
			};
			RouteValueDictionary expected = new RouteValueDictionary(values);
			RouteValueDictionary actual = new RouteValueDictionary(values);
			actual["key1"] = "ValueX";

			try
			{
				RoutingAssert.AreDictionariesEqual(expected, actual);
			}
			catch (Exception)
			{
				// assert exception is the correct behaviour
				return;
			}
			Assert.IsTrue(false, "RoutingAssert.AreRouteValuesEqual thinks two unequal objects are equal");
		}

		[TestMethod]
		public void ShouldNotEquateDifferentSizedDictionaries()
		{
			object values = new
			{
				key1 = "Value1",
				key2 = "Value2",
				key3 = 4
			};
			RouteValueDictionary expected = new RouteValueDictionary(values);
			RouteValueDictionary actual = new RouteValueDictionary(values);
			actual.Remove("key2");

			try
			{
				RoutingAssert.AreDictionariesEqual(expected, actual);
			}
			catch (Exception)
			{
				// assert exception is the correct behaviour
				return;
			}
			Assert.IsTrue(false, "RoutingAssert.AreRouteValuesEqual thinks two unequal objects are equal");
		}

		[TestMethod]
		public void ShouldNotEquateAgainstNull()
		{
			object values = new
			{
				key1 = "Value1",
				key2 = "Value2",
				key3 = 4
			};
			RouteValueDictionary expected = new RouteValueDictionary(values);

			try
			{
				RoutingAssert.AreDictionariesEqual(expected, null);
			}
			catch (Exception)
			{
				// assert exception is the correct behaviour
				return;
			}
			Assert.IsTrue(false, "RoutingAssert.AreRouteValuesEqual thinks two unequal objects are equal");
		}
	}
}
