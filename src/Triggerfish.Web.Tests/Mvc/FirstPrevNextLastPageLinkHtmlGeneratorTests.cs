using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Triggerfish.Web.Mvc;

namespace Triggerfish.Web.Tests
{
	[TestClass]
	public class FirstPrevNextLastPageLinkHtmlGeneratorTests
	{
		[TestMethod]
		public void ShouldGenerateHtmlForSinglePage()
		{
			// arrange
			List<PageLink> pageLinks = new List<PageLink> { new PageLink { Index = 0, IsFirst = true, IsLast = true, IsSelected = true } };
			FirstPrevNextLastPageLinkHtmlGenerator gen = new FirstPrevNextLastPageLinkHtmlGenerator();

			// act
			string html = gen.ToHtml(pageLinks, 1, 0, 3, GenerateUrl);
				
			// assert
			Assert.AreEqual(@"<span class=""disabled first"">&#171; First</span><span class=""disabled prev"">&#8249; Prev</span><span class=""selected"">1</span><span class=""disabled next"">Next &#8250;</span><span class=""disabled last"">Last &#187;</span>", html);
		}

		[TestMethod]
		public void ShouldGenerateHtmlFirstPage()
		{
			// act
			string html = GetHtml(0, 0, 2);

			// assert
			Assert.AreEqual(@"<span class=""disabled first"">&#171; First</span><span class=""disabled prev"">&#8249; Prev</span><span class=""selected"">1</span><a href=""page1"">2</a><a href=""page2"">3</a><a class=""next"" href=""page1"">Next &#8250;</a><a class=""last"" href=""page4"">Last &#187;</a>", html);
		}

		[TestMethod]
		public void ShouldGenerateHtmlSecondPage()
		{
			// act
			string html = GetHtml(1, 0, 2);

			// assert
			Assert.AreEqual(@"<a class=""first"" href=""page0"">&#171; First</a><a class=""prev"" href=""page0"">&#8249; Prev</a><a href=""page0"">1</a><span class=""selected"">2</span><a href=""page2"">3</a><a class=""next"" href=""page2"">Next &#8250;</a><a class=""last"" href=""page4"">Last &#187;</a>", html);
		}

		[TestMethod]
		public void ShouldGenerateHtmlPenultimatePage()
		{
			// act
			string html = GetHtml(3, 2, 4);

			// assert
			Assert.AreEqual(@"<a class=""first"" href=""page0"">&#171; First</a><a class=""prev"" href=""page2"">&#8249; Prev</a><a href=""page2"">3</a><span class=""selected"">4</span><a href=""page4"">5</a><a class=""next"" href=""page4"">Next &#8250;</a><a class=""last"" href=""page4"">Last &#187;</a>", html);
		}

		[TestMethod]
		public void ShouldGenerateHtmlLastPage()
		{
			// act
			string html = GetHtml(4, 2, 4);

			// assert
			Assert.AreEqual(@"<a class=""first"" href=""page0"">&#171; First</a><a class=""prev"" href=""page3"">&#8249; Prev</a><a href=""page2"">3</a><a href=""page3"">4</a><span class=""selected"">5</span><span class=""disabled next"">Next &#8250;</span><span class=""disabled last"">Last &#187;</span>", html);
		}

		public static string GenerateUrl(int i)
		{
			return "page" + i;
		}

		private string GetHtml(int pageIndex, int pageStartIndex, int pageEndIndex)
		{
			// page 0: 0, 1
			// page 1: 2, 3
			// page 2: 4, 5
			// page 3: 6, 7
			// page 4: 8, 9

			// arrange
			List<PageLink> pageLinks = new List<PageLink>();
			
			for (int i = pageStartIndex; i <= pageEndIndex; i++)
			{ 
				pageLinks.Add(new PageLink { Index = i, IsFirst = (i == 0), IsLast = (i == 4), IsSelected = (i == pageIndex) });
			};
			
			FirstPrevNextLastPageLinkHtmlGenerator gen = new FirstPrevNextLastPageLinkHtmlGenerator();

			// act
			return gen.ToHtml(pageLinks, 5, pageIndex, 3, GenerateUrl);
		}
	}
}
