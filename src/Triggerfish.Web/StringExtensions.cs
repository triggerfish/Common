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
		/// <param name="str">The string to convert</param>
		/// <returns>A copy of the string with the first character capitalised</returns>
		public static string Capitalise(this string str)
		{
			if (null == str)
				return null;

			if (str.Length <= 1)
				return str.ToUpper();

			char[] c = str.ToCharArray();
			c[0] = Char.ToUpper(c[0]);
			return new String(c);
		}
	}
}
