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
		/// <param name="a_ri">The RouteInformation object</param>
		/// <param name="a_allowAuthoriseAttributeOnAction">Applies if the url is valid and the controller action to which the url refers has an AuthorizeAttribute. 
		/// If true, the AuthorizeAttribute is allowed and the original url is returned. If false, the AuthorizeAttribute
		/// is not allowed and the default home url is returned ("/")</param>
		/// <returns>A sanitised valid url for the site</returns>
		public static string SanitiseUrl(this RouteInformation a_ri, bool a_allowAuthoriseAttributeOnAction)
		{
			if (a_ri.Valid && (a_allowAuthoriseAttributeOnAction || !a_ri.DoesActionRequireAuthorisation(Assembly.GetCallingAssembly())))
			{
				return a_ri.Url;
			}

			return "/"; // return home url by default
		}
	}
}
