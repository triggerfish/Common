using System.Web;
using System.Web.Routing;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Triggerfish.Testing.Web.Mvc;
using Triggerfish.Web.Routing;

namespace Triggerfish.Web.Tests
{
	[TestClass]
	public class FriendlyUrlRouteTests
	{
		[TestMethod]
		public void ShouldGenerateOutboundUrl1()
		{
			FriendlyUrlRoute route = new FriendlyUrlRoute("{controller}/{action}", new MvcRouteHandler());

			RouteValueDictionary values = new RouteValueDictionary(new { controller = "The Controller", action = "The Action" });

			MvcAssert.IsOutboundRouteCorrect("the-controller/the-action", values, route);
		}

		[TestMethod]
		public void ShouldGenerateOutboundUrl2()
		{
			FriendlyUrlRoute route = new FriendlyUrlRoute("artists/{genre}", new RouteValueDictionary(new { controller = "Artists", action = "List" }), new MvcRouteHandler());

			RouteValueDictionary values = new RouteValueDictionary(new { controller = "Artists", action = "List", genre = "Pop" });

			MvcAssert.IsOutboundRouteCorrect("artists/pop", values, route);
		}

		[TestMethod]
		public void ShouldGenerateInboundRoute1()
		{
			FriendlyUrlRoute route = new FriendlyUrlRoute("{controller}/{action}", new MvcRouteHandler());

			RouteValueDictionary values = new RouteValueDictionary(new { controller = "Controller", action = "Action" });

			MvcAssert.IsInboundRouteCorrect(values, "~/controller/action", route);
		}

		[TestMethod]
		public void ShouldGenerateInboundRoute2()
		{
			FriendlyUrlRoute route = new FriendlyUrlRoute("artists/{genre}", new RouteValueDictionary(new { controller = "Artists", action = "List" }), new MvcRouteHandler());

			RouteValueDictionary values = new RouteValueDictionary(new { controller = "Artists", action = "List", genre = "pop" });

			MvcAssert.IsInboundRouteCorrect(values, "~/artists/pop", route);
		}
	}
}
