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
		/// <param name="page">The page link object to which the anchor applies</param>
		/// <param name="cssClass">The CSS class style to apply</param>
		/// <param name="href">The link</param>
		/// <returns>The HTML string</returns>
		public static string ToAnchor(this PageLink page, string cssClass, string href)
		{
			return HtmlHelpers.CreateAnchor(cssClass, href, page.Number);
		}

		/// <summary>
		/// Create an HTML span tag
		/// </summary>
		/// <param name="page">The page link object to which the span applies</param>
		/// <param name="cssClass">The CSS class style to apply</param>
		/// <returns></returns>
		public static string ToSpan(this PageLink page, string cssClass)
		{
			return HtmlHelpers.CreateSpan(cssClass, page.Number);
		}
	}
}
