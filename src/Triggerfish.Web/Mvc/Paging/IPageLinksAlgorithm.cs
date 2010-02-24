using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Triggerfish.Web.Mvc
{
	/// <summary>
	/// Interface for an algorithm to calculate which page links appear on a page
	/// </summary>
	public interface IPageLinksAlgorithm
	{
		/// <summary>
		/// Calculate which page links to display on a page
		/// </summary>
		/// <param name="currentPage">The current selected page</param>
		/// <param name="pageCount">The total number of pages</param>
		/// <param name="pageLinksPerPageCount">The number of page links to be displayed per page</param>
		/// <returns>A list of PageLink objects</returns>
		IEnumerable<PageLink> GetPages(int currentPage, int pageCount, int pageLinksPerPageCount);
	}
}
