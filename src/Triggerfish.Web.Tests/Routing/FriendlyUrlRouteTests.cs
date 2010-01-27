using System.Web;
using System.Web.Routing;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Triggerfish.Web.Mvc.Testing;
using Triggerfish.Web.Routing;

namespace Triggerfish.Web.Tests
{
	[TestClass]
	public class FriendlyUrlRouteTests
	{
		[TestMethod]
		public void ShouldGenerateOutboundUrlFromRouteContainingSpaces()
		{
			FriendlyUrlRoute route = new FriendlyUrlRoute("{controller}/{action}", new MvcRouteHandler());

			RouteValueDictionary values = new RouteValueDictionary(new { controller = "The Controller", action = "The Action" });

			MvcAssert.IsOutboundRouteValid("the-controller/the-action", values, route);
		}

		[TestMethod]
		public void ShouldGenerateOutboundUrlFromRouteWithArgument()
		{
			FriendlyUrlRoute route = new FriendlyUrlRoute("artists/{genre}", new RouteValueDictionary(new { controller = "Artists", action = "List" }), new MvcRouteHandler());

			RouteValueDictionary values = new RouteValueDictionary(new { controller = "Artists", action = "List", genre = "Pop" });

			MvcAssert.IsOutboundRouteValid("artists/pop", values, route);
		}

		[TestMethod]
		public void ShouldGenerateInboundRouteFromStandardRoute()
		{
			FriendlyUrlRoute route = new FriendlyUrlRoute("{controller}/{action}", new MvcRouteHandler());

			RouteValueDictionary values = new RouteValueDictionary(new { controller = "controller", action = "action" }); 

			MvcAssert.IsInboundRouteValid(values, "~/controller/action", route);
		}

		[TestMethod]
		public void ShouldGenerateInboundRouteFromRouteWithArgument()
		{
			FriendlyUrlRoute route = new FriendlyUrlRoute("artists/{genre}", new RouteValueDictionary(new { controller = "Artists", action = "List" }), new MvcRouteHandler());

			RouteValueDictionary values = new RouteValueDictionary(new { controller = "Artists", action = "List", genre = "pop" });

			MvcAssert.IsInboundRouteValid(values, "~/artists/pop", route);
		}
	}
}
