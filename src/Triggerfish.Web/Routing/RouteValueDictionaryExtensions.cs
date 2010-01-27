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
		/// <param name="a_route">The RouteValueDictionary object to amend</param>
		/// <param name="a_url">The return url</param>
		/// <returns>The amended RouteValueDictionary object</returns>
		public static RouteValueDictionary AddReturnUrl(this RouteValueDictionary a_route, string a_url)
		{
			if (null != a_url && !String.IsNullOrEmpty(a_url))
			{
				a_route.Add("returnUrl", a_url);
			}
			return a_route;
		}

		/// <summary>
		/// Encodes the individual route values to be lower-case and containing hypens
		/// instead of spaces.
		/// It does not alter the controller and action route values
		/// </summary>
		/// <param name="a_route">Route value dictionary containing the route data</param>
		public static RouteValueDictionary Encode(this RouteValueDictionary a_route)
		{
			return a_route.Convert(@" ", @"-", true);
		}

		/// <summary>
		/// Decodes the individual route values to replace the hypens with spaces.
		/// It does not alter the case of the value and does not alter the controller 
		/// and action route values
		/// </summary>
		/// <param name="a_route">Route value dictionary containing the route data</param>
		public static RouteValueDictionary Decode(this RouteValueDictionary a_route)
		{
			return a_route.Convert(@"-", @" ", false);
		}

		private static RouteValueDictionary Convert(this RouteValueDictionary a_route, string a_pattern, string a_replacement, bool toLower)
		{
			foreach (string key in a_route.Select(kvp => kvp.Key).ToList())
			{
				if (a_route[key] is string)
				{
					string value = ((string)a_route[key]).Trim();
					if (toLower)
					{
						value = value.ToLower();
					}
					a_route[key] = Regex.Replace(value, a_pattern, a_replacement);
				}
			}

			return a_route;
		}
	}
}
