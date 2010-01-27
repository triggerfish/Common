using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace Triggerfish.Web.Mvc
{
	/// <summary>
	/// Created HTML page links with a format:
	/// First | Prev | 1 2 3 4 | Next | Last
	/// </summary>
	public class FirstPrevNextLastPageLinkHtmlGenerator : IPageLinkHtmlGenerator
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
		public string ToHtml(IEnumerable<PageLink> pageLinks, int pageCount, int currentPage, int pageLinksPerPageCount, Func<int, string> pageUrlGenerator)
		{
			if (null == pageLinks)
			{
				return "";
			}

			PageUrlGenerator urlGenerator = new PageUrlGenerator(pageUrlGenerator);
			int pageIndex = currentPage;

			PageLink selected = pageLinks.FirstOrDefault(p => p.IsSelected);

			StringBuilder html = new StringBuilder();

			if (!selected.IsFirst)
			{
				html.Append(HtmlHelpers.CreateAnchor("first", urlGenerator[0], "&#171; First"));
				html.Append(HtmlHelpers.CreateAnchor("prev", urlGenerator[pageIndex - 1], "&#8249; Prev"));
			}
			else
			{
				html.Append(HtmlHelpers.CreateSpan("disabled first", "&#171; First"));
				html.Append(HtmlHelpers.CreateSpan("disabled prev", "&#8249; Prev"));
			}

			foreach (PageLink page in pageLinks)
			{
				if (page.IsSelected)
				{
					html.Append(page.ToSpan("selected"));
				}
				else
				{
					html.Append(page.ToAnchor(null, urlGenerator[page.Index]));
				}
			}

			if (!selected.IsLast)
			{
				html.Append(HtmlHelpers.CreateAnchor("next", urlGenerator[pageIndex + 1], "Next &#8250;"));
				html.Append(HtmlHelpers.CreateAnchor("last", urlGenerator[pageCount - 1], "Last &#187;"));
			}
			else
			{
				html.Append(HtmlHelpers.CreateSpan("disabled next", "Next &#8250;"));
				html.Append(HtmlHelpers.CreateSpan("disabled last", "Last &#187;"));
			}

			return html.ToString();
		}
	}
}
