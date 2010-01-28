using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Routing;
using System.Web;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Triggerfish.Web.Routing.Testing;

namespace Triggerfish.Web.Mvc.Testing
{
	/// <summary>
	/// Asserts for use in unit testing code
	/// </summary>
	public static class MvcAssert
	{
		/// <summary>
		/// Tests whether the MVC generated inbound route matches the expected route values
		/// </summary>
		/// <param name="expectedRouteValues">The expected route values</param>
		/// <param name="actualUrl">The actual url from which the route values are generated</param>
		/// <param name="actualRoute">The actual route from which the route values are generated</param>
		public static void IsInboundRouteValid(object expectedRouteValues, string actualUrl, Route actualRoute)
		{
			IsInboundRouteValid(new RouteValueDictionary(expectedRouteValues), actualUrl, actualRoute);
		}

		/// <summary>
		/// Tests whether the MVC generated inbound route matches the expected route values
		/// </summary>
		/// <param name="expectedRouteValues">The expected route values</param>
		/// <param name="actualUrl">The actual url from which the route values are generated</param>
		/// <param name="actualRoute">The actual route from which the route values are generated</param>
		public static void IsInboundRouteValid(RouteValueDictionary expectedRouteValues, string actualUrl, Route actualRoute)
		{
			RoutingAssert.AreDictionariesEqual(expectedRouteValues, InboundRoutingHelpers.GenerateInboundRoute(actualUrl, actualRoute));
		}

		/// <summary>
		/// Tests whether the MVC generated inbound route matches the expected route values
		/// </summary>
		/// <param name="expectedRouteValues">The expected route values</param>
		/// <param name="actualUrl">The actual url from which the route values are generated</param>
		/// <param name="registerRoutes">Delegate to register all possible routes</param>
		public static void IsInboundRouteValid(object expectedRouteValues, string actualUrl, Action<RouteCollection> registerRoutes)
		{
			IsInboundRouteValid(new RouteValueDictionary(expectedRouteValues), actualUrl, registerRoutes);
		}

		/// <summary>
		/// Tests whether the MVC generated inbound route matches the expected route values
		/// </summary>
		/// <param name="expectedRouteValues">The expected route values</param>
		/// <param name="actualUrl">The actual url from which the route values are generated</param>
		/// <param name="registerRoutes">Delegate to register all possible routes</param>
		public static void IsInboundRouteValid(RouteValueDictionary expectedRouteValues, string actualUrl, Action<RouteCollection> registerRoutes)
		{
			RoutingAssert.AreDictionariesEqual(expectedRouteValues, InboundRoutingHelpers.GenerateInboundRoute(actualUrl, registerRoutes));
		}

		/// <summary>
		/// Tests whether the MVC generated outbound route matches the expected url
		/// </summary>
		/// <param name="expectedUrl">The expected url</param>
		/// <param name="actualRouteValues">The actual route values from which the url is generated</param>
		/// <param name="actualRoute">The actual route from which the url is generated</param>
		public static void IsOutboundRouteValid(string expectedUrl, object actualRouteValues, Route actualRoute)
		{
			IsOutboundRouteValid(expectedUrl, new RouteValueDictionary(actualRouteValues), actualRoute);
		}

		/// <summary>
		/// Tests whether the MVC generated outbound route matches the expected url
		/// </summary>
		/// <param name="expectedUrl">The expected url</param>
		/// <param name="actualRouteValues">The actual route values from which the url is generated</param>
		/// <param name="actualRoute">The actual route from which the url is generated</param>
		public static void IsOutboundRouteValid(string expectedUrl, RouteValueDictionary actualRouteValues, Route actualRoute)
		{
			Assert.AreEqual(expectedUrl, OutboundRoutingHelpers.GenerateOutboundUrl(actualRouteValues, actualRoute));
		}

		/// <summary>
		/// Tests whether the MVC generated outbound route matches the expected url. Requires a delegate
		/// to populate a route collection with all possible routes.
		/// </summary>
		/// <param name="expectedUrl">The expected url</param>
		/// <param name="actualRouteValues">The actual route values from which the url is generated</param>
		/// <param name="registerRoutes">Delegate to register all possible routes</param>
		public static void IsOutboundRouteValid(string expectedUrl, object actualRouteValues, Action<RouteCollection> registerRoutes)
		{
			IsOutboundRouteValid(expectedUrl, new RouteValueDictionary(actualRouteValues), registerRoutes);
		}

		/// <summary>
		/// Tests whether the MVC generated outbound route matches the expected url. Requires a delegate
		/// to populate a route collection with all possible routes.
		/// </summary>
		/// <param name="expectedUrl">The expected url</param>
		/// <param name="actualRouteValues">The actual route values from which the url is generated</param>
		/// <param name="registerRoutes">Delegate to register all possible routes</param>
		public static void IsOutboundRouteValid(string expectedUrl, RouteValueDictionary actualRouteValues, Action<RouteCollection> registerRoutes)
		{
			Assert.AreEqual(expectedUrl, OutboundRoutingHelpers.GenerateOutboundUrl(actualRouteValues, registerRoutes));
		}
	}
}
