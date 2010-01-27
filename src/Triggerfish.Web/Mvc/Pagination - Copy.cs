using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Triggerfish.Web.Mvc
{
	public class Page
	{
		public int Index { get; set; }
		public string Number { get { return (Index + 1).ToString(); } }
		public bool IsSelected { get; set; }
		public bool IsFirst { get; set; }
		public bool IsLast { get; set; }
	}

	public class Pagination<T>
	{
		public List<T> Items { get; private set; }
		public List<Page> Pages { get; private set; }
		public int PageCount { get; private set; }
		public int PageIndex { get; private set; }

		public Pagination(IEnumerable<T> source, int pageIndex, int pageSize)
		{
			Items = new List<T>();
			Pages = new List<Page>();
			if (null == source)
			{
				source = new List<T>();
			}

			if (pageIndex < 0)
			{
				throw new ArgumentOutOfRangeException("PageIndex cannot be less than 0.");
			}
			PageIndex = pageIndex;

			if (pageSize < 1)
			{
				throw new ArgumentOutOfRangeException("PageSize cannot be less than 1.");
			}

			// PageSize = 3
			// Items: A B C | D E F | G H I | J
			// Pages:   0       1       2     3

			int itemCount = source.Count();
			if (itemCount > 0)
			{
				int pageCount = (int)Math.Ceiling(itemCount / (double)pageSize);

				if (pageIndex >= pageCount)
				{
					throw new ArgumentOutOfRangeException("PageIndex cannot be greater than the number of pages");
				}
				PageCount = pageCount;

				Items = new List<T>();
				Items.AddRange(source.Skip(pageIndex * pageSize).Take(pageSize).ToList());

				List<Page> pages = new List<Page>();
				for (int i = 0; i < pageCount; i++)
				{
					pages.Add(new Page { Index = i, IsSelected = (pageIndex == i) });
				}
				pages.First().IsFirst = true;
				pages.Last().IsLast = true;
				Pages = pages;
			}
		}

		public IEnumerable<Page> GetPageLinks(int maxLinkCount)
		{
			int pageCount = PageCount;

			// 0 1 2 | 3 4 5 | 6 7 8 | 9
			//           -
			// 0 * 3 = 0
			// 1 * 3 = 3
			// 2 * 3 = 6

			int start = pageCount - 1;
			for (int i = 0; i < pageCount - 1; i++)
			{
				if (PageIndex >= (i * maxLinkCount) && PageIndex < ((i + 1) * maxLinkCount))
				{
					start = i;
					break;
				}
			}

			int take = pageCount - start;
			if (take > maxLinkCount)
			{
				take = maxLinkCount;
			}
			else if (take < maxLinkCount)
			{
				take = maxLinkCount;
				start = pageCount - maxLinkCount;

				if (start < 0)
					start = 0;
			}

			return Pages.Skip(start).Take(take);
		}
	}
}
