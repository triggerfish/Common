using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Routing;
using Moq;
using System.Security.Principal;

namespace Triggerfish.Web.Testing
{
	/// <summary>
	/// Extension methods to add further mocking behaviour
	/// </summary>
	public static class MockExtensions
	{
		/// <summary>
		/// Adds a mock session to a mock context
		/// </summary>
		/// <param name="context">The context mock</param>
		/// <param name="session">The session mock</param>
		/// <returns>The updated context mock</returns>
		public static Mock<HttpContextBase> WithSession(this Mock<HttpContextBase> context, Mock<HttpSessionStateBase> session)
		{
			context.Setup(x => x.Session).Returns(session.Object);
			return context;
		}

		/// <summary>
		/// Adds a user to the context mock
		/// </summary>
		/// <param name="context">The context mock</param>
		/// <param name="user">The user mock</param>
		/// <returns>The updated context mock</returns>
		public static Mock<HttpContextBase> WithUser(this Mock<HttpContextBase> context, Mock<IPrincipal> user)
		{
			context.Setup(x => x.User).Returns(user.Object);
			return context;
		}

		/// <summary>
		/// Updates a session with cached object
		/// </summary>
		/// <param name="session">The session mock</param>
		/// <param name="key">The key into the session</param>
		/// <param name="obj">The object to store under the key</param>
		/// <returns>The updated session mock</returns>
		public static Mock<HttpSessionStateBase> WithObject(this Mock<HttpSessionStateBase> session, string key, object obj)
		{
			session.Setup(x => x[key]).Returns(obj);
			return session;
		}

		/// <summary>
		/// Updates a user mock with an identity mock 
		/// </summary>
		/// <param name="user">The user mock</param>
		/// <param name="identity">The identity mock</param>
		/// <returns>The updated user mock</returns>
		public static Mock<IPrincipal> WithIdentity(this Mock<IPrincipal> user, Mock<IIdentity> identity)
		{
			user.Setup(x => x.Identity).Returns(identity.Object);
			return user;
		}
	
		/// <summary>
		/// Updates an identity mock with the authentication status
		/// </summary>
		/// <param name="identity">The identity mock</param>
		/// <param name="authenticated">true if authenticated, false if not</param>
		/// <returns>The identity user mock</returns>
		public static Mock<IIdentity> WithAuthenticationStatus(this Mock<IIdentity> identity, bool authenticated)
		{
			identity.Setup(x => x.IsAuthenticated).Returns(authenticated);
			return identity;
		}
	}
}
