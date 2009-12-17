using System.Web.Routing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Triggerfish.Web.Mvc;
using Triggerfish.Web.Routing;

namespace Triggerfish.Web.Tests
{
    /// <summary>
    ///This is a test class for RouteExtensionsTest and is intended
    ///to contain all RouteExtensionsTest Unit Tests
    ///</summary>
	[TestClass]
	public class RouteExtensionsTests
	{
		[TestMethod]
		public void MapFriendlyUrlRouteTest()
		{
			RouteCollection routes = new RouteCollection();
			routes.MapFriendlyUrlRoute("Name", "Url");
			Assert.AreEqual(1, routes.Count);

			FriendlyUrlRoute route = routes["Name"] as FriendlyUrlRoute;
			Assert.AreNotEqual(null, route);
			Assert.AreEqual("Url", route.Url);
		}

		[TestMethod]
		public void MapFriendlyUrlRouteTest2()
		{
			RouteCollection routes = new RouteCollection();
			routes.MapFriendlyUrlRoute("Name", "Url", new { controller = "Home" });
			Assert.AreEqual(1, routes.Count);

			FriendlyUrlRoute route = routes["Name"] as FriendlyUrlRoute;
			Assert.AreNotEqual(null, route);
			Assert.AreEqual(1, route.Defaults.Count);
			Assert.IsTrue(route.Defaults.ContainsKey("controller"));
			Assert.AreEqual("Home", route.Defaults["controller"]);
		}
	
		[TestMethod]
		public void MapFriendlyUrlRouteTest3()
		{
			RouteCollection routes = new RouteCollection();
			routes.MapFriendlyUrlRoute("Name", "Url", new { controller = "Home" }, new { constraint = "Constraint" });
			Assert.AreEqual(1, routes.Count);

			FriendlyUrlRoute route = routes["Name"] as FriendlyUrlRoute;
			Assert.AreNotEqual(null, route);
			Assert.AreEqual(1, route.Constraints.Count);
			Assert.IsTrue(route.Constraints.ContainsKey("constraint"));
			Assert.AreEqual("Constraint", route.Constraints["constraint"]);
		}
	}
}
