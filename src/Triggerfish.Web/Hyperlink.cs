using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Routing;

namespace Triggerfish.Web
{
	/// <summary>
	/// Represents the data required for a html hyperlink
	/// </summary>
	public class Hyperlink
	{
		/// <summary>
		/// The type of http action the link performs
		/// </summary>
		public HttpAction ActionType { get; set; }

		/// <summary>
		/// The name of the link as seen by the user (anchor inner html)
		/// </summary>
		public string Text { get; set; }
		
		/// <summary>
		/// The route values for the link
		/// </summary>
		public RouteValueDictionary Route { get; set; }
		
		/// <summary>
		/// True if the link is selected (currently displayed), false otherwise
		/// </summary>
		public bool IsSelected { get; set; }
	}
}
