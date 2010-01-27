using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Triggerfish.Web.Mvc
{
	/// <summary>
	/// Paginates a list of items
	/// </summary>
	/// <typeparam name="T">The item type</typeparam>
	public class PagedList<T>
	{
		private IPageLinksAlgorithm m_pageLinksAlgorithm;
		private IPageLinkHtmlGenerator m_pageLinksHtmlGenerator;

		/// <summary>
		/// Returns the items to be displayed in the current page
		/// </summary>
		public List<T> Items { get; private set; }

		/// <summary>
		/// The total number of pages to display the entire source list
		/// </summary>
		public int PageCount { get; private set; }

		/// <summary>
		/// The current displayed page
		/// </summary>
		public int CurrentPage { get; private set; }

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="a_algorithm">The algorithm to use to calculate the page links to display on the page</param>
		/// <param name="a_htmlGenerator">Interface with which to generate the page links html</param>
		public PagedList(IPageLinksAlgorithm a_algorithm, IPageLinkHtmlGenerator a_htmlGenerator)
		{
			m_pageLinksAlgorithm = a_algorithm;
			m_pageLinksHtmlGenerator = a_htmlGenerator;
		}

		/// <summary>
		/// Paginate the list
		/// </summary>
		/// <param name="source">The source list of items</param>
		/// <param name="currentPage">The current selected page</param>
		/// <param name="itemsPerPageCount">The number of items to display per page</param>
		public void Paginate(IEnumerable<T> source, int currentPage, int itemsPerPageCount)
		{
			Items = new List<T>();
			if (null == source)
			{
				source = new List<T>();
			}

			if (currentPage < 0)
			{
				throw new ArgumentOutOfRangeException("PageIndex cannot be less than 0.");
			}
			CurrentPage = currentPage;

			if (itemsPerPageCount < 1)
			{
				throw new ArgumentOutOfRangeException("PageSize cannot be less than 1.");
			}

			int itemCount = source.Count();
			if (itemCount > 0)
			{
				int pageCount = (int)Math.Ceiling(itemCount / (double)itemsPerPageCount);

				if (currentPage >= pageCount)
				{
					throw new ArgumentOutOfRangeException("PageIndex cannot be greater than the number of pages");
				}
				PageCount = pageCount;

				Items = new List<T>();
				Items.AddRange(source.Skip(currentPage * itemsPerPageCount).Take(itemsPerPageCount).ToList());
			}
		}

		/// <summary>
		/// Get the page links to display on the current page
		/// </summary>
		/// <param name="pageLinksPerPageCount">The number of links to display per page</param>
		/// <returns>A list of PageLink objects, or null if there is 0 or 1 pages</returns>
		public IEnumerable<PageLink> GetPageLinks(int pageLinksPerPageCount)
		{
			if (PageCount <= 1)
			{
				return new List<PageLink> { new PageLink { Index = 0, IsFirst = true, IsLast = true, IsSelected = true } };
			}

			if (null == m_pageLinksAlgorithm)
			{
				throw new InvalidOperationException("Algorithm not specified with which to calculate the page links");
			}

			return m_pageLinksAlgorithm.GetPages(CurrentPage, PageCount, pageLinksPerPageCount);
		}

		/// <summary>
		/// Get the HTML for the page links using the html generator
		/// </summary>
		/// <param name="pageLinksPerPageCount">The number of page links to be displayed per page</param>
		/// <param name="pageUrlGenerator">Delegate to return the URL for the given page index</param>
		/// <returns>The HTML string</returns>
		public string GetPageLinksHtml(int pageLinksPerPageCount, Func<int, string> pageUrlGenerator)
		{
			if (null == m_pageLinksHtmlGenerator)
			{
				throw new InvalidOperationException("HTML generator not specified with which to calculate the page links");
			}

			return m_pageLinksHtmlGenerator.ToHtml(GetPageLinks(pageLinksPerPageCount), PageCount, CurrentPage, pageLinksPerPageCount, pageUrlGenerator);
		}
	}
}
