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
		/// <param name="cssClass">The class attribute value, can be null</param>
		/// <param name="href">The href attribute value</param>
		/// <param name="text">The anchor element text</param>
		/// <returns>An HTML anchor tag</returns>
		public static string CreateAnchor(string cssClass, string href, string text)
		{
			TagBuilder a = new TagBuilder("a");
			if (!String.IsNullOrEmpty(cssClass))
				a.AddCssClass(cssClass);
			a.MergeAttribute("href", href);
			a.InnerHtml = text;
			return a.ToString();
		}

		/// <summary>
		/// Creates a span tag
		/// </summary>
		/// <param name="cssClass">The class attribute value, can be null</param>
		/// <param name="text">The span element text</param>
		/// <returns>An HTML span tag</returns>
		public static string CreateSpan(string cssClass, string text)
		{
			TagBuilder span = new TagBuilder("span");
			if (!String.IsNullOrEmpty(cssClass))
				span.AddCssClass(cssClass);
			span.InnerHtml = text;
			return span.ToString();
		}
	}
}
