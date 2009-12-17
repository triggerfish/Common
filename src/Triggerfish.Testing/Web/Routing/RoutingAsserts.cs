using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Routing;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Triggerfish.Testing.Web.Routing
{
	/// <summary>
	/// Assert methods for use in unit test projects
	/// </summary>
	public static class RoutingAssert
	{
		/// <summary>
		/// Tests whether two RouteValueDictionary objects have equal values
		/// </summary>
		/// <param name="a_expected">The expected values</param>
		/// <param name="a_actual">The actual values</param>
		public static void AreDictionariesEqual(RouteValueDictionary a_expected, RouteValueDictionary a_actual)
		{
			if (ReferenceEquals(a_expected, a_actual))
				return;
			Assert.AreNotEqual(null, a_expected);
			Assert.AreNotEqual(null, a_actual);
			Assert.AreEqual(a_expected.Count, a_actual.Count);

			foreach (var kvp in a_expected)
			{
				Assert.IsTrue(a_actual.ContainsKey(kvp.Key));
				Assert.AreEqual(kvp.Value, a_actual[kvp.Key]);
			}
		}
	}
}
