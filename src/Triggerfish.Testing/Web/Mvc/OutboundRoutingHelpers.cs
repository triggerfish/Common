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
		/// <param name="a_routeValues">The route values from which to generate the url</param>
		/// <param name="a_route">The route to use to generate the url</param>
		/// <returns>The string url</returns>
		public static string GenerateOutboundUrl(object a_routeValues, Route a_route)
		{
			return GenerateOutboundVirtualPath(a_routeValues, a_route).VirtualPath;
		}

		/// <summary>
		/// Generates an outbound url from the specified values.  
		/// </summary>
		/// <param name="a_routeValues">The route values from which to generate the url</param>
		/// <param name="a_route">The route to use to generate the url</param>
		/// <returns>The string url</returns>
		public static string GenerateOutboundUrl(RouteValueDictionary a_routeValues, Route a_route)
		{
			return GenerateOutboundVirtualPath(a_routeValues, a_route).VirtualPath;
		}

		/// <summary>
		/// Generates an outbound url from the specified values.  
		/// </summary>
		/// <param name="a_routeValues">The route values from which to generate the url</param>
		/// <param name="a_route">The route to use to generate the url</param>
		/// <returns>The virtual path data object</returns>
		public static VirtualPathData GenerateOutboundVirtualPath(object a_routeValues, Route a_route)
		{
			return GenerateOutboundVirtualPath(new RouteValueDictionary(a_routeValues), a_route);
		}

		/// <summary>
		/// Generates an outbound url from the specified values.  
		/// </summary>
		/// <param name="a_routeValues">The route values from which to generate the url</param>
		/// <param name="a_route">The route to use to generate the url</param>
		/// <returns>The virtual path data object</returns>
		public static VirtualPathData GenerateOutboundVirtualPath(RouteValueDictionary a_routeValues, Route a_route)
		{
			HttpContextBase mockHttp = HttpHelpers.MockHttpContext(null);
			RequestContext context = new RequestContext(mockHttp, new RouteData());

			// Act (generate a URL)
			return a_route.GetVirtualPath(context, a_routeValues);
		}

		/// <summary>
		/// Generates an outbound url from the specified values.  Requires a delegate
		/// to populate a route collection with all possible routes.
		/// </summary>
		/// <param name="a_routeValues">The route values from which to generate the url</param>
		/// <param name="a_registerRoutes">Delegate to register all possible routes</param>
		/// <returns>The string url</returns>
		public static string GenerateOutboundUrl(object a_routeValues, Action<RouteCollection> a_registerRoutes)
		{
			return GenerateOutboundVirtualPath(a_routeValues, a_registerRoutes).VirtualPath;
		}

		/// <summary>
		/// Generates an outbound url from the specified values.  Requires a delegate
		/// to populate a route collection with all possible routes.
		/// </summary>
		/// <param name="a_routeValues">The route values from which to generate the url</param>
		/// <param name="a_registerRoutes">Delegate to register all possible routes</param>
		/// <returns>The string url</returns>
		public static string GenerateOutboundUrl(RouteValueDictionary a_routeValues, Action<RouteCollection> a_registerRoutes)
		{
			return GenerateOutboundVirtualPath(a_routeValues, a_registerRoutes).VirtualPath;
		}

		/// <summary>
		/// Generates an outbound url from the specified values.  Requires a delegate
		/// to populate a route collection with all possible routes.
		/// </summary>
		/// <param name="a_routeValues">The route values from which to generate the url</param>
		/// <param name="a_registerRoutes">Delegate to register all possible routes</param>
		/// <returns>The virtual path data object</returns>
		public static VirtualPathData GenerateOutboundVirtualPath(object a_routeValues, Action<RouteCollection> a_registerRoutes)
		{
			return GenerateOutboundVirtualPath(new RouteValueDictionary(a_routeValues), a_registerRoutes);
		}

		/// <summary>
		/// Generates an outbound url from the specified values.  Requires a delegate
		/// to populate a route collection with all possible routes.
		/// </summary>
		/// <param name="a_routeValues">The route values from which to generate the url</param>
		/// <param name="a_registerRoutes">Delegate to register all possible routes</param>
		/// <returns>The virtual path data object</returns>
		public static VirtualPathData GenerateOutboundVirtualPath(RouteValueDictionary a_routeValues, Action<RouteCollection> a_registerRoutes)
		{
			// Arrange (get the routing config and test context)
			RouteCollection routeConfig = new RouteCollection();
			a_registerRoutes(routeConfig);

			HttpContextBase mockHttp = HttpHelpers.MockHttpContext(null);
			RequestContext context = new RequestContext(mockHttp, new RouteData());

			// Act (generate a URL)
			return routeConfig.GetVirtualPath(context, a_routeValues);
		}
	}
}
