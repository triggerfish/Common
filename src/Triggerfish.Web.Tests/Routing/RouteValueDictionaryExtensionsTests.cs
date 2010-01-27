using System.Web.Routing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Triggerfish.Web.Mvc.Testing;
using Triggerfish.Web.Routing.Testing;
using Triggerfish.Web.Routing;

namespace Triggerfish.Web.Tests
{
	[TestClass]
	public class RouteValueDictionaryExtensionsTests
	{
		[TestMethod]
		public void ShouldAddReturnUrl()
		{
			// arrange
			RouteValueDictionary expected = new RouteValueDictionary(new {
				returnUrl = "returnHere"
			});
			RouteValueDictionary actual = new RouteValueDictionary();

			// act

			// assert
			RoutingAssert.AreDictionariesEqual(expected, actual.AddReturnUrl("returnHere"));
		}

		[TestMethod]
		public void ShouldFailToAddReturnUrl()
		{
			// arrange
			RouteValueDictionary actual = new RouteValueDictionary();

			// act

			// assert
			Assert.AreEqual(0, actual.AddReturnUrl("").Count);
		}

		[TestMethod]
		public void ShouldEncodeAllValues()
		{
			RouteValueDictionary expected = new RouteValueDictionary(new {
				controller = "this-controller",
				action = "this-action",
				argument1 = "this-argument",
				argument2 = 3
			});

			RouteValueDictionary actual = new RouteValueDictionary(new {
				controller = "This Controller",
				action = "This Action",
				argument1 = "This Argument",
				argument2 = 3
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
				argument1 = "this argument",
				argument2 = 3
			});

			RouteValueDictionary actual = new RouteValueDictionary(new {
				controller = "this-controller",
				action = "this-action",
				argument1 = "this-argument",
				argument2 = 3
			});

			actual.Decode();
			RoutingAssert.AreDictionariesEqual(expected, actual);
		}
	}
}
