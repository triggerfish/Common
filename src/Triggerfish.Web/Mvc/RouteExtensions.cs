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
        /// <param name="routes">Route collection to add routes to.</param>
        public static void RegisterRoutes(this RouteCollection routes)
        {
            // Enumerate assembly for UrlRoute attributes.
			List<RouteParser> parsers = RouteParser.CreateFromAttributes(Assembly.GetCallingAssembly());

			parsers.Sort((a, b) => a.Order.CompareTo(b.Order));

            // Add the routes to the routes collection.
			foreach (RouteParser r in parsers)
            {
				routes.Add(r.RouteName, new FriendlyUrlRoute(r.Url, r.Defaults, r.Constraints, new MvcRouteHandler()));
            }
        }

		/// <summary>
		/// Adds a route entry to the route collection
		/// </summary>
		/// <param name="routes">The route collection</param>
		/// <param name="name">The name for the route (can be null)</param>
		/// <param name="url">The url</param>
		public static void MapFriendlyUrlRoute(this RouteCollection routes, string name, string url)
		{
			routes.Add(name, new FriendlyUrlRoute(url, new MvcRouteHandler()));
		}

		/// <summary>
		/// Adds a route entry to the route collection
		/// </summary>
		/// <param name="routes">The route collection</param>
		/// <param name="name">The name for the route (can be null)</param>
		/// <param name="url">The url</param>
		/// <param name="defaults">The route default values</param>
		public static void MapFriendlyUrlRoute(this RouteCollection routes, string name, string url, object defaults)
		{
			routes.Add(name, new FriendlyUrlRoute(url, new MvcRouteHandler())	{
				Defaults = new RouteValueDictionary(defaults)
			});
		}

		/// <summary>
		/// Adds a route entry to the route collection
		/// </summary>
		/// <param name="routes">The route collection</param>
		/// <param name="name">The name for the route (can be null)</param>
		/// <param name="url">The url</param>
		/// <param name="defaults">The route default values</param>
		/// <param name="constraints">The route constraints</param>
		public static void MapFriendlyUrlRoute(this RouteCollection routes, string name, string url, object defaults, object constraints)
		{
			routes.Add(name, new FriendlyUrlRoute(url, new MvcRouteHandler())
			{
				Defaults = new RouteValueDictionary(defaults),
				Constraints = new RouteValueDictionary(constraints)
			});
		}
	}
}
