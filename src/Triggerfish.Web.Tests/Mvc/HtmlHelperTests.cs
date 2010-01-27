using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Triggerfish.Web.Mvc;

namespace Triggerfish.Web.Tests
{
	[TestClass]
	public class HtmlHelperTests
	{
		[TestMethod]
		public void ShouldGenerateAnchorWithCSS()
		{
			Assert.AreEqual(@"<a class=""test"" href=""1"">1</a>", HtmlHelpers.CreateAnchor("test", "1", "1"));
		}

		[TestMethod]
		public void ShouldGenerateAnchorWithoutCSS()
		{
			Assert.AreEqual(@"<a href=""1"">1</a>", HtmlHelpers.CreateAnchor(null, "1", "1"));
		}

		[TestMethod]
		public void ShouldGenerateSpanWithCSS()
		{
			Assert.AreEqual(@"<span class=""test"">1</span>", HtmlHelpers.CreateSpan("test", "1"));
		}

		[TestMethod]
		public void ShouldGenerateSpanWithoutCSS()
		{
			Assert.AreEqual(@"<span>1</span>", HtmlHelpers.CreateSpan(null, "1"));
		}
	}
}
