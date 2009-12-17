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
	public class InboundRoutingTests
	{
		[TestMethod]
		public void ShouldGenerateInboundRouteFromRouteCollection1()
		{
			RouteValueDictionary expected = new RouteValueDictionary(new {
				controller = "Account",
				action = "Login"
			});

			MvcAssert.IsInboundRouteCorrect(expected, "~/Account/Login", RegisterRoutes);
		}

		[TestMethod]
		public void ShouldGenerateInboundRouteFromRouteCollection2()
		{
			RouteValueDictionary expected = new RouteValueDictionary(new
			{
				controller = "Account",
				action = "Index"
			});

			MvcAssert.IsInboundRouteCorrect(expected, "~/Account", RegisterRoutes);
		}

		[TestMethod]
		public void ShouldGenerateInboundRouteFromRouteCollection3()
		{
			RouteValueDictionary expected = new RouteValueDictionary(new
			{
				controller = "Artists",
				action = "List",
				genre = "All"
			});

			MvcAssert.IsInboundRouteCorrect(expected, "~/Artists", RegisterRoutes);
		}

		[TestMethod]
		public void ShouldGenerateInboundRouteFromRouteCollection4()
		{
			RouteValueDictionary expected = new RouteValueDictionary(new
			{
				controller = "Artists",
				action = "List",
				genre = "Pop"
			});

			MvcAssert.IsInboundRouteCorrect(expected, "~/Artists/Pop", RegisterRoutes);
		}

		[TestMethod]
		public void ShouldNotGenerateInboundRouteFromRouteCollection()
		{
			RouteValueDictionary expected = new RouteValueDictionary(new
			{
				controller = "Account",
				action = "Login",
				id = 12
			});

			try
			{
				MvcAssert.IsInboundRouteCorrect(expected, "~/Account/Login/12", RegisterRoutes);
			}
			catch (Exception)
			{
				// assert exception is the correct behaviour
				return;
			}
			Assert.Fail(String.Format("MvcAssert.IsInboundRouteCorrect thinks two unequal urls are equal"));
		}

		[TestMethod]
		public void ShouldGenerateInboundRouteFromRoute1()
		{
			Route route = new Route("{controller}/{action}", new MvcRouteHandler());
			RouteValueDictionary expected = new RouteValueDictionary(new
			{
				controller = "Account",
				action = "Login"
			});

			MvcAssert.IsInboundRouteCorrect(expected, "~/Account/Login", route);
		}

		[TestMethod]
		public void ShouldGenerateInboundRouteFromRoute2()
		{
			Route route = new Route("plibble/{action}", new RouteValueDictionary(new { controller = "Account" }), new MvcRouteHandler());
			RouteValueDictionary expected = new RouteValueDictionary(new
			{
				controller = "Account",
				action = "Login"
			});

			MvcAssert.IsInboundRouteCorrect(expected, "~/plibble/Login", route);
		}

		[TestMethod]
		public void ShouldNotGenerateInboundRouteFromRoute()
		{
			Route route = new Route("{controller}/{action}", new MvcRouteHandler());
			RouteValueDictionary expected = new RouteValueDictionary(new
			{
				controller = "Account",
				action = "Login",
				id = 12
			});

			try
			{
				MvcAssert.IsInboundRouteCorrect(expected, "~/Account/Login/12", route);
			}
			catch (Exception)
			{
				// assert exception is the correct behaviour
				return;
			}
			Assert.Fail(String.Format("MvcAssert.IsInboundRouteCorrect thinks two unequal urls are equal"));
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
