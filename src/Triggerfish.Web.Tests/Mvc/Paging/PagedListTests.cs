using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Triggerfish.Web.Mvc;

namespace Triggerfish.Web.Tests
{
	[TestClass]
	public class PagedListTests
	{
		[TestMethod]
		public void ShouldCalculatePageItemsCorrectly()
		{
			// arrange
			List<int> list = new List<int> {
				0, 1, 2, 3, 4, 5, 6, 7, 8, 9
			};
			PagedList<int> pages = new PagedList<int>(null, null);

			// act
			pages.Paginate(list, 0, 3);

			// assert
			Assert.AreEqual(3, pages.Items.Count);
			Assert.AreEqual(4, pages.PageCount);
			for (int i = 0; i < 3; i++)
			{
				Assert.AreEqual(list[i], pages.Items[i]);
			}
		}

		[TestMethod]
		public void ShouldCalculateSinglePageItemsCorrectly()
		{
			// arrange
			List<int> list = new List<int> {
				0, 1, 2, 3, 4, 5, 6, 7, 8, 9
			};
			PagedList<int> pages = new PagedList<int>(null, null);

			// act
			pages.Paginate(list, 0, 10);

			// assert
			Assert.AreEqual(10, pages.Items.Count);
			Assert.AreEqual(1, pages.PageCount);
			for (int i = 0; i < 10; i++)
			{
				Assert.AreEqual(list[i], pages.Items[i]);
			}
		}

		[TestMethod]
		public void ShouldAcceptNullList()
		{
			// arrange
			PagedList<int> pages = new PagedList<int>(null, null);

			// act
			pages.Paginate(null, 0, 3);

			// assert
			Assert.AreEqual(0, pages.Items.Count);
			Assert.AreEqual(0, pages.PageCount);
		}

		[TestMethod]
		public void ShouldReturnSinglePage()
		{
			// arrange
			List<int> list = new List<int> {
				0, 1, 2, 3, 4, 5, 6, 7, 8, 9
			};
			PagedList<int> pages = new PagedList<int>(null, null);

			// act
			pages.Paginate(list, 0, 10);
			IEnumerable<PageLink> links = pages.GetPageLinks(3);

			Assert.AreEqual(1, links.Count());
			PageLink link = links.First();
			Assert.AreEqual(0, link.Index);
			Assert.IsTrue(link.IsFirst);
			Assert.IsTrue(link.IsLast);
			Assert.IsTrue(link.IsSelected);
		}

		[TestMethod]
		public void ShouldThrowIfNegativePageIndexGiven()
		{
			// arrange
			PagedList<int> pages = new PagedList<int>(null, null);

			// act
			try
			{
				pages.Paginate(null, -1, 3);
			}
			catch (ArgumentOutOfRangeException)
			{
				// expected 
				return;
			}
			Assert.Fail("Should throw if negative pageIndex specified");
		}

		[TestMethod]
		public void ShouldThrowIfPageIndexGreaterThanPageCountGiven()
		{
			// arrange
			List<int> list = new List<int> {
				0, 1, 2, 3, 4, 5, 6, 7, 8, 9
			};
			PagedList<int> pages = new PagedList<int>(null, null);

			// act
			try
			{
				pages.Paginate(list, 4, 3);
			}
			catch (ArgumentOutOfRangeException)
			{
				// expected 
				return;
			}
			Assert.Fail("Should throw if negative pageIndex specified");
		}

		[TestMethod]
		public void ShouldThrowIfPageSizeLessThan1()
		{
			// arrange
			PagedList<int> pages = new PagedList<int>(null, null);

			// act
			try
			{
				pages.Paginate(null, 0, 0);
			}
			catch (ArgumentOutOfRangeException)
			{
				// expected 
				return;
			}
			Assert.Fail("Should throw if pageSize < 1");
		}

		[TestMethod]
		public void ShouldThrowIfNoAlgorithmWhenGettingPageLinks()
		{
			// arrange
			List<int> list = new List<int> {
				0, 1, 2, 3, 4, 5, 6, 7, 8, 9
			};
			PagedList<int> pages = new PagedList<int>(null, null);

			// act
			try
			{
				pages.Paginate(list, 0, 3);
				pages.GetPageLinks(3);
			}
			catch (InvalidOperationException)
			{
				// expected 
				return;
			}
			Assert.Fail("Should throw if no algorithm specified");
		}

		[TestMethod]
		public void ShouldThrowIfNoHtmlGeneratorWhenGettingPageLinksHtml()
		{
			// arrange
			List<int> list = new List<int> {
				0, 1, 2, 3, 4, 5, 6, 7, 8, 9
			};
			PagedList<int> pages = new PagedList<int>(null, null);

			// act
			try
			{
				pages.Paginate(list, 0, 3);
				pages.GetPageLinksHtml(3, x => {return "";});
			}
			catch (InvalidOperationException)
			{
				// expected 
				return;
			}
			Assert.Fail("Should throw if no html generator specified");
		}
	}
}
