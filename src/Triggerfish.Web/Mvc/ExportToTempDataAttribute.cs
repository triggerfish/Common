using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Triggerfish.Web.Mvc
{
	/// <summary>
	/// Enumeration to specify under what conditions the data should be
	/// exported
	/// </summary>
	public enum EExportWhen
	{
		/// <summary>
		/// Export the data if ModelState.IsValid is true
		/// </summary>
		ModelStateValid,
		/// <summary>
		/// Export the data if ModelState.IsValid is false
		/// </summary>
		ModelStateInvalid
	}	
	
	/// <summary>
	/// Abstract class to export data to TempData
	/// </summary>
	public abstract class ExportToTempDataAttribute : TempDataAttribute
	{
		/// <summary>
		/// Property for the EExportWhen value
		/// </summary>
		protected EExportWhen ExportWhen { get; private set; }

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="a_key">The key with which to store data in TempData</param>
		/// <param name="a_when">The EExportWhen value</param>
		protected ExportToTempDataAttribute(string a_key, EExportWhen a_when)
			: base(a_key)
		{
			ExportWhen = a_when;
		}

		/// <summary>
		/// Called after the action method executes. Perform the export of data
		/// to TempData
		/// </summary>
		/// <param name="filterContext">The filter context</param>
		public override void OnActionExecuted(ActionExecutedContext filterContext)
		{
			bool valid = filterContext.Controller.ViewData.ModelState.IsValid;
			// only export when ModelState is valid
			if (ExportWhen == EExportWhen.ModelStateValid && valid ||
				ExportWhen == EExportWhen.ModelStateInvalid && !valid)
			{
				Set(filterContext, GetModel(filterContext.Controller));
			}

			base.OnActionExecuted(filterContext);
		}

		/// <summary>
		/// Abstract method to get the actual data object from the Controller object
		/// </summary>
		/// <param name="a_controller">The controller object</param>
		/// <returns>The object to export to TempData</returns>
		protected abstract object GetModel(ControllerBase a_controller);
	}
}
