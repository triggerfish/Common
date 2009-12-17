using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Routing;
using System.Web;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Triggerfish.Testing.Web.Routing;

namespace Triggerfish.Testing.Web.Mvc
{
	/// <summary>
	/// Asserts for use in unit testing code
	/// </summary>
	public static class MvcAssert
	{
		/// <summary>
		/// Tests whether the MVC generated inbound route matches the expected route values
		/// </summary>
		/// <param name="a_expectedRouteValues">The expected route values</param>
		/// <param name="a_actualUrl">The actual url from which the route values are generated</param>
		/// <param name="a_actualRoute">The actual route from which the route values are generated</param>
		public static void IsInboundRouteCorrect(object a_expectedRouteValues, string a_actualUrl, Route a_actualRoute)
		{
			IsInboundRouteCorrect(new RouteValueDictionary(a_expectedRouteValues), a_actualUrl, a_actualRoute);
		}

		/// <summary>
		/// Tests whether the MVC generated inbound route matches the expected route values
		/// </summary>
		/// <param name="a_expectedRouteValues">The expected route values</param>
		/// <param name="a_actualUrl">The actual url from which the route values are generated</param>
		/// <param name="a_actualRoute">The actual route from which the route values are generated</param>
		public static void IsInboundRouteCorrect(RouteValueDictionary a_expectedRouteValues, string a_actualUrl, Route a_actualRoute)
		{
			RoutingAssert.AreDictionariesEqual(a_expectedRouteValues, InboundRoutingHelpers.GenerateInboundRoute(a_actualUrl, a_actualRoute));
		}

		/// <summary>
		/// Tests whether the MVC generated inbound route matches the expected route values
		/// </summary>
		/// <param name="a_expectedRouteValues">The expected route values</param>
		/// <param name="a_actualUrl">The actual url from which the route values are generated</param>
		/// <param name="a_registerRoutes">Delegate to register all possible routes</param>
		public static void IsInboundRouteCorrect(object a_expectedRouteValues, string a_actualUrl, Action<RouteCollection> a_registerRoutes)
		{
			IsInboundRouteCorrect(new RouteValueDictionary(a_expectedRouteValues), a_actualUrl, a_registerRoutes);
		}

		/// <summary>
		/// Tests whether the MVC generated inbound route matches the expected route values
		/// </summary>
		/// <param name="a_expectedRouteValues">The expected route values</param>
		/// <param name="a_actualUrl">The actual url from which the route values are generated</param>
		/// <param name="a_registerRoutes">Delegate to register all possible routes</param>
		public static void IsInboundRouteCorrect(RouteValueDictionary a_expectedRouteValues, string a_actualUrl, Action<RouteCollection> a_registerRoutes)
		{
			RoutingAssert.AreDictionariesEqual(a_expectedRouteValues, InboundRoutingHelpers.GenerateInboundRoute(a_actualUrl, a_registerRoutes));
		}

		/// <summary>
		/// Tests whether the MVC generated outbound route matches the expected url
		/// </summary>
		/// <param name="a_expectedUrl">The expected url</param>
		/// <param name="a_actualRouteValues">The actual route values from which the url is generated</param>
		/// <param name="a_actualRoute">The actual route from which the url is generated</param>
		public static void IsOutboundRouteCorrect(string a_expectedUrl, object a_actualRouteValues, Route a_actualRoute)
		{
			IsOutboundRouteCorrect(a_expectedUrl, new RouteValueDictionary(a_actualRouteValues), a_actualRoute);
		}

		/// <summary>
		/// Tests whether the MVC generated outbound route matches the expected url
		/// </summary>
		/// <param name="a_expectedUrl">The expected url</param>
		/// <param name="a_actualRouteValues">The actual route values from which the url is generated</param>
		/// <param name="a_actualRoute">The actual route from which the url is generated</param>
		public static void IsOutboundRouteCorrect(string a_expectedUrl, RouteValueDictionary a_actualRouteValues, Route a_actualRoute)
		{
			Assert.AreEqual(a_expectedUrl, OutboundRoutingHelpers.GenerateOutboundUrl(a_actualRouteValues, a_actualRoute));
		}

		/// <summary>
		/// Tests whether the MVC generated outbound route matches the expected url. Requires a delegate
		/// to populate a route collection with all possible routes.
		/// </summary>
		/// <param name="a_expectedUrl">The expected url</param>
		/// <param name="a_actualRouteValues">The actual route values from which the url is generated</param>
		/// <param name="a_registerRoutes">Delegate to register all possible routes</param>
		public static void IsOutboundRouteCorrect(string a_expectedUrl, object a_actualRouteValues, Action<RouteCollection> a_registerRoutes)
		{
			IsOutboundRouteCorrect(a_expectedUrl, new RouteValueDictionary(a_actualRouteValues), a_registerRoutes);
		}

		/// <summary>
		/// Tests whether the MVC generated outbound route matches the expected url. Requires a delegate
		/// to populate a route collection with all possible routes.
		/// </summary>
		/// <param name="a_expectedUrl">The expected url</param>
		/// <param name="a_actualRouteValues">The actual route values from which the url is generated</param>
		/// <param name="a_registerRoutes">Delegate to register all possible routes</param>
		public static void IsOutboundRouteCorrect(string a_expectedUrl, RouteValueDictionary a_actualRouteValues, Action<RouteCollection> a_registerRoutes)
		{
			Assert.AreEqual(a_expectedUrl, OutboundRoutingHelpers.GenerateOutboundUrl(a_actualRouteValues, a_registerRoutes));
		}
	}
}
