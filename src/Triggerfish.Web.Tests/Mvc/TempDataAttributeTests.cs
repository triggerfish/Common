using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Triggerfish.Web.Mvc;

namespace Triggerfish.Web.Tests
{
	[TestClass]
	public class TempDataAttributeTests
	{
		[TestMethod]
		public void ShouldGetDataObject()
		{
			// arrange
			ControllerContext context = new ControllerContext();
			context.Controller = new MockController();
			context.Controller.TempData["Test"] = "DataValue";
			MockAttribute attr = new MockAttribute("Test");

			// act
			string s = attr.Get(context) as string;

			// assert
			Assert.AreNotEqual(null, s);
			Assert.AreEqual("DataValue", s);
		}

		[TestMethod]
		public void ShouldSetDataObject()
		{
			// arrange
			ControllerContext context = new ControllerContext();
			MockController c = new MockController();
			context.Controller = c;
			MockAttribute attr = new MockAttribute("Test");

			// act
			attr.Set(context, "DataValue");

			// assert
			Assert.IsTrue(c.TempData.ContainsKey("Test"));
			Assert.AreEqual("DataValue", (string)c.TempData["Test"]);
		}
	
		[TestMethod]
		public void ShouldRemoveDataObject()
		{
			// arrange
			ControllerContext context = new ControllerContext();
			MockController c = new MockController();
			context.Controller = c;
			context.Controller.TempData["Test"] = "DataValue";
			MockAttribute attr = new MockAttribute("Test");

			// act
			attr.Remove(context);

			// assert
			Assert.IsFalse(c.TempData.ContainsKey("Test"));
		}
	}

	internal class MockAttribute : TempDataAttribute
	{
		public MockAttribute(string key) : base(key) { }
	}
}
