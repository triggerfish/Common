using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Triggerfish.Web.Mvc.Testing;

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

			MvcAssert.IsInboundRouteValid(expected, "~/Account/Login", RegisterRoutes);
		}

		[TestMethod]
		public void ShouldGenerateInboundRouteFromRouteCollection2()
		{
			RouteValueDictionary expected = new RouteValueDictionary(new
			{
				controller = "Account",
				action = "Index"
			});

			MvcAssert.IsInboundRouteValid(expected, "~/Account", RegisterRoutes);
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

			MvcAssert.IsInboundRouteValid(expected, "~/Artists", RegisterRoutes);
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

			MvcAssert.IsInboundRouteValid(expected, "~/Artists/Pop", RegisterRoutes);
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
				MvcAssert.IsInboundRouteValid(expected, "~/Account/Login/12", RegisterRoutes);
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

			MvcAssert.IsInboundRouteValid(expected, "~/Account/Login", route);
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

			MvcAssert.IsInboundRouteValid(expected, "~/plibble/Login", route);
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
				MvcAssert.IsInboundRouteValid(expected, "~/Account/Login/12", route);
			}
			catch (Exception)
			{
				// assert exception is the correct behaviour
				return;
			}
			Assert.Fail(String.Format("MvcAssert.IsInboundRouteCorrect thinks two unequal urls are equal"));
		}

		private static void RegisterRoutes(RouteCollection routes)
		{
			routes.MapRoute(
				null,
				"Artists/{genre}",
				new { controller = "Artists", Action = "List", genre = "All" }
			);
		
			routes.MapRoute(
				null,
				"{controller}/{action}",
				new { controller = "Home", action = "Index" }
			);
		}
	}
}
