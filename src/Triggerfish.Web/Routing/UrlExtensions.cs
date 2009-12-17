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
    public static class UrlExtensions
    {
		/// <summary>
		/// Encodes the individual route values to be lower-case and containing hypens
		/// instead of spaces.
		/// It does not alter the controller and action route values
		/// </summary>
		/// <param name="a_route">Route value dictionary containing the route data</param>
		public static RouteValueDictionary Encode(this RouteValueDictionary a_route)
		{
			return a_route.Convert(@" ", @"-");
		}

		/// <summary>
		/// Decodes the individual route values to replace the hypens with spaces.
		/// It does not alter the case of the value and does not alter the controller 
		/// and action route values
		/// </summary>
		/// <param name="a_route">Route value dictionary containing the route data</param>
		public static RouteValueDictionary Decode(this RouteValueDictionary a_route)
		{
			return a_route.Convert(@"-", @" ");
		}

		private static RouteValueDictionary Convert(this RouteValueDictionary a_route, string a_pattern, string a_replacement)
		{
			foreach (string key in a_route.Select(kvp => kvp.Key).ToList())
			{
				string value = ((string)a_route[key]).Trim().ToLower();
				a_route[key] = Regex.Replace(value, a_pattern, a_replacement);
			}

			return a_route;
		}
	}
}
