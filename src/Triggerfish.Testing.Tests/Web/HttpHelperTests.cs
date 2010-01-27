using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Triggerfish.Web.Testing;
using System.Web;

namespace Triggerfish.Testing.Tests
{
	[TestClass]
	public class HttpHelperTests
	{
		[TestMethod]
		public void ShouldCreateMockHttpContext()
		{
			string url = "http://www.a.com/b";
			HttpContextBase context = HttpHelpers.MockHttpContext(url);
			Assert.AreNotEqual(null, context.Request);
			Assert.AreEqual(url, context.Request.AppRelativeCurrentExecutionFilePath);
			Assert.AreNotEqual(null, context.Response);
		}
	}
}
