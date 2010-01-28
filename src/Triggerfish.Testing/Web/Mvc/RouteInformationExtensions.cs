using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Routing;
using System.Reflection;

namespace Triggerfish.Web.Mvc.Testing
{
	/// <summary>
	/// Static helper methods for processing urls
	/// </summary>
	public static class RouteInformationExtensions
	{
		/// <summary>
		/// Sanitises a url and ensures the url is valid for a registered route.
		/// If the route is not valid the default home url is returned ("/")
		/// </summary>
		/// <param name="ri">The RouteInformation object</param>
		/// <returns>A sanitised valid url for the site</returns>
		public static string SanitiseUrl(this RouteInformation ri)
		{
			return ri.SanitiseUrl(true, Assembly.GetCallingAssembly());
		}

		/// <summary>
		/// Sanitises a url and ensures the url is valid for a registered route.
		/// If the route is not valid the default home url is returned ("/")
		/// </summary>
		/// <param name="ri">The RouteInformation object</param>
		/// <param name="allowAuthoriseAttributeOnAction">Applies if the url is valid and the controller action to which the url refers has an AuthorizeAttribute. 
		/// If true, the AuthorizeAttribute is allowed and the original url is returned. If false, the AuthorizeAttribute
		/// is not allowed and the default home url is returned ("/")</param>
		/// <returns>A sanitised valid url for the site</returns>
		public static string SanitiseUrl(this RouteInformation ri, bool allowAuthoriseAttributeOnAction)
		{
			return ri.SanitiseUrl(allowAuthoriseAttributeOnAction, Assembly.GetCallingAssembly());
		}

		/// <summary>
		/// Sanitises a url and ensures the url is valid for a registered route.
		/// If the route is not valid the default home url is returned ("/")
		/// </summary>
		/// <param name="ri">The RouteInformation object</param>
		/// <param name="allowAuthoriseAttributeOnAction">Applies if the url is valid and the controller action to which the url refers has an AuthorizeAttribute. 
		/// If true, the AuthorizeAttribute is allowed and the original url is returned. If false, the AuthorizeAttribute
		/// is not allowed and the default home url is returned ("/")</param>
		/// <param name="callingAssembly">The assembly with the controller/action</param>
		/// <returns>A sanitised valid url for the site</returns>
		public static string SanitiseUrl(this RouteInformation ri, bool allowAuthoriseAttributeOnAction, Assembly callingAssembly)
		{
			if (ri.Valid && (allowAuthoriseAttributeOnAction || !ri.DoesActionRequireAuthorisation(callingAssembly)))
			{
				return ri.Url;
			}

			return "/"; // return home url by default
		}
	}
}
