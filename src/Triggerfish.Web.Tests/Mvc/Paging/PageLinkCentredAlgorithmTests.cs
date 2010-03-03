using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Triggerfish.Web.Mvc;

namespace Triggerfish.Web.Tests
{
	[TestClass]
	public class PageLinkCentredAlgorithmTests
	{
		private const int k_pageCount = 6;
		private const int k_nrPagesPerPage = 3;

		[TestMethod]
		public void ShouldHaveCorrectIndexNumbers()
		{
			// arrange
			PageLinkCentredAlgorithm alg = new PageLinkCentredAlgorithm();

			// act
			List<PageLink> links = alg.GetPages(0, 6, 6).ToList(); // gets all links

			// assert
			for (int i = 0; i < 6; i++)
			{
				Assert.AreEqual(i, links[i].Index);
			}
		}

		[TestMethod]
		public void ShouldHaveCorrectIndexNumbersWhenLinksPerPageGreaterThanPages()
		{
			// arrange
			PageLinkCentredAlgorithm alg = new PageLinkCentredAlgorithm();

			// act
			List<PageLink> links = alg.GetPages(0, 5, 6).ToList(); // gets all links

			// assert
			for (int i = 0; i < 5; i++)
			{
				Assert.AreEqual(i, links[i].Index);
			}
		}

		[TestMethod]
		public void ShouldOnlyHaveOneOfEachFlagSet()
		{
			// arrange
			PageLinkCentredAlgorithm alg = new PageLinkCentredAlgorithm();

			// act
			IEnumerable<PageLink> links = alg.GetPages(0, 6, 6); // gets all links

			// assert
			Assert.IsTrue(1 == links.Where(l => l.IsSelected).Count());
			Assert.IsTrue(1 == links.Where(l => l.IsFirst).Count());
			Assert.IsTrue(1 == links.Where(l => l.IsLast).Count());
		}

		[TestMethod]
		public void ShouldCalculatePageLinksCorrectlyFirstPage()
		{
			int index = 0;

			// act
			List<PageLink> links = GetPages(index);

			// assert
			TestFlags(links, true, false, index);
			TestReturnedIndexes(links, 0); // 0 1 2
		}

		[TestMethod]
		public void ShouldCalculatePageLinksCorrectlySecondPage()
		{
			int index = 1;

			// act
			List<PageLink> links = GetPages(index);

			// assert
			TestFlags(links, true, false, index);
			TestReturnedIndexes(links, 0); // 0 1 2
		}

		[TestMethod]
		public void ShouldCalculatePageLinksCorrectlyThirdPage()
		{
			int index = 2;

			// act
			List<PageLink> links = GetPages(index);

			// assert
			TestFlags(links, false, false, index);
			TestReturnedIndexes(links, 1); // 1 2 3
		}

		[TestMethod]
		public void ShouldCalculatePageLinksCorrectlyForthPage()
		{
			int index = 3;

			// act
			List<PageLink> links = GetPages(index);

			// assert
			TestFlags(links, false, false, index);
			TestReturnedIndexes(links, 2); // 2 3 4
		}

		[TestMethod]
		public void ShouldCalculatePageLinksCorrectlyFifthPage()
		{
			int index = 4;

			// act
			List<PageLink> links = GetPages(index);

			// assert
			TestFlags(links, false, true, index);
			TestReturnedIndexes(links, 3); // 3 4 5
		}

		[TestMethod]
		public void ShouldCalculatePageLinksCorrectlySixthPage()
		{
			int index = 5;

			// act
			List<PageLink> links = GetPages(index);

			// assert
			TestFlags(links, false, true, index);
			TestReturnedIndexes(links, 3); // 3 4 5
		}

		private List<PageLink> GetPages(int pageIndex)
		{
			PageLinkCentredAlgorithm alg = new PageLinkCentredAlgorithm();

			return alg.GetPages(pageIndex, k_pageCount, k_nrPagesPerPage).ToList();
		}

		private void TestFlags(List<PageLink> links, bool first, bool last, int selected)
		{
			Assert.AreEqual(first, links.First().IsFirst);
			Assert.AreEqual(last, links.Last().IsLast);
			Assert.IsTrue(links.FirstOrDefault(l => l.Index == selected).IsSelected);
		}
	
		private void TestReturnedIndexes(List<PageLink> links, int start)
		{
			for (int i = 0; i < k_nrPagesPerPage; i++)
			{
				Assert.AreEqual(i + start, links[i].Index);
			}
		}
	}
}
