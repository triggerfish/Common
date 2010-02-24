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
	public class MockHelperTests
	{
		[TestMethod]
		public void ShouldCreateMockHttpContext()
		{
			string url = "http://www.a.com/b";
			HttpContextBase context = MockHelpers.HttpContext(url).Object;
			Assert.AreNotEqual(null, context.Request);
			Assert.AreEqual(url, context.Request.AppRelativeCurrentExecutionFilePath);
			Assert.AreNotEqual(null, context.Response);
		}
	}
}
