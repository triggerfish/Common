using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Routing;
using Moq;
using Triggerfish.Web.Testing;

namespace Triggerfish.Web.Mvc.Testing
{
	/// <summary>
	/// Static helper methods to create inbound urls using the MVC framework
	/// </summary>
	public static class InboundRoutingHelpers
	{
		/// <summary>
		/// Create inbound route values from a string url. 
		/// </summary>
		/// <param name="a_url">The url from which to generate the route</param>
		/// <param name="a_route">The route from which to generate the route</param>
		/// <returns>The route values</returns>
		public static RouteValueDictionary GenerateInboundRoute(string a_url, Route a_route)
		{
			HttpContextBase mockHttp = HttpHelpers.MockHttpContext(a_url);
			RouteData data = a_route.GetRouteData(mockHttp);
			if (null != data)
			{
				return data.Values;
			}

			return null;
		}

		/// <summary>
		/// Create inbound route values from a string url. Requires a delegate
		/// to populate a route collection with all possible routes.
		/// </summary>
		/// <param name="a_url">The url from which to generate the route</param>
		/// <param name="a_registerRoutes">Delegate to register all possible routes</param>
		/// <returns>The route values</returns>
		public static RouteValueDictionary GenerateInboundRoute(string a_url, Action<RouteCollection> a_registerRoutes)
		{
			RouteCollection routes = new RouteCollection();
			a_registerRoutes(routes);

			HttpContextBase mockHttp = HttpHelpers.MockHttpContext(a_url);

			RouteData data = routes.GetRouteData(mockHttp);
			if (null != data)
			{
				return data.Values;
			}

			return null;
		}
	}
}
