using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Routing;
using Moq;

namespace Triggerfish.Web.Testing
{
	/// <summary>
	/// Helpers for mocking http objects
	/// </summary>
	public static class HttpHelpers
	{
		/// <summary>
		/// Create a mock HttpContext object
		/// </summary>
		/// <param name="url">The url that initiated the HttpContext (can be null)</param>
		/// <returns>The http context</returns>
		public static HttpContextBase MockHttpContext(string url)
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

			return context.Object;
		}
	}
}
