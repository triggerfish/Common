using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Triggerfish.Web.Mvc
{
	/// <summary>
	/// Calculates a list of page links where the current page is centred in the list
	/// For example, the following page links would be displayed for a list of 
	/// 6 pages with a max 3 page links per page:
	/// 
	/// Page 0: 0 1 2
	/// Page 1: 0 1 2
	/// Page 2: 1 2 3
	/// Page 3: 2 3 4
	/// Page 4: 3 4 5
	/// Page 5: 3 4 5
	///		
	/// </summary>
	public class PageLinkCentredAlgorithm : IPageLinksAlgorithm
	{
		/// <summary>
		/// Calculates the page links to be displayed on the current page
		/// </summary>
		/// <param name="currentPage">The current selected page</param>
		/// <param name="pageCount">The total number of pages</param>
		/// <param name="pageLinksPerPageCount">The number of page links to be displayed per page</param>
		/// <returns>A list of PageLink objects</returns>
		public IEnumerable<PageLink> GetPages(int currentPage, int pageCount, int pageLinksPerPageCount)
		{
			int lastPageIndex = pageCount - 1;
			int toTheLeft = (int)Math.Ceiling(pageLinksPerPageCount / 2d) - 1;

			int start = currentPage - toTheLeft;

			if (start < 0)
			{
				start = 0;
			}

			int end = start + pageLinksPerPageCount;

			if (end > pageCount)
			{
				end = pageCount;
				start = pageCount - pageLinksPerPageCount;

				if (start < 0)
					start = 0;
			}

			List<PageLink> pages = new List<PageLink>();
			for (int i = start; i < end; i++)
			{
				pages.Add(new PageLink { Index = i, IsSelected = (i == currentPage), IsFirst = (i == 0), IsLast = (i == lastPageIndex) });
			}
			return pages;
		}
	}
}
