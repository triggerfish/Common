using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Routing;

namespace Triggerfish.Web.Mvc.Testing
{
	/// <summary>
	/// Static helper methods for processing urls
	/// </summary>
	public static class UrlHelpers
	{
		/// <summary>
		/// Sanitises a url and ensures the url is valid for a registered route.
		/// If the route is not valid the default home url is returned ("/")
		/// </summary>
		/// <param name="a_url">The url to sanitise</param>
		/// <param name="a_registerRoutes">The route registration delegate</param>
		/// <returns>A sanitised valid url for the site</returns>
		public static string SanitiseUrl(string a_url, Action<RouteCollection> a_registerRoutes)
		{
			return SanitiseUrl(a_url, a_registerRoutes, true);
		}

		/// <summary>
		/// Sanitises a url and ensures the url is valid for a registered route.
		/// If the route is not valid the default home url is returned ("/")
		/// </summary>
		/// <param name="a_url">The url to sanitise</param>
		/// <param name="a_registerRoutes">The route registration delegate</param>
		/// <param name="a_allowAuthoriseAttributeOnAction">Applies if the url is valid and the controller action to which the url refers has an AuthorizeAttribute. 
		/// If true, the AuthorizeAttribute is allowed and the original url is returned. If false, the AuthorizeAttribute
		/// is not allowed and the default home url is returned ("/")</param>
		/// <returns>A sanitised valid url for the site</returns>
		public static string SanitiseUrl(string a_url, Action<RouteCollection> a_registerRoutes, bool a_allowAuthoriseAttributeOnAction)
		{
			return RouteInformation.Create(a_url, a_registerRoutes).SanitiseUrl(a_allowAuthoriseAttributeOnAction);
		}
	}
}
