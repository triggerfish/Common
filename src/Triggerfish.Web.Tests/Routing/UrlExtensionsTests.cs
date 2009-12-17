using System.Web.Routing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Triggerfish.Testing.Web.Mvc;
using Triggerfish.Testing.Web.Routing;
using Triggerfish.Web.Routing;

namespace Triggerfish.Web.Tests
{
	[TestClass]
	public class UrlExtensionsTests
	{
		[TestMethod]
		public void ShouldCapitaliseWord()
		{
			Assert.AreEqual("Plibble", "plibble".Capitalise());
		}

		[TestMethod]
		public void ShouldEncodeAllValues()
		{
			RouteValueDictionary expected = new RouteValueDictionary(new
			{
				controller = "this-controller",
				action = "this-action",
				argument = "this-argument"
			});

			RouteValueDictionary actual = new RouteValueDictionary(new
			{
				controller = "This Controller",
				action = "This Action",
				argument = "This Argument"
			});

			actual.Encode();
			RoutingAssert.AreDictionariesEqual(expected, actual);
		}

		[TestMethod]
		public void ShouldDecodeAllValues()
		{
			RouteValueDictionary expected = new RouteValueDictionary(new {
				controller = "this controller",
				action = "this action",
				argument = "this argument"
			});

			RouteValueDictionary actual = new RouteValueDictionary(new
			{
				controller = "this-controller",
				action = "this-action",
				argument = "this-argument"
			});

			actual.Decode();
			RoutingAssert.AreDictionariesEqual(expected, actual);
		}
	}
}
