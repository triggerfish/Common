using System;
using System.Web;
using System.Web.Security;

namespace Triggerfish.Web.Security
{
	/// <summary>
	/// WebForms authentication provider
	/// </summary>
	public class FormsAuthenticationProvider : IAuthenticationProvider
	{
		/// <summary>
		/// Record the specified user as logged in (credentials verification must
		/// have been performed before calling this method)
		/// </summary>
		/// <param name="name">The name of the user</param>
		/// <param name="createPersistentCookie">true to persist the login using a cookie, false to expire on session end</param>
		public void Login(string name, bool createPersistentCookie)
		{
			FormsAuthentication.SetAuthCookie(name, createPersistentCookie);
		}

		/// <summary>
		/// Logout the current user
		/// </summary>
		public void Logout()
		{
			FormsAuthentication.SignOut();
		}
	}
}
