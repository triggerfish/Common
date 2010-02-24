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
	/// Static helper methods to create outbound urls using the MVC framework
	/// </summary>
	public static class OutboundRoutingHelpers
	{
		/// <summary>
		/// Generates an outbound url from the specified values.  
		/// </summary>
		/// <param name="routeValues">The route values from which to generate the url</param>
		/// <param name="route">The route to use to generate the url</param>
		/// <returns>The string url</returns>
		public static string GenerateOutboundUrl(object routeValues, Route route)
		{
			return GenerateOutboundVirtualPath(routeValues, route).VirtualPath;
		}

		/// <summary>
		/// Generates an outbound url from the specified values.  
		/// </summary>
		/// <param name="routeValues">The route values from which to generate the url</param>
		/// <param name="route">The route to use to generate the url</param>
		/// <returns>The string url</returns>
		public static string GenerateOutboundUrl(RouteValueDictionary routeValues, Route route)
		{
			return GenerateOutboundVirtualPath(routeValues, route).VirtualPath;
		}

		/// <summary>
		/// Generates an outbound url from the specified values.  
		/// </summary>
		/// <param name="routeValues">The route values from which to generate the url</param>
		/// <param name="route">The route to use to generate the url</param>
		/// <returns>The virtual path data object</returns>
		public static VirtualPathData GenerateOutboundVirtualPath(object routeValues, Route route)
		{
			return GenerateOutboundVirtualPath(new RouteValueDictionary(routeValues), route);
		}

		/// <summary>
		/// Generates an outbound url from the specified values.  
		/// </summary>
		/// <param name="routeValues">The route values from which to generate the url</param>
		/// <param name="route">The route to use to generate the url</param>
		/// <returns>The virtual path data object</returns>
		public static VirtualPathData GenerateOutboundVirtualPath(RouteValueDictionary routeValues, Route route)
		{
			HttpContextBase mockHttp = MockHelpers.HttpContext(null).Object;
			RequestContext context = new RequestContext(mockHttp, new RouteData());

			// Act (generate a URL)
			return route.GetVirtualPath(context, routeValues);
		}

		/// <summary>
		/// Generates an outbound url from the specified values.  Requires a delegate
		/// to populate a route collection with all possible routes.
		/// </summary>
		/// <param name="routeValues">The route values from which to generate the url</param>
		/// <param name="registerRoutes">Delegate to register all possible routes</param>
		/// <returns>The string url</returns>
		public static string GenerateOutboundUrl(object routeValues, Action<RouteCollection> registerRoutes)
		{
			return GenerateOutboundVirtualPath(routeValues, registerRoutes).VirtualPath;
		}

		/// <summary>
		/// Generates an outbound url from the specified values.  Requires a delegate
		/// to populate a route collection with all possible routes.
		/// </summary>
		/// <param name="routeValues">The route values from which to generate the url</param>
		/// <param name="registerRoutes">Delegate to register all possible routes</param>
		/// <returns>The string url</returns>
		public static string GenerateOutboundUrl(RouteValueDictionary routeValues, Action<RouteCollection> registerRoutes)
		{
			return GenerateOutboundVirtualPath(routeValues, registerRoutes).VirtualPath;
		}

		/// <summary>
		/// Generates an outbound url from the specified values.  Requires a delegate
		/// to populate a route collection with all possible routes.
		/// </summary>
		/// <param name="routeValues">The route values from which to generate the url</param>
		/// <param name="registerRoutes">Delegate to register all possible routes</param>
		/// <returns>The virtual path data object</returns>
		public static VirtualPathData GenerateOutboundVirtualPath(object routeValues, Action<RouteCollection> registerRoutes)
		{
			return GenerateOutboundVirtualPath(new RouteValueDictionary(routeValues), registerRoutes);
		}

		/// <summary>
		/// Generates an outbound url from the specified values.  Requires a delegate
		/// to populate a route collection with all possible routes.
		/// </summary>
		/// <param name="routeValues">The route values from which to generate the url</param>
		/// <param name="registerRoutes">Delegate to register all possible routes</param>
		/// <returns>The virtual path data object</returns>
		public static VirtualPathData GenerateOutboundVirtualPath(RouteValueDictionary routeValues, Action<RouteCollection> registerRoutes)
		{
			// Arrange (get the routing config and test context)
			RouteCollection routeConfig = new RouteCollection();
			registerRoutes(routeConfig);

			HttpContextBase mockHttp = MockHelpers.HttpContext(null).Object;
			RequestContext context = new RequestContext(mockHttp, new RouteData());

			// Act (generate a URL)
			return routeConfig.GetVirtualPath(context, routeValues);
		}
	}
}
