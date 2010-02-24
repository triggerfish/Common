using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Triggerfish.Web.Mvc
{
	/// <summary>
	/// Exports the ViewData.ModelState object to TempData
	/// </summary>
	public class ExportModelStateAttribute : ExportToTempDataAttribute
	{
		/// <summary>
		/// Constuctor
		/// </summary>
		/// <param name="key">The key with which to store data in TempData</param>
		public ExportModelStateAttribute(string key) : base(key, EExportWhen.ModelStateInvalid) { }

		/// <summary>
		/// Abstract method implementation to get the object in ViewData.ModelState
		/// </summary>
		/// <param name="controller">The controller object</param>
		/// <returns>The ViewData.ModelState object</returns>
		protected override object GetModel(ControllerBase controller)
		{
			return controller.ViewData.ModelState;
		}
	}
}
