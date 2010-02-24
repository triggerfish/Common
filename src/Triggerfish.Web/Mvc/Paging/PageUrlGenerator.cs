using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Triggerfish.Web.Mvc
{
	internal class PageUrlGenerator
	{
		private Func<int, string> m_delegate;
		private Dictionary<int, string> m_urls = new Dictionary<int, string>();

		public string this[int index]
		{
			get
			{
				if (!m_urls.ContainsKey(index))
				{
					m_urls.Add(index, m_delegate(index));
				}

				return m_urls[index];
			}
		}

		public PageUrlGenerator(Func<int, string> del)
		{
			m_delegate = del;
		}
	}
}
