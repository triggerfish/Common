using System.Web.Routing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Triggerfish.Web.Routing;

namespace Triggerfish
{
	[TestClass]
	public class StringExtensionsTests
	{
		[TestMethod]
		public void ShouldCapitaliseWord()
		{
			Assert.AreEqual("Plibble", "plibble".Capitalise());
		}
	}
}
