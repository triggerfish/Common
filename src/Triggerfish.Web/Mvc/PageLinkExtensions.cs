using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace Triggerfish.Web.Mvc
{
	/// <summary>
	/// Extension methods for pagination
	/// </summary>
	public static class PageLinkExtensions
	{
		/// <summary>
		/// Create an HTML anchor tag
		/// </summary>
		/// <param name="a_page">The page link object to which the anchor applies</param>
		/// <param name="a_cssClass">The CSS class style to apply</param>
		/// <param name="a_href">The link</param>
		/// <returns>The HTML string</returns>
		public static string ToAnchor(this PageLink a_page, string a_cssClass, string a_href)
		{
			return HtmlHelpers.CreateAnchor(a_cssClass, a_href, a_page.Number);
		}

		/// <summary>
		/// Create an HTML span tag
		/// </summary>
		/// <param name="a_page">The page link object to which the span applies</param>
		/// <param name="a_cssClass">The CSS class style to apply</param>
		/// <returns></returns>
		public static string ToSpan(this PageLink a_page, string a_cssClass)
		{
			return HtmlHelpers.CreateSpan(a_cssClass, a_page.Number);
		}
	}
}
