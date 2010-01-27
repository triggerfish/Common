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
	public class ImportFromTempDataAttributeTests
	{
		[TestMethod]
		public void ShouldImportDataIfOfCorrectType()
		{
			// arrange
			ActionExecutingContext context = new ActionExecutingContext();
			MockController c = new MockController();
			context.Controller = c;
			c.TempData["Test"] = "DataValue";
			MockImportAttribute attr = new MockImportAttribute("Test", typeof(string));

			// act
			attr.OnActionExecuting(context);

			// assert
			Assert.AreNotEqual(null, attr.Model);
			Assert.AreEqual("DataValue", (string)attr.Model);
		}
	
		[TestMethod]
		public void ShouldRemoveDataIfNotOfCorrectType()
		{
			// arrange
			ActionExecutingContext context = new ActionExecutingContext();
			MockController c = new MockController();
			context.Controller = c;
			c.TempData["Test"] = "DataValue";
			MockImportAttribute attr = new MockImportAttribute("Test", typeof(int));

			// act
			attr.OnActionExecuting(context);

			// assert
			Assert.AreEqual(null, attr.Model);
			Assert.IsFalse(c.TempData.ContainsKey("Test"));
		}
	}

	internal class MockImportAttribute : ImportFromTempDataAttribute
	{
		public object Model { get; private set; }

		public MockImportAttribute(string key, Type type) : base(key, type) { }

		protected override void SetModel(object a_model, ControllerBase a_controller)
		{
			Model = a_model;
		}
	}
}
