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
		[TestMethod]
		public void ShouldDoNothingIfAuthenticated()
		{
			// arrange
			Mock<HttpContextBase> httpContext = MockHelpers.HttpContext("").WithUser(MockHelpers.User().WithIdentity(MockHelpers.Identity().WithAuthenticationStatus(true)));
			ActionExecutingContext context = new ActionExecutingContext();
			context.HttpContext = httpContext.Object;

			AuthoriseAttribute attr = new AuthoriseAttribute();

			// act
			attr.OnActionExecuting(context);

			// assert
			Assert.AreEqual(null, context.Result);
		}

		[TestMethod]
		public void ShouldRedirectIfNotAuthenticated()
		{
			// arrange
			Mock<HttpContextBase> httpContext = MockHelpers.HttpContext("").WithUser(MockHelpers.User().WithIdentity(MockHelpers.Identity().WithAuthenticationStatus(false)));
			ActionExecutingContext context = new ActionExecutingContext();
			context.HttpContext = httpContext.Object;

			AuthoriseAttribute attr = new AuthoriseAttribute { RedirectTo = "/here" };

			// act
			attr.OnActionExecuting(context);

			// assert
			Assert.IsTrue(context.Result is RedirectResult);
			RedirectResult redirect = context.Result as RedirectResult;
			Assert.AreEqual("/here", redirect.Url);
		}
	
		[TestMethod]
		public void ShouldAuthoriseIfNotAuthenticated()
		{
			// arrange
			Mock<HttpContextBase> httpContext = MockHelpers.HttpContext("").WithUser(MockHelpers.User().WithIdentity(MockHelpers.Identity().WithAuthenticationStatus(false)));
			ActionExecutingContext context = new ActionExecutingContext();
			context.HttpContext = httpContext.Object;

			AuthoriseAttribute attr = new AuthoriseAttribute { RedirectTo = "/here", DoAuthorise = true };

			// act
			attr.OnActionExecuting(context);

			// assert
			Assert.IsTrue(context.Result is HttpUnauthorizedResult);
		}
	}
}
