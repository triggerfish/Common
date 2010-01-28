using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Routing;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Triggerfish.Web.Routing.Testing
{
	/// <summary>
	/// Assert methods for use in unit test projects
	/// </summary>
	public static class RoutingAssert
	{
		/// <summary>
		/// Tests whether two RouteValueDictionary objects have equal values
		/// </summary>
		/// <param name="expected">The expected values</param>
		/// <param name="actual">The actual values</param>
		public static void AreDictionariesEqual(RouteValueDictionary expected, RouteValueDictionary actual)
		{
			if (ReferenceEquals(expected, actual))
				return;
			Assert.AreNotEqual(null, expected);
			Assert.AreNotEqual(null, actual);
			Assert.AreEqual(expected.Count, actual.Count);

			foreach (var kvp in expected)
			{
				Assert.IsTrue(actual.ContainsKey(kvp.Key));
					Assert.AreEqual(kvp.Value, actual[kvp.Key]);
			}
		}
	}
}
