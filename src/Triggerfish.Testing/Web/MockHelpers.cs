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
	/// Helpers for mocking objects
	/// </summary>
	public static class MockHelpers
	{
		/// <summary>
		/// Create a mock HttpContext object
		/// </summary>
		/// <param name="url">The url that initiated the HttpContext (can be null)</param>
		/// <returns>The http context mock object</returns>
		public static Mock<HttpContextBase> HttpContext(string url)
		{
			var context = new Mock<HttpContextBase>();

			// Mock the request
			var request = new Mock<HttpRequestBase>();
			context
				.Setup(x => x.Request)
				.Returns(request.Object);
			request
				.Setup(x => x.AppRelativeCurrentExecutionFilePath)
				.Returns(url);

			// Mock the response
			var mockResponse = new Mock<HttpResponseBase>();
			context
				.Setup(x => x.Response)
				.Returns(mockResponse.Object);
			mockResponse
				.Setup(x => x.ApplyAppPathModifier(It.IsAny<string>()))
				.Returns<string>(x => x);

			return context;
		}

		/// <summary>
		/// Creates a mock object for a http session
		/// </summary>
		/// <returns>The mock object</returns>
		public static Mock<HttpSessionStateBase> Session()
		{
			return new Mock<HttpSessionStateBase>();
		}
	
		/// <summary>
		/// Creates a mock object for a user
		/// </summary>
		/// <returns>The mock object</returns>
		public static Mock<IPrincipal> User()
		{
			return new Mock<IPrincipal>();
		}

		/// <summary>
		/// Creates a mock object for an identity
		/// </summary>
		/// <returns>The mock object</returns>
		public static Mock<IIdentity> Identity()
		{
			return new Mock<IIdentity>();
		}
	}
}
