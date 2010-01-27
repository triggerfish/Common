using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace Triggerfish.Web.Mvc
{
	/// <summary>
	/// Helper classes for building HTML strings
	/// </summary>
	public static class HtmlHelpers
	{
		/// <summary>
		/// Creates an anchor tag
		/// </summary>
		/// <param name="a_cssClass">The class attribute value, can be null</param>
		/// <param name="a_href">The href attribute value</param>
		/// <param name="a_text">The anchor element text</param>
		/// <returns>An HTML anchor tag</returns>
		public static string CreateAnchor(string a_cssClass, string a_href, string a_text)
		{
			TagBuilder a = new TagBuilder("a");
			if (!String.IsNullOrEmpty(a_cssClass))
				a.AddCssClass(a_cssClass);
			a.MergeAttribute("href", a_href);
			a.InnerHtml = a_text;
			return a.ToString();
		}

		/// <summary>
		/// Creates a span tag
		/// </summary>
		/// <param name="a_cssClass">The class attribute value, can be null</param>
		/// <param name="a_text">The span element text</param>
		/// <returns>An HTML span tag</returns>
		public static string CreateSpan(string a_cssClass, string a_text)
		{
			TagBuilder span = new TagBuilder("span");
			if (!String.IsNullOrEmpty(a_cssClass))
				span.AddCssClass(a_cssClass);
			span.InnerHtml = a_text;
			return span.ToString();
		}
	}
}
