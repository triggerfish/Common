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
		/// <param name="url">The url from which to generate the route</param>
		/// <param name="route">The route from which to generate the route</param>
		/// <returns>The route values</returns>
		public static RouteValueDictionary GenerateInboundRoute(string url, Route route)
		{
			HttpContextBase mockHttp = HttpHelpers.MockHttpContext(url);
			RouteData data = route.GetRouteData(mockHttp);
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
		/// <param name="url">The url from which to generate the route</param>
		/// <param name="registerRoutes">Delegate to register all possible routes</param>
		/// <returns>The route values</returns>
		public static RouteValueDictionary GenerateInboundRoute(string url, Action<RouteCollection> registerRoutes)
		{
			RouteCollection routes = new RouteCollection();
			registerRoutes(routes);

			HttpContextBase mockHttp = HttpHelpers.MockHttpContext(url);

			RouteData data = routes.GetRouteData(mockHttp);
			if (null != data)
			{
				return data.Values;
			}

			return null;
		}
	}
}
