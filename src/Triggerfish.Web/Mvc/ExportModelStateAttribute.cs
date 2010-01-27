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
		/// <param name="a_key">The key with which to store data in TempData</param>
		public ExportModelStateAttribute(string a_key) : base(a_key, EExportWhen.ModelStateInvalid) { }

		/// <summary>
		/// Abstract method implementation to get the object in ViewData.ModelState
		/// </summary>
		/// <param name="a_controller">The controller object</param>
		/// <returns>The ViewData.ModelState object</returns>
		protected override object GetModel(ControllerBase a_controller)
		{
			return a_controller.ViewData.ModelState;
		}
	}
}
