using System;
using System.Text;

namespace Triggerfish.Web.Security
{
	/// <summary>
	/// Interface for an authentication provider
	/// </summary>
	public interface IAuthenticationProvider
	{
		/// <summary>
		/// Record the specified user as logged in (credentials verification must
		/// have been performed before calling this method)
		/// </summary>
		/// <param name="name">The name of the user</param>
		/// <param name="createPersistentCookie">true to persist the login using a cookie, false to expire on session end</param>
		void Login(string name, bool createPersistentCookie);

		/// <summary>
		/// Logout the current user
		/// </summary>
		void Logout();
	}
}
