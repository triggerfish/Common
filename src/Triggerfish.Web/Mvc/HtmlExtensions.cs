using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace Triggerfish.Web.Mvc
{
	/// <summary>
	/// HtmlHElper extension methods
	/// </summary>
	public static class HtmlExtensions
	{
		/// <summary>
		/// Creates a html anchor tag for the Hyperlink object
		/// </summary>
		/// <param name="html">Extension method on HtmlHelper</param>
		/// <param name="link">The Hyperlink</param>
		/// <returns>An html string</returns>
		public static string RouteLink(this HtmlHelper html, Hyperlink link)
		{
			return html.RouteLink(link, null);
		}

		/// <summary>
		/// Creates a html anchor tag for the Hyperlink object
		/// </summary>
		/// <param name="html">Extension method on HtmlHelper</param>
		/// <param name="link">The Hyperlink</param>
		/// <param name="cssSelectedClass">The CSS class name to apply if the link is selected</param>
		/// <returns>An html string</returns>
		public static string RouteLink(this HtmlHelper html, Hyperlink link, string cssSelectedClass)
		{
			Dictionary<string, object> attrs = new Dictionary<string, object>();
			if (link.IsSelected && !String.IsNullOrEmpty(cssSelectedClass))
			{
				attrs.Add("class", cssSelectedClass);
			}
			return html.RouteLink(link.Text, link.Route, attrs);
		}
	}
}
