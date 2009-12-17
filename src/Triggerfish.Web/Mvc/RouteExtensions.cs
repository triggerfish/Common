using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using System.Web.Routing;
using System.Text.RegularExpressions;
using Triggerfish.Web.Routing;

namespace Triggerfish.Web.Mvc
{
	/// <summary>
	/// Extension methods for MVC routing
	/// </summary>
    public static class RouteExtensions
    {
        /// <summary>
        /// Uses reflection to enumerate all Controller classes in the
        /// assembly and registers a route for each method declaring a
        /// UrlRoute attribute.
        /// </summary>
        /// <param name="a_routes">Route collection to add routes to.</param>
        public static void RegisterRoutes(this RouteCollection a_routes)
        {
            // Enumerate assembly for UrlRoute attributes.
			List<RouteParser> parsers = RouteParser.CreateFromAttributes(Assembly.GetCallingAssembly());

			parsers.Sort((a, b) => a.Order.CompareTo(b.Order));

            // Add the routes to the routes collection.
			foreach (RouteParser r in parsers)
            {
				a_routes.Add(r.RouteName, new FriendlyUrlRoute(r.Url, r.Defaults, r.Constraints, new MvcRouteHandler()));
            }
        }

		/// <summary>
		/// Adds a route entry to the route collection
		/// </summary>
		/// <param name="a_routes">The route collection</param>
		/// <param name="a_name">The name for the route (can be null)</param>
		/// <param name="a_url">The url</param>
		public static void MapFriendlyUrlRoute(this RouteCollection a_routes, string a_name, string a_url)
		{
			a_routes.Add(a_name, new FriendlyUrlRoute(a_url, new MvcRouteHandler()));
		}

		/// <summary>
		/// Adds a route entry to the route collection
		/// </summary>
		/// <param name="a_routes">The route collection</param>
		/// <param name="a_name">The name for the route (can be null)</param>
		/// <param name="a_url">The url</param>
		/// <param name="a_defaults">The route default values</param>
		public static void MapFriendlyUrlRoute(this RouteCollection a_routes, string a_name, string a_url, object a_defaults)
		{
			a_routes.Add(a_name, new FriendlyUrlRoute(a_url, new MvcRouteHandler())	{
				Defaults = new RouteValueDictionary(a_defaults)
			});
		}

		/// <summary>
		/// Adds a route entry to the route collection
		/// </summary>
		/// <param name="a_routes">The route collection</param>
		/// <param name="a_name">The name for the route (can be null)</param>
		/// <param name="a_url">The url</param>
		/// <param name="a_defaults">The route default values</param>
		/// <param name="a_constraints">The route constraints</param>
		public static void MapFriendlyUrlRoute(this RouteCollection a_routes, string a_name, string a_url, object a_defaults, object a_constraints)
		{
			a_routes.Add(a_name, new FriendlyUrlRoute(a_url, new MvcRouteHandler())
			{
				Defaults = new RouteValueDictionary(a_defaults),
				Constraints = new RouteValueDictionary(a_constraints)
			});
		}
	}
}
