using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Triggerfish.Web.Mvc;
using Triggerfish.Web.Testing;
using System.Web;

namespace Triggerfish.Web.Tests
{
	[TestClass]
	public class AuthoriseAttributeTests
	{
		private AuthorizationContext m_context = new AuthorizationContext();
		private Mock<HttpContextBase> m_httpContext;

		[TestMethod]
		public void ShouldDoNothingIfAuthenticated()
		{
			// arrange
			m_httpContext.WithUser(MockHelpers.User().WithIdentity(MockHelpers.Identity().WithAuthenticationStatus(true)));

			AuthoriseAttribute attr = new AuthoriseAttribute();

			// act
			attr.OnAuthorization(m_context);

			// assert
			Assert.AreEqual(null, m_context.Result);
		}

		[TestMethod]
		public void ShouldRedirectIfNotAuthenticated()
		{
			// arrange
			AuthoriseAttribute attr = new AuthoriseAttribute { RedirectTo = "/here" };

			// act
			attr.OnAuthorization(m_context);

			// assert
			Assert.IsTrue(m_context.Result is RedirectResult);
			RedirectResult redirect = m_context.Result as RedirectResult;
			Assert.AreEqual("/here", redirect.Url);
		}
	
		[TestMethod]
		public void ShouldAuthoriseIfNotAuthenticated()
		{
			// arrange
			AuthoriseAttribute attr = new AuthoriseAttribute { RedirectTo = "/here", DoAuthorise = true };

			// act
			attr.OnAuthorization(m_context);

			// assert
			Assert.IsTrue(m_context.Result is HttpUnauthorizedResult);
		}

		[TestInitialize]
		public void SetupTest()
		{
			Mock<HttpResponseBase> response = new Mock<HttpResponseBase>();
			response.Setup(x => x.Cache).Returns(new Mock<HttpCachePolicyBase>().Object);
			m_httpContext = MockHelpers.HttpContext("");
			m_httpContext.WithUser(MockHelpers.User().WithIdentity(MockHelpers.Identity().WithAuthenticationStatus(false)));
			m_httpContext.Setup(x => x.Response).Returns(response.Object);
			m_context.HttpContext = m_httpContext.Object;
		}
	}
}
