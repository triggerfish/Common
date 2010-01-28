using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using System.Web.Routing;
using System.Text.RegularExpressions;

namespace Triggerfish.Web.Routing
{
	/// <summary>
	/// Extension methods for url processing
	/// </summary>
    public static class RouteValueDictionaryExtensions
    {
		/// <summary>
		/// Adds a returnUrl parameter to the RouteValueDictionary
		/// </summary>
		/// <param name="route">The RouteValueDictionary object to amend</param>
		/// <param name="url">The return url</param>
		/// <returns>The amended RouteValueDictionary object</returns>
		public static RouteValueDictionary AddReturnUrl(this RouteValueDictionary route, string url)
		{
			if (null != url && !String.IsNullOrEmpty(url))
			{
				route.Add("returnUrl", url);
			}
			return route;
		}

		/// <summary>
		/// Encodes the individual route values to be lower-case and containing hypens
		/// instead of spaces.
		/// It does not alter the controller and action route values
		/// </summary>
		/// <param name="route">Route value dictionary containing the route data</param>
		public static RouteValueDictionary Encode(this RouteValueDictionary route)
		{
			return route.Convert(@" ", @"-", true);
		}

		/// <summary>
		/// Decodes the individual route values to replace the hypens with spaces.
		/// It does not alter the case of the value and does not alter the controller 
		/// and action route values
		/// </summary>
		/// <param name="route">Route value dictionary containing the route data</param>
		public static RouteValueDictionary Decode(this RouteValueDictionary route)
		{
			return route.Convert(@"-", @" ", false);
		}

		private static RouteValueDictionary Convert(this RouteValueDictionary route, string pattern, string replacement, bool toLower)
		{
			foreach (string key in route.Select(kvp => kvp.Key).ToList())
			{
				if (route[key] is string)
				{
					string value = ((string)route[key]).Trim();
					if (toLower)
					{
						value = value.ToLower();
					}
					route[key] = Regex.Replace(value, pattern, replacement);
				}
			}

			return route;
		}
	}
}
