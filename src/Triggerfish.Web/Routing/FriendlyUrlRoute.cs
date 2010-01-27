using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Routing;

namespace Triggerfish.Web.Routing
{
	/// <summary>
	/// Overrides the default route handling behaviour to alter the 
	/// route values to be more friendly
	/// </summary>
	public class FriendlyUrlRoute : Route
	{
		/// <summary>
		/// Constructor
		/// </summary>
		public FriendlyUrlRoute(string url, IRouteHandler routeHandler) 
			: base(url, routeHandler) { }
		/// <summary>
		/// Constructor
		/// </summary>
		public FriendlyUrlRoute(string url, RouteValueDictionary defaults, IRouteHandler routeHandler) 
			: base(url, defaults, routeHandler) { }
		/// <summary>
		/// Constructor
		/// </summary>
		public FriendlyUrlRoute(string url, RouteValueDictionary defaults, RouteValueDictionary constraints, IRouteHandler routeHandler) 
			: base(url, defaults, constraints, routeHandler) { }
		/// <summary>
		/// Constructor
		/// </summary>
		public FriendlyUrlRoute(string url, RouteValueDictionary defaults, RouteValueDictionary constraints, RouteValueDictionary dataTokens, IRouteHandler routeHandler) 
			: base(url, defaults, constraints, dataTokens, routeHandler) { }

		/// <summary>
		/// Returns data about the requested route (inbound request)
		/// </summary>
		/// <param name="httpContext">The http request information</param>
		/// <returns>The route definition values</returns>
		public override RouteData GetRouteData(HttpContextBase httpContext)
		{
			RouteData rd = base.GetRouteData(httpContext);
			if (null != rd)
			{
				rd.Values.Decode();
			}
			return rd;
		}

		/// <summary>
		/// Returns data about the url associated with the route (outbound request)
		/// </summary>
		/// <param name="requestContext">Context data about the requested route</param>
		/// <param name="values">The route values</param>
		/// <returns>Url data</returns>
		public override VirtualPathData GetVirtualPath(RequestContext requestContext, RouteValueDictionary values)
		{
			if (null != values)
			{
				values.Encode();
			}
			return base.GetVirtualPath(requestContext, values);
		}
	}
}
