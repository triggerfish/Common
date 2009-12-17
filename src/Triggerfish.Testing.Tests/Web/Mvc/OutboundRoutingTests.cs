using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Triggerfish.Testing.Web.Mvc;

namespace Triggerfish.Testing.Tests
{
	[TestClass]
	public class OutboundRoutingTests
	{
		[TestMethod]
		public void ShouldGenerateOutboundRouteFromRouteCollection1()
		{
			RouteValueDictionary route = new RouteValueDictionary(new {
				controller = "Artists",
				action = "List"
			});
			MvcAssert.IsOutboundRouteCorrect("/Artists", route, RegisterRoutes);
		}

		[TestMethod]
		public void ShouldGenerateOutboundRouteFromRouteCollection2()
		{
			RouteValueDictionary route = new RouteValueDictionary(new {
				controller = "Artists",
				action = "List",
				genre = "Pop"
			});
			MvcAssert.IsOutboundRouteCorrect("/Artists/Pop", route, RegisterRoutes);
		}

		[TestMethod]
		public void ShouldGenerateOutboundRouteFromRouteCollection3()
		{
			RouteValueDictionary route = new RouteValueDictionary(new {
				controller = "Account",
				action = "Login"
			});
			MvcAssert.IsOutboundRouteCorrect("/Account/Login", route, RegisterRoutes);
		}

		[TestMethod]
		public void ShouldGenerateOutboundRouteFromRouteCollection4()
		{
			RouteValueDictionary route = new RouteValueDictionary(new {
				controller = "Account"
			});
			MvcAssert.IsOutboundRouteCorrect("/Account", route, RegisterRoutes);
		}

		[TestMethod]
		public void ShouldGenerateOutboundRouteFromRoute1()
		{
			Route route = new Route("{controller}/{action}", new MvcRouteHandler());
			RouteValueDictionary values = new RouteValueDictionary(new
			{
				controller = "Account",
				action = "Login"
			});

			MvcAssert.IsOutboundRouteCorrect("Account/Login", values, route);
		}

		[TestMethod]
		public void ShouldGenerateOutboundRouteFromRoute2()
		{
			Route route = new Route("plibble/{action}", new RouteValueDictionary(new { controller = "Account" }), new MvcRouteHandler());
			RouteValueDictionary values = new RouteValueDictionary(new
			{
				controller = "Account",
				action = "Login"
			});

			MvcAssert.IsOutboundRouteCorrect("plibble/Login", values, route);
		}

		private static void RegisterRoutes(RouteCollection a_routes)
		{
			a_routes.MapRoute(
				null,
				"Artists/{genre}",
				new { controller = "Artists", Action = "List", genre = "All" }
			);
		
			a_routes.MapRoute(
				null,
				"{controller}/{action}",
				new { controller = "Home", action = "Index" }
			);
		}
	}
}
