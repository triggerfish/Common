using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Triggerfish.Web.Mvc
{
	/// <summary>
	/// Interface for a page link html generator
	/// </summary>
	public interface IPageLinkHtmlGenerator
	{
		/// <summary>
		/// Convert the PageLink list into HTML
		/// </summary>
		/// <param name="pageLinks">The page links from which to generate the html</param>
		/// <param name="pageCount">The total number of pages</param>
		/// <param name="currentPage">The current displayed page index</param>
		/// <param name="pageLinksPerPageCount">The number of page links to be displayed per page</param>
		/// <param name="pageUrlGenerator">Delegate to return the URL for the given page index</param>
		/// <returns>The HTML string</returns>
		string ToHtml(IEnumerable<PageLink> pageLinks, int pageCount, int currentPage, int pageLinksPerPageCount, Func<int, string> pageUrlGenerator);
	}
}
