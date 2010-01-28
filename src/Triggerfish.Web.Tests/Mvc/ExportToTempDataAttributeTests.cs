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
	public class ExportToTempDataAttributeTests
	{
		[TestMethod]
		public void ShouldExportDataIfModelStateValid()
		{
			// arrange
			ActionExecutedContext context = new ActionExecutedContext();
			MockController c = new MockController();
			context.Controller = c;
			MockExportAttribute attr = new MockExportAttribute("Test", EExportWhen.ModelStateValid);

			// act
			attr.OnActionExecuted(context);

			// assert
			Assert.IsTrue(c.TempData.ContainsKey("Test"));
		}
	
		[TestMethod]
		public void ShouldNotExportDataIfModelStateValid()
		{
			// arrange
			ActionExecutedContext context = new ActionExecutedContext();
			MockController c = new MockController();
			context.Controller = c;
			c.ViewData.ModelState.AddModelError("", "Error");
			MockExportAttribute attr = new MockExportAttribute("Test", EExportWhen.ModelStateValid);

			// act
			attr.OnActionExecuted(context);

			// assert
			Assert.IsFalse(c.TempData.ContainsKey("Test"));
		}

		[TestMethod]
		public void ShouldExportDataIfModelStateInvalid()
		{
			// arrange
			ActionExecutedContext context = new ActionExecutedContext();
			MockController c = new MockController();
			context.Controller = c;
			c.ViewData.ModelState.AddModelError("", "Error");
			MockExportAttribute attr = new MockExportAttribute("Test", EExportWhen.ModelStateInvalid);

			// act
			attr.OnActionExecuted(context);

			// assert
			Assert.IsTrue(c.TempData.ContainsKey("Test"));
		}

		[TestMethod]
		public void ShouldNotExportDataIfModelStateInvalid()
		{
			// arrange
			ActionExecutedContext context = new ActionExecutedContext();
			MockController c = new MockController();
			context.Controller = c;
			MockExportAttribute attr = new MockExportAttribute("Test", EExportWhen.ModelStateInvalid);

			// act
			attr.OnActionExecuted(context);

			// assert
			Assert.IsFalse(c.TempData.ContainsKey("Test"));
		}
	}

	internal class MockExportAttribute : ExportToTempDataAttribute
	{
		public MockExportAttribute(string key, EExportWhen when) : base(key, when) { }

		protected override object GetModel(ControllerBase controller)
		{
			return "DataValue";
		}
	}
}
