using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Triggerfish.Web.Mvc
{
	/// <summary>
	/// Alternative to the Microsoft AuthorizeAttribute, which completely overrides
	/// any other attribute filters
	/// </summary>
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
	public class AuthoriseAttribute : AuthorizeAttribute
	{
		/// <summary>
		/// A url to redirect to if not authenticated. Not applicable
		/// if DoAuthorise is true
		/// </summary>
		public string RedirectTo { get; set; }

		/// <summary>
		/// Performs a 401 redirect to run authorisation as with
		/// Microsofts AuthorizeAttribute
		/// </summary>
		public bool DoAuthorise { get; set; }

		/// <summary>
		/// Default constructor
		/// </summary>
		public AuthoriseAttribute()
		{
			RedirectTo = "/"; // default home
			DoAuthorise = false;
		}

		/// <summary>
		/// Authorisation handler
		/// </summary>
		/// <param name="filterContext">The context</param>
		public override void OnAuthorization(AuthorizationContext filterContext)
		{
			base.OnAuthorization(filterContext);

			// authorisation is required, but we want to override the default behaviour to
			// redirect away from the page
			if (filterContext.Result is HttpUnauthorizedResult && !DoAuthorise)
			{
				filterContext.Result = new RedirectResult(RedirectTo);
			}
		}
	}
}
