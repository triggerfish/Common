using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using System.Web.Routing;
using System.Text.RegularExpressions;

namespace Triggerfish
{
	/// <summary>
	/// Extension methods for url processing
	/// </summary>
    public static class StringExtensions
    {
		/// <summary>
		/// Converts the first character of a string to uppercase
		/// </summary>
		/// <param name="a_string">The string to convert</param>
		/// <returns>A copy of the string with the first character capitalised</returns>
		public static string Capitalise(this string a_string)
		{
			if (null == a_string)
				return null;

			if (a_string.Length <= 1)
				return a_string.ToUpper();

			char[] c = a_string.ToCharArray();
			c[0] = Char.ToUpper(c[0]);
			return new String(c);
		}
	}
}
