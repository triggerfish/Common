using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Triggerfish.Web.Mvc
{
	/// <summary>
	/// A basic class to detail a page link although it doesn't actually
	/// include data for the href
	/// </summary>
	public class PageLink
	{
		/// <summary>
		/// The page index
		/// </summary>
		public int Index { get; set; }
		/// <summary>
		/// The page number (Index + 1)
		/// </summary>
		public string Number { get { return (Index + 1).ToString(); } }
		/// <summary>
		/// True if this page is currently selected, false otherwise
		/// </summary>
		public bool IsSelected { get; set; }
		/// <summary>
		/// True if this page is the first page, false otherwise
		/// </summary>
		public bool IsFirst { get; set; }
		/// <summary>
		/// True if this page is the last page, false otherwise
		/// </summary>
		public bool IsLast { get; set; }
	}
}
